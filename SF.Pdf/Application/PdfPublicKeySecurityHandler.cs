using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.X509;

namespace SF.Pdf.Application;
public class PdfPublicKeySecurityHandler
{

    private const int SEED_LENGTH = 20;

    private List<PdfPublicKeyRecipient> recipients = null;

    private byte[] seed;

    public PdfPublicKeySecurityHandler()
    {
        seed = IVGenerator.GetIV(SEED_LENGTH);
        recipients = new List<PdfPublicKeyRecipient>();
    }


    virtual public void AddRecipient(PdfPublicKeyRecipient recipient)
    {
        recipients.Add(recipient);
    }

    virtual protected internal byte[] GetSeed()
    {
        return (byte[])seed.Clone();
    }

    virtual public int GetRecipientsSize()
    {
        return recipients.Count;
    }

    virtual public byte[] GetEncodedRecipient(int index)
    {
        //Certificate certificate = recipient.GetX509();
        var recipient = recipients[index];
        var cms = recipient.Cms;

        if (cms != null) return cms;

        var certificate = recipient.Certificate;
        var permission = recipient.Permission;//PdfWriter.AllowCopy | PdfWriter.AllowPrinting | PdfWriter.AllowScreenReaders | PdfWriter.AllowAssembly;   
        var revision = 3;

        permission |= (int)(revision == 3 ? (uint)0xfffff0c0 : (uint)0xffffffc0);
        permission &= unchecked((int)0xfffffffc);
        permission += 1;

        var pkcs7input = new byte[24];

        var one = (byte)(permission);
        var two = (byte)(permission >> 8);
        var three = (byte)(permission >> 16);
        var four = (byte)(permission >> 24);

        global::System.Array.Copy(seed, 0, pkcs7input, 0, 20); // put this seed in the pkcs7 input

        pkcs7input[20] = four;
        pkcs7input[21] = three;
        pkcs7input[22] = two;
        pkcs7input[23] = one;

        var obj = CreateDERForRecipient(pkcs7input, certificate);

        var baos = new MemoryStream();

        var k = Asn1OutputStream.Create(baos);

        k.WriteObject(obj);

        cms = baos.ToArray();

        recipient.Cms = cms;

        return cms;
    }

    virtual public PdfArray GetEncodedRecipients()
    {
        var EncodedRecipients = new PdfArray();
        byte[] cms = null;
        for (var i = 0; i < recipients.Count; i++)
        {
            try
            {
                cms = GetEncodedRecipient(i);
                EncodedRecipients.Add(new PdfLiteral(StringUtils.EscapeString(cms)));
            }
            catch
            {
                EncodedRecipients = null;
            }
        }
        return EncodedRecipients;
    }

    private Asn1Object CreateDERForRecipient(byte[] inp, X509Certificate cert)
    {

        var s = "1.2.840.113549.3.2";

        var outp = new byte[100];
        var derob = new DerObjectIdentifier(s);
        byte[] keyp = IVGenerator.GetIV(16);
        var cf = CipherUtilities.GetCipher(derob);
        var kp = new KeyParameter(keyp);
        byte[] iv = IVGenerator.GetIV(cf.GetBlockSize());
        var piv = new ParametersWithIV(kp, iv);
        cf.Init(true, piv);
        var len = cf.DoFinal(inp, outp, 0);

        var abyte1 = new byte[len];
        global::System.Array.Copy(outp, 0, abyte1, 0, len);
        var deroctetstring = new DerOctetString(abyte1);
        var keytransrecipientinfo = ComputeRecipientInfo(cert, keyp);
        var derset = new DerSet(new RecipientInfo(keytransrecipientinfo));
        var ev = new Asn1EncodableVector();
        ev.Add(new DerInteger(58));
        ev.Add(new DerOctetString(iv));
        var seq = new DerSequence(ev);
        var algorithmidentifier = new AlgorithmIdentifier(derob, seq);
        var encryptedcontentinfo =
            new EncryptedContentInfo(PkcsObjectIdentifiers.Data, algorithmidentifier, deroctetstring);
        Asn1Set set = null;
        var env = new EnvelopedData(null, derset, encryptedcontentinfo, set);
        var contentinfo =
            new Org.BouncyCastle.Asn1.Cms.ContentInfo(PkcsObjectIdentifiers.EnvelopedData, env);
        return contentinfo.ToAsn1Object();
    }

    private KeyTransRecipientInfo ComputeRecipientInfo(X509Certificate x509certificate, byte[] abyte0)
    {
        var asn1inputstream =
            new Asn1InputStream(new MemoryStream(x509certificate.GetTbsCertificate()));
        var tbscertificatestructure =
            TbsCertificateStructure.GetInstance(asn1inputstream.ReadObject());
        var algorithmidentifier = tbscertificatestructure.SubjectPublicKeyInfo.AlgorithmID;
        var issuerandserialnumber =
            new Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber(
                tbscertificatestructure.Issuer,
                tbscertificatestructure.SerialNumber.Value);
        var cipher = CipherUtilities.GetCipher(algorithmidentifier.Algorithm);
        cipher.Init(true, x509certificate.GetPublicKey());
        var outp = new byte[10000];
        var len = cipher.DoFinal(abyte0, outp, 0);
        var abyte1 = new byte[len];
        global::System.Array.Copy(outp, 0, abyte1, 0, len);
        var deroctetstring = new DerOctetString(abyte1);
        var recipId = new RecipientIdentifier(issuerandserialnumber);
        return new KeyTransRecipientInfo(recipId, algorithmidentifier, deroctetstring);
    }
}
