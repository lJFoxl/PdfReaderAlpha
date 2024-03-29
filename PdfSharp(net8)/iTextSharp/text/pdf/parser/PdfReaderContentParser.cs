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

namespace PdfSharp_net8_.iTextSharp.text.pdf.parser {

    /**
     * A utility class that makes it cleaner to process content from pages of a PdfReader
     * through a specified RenderListener.
     * @since 5.0.2
     */
    public class PdfReaderContentParser {
        /** the reader this parser will process */
        private PdfReader reader;
        
        public PdfReaderContentParser(PdfReader reader) {
            this.reader = reader;
        }

        /**
         * ������������ ���������� � ���������� ������ ��������, ��������� ��������� ��������������.
         * ����� ��������� �������������� ���������������� ��������� ��������
         * @param <E> the type of the renderListener - this makes it easy to chain calls
         * @param pageNumber the page number to process
         * @param renderListener the listener that will receive render callbacks
         * @param additionalContentOperators an optional dictionary of custom IContentOperators for rendering instructions
         * @return the provided renderListener
         * @throws IOException if operations on the reader fail
         */
        public virtual E ProcessContent<E>(int pageNumber, E renderListener, IDictionary<string, IContentOperator> additionalContentOperators) where E : IRenderListener {
            PdfDictionary pageDic = reader.GetPageN(pageNumber);
            PdfDictionary resourcesDic = pageDic.GetAsDict(PdfName.RESOURCES);
            
            PdfContentStreamProcessor processor = new PdfContentStreamProcessor(renderListener);
            foreach (KeyValuePair<string, IContentOperator> entry in additionalContentOperators) {
                processor.RegisterContentOperator(entry.Key, entry.Value);
            }
            processor.ProcessContent(ContentByteUtils.GetContentBytesForPage(reader, pageNumber), resourcesDic);        
            return renderListener;
        }

        /**
         * Processes content from the specified page number using the specified listener
         * @param <E> the type of the renderListener - this makes it easy to chain calls
         * @param pageNumber the page number to process
         * @param renderListener the listener that will receive render callbacks
         * @return the provided renderListener
         * @throws IOException if operations on the reader fail
         */
        public virtual E ProcessContent<E>(int pageNumber, E renderListener) where E : IRenderListener {
            return ProcessContent(pageNumber, renderListener, new Dictionary<string, IContentOperator>());
        }
    }
}
