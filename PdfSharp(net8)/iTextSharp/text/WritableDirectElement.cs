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

using PdfSharp_net8_.iTextSharp.text.api;
using PdfSharp_net8_.iTextSharp.text.pdf;

namespace PdfSharp_net8_.iTextSharp.text {

    /**
     * An element that is not an element, it holds {@link Element#WRITABLE_DIRECT}
     * as Element type. It implements WriterOperation to do operations on the
     * {@link PdfWriter} and the {@link Document} that must be done at the time of
     * the writing. Much like a {@link VerticalPositionMark} but little different.
     *
     * @author itextpdf.com
     *
     */
    public abstract class WritableDirectElement : IElement, IWriterOperation {


        public static readonly int DIRECT_ELEMENT_TYPE_UNKNOWN = 0;
        public static readonly int DIRECT_ELEMENT_TYPE_HEADER = 1;

        protected int directElementType = DIRECT_ELEMENT_TYPE_UNKNOWN;

        public int DirectElemenType
        {
            get { return directElementType; }
        }

        public WritableDirectElement()
        {
        }

        public WritableDirectElement(int directElementType)
        {
            this.directElementType = directElementType;
        }

        /*
         * (non-Javadoc)
         *
         * @see com.itextpdf.text.Element#process(com.itextpdf.text.ElementListener)
         */
        virtual public bool Process(IElementListener listener) {
            throw new NotSupportedException();
        }

        /**
         * @return {@link Element#WRITABLE_DIRECT}
         */
        virtual public int Type {
            get {
                return Element.WRITABLE_DIRECT;
            }
        }

        /*
         * (non-Javadoc)
         *
         * @see com.itextpdf.text.Element#isContent()
         */
        virtual public bool IsContent() {
            return false;
        }

        /*
         * (non-Javadoc)
         *
         * @see com.itextpdf.text.Element#isNestable()
         */
        virtual public bool IsNestable() {
            throw new NotSupportedException();
        }

        /*
         * (non-Javadoc)
         *
         * @see com.itextpdf.text.Element#getChunks()
         */
        virtual public IList<Chunk> Chunks {
            get {
                return new List<Chunk>();
            }
        }

        public abstract void Write(PdfWriter writer, Document doc);
    }
}
