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

namespace PdfSharp_net8_.iTextSharp.text.pdf {

    /**
     * <CODE>PdfFormObject</CODE> is a type of XObject containing a template-object.
     */

    public class PdfFormXObject : PdfStream {
    
        // public static variables
    
        /** This is a PdfNumber representing 0. */
        public static PdfNumber ZERO = new PdfNumber(0);
    
        /** This is a PdfNumber representing 1. */
        public static PdfNumber ONE = new PdfNumber(1);
    
        /** This is the 1 - matrix. */
        public static PdfLiteral MATRIX = new PdfLiteral("[1 0 0 1 0 0]");
    
        /**
         * Constructs a <CODE>PdfFormXObject</CODE>-object.
         *
         * @param        template        the template
         * @param   compressionLevel    the compression level for the stream
         * @since   2.1.3 (Replacing the existing constructor with param compressionLevel)
         */
    
        internal PdfFormXObject(PdfTemplate template, int compressionLevel) : base() {
            Put(PdfName.TYPE, PdfName.XOBJECT);
            Put(PdfName.SUBTYPE, PdfName.FORM);
            Put(PdfName.RESOURCES, template.Resources);
            Put(PdfName.BBOX, new PdfRectangle(template.BoundingBox));
            Put(PdfName.FORMTYPE, ONE);
            PdfArray matrix = template.Matrix;
            if (template.Layer != null)
                Put(PdfName.OC, template.Layer.Ref);
            if (template.Group != null)
                Put(PdfName.GROUP, template.Group);
            if (matrix == null)
                Put(PdfName.MATRIX, MATRIX);
            else
                Put(PdfName.MATRIX, matrix);
            bytes = template.ToPdf(null);
            Put(PdfName.LENGTH, new PdfNumber(bytes.Length));
            if (template.Additional != null) {
                Merge(template.Additional);
            }
            FlateCompress(compressionLevel);
        }
    
    }
}
