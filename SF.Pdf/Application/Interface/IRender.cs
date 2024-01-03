using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Pdf.Application.Interface;
internal interface IRender
{
    /// <summary>
    /// Вызывается, когда начинается новый текстовый блок (BT).
    /// </summary>
    void BeginBlock();
    void RenderText(TextRenderInfo renderInfo);
}
