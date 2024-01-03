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

namespace SF.Pdf.Application {

    /**
     * Implements the pattern.
     */

    public sealed class PdfPatternPainter : PdfTemplate {
    
        internal float xstep, ystep;
        internal bool stencil = false;
        internal BaseColor defaultColor;
    
        /**
         *Creates a <CODE>PdfPattern</CODE>.
         */
    
        private PdfPatternPainter() : base() {
            type = TYPE_PATTERN;
        }

    
        public float XStep {
            get => this.xstep;

            set => this.xstep = value;
        }
    
        public float YStep {
            get => this.ystep;

            set => this.ystep = value;
        }
    
        public bool IsStencil() {
            return stencil;
        }
    
        public void SetPatternMatrix(float a, float b, float c, float d, float e, float f) {
            SetMatrix(a, b, c, d, e, f);
        }

        /**
        * Gets the stream representing this pattern
        * @return the stream representing this pattern
        */
        public PdfPattern GetPattern() {
            return new PdfPattern(this);
        }
        
        /**
        * Gets the stream representing this pattern
        * @param   compressionLevel    the compression level of the stream
        * @return the stream representing this pattern
        * @since   2.1.3
        */
        public PdfPattern GetPattern(int compressionLevel) {
            return new PdfPattern(this, compressionLevel);
        }
    
        /**
         * Gets a duplicate of this <CODE>PdfPatternPainter</CODE>. All
         * the members are copied by reference but the buffer stays different.
         * @return a copy of this <CODE>PdfPatternPainter</CODE>
         */
    
        public override PdfContentByte Duplicate {
            get {
                var tpl = new PdfPatternPainter();

                tpl.pdf = pdf;
                tpl.thisReference = thisReference;
                tpl.pageResources = pageResources;
                tpl.bBox = new Rectangle(bBox);
                tpl.xstep = xstep;
                tpl.ystep = ystep;
                tpl.matrix = matrix;
                tpl.stencil = stencil;
                tpl.defaultColor = defaultColor;
                return tpl;
            }
        }
    
        public BaseColor DefaultColor => defaultColor;

        internal void CheckNoColor() {
            if (stencil)
                throw new ArgumentException(MessageLocalization.GetComposedMessage("colors.are.not.allowed.in.uncolored.tile.patterns"));
        }
    }
}
