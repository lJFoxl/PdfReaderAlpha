using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Pdf.Application;
public class PRIndirectReference : PdfIndirectReference
{

    protected PdfReader reader;
    // membervariables

    // constructors

    /**
     * Constructs a <CODE>PdfIndirectReference</CODE>.
     *
     * @param        reader            a <CODE>PdfReader</CODE>
     * @param        number            the object number.
     * @param        generation        the generation number.
     */

    public PRIndirectReference(PdfReader reader, int number, int generation)
    {
        type = INDIRECT;
        this.number = number;
        this.generation = generation;
        this.reader = reader;
    }


    /// <summary>
    /// Конструирует PdfIndirectReference.
    /// </summary>
    /// <param name="reader">reader PdfReader</param>
    /// <param name="number">номер объекта.</param>
    public PRIndirectReference(PdfReader reader, int number) : this(reader, number, 0) { }

    public virtual PdfReader Reader => reader;

    public virtual void SetNumber(int number, int generation)
    {
        this.number = number;
        this.generation = generation;
    }
}
