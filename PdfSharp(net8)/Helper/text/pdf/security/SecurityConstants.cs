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

namespace PdfSharp_net8_.Helper.text.pdf.security
{
    internal sealed class SecurityConstants {

        public const string XMLNS = "xmlns";
        public const string XMLNS_XADES = "xmlns:xades";

        public const string XMLNS_URI = "http://www.w3.org/2000/xmlns/";
        public const string XMLDSIG_URI = "http://www.w3.org/2000/09/xmldsig#";
        public const string XADES_132_URI = "http://uri.etsi.org/01903/v1.3.2#";
        public const string XMLDSIG_URI_C14N = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";
        public const string XMLDSIG_URI_RSA_SHA1 = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
        public const string XMLDSIG_URI_DSA_SHA1 = "http://www.w3.org/2000/09/xmldsig#dsa-sha1";
        public const string XMLDSIG_URI_ENVELOPED = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";
        public const string XMLDSIG_URI_XPATH_FILTER2 = "http://www.w3.org/2002/06/xmldsig-filter2";
        public const string XMLDSIG_URI_SHA1 = "http://www.w3.org/2000/09/xmldsig#sha1";

        public const string SignedProperties_Type = "http://uri.etsi.org/01903#SignedProperties";

        public const string OIDAsURN = "OIDAsURN";
        public const string OID_DSA_SHA1 = "urn:oid:1.2.840.10040.4.3";
        public const string OID_DSA_SHA1_DESC = "ANSI X9.57 DSA signature generated with SHA-1 hash (DSA x9.30)";

        public const string OID_RSA_SHA1 = "urn:oid:1.2.840.113549.1.1.5";
        public const string OID_RSA_SHA1_DESC = "RSA (PKCS #1 v1.5) with SHA-1 signature";

        public const string DSA = "DSA";
        public const string RSA = "RSA";
        public const string SHA1 = "SHA1";

        public const string DigestMethod = "DigestMethod";
        public const string DigestValue = "DigestValue";
        public const string Signature = "Signature";
        public const string SignatureValue = "SignatureValue";
        public const string X509SerialNumber = "X509SerialNumber";
        public const string X509IssuerName = "X509IssuerName";

        public const string Algorithm = "Algorithm";
        public const string Id = "Id";
        public const string ObjectReference = "ObjectReference";
        public const string Target = "Target";
        public const string Qualifier = "Qualifier";

        public const string XADES_Encoding = "xades:Encoding";
        public const string XADES_MimeType = "xades:MimeType";
        public const string XADES_Description = "xades:Description";
        public const string XADES_DataObjectFormat = "xades:DataObjectFormat";
        public const string XADES_SignedDataObjectProperties = "xades:SignedDataObjectProperties";
        public const string XADES_IssuerSerial = "xades:IssuerSerial";
        public const string XADES_CertDigest = "xades:CertDigest";
        public const string XADES_Cert = "xades:Cert";
        public const string XADES_SigningCertificate = "xades:SigningCertificate";
        public const string XADES_SigningTime = "xades:SigningTime";
        public const string XADES_SignedSignatureProperties = "xades:SignedSignatureProperties";
        public const string XADES_SignedProperties = "xades:SignedProperties";
        public const string XADES_QualifyingProperties = "xades:QualifyingProperties";
        public const string XADES_SignaturePolicyIdentifier = "xades:SignaturePolicyIdentifier";
        public const string XADES_SignaturePolicyId = "xades:SignaturePolicyId";
        public const string XADES_SigPolicyId = "xades:SigPolicyId";
        public const string XADES_Identifier = "xades:Identifier";
        public const string XADES_SigPolicyHash = "xades:SigPolicyHash";


        public const string Reference_ = "Reference-";
        public const string SignedProperties_ = "SignedProperties-";
        public const string Signature_ = "Signature-";

        public const string SigningTimeFormat = "yyyy-MM-dd'T'HH:mm:sszzz";
    }
}
