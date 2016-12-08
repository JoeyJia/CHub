<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HistoryQuotationDetail.aspx.cs" Inherits="CHub.HistoryQuotationDetail"
    MasterPageFile="~/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td colspan="2" align="right">
                <asp:LinkButton ID="lnkbtnDelete" runat="server" CssClass="link" 
                    onclick="lnkbtnDelete_Click" OnClientClick="javascript:return confirm('Are you sure delete?');">Delete</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td style="padding: 4px; width: 15%;" nowrap="nowrap">
                <p>
                    Customer/OEM:</p>
            </td>
            <td style="padding: 4px;">
                <p>
                    <asp:Label ID="lblCustomer" runat="server" Text=""></asp:Label></p>
            </td>
        </tr>
        <tr>
            <td style="padding: 4px;">
                <p>
                    Channel:</p>
            </td>
            <td style="padding: 4px;">
                <p>
                    <asp:Label ID="lblChannel" runat="server" Text=""></asp:Label></p>
            </td>
        </tr>
        <tr>
            <td style="padding: 4px;">
                <p>
                    Project:</p>
            </td>
            <td style="padding: 4px;">
                <p>
                    <asp:Label ID="lblProject" runat="server" Text=""></asp:Label></p>
            </td>
        </tr>
        <tr>
            <td style="padding: 4px;">
                <p>
                    Quotation No.:</p>
            </td>
            <td style="padding: 4px;">
                <p>
                    <asp:Label ID="lblQuotationNo" runat="server" Text=""></asp:Label></p>
            </td>
        </tr>
        <tr>
            <td style="padding: 4px;">
                <p>
                    Sales Region</p>
            </td>
            <td style="padding: 4px;">
                <p>
                    <asp:Label ID="lblSalesRegion" runat="server" Text=""></asp:Label></p>
            </td>
        </tr>
        <tr>
            <td style="padding: 4px;">
                <p>
                    BU Responsible</p>
            </td>
            <td style="padding: 4px;">
                <p>
                    <asp:Label ID="lblBUResponsible" runat="server" Text=""></asp:Label></p>
            </td>
        </tr>
        <tr>
            <td style="padding: 4px;">
                <p>
                    K-Price</p>
            </td>
            <td style="padding: 4px;">
                <p>
                    <asp:Label ID="lblKP" runat="server" Text=""></asp:Label></p>
            </td>
        </tr>
        <tr id="trTP" runat="server">
            <td style="padding: 4px;">
                <p>
                    TP</p>
            </td>
            <td style="padding: 4px;">
                <p>
                    <asp:Label ID="lblTP" runat="server" Text=""></asp:Label></p>
            </td>
        </tr>
        <tr id="trBonusRate" runat="server">
            <td style="padding: 4px;">
                <p>
                    Bonus Rate</p>
            </td>
            <td style="padding: 4px;">
                <p>
                    <asp:Label ID="lblBonusRate" runat="server" Text=""></asp:Label></p>
            </td>
        </tr>
        <tr id="trGrossMargin" runat="server">
            <td style="padding: 4px;">
                <p>
                    Gross Margin</p>
            </td>
            <td style="padding: 4px;">
                <p>
                    <asp:Label ID="lblGrossMargin" runat="server" Text=""></asp:Label></p>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" border="0" width="100%" id="tableList" runat="server">
        <tr>
            <td>
                <div class="GridStyle" id="divList" runat="server" style="font-size: 12px;">
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
