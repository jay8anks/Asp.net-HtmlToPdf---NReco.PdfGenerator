using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NReco.PdfGenerator;

public partial class Default2 : System.Web.UI.Page
{
    private const int NumberOfRetries = 3;
    private const string orderId = "45896";
    private const int DelayOnRetry = 1000;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetImageUrl();
            BindGridView();
        }
    }

    protected void SetImageUrl()
    {
        // The url has to be a full URL, it can't be a relative path.

        Image1.ImageUrl = "http://localhost:51034/img/generic.png";

        // If you are using the same code on different sites, you can add an
        // app setting in the web.config for the url. Exampe:
        // <add key="Url" value="http://localhost:57005/" />.
        // You can then access it like this:
        // Image1.ImageUrl = ConfigurationManager.AppSettings["Url"].ToString() + "/img/generic.png";
        // The same code will work on multiple sites this way without having to remember
        // to change the URL for each site. Not as easy as a relative URL, but better
        // than a hard-coded URL.
    }

    protected void BindGridView()
    {
        DataTable dt = SampleDataTable();

        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void GridView_Button_Click(object sender, EventArgs e)
    {
        //Determine the RowIndex of the Row whose Button was clicked.
        int rowIndex = ((sender as Button).NamingContainer as GridViewRow).RowIndex;

        //Get the value of column from the DataKeys using the RowIndex.
        int id = Convert.ToInt32(GridView1.DataKeys[rowIndex].Values[0]);
        string group = GridView1.DataKeys[rowIndex].Values[1].ToString();

        Label1.Text = id.ToString();
        Label2.Text = group;

    }


    protected DataTable SampleDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Id"), new DataColumn("Group"), new DataColumn("Name"), new DataColumn("Country") });
        dt.Rows.Add(1, "A", "John Hammond", "United States");
        dt.Rows.Add(2, "B", "Mudassar Khan", "India");
        dt.Rows.Add(3, "A", "Suzanne Mathews", "France");
        dt.Rows.Add(4, "B", "Robert Schidner", "Russia");
        dt.Rows.Add(5, "B", "Dale Roberts", "Russia");
        dt.Rows.Add(6, "C", "Bena Mudra", "India");
        dt.Rows.Add(7, "B", "Galilahi Fala", "India");
        dt.Rows.Add(8, "C", "Una Kanti", "India");

        return dt;
    }



    protected void Button3_Click(object sender, EventArgs e)
    {
        SaveHtmlToPdf();
        DownloadPdf();
    }

    protected void SaveHtmlToPdf()
    {
       
        string htmlOutput = Server.UrlDecode(HiddenField1.Value);     
        
        htmlOutput = string.Join(" ", System.Text.RegularExpressions.Regex.Split(htmlOutput, @"(?:\r\n|\n|\r|\t)"));
        htmlOutput = htmlOutput.Replace("\"", "'");

        string headerStyle = HeaderStyle();

        string finalHtml = headerStyle + htmlOutput;

        var strWr = new StringWriter();
        var htmlWr = new HtmlTextWriter(strWr);
        // base.Render(htmlWr);
        var htmlToPdf = new HtmlToPdfConverter();

        string filename = (orderId + ".pdf");
        string filepath = "~/sales_pdfs";
        string combinedFilePath = Path.Combine(Server.MapPath(filepath), filename).ToString();

        for (int i = 1; i <= NumberOfRetries; ++i)
        {
            try
            {
                htmlToPdf.GeneratePdf(finalHtml, null, combinedFilePath);
                
                break; // When done we can break loop
            }
            catch (IOException e)
            {
                // You may check error code to filter some exceptions, not every error
                // can be recovered.
                if (i <= NumberOfRetries)
                {
                    Thread.Sleep(DelayOnRetry);
                }

                ltMsg.Text = "There was a problem creating the PDF";

            }

        }

    }

    protected void DownloadPdf()
    {       

        HttpContext.Current.Response.ContentType = "Application/pdf";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + orderId + ".pdf");
        HttpContext.Current.Response.TransmitFile(Server.MapPath("~/sales_pdfs/" + orderId + ".pdf"));
        Response.End();    
        // see: https://blogs.msdn.microsoft.com/tmarq/2009/06/25/correct-use-of-system-web-httpresponse-redirect/)
    }


    protected string HeaderStyle()
    {
        string headerStyle = @"<!DOCTYPE html>

       <html xmlns='http://www.w3.org/1999/xhtml'>
       <head runat='server'>
    <title></title>
    <style>

         /*This is added to scale the html. The generated PDF 
        looks a little big and blocky compared to the same
        view in the browser. This can be removed or adjusted  */


        html  {
        zoom: 70%;
       }

        .otr {
            text-align: center;
        }

        .inr {
            width: 90%;
        }

        .btn3 {
            background-color: #666699; /* Green */
            color: white;
            text-align: center;
            text-decoration: none;
            font-size: 15px;
            cursor: pointer;
            transition-duration: 0.4s;
            border: none;
        }

            .btn3:hover {
                box-shadow: 0 4px 6px 0 rgba(0,0,0,0.17),0 6px 8px 0 rgba(0,0,0,0.18);
                background-color: #9999FF;
            }

        #wrapper {
            width: 100%;
            display: inline-block;
        }

        #column_container {
            margin: 0 auto;
        }

        .column1 {
            text-align: right;
            float: left;
            width: 40%;
        }

        .column2 {
            float: left;
            margin: 10px 0px 10px 0px;
            padding: 10px;
            width: 439px;
            display: inline;
            position: relative;
        }
        

        .logo {
            
            width: 107px;
            height: 80px;
        }

        h1 {
            
            font-size: 30pt;
            font-weight: 600;
            font-family: arial,helvetica;              
            text-decoration: none;
            border: 0;
            margin: 0;
            padding: 0;
            margin-bottom:15px;
            
        }


             </style>
            </head>";

        return headerStyle;
    }


    
}