<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchGridList.ascx.cs"
    Inherits="CHub.ControlLibrary.SearchGridList" %>
<%@ Register Assembly="IdioSoft.DateTimePicker" Namespace="IdioSoft.DateTimePicker"
    TagPrefix="IdioSoft" %>
<%@ Register Assembly="IdioSoft.CustomPageEx" Namespace="IdioSoft.CustomPageEx" TagPrefix="IdioSoft" %>
<%@ Register Assembly="IdioSoft.ExportExcel" Namespace="IdioSoft.ExportExcel" TagPrefix="IdioSoft" %>
<table id="tableMain" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td valign="top">
            <table border="0" cellpadding="2" cellspacing="0" style="border: solid 1px #dddddd;"
                width="100%">
                <tr>
                    <td bgcolor="#f1f1f1">
                        <table border="0" cellpadding="2" cellspacing="0" style="border-bottom: #dddddd 1px solid"
                            width="100%">
                            <tr>
                                <td nowrap="nowrap">
                                    <img height="22" src="UI/Images/search.gif" width="22" border="0" alt="Search" />
                                </td>
                                <td nowrap="nowrap" width="53">
                                    <asp:DropDownList ID="cboField" runat="server" AutoPostBack="True" CssClass="idio_SelectNormalStyle"
                                        OnSelectedIndexChanged="cboField_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td id="tdsearchContent" runat="server" nowrap="nowrap">
                                    <input id="txtSearch" runat="server" class="idio_inputBoxNormalStyle" maxlength="20"
                                        name="txtSearch" style="width: 160px" type="text" />
                                </td>
                                <td id="tdsearchDate1" runat="server" nowrap="nowrap" visible="false">
                                    <IdioSoft:DateTimePicker ID="DateTimePickerStartDate" runat="server" TimeWidth="120" />
                                </td>
                                <td id="tdsearchDate2" runat="server" nowrap="nowrap" visible="false">
                                    <IdioSoft:DateTimePicker ID="DateTimePickerEndDate" runat="server" TimeWidth="120" />
                                </td>
                                <td id="tdsearchSelect" runat="server" nowrap="nowrap" visible="false">
                                    <select id="cboSearch" runat="server" class="idio_SelectNormalStyle">
                                        <option selected="selected" value="0">False</option>
                                        <option value="1">True</option>
                                    </select>
                                </td>
                                <td style="width: 80%; padding-left: 4px;">
                                    <input name="btnSearch" type="submit" class="idio_FlatBtnOnMouseOut" id="btnSearch"
                                        value="搜索" runat="server" onserverclick="btnSearch_OnServerClick" style="vertical-align: top">
                                    <IdioSoft:ExportExcel ID="ExportExcel1" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="trOperation" runat="server">
                    <td style="text-align: right; padding-top: 4px;">
                        <asp:Button ID="btnDetail" runat="server" CssClass="idio_FlatBtnOnMouseOut" OnClick="btnDetail_OnClick"
                            Text="详细" />
                        <asp:Button ID="btnAddnew" runat="server" Text="新增" CssClass="idio_FlatBtnOnMouseOut"
                            OnClick="btnAddnew_OnClick" />
                        <asp:Button ID="btnModify" runat="server" Text="修改" CssClass="idio_FlatBtnOnMouseOut"
                            OnClick="btnModify_OnClick" />
                        <asp:Button ID="btnDelete" runat="server" CssClass="idio_FlatBtnOnMouseOut" OnClick="btnDelete_OnClick"
                            Text="删除" OnClientClick="return confirm('确定删除吗？');" />
                        <asp:Button ID="btnStop" runat="server" CssClass="idio_FlatBtnOnMouseOut" OnClick="btnStop_OnClick"
                            Text="停用" OnClientClick="return confirm('确定停用吗？');" />
                        <asp:Button ID="btnOther1" runat="server" Text="Other1" CssClass="idio_FlatBtnOnMouseOut"
                            OnClick="btnOther1_OnClick" Visible="false" />
                        <asp:Button ID="btnOther2" runat="server" Text="Other2" CssClass="idio_FlatBtnOnMouseOut"
                            OnClick="btnOther2_OnClick" Visible="false" />
                        <asp:Button ID="btnOther3" runat="server" Text="btnOther3" CssClass="idio_FlatBtnOnMouseOut"
                            OnClick="btnOther3_OnClick" Visible="false" />
                        <asp:Button ID="btnOther4" runat="server" Text="btnOther4" CssClass="idio_FlatBtnOnMouseOut"
                            OnClick="btnOther4_OnClick" Visible="false" />
                        <asp:Button ID="btnOther5" runat="server" Text="btnOther5" CssClass="idio_FlatBtnOnMouseOut"
                            OnClick="btnOther5_OnClick" Visible="false" />
                        <asp:Button ID="btnOther6" runat="server" Text="btnOther6" CssClass="idio_FlatBtnOnMouseOut"
                            OnClick="btnOther6_OnClick" Visible="false" />
                        <asp:Button ID="btnOther7" runat="server" Text="btnOther7" CssClass="idio_FlatBtnOnMouseOut"
                            OnClick="btnOther7_OnClick" Visible="false" />
                        <asp:Button ID="btnOther8" runat="server" Text="btnOther8" CssClass="idio_FlatBtnOnMouseOut"
                            OnClick="btnOther8_OnClick" Visible="false" />
                    </td>
                </tr>
                <tr id="trlblTitle" runat="server" visible="false">
                    <td id="tdlblTitle" runat="server" style="padding-top: 4px;"></td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#f1f1f1" width="100%" style="padding-top: 4px;">
                        <div id="divGrid" runat="server" class="idio_ScroolStyle" style="overflow: scroll; border-left: #dddddd 1px solid; border-top: #dddddd 1px solid; border-right: #dddddd 1px solid; border-bottom: #dddddd 1px solid; width: 590px; height: 100%; background-color: white">
                            <p>
                                <asp:GridView ID="grdMain" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                    Width="100%" OnRowDataBound="grdMain_RowDataBound" AllowSorting="True" OnSorting="grdMain_Sorting"
                                    BorderColor="#CCCCCC" BorderStyle="Double" BorderWidth="1px" OnSelectedIndexChanged="grdMain_SelectedIndexChanged"
                                    Font-Size="12px" CssClass="GridStyle">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="GridStyle" />
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" InsertVisible="False" SelectImageUrl="~/UI/images/selectcell.png"
                                            ShowCancelButton="False" ShowSelectButton="True" />
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" Width="30" Visible="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" />
                                    <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" CssClass="GridStyle" />
                                    <HeaderStyle BackColor="#5D7B9D" ForeColor="White" CssClass="aLink GridStyle" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                </asp:GridView>
                            </p>
                        </div>
                        <div style="text-align: left; padding-top: 4px;">
                            <p>
                                <IdioSoft:CustomPageEx ID="CustomPageEx1" runat="server" Width="100%" />
                            </p>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
