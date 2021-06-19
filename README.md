# Asp.Net-HtmlToPdf-download
Grabs all the HTML inside an Asp.Net Panel control (or Div) And creates a PDF

Note: See Default2.aspx.

A simple Asp.Net website based on the project: https://github.com/jay8anks/Asp.Net-Panel-Control-to-HTML-download

Most of the code is the same, except the HTML is converted to a PDF file using NReco.PdfGenerator (A wrappter for wkhtmltopdf).

View the above projects readme file for more details.

There is a sample PDF created using the this code in the "sales_pdfs" directory.

The only change that needs to be made is the image URL for the Generic Company logo image. 

CSS works using NReco.PdfGenerator, but you can't use any relative URLs or paths. Change the logo image URL in the codebehind to the actual URL and you should get a nice PDF of the HTML, CSS formatting, images and all.

I've used Essential Objects EO.Pdf for several years and it works very well. It's just a bit on the expensive side.

NReco.PdfGenerator is very similar in ease of use, and cheaper for commercial use.

The only thing I noticed between the two products is that NReco.PdfGenerator PDFs look a little big and blocky compared to the actual HTML as viewed in a browser (or PDFs generated using EO.Pdf). To get around this issue, I added an HTML zoom tag to reduce the size a little bit. Outside of the image URL, this is the one other setting that may need to be adjusted:

   html  {
        zoom: 70%;
       }
       
Note: The PDF CSS is in the code behind, not the .aspx page.



https://www.nrecosite.com/pdf_generator_net.aspx















