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

namespace PdfSharp_net8_.iTextSharp.text.io {

    /**
     * Represents an abstract source that bytes can be read from.  This class forms the foundation for all byte input in iText. 
     * Implementations do not keep track of a current 'position', but rather provide absolute get methods.  Tracking position
     * should be handled in classes that use RandomAccessSource internally (via composition).
     * @since 5.3.5
     */
    public interface IRandomAccessSource : IDisposable {
        /**
         * Gets a byte at the specified position
         * @param position
         * @return the byte, or -1 if EOF is reached
         */
        int Get(long position);
        
        /**
         * Gets an array at the specified position.  If the number of bytes requested cannot be read, the bytes that can be
         * read will be placed in bytes and the number actually read will be returned.
         * @param position the position in the RandomAccessSource to read from
         * @param bytes output buffer
         * @param off offset into the output buffer where results will be placed
         * @param len the number of bytes to read
         * @return the number of bytes actually read, or -1 if the file is at EOF
         */
        int Get(long position, byte[] bytes, int off, int len);
        
        /**
         * @return the length of this source
         */
        long Length {get;}
        
        /**
         * Closes this source.  The underlying data structure or source (if any) will also be closed
         * @throws IOException
         */
        void Close();
    }
}
