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

namespace SF.Pdf.Application {

    public class CMapByteCid : AbstractCMap {
        private List<char[]> planes = new List<char[]>();

        public CMapByteCid() {
            planes.Add(new char[256]);
        }
        
        internal override void AddChar(PdfString mark, PdfObject code) {
            if (!(code is PdfNumber))
                return;
            EncodeSequence(DecodeStringToByte(mark), (char)((PdfNumber)code).IntValue);
        }
        
        private void EncodeSequence(byte[] seqs, char cid) {
            var size = seqs.Length - 1;
            var nextPlane = 0;
            int one;
            char[] plane;
            for (var idx = 0; idx < size; ++idx) {
                plane = planes[nextPlane];
                one = seqs[idx] & 0xff;
                var c = plane[one];
                if (c != 0 && (c & 0x8000) == 0)
                    throw new ArgumentException(MessageLocalization.GetComposedMessage("inconsistent.mapping"));
                if (c == 0) {
                    planes.Add(new char[256]);
                    c = (char)(planes.Count - 1 | 0x8000);
                    plane[one] = c;
                }
                nextPlane = c & 0x7fff;
            }
            plane = planes[nextPlane];
            one = seqs[size] & 0xff;
            var c2 = plane[one];
            if ((c2 & 0x8000) != 0)
                throw new ArgumentException(MessageLocalization.GetComposedMessage("inconsistent.mapping"));
            plane[one] = cid;
        }
        
        /**
         * 
         * @param seq
         * @return the cid code or -1 for end
         */
        virtual public int DecodeSingle(CMapSequence seq) {
            var end = seq.off + seq.len;
            var currentPlane = 0;
            while (seq.off < end) {
                var one = seq.seq[seq.off++] & 0xff;
                --seq.len;
                var plane = planes[currentPlane];
                int cid = plane[one];
                if ((cid & 0x8000) == 0) {
                    return cid;
                }
                else
                    currentPlane = cid & 0x7fff;
            }
            return -1;
        }

        virtual public string DecodeSequence(CMapSequence seq) {
            var sb = new StringBuilder();
            var cid = 0;
            while ((cid = DecodeSingle(seq)) >= 0) {
                sb.Append((char)cid);
            }
            return sb.ToString();
        }
    }
}