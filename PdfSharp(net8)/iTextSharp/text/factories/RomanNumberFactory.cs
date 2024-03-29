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

namespace PdfSharp_net8_.iTextSharp.text.factories {
    /**
    * This class can produce String combinations representing a roman number.
    */
    public class RomanNumberFactory {
        /**
        * Helper class for Roman Digits
        */
        internal class RomanDigit {

            /** part of a roman number */
            public char digit;

            /** value of the roman digit */
            public int value;

            /** can the digit be used as a prefix */
            public bool pre;

            /**
            * Constructs a roman digit
            * @param digit the roman digit
            * @param value the value
            * @param pre can it be used as a prefix
            */
            internal RomanDigit(char digit, int value, bool pre) {
                this.digit = digit;
                this.value = value;
                this.pre = pre;
            }
        }
        
        /**
        * Array with Roman digits.
        */
        private static RomanDigit[] roman = {
            new RomanDigit('m', 1000, false),
            new RomanDigit('d', 500, false),
            new RomanDigit('c', 100, true),
            new RomanDigit('l', 50, false),
            new RomanDigit('x', 10, true),
            new RomanDigit('v', 5, false),
            new RomanDigit('i', 1, true)
        };
        
        /** 
        * Changes an int into a lower case roman number.
        * @param index the original number
        * @return the roman number (lower case)
        */
        public static String GetString(int index) {
            StringBuilder buf = new StringBuilder();

            // lower than 0 ? Add minus
            if (index < 0) {
                buf.Append('-');
                index = -index;
            }

            // greater than 3000
            if (index > 3000) {
                buf.Append('|');
                buf.Append(GetString(index / 1000));
                buf.Append('|');
                // remainder
                index = index - (index / 1000) * 1000;
            }

            // number between 1 and 3000
            int pos = 0;
            while (true) {
                // loop over the array with values for m-d-c-l-x-v-i
                RomanDigit dig = roman[pos];
                // adding as many digits as we can
                while (index >= dig.value) {
                    buf.Append(dig.digit);
                    index -= dig.value;
                }
                // we have the complete number
                if (index <= 0) {
                    break;
                }
                // look for the next digit that can be used in a special way
                int j = pos;
                while (!roman[++j].pre);

                // does the special notation apply?
                if (index + roman[j].value >= dig.value) {
                    buf.Append(roman[j].digit).Append(dig.digit);
                    index -= dig.value - roman[j].value;
                }
                pos++;
            }
            return buf.ToString();
        }
        
        /** 
        * Changes an int into a lower case roman number.
        * @param index the original number
        * @return the roman number (lower case)
        */
        public static String GetLowerCaseString(int index) {
            return GetString(index);        
        }
        
        /** 
        * Changes an int into an upper case roman number.
        * @param index the original number
        * @return the roman number (lower case)
        */
        public static String GetUpperCaseString(int index) {
            return GetString(index).ToUpper(global::System.Globalization.CultureInfo.InvariantCulture);      
        }

        /** 
        * Changes an int into a roman number.
        * @param index the original number
        * @return the roman number (lower case)
        */
        public static String GetString(int index, bool lowercase) {
            if (lowercase) {
                return GetLowerCaseString(index);
            }
            else {
                return GetUpperCaseString(index);
            }
        }
    }
}
