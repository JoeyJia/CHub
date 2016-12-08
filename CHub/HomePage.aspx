<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="CHub.HomePage"
    MasterPageFile="~/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="padding-bottom: 4px;" id="tdWelcome" runat="server" colspan="2"></td>
        </tr>
        <tr>
            <td style="width:300px !important;">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="padding-top: 4px; padding-bottom: 8px;">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%" id="tableLoginUser"
                                runat="server">
                                <tr>
                                    <td style="padding-bottom: 4px;">
                                        <strong>Login User Management</strong>
                                    </td>
                                </tr>
                                <tr id="trLoginUserOperation" runat="server">
                                    <td style="padding-bottom: 4px;">
                                        <a href="AccountOperation.aspx" class="link">Add New</a>
                                    </td>
                                </tr>
                                <tr id="trLoginUserList" runat="server">
                                    <td style="padding-bottom: 14px;">
                                        <a href="AccountDefault.aspx" class="link">User List</a>
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%" id="tableSPR"
                                runat="server">
                                <tr>
                                    <td style="padding-bottom: 4px;">
                                        <strong>WorkSpace</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-bottom: 4px;">
                                        <a href="OrderEntry.aspx" class="link">Quick Order Entry</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-bottom: 14px;">
                                        <a href="OrderDefault.aspx" class="link">Order List</a>
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%" id="tableOptions"
                                runat="server">
                                <tr>
                                    <td style="padding-bottom: 4px;">
                                        <strong>Motor Options Management</strong>
                                    </td>
                                </tr>
                                <tr id="trOptionsOperation" runat="server">
                                    <td style="padding-bottom: 4px;">
                                        <a href="OptionsOperation.aspx" class="link">Add Motor Options</a>
                                    </td>
                                </tr>
                                <tr id="trOptionsList" runat="server">
                                    <td style="padding-bottom: 14px;">
                                        <a href="OptionsDefault.aspx" class="link">Motor Options List</a>
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%" id="tableDriveOptions"
                                runat="server">
                                <tr>
                                    <td style="padding-bottom: 4px;">
                                        <strong>Drive Options Management</strong>
                                    </td>
                                </tr>
                                <tr id="trDriveOptionsOperation" runat="server">
                                    <td style="padding-bottom: 4px;">
                                        <a href="DriveOptionsOperation.aspx" class="link">Add Drive Options</a>
                                    </td>
                                </tr>
                                <tr id="trDriveOptionsList" runat="server">
                                    <td style="padding-bottom: 14px;">
                                        <a href="DriveOptionsDefault.aspx" class="link">Drive Options List</a>
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%" id="tableKPDiscount"
                                runat="server">
                                <tr>
                                    <td style="padding-bottom: 4px;">
                                        <strong>KP Discount Management</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-bottom: 14px;">
                                        <a href="KPOperation.aspx" class="link">Motor KP Discount</a>
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td style="padding-bottom: 4px;">
                                        <strong>Products Quotation</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-bottom: 4px;">
                                        <a href="ProductsQuotation.aspx?Type=Paste" class="link">Motor Products Quotation(Copy/Paste)</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-bottom: 4px;">
                                        <a href="QuickOrderEntry.aspx?Type=Paste" class="link">Drive Products Quotation(Copy/Paste)</a>
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td style="padding-bottom: 4px;">
                                        <a href="MyInformation.aspx" class="link">My Information</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-bottom: 4px;">
                                        <a href="ExitSystem.aspx" class="link">Log Off</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                &nbsp;
                            </p>
                            <p>
                                &nbsp;
                            </p>
                            <p>
                                &nbsp;
                            </p>
                        </td>
                    </tr>
                </table>
            </td>
            <td  style="vertical-align:top; width:600px !important;">
                <div style="font:3000;  font-size:20pt; text-align:center; padding:30px;">
                   Welcome Messages
                </div>	
                <div style="padding:30px;">
                  <p>Recent Pages</p>
                    <table style=" border:dashed;width:700px; ">
                        <tr>
                            <td></td>
                            <td></td>
                  
                                    

                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                            <tr>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
              
                    </table>
                      
                </div>
                <div style="padding:30px;">
                    <table style="padding:30px;">
                        <tr>
                            <td>New Screen:</td>
                            <td><input type="text" title="Go!" value="Go!" /></td>
                            <td style="padding-left:10px;"><input type="button" title="Go!" value="Go!" /></td>
                        </tr>
                        <tr>
                            <td colspan="3">NOTE:	Input ? For the List</td>
                        </tr>
                    </table>	
                </div>
                <div style="padding:30px;">
                  <p>Notice</p>
                    <table style=" border:dashed; width:700px;">
                        <tr>
                            <td>Notice No</td>
                            <td>Topic</td>
                            <td>Content</td>
                            <td>Publisher</td>

                                    

                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                            <tr>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
              
                    </table>
                      
                </div>		
            </td>
        </tr>
    </table>
</asp:Content>
