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

namespace PdfSharp_net8_.Helper.text.html {

    /**
     * Static final values of supported HTML tags and attributes.
     * @since 5.0.6
     * @deprecated since 5.5.2
     */
    [Obsolete]
    public static class HtmlTags {
    
	    // tag names
    	
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const string A = "a";
	    /** name of a tag */
	    public const string B = "b";
	    /** name of a tag */
	    public const string BODY = "body";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const string BLOCKQUOTE = "blockquote";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const string BR = "br";
	    /** name of a tag */
	    public const string DIV = "div";
	    /** name of a tag */
	    public const string EM = "em";
	    /** name of a tag */
	    public const string FONT = "font";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const string H1 = "h1";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const string H2 = "h2";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const string H3 = "h3";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const string H4 = "h4";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const string H5 = "h5";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const string H6 = "h6";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const string HR = "hr";
	    /** name of a tag */
	    public const string I = "i";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const string IMG = "img";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const string LI = "li";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const string OL = "ol";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const string P = "p";
	    /** name of a tag */
	    public const string PRE = "pre";
	    /** name of a tag */
	    public const string S = "s";
	    /** name of a tag */
	    public const string SPAN = "span";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const string STRIKE = "strike";
	    /** name of a tag */
	    public const string STRONG = "strong";
	    /** name of a tag */
	    public const string SUB = "sub";
	    /** name of a tag */
	    public const string SUP = "sup";
	    /** name of a tag */
	    public const string TABLE = "table";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const string TD = "td";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const string TH = "th";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const string TR = "tr";
	    /** name of a tag */
	    public const string U = "u";
	    /**
	     * name of a tag.
	     * @since 5.0.6 (reorganized all constants)
	     */
	    public const string UL = "ul";

	    // attributes (some are not real HTML attributes!)

	    /** name of an attribute */
	    public const string ALIGN = "align";
	    /**
	     * name of an attribute
	     * @since 5.0.6
	     */
	    public const string BGCOLOR = "bgcolor";
	    /**
	     * name of an attribute
	     * @since 5.0.6
	     */
	    public const string BORDER = "border";
	    /** name of an attribute */
	    public const string CELLPADDING = "cellpadding";
	    /** name of an attribute */
	    public const string COLSPAN = "colspan";
	    /**
	     * name of an attribute
	     * @since 5.0.6
	     */
	    public const string EXTRAPARASPACE = "extraparaspace";
	    /**
	     * name of an attribute
	     * @since 5.0.6
	     */
	    public const string ENCODING = "encoding";
	    /**
	     * name of an attribute
	     * @since 5.0.6
	     */
	    public const string FACE = "face";
	    /**
	     * Name of an attribute.
	     * @since 5.0.6
	     */
	    public const string HEIGHT = "height";
	    /**
	     * Name of an attribute.
	     * @since 5.0.6
	     */
	    public const string HREF = "href";
	    /**
	     * Name of an attribute.
	     * @since 5.0.6
	     */
	    public const string HYPHENATION = "hyphenation";
	    /**
	     * Name of an attribute.
	     * @since 5.0.6
	     */
	    public const string IMAGEPATH = "image_path";
	    /**
	     * Name of an attribute.
	     * @since 5.0.6
	     */
	    public const string INDENT = "indent";
	    /**
	     * Name of an attribute.
	     * @since 5.0.6
	     */
	    public const string LEADING = "leading";
	    /** name of an attribute */
	    public const string ROWSPAN = "rowspan";
	    /** name of an attribute */
	    public const string SIZE = "size";
	    /**
	     * Name of an attribute.
	     * @since 5.0.6
	     */
	    public const string SRC = "src";
	    /**
	     * Name of an attribute.
	     * @since 5.0.6
	     */
	    public const string VALIGN = "valign";
	    /** name of an attribute */
	    public const string WIDTH = "width";
    	
	    // attribute values

	    /** the possible value of an alignment attribute */
	    public const string ALIGN_LEFT = "left";
	    /** the possible value of an alignment attribute */
	    public const string ALIGN_CENTER = "center";
	    /** the possible value of an alignment attribute */
	    public const string ALIGN_RIGHT = "right";
	    /** 
	     * The possible value of an alignment attribute.
	     * @since 5.0.6
	     */
	    public const string ALIGN_JUSTIFY = "justify";
	    /** 
	     * The possible value of an alignment attribute.
	     * @since 5.0.6
	     */
        public const string ALIGN_JUSTIFIED_ALL = "JustifyAll";
	    /** the possible value of an alignment attribute */
	    public const string ALIGN_TOP = "top";
	    /** the possible value of an alignment attribute */
	    public const string ALIGN_MIDDLE = "middle";
	    /** the possible value of an alignment attribute */
	    public const string ALIGN_BOTTOM = "bottom";
	    /** the possible value of an alignment attribute */
	    public const string ALIGN_BASELINE = "baseline";
    	
	    // CSS
    	
	    /** This is used for inline css style information */
	    public const string STYLE = "style";
	    /**
	     * Attribute for specifying externally defined CSS class.
	     * @since 5.0.6
	     */
	    public const string CLASS = "class";
	    /** the CSS tag for text color */
	    public const string COLOR = "color";
	    /**
	     * The CSS tag for the font size.
	     * @since 5.0.6
	     */
	    public const string FONTFAMILY = "font-family";
	    /**
	     * The CSS tag for the font size.
	     * @since 5.0.6
	     */
	    public const string FONTSIZE = "font-size";
	    /**
	     * The CSS tag for the font size.
	     * @since 5.0.6
	     */
	    public const string FONTSTYLE = "font-style";
	    /**
	     * The CSS tag for the font size.
	     * @since 5.0.6
	     */
	    public const string FONTWEIGHT = "font-weight";
	    /**
	     * The CSS tag for the font size.
	     * @since 5.0.6
	     */
	    public const string LINEHEIGHT = "line-height";
	    /**
	     * The CSS tag for the font size.
	     * @since 5.0.6
	     */
	    public const string PADDINGLEFT = "padding-left";
	    /**
	     * The CSS tag for the font size.
	     * @since 5.0.6
	     */
	    public const string TEXTALIGN = "text-align";
	    /**
	     * The CSS tag for the font size.
	     * @since 5.0.6
	     */
	    public const string TEXTDECORATION = "text-decoration";
	    /** the CSS tag for text decorations */
	    public const string VERTICALALIGN = "vertical-align";
	    /**
	     * a CSS value for text decoration
	     * @since 5.0.6
	     */
	    public const string BOLD = "bold";
	    /**
	     * a CSS value for text decoration
	     * @since 5.0.6
	     */
	    public const string ITALIC = "italic";
	    /**
	     * a CSS value for text decoration
	     * @since 5.0.6
	     */
	    public const string LINETHROUGH = "line-through";
	    /**
	     * a CSS value for text decoration
	     * @since 5.0.6
	     */
	    public const string NORMAL = "normal";
	    /**
	     * a CSS value for text decoration
	     * @since 5.0.6
	     */
	    public const string OBLIQUE = "oblique";
	    /**
	     * a CSS value for text decoration
	     * @since 5.0.6
	     */
	    public const string UNDERLINE = "underline";

	    /**
	     * A possible attribute.
	     * @since 5.0.6
	     */
	    public const string AFTER = "after";
	    /**
	     * A possible attribute.
	     * @since 5.0.6
	     */
	    public const string BEFORE = "before";
    }
}
