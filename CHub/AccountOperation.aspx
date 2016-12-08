<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountOperation.aspx.cs" Inherits="CHub.AccountOperation" 
    MasterPageFile="~/Main.Master" %>

<%@ Register Src="~/ControlLibrary/TreeViewEx.ascx" TagName="TreeViewEx" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="UI/Js/TreeViewEx.js" charset="gb2312"></script>

    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="padding-bottom: 4px;" nowrap="nowrap">
                <table cellpadding="0" cellspacing="1" border="0" width="100%" style="background-color: #CCCCCC;">
                    <tr style="background-color: #FFFFFF;">
                        <td width="20%" style="padding: 4px;">
                            Login Name
                        </td>
                        <td style="padding: 4px;">
                            <input id="txtLoginName" type="text" runat="server" maxlength="30" class="idio_TextAreaNormalStyleNoWH" />
                            <span class="FontRed">*</span>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;" id="trPassword" runat="server">
                        <td width="20%" style="padding: 4px;">
                            Password
                        </td>
                        <td style="padding: 4px;">
                            <input id="txtPassword" type="text" runat="server" maxlength="20" class="idio_TextAreaNormalStyleNoWH" />
                            <span class="FontRed">*</span>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF; display: none;">
                        <td style="padding: 4px;">
                            User Name(EN)
                        </td>
                        <td style="padding: 4px;">
                            <input id="txtUserNameEN" type="text" runat="server" maxlength="50" class="idio_TextAreaNormalStyleNoWH" />
                            <span class="FontRed">*</span>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF; display: none;">
                        <td style="padding: 4px;" nowrap="nowrap">
                            User Name(CN)
                        </td>
                        <td style="padding: 4px;">
                            <input id="txtUserNameCN" type="text" runat="server" maxlength="50" class="idio_TextAreaNormalStyleNoWH" />
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px;">
                            Gender
                        </td>
                        <td style="padding: 4px;">
                            <select id="cboGender" runat="server" class="idio_TextAreaNormalStyleNoWH">
                                <option value="Male">Male</option>
                                <option value="Female">Female</option>
                            </select>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px;">
                            Email
                        </td>
                        <td style="padding: 4px;">
                            <input id="txtEmail" type="text" runat="server" maxlength="100" class="idio_TextAreaNormalStyleNoWH"
                                style="width: 200px;" />
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px;">
                            Region
                        </td>
                        <td style="padding: 4px;">
                            <select id="cboRegionName" runat="server" class="idio_TextAreaNormalStyleNoWH">
                            </select>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px;">
                            Duty
                        </td>
                        <td style="padding: 4px;">
                            <select id="cboDutyID" runat="server" class="idio_TextAreaNormalStyleNoWH">
                            </select>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px;" valign="top">
                            System Limited
                        </td>
                        <td style="padding: 4px;">
                            <uc1:TreeViewEx ID="tveSystemLimited" runat="server" />
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
