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

    /** Implements form fields.
     *
     * @author Paulo Soares
     */
    public class PdfFormField : PdfAnnotation {

        public const int FF_READ_ONLY = 1;
        public const int FF_REQUIRED = 2;
        public const int FF_NO_EXPORT = 4;
        public const int FF_NO_TOGGLE_TO_OFF = 16384;
        public const int FF_RADIO = 32768;
        public const int FF_PUSHBUTTON = 65536;
        public const int FF_MULTILINE = 4096;
        public const int FF_PASSWORD = 8192;
        public const int FF_COMBO = 131072;
        public const int FF_EDIT = 262144;
        public const int FF_FILESELECT = 1048576;
        public const int FF_MULTISELECT = 2097152;
        public const int FF_DONOTSPELLCHECK = 4194304;
        public const int FF_DONOTSCROLL = 8388608;
        public const int FF_COMB = 16777216;
        public const int FF_RADIOSINUNISON = 1 << 25;
        /**
         * Allows text fields to support rich text.
         * @since 5.0.6
         */
        public const int FF_RICHTEXT = 1 << 25;
        public const int Q_LEFT = 0;
        public const int Q_CENTER = 1;
        public const int Q_RIGHT = 2;
        public const int MK_NO_ICON = 0;
        public const int MK_NO_CAPTION = 1;
        public const int MK_CAPTION_BELOW = 2;
        public const int MK_CAPTION_ABOVE = 3;
        public const int MK_CAPTION_RIGHT = 4;
        public const int MK_CAPTION_LEFT = 5;
        public const int MK_CAPTION_OVERLAID = 6;
        public static readonly PdfName IF_SCALE_ALWAYS = PdfName.A;
        public static readonly PdfName IF_SCALE_BIGGER = PdfName.B;
        public static readonly PdfName IF_SCALE_SMALLER = PdfName.S;
        public static readonly PdfName IF_SCALE_NEVER = PdfName.N;
        public static readonly PdfName IF_SCALE_ANAMORPHIC = PdfName.A;
        public static readonly PdfName IF_SCALE_PROPORTIONAL = PdfName.P;
        public const bool MULTILINE = true;
        public const bool SINGLELINE = false;
        public const bool PLAINTEXT = false;
        public const bool PASSWORD = true;
        public static PdfName[] mergeTarget = {PdfName.FONT, PdfName.XOBJECT, PdfName.COLORSPACE, PdfName.PATTERN};
    
        /** Holds value of property parent. */
        internal PdfFormField parent;
    
        internal List<PdfFormField> kids;
    

    
        virtual public void SetWidget(Rectangle rect, PdfName highlight) {
            Put(PdfName.TYPE, PdfName.ANNOT);
            Put(PdfName.SUBTYPE, PdfName.WIDGET);
            Put(PdfName.RECT, new PdfRectangle(rect));
            annotation = true;
            if (highlight != null && !highlight.Equals(HIGHLIGHT_INVERT))
                Put(PdfName.H, highlight);
        }
 
        virtual public int Button {
            set {
                Put(PdfName.FT, PdfName.BTN);
                if (value != 0)
                    Put(PdfName.FF, new PdfNumber(value));
            }
        }

        protected static PdfArray ProcessOptions(String[] options) {
            PdfArray array = new PdfArray();
            for (int k = 0; k < options.Length; ++k) {
                array.Add(new PdfString(options[k], PdfObject.TEXT_UNICODE));
            }
            return array;
        }
        
        protected static PdfArray ProcessOptions(String[,] options) {
            PdfArray array = new PdfArray();
            for (int k = 0; k < options.GetLength(0); ++k) {
                PdfArray ar2 = new PdfArray(new PdfString(options[k, 0], PdfObject.TEXT_UNICODE));
                ar2.Add(new PdfString(options[k, 1], PdfObject.TEXT_UNICODE));
                array.Add(ar2);
            }
            return array;
        }
        
        /** Getter for property parent.
        * @return Value of property parent.
        */
        virtual public PdfFormField Parent => parent;

        virtual public void AddKid(PdfFormField field) {
            field.parent = this;
            if (kids == null)
                kids = new List<PdfFormField>();
            kids.Add(field);
        }
        
        virtual public List<PdfFormField> Kids => kids;

        virtual public int SetFieldFlags(int flags) {
            PdfNumber obj = (PdfNumber)Get(PdfName.FF);
            int old;
            if (obj == null)
                old = 0;
            else
                old = obj.IntValue;
            int v = old | flags;
            Put(PdfName.FF, new PdfNumber(v));
            return old;
        }
        
        virtual public string ValueAsString {
            set => Put(PdfName.V, new PdfString(value, PdfObject.TEXT_UNICODE));
        }

        virtual public string ValueAsName {
            set => Put(PdfName.V, new PdfName(value));
        }

        virtual public PdfSignature ValueAsSig {
            set => Put(PdfName.V, value);
        }

        /**
         * Sets the rich value for this field.  
         * It is suggested that the regular value of this field be set to an 
         * equivalent value.  Rich text values are only supported since PDF 1.5,
         * and require that the FF_RV flag be set.  See PDF Reference chapter 
         * 12.7.3.4 for details.
         * @param rv HTML markup for the rich value of this field
         * @since 5.0.6
         */
        virtual public String RichValue {
            set => Put(PdfName.RV, new PdfString(value));
        }

        virtual public string DefaultValueAsString {
            set => Put(PdfName.DV, new PdfString(value, PdfObject.TEXT_UNICODE));
        }

        virtual public string DefaultValueAsName {
            set => Put(PdfName.DV, new PdfName(value));
        }
        
        virtual public string FieldName {
            set {
                if (value != null)
                    Put(PdfName.T, new PdfString(value, PdfObject.TEXT_UNICODE));
            }
        }
        
        virtual public string UserName {
            set => Put(PdfName.TU, new PdfString(value, PdfObject.TEXT_UNICODE));
        }
        
        virtual public string MappingName {
            set => Put(PdfName.TM, new PdfString(value, PdfObject.TEXT_UNICODE));
        }
        
        virtual public int Quadding {
            set => Put(PdfName.Q, new PdfNumber(value));
        }
    }
}
