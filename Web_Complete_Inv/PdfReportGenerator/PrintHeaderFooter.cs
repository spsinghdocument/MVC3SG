// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrintHeaderFooter.cs" company="SemanticArchitecture">
//   http://www.SemanticArchitecture.net pkalkie@gmail.com
// </copyright>
// <summary>
//   This class represents the standard header and footer for a PDF print.
//   application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ReportManagement
{
    using System;

    using iTextSharp.text;
    using iTextSharp.text.pdf;
    
    /// <summary>
    /// This class represents the standard header and footer for a PDF print.
    /// application.
    /// </summary>
    public class PrintHeaderFooter : PdfPageEventHelper
    {
        private PdfContentByte pdfContent;
        private PdfTemplate pageNumberTemplate;
        private BaseFont baseFont;
        private DateTime printTime;

        public string Title { get; set; }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            printTime = DateTime.Now;
            baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            pdfContent = writer.DirectContent;
            pageNumberTemplate = pdfContent.CreateTemplate(50, 50);

        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);

            Rectangle pageSize = document.PageSize;

            if (Title != string.Empty)
            {
                pdfContent.BeginText();
                pdfContent.SetFontAndSize(baseFont, 11);
                pdfContent.SetRGBColorFill(0, 0, 0);
                pdfContent.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetTop(40));
                pdfContent.ShowText(Title);
                pdfContent.EndText();
            }
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            int pageN = writer.PageNumber;

            string text = "From SG Telecom        SIGN..........................   Exe. Name  "+ BusinessAccessLeyer.BAL.StaticVariables.selectuser+"     Given To........................... ";
           // string text = pageN + " - ";
            float len = baseFont.GetWidthPoint(text, 8);

            Rectangle pageSize = document.PageSize;
            pdfContent = writer.DirectContent;
            pdfContent.SetRGBColorFill(100, 100, 100);

           

            pdfContent.BeginText();
            pdfContent.SetFontAndSize(baseFont, 12);
           // pdfContent.ShowTextAligned(PdfContentByte.ALIGN_LEFT, text, pageSize.GetRight(40), pageSize.GetBottom(30), 0);
            pdfContent.ShowTextAligned(PdfContentByte.ALIGN_LEFT, text, pageSize.GetLeft(40) , pageSize.GetBottom(30), 0);
            pdfContent.EndText();


            BusinessAccessLeyer.BAL.StaticVariables.selectuser = "";

          //  string path = @"D:\Dropboxdata\Dropbox\mayankmvc\Web_Complete_Inv\PresantationAccessLeyer\Images\Address Img.jpg";

          ////  iTextSharp.text.Image instanceImg = iTextSharp.text.Image.GetInstance(path);

          //  //Image logo = Image.GetInstance(path);
          //  // logo.ScaleAbsolute(500, 300);

            
          //   string imageURL =   path ;  // Server.MapPath(".") + "/image2.jpg";
          //   iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
          //   //Resize image depend upon your need
          //   jpg.ScaleToFit(240f, 120f);
          //   //Give space before image
          //   jpg.SpacingBefore = 10f;
          //   //Give some space after the image
          //   jpg.SpacingAfter = 1f;
          //   //jpg.Alignment = Element.ALIGN_LEFT;
          //   jpg.Alignment =  Element.ALIGN_CENTER;             
          //   document.Add(jpg);

            ////////////////////////////////////
          
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            pageNumberTemplate.BeginText();
            pageNumberTemplate.SetFontAndSize(baseFont, 8);
            pageNumberTemplate.SetTextMatrix(0, 0);
            pageNumberTemplate.ShowText(string.Empty + (writer.PageNumber - 1));
            pageNumberTemplate.EndText();
        }
    }
}