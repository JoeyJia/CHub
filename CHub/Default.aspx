<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CHub._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="content-type" content="text/html; charset=GB2312" />
    <meta http-equiv="content-language" content="en" />
    <meta http-equiv="content-style-type" content="text/css" />
    <link rel="SHORTCUT ICON" href="_resources/img/cummins%20logo-large%20-black.png"/>
    <title>Cummins CHub Tools 1.0.0</title>
    <meta http-equiv="imagetoolbar" content="no" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta name="copyright" content="Copyright Cummins AG - all rights reserved" />
    <meta name="producer" content="virtual identity AG | http://www.virtual-identity.com" />
    <link rel="stylesheet" type="text/css" media="screen,projection" href="_resources/css/styles.css" />
    <!--[if IE]>
	<link rel="stylesheet" type="text/css" media="screen,projection" href="./_resources/css/styles-ie.css" />
	<![endif]-->
    <link rel="stylesheet" type="text/css" media="print" href="_resources/css/print.css" />
    <link rel="Stylesheet" type="text/css" media="all" href="UI/CSS/Style.css" />
    <link rel="contents" title="Sitemap" href="#sitemap-link" />

    <script type="text/javascript">
        var RESOURCES_PATH = "_resources/";
        var headerVisualImages = {
            "swap-1": "_assets/swap-1.jpg",
            "swap-2": "_assets/swap-3.jpg",
            "swap-3": "_assets/swap-4.jpg",
            "swap-4": "_assets/swap-5.jpg",
            "swap-5": "_assets/swap-7.jpg"
        };
    </script>

    <script type="text/javascript" src="_resources/js/lib/prototype.js"></script>

    <script type="text/javascript" src="_resources/js/lib/common.js"></script>

    <script type="text/javascript" src="_resources/js/lib/core.js"></script>

    <script type="text/javascript" src="_resources/js/lib/module.breadcrumb.js"></script>

    <script type="text/javascript" src="_resources/js/lib/module.header-visual.js"></script>

    <script type="text/javascript" src="_resources/js/lib/module.language-selector.js"></script>

    <script type="text/javascript" src="_resources/js/lib/module.magnifier.js"></script>

    <script type="text/javascript" src="_resources/js/lib/module.lightbox-layer.js"></script>

    <!-- START: DEV NOTE -->
    <!-- If you do not wish to use all the scripts above (exclusive prototype.js) separately, you can use the compiled script instead -->
    <!-- <script type="text/javascript" src="./_resources/js/compiled/script.js"></script> -->
    <!-- END: DEV NOTE -->

    <script type="text/javascript" src="_resources/js/init.js"></script>

    <script type="text/javascript" src="_resources/js/example/siteexplorer-get-content.js"></script>

    <script type="text/javascript">
        document.title = "Cummins CHub Tools 1.0.0";
    </script>

</head>
<body class="page-type-3">
    <form id="Form1" name="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <p class="access">
        <a href="#content-zone">Skip to Content</a></p>
    <div id="footer-position-wrapper" >
        <p id="logo" style="background:url(_resources/img/bg-header.png) rgb(182, 16, 32)">
            <a class="content-layer-2-trigger" href="http://www.cummins.com/" rel="lightbox-layer-logo"
                title="Go to the homepage">
                <img src="_resources/img/cummins-logo-print.gif" alt="cummins" height="50" width="55" style="margin-top:10px; margin-left:50px;"
                     /></a>
            
        </p>
        <div id="header-zone" style="background:url(_resources/img/bg-header.png) rgb(182, 16, 32)">
            <div id="headervisual-zone" >
                &nbsp;</div>
            <div id="fluid-zone">
                <div id="headertext-zone">
                    <div class="headertext-content">
                       
                    </div>
                    <div class="hidden-headertext-contents">
                        <p class="access">
                            Content preview for [products &amp; solutions]</p>
                        <div id="headertext-swap-1" class="headertext-content">
                            <div class="sifr-header">
                                <h2>
                                    LD BU</h2>
                            </div>
                            <div class="sifr-header">
                                <h3>
                                    LD Products Configuration Tools</h3>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="toolbar-zone" class="clearfix">
            <div id="generic-nav-zone" style="padding-left: 320px; display: none;">
                <hr />
                <p class="access">
                    <a href="#search-zone">Skip over Generic Navigation</a></p>
                <ul id="toolbar-nav" class="clearfix">
                    <li><a href="javascript:void(0);" rel="jump-to-contact" style="background-image: url(_resources/img/tb-contact-en.gif);
                        width: 72px;"><span class="access">Contact</span></a></li>
                </ul>
            </div>
            <div id="search-zone" style="display: none;">
                <hr />
                <p class="access">
                    <a href="#content-zone">Skip over Site Explorer</a></p>
                <div id="site-explorer">
                    <p id="sitemap-link">
                        <a href="javascript:void(0);" style="background-image: url(_resources/img/tb-sitemap-en.gif);
                            width: 99px;"><span class="access">Site Explorer</span></a></p>
                    <div id="site-explorer-layer">
                        <div class="close">
                            <a href="javascript:void(0);"><span class="access">Close Site Explorer</span></a></div>
                    </div>
                </div>
            </div>
        </div>
        <div id="content-zone" class="clearfix">
            <hr />
            <div id="breadcrumb-zone">
                <dl id="breadcrumb" class="clearfix">
                    <dt class="access">You are here:</dt>
                    <dd id="ddHome" runat="server">
                        <a href="Default.aspx" class="link">Login</a>
                    </dd>
                </dl>
            </div>
            <div class="left-content">
                <div class="column" style="width: 594px;">
                    <div class="teaser">
                        <div class="sifr">
                            <h3 id="h3Title" runat="server">
                            </h3>
                        </div>
                        <div class="hruler">
                        </div>
                        <p>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td style="padding-bottom: 4px;" nowrap="nowrap">
                                                    Login Name
                                                </td>
                                                <td style="padding-left: 4px; padding-bottom: 4px;">
                                                    <input id="txtLoginName" type="text" class="idio_inputBoxNormalStyle" maxlength="30"
                                                        runat="server" />
                                                    <span class="FontRed">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-bottom: 4px;">
                                                    Password
                                                </td>
                                                <td style="padding-left: 4px; padding-bottom: 4px;">
                                                    <input id="txtPassword" type="password" class="idio_inputBoxNormalStyle" maxlength="20"
                                                        runat="server" />
                                                    <span class="FontRed">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td style="padding: 4px;">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Login" CssClass="idio_FlatBtnOnMouseOut"
                                                        OnClick="btnSubmit_Click" />
                                                    <input name="btnReset" type="reset" class="idio_FlatBtnOnMouseOut" id="btnReset"
                                                        runat="server" value="Reset" />
                                                </td>
                                            </tr>
                                            <tr runat="server" visible="false">
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td style="padding-left: 4px; padding-top: 8px; padding-bottom: 4px;">
                                                    <img src="UI/Images/arrow_red.gif" border="0" />
                                                    <asp:LinkButton ID="lnkForgetPwd" runat="server" OnClick="lnkForgetPwd_Click">Forget Password?</asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr id="trFindPwd" runat="server" visible="false">
                                                <td style="padding-top: 4px; padding-bottom: 4px;">
                                                    Email
                                                </td>
                                                <td style="padding-left: 4px; padding-top: 4px; padding-bottom: 4px;">
                                                    <input name="txtEMail" type="text" class="idio_inputBoxNormalStyle" id="txtEMail"
                                                        style="width: 160px;" runat="server" />
                                                    <input name="btnFindPwd" type="button" class="idio_FlatBtnOnMouseOut" id="btnFindPwd"
                                                        runat="server" value="Submit" onserverclick="btnFindPwd_OnServerClick" />
                                                </td>
                                            </tr>
                                            <tr id="trError" runat="server" visible="false">
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td style="table-layout: fixed; padding-left: 4px; padding-top: 8px;">
                                                    <img src="UI/Images/index_Error.gif" border="0" align="top" /><asp:Label ID="lblError"
                                                        runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <p>
                                                        &nbsp;</p>
                                                    <p>
                                                        &nbsp;</p>
                                                    <p>
                                                        &nbsp;</p>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>
                                            &nbsp;</p>
                                        <p>
                                            &nbsp;</p>
                                        <p>
                                            &nbsp;</p>
                                    </td>
                                </tr>
                            </table>
                        </p>
                    </div>
                </div>
            </div>
            <div class="right-content">
                <div class="column">
                    <div class="teaser">
                        <div class="sifr">
                            <h3>
                            </h3>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="toolbar-layer-contact" class="toolbar-layer">
            <div class="toolbar-content clearfix">
                <div class="close">
                    <a href="javascript:void(0);"><span class="access">Close Contact Layer</span></a></div>
                <div class="main-block clearfix">
                    <img src="_assets/contact-georg-mustermann.jpg" width="95" height="125" alt="Photo: Ms. Wang Yilin" />
                    <h3>
                        Ms. Wang Yilin</h3>
                    <p>
                        &nbsp;</p>
                    <h4>
                        Organisation</h4>
                    <p>
                        SLC I DT LD</p>
                </div>
                <div class="additional-block">
                    <h4>
                        Contact</h4>
                    <p>
                        Industry Sector, Drive Technologies Division, Cummins Central Building,No.7, WangJing
                        Zhonghuan Nanlu, Beijing 100102<br />
                        P.R.China</p>
                    <p class="top-spacer">
                        Tel.: (..86)-10-6476 5321</p>
                </div>
                <div class="additional-block">
                    <ul class="link-list">
                        <li><a href="mailto:zhuo.wang@Cummins.com" class="email">Wang, Zhuo - Carol SLC I IA&amp;DT
                            PEK</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div id="footer-position-placeholder">
            &nbsp;</div>
    </div>
    <div id="footer-zone">
        <div class="min-width">
            <dl id="footer">
                <dt class="global"><a href="http://www.cummins.com" title="Go to www.cummins.com">Cummins.com
                    Global Website</a><span class="pipe"> | </span></dt>
                <dt class="mobile"></dt>
                <dt>&copy; Cummins<span class="dash"> - </span></dt>
                <dd>
                    <a href="javascript:void(0);">Corporate Information</a><span class="pipe"> | </span>
                    <a href="javascript:void(0);">Privacy Policy</a><span class="pipe"> | </span><a href="javascript:void(0);">
                        Terms of Use</a><span class="pipe"> | </span><a href="javascript:void(0);">Digital ID</a></dd>
            </dl>
        </div>
    </div>
    </form>
</body>
</html>
