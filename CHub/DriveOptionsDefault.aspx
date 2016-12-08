<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DriveOptionsDefault.aspx.cs" Inherits="CHub.DriveOptionsDefault" 
MasterPageFile="~/Main.Master" %>

<%@ Register Src="~/ControlLibrary/SearchGridList.ascx" TagName="SearchGridList" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="padding-bottom: 4px;" nowrap="nowrap">
                <uc1:SearchGridList ID="SearchGridList1" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
