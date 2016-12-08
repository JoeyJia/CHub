<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPOperation.aspx.cs" Inherits="CHub.KPOperation" 
    MasterPageFile="~/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="padding-bottom: 4px;" nowrap="nowrap">
                <table cellpadding="0" cellspacing="1" border="0" width="100%" style="background-color: #CCCCCC;">
                    <tr style="background-color: #FFFFFF;">
                        <td width="20%" style="padding: 4px;" nowrap="nowrap">
                            Motor KP Discount
                        </td>
                        <td style="padding: 4px;">
                            <input id="txtKPDiscount" type="text" runat="server" maxlength="50" class="idio_TextAreaNormalStyleNoWH"
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
