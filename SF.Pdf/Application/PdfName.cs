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

using System.Reflection;
using System.Text;

namespace SF.Pdf.Application {
    /**
     * <CODE>PdfName</CODE> is an object that can be used as a name in a PDF-file.
     * <P>
     * A name, like a string, is a sequence of characters. It must begin with a slash
     * followed by a sequence of ASCII characters in the range 32 through 136 except
     * %, (, ), [, ], &lt;, &gt;, {, }, / and #. Any character except 0x00 may be included
     * in a name by writing its twocharacter hex code, preceded by #. The maximum number
     * of characters in a name is 127.<BR>
     * This object is described in the 'Portable Document Format Reference Manual version 1.3'
     * section 4.5 (page 39-40).
     * <P>
     *
     * @see        PdfObject
     * @see        PdfDictionary
     * @see        BadPdfFormatException
     */

    public class PdfName : PdfObject, IComparable<PdfName> {
    
        // CLASS CONSTANTS (a variety of standard names used in PDF))
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName _3D = new("3D");
        /** A name */
        public static readonly PdfName A = new("A");
        /** A name */
        public static readonly PdfName A85 = new("A85");
        /** A name */
        public static readonly PdfName AA = new("AA");
        /**
         * A name
         * @since 2.1.5 renamed from ABSOLUTECALORIMETRIC
         */
        public static readonly PdfName ABSOLUTECOLORIMETRIC = new("AbsoluteColorimetric");
        /** A name */
        public static readonly PdfName AC = new("AC");
        /** A name */
        public static readonly PdfName ACROFORM = new("AcroForm");
        /** A name */
        public static readonly PdfName ACTION = new("Action");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName ACTIVATION = new("Activation");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName ADBE = new("ADBE");
        /**
         * a name used in PDF structure
         * @since 2.1.6
         */
        public static readonly PdfName ACTUALTEXT = new("ActualText");
        /** A name */
        public static readonly PdfName ADBE_PKCS7_DETACHED = new("adbe.pkcs7.detached");
        /** A name */
        public static readonly PdfName ADBE_PKCS7_S4 =new("adbe.pkcs7.s4");
        /** A name */
        public static readonly PdfName ADBE_PKCS7_S5 =new("adbe.pkcs7.s5");
        /** A name */
        public static readonly PdfName ADBE_PKCS7_SHA1 = new("adbe.pkcs7.sha1");
        /** A name */
        public static readonly PdfName ADBE_X509_RSA_SHA1 = new("adbe.x509.rsa_sha1");
        /** A name */
        public static readonly PdfName ADOBE_PPKLITE = new("Adobe.PPKLite");
        /** A name */
        public static readonly PdfName ADOBE_PPKMS = new("Adobe.PPKMS");
        /** A name */
        public static readonly PdfName AESV2 = new("AESV2");
        /** A name */
        public static readonly PdfName AESV3 = new("AESV3");
        /**
         * A name
         * @since 5.4.5
         */
        public static readonly PdfName AFRELATIONSHIP = new("AFRelationship");
        /**
         * A name
         * @since 5.0.3
         */
        public static readonly PdfName AHX = new("AHx");
        /** A name */
        public static readonly PdfName AIS = new("AIS");
        /** A name */
        public static readonly PdfName ALL = new("All");
        /** A name */
        public static readonly PdfName ALLPAGES = new("AllPages");
        /**
         * Use ALT to specify alternate texts in Tagged PDF.
         * For alternate ICC profiles, use {@link #ALTERNATE}
         */
        public static readonly PdfName ALT = new("Alt");
        /**
         * Use ALTERNATE only in ICC profiles. It specifies an alternative color
         * space, in case the primary one is not supported, for legacy purposes.
         * For various types of alternate texts in Tagged PDF, use {@link #ALT}
         */
        public static readonly PdfName ALTERNATE = new("Alternate");

         /**
         * A name
         * @since 5.5.8
         */
        public static readonly PdfName AF = new("AF");

        /**
         * A name
         * @since 5.4.5
         */
        public static readonly PdfName ALTERNATEPRESENTATION = new("AlternatePresentations");
        /**
         * A name
         * @since 5.4.3
         */
        public static readonly PdfName ALTERNATES = new("Alternates");


        public static readonly PdfName AND = new("And");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName ANIMATION = new("Animation");
        /** A name */
        public static readonly PdfName ANNOT = new("Annot");
        /** A name */
        public static readonly PdfName ANNOTS = new("Annots");
        /** A name */
        public static readonly PdfName ANTIALIAS = new("AntiAlias");
        /** A name */
        public static readonly PdfName AP = new("AP");
        /** A name */
        public static readonly PdfName APP = new("App");
        /** A name */
        public static readonly PdfName APPDEFAULT = new("AppDefault");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName ART = new("Art");
        /** A name */
        public static readonly PdfName ARTBOX = new("ArtBox");
        /**
         * A name
         * @since 5.4.2
         */
        public static readonly PdfName ARTIFACT = new("Artifact");

        /** A name */
        public static readonly PdfName ASCENT = new("Ascent");
        /** A name */
        public static readonly PdfName AS = new("AS");
        /** A name */
        public static readonly PdfName ASCII85DECODE = new("ASCII85Decode");
        /** A name */
        public static readonly PdfName ASCIIHEXDECODE = new("ASCIIHexDecode");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName ASSET = new("Asset");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName ASSETS = new("Assets");
        /**
         * A name
         * @since 5.4.2
         */
        public static PdfName ATTACHED = new("Attached");

        /** A name */
        public static readonly PdfName AUTHEVENT = new("AuthEvent");
        /** A name */
        public static readonly PdfName AUTHOR = new("Author");
        /** A name */
        public static readonly PdfName B = new("B");
        /**
         * A name
         * @since	2.1.6
         */
        public static readonly PdfName BACKGROUND = new("Background");
        /**
         * A name
         * @since	5.3.5
         */
        public static readonly PdfName BACKGROUNDCOLOR = new("BackgroundColor");
        /** A name */
        public static readonly PdfName BASEENCODING = new("BaseEncoding");
        /** A name */
        public static readonly PdfName BASEFONT = new("BaseFont");
        /**
         * A name
         * @since	2.1.6
         */
        public static readonly PdfName BASEVERSION = new("BaseVersion");
        /** A name */
        public static readonly PdfName BBOX = new("BBox");
        /** A name */
        public static readonly PdfName BC = new("BC");
        /** A name */
        public static readonly PdfName BG = new("BG");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName BIBENTRY = new("BibEntry");
        /** A name */
        public static readonly PdfName BIGFIVE = new("BigFive");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName BINDING = new("Binding");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName BINDINGMATERIALNAME = new("BindingMaterialName");
        /** A name */
        public static readonly PdfName BITSPERCOMPONENT = new("BitsPerComponent");
        /** A name */
        public static readonly PdfName BITSPERSAMPLE = new("BitsPerSample");
        /** A name */
        public static readonly PdfName BL = new("Bl");
        /** A name */
        public static readonly PdfName BLACKIS1 = new("BlackIs1");
        /** A name */
        public static readonly PdfName BLACKPOINT = new("BlackPoint");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName BLOCKQUOTE = new("BlockQuote");
        /** A name */
        public static readonly PdfName BLEEDBOX = new("BleedBox");
        /** A name */
        public static readonly PdfName BLINDS = new("Blinds");
        /** A name */
        public static readonly PdfName BM = new("BM");
        /** A name */
        public static readonly PdfName BORDER = new("Border");
        /**
         * A name
         * @since 5.4.0
         */
        public static readonly PdfName BOTH = new("Both");
        /** A name */
        public static readonly PdfName BOUNDS = new("Bounds");
        /** A name */
        public static readonly PdfName BOX = new("Box");
        /** A name */
        public static readonly PdfName BS = new("BS");
        /** A name */
        public static readonly PdfName BTN = new("Btn");
        /** A name */
        public static readonly PdfName BYTERANGE = new("ByteRange");
        /** A name */
        public static readonly PdfName C = new("C");
        /** A name */
        public static readonly PdfName C0 = new("C0");
        /** A name */
        public static readonly PdfName C1 = new("C1");
        /** A name */
        public static readonly PdfName CA = new("CA");
        /** A name */
        public static readonly PdfName ca = new("ca");
        /** A name */
        public static readonly PdfName CALGRAY = new("CalGray");
        /** A name */
        public static readonly PdfName CALRGB = new("CalRGB");
        /** A name */
        public static readonly PdfName CAPHEIGHT = new("CapHeight");
        /**
         * A name
         * @since 5.4.5
         */
        public static readonly PdfName CARET = new("Caret");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName CAPTION = new("Caption");
        /** A name */
        public static readonly PdfName CATALOG = new("Catalog");
        /** A name */
        public static readonly PdfName CATEGORY = new("Category");
        /**
         * A name
         * @since 5.4.4
         */
        public static readonly PdfName CB = new("cb");

        /** A name */
        public static readonly PdfName CCITTFAXDECODE = new("CCITTFaxDecode");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName CENTER = new("Center");
        /** A name */
        public static readonly PdfName CENTERWINDOW = new("CenterWindow");
        /** A name */
        public static readonly PdfName CERT = new("Cert");

        public static readonly PdfName CERTS = new("Certs");
        /** A name */
        public static readonly PdfName CF = new("CF");
        /** A name */
        public static readonly PdfName CFM = new("CFM");
        /** A name */
        public static readonly PdfName CH = new("Ch");
        /** A name */
        public static readonly PdfName CHARPROCS = new("CharProcs");
        /** A name */
        public static readonly PdfName CHECKSUM = new("CheckSum");
        /** A name */
        public static readonly PdfName CI = new("CI");
        /** A name */
        public static readonly PdfName CIDFONTTYPE0 = new("CIDFontType0");
        /** A name */
        public static readonly PdfName CIDFONTTYPE2 = new("CIDFontType2");
        /**
         * A name
         * @since 2.0.7
         */
        public static readonly PdfName CIDSET = new("CIDSet");
        /** A name */
        public static readonly PdfName CIDSYSTEMINFO = new("CIDSystemInfo");
        /** A name */
        public static readonly PdfName CIDTOGIDMAP = new("CIDToGIDMap");
        /** A name */
        public static readonly PdfName CIRCLE = new("Circle");
        /**
         * A name
         * @since 5.3.2
         */
        public static readonly PdfName CLASSMAP = new("ClassMap");
        /**
         * A name
         * @since 5.1.0
         */
        public static readonly PdfName CLOUD = new("Cloud");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName CMD = new("CMD");
        /** A name */
        public static readonly PdfName CO = new("CO");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName CODE = new("Code");
        /**
         * A name
         * @since 5.3.4
         */
        public static readonly PdfName COLOR = new("Color");
        public static readonly PdfName COLORANTS = new("Colorants");
        /** A name */
        public static readonly PdfName COLORS = new("Colors");
        /** A name */
        public static readonly PdfName COLORSPACE = new("ColorSpace");
        /** 
         * A name
         * @since 5.4.4 
         */
        public static readonly PdfName COLORTRANSFORM = new("ColorTransform");

        /** A name */
        public static readonly PdfName COLLECTION = new("Collection");
        /** A name */
        public static readonly PdfName COLLECTIONFIELD = new("CollectionField");
        /** A name */
        public static readonly PdfName COLLECTIONITEM = new("CollectionItem");
        /** A name */
        public static readonly PdfName COLLECTIONSCHEMA = new("CollectionSchema");
        /** A name */
        public static readonly PdfName COLLECTIONSORT = new("CollectionSort");
        /** A name */
        public static readonly PdfName COLLECTIONSUBITEM = new("CollectionSubitem");
        /**
         * A name
         * @since 5.4.0
         */
        public static readonly PdfName COLSPAN = new("ColSpan");
        /**
         * A name
         * @since 5.4.0
        */
        public static readonly PdfName COLUMN = new("Column");
        /** A name */
        public static readonly PdfName COLUMNS = new("Columns");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName CONDITION = new("Condition");
        /**
         * A name
         * @since 5.4.2
         */
        public static readonly PdfName CONFIGS = new("Configs");

        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName CONFIGURATION = new("Configuration");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName CONFIGURATIONS = new("Configurations");
        /** A name */
        public static readonly PdfName CONTACTINFO = new("ContactInfo");
        /** A name */
        public static readonly PdfName CONTENT = new("Content");
        /** A name */
        public static readonly PdfName CONTENTS = new("Contents");
        /** A name */
        public static readonly PdfName COORDS = new("Coords");
        /** A name */
        public static readonly PdfName COUNT = new("Count");
        /** A name of a base 14 type 1 font */
        public static readonly PdfName COURIER = new("Courier");
        /** A name of a base 14 type 1 font */
        public static readonly PdfName COURIER_BOLD = new("Courier-Bold");
        /** A name of a base 14 type 1 font */
        public static readonly PdfName COURIER_OBLIQUE = new("Courier-Oblique");
        /** A name of a base 14 type 1 font */
        public static readonly PdfName COURIER_BOLDOBLIQUE = new("Courier-BoldOblique");
        /** A name */
        public static readonly PdfName CREATIONDATE = new("CreationDate");
        /** A name */
        public static readonly PdfName CREATOR = new("Creator");
        /** A name */
        public static readonly PdfName CREATORINFO = new("CreatorInfo");
        public static readonly PdfName CRL = new("CRL");
        public static readonly PdfName CRLS = new("CRLs");
        /** A name */
        public static readonly PdfName CROPBOX = new("CropBox");
        /** A name */
        public static readonly PdfName CRYPT = new("Crypt");
        /** A name */
        public static readonly PdfName CS = new("CS");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName CUEPOINT = new("CuePoint");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName CUEPOINTS = new("CuePoints");
        /**
         * A name of an attribute.
         * @since 5.1.0
         */
        public static readonly PdfName CYX = new("CYX");
        /** A name */
        public static readonly PdfName D = new("D");
        /** A name */
        public static readonly PdfName DA = new("DA");
        /** A name */
        public static readonly PdfName DATA = new("Data");
        /** A name */
        public static readonly PdfName DC = new("DC");
        /**
         * A name of an attribute.
         * @since 5.1.0
         */
        public static readonly PdfName DCS = new("DCS");
        /** A name */
        public static readonly PdfName DCTDECODE = new("DCTDecode");
        /**
         * A name
         * @since 5.4.0
         */
        public static readonly PdfName DECIMAL = new("Decimal");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName DEACTIVATION = new("Deactivation");
        /** A name */
        public static readonly PdfName DECODE = new("Decode");
        /** A name */
        public static readonly PdfName DECODEPARMS = new("DecodeParms");
        /**
         * A name
         * @since	2.1.6
         */
        public static readonly PdfName DEFAULT = new("Default");
        /**
         * A name
         * @since	2.1.5 renamed from DEFAULTCRYPTFILER
         */
        public static readonly PdfName DEFAULTCRYPTFILTER = new("DefaultCryptFilter");
        /** A name */
        public static readonly PdfName DEFAULTCMYK = new("DefaultCMYK");
        /** A name */
        public static readonly PdfName DEFAULTGRAY = new("DefaultGray");
        /** A name */
        public static readonly PdfName DEFAULTRGB = new("DefaultRGB");
        /** A name */
        public static readonly PdfName DESC = new("Desc");
        /** A name */
        public static readonly PdfName DESCENDANTFONTS = new("DescendantFonts");
        /** A name */
        public static readonly PdfName DESCENT = new("Descent");
        /** A name */
        public static readonly PdfName DEST = new("Dest");
        /** A name */
        public static readonly PdfName DESTOUTPUTPROFILE = new("DestOutputProfile");
        /** A name */
        public static readonly PdfName DESTS = new("Dests");
        /** A name */
        public static readonly PdfName DEVICEGRAY = new("DeviceGray");
        /** A name */
        public static readonly PdfName DEVICERGB = new("DeviceRGB");
        /** A name */
        public static readonly PdfName DEVICECMYK = new("DeviceCMYK");
        /**
         * A name
         * @since 5.2.1
         */
        public static readonly PdfName DEVICEN = new("DeviceN");
        /** A name */
        public static readonly PdfName DI = new("Di");
        /** A name */
        public static readonly PdfName DIFFERENCES = new("Differences");
        /** A name */
        public static readonly PdfName DISSOLVE = new("Dissolve");
        /** A name */
        public static readonly PdfName DIRECTION = new("Direction");
        /** A name */
        public static readonly PdfName DISPLAYDOCTITLE = new("DisplayDocTitle");
        /** A name */
        public static readonly PdfName DIV = new("Div");
        /** A name */
        public static readonly PdfName DL = new("DL");
        /** A name */
        public static readonly PdfName DM = new("Dm");
        /**
         * A name
         * @since 5.2.1
         */
        public static readonly PdfName DOS = new("DOS");
        /** A name */
        public static readonly PdfName DOCMDP = new("DocMDP");
        /** A name */
        public static readonly PdfName DOCOPEN = new("DocOpen");
        /**
         * A name
         * @since 5.1.3
         */
        public static readonly PdfName DOCTIMESTAMP = new( "DocTimeStamp" );
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName DOCUMENT = new( "Document" );
        /** A name */
        public static readonly PdfName DOMAIN = new("Domain");
        /** A name */
        public static readonly PdfName DP = new("DP");
        /** A name */
        public static readonly PdfName DR = new("DR");
        /** A name */
        public static readonly PdfName DS = new("DS");
        public static readonly PdfName DSS = new("DSS");
        /** A name */
        public static readonly PdfName DUR = new("Dur");
        /** A name */
        public static readonly PdfName DUPLEX = new("Duplex");
        /** A name */
        public static readonly PdfName DUPLEXFLIPSHORTEDGE = new("DuplexFlipShortEdge");
        /** A name */
        public static readonly PdfName DUPLEXFLIPLONGEDGE = new("DuplexFlipLongEdge");
        /** A name */
        public static readonly PdfName DV = new("DV");
        /** A name */
        public static readonly PdfName DW = new("DW");
        /** A name */
        public static readonly PdfName E = new("E");
        /** A name */
        public static readonly PdfName EARLYCHANGE = new("EarlyChange");
        /** A name */
        public static readonly PdfName EF = new("EF");
        /**
         * A name
         * @since	2.1.3
         */
        public static readonly PdfName EFF = new("EFF");
        /**
         * A name
         * @since	2.1.3
         */
        public static readonly PdfName EFOPEN = new("EFOpen");
        /**
         * A name
         * @since	2.1.6
         */
        public static readonly PdfName EMBEDDED = new("Embedded");
        /** A name */
        public static readonly PdfName EMBEDDEDFILE = new("EmbeddedFile");
        /** A name */
        public static readonly PdfName EMBEDDEDFILES = new("EmbeddedFiles");
        /** A name */
        public static readonly PdfName ENCODE = new("Encode");
        /** A name */
        public static readonly PdfName ENCODEDBYTEALIGN = new("EncodedByteAlign");
        /** A name */
        public static readonly PdfName ENCODING = new("Encoding");
        /** A name */
        public static readonly PdfName ENCRYPT = new("Encrypt");
        /** A name */
        public static readonly PdfName ENCRYPTMETADATA = new("EncryptMetadata");
        /**
         * A name
         * @since 5.3.4
         */
        public static readonly PdfName END = new("End");
        /**
         * A name
         * @since 5.3.4
         */
        public static readonly PdfName ENDINDENT = new("EndIndent");
        /** A name */
        public static readonly PdfName ENDOFBLOCK = new("EndOfBlock");
        /** A name */
        public static readonly PdfName ENDOFLINE = new("EndOfLine");
        /**
         * A name of an attribute.
         * @since 5.1.0
         */
        public static readonly PdfName EPSG = new("EPSG");
        /**
         * A name
         * @since 5.4.3
         */
        public static readonly PdfName ESIC = new("ESIC");
        public static readonly PdfName ETSI_CADES_DETACHED = new("ETSI.CAdES.detached");
        public static readonly PdfName ETSI_RFC3161 = new("ETSI.RFC3161");
        /** A name */
        public static readonly PdfName EXCLUDE = new("Exclude");
        /** A name */
        public static readonly PdfName EXTEND = new("Extend");
        /**
         * A name
         * @since	2.1.6
         */
        public static readonly PdfName EXTENSIONS = new("Extensions");
        /**
         * A name
         * @since	2.1.6
         */
        public static readonly PdfName EXTENSIONLEVEL = new("ExtensionLevel");
        /** A name */
        public static readonly PdfName EXTGSTATE = new("ExtGState");
        /** A name */
        public static readonly PdfName EXPORT = new("Export");
        /** A name */
        public static readonly PdfName EXPORTSTATE = new("ExportState");
        /** A name */
        public static readonly PdfName EVENT = new("Event");
        /** A name */
        public static readonly PdfName F = new("F");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName FAR = new("Far");
        /** A name */
        public static readonly PdfName FB = new("FB");
        /**
         * A name
         * @since 5.1.0
         */
        public static readonly PdfName FD = new("FD");
        /** A name */
        public static readonly PdfName FDECODEPARMS = new("FDecodeParms");
        /** A name */
        public static readonly PdfName FDF = new("FDF");
        /** A name */
        public static readonly PdfName FF = new("Ff");
        /** A name */
        public static readonly PdfName FFILTER = new("FFilter");
        public static readonly PdfName FG = new("FG");
        /** A name */
        public static readonly PdfName FIELDMDP = new("FieldMDP");
        /** A name */
        public static readonly PdfName FIELDS = new("Fields");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName FIGURE = new( "Figure" );
        /** A name */
        public static readonly PdfName FILEATTACHMENT = new("FileAttachment");
        /** A name */
        public static readonly PdfName FILESPEC = new("Filespec");
        /** A name */
        public static readonly PdfName FILTER = new("Filter");
        /** A name */
        public static readonly PdfName FIRST = new("First");
        /** A name */
        public static readonly PdfName FIRSTCHAR = new("FirstChar");
        /** A name */
        public static readonly PdfName FIRSTPAGE = new("FirstPage");
        /** A name */
        public static readonly PdfName FIT = new("Fit");
        /** A name */
        public static readonly PdfName FITH = new("FitH");
        /** A name */
        public static readonly PdfName FITV = new("FitV");
        /** A name */
        public static readonly PdfName FITR = new("FitR");
        /** A name */
        public static readonly PdfName FITB = new("FitB");
        /** A name */
        public static readonly PdfName FITBH = new("FitBH");
        /** A name */
        public static readonly PdfName FITBV = new("FitBV");
        /** A name */
        public static readonly PdfName FITWINDOW = new("FitWindow");
        /** A name */
        public static readonly PdfName FL = new("Fl");
        /** A name */
        public static readonly PdfName FLAGS = new("Flags");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName FLASH = new("Flash");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName FLASHVARS = new("FlashVars");
        /** A name */
        public static readonly PdfName FLATEDECODE = new("FlateDecode");
        /** A name */
        public static readonly PdfName FO = new("Fo");
        /** A name */
        public static readonly PdfName FONT = new("Font");
        /** A name */
        public static readonly PdfName FONTBBOX = new("FontBBox");
        /** A name */
        public static readonly PdfName FONTDESCRIPTOR = new("FontDescriptor");
        /** A name */
        public static readonly PdfName FONTFAMILY = new("FontFamily");
        /** A name */
        public static readonly PdfName FONTFILE = new("FontFile");
        /** A name */
        public static readonly PdfName FONTFILE2 = new("FontFile2");
        /** A name */
        public static readonly PdfName FONTFILE3 = new("FontFile3");
        /** A name */
        public static readonly PdfName FONTMATRIX = new("FontMatrix");
        /** A name */
        public static readonly PdfName FONTNAME = new("FontName");
        /** A name */
        public static readonly PdfName FONTWEIGHT = new("FontWeight");
        /**
         * A name
         * @since	2.1.6
         */
        public static readonly PdfName FOREGROUND = new("Foreground");
        /** A name */
        public static readonly PdfName FORM = new("Form");
        /** A name */
        public static readonly PdfName FORMTYPE = new("FormType");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName FORMULA = new( "Formula" );
        /** A name */
        public static readonly PdfName FREETEXT = new("FreeText");
        /** A name */
        public static readonly PdfName FRM = new("FRM");
        /** A name */
        public static readonly PdfName FS = new("FS");
        /** A name */
        public static readonly PdfName FT = new("FT");
        /** A name */
        public static readonly PdfName FULLSCREEN = new("FullScreen");
        /** A name */
        public static readonly PdfName FUNCTION = new("Function");
        /** A name */
        public static readonly PdfName FUNCTIONS = new("Functions");
        /** A name */
        public static readonly PdfName FUNCTIONTYPE = new("FunctionType");
        /** A name of an attribute. */
        public static readonly PdfName GAMMA = new("Gamma");
        /** A name of an attribute. */
        public static readonly PdfName GBK = new("GBK");
        /**
         * A name of an attribute.
         * @since 5.1.0
         */
        public static readonly PdfName GCS = new("GCS");
        /**
         * A name of an attribute.
         * @since 5.1.0
         */
        public static readonly PdfName GEO = new("GEO");
        /**
         * A name of an attribute.
         * @since 5.1.0
         */
        public static readonly PdfName GEOGCS = new("GEOGCS");
        /** A name of an attribute. */
        public static readonly PdfName GLITTER = new("Glitter");
        /** A name of an attribute. */
        public static readonly PdfName GOTO = new("GoTo");
        /**
         * A name
         * @since 5.4.5
         */
        public static readonly PdfName GOTO3DVIEW = new("GoTo3DView");
        /** A name of an attribute. */
        public static readonly PdfName GOTOE = new("GoToE");
        /** A name of an attribute. */
        public static readonly PdfName GOTOR = new("GoToR");
        /**
         * A name of an attribute.
         * @since 5.1.0
         */
        public static readonly PdfName GPTS = new("GPTS");
        /** A name of an attribute. */
        public static readonly PdfName GROUP = new("Group");
        /** A name of an attribute. */
        public static readonly PdfName GTS_PDFA1 = new("GTS_PDFA1");
        /** A name of an attribute. */
        public static readonly PdfName GTS_PDFX = new("GTS_PDFX");
        /** A name of an attribute. */
        public static readonly PdfName GTS_PDFXVERSION = new("GTS_PDFXVersion");
        /** A name of an attribute. */
        public static readonly PdfName H = new("H");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName H1 = new( "H1" );
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName H2 = new("H2");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName H3 = new("H3");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName H4 = new("H4");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName H5 = new("H5");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName H6 = new("H6");
        /**
         * A name
         * @since 5.4.5
         */
        public static readonly PdfName HALFTONENAME = new("HalftoneName");
        /**
         * A name
         * @since 5.4.5
         */
        public static readonly PdfName HALFTONETYPE = new("HalftoneType");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName HALIGN = new("HAlign");
        /**
         * A name
         * @since 5.4.0
         */
        public static readonly PdfName HEADERS = new("Headers");
        /** A name of an attribute. */
        public static readonly PdfName HEIGHT = new("Height");
        /** A name */
        public static readonly PdfName HELV = new("Helv");
        /** A name of a base 14 type 1 font */
        public static readonly PdfName HELVETICA = new("Helvetica");
        /** A name of a base 14 type 1 font */
        public static readonly PdfName HELVETICA_BOLD = new("Helvetica-Bold");
        /** A name of a base 14 type 1 font */
        public static readonly PdfName HELVETICA_OBLIQUE = new("Helvetica-Oblique");
        /** A name of a base 14 type 1 font */
        public static readonly PdfName HELVETICA_BOLDOBLIQUE = new("Helvetica-BoldOblique");
        public static readonly PdfName HF = new("HF");
        /** A name */
        public static readonly PdfName HID = new("Hid");
        /** A name */
        public static readonly PdfName HIDE = new("Hide");
        /** A name */
        public static readonly PdfName HIDEMENUBAR = new("HideMenubar");
        /** A name */
        public static readonly PdfName HIDETOOLBAR = new("HideToolbar");
        /** A name */
        public static readonly PdfName HIDEWINDOWUI = new("HideWindowUI");
        /** A name */
        public static readonly PdfName HIGHLIGHT = new("Highlight");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName HOFFSET = new("HOffset");
        /**
         * A name
         * @since 5.4.5
         */
        public static readonly PdfName HT = new("HT");
        /**
         * A name
         * @since 5.4.5
         */
        public static readonly PdfName HTP = new("HTP");
        /** A name */
        public static readonly PdfName I = new("I");
        /**
         * A name
         * @since 5.4.3
         */
        public static readonly PdfName IC = new("IC");
        /** A name */
        public static readonly PdfName ICCBASED = new("ICCBased");
        /** A name */
        public static readonly PdfName ID = new("ID");
        /** A name */
        public static readonly PdfName IDENTITY = new("Identity");
        /** A name */
        public static readonly PdfName IDTREE = new("IDTree");
        /** A name */
        public static readonly PdfName IF = new("IF");
        /**
         * A name
         * @since 5.5.3
         */
        public static readonly PdfName IM = new("IM");
        /** A name */
        public static readonly PdfName IMAGE = new("Image");
        /** A name */
        public static readonly PdfName IMAGEB = new("ImageB");
        /** A name */
        public static readonly PdfName IMAGEC = new("ImageC");
        /** A name */
        public static readonly PdfName IMAGEI = new("ImageI");
        /** A name */
        public static readonly PdfName IMAGEMASK = new("ImageMask");
        /** A name */
        public static readonly PdfName INCLUDE = new("Include");
        public static readonly PdfName IND = new("Ind");
        /** A name */
        public static readonly PdfName INDEX = new("Index");
        /** A name */
        public static readonly PdfName INDEXED = new("Indexed");
        /** A name */
        public static readonly PdfName INFO = new("Info");
        /** A name */
        public static readonly PdfName INK = new("Ink");
        /** A name */
        public static readonly PdfName INKLIST = new("InkList");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName INSTANCES = new("Instances");
        /** A name */
        public static readonly PdfName IMPORTDATA = new("ImportData");
        /** A name */
        public static readonly PdfName INTENT = new("Intent");
        /** A name */
        public static readonly PdfName INTERPOLATE = new("Interpolate");
        /** A name */
        public static readonly PdfName ISMAP = new("IsMap");
        /** A name */
        public static readonly PdfName IRT = new("IRT");
        /** A name */
        public static readonly PdfName ITALICANGLE = new("ItalicAngle");
        /**
         * A name
         * @since	2.1.6
         */
        public static readonly PdfName ITXT = new("ITXT");
        /** A name */
        public static readonly PdfName IX = new("IX");
        /** A name */
        public static readonly PdfName JAVASCRIPT = new("JavaScript");
        /**
         * A name
         * @since	2.1.5
         */
        public static readonly PdfName JBIG2DECODE = new("JBIG2Decode");
        /**
         * A name
         * @since	2.1.5
         */
        public static readonly PdfName JBIG2GLOBALS = new("JBIG2Globals");
        /** A name */
        public static readonly PdfName JPXDECODE = new("JPXDecode");
        /** A name */
        public static readonly PdfName JS = new("JS");
        /**
         * A name
         * @since 5.3.4
         */
        public static readonly PdfName JUSTIFY = new("Justify");
        /** A name */
        public static readonly PdfName K = new("K");
        /** A name */
        public static readonly PdfName KEYWORDS = new("Keywords");
        /** A name */
        public static readonly PdfName KIDS = new("Kids");
        /** A name */
        public static readonly PdfName L = new("L");
        /** A name */
        public static readonly PdfName L2R = new("L2R");
        /**
         * A name
         * @since 5.1.4
         */
        public static readonly PdfName LAB = new("Lab");
        /**
         * An entry specifying the natural language, and optionally locale. Use this
         * to specify the Language attribute on a Tagged Pdf element.
         * For the content usage dictionary, use {@link #LANGUAGE}
         */
        public static readonly PdfName LANG = new("Lang");
        /**
         * A dictionary type, strictly for use in the content usage dictionary. For
         * dictionary entries in Tagged Pdf, use {@link #LANG}
         */
        public static readonly PdfName LANGUAGE = new("Language");
        /** A name */
        public static readonly PdfName LAST = new("Last");
        /** A name */
        public static readonly PdfName LASTCHAR = new("LastChar");
        /** A name */
        public static readonly PdfName LASTPAGE = new("LastPage");
        /** A name */
        public static readonly PdfName LAUNCH = new("Launch");
        /**
         * A name
         * @since 5.5.0
         */
        public static readonly PdfName LAYOUT = new("Layout");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName LBL = new("Lbl");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName LBODY = new("LBody");
        /** A name */
        public static readonly PdfName LENGTH = new("Length");
        /** A name */
        public static readonly PdfName LENGTH1 = new("Length1");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName LI = new("LI");
        /** A name */
        public static readonly PdfName LIMITS = new("Limits");
        /** A name */
        public static readonly PdfName LINE = new("Line");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName LINEAR = new("Linear");
        /**
         * A name
         * @since 5.3.5
         */
        public static readonly PdfName LINEHEIGHT = new("LineHeight");
        /** A name */
        public static readonly PdfName LINK = new("Link");
        /**
         * A name
         * @since 5.4.0
         */
        public static readonly PdfName LIST = new("List");

        /** A name */
        public static readonly PdfName LISTMODE = new("ListMode");
        /**
         * A name
         * @since 5.4.0
         */
        public static readonly PdfName LISTNUMBERING = new("ListNumbering");
        /** A name */
        public static readonly PdfName LOCATION = new("Location");
        /** A name */
        public static readonly PdfName LOCK = new("Lock");
        /**
         * A name
         * @since	2.1.2
         */
        public static readonly PdfName LOCKED = new("Locked");
        /**
         * A name
         * @since 5.4.0
         */        
        public static readonly PdfName LOWERALPHA = new("LowerAlpha");
        /**
         * A name
         * @since 5.4.0
         */
        public static readonly PdfName LOWERROMAN = new("LowerRoman");

        /**
         * A name of an attribute.
         * @since 5.1.0
         */
        public static readonly PdfName LPTS = new("LPTS");
        /** A name */
        public static readonly PdfName LZWDECODE = new("LZWDecode");
        /** A name */
        public static readonly PdfName M = new("M");
        /**
         * A name
         * @since 5.2.1
         */
        public static readonly PdfName MAC = new("Mac");
        /**
         * A name
         * @since	2.1.6
         */
        public static readonly PdfName MATERIAL = new("Material");
        /** A name */
        public static readonly PdfName MATRIX = new("Matrix");
        /** A name of an encoding */
        public static readonly PdfName MAC_EXPERT_ENCODING = new("MacExpertEncoding");
        /** A name of an encoding */
        public static readonly PdfName MAC_ROMAN_ENCODING = new("MacRomanEncoding");
        /** A name */
        public static readonly PdfName MARKED = new("Marked");
        /** A name */
        public static readonly PdfName MARKINFO = new("MarkInfo");
        /** A name */
        public static readonly PdfName MASK = new("Mask");
        /**
         * A name
         * @since	2.1.6 renamed from MAX
         */
        public static readonly PdfName MAX_LOWER_CASE = new("max");
        /**
         * A name
         * @since	2.1.6
         */
        public static readonly PdfName MAX_CAMEL_CASE = new("Max");
        /** A name */
        public static readonly PdfName MAXLEN = new("MaxLen");
        /** A name */
        public static readonly PdfName MEDIABOX = new("MediaBox");
        /** A name */
        public static readonly PdfName MCID = new("MCID");
        /** A name */
        public static readonly PdfName MCR = new("MCR");
        /**
         * A name
         * @since   5.1.0
         */
        public static readonly PdfName MEASURE = new("Measure");
        /** A name */
        public static readonly PdfName METADATA = new("Metadata");
        /**
         * A name
         * @since	2.1.6 renamed from MIN
         */
        public static readonly PdfName MIN_LOWER_CASE = new("min");
        /**
         * A name
         * @since	2.1.6
         */
        public static readonly PdfName MIN_CAMEL_CASE = new("Min");
        /** A name */
        public static readonly PdfName MK = new("MK");
        /** A name */
        public static readonly PdfName MMTYPE1 = new("MMType1");
        /** A name */
        public static readonly PdfName MODDATE = new("ModDate");
        /**
         * A name
         * @since	5.4.3
         */
        public static readonly PdfName MOVIE = new("Movie");
        /** A name */
        public static readonly PdfName N = new("N");
        /** A name */
        public static readonly PdfName N0 = new("n0");
        /** A name */
        public static readonly PdfName N1 = new("n1");
        /** A name */
        public static readonly PdfName N2 = new("n2");
        /** A name */
        public static readonly PdfName N3 = new("n3");
        /** A name */
        public static readonly PdfName N4 = new("n4");
        /** A name */
        public static new readonly PdfName NAME = new("Name");
        /** A name */
        public static readonly PdfName NAMED = new("Named");
        /** A name */
        public static readonly PdfName NAMES = new("Names");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName NAVIGATION = new("Navigation");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName NAVIGATIONPANE = new("NavigationPane");
        public static readonly PdfName NCHANNEL = new("NChannel");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName NEAR = new("Near");
        /** A name */
        public static readonly PdfName NEEDAPPEARANCES = new("NeedAppearances");
        /**
         * A name.
         * @since 5.4.5
         */
        public static readonly PdfName NEEDRENDERING = new("NeedsRendering");
        /** A name */
        public static readonly PdfName NEWWINDOW = new("NewWindow");
        /** A name */
        public static readonly PdfName NEXT = new("Next");
        /** A name */
        public static readonly PdfName NEXTPAGE = new("NextPage");
        /** A name */
        public static readonly PdfName NM = new("NM");
        /** A name */
        public static readonly PdfName NONE = new("None");
        /** A name */
        public static readonly PdfName NONFULLSCREENPAGEMODE = new("NonFullScreenPageMode");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName NONSTRUCT = new("NonStruct");
        public static readonly PdfName NOT = new("Not");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName NOTE = new("Note");
        /**
         * A name
         * @since 5.1.0
         */
        public static readonly PdfName NUMBERFORMAT = new("NumberFormat");
        /** A name */
        public static readonly PdfName NUMCOPIES = new("NumCopies");
        /** A name */
        public static readonly PdfName NUMS = new("Nums");
        /** A name */
        public static readonly PdfName O = new("O");
        /**
         * A name used with Document Structure
         * @since 2.1.5
         */
        public static readonly PdfName OBJ = new("Obj");
        /**
         * a name used with Document Structure
         * @since 2.1.5
         */
        public static readonly PdfName OBJR = new("OBJR");
        /** A name */
        public static readonly PdfName OBJSTM = new("ObjStm");
        /** A name */
        public static readonly PdfName OC = new("OC");
        /** A name */
        public static readonly PdfName OCG = new("OCG");
        /** A name */
        public static readonly PdfName OCGS = new("OCGs");
        /** A name */
        public static readonly PdfName OCMD = new("OCMD");
        /** A name */
        public static readonly PdfName OCPROPERTIES = new("OCProperties");
        public static readonly PdfName OCSP = new("OCSP");
        public static readonly PdfName OCSPS = new("OCSPs");
        /** A name */
        public static readonly PdfName OE = new("OE");
        /** A name */
        public static readonly PdfName Off_ = new("Off");
        /** A name */
        public static readonly PdfName OFF = new("OFF");
        /** A name */
        public static readonly PdfName ON = new("ON");
        /** A name */
        public static readonly PdfName ONECOLUMN = new("OneColumn");
        /** A name */
        public static readonly PdfName OPEN = new("Open");
        /** A name */
        public static readonly PdfName OPENACTION = new("OpenAction");
        /** A name */
        public static readonly PdfName OP = new("OP");
        /** A name */
        public static readonly PdfName op_ = new("op");
        /**
         * A name
         * @since 5.4.3
         */
        public static readonly PdfName OPI = new("OPI");
        /** A name */
        public static readonly PdfName OPM = new("OPM");
        /** A name */
        public static readonly PdfName OPT = new("Opt");
        public static readonly PdfName OR = new("Or");
        /** A name */
        public static readonly PdfName ORDER = new("Order");
        /** A name */
        public static readonly PdfName ORDERING = new("Ordering");
        public static readonly PdfName ORG = new("Org");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName OSCILLATING = new("Oscillating");
    
        /** A name */
        public static readonly PdfName OUTLINES = new("Outlines");
        /** A name */
        public static readonly PdfName OUTPUTCONDITION = new("OutputCondition");
        /** A name */
        public static readonly PdfName OUTPUTCONDITIONIDENTIFIER = new("OutputConditionIdentifier");
        /** A name */
        public static readonly PdfName OUTPUTINTENT = new("OutputIntent");
        /** A name */
        public static readonly PdfName OUTPUTINTENTS = new("OutputIntents");
        /**
         * A name
         * @since 5.5.4
         */
        public static readonly PdfName OVERLAYTEXT = new("OverlayText");
        /** A name */
        public static readonly PdfName P = new("P");
        /** A name */
        public static readonly PdfName PAGE = new("Page");
        public static readonly PdfName PAGEELEMENT = new("PageElement");
        /** A name */
        public static readonly PdfName PAGELABELS = new("PageLabels");
        /** A name */
        public static readonly PdfName PAGELAYOUT = new("PageLayout");
        /** A name */
        public static readonly PdfName PAGEMODE = new("PageMode");
        /** A name */
        public static readonly PdfName PAGES = new("Pages");
        /** A name */
        public static readonly PdfName PAINTTYPE = new("PaintType");
        /** A name */
        public static readonly PdfName PANOSE = new("Panose");
        /** A name */
        public static readonly PdfName PARAMS = new("Params");
        /** A name */
        public static readonly PdfName PARENT = new("Parent");
        /** A name */
        public static readonly PdfName PARENTTREE = new("ParentTree");
        /**
         * A name used in defining Document Structure.
         * @since 2.1.5
         */
        public static readonly PdfName PARENTTREENEXTKEY = new( "ParentTreeNextKey" );
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName PART = new( "Part" );
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName PASSCONTEXTCLICK = new("PassContextClick");
        /** A name */
        public static readonly PdfName PATTERN = new("Pattern");
        /** A name */
        public static readonly PdfName PATTERNTYPE = new("PatternType");
        /**
         * A name
         * @since 5.4.4
         */
        public static readonly PdfName PB = new("pb");

        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName PC = new("PC");
        /** A name */
        public static readonly PdfName PDF = new("PDF");
        /** A name */
        public static readonly PdfName PDFDOCENCODING = new("PDFDocEncoding");
        /**
         * A name
         * @since 5.1.0
         */
        public static readonly PdfName PDU = new("PDU");
        /** A name */
        public static readonly PdfName PERCEPTUAL = new("Perceptual");
        /** A name */
        public static readonly PdfName PERMS = new("Perms");
        /** A name */
        public static readonly PdfName PG = new("Pg");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName PI = new("PI");
        /** A name */
        public static readonly PdfName PICKTRAYBYPDFSIZE = new("PickTrayByPDFSize");
        /**
         * A name
         * @since 5.5.0
         */
        public static readonly PdfName PIECEINFO = new("PieceInfo");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName PLAYCOUNT = new("PlayCount");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName PO = new("PO");
        /**
         * A name
         * @since 5.0.2
         */
        public static readonly PdfName POLYGON = new("Polygon");
        /**
         * A name
         * @since 5.0.2
         */
        public static readonly PdfName POLYLINE = new("PolyLine");
        /** A name */
        public static readonly PdfName POPUP = new("Popup");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName POSITION = new("Position");
        /** A name */
        public static readonly PdfName PREDICTOR = new("Predictor");
        /** A name */
        public static readonly PdfName PREFERRED = new("Preferred");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName PRESENTATION = new("Presentation");
        /** A name */
        public static readonly PdfName PRESERVERB = new("PreserveRB");
        /**
         * A name.
         * @since 5.4.5
         */
        public static readonly PdfName PRESSTEPS = new("PresSteps");
        /** A name */
        public static readonly PdfName PREV = new("Prev");
        /** A name */
        public static readonly PdfName PREVPAGE = new("PrevPage");
        /** A name */
        public static readonly PdfName PRINT = new("Print");
        /** A name */
        public static readonly PdfName PRINTAREA = new("PrintArea");
        /** A name */
        public static readonly PdfName PRINTCLIP = new("PrintClip");
        /**
         * A name
         * @since 5.4.3
         */
        public static readonly PdfName PRINTERMARK = new("PrinterMark");
        /**
         * A name
         * @since 5.4.4
         */
        public static readonly PdfName PRINTFIELD = new("PrintField");
        /** A name */
        public static readonly PdfName PRINTPAGERANGE = new("PrintPageRange");
        /** A name */
        public static readonly PdfName PRINTSCALING = new("PrintScaling");
        /** A name */
        public static readonly PdfName PRINTSTATE = new("PrintState");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName PRIVATE = new("Private");
        /** A name */
        public static readonly PdfName PROCSET = new("ProcSet");
        /** A name */
        public static readonly PdfName PRODUCER = new("Producer");
        /**
         * A name of an attribute.
         * @since 5.1.0
         */
        public static readonly PdfName PROJCS = new("PROJCS");
        /** A name */
        public static readonly PdfName PROP_BUILD = new("Prop_Build");
        /** A name */
        public static readonly PdfName PROPERTIES = new("Properties");
        /** A name */
        public static readonly PdfName PS = new("PS");
        /**
         * A name
         * @since 5.1.0
         */
        public static readonly PdfName PTDATA = new("PtData");
        /** A name */
        public static readonly PdfName PUBSEC = new("Adobe.PubSec");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName PV = new("PV");
        /** A name */
        public static readonly PdfName Q = new("Q");
        /** A name */
        public static readonly PdfName QUADPOINTS = new("QuadPoints");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName QUOTE = new("Quote");
        /** A name */
        public static readonly PdfName R = new("R");
        /** A name */
        public static readonly PdfName R2L = new("R2L");
        /** A name */
        public static readonly PdfName RANGE = new("Range");
        /**
         * A name
         * @since 5.4.3
         */
        public static readonly PdfName RB = new("RB");
        /**
         * A name
         * @since 5.4.4
         */
        public static readonly PdfName rb = new("rb");
        /** A name */
        public static readonly PdfName RBGROUPS = new("RBGroups");
        /** A name */
        public static readonly PdfName RC = new("RC");
        /**
         * A name
         * @since 5.1.0
         */
        public static readonly PdfName RD = new("RD");
        /** A name */
        public static readonly PdfName REASON = new("Reason");
        /** A name */
        public static readonly PdfName RECIPIENTS = new("Recipients");
        /** A name */
        public static readonly PdfName RECT = new("Rect");
        /**
         * A name
         * @since 5.4.4
         */
        public static readonly PdfName REDACT = new("Redact");
        /** A name */
        public static readonly PdfName REFERENCE = new("Reference");
        /** A name */
        public static readonly PdfName REGISTRY = new("Registry");
        /** A name */
        public static readonly PdfName REGISTRYNAME = new("RegistryName");
        /**
         * A name
         * @since	2.1.5 renamed from RELATIVECALORIMETRIC
         */
        public static readonly PdfName RELATIVECOLORIMETRIC = new("RelativeColorimetric");
        /** A name */
        public static readonly PdfName RENDITION = new("Rendition");
        /**
         * A name
         * @since 5.5.4
         */
        public static readonly PdfName REPEAT = new("Repeat");

       
        public static readonly PdfName REVERSEDCHARS = new("ReversedChars");

        /** A name */
        public static readonly PdfName RESETFORM = new("ResetForm");
        /** A name */
        public static readonly PdfName RESOURCES = new("Resources");
        public static readonly PdfName REQUIREMENTS = new("Requirements");
        /** A name */
        public static readonly PdfName RI = new("RI");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName RICHMEDIA = new("RichMedia");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName RICHMEDIAACTIVATION = new("RichMediaActivation");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName RICHMEDIAANIMATION = new("RichMediaAnimation");
        /**
         * A name
         * @since	2.1.6
         */
        public static readonly PdfName RICHMEDIACOMMAND = new("RichMediaCommand");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName RICHMEDIACONFIGURATION = new("RichMediaConfiguration");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName RICHMEDIACONTENT = new("RichMediaContent");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName RICHMEDIADEACTIVATION = new("RichMediaDeactivation");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName RICHMEDIAEXECUTE = new("RichMediaExecute");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName RICHMEDIAINSTANCE = new("RichMediaInstance");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName RICHMEDIAPARAMS = new("RichMediaParams");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName RICHMEDIAPOSITION = new("RichMediaPosition");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName RICHMEDIAPRESENTATION = new("RichMediaPresentation");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName RICHMEDIASETTINGS = new("RichMediaSettings");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName RICHMEDIAWINDOW = new("RichMediaWindow");
        /**
         * A name
         * @since 5.1.0
         */
        public static readonly PdfName RL = new("RL");
        /**
         * A name
         * @since 5.4.4
         */
        public static readonly PdfName ROLE = new("Role");
        /**
         * A name
         * @since 5.4.4
         */
        public static readonly PdfName RO = new("RO");
        /** A name */
        public static readonly PdfName ROLEMAP = new("RoleMap");
        /** A name */
        public static readonly PdfName ROOT = new("Root");
        /** A name */
        public static readonly PdfName ROTATE = new("Rotate");
        /**
         * A name
         * @since 5.4.0
         */
        public static readonly PdfName ROW = new("Row");
        /** A name */
        public static readonly PdfName ROWS = new("Rows");
        /**
         * A name
         * @since 5.4.0
         */
        public static readonly PdfName ROWSPAN = new("RowSpan");
        /**
         * A name
         * @since 5.4.3
         */
        public static readonly PdfName RP = new("RP");
        /**
         * A name
         * @since 5.1.0
         */
        public static readonly PdfName RT = new("RT");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName RUBY = new( "Ruby" );
        /** A name */
        public static readonly PdfName RUNLENGTHDECODE = new("RunLengthDecode");
        /** A name */
        public static readonly PdfName RV = new("RV");
        /** A name */
        public static readonly PdfName S = new("S");
        /** A name */
        public static readonly PdfName SATURATION = new("Saturation");
        /** A name */
        public static readonly PdfName SCHEMA = new("Schema");
        /**
         * A name
         * @since 5.4.0
         */
        public static readonly PdfName SCOPE = new("Scope");
        /** A name */
        public static readonly PdfName SCREEN = new("Screen");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName SCRIPTS = new("Scripts");
        /** A name */
        public static readonly PdfName SECT = new("Sect");
        /** A name */
        public static readonly PdfName SEPARATION = new("Separation");
        /** A name */
        public static readonly PdfName SETOCGSTATE = new("SetOCGState");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName SETTINGS = new("Settings");
        /** A name */
        public static readonly PdfName SHADING = new("Shading");
        /** A name */
        public static readonly PdfName SHADINGTYPE = new("ShadingType");
        /** A name */
        public static readonly PdfName SHIFT_JIS = new("Shift-JIS");
        /** A name */
        public static readonly PdfName SIG = new("Sig");
        /** A name */
        public static readonly PdfName SIGFIELDLOCK = new("SigFieldLock");
        /** A name */
        public static readonly PdfName SIGFLAGS = new("SigFlags");
        /** A name */
        public static readonly PdfName SIGREF = new("SigRef");
        /** A name */
        public static readonly PdfName SIMPLEX = new("Simplex");
        /** A name */
        public static readonly PdfName SINGLEPAGE = new("SinglePage");
        /** A name */
        public static readonly PdfName SIZE = new("Size");
        /** A name */
        public static readonly PdfName SMASK = new("SMask");

        public static readonly PdfName SMASKINDATA = new("SMaskInData");
        /** A name */
        public static readonly PdfName SORT = new("Sort");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName SOUND = new("Sound");
        /**
         * A name
         * @since 5.3.4
         */
        public static readonly PdfName SPACEAFTER = new("SpaceAfter");
        /**
         * A name
         * @since 5.3.4
         */
        public static readonly PdfName SPACEBEFORE = new("SpaceBefore");
        /** A name */
        public static readonly PdfName SPAN = new("Span");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName SPEED = new("Speed");
        /** A name */
        public static readonly PdfName SPLIT = new("Split");
        /** A name */
        public static readonly PdfName SQUARE = new("Square");
        /**
         * A name
         * @since 2.1.3
         */
        public static readonly PdfName SQUIGGLY = new("Squiggly");
        /**
         * A name
         * @since 5.1.0
         */
        public static readonly PdfName SS = new("SS");
        /** A name */
        public static readonly PdfName ST = new("St");
        /** A name */
        public static readonly PdfName STAMP = new("Stamp");
        /** A name */
        public static readonly PdfName STANDARD = new("Standard");
        /**
         * A name
         * @since 5.3.4
         */
        public static readonly PdfName START = new("Start");
        /**
         * A name
         * @since 5.3.4
         */
        public static readonly PdfName STARTINDENT = new("StartIndent");
        /** A name */
        public static readonly PdfName STATE = new("State");
        /** A name */
        public static readonly PdfName STATUS = new("Status");
        /** A name */
        public static readonly PdfName STDCF = new("StdCF");
        /** A name */
        public static readonly PdfName STEMV = new("StemV");
        /** A name */
        public static readonly PdfName STMF = new("StmF");
        /** A name */
        public static readonly PdfName STRF = new("StrF");
        /** A name */
        public static readonly PdfName STRIKEOUT = new("StrikeOut");
        /** A name */
        public static readonly PdfName STRUCTELEM = new("StructElem");
        /** A name */
        public static readonly PdfName STRUCTPARENT = new("StructParent");
        /** A name */
        public static readonly PdfName STRUCTPARENTS = new("StructParents");
        /** A name */
        public static readonly PdfName STRUCTTREEROOT = new("StructTreeRoot");
        /** A name */
        public static readonly PdfName STYLE = new("Style");
        /** A name */
        public static readonly PdfName SUBFILTER = new("SubFilter");
        /** A name */
        public static readonly PdfName SUBJECT = new("Subject");
        /** A name */
        public static readonly PdfName SUBMITFORM = new("SubmitForm");
        /** A name */
        public static readonly PdfName SUBTYPE = new("Subtype");
        /** A name */
        public static readonly PdfName SUMMARY = new("Summary");
        /** A name */
        public static readonly PdfName SUPPLEMENT = new("Supplement");
        /** A name */
        public static readonly PdfName SV = new("SV");
        /** A name */
        public static readonly PdfName SW = new("SW");
        /** A name of a base 14 type 1 font */
        public static readonly PdfName SYMBOL = new("Symbol");
        /**
         * T is very commonly used for various dictionary entries, including title
         * entries in a Tagged PDF element dictionary, and target dictionaries.
         */
        public static readonly PdfName T = new("T");
        /**
         * A name
         * @since	2.1.6
         */
        public static readonly PdfName TA = new("TA");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName TABLE = new("Table");
        /**
         * A name
         * @since	2.1.5
         */
        public static readonly PdfName TABS = new("Tabs");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName TBODY = new("TBody");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName TD = new("TD");
        /**
         * A name
         * @since 5.3.5
         */
        public static readonly PdfName TR = new("TR");
        /**
         * A name
         * @since 5.4.3
         */
        public static readonly PdfName TR2 = new("TR2");
        /** A name */
        public static readonly PdfName TEXT = new("Text");
        /**
         * A name
         * @since 5.3.4
         */
        public static readonly PdfName TEXTALIGN = new("TextAlign");
        /**
         * A name
         * @since 5.3.5
         */
        public static readonly PdfName TEXTDECORATIONCOLOR = new("TextDecorationColor");
        /**
         * A name
         * @since 5.3.5
         */
        public static readonly PdfName TEXTDECORATIONTHICKNESS = new("TextDecorationThickness");
        /**
         * A name
         * @since 5.3.5
         */
        public static readonly PdfName TEXTDECORATIONTYPE = new("TextDecorationType");

        /**
         * A name
         * @since 5.3.4
         */
        public static readonly PdfName TEXTINDENT = new("TextIndent");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName TFOOT = new("TFoot");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName TH = new("TH");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName THEAD = new("THead");
        /** A name */
        public static readonly PdfName THUMB = new("Thumb");
        /** A name */
        public static readonly PdfName THREADS = new("Threads");
        /** A name */
        public static readonly PdfName TI = new("TI");
        /**
         * A name
         * @since	2.1.6
         */
        public static readonly PdfName TIME = new("Time");
        /** A name */
        public static readonly PdfName TILINGTYPE = new("TilingType");
        /** A name of a base 14 type 1 font */
        public static readonly PdfName TIMES_ROMAN = new("Times-Roman");
        /** A name of a base 14 type 1 font */
        public static readonly PdfName TIMES_BOLD = new("Times-Bold");
        /** A name of a base 14 type 1 font */
        public static readonly PdfName TIMES_ITALIC = new("Times-Italic");
        /** A name of a base 14 type 1 font */
        public static readonly PdfName TIMES_BOLDITALIC = new("Times-BoldItalic");
        /**
         * Use Title for the document's top level title (optional), and for document
         * outline dictionaries, which can store bookmarks.
         * For all other uses of a title entry, including Tagged PDF, use {@link #T}
         */
        public static readonly PdfName TITLE = new("Title");
        /** A name */
        public static readonly PdfName TK = new("TK");
        /** A name */
        public static readonly PdfName TM = new("TM"); 
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName TOC = new("TOC");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName TOCI = new("TOCI");
        /** A name */
        public static readonly PdfName TOGGLE = new("Toggle");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName TOOLBAR = new("Toolbar");
        /** A name */
        public static readonly PdfName TOUNICODE = new("ToUnicode");
        /** A name */
        public static readonly PdfName TP = new("TP");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName TABLEROW = new( "TR" );
        /** A name */
        public static readonly PdfName TRANS = new("Trans");
        /** A name */
        public static readonly PdfName TRANSFORMPARAMS = new("TransformParams");
        /** A name */
        public static readonly PdfName TRANSFORMMETHOD = new("TransformMethod");
        /** A name */
        public static readonly PdfName TRANSPARENCY = new("Transparency");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName TRANSPARENT = new("Transparent");
        /**
         * A name
         * @since 5.4.3
         */
        public static readonly PdfName TRAPNET = new("TrapNet");
        /** A name */
        public static readonly PdfName TRAPPED = new("Trapped");
        /** A name */
        public static readonly PdfName TRIMBOX = new("TrimBox");
        /** A name */
        public static readonly PdfName TRUETYPE = new("TrueType");
        public static readonly PdfName TS = new("TS");
        public static readonly PdfName TTL = new("Ttl");
        /** A name */
        public static readonly PdfName TU = new("TU");
        /**
         * A name 
         * @since 5.4.4
         */
        public static readonly PdfName TV = new("tv");
        /** A name */
        public static readonly PdfName TWOCOLUMNLEFT = new("TwoColumnLeft");
        /** A name */
        public static readonly PdfName TWOCOLUMNRIGHT = new("TwoColumnRight");
        /** A name */
        public static readonly PdfName TWOPAGELEFT = new("TwoPageLeft");
        /** A name */
        public static readonly PdfName TWOPAGERIGHT = new("TwoPageRight");
        /** A name */
        public static readonly PdfName TX = new("Tx");
        /** A name */
        public static readonly PdfName TYPE = new("Type");
        /** A name */
        public static readonly PdfName TYPE0 = new("Type0");
        /** A name */
        public static readonly PdfName TYPE1 = new("Type1");
        /** A name of an attribute. */
        public static readonly PdfName TYPE3 = new("Type3");
        /** A name of an attribute. */
        public static readonly PdfName U = new("U");
        /** A name */
        public static readonly PdfName UE = new("UE");
        /** A name of an attribute. */
        public static readonly PdfName UF = new("UF");
        /** A name of an attribute. */
        public static readonly PdfName UHC = new("UHC");
        /** A name of an attribute. */
        public static readonly PdfName UNDERLINE = new("Underline");
        /**
         * A name
         * @since 5.2.1
         */
        public static readonly PdfName UNIX = new("Unix");
        /**
         * A name
         * @since 5.4.0
         */
        public static readonly PdfName UPPERALPHA = new("UpperAlpha");
        /**
         * A name
         * @since 5.4.0
         */
        public static readonly PdfName UPPERROMAN = new("UpperRoman");
        /** A name */
        public static readonly PdfName UR = new("UR");
        /** A name */
        public static readonly PdfName UR3 = new("UR3");
        /** A name */
        public static readonly PdfName URI = new("URI");
        /** A name */
        public static readonly PdfName URL = new("URL");
        /** A name */
        public static readonly PdfName USAGE = new("Usage");
        /** A name */
        public static readonly PdfName USEATTACHMENTS = new("UseAttachments");
        /** A name */
        public static readonly PdfName USENONE = new("UseNone");
        /** A name */
        public static readonly PdfName USEOC = new("UseOC");
        /** A name */
        public static readonly PdfName USEOUTLINES = new("UseOutlines");
        /** A name */
        public static readonly PdfName USER = new("User");
        /** A name */
        public static readonly PdfName USERPROPERTIES = new("UserProperties");
        /** A name */
        public static readonly PdfName USERUNIT = new("UserUnit");
        /** A name */
        public static readonly PdfName USETHUMBS = new("UseThumbs");
        /**
         * A name
         * @since 5.4.0
         */        
        public static readonly PdfName UTF_8 = new("utf_8");
        /** A name */
        public static readonly PdfName V = new("V");
        /** A name */
        public static readonly PdfName V2 = new("V2");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName VALIGN = new("VAlign");
        public static readonly PdfName VE = new("VE");
        /** A name */
        public static readonly PdfName VERISIGN_PPKVS = new("VeriSign.PPKVS");
        /** A name */
        public static readonly PdfName VERSION = new("Version");
        /**
         * A name
         * @since 5.0.2
         */
        public static readonly PdfName VERTICES = new("Vertices");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName VIDEO = new("Video");
        /** A name */
        public static readonly PdfName VIEW = new("View");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName VIEWS = new("Views");
        /** A name */
        public static readonly PdfName VIEWAREA = new("ViewArea");
        /** A name */
        public static readonly PdfName VIEWCLIP = new("ViewClip");
        /** A name */
        public static readonly PdfName VIEWERPREFERENCES = new("ViewerPreferences");
        /**
         * A name
         * @since 5.1.0
         */
        public static readonly PdfName VIEWPORT = new("Viewport");
        /** A name */
        public static readonly PdfName VIEWSTATE = new("ViewState");
        /** A name */
        public static readonly PdfName VISIBLEPAGES = new("VisiblePages");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName VOFFSET = new("VOffset");
        /**
         * A name
         * @since 5.1.0
         */
        public static readonly PdfName VP = new("VP");
        public static readonly PdfName VRI = new("VRI");
        /** A name of an attribute. */
        public static readonly PdfName W = new("W");
        /** A name of an attribute. */
        public static readonly PdfName W2 = new("W2");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName WARICHU = new("Warichu");
        /**
         * A name
         * @since 5.4.5
         */
        public static readonly PdfName WATERMARK = new("Watermark");
        /** A name of an attribute. */
        public static readonly PdfName WC = new("WC");
        /** A name of an attribute. */
        public static readonly PdfName WIDGET = new("Widget");
        /** A name of an attribute. */
        public static readonly PdfName WIDTH = new("Width");
        /** A name */
        public static readonly PdfName WIDTHS = new("Widths");
        /** A name of an encoding */
        public static readonly PdfName WIN = new("Win");
        /** A name of an encoding */
        public static readonly PdfName WIN_ANSI_ENCODING = new("WinAnsiEncoding");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName WINDOW = new("Window");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName WINDOWED = new("Windowed");
        /** A name of an encoding */
        public static readonly PdfName WIPE = new("Wipe");
        /** A name */
        public static readonly PdfName WHITEPOINT = new("WhitePoint");
        /**
         * A name of an attribute.
         * @since 5.1.0
         */
        public static readonly PdfName WKT = new("WKT");
        /** A name */
        public static readonly PdfName WP = new("WP");
        /** A name of an encoding */
        public static readonly PdfName WS = new("WS");
        /**
         * A name
         * @since 5.4.3
         */
        public static readonly PdfName WT = new("WT");
        /** A name */
        public static readonly PdfName X = new("X");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName XA = new("XA");
        /**
         * A name
         * @since 2.1.6
         */
        public static readonly PdfName XD = new("XD");
        /** A name */
        public static readonly PdfName XFA = new("XFA");
        /** A name */
        public static readonly PdfName XML = new("XML");
        /** A name */
        public static readonly PdfName XOBJECT = new("XObject");
        /**
         * A name
         * @since 5.1.0
         */
        public static readonly PdfName XPTS = new("XPTS");
        /** A name */
        public static readonly PdfName XREF = new("XRef");
        /** A name */
        public static readonly PdfName XREFSTM = new("XRefStm");
        /** A name */
        public static readonly PdfName XSTEP = new("XStep");
        /** A name */
        public static readonly PdfName XYZ = new("XYZ");
        /** A name */
        public static readonly PdfName YSTEP = new("YStep");
        /** A name */
        public static readonly PdfName ZADB = new("ZaDb");
        /** A name of a base 14 type 1 font */
        public static readonly PdfName ZAPFDINGBATS = new("ZapfDingbats");
        /** A name */
        public static readonly PdfName ZOOM = new("Zoom");
    
        /**
         * map strings to all known static names
         * @since 2.1.6
         */
        public static Dictionary<string, PdfName> staticNames;

        /**
         * Use reflection to cache all the static public readonly names so
         * future <code>PdfName</code> additions don't have to be "added twice".
         * A bit less efficient (around 50ms spent here on a 2.2ghz machine),
         *  but Much Less error prone.
         * @since 2.1.6
         */

        static PdfName() {
            var fields = typeof(PdfName).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            staticNames = new Dictionary<string,PdfName>(fields.Length);
            try {
                for (var fldIdx = 0; fldIdx < fields.Length; ++fldIdx) {
                    var curFld = fields[fldIdx];
                    if (curFld.FieldType.Equals(typeof(PdfName))) {
                        var name = (PdfName)curFld.GetValue(null);
                        staticNames[DecodeName(name.ToString())] = name;
                    }
               }
            } 
            catch {}
        }

        // CLASS VARIABLES
        private int hash = 0;
    
        // constructors
    
        /**
        * Constructs a new <CODE>PdfName</CODE>. The name length will be checked.
        * @param name the new name
        */
        public PdfName(string name) : this(name, true) {
        }
        
        /**
        * Constructs a new <CODE>PdfName</CODE>.
        * @param name the new name
        * @param lengthCheck if <CODE>true</CODE> check the lenght validity, if <CODE>false</CODE> the name can
        * have any length
        */
        public PdfName(string name, bool lengthCheck) : base(PdfObject.NAME) {
            // The minimum number of characters in a name is 0, the maximum is 127 (the '/' not included)
            var length = name.Length;
            if (lengthCheck && length > 127) {
                throw new ArgumentException(MessageLocalization.GetComposedMessage("the.name.1.is.too.long.2.characters", name, length));
            }
            bytes = EncodeName(name);
        }

        public PdfName(byte[] bytes) : base(PdfObject.NAME, bytes) {
        }
       
        // methods
    
        /**
         * Compares this object with the specified object for order.  Returns a
         * negative int, zero, or a positive int as this object is less
         * than, equal to, or greater than the specified object.<p>
         *
         *
         * @param   object the Object to be compared.
         * @return  a negative int, zero, or a positive int as this object
         *        is less than, equal to, or greater than the specified object.
         *
         * @throws Exception if the specified object's type prevents it
         *         from being compared to this Object.
         */
        virtual public int CompareTo(PdfName name) {
            var myBytes = bytes;
            var objBytes = name.bytes;
            var len = Math.Min(myBytes.Length, objBytes.Length);
            for (var i=0; i<len; i++) {
                if (myBytes[i] > objBytes[i])
                    return 1;
            
                if (myBytes[i] < objBytes[i])
                    return -1;
            }
            if (myBytes.Length < objBytes.Length)
                return -1;
            if (myBytes.Length > objBytes.Length)
                return 1;
            return 0;
        }
    
        /**
         * Indicates whether some other object is "equal to" this one.
         *
         * @param   obj   the reference object with which to compare.
         * @return  <code>true</code> if this object is the same as the obj
         *          argument; <code>false</code> otherwise.
         */
        public override bool Equals(object obj) {
            if (this == obj)
                return true;
            if (obj is PdfName)
                return CompareTo((PdfName)obj) == 0;
            return false;
        }
    
        /**
         * Returns a hash code value for the object. This method is
         * supported for the benefit of hashtables such as those provided by
         * <code>java.util.Hashtable</code>.
         *
         * @return  a hash code value for this object.
         */
        public override int GetHashCode() {
            var h = hash;
            if (h == 0) {
                var ptr = 0;
                var len = bytes.Length;
            
                for (var i = 0; i < len; i++)
                    h = 31*h + (bytes[ptr++] & 0xff);
                hash = h;
            }
            return h;
        }
    
        /**
        * Encodes a plain name given in the unescaped form "AB CD" into "/AB#20CD".
        *
        * @param name the name to encode
        * @return the encoded name
        * @since	2.1.5
        */
        public static byte[] EncodeName(string name)
        {
            var length = name.Length;
            // every special character has to be substituted
            var pdfName = new ByteBuffer(length + 20);
            pdfName.Append('/');
            var chars = name.ToCharArray();
            char character;
            // loop over all the characters
            foreach (var cc in chars) {
                character = (char)(cc & 0xff);
                // special characters are escaped (reference manual p.39)
                switch (character) {
                    case ' ':
                    case '%':
                    case '(':
                    case ')':
                    case '<':
                    case '>':
                    case '[':
                    case ']':
                    case '{':
                    case '}':
                    case '/':
                    case '#':
                        pdfName.Append('#');
                        pdfName.Append(global::System.Convert.ToString(character, 16));
                        break;
                    default:
                        if (character > 126 || character < 32) {
                            pdfName.Append('#');
                            if (character < 16)
                                pdfName.Append('0');
                            pdfName.Append(global::System.Convert.ToString(character, 16));
                        }
                        else
                            pdfName.Append(character);
                        break;
                }
            }
            return pdfName.ToByteArray();
        }

        /** Decodes an escaped name in the form "/AB#20CD" into "AB CD".
         * @param name the name to decode
         * @return the decoded name
         */
        public static string DecodeName(string name) {
            var buf = new StringBuilder();
            var len = name.Length;
            for (var k = 1; k < len; ++k) {
                var c = name[k];
                if (c == '#') {
                    c = (char)((PRTokeniser.GetHex(name[k + 1]) << 4) + PRTokeniser.GetHex(name[k + 2]));
                    k += 2;
                }
                buf.Append(c);
            }
            return buf.ToString();
        }
    }
}
