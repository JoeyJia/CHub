<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DriveOptionsDetail.aspx.cs" Inherits="CHub.DriveOptionsDetail" 
    MasterPageFile="~/Main.Master" %>

<%@ Register Src="~/ControlLibrary/TreeViewEx.ascx" TagName="TreeViewEx"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="UI/Js/TreeViewEx.js" charset="gb2312"></script>

    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="padding-bottom: 4px;" align="right">
                <asp:LinkButton ID="lnkbtnModify" runat="server" CssClass="link" OnClick="lnkbtnModify_Click">Modify</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 4px;" nowrap="nowrap">
                <table cellpadding="0" cellspacing="1" border="0" width="100%" style="background-color: #CCCCCC;">
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px;">MLFB
                        </td>
                        <td style="padding: 4px;">
                            <asp:Label ID="lblMLFB" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px;">Options
                        </td>
                        <td style="padding: 4px;">
                            <asp:Label ID="lblOptions" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px;" nowrap="nowrap">Price
                        </td>
                        <td style="padding: 4px;">
                            <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
