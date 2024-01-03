using SF.Pdf.Application.collection;
using SF.Pdf.Application.Interface;

namespace SF.Pdf.Application {
    /**
    * <CODE>PdfDocument</CODE> is the class that is used by <CODE>PdfWriter</CODE>
    * to translate a <CODE>Document</CODE> into a PDF with different pages.
    * <P>
    * A <CODE>PdfDocument</CODE> always listens to a <CODE>Document</CODE>
    * and adds the Pdf representation of every <CODE>Element</CODE> that is
    * added to the <CODE>Document</CODE>.
    *
    * @see      com.lowagie.text.Document
    * @see      com.lowagie.text.DocListener
    * @see      PdfWriter
    */

    public class PdfDocument : Document {
        
        /**
        * <CODE>PdfInfo</CODE> is the PDF InfoDictionary.
        * <P>
        * A document's trailer may contain a reference to an Info dictionary that provides information
        * about the document. This optional dictionary may contain one or more keys, whose values
        * should be strings.<BR>
        * This object is described in the 'Portable Document Format Reference Manual version 1.3'
        * section 6.10 (page 120-121)
        */
        
        public class PdfInfo : PdfDictionary {
            
            // constructors
            
            /**
            * Construct a <CODE>PdfInfo</CODE>-object.
            */
            
            internal PdfInfo() {
                AddProducer();
                AddCreationDate();
            }
            
            /**
            * Constructs a <CODE>PdfInfo</CODE>-object.
            *
            * @param        author      name of the author of the document
            * @param        title       title of the document
            * @param        subject     subject of the document
            */
            
            internal PdfInfo(string author, string title, string subject) : base() {
                AddTitle(title);
                AddSubject(subject);
                AddAuthor(author);
            }
            
            /**
            * Adds the title of the document.
            *
            * @param    title       the title of the document
            */
            
            internal void AddTitle(string title) {
                Put(PdfName.TITLE, new PdfString(title, PdfObject.TEXT_UNICODE));
            }
            
            /**
            * Adds the subject to the document.
            *
            * @param    subject     the subject of the document
            */
            
            internal void AddSubject(string subject) {
                Put(PdfName.SUBJECT, new PdfString(subject, PdfObject.TEXT_UNICODE));
            }
            
            /**
            * Adds some keywords to the document.
            *
            * @param    keywords        the keywords of the document
            */
            
            internal void AddKeywords(string keywords) {
                Put(PdfName.KEYWORDS, new PdfString(keywords, PdfObject.TEXT_UNICODE));
            }
            
            /**
            * Adds the name of the author to the document.
            *
            * @param    author      the name of the author
            */
            
            internal void AddAuthor(string author) {
                Put(PdfName.AUTHOR, new PdfString(author, PdfObject.TEXT_UNICODE));
            }
            
            /**
            * Adds the name of the creator to the document.
            *
            * @param    creator     the name of the creator
            */
            
            internal void AddCreator(string creator) {
                Put(PdfName.CREATOR, new PdfString(creator, PdfObject.TEXT_UNICODE));
            }
            
            /**
            * Adds the name of the producer to the document.
            */
            
            internal void AddProducer() {
                // This line may only be changed by Bruno Lowagie or Paulo Soares
                Put(PdfName.PRODUCER, new PdfString(Version.GetInstance().GetVersion));
                // Do not edit the line above!
            }
            
            /**
            * Adds the date of creation to the document.
            */
            
            internal void AddCreationDate() {
                PdfString date = new PdfDate();
                Put(PdfName.CREATIONDATE, date);
                Put(PdfName.MODDATE, date);
            }
            
            internal void Addkey(string key, string value) {
                if (key.Equals("Producer") || key.Equals("CreationDate"))
                    return;
                Put(new PdfName(key), new PdfString(value, PdfObject.TEXT_UNICODE));
            }
        }
        
        /**
        * <CODE>PdfCatalog</CODE> is the PDF Catalog-object.
        * <P>
        * The Catalog is a dictionary that is the root node of the document. It contains a reference
        * to the tree of pages contained in the document, a reference to the tree of objects representing
        * the document's outline, a reference to the document's article threads, and the list of named
        * destinations. In addition, the Catalog indicates whether the document's outline or thumbnail
        * page images should be displayed automatically when the document is viewed and wether some location
        * other than the first page should be shown when the document is opened.<BR>
        * In this class however, only the reference to the tree of pages is implemented.<BR>
        * This object is described in the 'Portable Document Format Reference Manual version 1.3'
        * section 6.2 (page 67-71)
        */
        
        internal class PdfCatalog : PdfDictionary {

            
            internal PdfAction OpenAction {
                set => Put(PdfName.OPENACTION, value);
            }
        }
        
    // CONSTRUCTING A PdfDocument/PdfWriter INSTANCE

        /**
        * Constructs a new PDF document.
        * @throws DocumentException on error
        */
        public PdfDocument() {
            AddProducer();
            AddCreationDate();
        }
        

        internal Dictionary<AccessibleElementId, PdfStructureElement> structElements = new Dictionary<AccessibleElementId, PdfStructureElement>();

        protected internal bool openMCDocument = false;

        protected Dictionary<object, int[]> structParentIndices = new Dictionary<object, int[]>();

        protected Dictionary<object, int> markPoints = new Dictionary<object, int>();


        
    // LISTENER METHODS START
        
    //  [L0] ElementListener interface
        
        /** This is the PdfContentByte object, containing the text. */
        protected internal PdfContentByte text;
        
        /** This is the PdfContentByte object, containing the borders and other Graphics. */
        protected internal PdfContentByte graphics;
        
        /** This represents the leading of the lines. */
        protected internal float leading = 0;
        
        /**
        * Getter for the current leading.
        * @return  the current leading
        * @since   2.1.2
        */
        virtual public float Leading {
            get => leading;
            set => leading = value;
        }
        /** This is the current height of the document. */
        protected internal float currentHeight = 0;
        
        /**
        * Signals that onParagraph is valid (to avoid that a Chapter/Section title is treated as a Paragraph).
        * @since 2.1.2
        */
        protected bool isSectionTitle = false;

        /** This represents the current alignment of the PDF Elements. */
        protected internal int alignment = Element.ALIGN_LEFT;
        
        /** The current active <CODE>PdfAction</CODE> when processing an <CODE>Anchor</CODE>. */
        protected internal PdfAction anchorAction = null;

        /**
         * The current tab settings.
         * @return	the current
         * @since 5.4.0
         */
        protected TabSettings tabSettings;

        /**
         * Signals that the current leading has to be subtracted from a YMark object when positive
         * and save current leading
         * @since 2.1.2
         */
        private Stack<float> leadingStack = new Stack<float>();

        private PdfBody body;

        /**
         * Save current @leading
         */
        virtual protected void PushLeading() {
            leadingStack.Push(leading);
        }

        /**
         * Restore @leading from leadingStack
         */
        virtual protected void PopLeading()
        {
            leading = leadingStack.Pop();
            if (leadingStack.Count > 0)
                leading = leadingStack.Peek();
        }

        /**
         * Getter and setter for the current tab stops.
         * @since	5.4.0
         */
        virtual public TabSettings TabSettings {
            get => tabSettings;
            set => tabSettings = value;
        }

    //  [L3] DocListener interface

        protected internal int textEmptySize;

    //  [L4] DocListener interface

        /** margin in x direction starting from the left. Will be valid in the next page */
        protected float nextMarginLeft;
        
        /** margin in x direction starting from the right. Will be valid in the next page */
        protected float nextMarginRight;
        
        /** margin in y direction starting from the top. Will be valid in the next page */
        protected float nextMarginTop;
        
        /** margin in y direction starting from the bottom. Will be valid in the next page */
        protected float nextMarginBottom;

        // DOCLISTENER METHODS END
    
        /** Signals that OnOpenDocument should be called. */
        protected internal bool firstPageEvent = true;
    
        /**
        * Initializes a page.
        * <P>
        * If the footer/header is set, it is printed.
        * @throws DocumentException on error
        */

        /** The line that is currently being written. */
        protected internal PdfLine line = null;
        
        /** The lines that are written until now. */
        protected internal List<PdfLine> lines = new List<PdfLine>();
        
        /**
        * Adds the current line to the list of lines and also adds an empty line.
        * @throws DocumentException on error
        */
        
        virtual protected internal void NewLine() {
            lastElementType = -1;
            CarriageReturn();
            if (lines != null && lines.Count > 0) {
                lines.Add(line);
                currentHeight += line.Height;
            }
            line = new PdfLine(IndentLeft, IndentRight, alignment, leading);
        }

        /**
         * line.height() is usually the same as the leading
         * We should take leading into account if it is not the same as the line.height
         *
         * @return float combined height of the line
         * @since 5.5.1
         */
        protected virtual float CalculateLineHeight() {
            var tempHeight = line.Height;

            if (tempHeight != leading) {
                tempHeight += leading;
            }

            return tempHeight;
        }
        
        /**
        * If the current line is not empty or null, it is added to the arraylist
        * of lines and a new empty line is added.
        * @throws DocumentException on error
        */
        virtual protected internal void CarriageReturn() {
            // the arraylist with lines may not be null
            if (lines == null) {
                lines = new List<PdfLine>();
            }
            // If the current line is not null or empty
            if (line != null && line.Size > 0) {
                // we check if the end of the page is reached (bugfix by Francois Gravel)
                if (currentHeight + CalculateLineHeight() > IndentTop - IndentBottom) {
                    // if the end of the line is reached, we start a newPage which will flush existing lines
                    // then move to next page but before then we need to exclude the current one that does not fit
                    // After the new page we add the current line back in
                    if (currentHeight != 0) {
                        var overflowLine = line;
                        line = null;
                        NewPage();
                        line = overflowLine;
                        //update left indent because of mirror margins.
                        overflowLine.left = IndentLeft;
                    }
                }
                currentHeight += line.Height;
                lines.Add(line);
                pageEmpty = false;
            }
            if (imageEnd > -1 && currentHeight > imageEnd) {
                imageEnd = -1;
                indentation.imageIndentRight = 0;
                indentation.imageIndentLeft = 0;
            }
            // a new current line is constructed
            line = new PdfLine(IndentLeft, IndentRight, alignment, leading);
        }
        
        /**
        * Gets the current vertical page position.
        * @param ensureNewLine Tells whether a new line shall be enforced. This may cause side effects 
        *   for elements that do not terminate the lines they've started because those lines will get
        *   terminated. 
        * @return The current vertical page position.
        */
        virtual public float GetVerticalPosition(bool ensureNewLine) {
            // ensuring that a new line has been started.
            if (ensureNewLine) {
                EnsureNewLine();
            }
            return Top - currentHeight - indentation.indentTop;
        }

        /** Holds the type of the last element, that has been added to the document. */
        protected internal int lastElementType = -1;    


        /** The characters to be applied the hanging punctuation. */
        internal const string hangingPunctuation = ".,;:'";
        
        protected internal Indentation indentation = new Indentation();
        public class Indentation {
            /** This represents the current indentation of the PDF Elements on the left side. */
            internal float indentLeft = 0;
            
            /** Indentation to the left caused by a section. */
            internal float sectionIndentLeft = 0;
            
            /** This represents the current indentation of the PDF Elements on the left side. */
            internal float listIndentLeft = 0;
            
            /** This is the indentation caused by an image on the left. */
            internal float imageIndentLeft = 0;
            
            /** This represents the current indentation of the PDF Elements on the right side. */
            internal float indentRight = 0;
            
            /** Indentation to the right caused by a section. */
            internal float sectionIndentRight = 0;
            
            /** This is the indentation caused by an image on the right. */
            internal float imageIndentRight = 0;
            
            /** This represents the current indentation of the PDF Elements on the top side. */
            internal float indentTop = 0;
            
            /** This represents the current indentation of the PDF Elements on the bottom side. */
            internal float indentBottom = 0;
        }
        
        /**
        * Gets the indentation on the left side.
        *
        * @return   a margin
        */
        
        virtual protected internal float IndentLeft => GetLeft(indentation.indentLeft + indentation.listIndentLeft + indentation.imageIndentLeft + indentation.sectionIndentLeft);

        /**
        * Gets the indentation on the right side.
        *
        * @return   a margin
        */
        
        virtual protected internal float IndentRight => GetRight(indentation.indentRight + indentation.sectionIndentRight + indentation.imageIndentRight);

        /**
        * Gets the indentation on the top side.
        *
        * @return   a margin
        */
        
        virtual protected internal float IndentTop => GetTop(indentation.indentTop);

        /**
        * Gets the indentation on the bottom side.
        *
        * @return   a margin
        */
        
        virtual protected internal float IndentBottom => GetBottom(indentation.indentBottom);

        /**
         * Calls addSpacing(float, float, Font, boolean (false)).
         */
        protected internal virtual void AddSpacing(float extraspace, float oldleading, Font f) {
            AddSpacing(extraspace, oldleading, f, false);
        }

        /**
        * Adds extra space.
        */
        // this method should probably be rewritten
        virtual protected internal void AddSpacing(float extraspace, float oldleading, Font f, bool spacingAfter) {
            if (extraspace == 0) 
                return;

            if (pageEmpty) 
                return;
           

            var height = spacingAfter ? extraspace : CalculateLineHeight();

            if (currentHeight + height > IndentTop - IndentBottom) {
                NewPage();
                return;
            }

            leading = extraspace;
            CarriageReturn();
            if (f.IsUnderlined() || f.IsStrikethru()) {
                f = new Font(f);
                var style = f.Style;
                style &= ~Font.UNDERLINE;
                style &= ~Font.STRIKETHRU;
                f.SetStyle(style);
            }
            var space = new Chunk(" ", f);
            if (spacingAfter && pageEmpty) {
                space = new Chunk("", f);
            }
            space.Process(this);
            CarriageReturn();

            leading = oldleading;
        }
        
    //  Info Dictionary and Catalog

        /** some meta information about the Document. */
        protected internal PdfInfo info = new PdfInfo();

        /**
        * Gets the <CODE>PdfInfo</CODE>-object.
        *
        * @return   <CODE>PdfInfo</COPE>
        */
        internal PdfInfo Info => info;

        //  [C1] outlines

        /** This is the root outline of the document. */
        protected internal PdfOutline rootOutline;
        
        /** This is the current <CODE>PdfOutline</CODE> in the hierarchy of outlines. */
        protected internal PdfOutline currentOutline;
        
        /**
        * Adds a named outline to the document .
        * @param outline the outline to be added
        * @param name the name of this local destination
        */
        internal void AddOutline(PdfOutline outline, string name) {
            LocalDestination(name, outline.PdfDestination);
        }
        
        /**
        * Gets the root outline. All the outlines must be created with a parent.
        * The first level is created with this outline.
        * @return the root outline
        */
        virtual public PdfOutline RootOutline => rootOutline;

        internal void CalculateOutlineCount() {
            if (rootOutline.Kids.Count == 0)
                return;
            TraverseOutlineCount(rootOutline);
        }

        internal void TraverseOutlineCount(PdfOutline outline) {
            var kids = outline.Kids;
            var parent = outline.Parent;
            if (kids.Count == 0) {
                if (parent != null) {
                    parent.Count = parent.Count + 1;
                }
            }
            else {
                for (var k = 0; k < kids.Count; ++k) {
                    TraverseOutlineCount(kids[k]);
                }
                if (parent != null) {
                    if (outline.Open) {
                        parent.Count = outline.Count + parent.Count + 1;
                    }
                    else {
                        parent.Count = parent.Count + 1;
                        outline.Count = -outline.Count;
                    }
                }
            }
        }
        
 
    //  [C3] PdfViewerPreferences interface

        /** Contains the Viewer preferences of this PDF document. */
        protected PdfViewerPreferencesImp viewerPreferences = new PdfViewerPreferencesImp();
        /** @see com.lowagie.text.pdf.interfaces.PdfViewerPreferences#setViewerPreferences(int) */
        internal int ViewerPreferences {
            set => this.viewerPreferences.ViewerPreferences = value;
        }

        /** @see com.lowagie.text.pdf.interfaces.PdfViewerPreferences#addViewerPreference(com.lowagie.text.pdf.PdfName, com.lowagie.text.pdf.PdfObject) */
        internal void AddViewerPreference(PdfName key, PdfObject value) {
            this.viewerPreferences.AddViewerPreference(key, value);
        }

    //  [C4] Page labels

        protected internal PdfPageLabels pageLabels;

        virtual public PdfPageLabels PageLabels {
            get => pageLabels;
            internal set => this.pageLabels = value;
        }

        
        /**
        * Stores the destinations keyed by name. Value is
        * <CODE>Object[]{PdfAction,PdfIndirectReference,PdfDestintion}</CODE>.
        */
        protected internal SortedDictionary<string,Destination> localDestinations = new SortedDictionary<string,Destination>(StringComparer.Ordinal);
        
        /**
        * Stores a list of document level JavaScript actions.
        */
        private int jsCounter;
        protected internal Dictionary<string, PdfObject> documentLevelJS = new Dictionary<string,PdfObject>();

        internal Dictionary<string, PdfObject> GetDocumentLevelJS() {
            return documentLevelJS;
        }

        protected internal Dictionary<string, PdfObject> documentFileAttachment = new Dictionary<string, PdfObject>();

        internal void AddFileAttachment(string description, PdfFileSpecification fs) {
            if (description == null) {
                var desc = (PdfString)fs.Get(PdfName.DESC);
                if (desc == null) {
                    description = ""; 
                }
                else {
                    description = PdfEncodings.ConvertToString(desc.GetBytes(), null);
                }
            }
            fs.AddDescription(description, true);
            if (description.Length == 0)
                description = "Unnamed";
            var fn = PdfEncodings.ConvertToString(new PdfString(description, PdfObject.TEXT_UNICODE).GetBytes(), null);
            var k = 0;
            while (documentFileAttachment.ContainsKey(fn)) {
                ++k;
                fn = PdfEncodings.ConvertToString(new PdfString(description + " " + k, PdfObject.TEXT_UNICODE).GetBytes(), null);
            }
            documentFileAttachment[fn] = fs.Reference;
        }
        
        internal Dictionary<string, PdfObject> GetDocumentFileAttachment() {
            return documentFileAttachment;
        }

    //  [C6] document level actions

        protected internal string openActionName;

        internal void SetOpenAction(string name) {
            openActionName = name;
            openActionAction = null;
        }
        
        protected internal PdfAction openActionAction;

        internal void SetOpenAction(PdfAction action) {
            openActionAction = action;
            openActionName = null;
        }

        protected internal PdfDictionary additionalActions;

        internal void AddAdditionalAction(PdfName actionType, PdfAction action)  {
            if (additionalActions == null)  {
                additionalActions = new PdfDictionary();
            }
            if (action == null)
                additionalActions.Remove(actionType);
            else
                additionalActions.Put(actionType, action);
            if (additionalActions.Size == 0)
                additionalActions = null;
        }
        
    //  [C7] portable collections

        protected internal PdfCollection collection;

        /**
        * Sets the collection dictionary.
        * @param collection a dictionary of type PdfCollection
        */
        virtual public PdfCollection Collection {
            set => this.collection = value;
        }

    //  [C8] AcroForm
        
        internal PdfAnnotationsImp annotationsImp;

        /**
        * Gets the AcroForm object.
        * @return the PdfAcroform object of the PdfDocument
        */
        virtual public PdfAcroForm AcroForm => annotationsImp.AcroForm;

        internal int SigFlags {
            set => annotationsImp.SigFlags = value;
        }
        
        internal void AddCalculationOrder(PdfFormField formField) {
            annotationsImp.AddCalculationOrder(formField);
        }

        internal void AddAnnotation(PdfAnnotation annot) {
            pageEmpty = false;
            annotationsImp.AddAnnotation(annot);
        }
        
        protected PdfString language;
        internal void SetLanguage(string language) {
            this.language = new PdfString(language);
        }

    //	[F12] tagged PDF
    //	[U1] page sizes

        /** This is the size of the next page. */
        protected Rectangle nextPageSize = null;
        
        /** This is the size of the several boxes of the current Page. */
        protected Dictionary<string, PdfRectangle> thisBoxSize = new Dictionary<string,PdfRectangle>();
        
        /** This is the size of the several boxes that will be used in
        * the next page. */
        protected Dictionary<string, PdfRectangle> boxSize = new Dictionary<string, PdfRectangle>();
        
        internal Rectangle CropBoxSize {
            set => SetBoxSize("crop", value);
        }
        
        internal void SetBoxSize(string boxName, Rectangle size) {
            if (size == null)
                boxSize.Remove(boxName);
            else
                boxSize[boxName] = new PdfRectangle(size);
        }

        /**
        * Gives the size of a trim, art, crop or bleed box, or null if not defined.
        * @param boxName crop, trim, art or bleed
        */
        internal Rectangle GetBoxSize(string boxName) {
            PdfRectangle r;
            thisBoxSize.TryGetValue(boxName, out r);
            if (r != null)
                return r.Rectangle;
            return null;
        }
        
    //	[U2] empty pages

        /** This checks if the page is empty. */
        private bool pageEmpty = true;

        protected PdfDictionary pageAA = null;

        internal void SetPageAction(PdfName actionType, PdfAction action) {
            if (pageAA == null) {
                pageAA = new PdfDictionary();
            }
            pageAA.Put(actionType, action);
        }
        //	[M0] Page resources contain references to fonts, extgstate, images,...

        /** This are the page resources of the current Page. */
        protected internal PageResources pageResources;
        
        internal PageResources PageResources => pageResources;

        //	[M3] Images

        /** Holds value of property strictImageSequence. */
        protected internal bool strictImageSequence = false;    

        /** Setter for property strictImageSequence.
        * @param strictImageSequence New value of property strictImageSequence.
        *
        */
        internal bool StrictImageSequence {
            set => this.strictImageSequence = value;
            get => strictImageSequence;
        }
     
        /** This is the position where the image ends. */
        protected internal float imageEnd = -1;
        
        /**
        * Method added by Pelikan Stephan
        * @see com.lowagie.text.DocListener#clearTextWrap()
        */
        virtual public void ClearTextWrap() {
            var tmpHeight = imageEnd - currentHeight;
            if (line != null) {
                tmpHeight += line.Height;
            }
            if ((imageEnd > -1) && (tmpHeight > 0)) {
                CarriageReturn();
                currentHeight += tmpHeight;
            }
        }

        virtual public int GetStructParentIndex(object obj) {
            int[] i;
            structParentIndices.TryGetValue(obj, out i);
            if(i == null) {
                i = new int[] { structParentIndices.Count, 0 };
                structParentIndices[obj] = i;
            }
            return i[0];
        }

        virtual public int GetNextMarkPoint(object obj) {
            int[] i;
            structParentIndices.TryGetValue(obj, out i);
            if(i == null) {
                i = new int[] { structParentIndices.Count, 0 };
                structParentIndices[obj] = i;
            }
            var markPoint = i[1];
            i[1]++;
            return markPoint;
        }

        virtual public int[] GetStructParentIndexAndNextMarkPoint(object obj) {
            int[] i;
            structParentIndices.TryGetValue(obj, out i);
            if(i == null) {
                i = new int[] { structParentIndices.Count, 0 };
                structParentIndices[obj] = i;
            }
            var markPoint = i[1];
            i[1]++;
            return new int[] {i[0], markPoint};
        }


        /** This is the image that could not be shown on a previous page. */
        protected internal Image imageWait = null;
        
        /**
        * Adds an image to the document.
        * @param image the <CODE>Image</CODE> to add
        * @throws PdfException on error
        * @throws DocumentException on error
        */        
        virtual protected internal void Add(Image image) {
            if (image.HasAbsolutePosition()) {
                graphics.AddImage(image);
                pageEmpty = false;
                return;
            }
            
            // if there isn't enough room for the image on this page, save it for the next page
            if (currentHeight != 0 && IndentTop - currentHeight - image.ScaledHeight < IndentBottom) {
                if (!strictImageSequence && imageWait == null) {
                    imageWait = image;
                    return;
                }
                NewPage();
                if (currentHeight != 0 && IndentTop - currentHeight - image.ScaledHeight < IndentBottom) {
                    imageWait = image;
                    return;
                }
            }
            pageEmpty = false;
            // avoid endless loops
            if (image == imageWait)
                imageWait = null;
            var textwrap = (image.Alignment & Image.TEXTWRAP) == Image.TEXTWRAP
                           && (image.Alignment & Image.MIDDLE_ALIGN) != Image.MIDDLE_ALIGN;
            var underlying = (image.Alignment & Image.UNDERLYING) == Image.UNDERLYING;
            var diff = leading / 2;
            if (textwrap) {
                diff += leading;
            }
            var lowerleft = IndentTop - currentHeight - image.ScaledHeight - diff;
            var mt = image.GetMatrix();
            var startPosition = IndentLeft - mt[4];
            if ((image.Alignment & Image.RIGHT_ALIGN) == Image.RIGHT_ALIGN) startPosition = IndentRight - image.ScaledWidth - mt[4];
            if ((image.Alignment & Image.MIDDLE_ALIGN) == Image.MIDDLE_ALIGN) startPosition = IndentLeft + ((IndentRight - IndentLeft - image.ScaledWidth) / 2) - mt[4];
            if (image.HasAbsoluteX()) startPosition = image.AbsoluteX;
            if (textwrap) {
                if (imageEnd < 0 || imageEnd < currentHeight + image.ScaledHeight + diff) {
                    imageEnd = currentHeight + image.ScaledHeight + diff;
                }
                if ((image.Alignment & Image.RIGHT_ALIGN) == Image.RIGHT_ALIGN) {
                    // indentation suggested by Pelikan Stephan
                    indentation.imageIndentRight += image.ScaledWidth + image.IndentationLeft;
                }
                else {
                    // indentation suggested by Pelikan Stephan
                    indentation.imageIndentLeft += image.ScaledWidth + image.IndentationRight;
                }
            }
            else {
                if ((image.Alignment & Image.RIGHT_ALIGN) == Image.RIGHT_ALIGN) startPosition -= image.IndentationRight;
                else if ((image.Alignment & Image.MIDDLE_ALIGN) == Image.MIDDLE_ALIGN) startPosition += image.IndentationLeft - image.IndentationRight;
                else startPosition -= image.IndentationRight;
            }
            graphics.AddImage(image, mt[0], mt[1], mt[2], mt[3], startPosition, lowerleft - mt[5]);
            if (!(textwrap || underlying)) {
                currentHeight += image.ScaledHeight + diff;
                FlushLines();
                text.MoveText(0, - (image.ScaledHeight + diff));
                NewLine();
            }
        }
        
        internal List<IElement> floatingElements = new List<IElement>();

        internal void AddDiv(PdfDiv div) {
            if (floatingElements == null) {
                floatingElements = new List<IElement>();
            }
            floatingElements.Add(div);
        }
     
        internal bool FitsPage(PdfPTable table, float margin) {
            if (!table.LockedWidth) {
                var totalWidth = (IndentRight - IndentLeft) * table.WidthPercentage / 100;
                table.TotalWidth = totalWidth;
            }
            // ensuring that a new line has been started.
            EnsureNewLine();
            float spaceNeeded = table.SkipFirstHeader ? table.TotalHeight - table.HeaderHeight : table.TotalHeight;
            return spaceNeeded + (currentHeight > 0 ? table.SpacingBefore : 0f)
                <= IndentTop - currentHeight - IndentBottom - margin;
        } 


        private PdfLine GetLastLine() {
            if (lines.Count > 0)
                return lines[lines.Count - 1];
            else
                return null;
        }

        /**
         * @since 5.0.1
         */
        public class Destination {
            public PdfAction action;
            public PdfIndirectReference reference;
            public PdfDestination destination;
        }
    }
}
