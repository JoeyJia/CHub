<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SPRImport.aspx.cs" Inherits="CHub.SPRImport" 
    MasterPageFile="~/Main.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="2" id="tblUpLoad" runat="server">
        <tr>
            <td colspan="2" style="padding-bottom:14px; font-size:12px; line-height:20px; text-align:right;">
                (Template:<a href="Document/Excel/SPR.xlsx" class="active" target="_blank">click download template</a>)
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap" style="padding-bottom:14px; padding-right:4px;">
                选取上传Excel
            </td>
            <td width="99%" style="padding-bottom:14px;">
                <asp:FileUpload ID="fuMain" runat="server" Width="400" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-bottom:14px;">
                <asp:Label ID="lblName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-bottom:4px;">
                <asp:Button ID="Button1" runat="server" Text="UpLoad" CssClass="idio_FlatBtnOnMouseOut"
                    OnClick="btnUpload_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-bottom:4px;">
                <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
