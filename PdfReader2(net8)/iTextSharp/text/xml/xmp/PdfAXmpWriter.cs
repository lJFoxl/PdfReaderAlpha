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
/**
 * Subclass of XmpWriter that adds info about the PDF/A level.
 * 
 * @see XmpWriter
 */

using PdfReader2_net8_.iTextSharp.text.pdf;
using PdfSharp_net8_.iTextSharp.text.pdf;
using PdfSharp_net8_.iTextSharp.text.xml.xmp;
using PdfSharp_net8_.iTextSharp.xmp;

namespace PdfReader2_net8_.iTextSharp.text.xml.xmp {

    public class PdfAXmpWriter : XmpWriter {
        private const String pdfUaExtension =
            "    <x:xmpmeta xmlns:x=\"adobe:ns:meta/\">\n" +
            "      <rdf:RDF xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\">\n" +
            "        <rdf:Description rdf:about=\"\" xmlns:pdfaExtension=\"http://www.aiim.org/pdfa/ns/extension/\" xmlns:pdfaSchema=\"http://www.aiim.org/pdfa/ns/schema#\" xmlns:pdfaProperty=\"http://www.aiim.org/pdfa/ns/property#\">\n" +
            "          <pdfaExtension:schemas>\n" +
            "            <rdf:Bag>\n" +
            "              <rdf:li rdf:parseType=\"Resource\">\n" +
            "                <pdfaSchema:namespaceURI>http://www.aiim.org/pdfua/ns/id/</pdfaSchema:namespaceURI>\n" +
            "                <pdfaSchema:prefix>pdfuaid</pdfaSchema:prefix>\n" +
            "                <pdfaSchema:schema>PDF/UA identification schema</pdfaSchema:schema>\n" +
            "                <pdfaSchema:property>\n" +
            "                  <rdf:Seq>\n" +
            "                    <rdf:li rdf:parseType=\"Resource\">\n" +
            "                      <pdfaProperty:category>internal</pdfaProperty:category>\n" +
            "                      <pdfaProperty:description>PDF/UA version identifier</pdfaProperty:description>\n" +
            "                      <pdfaProperty:name>part</pdfaProperty:name>\n" +
            "                      <pdfaProperty:valueType>Integer</pdfaProperty:valueType>\n" +
            "                    </rdf:li>\n" +
            "                    <rdf:li rdf:parseType=\"Resource\">\n" +
            "                      <pdfaProperty:category>internal</pdfaProperty:category>\n" +
            "                      <pdfaProperty:description>PDF/UA amendment identifier</pdfaProperty:description>\n" +
            "                      <pdfaProperty:name>amd</pdfaProperty:name>\n" +
            "                      <pdfaProperty:valueType>Text</pdfaProperty:valueType>\n" +
            "                    </rdf:li>\n" +
            "                    <rdf:li rdf:parseType=\"Resource\">\n" +
            "                      <pdfaProperty:category>internal</pdfaProperty:category>\n" +
            "                      <pdfaProperty:description>PDF/UA corrigenda identifier</pdfaProperty:description>\n" +
            "                      <pdfaProperty:name>corr</pdfaProperty:name>\n" +
            "                      <pdfaProperty:valueType>Text</pdfaProperty:valueType>\n" +
            "                    </rdf:li>\n" +
            "                  </rdf:Seq>\n" +
            "                </pdfaSchema:property>\n" +
            "              </rdf:li>\n" +
            "            </rdf:Bag>\n" +
            "          </pdfaExtension:schemas>\n" +
            "        </rdf:Description>\n" +
            "      </rdf:RDF>\n" +
            "    </x:xmpmeta>";

        private const String zugferdExtension =
            "    <x:xmpmeta xmlns:x=\"adobe:ns:meta/\">\n" +
            "      <rdf:RDF xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\">\n" +
            "        <rdf:Description rdf:about=\"\" xmlns:zf=\"urn:ferd:pdfa:CrossIndustryDocument:invoice:1p0#\">\n" +
            "          <zf:ConformanceLevel>%s</zf:ConformanceLevel>\n" +
            "          <zf:DocumentFileName>ZUGFeRD-invoice.xml</zf:DocumentFileName>\n" +
            "          <zf:DocumentType>INVOICE</zf:DocumentType>\n" +
            "          <zf:Version>1.0</zf:Version>\n" +
            "        </rdf:Description>\n" +
            "        <rdf:Description rdf:about=\"\" xmlns:pdfaExtension=\"http://www.aiim.org/pdfa/ns/extension/\" xmlns:pdfaSchema=\"http://www.aiim.org/pdfa/ns/schema#\" xmlns:pdfaProperty=\"http://www.aiim.org/pdfa/ns/property#\">\n" +
            "          <pdfaExtension:schemas>\n" +
            "            <rdf:Bag>\n" +
            "              <rdf:li rdf:parseType=\"Resource\">\n" +
            "                <pdfaSchema:schema>ZUGFeRD PDFA Extension Schema</pdfaSchema:schema>\n" +
            "                <pdfaSchema:namespaceURI>urn:ferd:pdfa:CrossIndustryDocument:invoice:1p0#</pdfaSchema:namespaceURI>\n" +
            "                <pdfaSchema:prefix>zf</pdfaSchema:prefix>\n" +
            "                <pdfaSchema:property>\n" +
            "                  <rdf:Seq>\n" +
            "                    <rdf:li rdf:parseType=\"Resource\">\n" +
            "                      <pdfaProperty:name>DocumentFileName</pdfaProperty:name>\n" +
            "                      <pdfaProperty:valueType>Text</pdfaProperty:valueType>\n" +
            "                      <pdfaProperty:category>external</pdfaProperty:category>\n" +
            "                      <pdfaProperty:description>name of the embedded XML invoice file</pdfaProperty:description>\n" +
            "                    </rdf:li>\n" +
            "                    <rdf:li rdf:parseType=\"Resource\">\n" +
            "                      <pdfaProperty:name>DocumentType</pdfaProperty:name>\n" +
            "                      <pdfaProperty:valueType>Text</pdfaProperty:valueType>\n" +
            "                      <pdfaProperty:category>external</pdfaProperty:category>\n" +
            "                      <pdfaProperty:description>INVOICE</pdfaProperty:description>\n" +
            "                    </rdf:li>\n" +
            "                    <rdf:li rdf:parseType=\"Resource\">\n" +
            "                      <pdfaProperty:name>Version</pdfaProperty:name>\n" +
            "                      <pdfaProperty:valueType>Text</pdfaProperty:valueType>\n" +
            "                      <pdfaProperty:category>external</pdfaProperty:category>\n" +
            "                      <pdfaProperty:description>The actual version of the ZUGFeRD data</pdfaProperty:description>\n" +
            "                    </rdf:li>\n" +
            "                    <rdf:li rdf:parseType=\"Resource\">\n" +
            "                      <pdfaProperty:name>ConformanceLevel</pdfaProperty:name>\n" +
            "                      <pdfaProperty:valueType>Text</pdfaProperty:valueType>\n" +
            "                      <pdfaProperty:category>external</pdfaProperty:category>\n" +
            "                      <pdfaProperty:description>The conformance level of the ZUGFeRD data</pdfaProperty:description>\n" +
            "                    </rdf:li>\n" +
            "                  </rdf:Seq>\n" +
            "                </pdfaSchema:property>\n" +
            "              </rdf:li>\n" +
            "            </rdf:Bag>\n" +
            "          </pdfaExtension:schemas>\n" +
            "        </rdf:Description>\n" +
            "      </rdf:RDF>\n" +
            "    </x:xmpmeta>\n";


        private PdfWriter writer;

        public const String zugferdSchemaNS = "urn:ferd:pdfa:CrossIndustryDocument:invoice:1p0#";
        public const String zugferdConformanceLevel = "ConformanceLevel";
        public const String zugferdDocumentFileName = "DocumentFileName";
        public const String zugferdDocumentType = "DocumentType";
        public const String zugferdVersion = "Version";

        /**
         * Creates and XMP writer that adds info about the PDF/A conformance level.
         * 
         * @param os
         * @param conformanceLevel
         * @throws IOException
         */
        public PdfAXmpWriter(Stream os, PdfAConformanceLevel conformanceLevel, PdfWriter writer)
            : base(os) {
            this.writer = writer;
            try {
                AddRdfDescription(conformanceLevel);
            }
            catch (XmpException xmpExc) {
                throw new IOException(xmpExc.Message);
            }
        }

        /**
         * Creates and XMP writer that adds info about the PDF/A conformance level.
         * 
         * @param os
         * @param info
         * @param conformanceLevel
         * @throws IOException
         */
        public PdfAXmpWriter(Stream os, PdfDictionary info, PdfAConformanceLevel conformanceLevel, PdfWriter writer)
            : base(os, info) {
            this.writer = writer;
            try {
                AddRdfDescription(conformanceLevel);
            }
            catch (XmpException xmpExc) {
                throw new IOException(xmpExc.Message);
            }
        }

        /**
         * Creates and XMP writer that adds info about the PDF/A conformance level.
         * 
         * @param os
         * @param info
         * @param conformanceLevel
         * @throws IOException
         */
        public PdfAXmpWriter(Stream os, IDictionary<String, String> info, PdfAConformanceLevel conformanceLevel, PdfWriter writer)
            : base(os, info) {
            this.writer = writer;
            try {
                AddRdfDescription(conformanceLevel);
            }
            catch (XmpException xmpExc) {
                throw new IOException(xmpExc.Message);
            }
        }

        /**
         * Adds information about the PDF/A conformance level to the XMP metadata.
         * 
         * @param conformanceLevel
         * @throws IOException
         */
        private void AddRdfDescription(PdfAConformanceLevel conformanceLevel) {
            switch (conformanceLevel) {
                case PdfAConformanceLevel.PDF_A_1A:
                    xmpMeta.SetProperty(XmpConst.NS_PDFA_ID, PdfAProperties.PART, "1");
                    xmpMeta.SetProperty(XmpConst.NS_PDFA_ID, PdfAProperties.CONFORMANCE, "A");
                    break;
                case PdfAConformanceLevel.PDF_A_1B:
                    xmpMeta.SetProperty(XmpConst.NS_PDFA_ID, PdfAProperties.PART, "1");
                    xmpMeta.SetProperty(XmpConst.NS_PDFA_ID, PdfAProperties.CONFORMANCE, "B");
                    break;
                case PdfAConformanceLevel.PDF_A_2A:
                    xmpMeta.SetProperty(XmpConst.NS_PDFA_ID, PdfAProperties.PART, "2");
                    xmpMeta.SetProperty(XmpConst.NS_PDFA_ID, PdfAProperties.CONFORMANCE, "A");
                    break;
                case PdfAConformanceLevel.PDF_A_2B:
                    xmpMeta.SetProperty(XmpConst.NS_PDFA_ID, PdfAProperties.PART, "2");
                    xmpMeta.SetProperty(XmpConst.NS_PDFA_ID, PdfAProperties.CONFORMANCE, "B");
                    break;
                case PdfAConformanceLevel.PDF_A_2U:
                    xmpMeta.SetProperty(XmpConst.NS_PDFA_ID, PdfAProperties.PART, "2");
                    xmpMeta.SetProperty(XmpConst.NS_PDFA_ID, PdfAProperties.CONFORMANCE, "U");
                    break;
                case PdfAConformanceLevel.PDF_A_3A:
                    xmpMeta.SetProperty(XmpConst.NS_PDFA_ID, PdfAProperties.PART, "3");
                    xmpMeta.SetProperty(XmpConst.NS_PDFA_ID, PdfAProperties.CONFORMANCE, "A");
                    break;
                case PdfAConformanceLevel.PDF_A_3B:
                    xmpMeta.SetProperty(XmpConst.NS_PDFA_ID, PdfAProperties.PART, "3");
                    xmpMeta.SetProperty(XmpConst.NS_PDFA_ID, PdfAProperties.CONFORMANCE, "B");
                    break;
                case PdfAConformanceLevel.PDF_A_3U:
                    xmpMeta.SetProperty(XmpConst.NS_PDFA_ID, PdfAProperties.PART, "3");
                    xmpMeta.SetProperty(XmpConst.NS_PDFA_ID, PdfAProperties.CONFORMANCE, "U");
                    break;
                case PdfAConformanceLevel.ZUGFeRD:
                case PdfAConformanceLevel.ZUGFeRDBasic:
                case PdfAConformanceLevel.ZUGFeRDComfort:
                case PdfAConformanceLevel.ZUGFeRDExtended:
                    xmpMeta.SetProperty(XmpConst.NS_PDFA_ID, PdfAProperties.PART, "3");
                    xmpMeta.SetProperty(XmpConst.NS_PDFA_ID, PdfAProperties.CONFORMANCE, "B");
                    IXmpMeta taggedExtensionMetaComfort = XmpMetaFactory.ParseFromString(getZugfredExtension(conformanceLevel));
                    XmpUtils.AppendProperties(taggedExtensionMetaComfort, xmpMeta, true, false);
                    break;
                default:
                    break;
            }
            if (writer.IsTagged()) {
                IXmpMeta taggedExtensionMeta = XmpMetaFactory.ParseFromString(pdfUaExtension);
                XmpUtils.AppendProperties(taggedExtensionMeta, xmpMeta, true, false);
            }
        }

        private String getZugfredExtension(PdfAConformanceLevel conformanceLevel) {
            switch (conformanceLevel) {
                case PdfAConformanceLevel.ZUGFeRD:
                case PdfAConformanceLevel.ZUGFeRDBasic:
                    return String.Format(zugferdExtension, "BASIC");
                case PdfAConformanceLevel.ZUGFeRDComfort:
                    return String.Format(zugferdExtension, "COMFORT");
                case PdfAConformanceLevel.ZUGFeRDExtended:
                    return String.Format(zugferdExtension, "EXTENDED");
                default:
                    return null;
            }
        }
    }
}
