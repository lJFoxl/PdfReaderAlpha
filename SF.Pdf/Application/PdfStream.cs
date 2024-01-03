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

using System.Reflection.Metadata;

namespace SF.Pdf.Application {
    /**
     * <CODE>PdfStream</CODE> is the Pdf stream object.
     * <P>
     * A stream, like a string, is a sequence of characters. However, an application can
     * read a small portion of a stream at a time, while a string must be read in its entirety.
     * For this reason, objects with potentially large amounts of data, such as images and
     * page descriptions, are represented as streams.<BR>
     * A stream consists of a dictionary that describes a sequence of characters, followed by
     * the keyword <B>stream</B>, followed by zero or more lines of characters, followed by
     * the keyword <B>endstream</B>.<BR>
     * All streams must be <CODE>PdfIndirectObject</CODE>s. The stream dictionary must be a direct
     * object. The keyword <B>stream</B> that follows the stream dictionary should be followed by
     * a carriage return and linefeed or just a linefeed.<BR>
     * Remark: In this version only the FLATEDECODE-filter is supported.<BR>
     * This object is described in the 'Portable Document Format Reference Manual version 1.3'
     * section 4.8 (page 41-53).<BR>
     * </P>
     *
     * @see        PdfObject
     * @see        PdfDictionary
     */

    public class PdfStream : PdfDictionary {
    
        // membervariables
    
        /**
        * A possible compression level.
        * @since   2.1.3
        */
        public const int DEFAULT_COMPRESSION = -1;
        /**
        * A possible compression level.
        * @since   2.1.3
        */
        public const int NO_COMPRESSION = 0;
        /**
        * A possible compression level.
        * @since   2.1.3
        */
        public const int BEST_SPEED = 1;
        /**
        * A possible compression level.
        * @since   2.1.3
        */
        public const int BEST_COMPRESSION = 9;        
        
        /** is the stream compressed? */
        protected bool compressed = false;

        /**
        * The level of compression.
        * @since   2.1.3
        */
        protected int compressionLevel = NO_COMPRESSION;
    
        protected MemoryStream streamBytes = null;
    
        protected Stream inputStream;
        protected PdfIndirectReference iref;
        protected int inputStreamLength = -1;
        protected int rawLength;
        
        
        // constructors
    
        /**
         * Constructs a <CODE>PdfStream</CODE>-object.
         *
         * @param        bytes            content of the new <CODE>PdfObject</CODE> as an array of <CODE>byte</CODE>.
         */
 
        public PdfStream(byte[] bytes) : base() {
            type = STREAM;
            this.bytes = bytes;
            rawLength = bytes.Length;
            Put(PdfName.LENGTH, new PdfNumber(bytes.Length));
        }
        
        /**
         * Constructs a <CODE>PdfStream</CODE>-object.
         */
    
        protected PdfStream() : base() {
            type = STREAM;
        }



        public virtual int RawLength => rawLength;

        // methods
    
        /**
        * Compresses the stream.
        */
        virtual public void FlateCompress() {
            FlateCompress(DEFAULT_COMPRESSION);
        }
        
        /**
        * Compresses the stream.
        * @param compressionLevel the compression level (0 = best speed, 9 = best compression, -1 is default)
        * @since   2.1.3
        */
        virtual public void FlateCompress(int compressionLevel) {
            if (!Document.Compress)
                return;
            // check if the flateCompress-method has already been used
            if (compressed) {
                return;
            }
            this.compressionLevel = compressionLevel;
            if (inputStream != null) {
                compressed = true;
                return;
            }
            // check if a filter already exists
            var filter = PdfReader.GetPdfObject(Get(PdfName.FILTER));
            if (filter != null) {
                if (filter.IsName()) {
                    if (PdfName.FLATEDECODE.Equals(filter))
                        return;
                }
                else if (filter.IsArray()) {
                    if (((PdfArray) filter).Contains(PdfName.FLATEDECODE))
                        return;
                }
                else {
                    throw new PdfException(MessageLocalization.GetComposedMessage("stream.could.not.be.compressed.filter.is.not.a.name.or.array"));
                }
            }
            // compress
            var stream = new MemoryStream();
            var zip = new ZDeflaterOutputStream(stream, compressionLevel);
            if (streamBytes != null)
                streamBytes.WriteTo(zip);
            else
                zip.Write(bytes, 0, bytes.Length);
            //zip.Close();
            zip.Finish();
            // update the object
            streamBytes = stream;
            bytes = null;
            Put(PdfName.LENGTH, new PdfNumber(streamBytes.Length));
            if (filter == null) {
                Put(PdfName.FILTER, PdfName.FLATEDECODE);
            }
            else {
                var filters = new PdfArray(filter);
                filters.Add(0, PdfName.FLATEDECODE);
                Put(PdfName.FILTER, filters);
            }
            compressed = true;
        }
        
        /**
        * Writes the data content to an <CODE>Stream</CODE>.
        * @param os the destination to write to
        * @throws IOException on error
        */    
        virtual public void WriteContent(Stream os) {
            if (streamBytes != null)
                streamBytes.WriteTo(os);
            else if (bytes != null)
                os.Write(bytes, 0, bytes.Length);
        }

        /**
        * @see com.lowagie.text.pdf.PdfObject#toString()
        */
        public override string ToString() {
            if (Get(PdfName.TYPE) == null) return "Stream";
            return "Stream of type: " + Get(PdfName.TYPE);
        }
    }
}
