<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductsQuotation.aspx.cs" Inherits="CHub.ProductsQuotation"
    MasterPageFile="~/Main.Master" ValidateRequest="false" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="js/jquery.handsontable.full.js"></script>
    <link rel="stylesheet" media="screen" href="js/jquery.handsontable.full.css">
    <style>
        div {
            font-size: 12px;
            line-height: normal;
        }

        .htCore th {
            border: 1px solid #cccccc;
        }
    </style>

    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>  --%>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td>
                <!--粘贴文本-->
                <table cellpadding="0" cellspacing="0" border="0" width="100%" id="tablePaste" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-bottom: 4px;">
                            <p>
                                <strong>Please"MLFB-Z=Options","SPR","QTY","Discount Factor(%)","Voltage","Y70=?" Paste the following Excel column</strong>
                            </p>
                            <p>
                                如果MLFB前四位为"1RA1","1RQ1","1RN1",倒数第二位为9的MLFB，那么请填写电压；<br />
                                如果MLFB前四位为"1LA4","1MS4","1PQ4",并且倒数第三位为"N","W",倒数第二位为9的MLFB，那么请填写电压<br />
                                Y70=?列填写的索引号 <a href="#" onclick="javascript:window.open('Y70Index.aspx','','width=500px,height=400px,status=no,toolbar=no,menubar=no,location=no');" class="link">点击此处查看</a>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-bottom: 4px;">
                            <div id="example1" style="background-color: #EEEEEE;"></div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-bottom: 4px;" align="center">
                            <input id="btnSave" name="btnSave" type="button" value="Submit" class="idio_FlatBtnOnMouseOut" onclick="javascript: fnSaveData();" />
                            <asp:Button ID="btnPSubmit" CssClass="clsbtnPSubmit" runat="server" Text="Submit" OnClick="btnPSubmit_Click" Style="display: none;" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="line-height: 24px;">
                            <p>
                                For example<br />
                                <img alt="" src="UI/Images/Example1.gif" border="0" />
                            </p>
                        </td>
                    </tr>
                </table>
                <script type="text/javascript">
                    var $container = jQuery("#example1");
                    $container.handsontable({
                        startRows: 8,
                        startCols: 4,
                        rowHeaders: true,
                        colHeaders: true,
                        minSpareRows: 3,
                        contextMenu: false,
                        colHeaders: ["MLFB-Z=Options", "SPR", "Qty", "Discount Factor", "Voltage", "Y70=?"],
                        columns: [
                          {
                              data: "MLFB",
                              width: 180
                              //1nd column is simple text, no special options here
                          },
                          {
                              data: "SPR",
                              width: 180
                              //1nd column is simple text, no special options here
                          },
                          {
                              data: "Qty",
                              type: 'numeric'
                          },
                          {
                              data: "DiscountFactor",
                              width: 100,
                              type: 'numeric',
                              format: '0 %',
                              language: 'en' //this is the default locale, set up for USD
                          },
                          {
                              data: "Voltage",
                              width: 40,
                              type: 'numeric',
                              format: '0.0'
                          },
                          {
                              data: "Y70Index",
                              width: 40,
                              type: 'numeric'
                          }
                        ]
                    });

                    function fnSaveData() {
                        var handsontable = $container.data('handsontable');
                        var arypostjs = handsontable.getData();
                        var postjs = "";
                        var tmpjs = "";
                        postjs = "{\"Material\":[";
                        for (var i = 0 ; i < arypostjs.length; i++) {
                            if (!(arypostjs[i].MLFB == null && arypostjs[i].SPR == null)) {
                                var mlfb = "";
                                if (arypostjs[i].MLFB != null) {
                                    mlfb = arypostjs[i].MLFB.toString();
                                }
                                var spr = "";
                                if (arypostjs[i].SPR != null) {
                                    spr = arypostjs[i].SPR.toString();
                                }
                                var qty = "";
                                if (arypostjs[i].Qty != null) {
                                    qty = arypostjs[i].Qty.toString();
                                }
                                else {
                                    qty = "1";
                                }
                                var discountfactor = "";
                                if (arypostjs[i].DiscountFactor != null) {
                                    discountfactor = arypostjs[i].DiscountFactor.toString();
                                }
                                else {
                                    discountfactor = "1";
                                }
                                var Voltage = "";
                                if (arypostjs[i].Voltage != null) {
                                    Voltage = arypostjs[i].Voltage.toString();
                                }
                                var Y70Index = "";
                                if (arypostjs[i].Y70Index != null) {
                                    Voltage = arypostjs[i].Y70Index.toString();
                                }

                                tmpjs = tmpjs + "{\"MLFB\":\"" + mlfb + "\",";
                                tmpjs = tmpjs + "\"SPR\":\"" + spr + "\",";
                                tmpjs = tmpjs + "\"Qty\":\"" + qty + "\",";
                                tmpjs = tmpjs + "\"DiscountFactor\":\"" + discountfactor + "\",";
                                tmpjs = tmpjs + "\"Voltage\":\"" + Voltage + "\",";
                                tmpjs = tmpjs + "\"Y70Index\":\"" + Y70Index + "\"},";
                            }
                        }
                        if (tmpjs != "") {
                            tmpjs = tmpjs.substring(0, tmpjs.length - 1);
                        }
                        postjs = postjs + tmpjs + "]}";
                        jQuery.ajax({
                            url: "ProductQuotationSaveJson.aspx",
                            data: { "data": postjs }, //returns all cells' data
                            dataType: 'json',
                            type: 'POST',
                            success: function (res) {
                                if (res.result === 'ok') {
                                    jQuery(".clsbtnPSubmit").click();
                                }
                                else {
                                    //alert(res.result + " 1");
                                    jQuery(".clsbtnPSubmit").click();
                                }
                            },
                            error: function (res) {
                                //alert(res.result + " 2");
                                jQuery(".clsbtnPSubmit").click();
                            }
                        });
                    }
                </script>
                <!--End-->
                <!--列表-->
                <table cellpadding="0" cellspacing="0" border="0" width="100%" id="tableList" runat="server">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td style="padding: 4px; width: 15%;" nowrap="nowrap">
                                        <p>
                                            Customer/OEM:
                                        </p>
                                    </td>
                                    <td style="padding: 4px;">
                                        <input id="txtCustomer" type="text" runat="server" maxlength="200" class="idio_TextAreaNormalStyleNoWH"
                                            style="width: 300px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 4px;">
                                        <p>
                                            Channel:
                                        </p>
                                    </td>
                                    <td style="padding: 4px;">
                                        <input id="txtChannel" type="text" runat="server" maxlength="200" class="idio_TextAreaNormalStyleNoWH"
                                            style="width: 300px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 4px;">
                                        <p>
                                            Project:
                                        </p>
                                    </td>
                                    <td style="padding: 4px;">
                                        <input id="txtProject" type="text" runat="server" maxlength="200" class="idio_TextAreaNormalStyleNoWH"
                                            style="width: 300px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 4px;">
                                        <p>
                                            Quotation No.:
                                        </p>
                                    </td>
                                    <td style="padding: 4px;">
                                        <input id="txtQuotationNo" type="text" runat="server" maxlength="100" class="idio_TextAreaNormalStyleNoWH" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 4px;">
                                        <p>
                                            Bonus Rate:
                                        </p>
                                    </td>
                                    <td style="padding: 4px;">
                                        <p>
                                            <asp:TextBox ID="txtBonusRate" runat="server" MaxLength="10" CssClass="idio_TextAreaNormalStyleNoWH"
                                                Style="width: 40px;" OnTextChanged="txtBonusRate_TextChanged" AutoPostBack="True"></asp:TextBox>%
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td style="border-bottom: 1px solid #CCCCCC;">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 4px; padding-bottom: 4px;">
                            <p>
                                <strong>Options List</strong>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>
                                <span class="FontRed">Note:red text line is error!</span>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="GridStyle" id="divList" runat="server" style="font-size: 12px;">
                            </div>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td style="border-bottom: 1px solid #CCCCCC;">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td style="padding: 4px;" align="right">
                                        <p>
                                            Total:<asp:Label ID="lblTotalPrice" runat="server" Text=""></asp:Label>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="padding: 4px; line-height: 30px;">
                            <asp:Button ID="btnModify" runat="server" Text="Modify" CssClass="idio_FlatBtnOnMouseOut"
                                OnClick="btnModify_Click" Width="80px" />&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="idio_FlatBtnOnMouseOut"
                                OnClick="btnReset_Click" Width="80px" />
                            <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="idio_FlatBtnOnMouseOut"
                                OnClick="btnExport_Click" Width="80px" />&nbsp;
                        </td>
                    </tr>
                </table>
                <!--End-->
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
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>

    <script type="text/javascript">
        function ExpandOptionsTable(selIndex) {
            var obj = document.getElementById("ctl00_ContentPlaceHolder1_tr" + selIndex);

            if (obj.style.display == "none") {
                obj.style.display = "block";
            }
            else {
                obj.style.display = "none";
            }
        }

    </script>
</asp:Content>
