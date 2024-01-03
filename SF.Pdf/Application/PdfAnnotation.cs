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

using System.Text;
using SF.Pdf.Application.Interface;

namespace SF.Pdf.Application
{
    /**
     * A <CODE>PdfAnnotation</CODE> is a note that is associated with a page.
     *
     * @see     PdfDictionary
     */
    public class PdfAnnotation : PdfDictionary, IAccessibleElement {
    
        public static readonly PdfName HIGHLIGHT_NONE = PdfName.N;
        public static readonly PdfName HIGHLIGHT_INVERT = PdfName.I;
        public static readonly PdfName HIGHLIGHT_OUTLINE = PdfName.O;
        public static readonly PdfName HIGHLIGHT_PUSH = PdfName.P;
        public static readonly PdfName HIGHLIGHT_TOGGLE = PdfName.T;
        public const int FLAGS_INVISIBLE = 1;
        public const int FLAGS_HIDDEN = 2;
        public const int FLAGS_PRINT = 4;
        public const int FLAGS_NOZOOM = 8;
        public const int FLAGS_NOROTATE = 16;
        public const int FLAGS_NOVIEW = 32;
        public const int FLAGS_READONLY = 64;
        public const int FLAGS_LOCKED = 128;
        public const int FLAGS_TOGGLENOVIEW = 256;
        /** flagvalue PDF 1.7*/
        public const int FLAGS_LOCKEDCONTENTS = 512;
        public static readonly PdfName APPEARANCE_NORMAL = PdfName.N;
        public static readonly PdfName APPEARANCE_ROLLOVER = PdfName.R;
        public static readonly PdfName APPEARANCE_DOWN = PdfName.D;
        public static readonly PdfName AA_ENTER = PdfName.E;
        public static readonly PdfName AA_EXIT = PdfName.X;
        public static readonly PdfName AA_DOWN = PdfName.D;
        public static readonly PdfName AA_UP = PdfName.U;
        public static readonly PdfName AA_FOCUS = PdfName.FO;
        public static readonly PdfName AA_BLUR = PdfName.BL;
        public static readonly PdfName AA_JS_KEY = PdfName.K;
        public static readonly PdfName AA_JS_FORMAT = PdfName.F;
        public static readonly PdfName AA_JS_CHANGE = PdfName.V;
        public static readonly PdfName AA_JS_OTHER_CHANGE = PdfName.C;
        public const int MARKUP_HIGHLIGHT = 0;
        public const int MARKUP_UNDERLINE = 1;
        public const int MARKUP_STRIKEOUT = 2;
        /** attributevalue */
        public const int MARKUP_SQUIGGLY = 3;
        protected internal PdfIndirectReference reference;
        protected internal HashSet2<PdfTemplate> templates;
        protected internal bool form = false;
        protected internal bool annotation = true;
    
        /** Holds value of property used. */
        protected internal bool used = false;
    
        /** Holds value of property placeInPage. */
        private int placeInPage = -1;

        protected PdfName role = null;
        protected Dictionary<PdfName, PdfObject> accessibleAttributes = null;
        private AccessibleElementId id = null;

        virtual public PdfContentByte DefaultAppearanceString {
            set {
                var b = value.InternalBuffer.ToByteArray();
                var len = b.Length;
                for (var k = 0; k < len; ++k) {
                    if (b[k] == '\n')
                        b[k] = 32;
                }
                Put(PdfName.DA, new PdfString(b));
            }
        }
    
        virtual public int Flags {
            set {
                if (value == 0)
                    Remove(PdfName.F);
                else
                    Put(PdfName.F, new PdfNumber(value));
            }
        }
    
        virtual public PdfBorderArray Border {
            set => Put(PdfName.BORDER, value);
        }

        virtual public PdfBorderDictionary BorderStyle {
            set => Put(PdfName.BS, value);
        }
    
        /**
        * Sets the annotation's highlighting mode. The values can be
        * <CODE>HIGHLIGHT_NONE</CODE>, <CODE>HIGHLIGHT_INVERT</CODE>,
        * <CODE>HIGHLIGHT_OUTLINE</CODE> and <CODE>HIGHLIGHT_PUSH</CODE>;
        * @param highlight the annotation's highlighting mode
        */    
        virtual public void SetHighlighting(PdfName highlight) {
            if (highlight.Equals(HIGHLIGHT_INVERT))
                Remove(PdfName.H);
            else
                Put(PdfName.H, highlight);
        }

        virtual public void SetAppearance(PdfName ap, PdfTemplate template) {
            var dic = (PdfDictionary)Get(PdfName.AP);
            if (dic == null)
                dic = new PdfDictionary();
            dic.Put(ap, template.IndirectReference);
            Put(PdfName.AP, dic);
            if (!form)
                return;
            if (templates == null)
                templates = new HashSet2<PdfTemplate>();
            templates.Add(template);
        }

        virtual public void SetAppearance(PdfName ap, string state, PdfTemplate template) {
            var dicAp = (PdfDictionary)Get(PdfName.AP);
            if (dicAp == null)
                dicAp = new PdfDictionary();

            PdfDictionary dic;
            var obj = dicAp.Get(ap);
            if (obj != null && obj.IsDictionary())
                dic = (PdfDictionary)obj;
            else
                dic = new PdfDictionary();
            dic.Put(new PdfName(state), template.IndirectReference);
            dicAp.Put(ap, dic);
            Put(PdfName.AP, dicAp);
            if (!form)
                return;
            if (templates == null)
                templates = new HashSet2<PdfTemplate>();
            templates.Add(template);
        }

        virtual public string AppearanceState {
            set {
                if (value == null) {
                    Remove(PdfName.AS);
                    return;
                }
                Put(PdfName.AS, new PdfName(value));
            }
        }
    
        virtual public BaseColor Color {
            set => Put(PdfName.C, new PdfColor(value));
        }
    
        virtual public string Title {
            set {
                if (value == null) {
                    Remove(PdfName.T);
                    return;
                }
                Put(PdfName.T, new PdfString(value, PdfObject.TEXT_UNICODE));
            }
        }
    
        virtual public PdfAnnotation Popup {
            set {
                Put(PdfName.POPUP, value.IndirectReference);
                value.Put(PdfName.PARENT, this.IndirectReference);
            }
        }
    
        virtual public PdfAction Action {
            set => Put(PdfName.A, value);
        }
    
        virtual public void SetAdditionalActions(PdfName key, PdfAction action) {
            PdfDictionary dic;
            var obj = Get(PdfName.AA);
            if (obj != null && obj.IsDictionary())
                dic = (PdfDictionary)obj;
            else
                dic = new PdfDictionary();
            dic.Put(key, action);
            Put(PdfName.AA, dic);
        }
        
        internal virtual bool IsUsed() {
            return used;
        }

        public virtual void SetUsed() {
            used = true;
        }
    
        [Obsolete("Use GetTemplates() instead")]
        virtual public Dictionary<PdfTemplate,object> Templates => templates != null ? templates.InternalSet : null;

        virtual public HashSet2<PdfTemplate> GetTemplates()
        {
            return templates;
        }
    
        /** Getter for property form.
         * @return Value of property form.
         */
        virtual public bool IsForm() {
            return form;
        }
    
        /** Getter for property annotation.
         * @return Value of property annotation.
         */
        virtual public bool IsAnnotation() {
            return annotation;
        }
    
        virtual public int Page {
            set => Put(PdfName.P, writer.GetPageReference(value));
        }
    
        virtual public void SetPage() {
            Put(PdfName.P, writer.CurrentPage);
        }
    
        /** Getter for property placeInPage.
         * @return Value of property placeInPage.
         */
        virtual public int PlaceInPage {
            get => placeInPage;

            set => this.placeInPage = value;
        }    
    
        public static PdfAnnotation ShallowDuplicate(PdfAnnotation annot) {
            PdfAnnotation dup;
            if (annot.IsForm()) {
                dup = new PdfFormField(annot.writer);
                PdfFormField dupField = (PdfFormField)dup;
                PdfFormField srcField = (PdfFormField)annot;
                dupField.parent = srcField.parent;
                dupField.kids = srcField.kids;
            }
            else
                dup = annot.writer.CreateAnnotation(null, (PdfName)annot.Get(PdfName.SUBTYPE));
            dup.Merge(annot);
            dup.form = annot.form;
            dup.annotation = annot.annotation;
            dup.templates = annot.templates;
            return dup;
        }

        virtual public int Rotate {
            set => Put(PdfName.ROTATE, new PdfNumber(value));
        }
        
        internal PdfDictionary MK {
            get {
                var mk = (PdfDictionary)Get(PdfName.MK);
                if (mk == null) {
                    mk = new PdfDictionary();
                    Put(PdfName.MK, mk);
                }
                return mk;
            }
        }
        
        virtual public int MKRotation {
            set => MK.Put(PdfName.R, new PdfNumber(value));
        }
        
        public static PdfArray GetMKColor(BaseColor color) {
            var array = new PdfArray();
            var type = ExtendedColor.GetType(color);
            switch (type) {
                case ExtendedColor.TYPE_GRAY: {
                    array.Add(new PdfNumber(((GrayColor)color).Gray));
                    break;
                }
                case ExtendedColor.TYPE_CMYK: {
                    var cmyk = (CMYKColor)color;
                    array.Add(new PdfNumber(cmyk.Cyan));
                    array.Add(new PdfNumber(cmyk.Magenta));
                    array.Add(new PdfNumber(cmyk.Yellow));
                    array.Add(new PdfNumber(cmyk.Black));
                    break;
                }
                case ExtendedColor.TYPE_SEPARATION:
                case ExtendedColor.TYPE_PATTERN:
                case ExtendedColor.TYPE_SHADING:
                    throw new Exception(MessageLocalization.GetComposedMessage("separations.patterns.and.shadings.are.not.allowed.in.mk.dictionary"));
                default:
                    array.Add(new PdfNumber(color.R / 255f));
                    array.Add(new PdfNumber(color.G / 255f));
                    array.Add(new PdfNumber(color.B / 255f));
                    break;
            }
            return array;
        }
        
        virtual public BaseColor MKBorderColor {
            set {
                if (value == null)
                    MK.Remove(PdfName.BC);
                else
                    MK.Put(PdfName.BC, GetMKColor(value));
            }
        }
        
        virtual public BaseColor MKBackgroundColor {
            set {
                if (value == null)
                    MK.Remove(PdfName.BG);
                else
                    MK.Put(PdfName.BG, GetMKColor(value));
            }
        }
        
        virtual public string MKNormalCaption {
            set => MK.Put(PdfName.CA, new PdfString(value, PdfObject.TEXT_UNICODE));
        }
        
        virtual public string MKRolloverCaption {
            set => MK.Put(PdfName.RC, new PdfString(value, PdfObject.TEXT_UNICODE));
        }
        
        virtual public string MKAlternateCaption {
            set => MK.Put(PdfName.AC, new PdfString(value, PdfObject.TEXT_UNICODE));
        }
        
        virtual public PdfTemplate MKNormalIcon {
            set => MK.Put(PdfName.I, value.IndirectReference);
        }
        
        virtual public PdfTemplate MKRolloverIcon {
            set => MK.Put(PdfName.RI, value.IndirectReference);
        }
        
        virtual public PdfTemplate MKAlternateIcon {
            set => MK.Put(PdfName.IX, value.IndirectReference);
        }
        
        virtual public void SetMKIconFit(PdfName scale, PdfName scalingType, float leftoverLeft, float leftoverBottom, bool fitInBounds) {
            var dic = new PdfDictionary();
            if (!scale.Equals(PdfName.A))
                dic.Put(PdfName.SW, scale);
            if (!scalingType.Equals(PdfName.P))
                dic.Put(PdfName.S, scalingType);
            if (leftoverLeft != 0.5f || leftoverBottom != 0.5f) {
                var array = new PdfArray(new PdfNumber(leftoverLeft));
                array.Add(new PdfNumber(leftoverBottom));
                dic.Put(PdfName.A, array);
            }
            if (fitInBounds)
                dic.Put(PdfName.FB, PdfBoolean.PDFTRUE);
            MK.Put(PdfName.IF, dic);
        }
        
        virtual public int MKTextPosition {
            set => MK.Put(PdfName.TP, new PdfNumber(value));
        }
        
        /**
        * Sets the layer this annotation belongs to.
        * @param layer the layer this annotation belongs to
        */    
        virtual public IPdfOCG Layer {
            set => Put(PdfName.OC, value.Ref);
        }

        /**
        * Sets the name of the annotation.
        * With this name the annotation can be identified among
        * all the annotations on a page (it has to be unique).
        */
        virtual public string Name {
            set => Put(PdfName.NM, new PdfString(value));
        }

        virtual public void ApplyCTM(AffineTransform ctm) {
            var origRect = GetAsArray(PdfName.RECT);
            if(origRect != null) {
                PdfRectangle rect;
                if(origRect.Size == 4) {
                    rect = new PdfRectangle(origRect.GetAsNumber(0).FloatValue, origRect.GetAsNumber(1).FloatValue,
                        origRect.GetAsNumber(2).FloatValue, origRect.GetAsNumber(3).FloatValue);
                }
                else {
                    rect = new PdfRectangle(origRect.GetAsNumber(0).FloatValue, origRect.GetAsNumber(1).FloatValue);
                }
                Put(PdfName.RECT, rect.Transform(ctm));
            }
        }

#if DRAWING
        [Obsolete]
        public void ApplyCTM(System.Drawing.Drawing2D.Matrix ctm) {
            PdfArray origRect = GetAsArray(PdfName.RECT);
            if(origRect != null) {
                PdfRectangle rect;
                if(origRect.Size == 4) {
                    rect = new PdfRectangle(origRect.GetAsNumber(0).FloatValue, origRect.GetAsNumber(1).FloatValue, origRect.GetAsNumber(2).FloatValue, origRect.GetAsNumber(3).FloatValue);
                }
                else {
                    rect = new PdfRectangle(origRect.GetAsNumber(0).FloatValue, origRect.GetAsNumber(1).FloatValue);
                }
                Put(PdfName.RECT, rect.Transform(ctm));
            }
        }
#endif// DRAWING


        /**
        * This class processes links from imported pages so that they may be active. The following example code reads a group
        * of files and places them all on the output PDF, four pages in a single page, keeping the links active.
        * <pre>
        * String[] files = new String[] {&quot;input1.pdf&quot;, &quot;input2.pdf&quot;};
        * String outputFile = &quot;output.pdf&quot;;
        * int firstPage=1;
        * Document document = new Document();
        * PdfWriter writer = PdfWriter.GetInstance(document, new FileOutputStream(outputFile));
        * document.SetPageSize(PageSize.A4);
        * float W = PageSize.A4.GetWidth() / 2;
        * float H = PageSize.A4.GetHeight() / 2;
        * document.Open();
        * PdfContentByte cb = writer.GetDirectContent();
        * for (int i = 0; i &lt; files.length; i++) {
        *    PdfReader currentReader = new PdfReader(files[i]);
        *    currentReader.ConsolidateNamedDestinations();
        *    for (int page = 1; page &lt;= currentReader.GetNumberOfPages(); page++) {
        *        PdfImportedPage importedPage = writer.GetImportedPage(currentReader, page);
        *        float a = 0.5f;
        *        float e = (page % 2 == 0) ? W : 0;
        *        float f = (page % 4 == 1 || page % 4 == 2) ? H : 0;
        *        ArrayList links = currentReader.GetLinks(page);
        *        cb.AddTemplate(importedPage, a, 0, 0, a, e, f);
        *        for (int j = 0; j &lt; links.Size(); j++) {
        *            PdfAnnotation.PdfImportedLink link = (PdfAnnotation.PdfImportedLink)links.Get(j);
        *            if (link.IsInternal()) {
        *                int dPage = link.GetDestinationPage();
        *                int newDestPage = (dPage-1)/4 + firstPage;
        *                float ee = (dPage % 2 == 0) ? W : 0;
        *                float ff = (dPage % 4 == 1 || dPage % 4 == 2) ? H : 0;
        *                link.SetDestinationPage(newDestPage);
        *                link.TransformDestination(a, 0, 0, a, ee, ff);
        *            }
        *            link.TransformRect(a, 0, 0, a, e, f);
        *            writer.AddAnnotation(link.CreateAnnotation(writer));
        *        }
        *        if (page % 4 == 0)
        *        document.NewPage();
        *    }
        *    if (i &lt; files.length - 1)
        *    document.NewPage();
        *    firstPage += (currentReader.GetNumberOfPages()+3)/4;
        * }
        * document.Close();
        * </pre>
        */
        public class PdfImportedLink {
            float llx, lly, urx, ury;
            Dictionary<PdfName, PdfObject> parameters;
            PdfArray destination = null;
            int newPage=0;
            PdfArray rect;
            
            internal PdfImportedLink(PdfDictionary annotation) {
                parameters = new Dictionary<PdfName,PdfObject>(annotation.hashMap);
                try {
                    if (parameters.ContainsKey(PdfName.DEST))
                        destination = (PdfArray)parameters[PdfName.DEST];
                    parameters.Remove(PdfName.DEST);
                } catch (Exception) {
                    throw new ArgumentException(MessageLocalization.GetComposedMessage("you.have.to.consolidate.the.named.destinations.of.your.reader"));
                }
                if (destination != null) {
                    destination = new PdfArray(destination);
                }
                var rc = (PdfArray)parameters[PdfName.RECT];
                parameters.Remove(PdfName.RECT);
                llx = rc.GetAsNumber(0).FloatValue;
                lly = rc.GetAsNumber(1).FloatValue;
                urx = rc.GetAsNumber(2).FloatValue;
                ury = rc.GetAsNumber(3).FloatValue;

                rect = new PdfArray(rc);
            }

            virtual public IDictionary<PdfName, PdfObject> GetParameters() {
                return new Dictionary<PdfName, PdfObject>(parameters);
            }

            virtual public PdfArray GetRect() {
                return new PdfArray(rect);
            }
            
            virtual public bool IsInternal() {
                return destination != null;
            }
            
            virtual public int GetDestinationPage() {
                if (!IsInternal()) return 0;
                
                // here destination is something like
                // [132 0 R, /XYZ, 29.3898, 731.864502, null]
                var refi = destination.GetAsIndirectObject(0);
                
                var pr = (PRIndirectReference) refi;
                var r = pr.Reader;
                for (var i = 1; i <= r.NumberOfPages; i++) {
                    var pp = r.GetPageOrigRef(i);
                    if (pp.Generation == pr.Generation && pp.Number == pr.Number) return i;
                }
                throw new ArgumentException(MessageLocalization.GetComposedMessage("page.not.found"));
            }
            
            virtual public void SetDestinationPage(int newPage) {
                if (!IsInternal()) throw new ArgumentException(MessageLocalization.GetComposedMessage("cannot.change.destination.of.external.link"));
                this.newPage=newPage;
            }
            
            virtual public void TransformDestination(float a, float b, float c, float d, float e, float f) {
                if (!IsInternal()) throw new ArgumentException(MessageLocalization.GetComposedMessage("cannot.change.destination.of.external.link"));
                if (destination.GetAsName(1).Equals(PdfName.XYZ)) {
                    var x = destination.GetAsNumber(2).FloatValue;
                    var y = destination.GetAsNumber(3).FloatValue;
                    var xx = x * a + y * c + e;
                    var yy = x * b + y * d + f;
                    destination.ArrayList[2] = new PdfNumber(xx);
                    destination.ArrayList[3] = new PdfNumber(yy);
                }
            }
            
            virtual public void TransformRect(float a, float b, float c, float d, float e, float f) {
                var x = llx * a + lly * c + e;
                var y = llx * b + lly * d + f;
                llx = x;
                lly = y;
                x = urx * a + ury * c + e;
                y = urx * b + ury * d + f;
                urx = x;
                ury = y;
            }


            /**
             * Returns a String representation of the link.
             * @return	a String representation of the imported link
             * @since	2.1.6
             */
            public override string ToString() {
                var buf = new StringBuilder("Imported link: location [");
                buf.Append(llx);
                buf.Append(' ');
                buf.Append(lly);
                buf.Append(' ');
                buf.Append(urx);
                buf.Append(' ');
                buf.Append(ury);
                buf.Append("] destination ");
                buf.Append(destination);
                buf.Append(" parameters ");
                buf.Append(parameters);
                return buf.ToString();
            }
        }


        public virtual PdfObject GetAccessibleAttribute(PdfName key) {
            if (accessibleAttributes != null)
                return accessibleAttributes.ContainsKey(key) ? accessibleAttributes[key] : null;
            else
                return null;
        }

        public virtual void SetAccessibleAttribute(PdfName key, PdfObject value) {
            if (accessibleAttributes == null)
                accessibleAttributes = new Dictionary<PdfName, PdfObject>();
            accessibleAttributes[key] = value;
        }

        public virtual Dictionary<PdfName, PdfObject> GetAccessibleAttributes() {
            return accessibleAttributes;
        }

        public virtual PdfName Role {
            get => role;
            set => role = value;
        }

        public virtual AccessibleElementId ID {
            get {
                if (id == null)
                    id = new AccessibleElementId();
                return id;
            }
            set => id = value;
        }

        public virtual bool IsInline => false;
    }
}
