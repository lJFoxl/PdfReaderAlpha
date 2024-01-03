﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Pdf.Application;
/**
    * A <CODE>PdfString</CODE>-class is the PDF-equivalent of a JAVA-<CODE>string</CODE>-object.
    * <P>
    * A string is a sequence of characters delimited by parenthesis. If a string is too long
    * to be conveniently placed on a single line, it may be split across multiple lines by using
    * the backslash character (\) at the end of a line to indicate that the string continues
    * on the following line. Within a string, the backslash character is used as an escape to
    * specify unbalanced parenthesis, non-printing ASCII characters, and the backslash character
    * itself. Use of the \<I>ddd</I> escape sequence is the preferred way to represent characters
    * outside the printable ASCII character set.<BR>
    * This object is described in the 'Portable Document Format Reference Manual version 1.3'
    * section 4.4 (page 37-39).
    *
    * @see        PdfObject
    * @see        BadPdfFormatException
    */

public class PdfString : PdfObject
{

    // membervariables

    /** The value of this object. */
    protected string value = NOTHING;
    protected string originalValue = null;

    /** The encoding. */
    protected string encoding = TEXT_PDFDOCENCODING;
    protected int objNum = 0;
    protected int objGen = 0;
    protected bool hexWriting = false;
    // constructors

    /**
     * Constructs an empty <CODE>PdfString</CODE>-object.
     */

    public PdfString() : base(STRING) { }

    /**
     * Constructs a <CODE>PdfString</CODE>-object.
     *
     * @param        value        the content of the string
     */

    public PdfString(string value) : base(STRING)
    {
        this.value = value;
    }

    /**
     * Constructs a <CODE>PdfString</CODE>-object.
     *
     * @param        value        the content of the string
     * @param        encoding    an encoding
     */

    public PdfString(string value, string encoding) : base(STRING)
    {
        this.value = value;
        this.encoding = encoding;
    }

    /**
     * Constructs a <CODE>PdfString</CODE>-object.
     *
     * @param        bytes    an array of <CODE>byte</CODE>
     */

    public PdfString(byte[] bytes) : base(STRING)
    {
        value = PdfEncodings.ConvertToString(bytes, null);
        encoding = NOTHING;
    }

    // methods overriding some methods in PdfObject

    /**
     * Returns the <CODE>string</CODE> value of the <CODE>PdfString</CODE>-object.
     *
     * @return        a <CODE>string</CODE>
     */

    public override string ToString()
    {
        return value;
    }

    // other methods

    /**
     * Gets the encoding of this string.
     *
     * @return        a <CODE>string</CODE>
     */

    virtual public string Encoding => encoding;

    virtual public string ToUnicodeString()
    {
        if (encoding != null && encoding.Length != 0)
            return value;
        GetBytes();
        if (bytes.Length >= 2 && bytes[0] == (byte)254 && bytes[1] == (byte)255)
            return PdfEncodings.ConvertToString(bytes, PdfObject.TEXT_UNICODE);
        else
            return PdfEncodings.ConvertToString(bytes, PdfObject.TEXT_PDFDOCENCODING);
    }

    internal void SetObjNum(int objNum, int objGen)
    {
        this.objNum = objNum;
        this.objGen = objGen;
    }

    internal void Decrypt(PdfReader reader)
    {
        var decrypt = reader.Decrypt;
        if (decrypt != null)
        {
            originalValue = value;
            decrypt.SetHashKey(objNum, objGen);
            bytes = PdfEncodings.ConvertToBytes(value, null);
            bytes = decrypt.DecryptByteArray(bytes);
            value = PdfEncodings.ConvertToString(bytes, null);
        }
    }

    public override byte[] GetBytes()
    {
        if (bytes == null)
        {
            if (encoding != null && encoding.Equals(TEXT_UNICODE) && PdfEncodings.IsPdfDocEncoding(value))
                bytes = PdfEncodings.ConvertToBytes(value, TEXT_PDFDOCENCODING);
            else
                bytes = PdfEncodings.ConvertToBytes(value, encoding);
        }
        return bytes;
    }

    virtual public byte[] GetOriginalBytes()
    {
        if (originalValue == null)
            return GetBytes();
        return PdfEncodings.ConvertToBytes(originalValue, null);
    }

    virtual public PdfString SetHexWriting(bool hexWriting)
    {
        this.hexWriting = hexWriting;
        return this;
    }

    virtual public bool IsHexWriting()
    {
        return hexWriting;
    }
}
