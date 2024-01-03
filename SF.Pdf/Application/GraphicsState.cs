﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Pdf.Application;
/**
   * Keeps all the parameters of the graphics state.
   * @since   2.1.4
   */
public class GraphicsState
{
    /** The current transformation matrix. */
    internal Matrix ctm;
    /** The current character spacing. */
    internal float characterSpacing;

    virtual public float CharacterSpacing => characterSpacing;

    /** The current word spacing. */
    internal float wordSpacing;

    virtual public float WordSpacing => wordSpacing;

    /** The current horizontal scaling */
    internal float horizontalScaling;

    virtual public float HorizontalScaling => horizontalScaling;

    /** The current leading. */
    internal float leading;
    /** The active font. */
    internal CMapAwareDocumentFont font;

    virtual public CMapAwareDocumentFont Font => font;

    /** The current font size. */
    internal float fontSize;

    virtual public float FontSize => fontSize;

    /** The current render mode. */
    internal int renderMode;
    /** The current text rise */
    internal float rise;
    /** The current knockout value. */
    internal bool knockout;
    /** The current color space for stroke. */
    internal PdfName colorSpaceFill;
    /** The current color space for stroke. */
    internal PdfName colorSpaceStroke;
    /** The current fill color. */
    internal BaseColor fillColor;
    /** The current stroke color. */
    internal BaseColor strokeColor;

    /** The line width for stroking operations */
    private float lineWidth;

    /**
     * The line cap style. For possible values
     * see {@link PdfContentByte}
     */
    private int lineCapStyle;

    /**
     * The line join style. For possible values
     * see {@link PdfContentByte}
     */
    private int lineJoinStyle;

    /** The mitir limit value */
    private float miterLimit;

    /** The line dash pattern */
    private LineDashPattern lineDashPattern;

    /**
     * Constructs a new Graphics State object with the default values.
     */
    public GraphicsState()
    {
        ctm = new Matrix();
        characterSpacing = 0;
        wordSpacing = 0;
        horizontalScaling = 1.0f;
        leading = 0;
        font = null;
        fontSize = 0;
        renderMode = 0;
        rise = 0;
        knockout = true;
        colorSpaceFill = null;
        colorSpaceStroke = null;
        fillColor = null;
        strokeColor = null;
        lineWidth = 1.0f;
        lineCapStyle = PdfContentByte.LINE_CAP_BUTT;
        lineJoinStyle = PdfContentByte.LINE_JOIN_MITER;
        miterLimit = 10.0f;
    }

    /**
     * Copy constructor.
     * @param source    another GraphicsState object
     */
    public GraphicsState(GraphicsState source)
    {
        // note: all of the following are immutable, with the possible exception of font
        // so it is safe to copy them as-is
        ctm = source.ctm;
        characterSpacing = source.characterSpacing;
        wordSpacing = source.wordSpacing;
        horizontalScaling = source.horizontalScaling;
        leading = source.leading;
        font = source.font;
        fontSize = source.fontSize;
        renderMode = source.renderMode;
        rise = source.rise;
        knockout = source.knockout;
        colorSpaceFill = source.colorSpaceFill;
        colorSpaceStroke = source.colorSpaceStroke;
        fillColor = source.fillColor;
        strokeColor = source.strokeColor;
        lineWidth = source.lineWidth;
        lineCapStyle = source.lineCapStyle;
        lineJoinStyle = source.lineJoinStyle;
        miterLimit = source.miterLimit;

        if (source.lineDashPattern != null)
        {
            lineDashPattern = new LineDashPattern(source.lineDashPattern.DashArray, source.lineDashPattern.DashPhase);
        }
    }

    /**
     * Getter for the current transformation matrix
     * @return the ctm
     * @since iText 5.0.1
     */
    virtual public Matrix GetCtm()
    {
        return ctm;
    }

    /**
     * Getter for the character spacing.
     * @return the character spacing
     * @since iText 5.0.1
     */
    virtual public float GetCharacterSpacing()
    {
        return characterSpacing;
    }

    /**
     * Getter for the word spacing
     * @return the word spacing
     * @since iText 5.0.1
     */
    virtual public float GetWordSpacing()
    {
        return wordSpacing;
    }

    /**
     * Getter for the horizontal scaling
     * @return the horizontal scaling
     * @since iText 5.0.1
     */
    virtual public float GetHorizontalScaling()
    {
        return horizontalScaling;
    }

    /**
     * Getter for the leading
     * @return the leading
     * @since iText 5.0.1
     */
    virtual public float GetLeading()
    {
        return leading;
    }

    /**
     * Getter for the font
     * @return the font
     * @since iText 5.0.1
     */
    virtual public CMapAwareDocumentFont GetFont()
    {
        return font;
    }

    /**
     * Getter for the font size
     * @return the font size
     * @since iText 5.0.1
     */
    virtual public float GetFontSize()
    {
        return fontSize;
    }

    /**
     * Getter for the render mode
     * @return the renderMode
     * @since iText 5.0.1
     */
    virtual public int GetRenderMode()
    {
        return renderMode;
    }

    /**
     * Getter for text rise
     * @return the text rise
     * @since iText 5.0.1
     */
    virtual public float GetRise()
    {
        return rise;
    }

    /**
     * Getter for knockout
     * @return the knockout
     * @since iText 5.0.1
     */
    virtual public bool IsKnockout()
    {
        return knockout;
    }

    /**
     * Gets the current color space for fill operations
     */
    virtual public PdfName ColorSpaceFill => colorSpaceFill;

    /**
     * Gets the current color space for stroke operations
     */
    virtual public PdfName ColorSpaceStroke => colorSpaceStroke;

    /**
     * Gets the current fill color
     * @return a BaseColor
     */
    virtual public BaseColor FillColor => fillColor;

    /**
     * Gets the current stroke color
     * @return a BaseColor
     */
    virtual public BaseColor StrokeColor => strokeColor;


    /**
     * Getter  and setter for the line width.
     * @return The line width
     * @since 5.5.6
     */
    public float LineWidth
    {
        get => lineWidth;
        set => lineWidth = value;
    }

    /**
     * Getter and setter for the line cap style.
     * For possible values see {@link PdfContentByte}
     * @return The line cap style.
     * @since 5.5.6
     */
    public int LineCapStyle
    {
        get => lineCapStyle;
        set => lineCapStyle = value;
    }

    /**
     * Getter and setter for the line join style.
     * For possible values see {@link PdfContentByte}
     * @return The line join style.
     * @since 5.5.6
     */
    public int LineJoinStyle
    {
        get => lineJoinStyle;
        set => lineJoinStyle = value;
    }

    /**
     * Getter and setter for the miter limit value.
     * @return The miter limit.
     * @since 5.5.6
     */
    public float MiterLimit
    {
        get => miterLimit;
        set => miterLimit = value;
    }

    /**
     * Getter for the line dash pattern.
     * @return The line dash pattern.
     * @since 5.5.6
     */
    public virtual LineDashPattern GetLineDashPattern()
    {
        return lineDashPattern;
    }

    /**
     * Setter for the line dash pattern.
     * @param lineDashPattern New line dash pattern.
     * @since 5.5.6
     */
    public virtual void SetLineDashPattern(LineDashPattern lineDashPattern)
    {
        this.lineDashPattern = new LineDashPattern(lineDashPattern.DashArray, lineDashPattern.DashPhase);
    }
}
