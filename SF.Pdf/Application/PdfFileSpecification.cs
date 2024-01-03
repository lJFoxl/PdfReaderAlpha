/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2022 iText Group NV
    Authors: iText Software.

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License version 3
    as published by the Free Software Foundation with the addition of the
    following permission added to Section 15 as permitted in Section 7(a):
    FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
    ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
    OF THIRD PARTY RIGHTS
    
    This program is distributed in the hope that it will be useful, but
    WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
    or FITNESS FOR A PARTICULAR PURPOSE.
    See the GNU Affero General Public License for more details.
    You should have received a copy of the GNU Affero General Public License
    along with this program; if not, see http://www.gnu.org/licenses or write to
    the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
    Boston, MA, 02110-1301 USA, or download the license from the following URL:
    http://itextpdf.com/terms-of-use/
    
    The interactive user interfaces in modified source and object code versions
    of this program must display Appropriate Legal Notices, as required under
    Section 5 of the GNU Affero General Public License.
    
    In accordance with Section 7(b) of the GNU Affero General Public License,
    a covered work must retain the producer line in every PDF that is created
    or manipulated using iText.
    
    You can be released from the requirements of the license by purchasing
    a commercial license. Buying such a license is mandatory as soon as you
    develop commercial activities involving the iText software without
    disclosing the source code of your own applications.
    These activities include: offering paid services to customers as an ASP,
    serving PDFs on the fly in a web application, shipping iText with a closed
    source product.
    
    For more information, please contact iText Software Corp. at this
    address: sales@itextpdf.com
 */

using SF.Pdf.Application.collection;

namespace SF.Pdf.Application
{
    /** Specifies a file or an URL. The file can be extern or embedded.
    *
    * @author Paulo Soares
    */
    public class PdfFileSpecification : PdfDictionary
    {
        protected PdfIndirectReference refi;

        /** Creates a new instance of PdfFileSpecification. The static methods are preferred. */
        public PdfFileSpecification() : base(PdfName.FILESPEC)
        {
        }


        /**
        * Sets the file name (the key /F) string as an hex representation
        * to support multi byte file names. The name must have the slash and
        * backslash escaped according to the file specification rules
        * @param fileName the file name as a byte array
        */
        virtual public byte[] MultiByteFileName
        {
            set => Put(PdfName.F, new PdfString(value).SetHexWriting(true));
        }

        /**
        * Adds the unicode file name (the key /UF). This entry was introduced
        * in PDF 1.7. The filename must have the slash and backslash escaped
        * according to the file specification rules.
        * @param filename  the filename
        * @param unicode   if true, the filename is UTF-16BE encoded; otherwise PDFDocEncoding is used;
        */
        virtual public void SetUnicodeFileName(string filename, bool unicode)
        {
            Put(PdfName.UF, new PdfString(filename, unicode ? PdfObject.TEXT_UNICODE : PdfObject.TEXT_PDFDOCENCODING));
        }

        /**
        * Sets a flag that indicates whether an external file referenced by the file
        * specification is volatile. If the value is true, applications should never
        * cache a copy of the file.
        * @param volatile_file if true, the external file should not be cached
        */
        virtual public bool Volatile
        {
            set => Put(PdfName.V, new PdfBoolean(value));
        }

        /**
        * Adds a description for the file that is specified here.
        * @param description   some text
        * @param unicode       if true, the text is added as a unicode string
        */
        virtual public void AddDescription(string description, bool unicode)
        {
            Put(PdfName.DESC, new PdfString(description, unicode ? PdfObject.TEXT_UNICODE : PdfObject.TEXT_PDFDOCENCODING));
        }

        /**
        * Adds the Collection item dictionary.
        */
        virtual public void AddCollectionItem(PdfCollectionItem ci)
        {
            Put(PdfName.CI, ci);
        }

    }
}
