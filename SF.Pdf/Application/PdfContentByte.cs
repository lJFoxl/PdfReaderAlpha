
using System.Globalization;
using System.Text;
using SF.Pdf.Application.Interface;

namespace SF.Pdf.Application
{
    /**
     * <CODE>PdfContentByte</CODE> is an object containing the user positioned
     * text and graphic contents of a page. It knows how to apply the proper
     * font encoding.
     */
    public class PdfContentByte {
    
        /**
         * This class keeps the graphic state of the current page
         */
    
        public class GraphicState {
        
            /** This is the font in use */
            internal FontDetails fontDetails;
        
            /** This is the color in use */
            internal ColorDetails colorDetails;
        
            /** This is the font size in use */
            internal float size;
        
            /** The x position of the text line matrix. */
            protected internal float xTLM = 0;
            /** The y position of the text line matrix. */
            protected internal float yTLM = 0;

            internal float aTLM = 1;
            internal float bTLM = 0;
            internal float cTLM = 0;
            internal float dTLM = 1;

            internal float tx = 0;

            /** The current text leading. */
            protected internal float leading = 0;

            /** The current horizontal scaling */
            protected internal float scale = 100;

            /** The current character spacing */
            protected internal float charSpace = 0;

            /** The current word spacing */
            protected internal float wordSpace = 0;

            protected internal BaseColor colorFill = BaseColor.BLACK;
            protected internal BaseColor colorStroke = BaseColor.BLACK;
            protected internal int textRenderMode = TEXT_RENDER_MODE_FILL;
            protected internal AffineTransform CTM = new AffineTransform();
            protected internal PdfObject extGState = null;

            internal GraphicState() {
            }

            internal GraphicState(GraphicState cp) {
                CopyParameters(cp);
            }

            internal void CopyParameters(GraphicState cp) {
                fontDetails = cp.fontDetails;
                colorDetails = cp.colorDetails;
                size = cp.size;
                xTLM = cp.xTLM;
                yTLM = cp.yTLM;
                aTLM = cp.aTLM;
                bTLM = cp.bTLM;
                cTLM = cp.cTLM;
                dTLM = cp.dTLM;
                tx = cp.tx;
                leading = cp.leading;
                scale = cp.scale;
                charSpace = cp.charSpace;
                wordSpace = cp.wordSpace;
                colorFill = cp.colorFill;
                colorStroke = cp.colorStroke;
                CTM = (AffineTransform)cp.CTM.Clone();
                textRenderMode = cp.textRenderMode;
                extGState = cp.extGState;
            }

            internal void Restore(GraphicState restore) {
                CopyParameters(restore);
            }
        }
    
        /** The alignement is center */
        public const int ALIGN_CENTER = Element.ALIGN_CENTER;
        
        /** The alignement is left */
        public const int ALIGN_LEFT = Element.ALIGN_LEFT;
        
        /** The alignement is right */
        public const int ALIGN_RIGHT = Element.ALIGN_RIGHT;

        /** A possible line cap value */
        public const int LINE_CAP_BUTT = 0;
        /** A possible line cap value */
        public const int LINE_CAP_ROUND = 1;
        /** A possible line cap value */
        public const int LINE_CAP_PROJECTING_SQUARE = 2;
        
        /** A possible line join value */
        public const int LINE_JOIN_MITER = 0;
        /** A possible line join value */
        public const int LINE_JOIN_ROUND = 1;
        /** A possible line join value */
        public const int LINE_JOIN_BEVEL = 2;

        /** A possible text rendering value */
        public const int TEXT_RENDER_MODE_FILL = 0;
        /** A possible text rendering value */
        public const int TEXT_RENDER_MODE_STROKE = 1;
        /** A possible text rendering value */
        public const int TEXT_RENDER_MODE_FILL_STROKE = 2;
        /** A possible text rendering value */
        public const int TEXT_RENDER_MODE_INVISIBLE = 3;
        /** A possible text rendering value */
        public const int TEXT_RENDER_MODE_FILL_CLIP = 4;
        /** A possible text rendering value */
        public const int TEXT_RENDER_MODE_STROKE_CLIP = 5;
        /** A possible text rendering value */
        public const int TEXT_RENDER_MODE_FILL_STROKE_CLIP = 6;
        /** A possible text rendering value */
        public const int TEXT_RENDER_MODE_CLIP = 7;
    
        private static float[] unitRect = {0, 0, 0, 1, 1, 0, 1, 1};
        // membervariables
    
        /** This is the actual content */
        protected ByteBuffer content = new ByteBuffer();

        protected int markedContentSize = 0;


    
        /** This is the PdfDocument */
        protected internal PdfDocument pdf;
    
        /** This is the GraphicState in use */
        protected GraphicState state = new GraphicState();
    
        /** The list were we save/restore the layer depth */
        protected List<int> layerDepth;
        
        /** The list were we save/restore the state */
        protected List<GraphicState> stateList = new List<GraphicState>();
    
        /** The separator between commands.
         */    
        protected int separator = '\n';

		private bool suppressTagging;
        private int mcDepth = 0;
        private bool inText = false;

        private IList<IAccessibleElement> mcElements = new List<IAccessibleElement>();

        protected internal PdfContentByte duplicatedFrom = null;
        
        private static Dictionary<PdfName, string> abrev = new Dictionary<PdfName,string>();
        
        static PdfContentByte() {
            abrev[PdfName.BITSPERCOMPONENT] = "/BPC ";
            abrev[PdfName.COLORSPACE] = "/CS ";
            abrev[PdfName.DECODE] = "/D ";
            abrev[PdfName.DECODEPARMS] = "/DP ";
            abrev[PdfName.FILTER] = "/F ";
            abrev[PdfName.HEIGHT] = "/H ";
            abrev[PdfName.IMAGEMASK] = "/IM ";
            abrev[PdfName.INTENT] = "/Intent ";
            abrev[PdfName.INTERPOLATE] = "/I ";
            abrev[PdfName.WIDTH] = "/W ";
        }
        
        // methods to get the content of this object
    
        /**
         * Returns the <CODE>string</CODE> representation of this <CODE>PdfContentByte</CODE>-object.
         *
         * @return      a <CODE>string</CODE>
         */
    
        public override string ToString() {
            return content.ToString();
        }

        /**
         * [SUP-1395] If set, prevents iText from marking content and creating structure tags for items added to this content stream.
         * (By default, iText automatically marks content using BDC/EMC operators, and adds a structure tag for the new content
         * at the end of the page.)
         */
		public bool SuppressTagging {
			get => suppressTagging;
            set => suppressTagging = value;
        }

        /**
         * Checks if the content needs to be tagged.
         * @return false if no tags need to be added
         */
        public virtual bool IsTagged()
        {
            return !SuppressTagging;
        }

        /**
         * Gets the internal buffer.
         * @return the internal buffer
         */
        virtual public ByteBuffer InternalBuffer => content;

        /**
         * Gets the x position of the text line matrix.
         *
         * @return the x position of the text line matrix
         */
        virtual public float XTLM => state.xTLM;

        /**
         * Gets the y position of the text line matrix.
         *
         * @return the y position of the text line matrix
         */
        virtual public float YTLM => state.yTLM;

        /**
        * Gets the current character spacing.
        *
        * @return the current character spacing
        */
        virtual public float CharacterSpacing => state.charSpace;

        /**
        * Gets the current word spacing.
        *
        * @return the current word spacing
        */
        virtual public float WordSpacing => state.wordSpace;

        /**
        * Gets the current character spacing.
        *
        * @return the current character spacing
        */
        virtual public float HorizontalScaling => state.scale;

        /**
         * Gets the current text leading.
         *
         * @return the current text leading
         */
        virtual public float Leading => state.leading;

        private bool CompareColors(BaseColor c1, BaseColor c2) {
            if (c1 == null && c2 == null)
                return true;
            if (c1 == null || c2 == null)
                return false;
            if (c1 is ExtendedColor)
                return c1.Equals(c2);
            return c2.Equals(c1);
        }

        /**
        * Constructs a kern array for a text in a certain font
        * @param text the text
        * @param font the font
        * @return a PdfTextArray
        */
        public static PdfTextArray GetKernArray(string text, BaseFont font) {
            var pa = new PdfTextArray();
            var acc = new StringBuilder();
            var len = text.Length - 1;
            var c = text.ToCharArray();
            if (len >= 0)
                acc.Append(c, 0, 1);
            for (var k = 0; k < len; ++k) {
                var c2 = c[k + 1];
                var kern = font.GetKerning(c[k], c2);
                if (kern == 0) {
                    acc.Append(c2);
                }
                else {
                    pa.Add(acc.ToString());
                    acc.Length = 0;
                    acc.Append(c, k + 1, 1);
                    pa.Add(-kern);
                }
            }
            pa.Add(acc.ToString());
            return pa;
        }
 
        /**
         * Gets the size of this content.
         *
         * @return the size of the content
         */
        internal int Size => GetSize(true);

        internal int GetSize(bool includeMarkedContentSize) {
            if (includeMarkedContentSize)
                return content.Size;
            else
                return content.Size - markedContentSize;
        }

        /**
         * Gets the root outline.
         *
         * @return the root outline
         */
        virtual public PdfOutline RootOutline => pdf.RootOutline;

        /**
        * Computes the width of the given string taking in account
        * the current values of "Character spacing", "Word Spacing"
        * and "Horizontal Scaling".
        * The additional spacing is not computed for the last character
        * of the string.
        * @param text the string to get width of
        * @param kerned the kerning option
        * @return the width
        */

        virtual public float GetEffectiveStringWidth(string text, bool kerned) {
            var bf = state.fontDetails.BaseFont;
            
            float w;
            if (kerned)
                w = bf.GetWidthPointKerned(text, state.size);
            else
                w = bf.GetWidthPoint(text, state.size);
            
            if (state.charSpace != 0.0f && text.Length > 1) {
                w += state.charSpace * (text.Length -1);
            }

            if (state.wordSpace != 0.0f && !bf.IsVertical()) {
                for (var i = 0; i < (text.Length -1); i++) {
                    if (text[i] == ' ')
                        w += state.wordSpace;
                }
            }
            if (state.scale != 100.0)
                w = (w * state.scale) / 100.0f;
            
            //System.out.Println("String width = " + Float.ToString(w));
            return w;
        }
        
            /**
         * Computes the width of the given string taking in account
         * the current values of "Character spacing", "Word Spacing"
         * and "Horizontal Scaling".
         * The spacing for the last character is also computed.
         * It also takes into account kerning that can be specified within TJ operator (e.g. [(Hello) 123 (World)] TJ)
         * @param text the string to get width of
         * @param kerned the kerning option
         * @param kerning the kerning option from TJ array
         * @return the width
         */
        private float GetEffectiveStringWidth(string text, bool kerned, float kerning) {
            var bf = state.fontDetails.BaseFont;
            float w;
            if (kerned)
                w = bf.GetWidthPointKerned(text, state.size);
            else
                w = bf.GetWidthPoint(text, state.size);
            if (state.charSpace != 0.0f && text.Length > 0) {
                w += state.charSpace * (text.Length);
            }
            if (state.wordSpace != 0.0f && !bf.IsVertical()) {
                for (var i = 0; i < text.Length; i++) {
                    if (text[i] == ' ')
                        w += state.wordSpace;
                }
            }
            w -= kerning / 1000f * state.size;
            if (state.scale != 100.0)
                w = w * state.scale / 100.0f;
            return w;
        }

        /**
         * Generates an array of bezier curves to draw an arc.
         * <P>
         * (x1, y1) and (x2, y2) are the corners of the enclosing rectangle.
         * Angles, measured in degrees, start with 0 to the right (the positive X
         * axis) and increase counter-clockwise.  The arc : from startAng
         * to startAng+extent.  I.e. startAng=0 and extent=180 yields an openside-down
         * semi-circle.
         * <P>
         * The resulting coordinates are of the form float[]{x1,y1,x2,y2,x3,y3, x4,y4}
         * such that the curve goes from (x1, y1) to (x4, y4) with (x2, y2) and
         * (x3, y3) as their respective Bezier control points.
         * <P>
         * Note: this code was taken from ReportLab (www.reportlab.com), an excelent
         * PDF generator for Python.
         *
         * @param x1 a corner of the enclosing rectangle
         * @param y1 a corner of the enclosing rectangle
         * @param x2 a corner of the enclosing rectangle
         * @param y2 a corner of the enclosing rectangle
         * @param startAng starting angle in degrees
         * @param extent angle extent in degrees
         * @return a list of float[] with the bezier curves
         */

        public static List<double[]> BezierArc(float x1, float y1, float x2, float y2, float startAng, float extent) {
            return BezierArc((double)x1, (double)y1, (double)x2, (double)y2, (double)startAng, (double)extent);
        }

        /**
         * Generates an array of bezier curves to draw an arc.
         * <P>
         * (x1, y1) and (x2, y2) are the corners of the enclosing rectangle.
         * Angles, measured in degrees, start with 0 to the right (the positive X
         * axis) and increase counter-clockwise.  The arc : from startAng
         * to startAng+extent.  I.e. startAng=0 and extent=180 yields an openside-down
         * semi-circle.
         * <P>
         * The resulting coordinates are of the form float[]{x1,y1,x2,y2,x3,y3, x4,y4}
         * such that the curve goes from (x1, y1) to (x4, y4) with (x2, y2) and
         * (x3, y3) as their respective Bezier control points.
         * <P>
         * Note: this code was taken from ReportLab (www.reportlab.com), an excelent
         * PDF generator for Python.
         *
         * @param x1 a corner of the enclosing rectangle
         * @param y1 a corner of the enclosing rectangle
         * @param x2 a corner of the enclosing rectangle
         * @param y2 a corner of the enclosing rectangle
         * @param startAng starting angle in degrees
         * @param extent angle extent in degrees
         * @return a list of float[] with the bezier curves
         */
        public static List<double[]> BezierArc(double x1, double y1, double x2, double y2, double startAng, double extent) {
            double tmp;
            if (x1 > x2) {
                tmp = x1;
                x1 = x2;
                x2 = tmp;
            }
            if (y2 > y1) {
                tmp = y1;
                y1 = y2;
                y2 = tmp;
            }
        
            double fragAngle;
            int Nfrag;
            if (Math.Abs(extent) <= 90f) {
                fragAngle = extent;
                Nfrag = 1;
            }
            else {
                Nfrag = (int)(Math.Ceiling(Math.Abs(extent)/90f));
                fragAngle = extent / Nfrag;
            }
            var x_cen = (x1+x2)/2f;
            var y_cen = (y1+y2)/2f;
            var rx = (x2-x1)/2f;
            var ry = (y2-y1)/2f;
            double halfAng = (float)(fragAngle * Math.PI / 360.0);
            double kappa = (float)(Math.Abs(4.0 / 3.0 * (1.0 - Math.Cos(halfAng)) / Math.Sin(halfAng)));
            var pointList = new List<double[]>();
            for (var i = 0; i < Nfrag; ++i) {
                var theta0 = (float)((startAng + i*fragAngle) * Math.PI / 180.0);
                var theta1 = (float)((startAng + (i+1)*fragAngle) * Math.PI / 180.0);
                var cos0 = (float)Math.Cos(theta0);
                var cos1 = (float)Math.Cos(theta1);
                var sin0 = (float)Math.Sin(theta0);
                var sin1 = (float)Math.Sin(theta1);
                if (fragAngle > 0f) {
                    pointList.Add(new double[]{x_cen + rx * cos0,
                                                 y_cen - ry * sin0,
                                                 x_cen + rx * (cos0 - kappa * sin0),
                                                 y_cen - ry * (sin0 + kappa * cos0),
                                                 x_cen + rx * (cos1 + kappa * sin1),
                                                 y_cen - ry * (sin1 - kappa * cos1),
                                                 x_cen + rx * cos1,
                                                 y_cen - ry * sin1});
                }
                else {
                    pointList.Add(new double[]{x_cen + rx * cos0,
                                                 y_cen - ry * sin0,
                                                 x_cen + rx * (cos0 + kappa * sin0),
                                                 y_cen - ry * (sin0 - kappa * cos0),
                                                 x_cen + rx * (cos1 - kappa * sin1),
                                                 y_cen - ry * (sin1 + kappa * cos1),
                                                 x_cen + rx * cos1,
                                                 y_cen - ry * sin1});
                }
            }
            return pointList;
        }

        /**
         * Draws an ellipse inscribed within the rectangle x1,y1,x2,y2.
         *
         * @param x1 a corner of the enclosing rectangle
         * @param y1 a corner of the enclosing rectangle
         * @param x2 a corner of the enclosing rectangle
         * @param y2 a corner of the enclosing rectangle
         */

        public virtual void Ellipse(double x1, double y1, double x2, double y2) {
            Ellipse((float)x1, (float)y1, (float)x2, (float)y2);
        }

        /**
         * Gets the <CODE>PdfDocument</CODE> in use by this object.
         * @return the <CODE>PdfDocument</CODE> in use by this object
         */
        virtual public PdfDocument PdfDocument => pdf;

        public virtual void InheritGraphicState(PdfContentByte parentCanvas) {
            this.state = parentCanvas.state;
            this.stateList = parentCanvas.stateList;
        }
    
        /** Outputs a <CODE>char</CODE> directly to the content.
         * @param c the <CODE>char</CODE>
         */    
        virtual public void SetLiteral(char c) {
            content.Append(c);
        }
    
        /** Throws an error if it is a pattern.
         * @param t the object to check
         */    
        internal void CheckNoPattern(PdfTemplate t) {
            if (t.Type == PdfTemplate.TYPE_PATTERN)
                throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.use.of.a.pattern.a.template.was.expected"));
        }

        
        internal virtual PageResources PageResources => pdf.PageResources;

        /**
        * Sets the default colorspace.
        * @param name the name of the colorspace. It can be <CODE>PdfName.DEFAULTGRAY</CODE>, <CODE>PdfName.DEFAULTRGB</CODE>
        * or <CODE>PdfName.DEFAULTCMYK</CODE>
        * @param obj the colorspace. A <CODE>null</CODE> or <CODE>PdfNull</CODE> removes any colorspace with the same name
        */    
        public virtual void SetDefaultColorspace(PdfName name, PdfObject obj) {
            var prs = PageResources;
            prs.AddDefaultColor(name, obj);
        }

        internal int GetMcDepth() {
            if (duplicatedFrom != null)
                return duplicatedFrom.GetMcDepth();
            else
                return mcDepth;
        }

        internal void SetMcDepth(int value) {
            if (duplicatedFrom != null)
                duplicatedFrom.SetMcDepth(value);
            else
                mcDepth = value;
        }

        internal IList<IAccessibleElement> GetMcElements()
        {
            if (duplicatedFrom != null)
                return duplicatedFrom.GetMcElements();
            else
                return mcElements;
        }

        internal void SetMcElements(IList<IAccessibleElement> value)
        {
            if (duplicatedFrom != null)
                duplicatedFrom.SetMcElements(value);
            else
                mcElements = value;
        }

        internal void UpdateTx(string text, float Tj) {
            state.tx += GetEffectiveStringWidth(text, false, Tj);
        }

        private void SaveColor(BaseColor color, bool fill) {
            if (fill) {
                state.colorFill = color;
            } else {
                state.colorStroke = color;
            }
        }

        class UncoloredPattern : PatternColor {
            protected internal BaseColor color;
            protected internal float tint;

            protected internal UncoloredPattern(PdfPatternPainter p, BaseColor color, float tint) : base(p) {
                this.color = color;
                this.tint = tint;
            }

            public override bool Equals(object obj) {
                return obj is UncoloredPattern && (((UncoloredPattern)obj).Painter).Equals(this.Painter) && (((UncoloredPattern)obj).color).Equals(this.color) && ((UncoloredPattern)obj).tint == this.tint;
            }

            public override int GetHashCode() {
                return base.GetHashCode();
            }

        }

        virtual protected internal bool InText => inText;
    }
}
