<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderEntry.aspx.cs" Inherits="CHub.OrderEntry"
    MasterPageFile="~/Main.Master" %>

<%@ Register Src="~/ControlLibrary/TreeViewEx.ascx" TagName="TreeViewEx"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="UI/Js/TreeViewEx.js" charset="gb2312"></script>
   <style type="text/css">
        td 
        {
            padding:4px;
        }
    </style>

            <table style="width:900px;">
                <tr>
                    <td style="width:150px;">Customer Alias:</td>
                    <td>
                          <asp:DropDownList ID="DropDownCustomerAlias" runat="server">
                            <asp:ListItem Text="Customer Alias" Value="Customer Alias"></asp:ListItem>
                            <asp:ListItem Text="Customer Alias" Value="Customer Alias"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    
                    <td style="width:150px;">Customer PO No:</td>
                    <td>
                        <input type="text" runat="server" id="TextCustomerPONo"/>
                    </td>
                </tr>
                <tr>
                    <td>Order Type:</td>
                    <td>
                        <asp:DropDownList ID="DropDownOrderType" runat="server">
                            <asp:ListItem Text="STOCK" Value="STOCK "></asp:ListItem>
                            <asp:ListItem Text="Type2" Value="test1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td></td>
                    
                </tr>
                <tr>
                    <td>Special Ship:</td>
                    <td>
                        <input type="checkbox" name="SpecialShip" id="CheckboxSpecialShip"  runat="server"  checked="checked"/>
                    </td>

                </tr>
                <tr>
                    <td>Ship Name:</td>
                    <td><input type="text" runat="server" id="InputShipName"  /></td>
                     <td>Address:</td>
                    <td><input type="text" runat="server" id="InputAddress" /></td>
                    <td><input type="button" runat="server" id="ButtonSearch" title="Search" value="Search" /></td>
                </tr>

            </table>
           
                <table>
                    <tr>
                        <td style="vertical-align:top; padding-top:25px; width:100px;">
                                 <table>
                                     <tr>
                                        <td>Primary Ship From:</td>
                                    </tr>
                                </table>
                        </td>
                        <td>
                          
                                <table runat="server" id="Table1">
                                    <tr>
                                         <td style="width:150px;">Customer PO No:</td>
                                         <td><input type="text" runat="server" id="Text1"/></td>
                                    </tr>
                                     <tr>
                                        <td><input type="radio" id="Radio2" runat="server" name="radiobutton" value="radiobutton"/></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td><input type="radio" name="radiobutton" value="radiobutton"/></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td><input type="radio" name="radiobutton" value="radiobutton"/></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td><input type="radio" name="radiobutton" value="radiobutton"/></td>
                                        <td></td>
                                    </tr>

                                </table>
                                
                                    <table>
                                        <tr>
                                            <td><input type="button" runat="server" id="Button1" title="Save" onclick="javascript: return Save();"  value="Save" /></td>
                                            <td><input type="button" runat="server" id="Button2" title="Download" value="Download" /></td>
                                            <td><input type="button" runat="server" id="Button3" title="Cancel" value="Cancel" /></td>
                                            <td><input type="button" runat="server" id="Button4" title="Auto Fillfulemts" value="Auto Fillfulemts" /></td>
                                        </tr>
                                    </table>
                                
                        </td>
                    </tr>

                </table>    

                <table>
                    <tr>
                        <td style="vertical-align:top; padding-top:25px; width:100px;">
                                 <table>
                                     <tr>
                                        <td>Alternative Ship from:</td>
                                    </tr>
                                </table>
                        </td>
                        <td>
                          
                                <table style=" border:dashed;" runat="server" id="TableList">
                                    <tr>
                                        <td></td>
                                        <td>warehouse</td>
                                        <td>Customer_no</td>
                                        <td>SPL_no</td>
                                        <td>SYSID</td>
                                        <td>DEST_ATTENTION</td>
                                        <td>Local_dest_name</td>
                                        <td>Local_dest_addr_1</td>
                                        <td>Local_dest_addr_2</td>
                                        <td>Local_dest_addr_3</td>
                                        <td>Local_CITY</td>
                                        <td>DEST_LOCSTION</td>
                                        <td>DEST_NAME</td>
                                        <td>DEST_ADDR_1</td>
                                    

                                    </tr>
                                    <tr>
                                        <td><input type="radio" id="sss"  runat="server" name="radiobutton" value="radiobutton"/></td>
                                        <td></td>
                                    </tr>
                                     <tr>
                                        <td><input type="radio" id="ddd" runat="server" name="radiobutton" value="radiobutton"/></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td><input type="radio" name="radiobutton" value="radiobutton"/></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td><input type="radio" name="radiobutton" value="radiobutton"/></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td><input type="radio" name="radiobutton" value="radiobutton"/></td>
                                        <td></td>
                                    </tr>

                                </table>
                                
                                    <table>
                                        <tr>
                                            <td><input type="button" runat="server" id="ButtonSave" title="Save" onclick="javascript: return Save();"  value="Save" /></td>
                                            <td><input type="button" runat="server" id="ButtonDownload" title="Download" value="Download" /></td>
                                            <td><input type="button" runat="server" id="ButtonCancel" title="Cancel" value="Cancel" /></td>
                                            <td><input type="button" runat="server" id="ButtonAutoFillfulemts" title="Auto Fillfulemts" value="Auto Fillfulemts" /></td>
                                        </tr>
                                    </table>
                                
                        </td>
                    </tr>

                </table>
                

          

       
                <div>Order Lines</div>

                <table style=" border:dashed;">

                    <tr>
                        <td>SEQ</td>
                        <td>Customer Part</td>
                        <td>Part_No</td>
                        <td>Qty</td>
                        <td>AVL Check (Primary)</td>
                        <td>Alternative</td>
                        <td>&nbsp; </td>
                    </tr>
                    <tr>
                        <td>1</td>
                        <td><input type="text"  /></td>
                        <td><input type="text"  /></td>
                        <td><input type="text"  /></td>
                        <td><input type="text"  /></td>
                        <td><input type="text"  /></td>
                        <td><input type="button" title="Delete" value="Delete" /></td>
                    </tr>
                    <tr>
                        <td>2</td>
                        <td><input type="text"  /></td>
                        <td><input type="text"  /></td>
                        <td><input type="text"  /></td>
                        <td><input type="text"  /></td>
                        <td><input type="text"  /></td>
                        <td><input type="button" title="Delete" value="Delete" /></td>
                    </tr>
                     <tr>
                        <td>3</td>
                        <td><input type="text"  /></td>
                        <td><input type="text"  /></td>
                        <td><input type="text"  /></td>
                        <td><input type="text"  /></td>
                        <td><input type="text"  /></td>
                        <td><input type="button" title="Delete" value="Delete" /></td>
                    </tr>

                </table>
                								
     
 

    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="display:none;">
        <tr>
            <td style="padding-bottom: 4px;" nowrap="nowrap">
                <table cellpadding="0" cellspacing="1" border="0" width="100%" style="background-color: #CCCCCC;">
                    <tr style="background-color: #FFFFFF;">
                        <td width="20%" style="padding: 4px;">SPR No
                        </td>
                        <td style="padding: 4px;">
                            <input id="txtSPRNo" type="text" runat="server" class="idio_TextAreaNormalStyleNoWH" style="width: 400px;" />
                            <span class="FontRed">*</span>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td width="20%" style="padding: 4px;">MLFB
                        </td>
                        <td style="padding: 4px;">
                            <input id="txtMLFB" type="text" runat="server" class="idio_TextAreaNormalStyleNoWH" style="width: 400px;" />
                            <span class="FontRed">*</span>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px; vertical-align: top;">Options
                        </td>
                        <td style="padding: 4px;">
                            <textarea id="txtOptions" runat="Server" style="width: 400px;" rows="12"></textarea>
                            <span class="FontRed">
                                <br />
                                选件价格的格式为：选件,价格，多个选件请使用“+”号隔开！<br />
                                例如：A01,1854+A02,1200+A12+A13,2000<br /></span>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px;" nowrap="nowrap">Voltage
                        </td>
                        <td style="padding: 4px;">
                            <input id="txtVoltage" type="text" runat="server" maxlength="50" class="idio_TextAreaNormalStyleNoWH" onkeypress="return event.keyCode>=48&&event.keyCode<=57||event.keyCode==13||event.keyCode==46" />
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px;" nowrap="nowrap">Quantity
                        </td>
                        <td style="padding: 4px;">
                            <input id="txtQuantity" type="text" runat="server" maxlength="50" class="idio_TextAreaNormalStyleNoWH" onkeypress="return event.keyCode>=48&&event.keyCode<=57||event.keyCode==13||event.keyCode==46" /><span class="FontRed">*</span>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px;">Others Per Unit
                        </td>
                        <td style="padding: 4px;">
                            <input id="txtOthersPerUnit" type="text" runat="server" maxlength="50" class="idio_TextAreaNormalStyleNoWH" onkeypress="return event.keyCode>=48&&event.keyCode<=57||event.keyCode==13||event.keyCode==46" />
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px;">Others Per Item
                        </td>
                        <td style="padding: 4px;">
                            <input id="txtOthersPerItem" type="text" runat="server" maxlength="50" class="idio_TextAreaNormalStyleNoWH" onkeypress="return event.keyCode>=48&&event.keyCode<=57||event.keyCode==13||event.keyCode==46" />
                        </td>
                    </tr>

                    <tr style="background-color: #FFFFFF;">
                        <td colspan="2" style="padding: 4px;" align="center">
                            <asp:Button ID="btnSumbit" runat="server" Text="Submit" CssClass="idio_FlatBtnOnMouseOut"
                                OnClick="btnSumbit_Click" />
                            <input id="btnReset" type="reset" value="Reset" class="idio_FlatBtnOnMouseOut" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>


</asp:Content>
