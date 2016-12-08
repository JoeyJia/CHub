<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TreeViewEx.ascx.cs"
    Inherits="CHub.ControlLibrary.TreeViewEx" %>
<table style="width: 100%; border: 1px #dddddd solid;" cellpadding="2" cellspacing="0">
    <tr id="trChkAll" runat="server" visible="false">
        <td style="background-color: #FFFFCC; border-bottom: 1px #dddddd solid;">
            <asp:CheckBox ID="chkALL_TreeViewEx" runat="server" AutoPostBack="True" OnCheckedChanged="chkALL_TreeViewEx_CheckedChanged" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:TreeView ID="tvMain_TreeViewEx" runat="server" ImageSet="Simple" ShowCheckBoxes="All"
                ShowLines="True">
                <ParentNodeStyle Font-Bold="False" />
                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                    VerticalPadding="0px" />
                <NodeStyle Font-Names="Arial" Font-Size="12px" ForeColor="#333333" HorizontalPadding="0px"
                    NodeSpacing="0px" VerticalPadding="0px" />
            </asp:TreeView>

            <script type="text/javascript" language="javascript">
                SetTreeNodeAutoSelectParentNodeHandle("<%=tvMain_TreeViewEx.ClientID%>");                   </script>

        </td>
    </tr>
</table>
