<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileDownload.aspx.cs" Inherits="CHub.FileDownload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="UI/css/style.css" />
    <style type="text/css">
        body
        {
            margin-left: 5px;
            margin-top: 5px;
            margin-right: 5px;
            margin-bottom: 5px;
        }
    </style>

    <script language="javascript">
        function movewin() {
            //window.moveTo((screen.Width) / 2 - 200, (screen.height) / 2 - 150);
            window.moveTo(450, 300);
        }
    </script>

</head>
<body onload="movewin()">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="4" cellspacing="0" width="95%" style="border: 1px #999999 solid">
            <tr>
                <td style="background-color: #eeeeee; border-bottom: 1px solid #999999;">
                    Please download the file via the link below<br />
                    请通过以下链接下载文件
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HyperLink ID="hlnkFile" runat="server" CssClass="gtArrowLinkBlue" Target="_blank"></asp:HyperLink>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>