<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="PresantationAccessLeyer.E.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <style type="text/css">
    /*fix dropdown z-index problem. Credit to http://hedgerwow.blogspot.com/ */
    .select-free
    {
        position: absolute;
        z-index: 10; /*any value*/
        overflow: hidden; /*must have*/
        width: 247px; /*must have for any value*/ /*width of area +5*/
    }
    .select-free iframe
    {
        display: none; /*sorry for IE5*/
        display: /**/ block; /*sorry for IE5*/
        position: absolute; /*must have*/
        top: 0px; /*must have*/
        left: 0px; /*must have*/
        z-index: -1; /*must have*/
        filter: mask(); /*must have*/
        width: 3000px; /*must have for any big value*/
        height: 3000px /*must have for any big value*/;
    }
    .select-free .bd
    {
        padding: 11px;
    }
</style>

    <script language="javascript" type="text/javascript">
        function DropDownTextToBox(objDropdown, strTextboxId) {
            document.getElementById(strTextboxId).value = objDropdown.options[objDropdown.selectedIndex].value;
            DropDownIndexClear(objDropdown.id);
            document.getElementById(strTextboxId).focus();
        }
        function DropDownIndexClear(strDropdownId) {
            if (document.getElementById(strDropdownId) != null) {
                document.getElementById(strDropdownId).selectedIndex = -1;
            }
        }
</script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div style="width:100%; height:100%;">
    <table border="2" style="width:100%;">
    <tr><td style = "width:10%">
        Select Country</td>
        <td style = "width:10%">
        <asp:DropDownList ID="ddlcountry" runat="server" Width = "50%" AutoPostBack = "true"
            onselectedindexchanged="ddlcountry_SelectedIndexChanged">
            <asp:ListItem Value="-1">SELECT</asp:ListItem>
        </asp:DropDownList>
        </td>
        <td style = "width:8%">Select Tariff</td>
        <td style = "width:15%">
        
             <asp:DropDownList ID="ddltariff" runat="server" Width = "50%" AutoPostBack = "true"
                 onselectedindexchanged="ddltariff_SelectedIndexChanged">
                 <asp:ListItem Value="-1">SELECT</asp:ListItem>
             </asp:DropDownList>
         </td>
         
        
        </tr>
        <tr>
          <td style = "width:8%">Data Pack</td>
        <td style = "width:20%"> 
       <div style="position: relative;">
   
     <asp:TextBox name="TextboxExample1" type="text" maxlength="50" id="tctdatapname" tabindex="2" runat ="server"
        onchange="DropDownIndexClear('DropDownExTextboxExample1');" style="width: 15%;
        position: absolute; top: 0px; left: 0px; z-index: 2;" />

    <asp:DropDownList name="DropDownExTextboxExample1" id="DropDownExTextboxExample1" 
               tabindex="1000" runat ="server"
        onchange="DropDownTextToBox(this,'tctdatapname');" style="position: absolute;
        top: 0px; left: 0px; z-index: 1; width: 50%; right: 194px;">
        <asp:ListItem Value="-1">SELECT</asp:ListItem>
     
    </asp:DropDownList>

    <script language="javascript" type="text/javascript">

        DropDownIndexClear("DropDownExTextboxExample");
    </script>
    <br />
</div>
                
            
                 </td>

                   <td style = "width:14%">
                 <div style="position: relative; top: 0px; left: 0px;">
   
     <asp:TextBox name="TextboxExample" type="text" maxlength="50" id="TextboxExample" tabindex="2" runat ="server"
        onchange="DropDownIndexClear('DropDownExTextboxExample');" style="width: 35%;
        position: absolute; top: 0px; left: 0px; z-index: 2; right: 178px;" />

    <asp:DropDownList name="DropDownExTextboxExample" id="DropDownExTextboxExample" tabindex="1000" runat ="server"
        onchange="DropDownTextToBox(this,'TextboxExample');" style="position: absolute;
        top: 3px; left: 0px; z-index: 1; width:42%; height: 13px; margin-top: 0px;" 
                         onselectedindexchanged="DropDownExTextboxExample_SelectedIndexChanged1">
        <asp:ListItem Value="-1">SELECT</asp:ListItem>
     
    </asp:DropDownList>
    <script language="javascript" type="text/javascript">

        DropDownIndexClear("DropDownExTextboxExample");
    </script>
    <br />
</div>
               
             
         </td>
         <td>
     <%--<asp:Label Text=   <% decimal a = Convert.ToDecimal(TextboxExample.Text) + Convert.ToDecimal(TextboxExample.Text); %></asp:Label >--%>
    <%--    <asp:Label runat="server" Text='<% decimal a = Convert.ToDecimal(TextboxExample.Text) + Convert.ToDecimal(TextboxExample.Text); %>'></asp:Label>--%>
        
        </td>
         
        
        
        </tr>
        <tr>
        <td style = "width:10%">BOLTONS</td>
         <td style = "width:10%">
         <asp:DropDownList ID="DropDownListBoltion" runat="server" Width="50%"  AutoPostBack="true"
                 onselectedindexchanged="DropDownListBoltion_SelectedIndexChanged" >
             <asp:ListItem Value="-1">SELECT</asp:ListItem>
            </asp:DropDownList>
         
         </td>
         <td>

               <asp:TextBox ID="TextBox1" runat="server" Width="210"></asp:TextBox>

         </td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Enter" onclick="Button1_Click" />
        
        
        </td>
        
        
        </tr>

        <tr>
        <td>
      NET  DATA PACK
        
        </td>
        <td>
        <asp:TextBox ID="TextBox23" runat="server"  Width="50%" ReadOnly="true" 
                Visible="False"></asp:TextBox>
<asp:Label ID="Label2" runat="server" Text="Label" Visible="False" ForeColor="Red"></asp:Label>
        </td>
        <td>
            
        
        
            <asp:Button ID="Button3" runat="server" Text="Reference" />
        </td>
        </tr>
        </table>
        <br />
    <asp:GridView ID="GridView1" Width="100%"  ShowFooter = "true"  AutoGenerateColumns = "False" runat="server" 
        onrowcommand="GridView1_RowCommand" 
        onrowdatabound="GridView1_RowDataBound" BackColor="White" BorderColor="#CCCCCC" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3">
   
    <Columns>
    
      <%--<asp:TemplateField visible='<% #Eval("ORIGIN").ToString()!=""? true:false %>' >--%>
      <asp:TemplateField  ItemStyle-CssClass="hideGridColumn" HeaderStyle-CssClass="hideGridColumn">
      
    <HeaderTemplate >ORIGIN</HeaderTemplate>
    <ItemTemplate>
        <asp:Label ID="LabelORIGIN"  runat="server"  Text='<% #BIND("ORIGIN") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
       <asp:TemplateField>
    <HeaderTemplate>DAYDATE</HeaderTemplate>
    <ItemTemplate>
        <asp:Label ID="LabelDAYDATE" runat="server" Text='<% #BIND("DAYDATE") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField>
    <HeaderTemplate>DATETIME</HeaderTemplate>
    <ItemTemplate>
        <asp:Label ID="lbldtime" runat="server" Text='<% #BIND("DATETIME") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>

    <asp:TemplateField>
    <HeaderTemplate>DATE</HeaderTemplate>
    <ItemTemplate>
     <%--   <asp:Label ID="lbldtime" runat="server" Text='<% #BIND("DATETIME") %>'></asp:Label>  --%>
        <asp:Label ID="LabelDATE" runat="server" Text='<% #BIND("DATE") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>

<asp:TemplateField><HeaderTemplate>
TIME
</HeaderTemplate>
<ItemTemplate>
       <%-- <asp:Label ID="lblnum" runat="server" Text='<% #BIND("NUMBER") %>'></asp:Label>--%>
     <asp:Label ID="LabelTime" runat="server" Text='<% #BIND("TIME") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField><HeaderTemplate>
NUMBER
</HeaderTemplate>
<ItemTemplate>
<%--        <asp:Label ID="lbltype" runat="server" Text='<% #BIND("TYPE") %>'></asp:Label>--%>
     <asp:Label ID="lblnum" runat="server" Text='<% #BIND("NUMBER") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField><HeaderTemplate>
DURATION
</HeaderTemplate>
<ItemTemplate>
        <asp:Label ID="lbldur" runat="server" Text='<% #BIND("DURATION") %>'></asp:Label>
    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField><HeaderTemplate>
UNITS
</HeaderTemplate>
<ItemTemplate>
     <asp:Label ID="lblunits" runat="server" Text='<% #BIND("UNITS") %>'></asp:Label>
       
    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField><HeaderTemplate>
RATE
</HeaderTemplate>
<ItemTemplate>
     <asp:UpdatePanel runat="server" ID="UpId" 
    UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
        <asp:TextBox ID="txtrate" OnTextChanged="TxtId_TextChanged" runat="server" 
            AutoPostBack="true" Text = '<% #BIND("RATE") %>'></asp:TextBox>
        </ContentTemplate>
        </asp:UpdatePanel>
    
</ItemTemplate>
<FooterTemplate>
GRAND TOTAL
</FooterTemplate>
</asp:TemplateField>
<asp:TemplateField><HeaderTemplate>
COST
</HeaderTemplate>
<ItemTemplate>
        <asp:Label  ID="lbldcost" runat="server" Text='<% #BIND("COST") %>'></asp:Label>
    
</ItemTemplate>
<FooterTemplate>
    <asp:TextBox ID="txtgrandtotal" runat="server"></asp:TextBox>
</FooterTemplate>
</asp:TemplateField>
    </Columns>


    
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="Wheat" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />
    </asp:GridView>
   
    <br />
   <%-- <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />--%>
    
        
    <asp:Panel id = "btdiv" align="center" runat = "server"> <asp:Button ID="Button2" runat="server" Text="Apply change" 
        onclick="Button2_Click" style= "width:20% ; border: 1px solid #c4c4c4;padding: 1px 4px 1px 4px;border-radius: 4px; -moz-border-radius: 4px;-webkit-border-radius: 4px; box-shadow: 0px 0px 8px #d9d9d9;"  />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
</asp:Panel>
 </div>
   <%-- <a href = "../Bill/Bill_Index" runat="server" >BACK</a>--%>
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </form>
</body>
</html>
