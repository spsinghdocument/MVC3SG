using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace ReportManagement
{
   public class pdfreader_i
    {
       public static string ExtractTextFromPdf(string path)
       {
           PdfReader reader = new PdfReader(@"D:\Dropboxdata\sp bil\ab.pdf");
         
               StringBuilder text = new StringBuilder();

               for (int i = 1; i <= reader.NumberOfPages; i++)
               {
                   text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
               }

               return text.ToString();
           
       }
    }
}
