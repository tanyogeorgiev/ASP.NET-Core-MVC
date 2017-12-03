using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LearningSystem.Service.Implementations
{
    public class PdfGenerator : IPdfGenerator
    {
        public byte[] GeneratePdfFromThml(string html)
        {
            
            var pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            var htmlparser = new HtmlWorker(pdfDoc);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                var writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                using (var stringReader = new StringReader(html))
                {
                    htmlparser.Parse(stringReader);
                }

                
                pdfDoc.Close();

               return memoryStream.ToArray();
              
            }
        }
    }
}
