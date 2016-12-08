<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountDetail.aspx.cs" Inherits="CHub.AccountDetail" 
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
                        <td width="20%" style="padding: 4px;">
                            Login Namesss
                        </td>
                        <td style="padding: 4px;">
                            <asp:Label ID="lblLoginName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px;">
                            Password
                        </td>
                        <td style="padding: 4px;">
                            <asp:Label ID="lblPassword" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF; display: none;">
                        <td style="padding: 4px;">
                            User Name(EN)
                        </td>
                        <td style="padding: 4px;">
                            <asp:Label ID="lblUserNameEN" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF; display: none;">
                        <td style="padding: 4px;" nowrap="nowrap">
                            User Name(CN)
                        </td>
                        <td style="padding: 4px;">
                            <asp:Label ID="lblUserNameCN" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px;">
                            Gender
                        </td>
                        <td style="padding: 4px;">
                            <asp:Label ID="lblGender" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px;">
                            Email
                        </td>
                        <td style="padding: 4px;">
                            <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px;">
                            Region
                        </td>
                        <td style="padding: 4px;">
                            <asp:Label ID="lblRegion" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px;">
                            Duty
                        </td>
                        <td style="padding: 4px;">
                            <asp:Label ID="lblDutyName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;" id="trSystemLimited" runat="server">
                        <td style="padding: 4px;" valign="top">
                            System Limited
                        </td>
                        <td style="padding: 4px;">
                            <uc1:TreeViewEx ID="tveSystemLimited" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
