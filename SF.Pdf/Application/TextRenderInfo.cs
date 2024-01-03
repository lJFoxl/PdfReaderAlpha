﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SF.Pdf.Application;
internal class TextRenderInfo
{
    private readonly PdfString @string;
    private string text = null;
    private Matrix textToUserSpaceTransformMatrix;
    private GraphicsState gs;
    private float? unscaledWidth = null;
    private double[] fontMatrix = null;

    /**
     * ! .NET SPECIFIC ! 
     * is used for caching "UTF-16BE" encoding to improve performance
     */
    private static Encoding utf_16BeEncoding;

    /**
     * Array containing marked content info for the text.
     * @since 5.0.2
     */
    private ICollection<MarkedContentInfo> markedContentInfos;

    /**
     * Creates a new TextRenderInfo object
     * @param string the PDF string that should be displayed
     * @param gs the graphics state (note: at this time, this is not immutable, so don't cache it)
     * @param textMatrix the text matrix at the time of the render operation
     * @param markedContentInfo the marked content sequence, if available
     */
    internal TextRenderInfo(PdfString @string, GraphicsState gs, Matrix textMatrix, ICollection markedContentInfo)
    {
        this.@string = @string;
        this.textToUserSpaceTransformMatrix = textMatrix.Multiply(gs.ctm);
        this.gs = gs;
        this.markedContentInfos = new List<MarkedContentInfo>();
        if (markedContentInfo.Count > 0)
        { // check for performance purposes, as markedContentInfo.GetEnumerator is a costly operation for some reason
            foreach (MarkedContentInfo m in markedContentInfo)
            {
                this.markedContentInfos.Add(m);
            }
        }
        this.fontMatrix = gs.font.GetFontMatrix();
    }

    /**
     * Used for creating sub-TextRenderInfos for each individual character
     * @param parent the parent TextRenderInfo
     * @param string the content of a TextRenderInfo
     * @param horizontalOffset the unscaled horizontal offset of the character that this TextRenderInfo represents
     * @since 5.3.3
     */
    private TextRenderInfo(TextRenderInfo parent, PdfString @string, float horizontalOffset)
    {
        this.@string = @string;
        this.textToUserSpaceTransformMatrix = new Matrix(horizontalOffset, 0).Multiply(parent.textToUserSpaceTransformMatrix);
        this.gs = parent.gs;
        this.markedContentInfos = parent.markedContentInfos;
        this.fontMatrix = gs.font.GetFontMatrix();
    }

    /**
     * @return the text to render
     */
    virtual public string GetText()
    {
        if (text == null)
            text = Decode(@string);
        return text;
    }

    /**
     * @return original PDF string
     */
    public virtual PdfString PdfString => @string;

    /**
     * Checks if the text belongs to a marked content sequence
     * with a given mcid.
     * @param mcid a marked content id
     * @return true if the text is marked with this id
     * @since 5.0.2
     */
    virtual public bool HasMcid(int mcid)
    {
        return HasMcid(mcid, false);
    }

    /**
     * Checks if the text belongs to a marked content sequence
     * with a given mcid.
     * @param mcid a marked content id
     * @param checkTheTopmostLevelOnly indicates whether to check the topmost level of marked content stack only
     * @return true if the text is marked with this id
     * @since 5.3.5
     */
    virtual public bool HasMcid(int mcid, bool checkTheTopmostLevelOnly)
    {
        if (checkTheTopmostLevelOnly)
        {
            if (markedContentInfos is IList)
            {
                var infoMcid = GetMcid();
                return (infoMcid != null) ? infoMcid == mcid : false;
            }
        }
        else
        {
            foreach (var info in markedContentInfos)
            {
                if (info.HasMcid())
                    if (info.GetMcid() == mcid)
                        return true;
            }
        }
        return false;
    }

    /**
     * @return the marked content associated with the TextRenderInfo instance.
     */
    virtual public int? GetMcid()
    {
        if (markedContentInfos is IList)
        {
            var mci = (IList<MarkedContentInfo>)markedContentInfos;
            // Java and C# Stack classes have different numeration direction, so top element of the stack is 
            // at last postion in Java and at first position in C#
            var info = mci.Count > 0 ? mci[0] : null;
            return (info != null && info.HasMcid()) ? (int?)info.GetMcid() : null;
        }
        return null;
    }

    /**
     * @return the unscaled (i.e. in Text space) width of the text
     */
    internal float GetUnscaledWidth()
    {
        if (unscaledWidth == null)
            unscaledWidth = GetPdfStringWidth(@string, false);
        return unscaledWidth.Value;
    }

    /**
     * Gets the baseline for the text (i.e. the line that the text 'sits' on)
     * This value includes the Rise of the draw operation - see {@link #getRise()} for the amount added by Rise
     * @return the baseline line segment
     * @since 5.0.2
     */
    virtual public LineSegment GetBaseline()
    {
        return GetUnscaledBaselineWithOffset(0 + gs.rise).TransformBy(textToUserSpaceTransformMatrix);
    }

    virtual public LineSegment GetUnscaledBaseline()
    {
        return GetUnscaledBaselineWithOffset(0 + gs.rise);
    }

    /**
     * Gets the ascentline for the text (i.e. the line that represents the topmost extent that a string of the current font could have)
     * This value includes the Rise of the draw operation - see {@link #getRise()} for the amount added by Rise
     * @return the ascentline line segment
     * @since 5.0.2
     */
    virtual public LineSegment GetAscentLine()
    {
        var ascent = gs.GetFont().GetFontDescriptor(BaseFont.ASCENT, gs.GetFontSize());
        return GetUnscaledBaselineWithOffset(ascent + gs.rise).TransformBy(textToUserSpaceTransformMatrix);
    }

    /**
     * Gets the descentline for the text (i.e. the line that represents the bottom most extent that a string of the current font could have)
     * This value includes the Rise of the draw operation - see {@link #getRise()} for the amount added by Rise
     * @return the descentline line segment
     * @since 5.0.2
     */
    virtual public LineSegment GetDescentLine()
    {
        // per GetFontDescription() API, descent is returned as a negative number, so we apply that as a normal vertical offset
        var descent = gs.GetFont().GetFontDescriptor(BaseFont.DESCENT, gs.GetFontSize());
        return GetUnscaledBaselineWithOffset(descent + gs.rise).TransformBy(textToUserSpaceTransformMatrix);
    }

    private LineSegment GetUnscaledBaselineWithOffset(float yOffset)
    {
        // we need to correct the width so we don't have an extra character and word spaces at the end.  The extra character and word spaces
        // are important for tracking relative text coordinate systems, but should not be part of the baseline
        var unicodeStr = @string.ToUnicodeString();

        var correctedUnscaledWidth = GetUnscaledWidth() - (gs.characterSpacing +
                                                           (unicodeStr.Length > 0 && unicodeStr[unicodeStr.Length - 1] == ' ' ? gs.wordSpacing : 0)) * gs.horizontalScaling;

        return new LineSegment(new Vector(0, yOffset, 1), new Vector(correctedUnscaledWidth, yOffset, 1));
    }

    /**
     * Getter for the font
     * @return the font
     * @since iText 5.0.2
     */
    virtual public DocumentFont GetFont()
    {
        return gs.GetFont();
    }


    // removing - this shouldn't be needed now that we are exposing getCharacterRenderInfos()
    //	/**
    //	 * @return The character spacing width, in user space units (Tc value, scaled to user space)
    //	 * @since 5.3.3
    //	 */
    //	public float getCharacterSpacing(){
    //		return convertWidthFromTextSpaceToUserSpace(gs.characterSpacing);
    //	}
    //	
    //	/**
    //	 * @return The word spacing width, in user space units (Tw value, scaled to user space)
    //	 * @since 5.3.3
    //	 */
    //	public float getWordSpacing(){
    //		return convertWidthFromTextSpaceToUserSpace(gs.wordSpacing);
    //	}

    /**
     * The rise represents how far above the nominal baseline the text should be rendered.  The {@link #getBaseline()}, {@link #getAscentLine()} and {@link #getDescentLine()} methods already include Rise.
     * This method is exposed to allow listeners to determine if an explicit rise was involved in the computation of the baseline (this might be useful, for example, for identifying superscript rendering)
     * @return The Rise for the text draw operation, in user space units (Ts value, scaled to user space)
     * @since 5.3.3
     */
    public virtual float GetRise()
    {
        if (gs.rise == 0) return 0; // optimize the common case

        return ConvertHeightFromTextSpaceToUserSpace(gs.rise);
    }

    /**
     *
     * @param width the width, in text space
     * @return the width in user space
     * @since 5.3.3
     */
    private float ConvertWidthFromTextSpaceToUserSpace(float width)
    {
        var textSpace = new LineSegment(new Vector(0, 0, 1), new Vector(width, 0, 1));
        var userSpace = textSpace.TransformBy(textToUserSpaceTransformMatrix);
        return userSpace.GetLength();
    }

    /**
     *
     * @param height the height, in text space
     * @return the height in user space
     * @since 5.3.3
     */
    private float ConvertHeightFromTextSpaceToUserSpace(float height)
    {
        var textSpace = new LineSegment(new Vector(0, 0, 1), new Vector(0, height, 1));
        var userSpace = textSpace.TransformBy(textToUserSpaceTransformMatrix);
        return userSpace.GetLength();
    }

    /**
     * @return The width, in user space units, of a single space character in the current font
     */
    virtual public float GetSingleSpaceWidth()
    {
        return ConvertWidthFromTextSpaceToUserSpace(GetUnscaledFontSpaceWidth());
    }

    /**
     * @return the text render mode that should be used for the text.  From the
     * PDF specification, this means:
     * <ul>
     *   <li>0 = Fill text</li>
     *   <li>1 = Stroke text</li>
     *   <li>2 = Fill, then stroke text</li>
     *   <li>3 = Invisible</li>
     *   <li>4 = Fill text and add to path for clipping</li>
     *   <li>5 = Stroke text and add to path for clipping</li>
     *   <li>6 = Fill, then stroke text and add to path for clipping</li>
     *   <li>7 = Add text to padd for clipping</li>
     * </ul>
     * @since iText 5.0.1
     */
    virtual public int GetTextRenderMode()
    {
        return gs.renderMode;
    }

    /**
     * @return the current fill color.
     */
    virtual public BaseColor GetFillColor()
    {
        return gs.fillColor;
    }

    /**
     * @return the current stroke color.
     */
    virtual public BaseColor GetStrokeColor()
    {
        return gs.strokeColor;
    }

    /**
     * Calculates the width of a space character.  If the font does not define
     * a width for a standard space character \u0020, we also attempt to use
     * the width of \u00A0 (a non-breaking space in many fonts)
     * @return the width of a single space character in text space units
     */
    private float GetUnscaledFontSpaceWidth()
    {
        var charToUse = ' ';
        if (gs.font.GetWidth(charToUse) == 0)
            charToUse = '\u00A0';
        return GetStringWidth(charToUse.ToString());
    }

    /**
     * Gets the width of a String in text space units
     * @param string    the string that needs measuring
     * @return          the width of a String in text space units
     */
    private float GetStringWidth(string @string)
    {
        float totalWidth = 0;
        for (var i = 0; i < @string.Length; i++)
        {
            var c = @string[i];
            var w = gs.font.GetWidth(c) / 1000.0f;
            var wordSpacing = c == 32 ? gs.wordSpacing : 0f;
            totalWidth += (w * gs.fontSize + gs.characterSpacing + wordSpacing) * gs.horizontalScaling;
        }
        return totalWidth;
    }

    /**
     * Gets the width of a PDF string in text space units
     * @param string        the string that needs measuring
     * @return  the width of a String in text space units
     */
    private float GetPdfStringWidth(PdfString @string, bool singleCharString)
    {
        if (singleCharString)
        {
            var widthAndWordSpacing = GetWidthAndWordSpacing(@string, singleCharString);
            return (widthAndWordSpacing[0] * gs.fontSize + gs.characterSpacing + widthAndWordSpacing[1]) * gs.horizontalScaling;
        }
        else
        {
            float totalWidth = 0;
            foreach (var str in SplitString(@string))
            {
                totalWidth += GetPdfStringWidth(str, true);
            }
            return totalWidth;
        }
    }

    /**
     * Provides detail useful if a listener needs access to the position of each individual glyph in the text render operation
     * @return  A list of {@link TextRenderInfo} objects that represent each glyph used in the draw operation. The next effect is if there was a separate Tj opertion for each character in the rendered string
     * @since   5.3.3
     */
    public virtual IList<TextRenderInfo> GetCharacterRenderInfos()
    {
        IList<TextRenderInfo> rslt = new List<TextRenderInfo>(@string.Length);
        var strings = SplitString(@string);
        float totalWidth = 0;
        for (var i = 0; i < strings.Length; i++)
        {
            var widthAndWordSpacing = GetWidthAndWordSpacing(strings[i], true);
            var subInfo = new TextRenderInfo(this, strings[i], totalWidth);
            rslt.Add(subInfo);
            totalWidth += (widthAndWordSpacing[0] * gs.fontSize + gs.characterSpacing + widthAndWordSpacing[1]) *
                          gs.horizontalScaling;
        }
        foreach (var tri in rslt)
            tri.GetUnscaledWidth();
        return rslt;
    }

    /**
     * Calculates width and word spacing of a single character PDF string.
     * @param string            a character to calculate width.
     * @param singleCharString  true if PDF string represents single character, false otherwise.
     * @return                  array of 2 items: first item is a character width, second item is a calculated word spacing.
     */
    private float[] GetWidthAndWordSpacing(PdfString @string, bool singleCharString)
    {
        if (singleCharString == false)
            throw new InvalidOperationException();
        var result = new float[2];
        var decoded = DecodeSingleCharacter(@string);
        result[0] = (float)(gs.font.GetWidth(GetCharCode(decoded)) * fontMatrix[0]);
        result[1] = decoded.Equals(" ") ? gs.wordSpacing : 0;
        return result;
    }

    /**
     * Decodes a PdfString (which will contain glyph ids encoded in the font's encoding)
     * based on the active font, and determine the unicode equivalent
     * @param in	the String that needs to be encoded
     * @return	    the encoded String
     */
    private string Decode(PdfString @in)
    {
        var bytes = @in.GetBytes();
        return gs.font.Decode(bytes, 0, bytes.Length);
    }

    /**
     * ! .NET SPECIFIC; this method is used to avoid unecessary using of StringBuilder because it is slow in .NET !
     * Decodes a single character PdfString (which will contain glyph ids encoded in the font's encoding)
     * based on the active font, and determine the unicode equivalent
     * @param in	the String that needs to be encoded
     * @return	    the encoded String
     */
    private string DecodeSingleCharacter(PdfString @in)
    {
        var bytes = @in.GetBytes();
        return gs.font.DecodeSingleCharacter(bytes, 0, bytes.Length);
    }

    /**
     * Converts a single character string to char code.
     *
     * @param string single character string to convert to.
     * @return char code.
     */
    private int GetCharCode(string @string)
    {
        try
        {
            var b = Utf_16BeEncoding.GetBytes(@string);
            var value = 0;
            for (var i = 0; i < b.Length - 1; i++)
            {
                value += b[i] & 0xff;
                value <<= 8;
            }

            if (b.Length > 0)
            {
                value += b[b.Length - 1] & 0xff;
            }

            return value;
        }
        catch (ArgumentException e)
        {
        }
        return 0;
    }

    /**
     * Split PDF string into array of single character PDF strings.
     * @param string    PDF string to be splitted.
     * @return          splitted PDF string.
     */
    private PdfString[] SplitString(PdfString @string)
    {
        var strings = new List<PdfString>();
        var stringValue = @string.ToString();
        for (var i = 0; i < stringValue.Length; i++)
        {
            var newString = new PdfString(stringValue.Substring(i, 1), @string.Encoding);
            var text = DecodeSingleCharacter(newString);
            if (text.Length == 0 && i < stringValue.Length - 1)
            {
                newString = new PdfString(stringValue.Substring(i, 2), @string.Encoding);
                i++;
            }
            strings.Add(newString);
        }
        return strings.ToArray();
    }

    private Encoding Utf_16BeEncoding
    {
        get
        {
            if (utf_16BeEncoding != null)
                return utf_16BeEncoding;
            else return utf_16BeEncoding = Encoding.GetEncoding("UTF-16BE");
        }
    }
}