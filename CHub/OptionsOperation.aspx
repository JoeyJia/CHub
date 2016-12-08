<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OptionsOperation.aspx.cs" Inherits="CHub.OptionsOperation" 
    MasterPageFile="~/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="UI/Js/TreeViewEx.js" charset="gb2312"></script>

    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="padding-bottom: 4px;" nowrap="nowrap">
                <table cellpadding="0" cellspacing="1" border="0" width="100%" style="background-color: #CCCCCC;">
                    <tr style="background-color: #FFFFFF;">
                        <td width="20%" style="padding: 4px;">
                            MLFB
                        </td>
                        <td style="padding: 4px;">
                            <input id="txtMLFB" type="text" runat="server" class="idio_TextAreaNormalStyleNoWH" />
                            <span class="FontRed">*</span>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px;">
                            Options
                        </td>
                        <td style="padding: 4px;">
                            <input id="txtOptions" type="text" runat="server" class="idio_TextAreaNormalStyleNoWH" />
                            <span class="FontRed">*</span>
                        </td>
                    </tr>
                    <tr style="background-color: #FFFFFF;">
                        <td style="padding: 4px;" nowrap="nowrap">
                            Price
                        </td>
                        <td style="padding: 4px;">
                            <input id="txtPrice" type="text" runat="server" maxlength="50" class="idio_TextAreaNormalStyleNoWH"
                                onkeypress="return event.keyCode>=48&&event.keyCode<=57||event.keyCode==13||event.keyCode==46" />
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
