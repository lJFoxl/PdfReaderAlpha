using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.X509;

namespace SF.Pdf.Application;
public class PdfPublicKeyRecipient
{

    private X509Certificate certificate = null;

    private int permission = 0;

    protected byte[] cms = null;

    public PdfPublicKeyRecipient(X509Certificate certificate, int permission)
    {
        this.certificate = certificate;
        this.permission = permission;
    }

    virtual public X509Certificate Certificate => certificate;

    virtual public int Permission => permission;

    protected virtual internal byte[] Cms
    {
        set => cms = value;
        get => cms;
    }
}
