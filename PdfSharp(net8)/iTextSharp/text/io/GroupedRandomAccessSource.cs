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
     * A RandomAccessSource that is based on a set of underlying sources, treating the sources as if they were a contiguous block of data.
     * @since 5.3.5
     */
    internal class GroupedRandomAccessSource : IRandomAccessSource {
        /**
         * The underlying sources (along with some meta data to quickly determine where each source begins and ends)
         */
        private readonly SourceEntry[] sources;
        
        /**
         * Cached value to make multiple reads from the same underlying source more efficient
         */
        private SourceEntry currentSourceEntry;

        /**
         * Cached size of the underlying channel
         */
        private readonly long size;



        /**
         * Constructs a new {@link GroupedRandomAccessSource} based on the specified set of sources
         * @param sources the sources used to build this group
         */
        public GroupedRandomAccessSource(ICollection<IRandomAccessSource> sources) {
            this.sources = new SourceEntry[sources.Count];
            
            long totalSize = 0;
            int i = 0;
            foreach (IRandomAccessSource ras in sources) {
                this.sources[i] = new SourceEntry(i, ras, totalSize);
                ++i;
                totalSize += ras.Length;
            }
            size = totalSize;
            currentSourceEntry = this.sources[sources.Count-1];
            SourceInUse(currentSourceEntry.source);
        }

        /**
         * For a given offset, return the index of the source that contains the specified offset.
         * This is an optimization feature to help optimize the access of the correct source without having to iterate
         * through every single source each time.  It is safe to always return 0, in which case the full set of sources will be searched.
         * Subclasses should override this method if they are able to compute the source index more efficiently (for example {@link FileChannelRandomAccessSource} takes advantage of fixed size page buffers to compute the index) 
         * @param offset the offset
         * @return the index of the input source that contains the specified offset, or 0 if unknown
         */
        protected internal virtual int GetStartingSourceIndex(long offset){
            if (offset >= currentSourceEntry.firstByte)
                return currentSourceEntry.index;

            return 0;
        }
        
        /**
         * Returns the SourceEntry that contains the byte at the specified offset  
         * sourceReleased is called as a notification callback so subclasses can take care of cleanup when the source is no longer the active source
         * @param offset the offset of the byte to look for
         * @return the SourceEntry that contains the byte at the specified offset
         * @throws IOException if there is a problem with IO (usually the result of the sourceReleased() call)
         */
        private SourceEntry GetSourceEntryForOffset(long offset) {
            if (offset >= size)
                return null;
            
            if (offset >= currentSourceEntry.firstByte && offset <= currentSourceEntry.lastByte)
                return currentSourceEntry;
            
            // hook to allow subclasses to release resources if necessary
            SourceReleased(currentSourceEntry.source);
            
            int startAt = GetStartingSourceIndex(offset);
            
            for(int i = startAt; i < sources.Length; i++){ 
                if (offset >= sources[i].firstByte && offset <= sources[i].lastByte){
                    currentSourceEntry = sources[i];
                    SourceInUse(currentSourceEntry.source);
                    return currentSourceEntry;
                }
            }
            
            return null;
            
        }
        
        /**
         * Called when a given source is no longer the active source.  This gives subclasses the abilty to release resources, if appropriate. 
         * @param source the source that is no longer the active source
         * @throws IOException if there are any problems
         */
        protected internal virtual void SourceReleased(IRandomAccessSource source) {
            // by default, do nothing
        }
        
        /**
         * Called when a given source is about to become the active source.  This gives subclasses the abilty to retrieve resources, if appropriate. 
         * @param source the source that is about to become the active source
         * @throws IOException if there are any problems
         */
        protected internal virtual void SourceInUse(IRandomAccessSource source) {
            // by default, do nothing
        }
        
        /** 
         * {@inheritDoc} 
         * The source that contains the byte at position is retrieved, the correct offset into that source computed, then the value
         * from that offset in the underlying source is returned.
         */  
        public virtual int Get(long position) {
            SourceEntry entry = GetSourceEntryForOffset(position);
            
            if (entry == null) // we have run out of data to read from
                return -1;
            
            return entry.source.Get(entry.OffsetN(position));

        }
        
        /** 
         * {@inheritDoc} 
         */  
        public virtual int Get(long position, byte[] bytes, int off, int len) {
            SourceEntry entry = GetSourceEntryForOffset(position);
            
            if (entry == null) // we have run out of data to read from
                return -1;
            
            long offN = entry.OffsetN(position);

            int remaining = len;
            
            while(remaining > 0){
                if (entry == null) // we have run out of data to read from
                    break;
                if (offN > entry.source.Length)
                    break;
                
                int count = entry.source.Get(offN, bytes, off, remaining);
                if (count == -1)
                    break;
                
                off += count;
                position += count;
                remaining -= count;

                offN = 0;
                entry = GetSourceEntryForOffset(position);
            }
            return remaining == len ? -1 : len - remaining; 
        }

        
        /** 
         * {@inheritDoc} 
         */  
        public virtual long Length {
            get {
                return size;
            }
        }

        /**
         * {@inheritDoc}
         * Closes all of the underlying sources
         */
        public virtual void Close() {
            foreach (SourceEntry entry in  sources) {
                entry.source.Close();
            }
        }

        virtual public void Dispose() {
            Close();
        }

        /**
         * Used to track each source, along with useful meta data 
         */
        private sealed class SourceEntry{
            /**
             * The underlying source
             */
            internal readonly IRandomAccessSource source;
            /**
             * The first byte (in the coordinates of the GroupedRandomAccessSource) that this source contains
             */
            internal readonly long firstByte;
            /**
             * The last byte (in the coordinates of the GroupedRandomAccessSource) that this source contains
             */
            internal readonly long lastByte;
            /**
             * The index of this source in the GroupedRandomAccessSource
             */
            internal readonly int index;
            
            /**
             * Standard constructor
             * @param index the index
             * @param source the source
             * @param offset the offset of the source in the GroupedRandomAccessSource
             */
            internal SourceEntry(int index, IRandomAccessSource source, long offset) {
                this.index = index;
                this.source = source;
                this.firstByte = offset;
                this.lastByte = offset + source.Length - 1;
            }
            
            /**
             * Given an absolute offset (in the GroupedRandomAccessSource coordinates), calculate the effective offset in the underlying source
             * @param absoluteOffset the offset in the parent GroupedRandomAccessSource
             * @return the effective offset in the underlying source
             */
            internal long OffsetN(long absoluteOffset){
                return absoluteOffset - firstByte;
            }
        }
    }
}
