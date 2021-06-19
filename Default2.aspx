<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>


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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="otr"><br />
                <asp:Button ID="Button2" OnClientClick="saveHtml('Panel1')" runat="server" CssClass="btn3" Text="Download" /><br />
               <hr style="height:1px;border:none;color:#333;background-color:#333;" />
            </div>
            <br /><br />
            <asp:Panel ID="Panel1" runat="server">
            <br />

                <div id="wrapper">
                    <div id="column_container">
                        <div class="column1"><asp:Image ID="Image1" CssClass="logo" runat="server" /></div>
                        <div class="column2"><h1>GENERIC COMPANY</h1></div>
                    </div>
               </div><br />
              
                <asp:GridView ID="GridView1" Align="center" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,Group">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="150">
                            <HeaderStyle BackColor="#99CCFF" />
                            <ItemStyle Width="150px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Country" HeaderText="Country" ItemStyle-Width="100">
                            <HeaderStyle BackColor="#99CCFF" />
                            <ItemStyle Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="Button1" CssClass="btn3" Text="Get Value" runat="server" OnClick="GridView_Button_Click" ForeColor="White" />
                            </ItemTemplate>
                            <HeaderStyle BackColor="#99CCFF" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            <br />
            <br />
            <br />
            <div class="otr">
               <div class="inr"> 
                     RowId: <asp:Label ID="Label1" runat="server"></asp:Label><br />
                     Group: &nbsp;<asp:Label ID="Label2" runat="server"></asp:Label>
                     <br />
                     <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                </div>
            </div>
            <br /></asp:Panel>
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" style="display:none" Text="Button" />

           <script language="javascript" type="text/javascript">
               
               function saveHtml(divID) {
                   //Get the HTML of div
                   var divElements = document.getElementById(divID).outerHTML;
                   //Get the HTML of whole page
                   var oldPage = document.body.innerHTML;

                   //Reset the page's HTML with div's HTML only
                   var htmlOutput =
                       "<body>" +
                       divElements + "</body>";

                   var encodedHtml = encodeURI(htmlOutput)

                   document.getElementById("HiddenField1").value = encodedHtml;
                   document.getElementById("Button3").click();
                   //Restore orignal HTML
                   document.body.innerHTML = oldPage;

               }

           </script>
            <asp:HiddenField ID="HiddenField1" runat="server" />

    
        </div>
    </form>
</body>
</html>
