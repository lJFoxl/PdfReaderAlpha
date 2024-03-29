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

using PdfSharp_net8_.iTextSharp.text.pdf.draw;

namespace PdfSharp_net8_.iTextSharp.text
{
	public class TabStop
	{
        public static TabStop NewInstance(float currentPosition, float tabInterval)
        {
            currentPosition = (float)Math.Round(currentPosition * 1000) / 1000;
            tabInterval = (float)Math.Round(tabInterval * 1000) / 1000;

            TabStop tabStop = new TabStop(currentPosition + tabInterval - currentPosition % tabInterval);
            return tabStop;
        }

        public enum Alignment
        {
            LEFT,
            RIGHT,
            CENTER,
            ANCHOR
        }

        protected float position;
        protected Alignment alignment = Alignment.LEFT;
        protected IDrawInterface leader;
        protected char anchorChar = '.';

        public TabStop(float position) : this(position, Alignment.LEFT)
        {
           
        }

        public TabStop(float position, IDrawInterface leader) : this(position, leader, Alignment.LEFT)
        {
        }

        public TabStop(float position, Alignment alignment)
            : this(position, null, alignment)
        {
        }

        public TabStop(float position, Alignment alignment, char anchorChar)
            : this(position, null, alignment, anchorChar)
        {
        }

        public TabStop(float position, IDrawInterface leader, Alignment alignment)
            : this(position, leader, alignment, '.')
        {
        }

        public TabStop(float position, IDrawInterface leader, Alignment alignment, char anchorChar)
        {
            this.position = position;
            this.leader = leader;
            this.alignment = alignment;
            this.anchorChar = anchorChar;
        }

        public TabStop(TabStop tabStop)
            : this(tabStop.Position, tabStop.Leader, tabStop.Align, tabStop.AnchorChar)
        {
        }

	    virtual public float Position
	    {
	        get { return position; }
	        set { position = value; }
	    }

	    virtual public Alignment Align
	    {
	        get { return alignment; }
	        set { alignment = value; }
	    }

	    virtual public IDrawInterface Leader
	    {
	        get { return leader; }
	        set { leader = value; }
	    }

	    virtual public char AnchorChar
	    {
	        get { return anchorChar; }
	        set { anchorChar = value; }
	    }

	    virtual public float GetPosition(float tabPosition, float currentPosition, float anchorPosition)
        {
            float newPosition = position;
            float textWidth = currentPosition - tabPosition;
            switch (alignment)
            {
                case Alignment.RIGHT:
                    if (tabPosition + textWidth < position)
                    {
                        newPosition = position - textWidth;
                    }
                    else
                    {
                        newPosition = tabPosition;
                    }
                    break;
                case Alignment.CENTER:
                    if (tabPosition + textWidth / 2f < position)
                    {
                        newPosition = position - textWidth / 2f;
                    }
                    else
                    {
                        newPosition = tabPosition;
                    }
                    break;
                case Alignment.ANCHOR:
                    if (!float.IsNaN(anchorPosition))
                    {
                        if (anchorPosition < position)
                        {
                            newPosition = position - (anchorPosition - tabPosition);
                        }
                        else
                        {
                            newPosition = tabPosition;
                        }
                    }
                    else
                    {
                        if (tabPosition + textWidth < position)
                        {
                            newPosition = position - textWidth;
                        }
                        else
                        {
                            newPosition = tabPosition;
                        }
                    }
                    break;
            }
            return newPosition;
        }
	}
}
