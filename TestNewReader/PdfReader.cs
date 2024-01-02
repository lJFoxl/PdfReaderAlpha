using PdfSharp_net8_.iTextSharp.text.pdf;
using PdfSharp_net8_.iTextSharp.text.pdf.parser;

namespace TestNewReader;
public class PdfReaderEx
{
    public PdfReader Reader { get; set; }
    string filePath = @"G:\liga_с_qr.pdf";
    public PdfReaderEx()
    {
        Reader = new PdfReader(filePath);
    }

    public void Read()
    {
        string extractedText = string.Empty;

        for (int pageNumber = 1; pageNumber <= Reader.NumberOfPages; pageNumber++)
        {
            Console.WriteLine($"страница {pageNumber}");
            var strategy = new SimpleTextExtractionStrategy();
            string pageText = PdfTextExtractor.GetTextFromPage(Reader, pageNumber, strategy);
            PdfDictionary pageDict = Reader.GetPageN(pageNumber);
            ImageRenderListener listener = new ImageRenderListener();
            PdfContentStreamProcessor processor = new PdfContentStreamProcessor(listener);
            processor.ProcessContent(PdfReader.GetPageContent(pageDict), pageDict.GetAsDict(PdfName.RESOURCES));

            extractedText += pageText;
        }
        Console.WriteLine(extractedText);
    }
}
class ImageRenderListener : IRenderListener
{
    public List<PdfImageObject> Images { get; private set; }

    public ImageRenderListener()
    {
        Images = new List<PdfImageObject>();
    }

    public void BeginTextBlock() { }
    public void EndTextBlock() { }
    public void RenderText(TextRenderInfo renderInfo) { }

    public void RenderImage(ImageRenderInfo renderInfo)
    {
        var imageObject = renderInfo.GetImage();
        if (imageObject != null)
        {
            //var imageData = ImageDataFactory.Create(imageObject.GetImageAsBytes());
            Images.Add(imageObject);
            // Сохранение изображения в файл
            string extractImagToDir = "G:\\OutputFiles\\";
            try
            {
                var filename = extractImagToDir + "new"+DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
                File.WriteAllBytes(filename, imageObject.GetImageAsBytes());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}