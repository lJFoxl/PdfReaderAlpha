using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace PdfSharp_net8_.iTextSharp.text.pdf.security {

/**
 * An exception that is thrown when something is wrong with a certificate.
 */

    public class VerificationException : GeneralSecurityException {

        /**
	     * Creates a VerificationException
	     */
        public VerificationException(X509Certificate cert, String message)
            : base(String.Format("Certificate {0} failed: {1}",
                                cert == null ? "Unknown" : cert.SubjectDN.ToString(), message)) {}
    }
}
