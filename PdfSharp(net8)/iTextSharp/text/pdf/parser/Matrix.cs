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
     * Keeps all the values of a 3 by 3 matrix
     * and allows you to do some math with matrices.
     * @since   2.1.4
     */
    public class Matrix {
        /** the row=1, col=1 position ('a') in the matrix. */
        public const int I11 = 0; 
        /** the row=1, col=2 position ('b') in the matrix. */
        public const int I12 = 1; 
        /** the row=1, col=3 position (always 0 for 2-D) in the matrix. */
        public const int I13 = 2;
        /** the row=2, col=1 position ('c') in the matrix. */
        public const int I21 = 3; 
        /** the row=2, col=2 position ('d') in the matrix. */
        public const int I22 = 4;  
        /** the row=2, col=3 position (always 0 for 2-D) in the matrix. */
        public const int I23 = 5;  
        /** the row=3, col=1 ('e', or X translation) position in the matrix. */
        public const int I31 = 6;  
        /** the row=3, col=2 ('f', or Y translation) position in the matrix. */
        public const int I32 = 7;  
        /** the row=3, col=3 position (always 1 for 2-D) in the matrix. */
        public const int I33 = 8;   
        
        /** the values inside the matrix (the identity matrix by default).*/
        /** default initialization is performed in the default constructor.*/
        private readonly float[] vals = new float[9];

        /**
         * constructs a new Matrix with identity.
         * !shall be called from any other constructor!
         */
        public Matrix() { 
            //here we are initializing matrix with default values
            // ! not using inline array initializer because it is ~20 times slower in .net 2.0 !
            vals[I11] = 1;
            vals[I12] = 0;
            vals[I13] = 0;
            vals[I21] = 0;
            vals[I22] = 1;
            vals[I23] = 0;
            vals[I31] = 0;
            vals[I32] = 0;
            vals[I33] = 1;
        }

        /**
         * Constructs a matrix that represents translation
         * @param tx
         * @param ty
         */
        public Matrix(float tx, float ty): this(){
            vals[I31] = tx;
            vals[I32] = ty;
        }
        
        /**
         * Creates a Matrix with 6 specified entries
         * @param a
         * @param b
         * @param c
         * @param d
         * @param e
         * @param f
         */
        public Matrix(float a, float b, float c, float d, float e, float f) {
            vals[I11] = a;
            vals[I12] = b;
            vals[I13] = 0;
            vals[I21] = c;
            vals[I22] = d;
            vals[I23] = 0;
            vals[I31] = e;
            vals[I32] = f;
            vals[I33] = 1;
        }
        
        /**
         * Gets a specific value inside the matrix.
         * @param   index   an array index corresponding with a value inside the matrix
         * @return  the value at that specific position.
         */
        public float this[int index] {
            get {
                return vals[index];
            }
        }
        
        /**
         * multiplies this matrix by 'b' and returns the result
         * See http://en.wikipedia.org/wiki/Matrix_multiplication
         * @param by The matrix to multiply by
         * @return  the resulting matrix
         */
        virtual public Matrix Multiply(Matrix by){
            Matrix rslt = new Matrix();
            
            float[] a = vals;
            float[] b = by.vals;
            float[] c = rslt.vals;
            
            c[I11] = a[I11]*b[I11] + a[I12]*b[I21] + a[I13]*b[I31];  
            c[I12] = a[I11]*b[I12] + a[I12]*b[I22] + a[I13]*b[I32]; 
            c[I13] = a[I11]*b[I13] + a[I12]*b[I23] + a[I13]*b[I33]; 
            c[I21] = a[I21]*b[I11] + a[I22]*b[I21] + a[I23]*b[I31];  
            c[I22] = a[I21]*b[I12] + a[I22]*b[I22] + a[I23]*b[I32]; 
            c[I23] = a[I21]*b[I13] + a[I22]*b[I23] + a[I23]*b[I33]; 
            c[I31] = a[I31]*b[I11] + a[I32]*b[I21] + a[I33]*b[I31];  
            c[I32] = a[I31]*b[I12] + a[I32]*b[I22] + a[I33]*b[I32]; 
            c[I33] = a[I31]*b[I13] + a[I32]*b[I23] + a[I33]*b[I33]; 
            
            return rslt;
        }

        /**
         * Subtracts a matrix from this matrix and returns the results
         * @param arg the matrix to subtract from this matrix
         * @return a Matrix object
         */
        virtual public Matrix Subtract(Matrix arg){
            Matrix rslt = new Matrix();
            
            float[] a = vals;
            float[] b = arg.vals;
            float[] c = rslt.vals;
            
            c[I11] = a[I11]-b[I11];  
            c[I12] = a[I12]-b[I12]; 
            c[I13] = a[I13]-b[I13]; 
            c[I21] = a[I21]-b[I21];  
            c[I22] = a[I22]-b[I22]; 
            c[I23] = a[I23]-b[I23]; 
            c[I31] = a[I31]-b[I31];  
            c[I32] = a[I32]-b[I32]; 
            c[I33] = a[I33]-b[I33]; 

            return rslt;
        }
        
        /**
         * Computes the determinant of the matrix.
         * @return the determinant of the matrix
         */
        virtual public float GetDeterminant(){
            // ref http://en.wikipedia.org/wiki/Determinant   
            // note that in PDF, I13 and I23 are always 0 and I33 is always 1
            // so this could be simplified/faster
            return    vals[I11] * vals[I22] * vals[I33] 
                    + vals[I12] * vals[I23] * vals[I31]
                    + vals[I13] * vals[I21] * vals[I32]
                    - vals[I11] * vals[I23] * vals[I32]
                    - vals[I12] * vals[I21] * vals[I33]
                    - vals[I13] * vals[I22] * vals[I31];
        }

        /**
         * Checks equality of matrices.
         * @param obj   the other Matrix that needs to be compared with this matrix.
         * @return  true if both matrices are equal
         * @see java.lang.Object#equals(java.lang.Object)
         */
        public override bool Equals(Object obj) {
            if (!(obj is Matrix))
                return false;
            Matrix other = (Matrix)obj;
            for (int i = 0; i < vals.Length; i++) {
                if (vals[i] != other.vals[i])
                    return false;
            }
            return true;
        }
        
        /**
         * Generates a hash code for this object.
         * @return  the hash code of this object
         * @see java.lang.Object#hashCode()
         */
        public override int GetHashCode() {            
            int result = 1;
            for (int i = 0; i < vals.Length; i++)
                result = 31 * result + vals[i].GetHashCode();
            return result;
        }
        
        /**
         * Generates a String representation of the matrix.
         * @return  the values, delimited with tabs and newlines.
         * @see java.lang.Object#toString()
         */
        public override String ToString() {
            return  vals[I11] + "\t" + vals[I12] + "\t" + vals[I13] + "\n" + 
                    vals[I21] + "\t" + vals[I22] + "\t" + vals[I13] + "\n" +
                    vals[I31] + "\t" + vals[I32] + "\t" + vals[I33];
        }
    }
}
