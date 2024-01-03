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

namespace SF.Pdf.Application
{

    /** Implements the shading dictionary (or stream).
     *
     * @author Paulo Soares
     */
    public class PdfShading {

        protected PdfDictionary shading;
        
        protected int shadingType;
    
        protected ColorDetails colorDetails;
    
        protected PdfName shadingName;
    
        protected PdfIndirectReference shadingReference;
    
        /** Holds value of property bBox. */
        protected float[] bBox;
    
        /** Holds value of property antiAlias. */
        protected bool antiAlias = false;

        private BaseColor cspace;
    
        virtual public BaseColor ColorSpace => cspace;

        public static void ThrowColorSpaceError() {
            throw new ArgumentException(MessageLocalization.GetComposedMessage("a.tiling.or.shading.pattern.cannot.be.used.as.a.color.space.in.a.shading.pattern"));
        }
    
        public static void CheckCompatibleColors(BaseColor c1, BaseColor c2) {
            var type1 = ExtendedColor.GetType(c1);
            var type2 = ExtendedColor.GetType(c2);
            if (type1 != type2)
                throw new ArgumentException(MessageLocalization.GetComposedMessage("both.colors.must.be.of.the.same.type"));
            if (type1 == ExtendedColor.TYPE_SEPARATION && ((SpotColor)c1).PdfSpotColor != ((SpotColor)c2).PdfSpotColor)
                throw new ArgumentException(MessageLocalization.GetComposedMessage("the.spot.color.must.be.the.same.only.the.tint.can.vary"));
            if (type1 == ExtendedColor.TYPE_PATTERN || type1 == ExtendedColor.TYPE_SHADING)
                ThrowColorSpaceError();
        }
    
        public static float[] GetColorArray(BaseColor color) {
            var type = ExtendedColor.GetType(color);
            switch (type) {
                case ExtendedColor.TYPE_GRAY: {
                    return new float[]{((GrayColor)color).Gray};
                }
                case ExtendedColor.TYPE_CMYK: {
                    var cmyk = (CMYKColor)color;
                    return new float[]{cmyk.Cyan, cmyk.Magenta, cmyk.Yellow, cmyk.Black};
                }
                case ExtendedColor.TYPE_SEPARATION: {
                    return new float[]{((SpotColor)color).Tint};
                }
                case ExtendedColor.TYPE_DEVICEN: {
                    return ((DeviceNColor) color).Tints;
                }
                case ExtendedColor.TYPE_RGB: {
                    return new float[]{color.R / 255f, color.G / 255f, color.B / 255f};
                }
            }
            ThrowColorSpaceError();
            return null;
        }

        internal PdfName ShadingName => shadingName;

        internal int Name {
            set => shadingName = new PdfName("Sh" + value);
        }
        
        internal ColorDetails ColorDetails => colorDetails;

        virtual public float[] BBox {
            get => bBox;
            set {
                if (value.Length != 4)
                    throw new ArgumentException(MessageLocalization.GetComposedMessage("bbox.must.be.a.4.element.array"));
                this.bBox = value;
            }
        }
    
        virtual public bool AntiAlias {
            set => this.antiAlias = value;
            get => antiAlias;
        }
    
    }
}
