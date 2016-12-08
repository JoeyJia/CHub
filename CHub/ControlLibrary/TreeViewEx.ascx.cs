using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.ComponentModel;
using System.Collections.Generic;
/*
 *初始化
    TreeViewEx1.CheckALLTitle = "wll998";
    TreeViewEx1.ShowCheckBoxes = TreeNodeTypes.Leaf;
    TreeViewEx1.TreeViewSQL = "SELECT SortID, SortName FROM Information_Basic_Sort where  isdisplay=1 order by orderno";
    TreeViewEx1.TreeViewSQLParentID = "ParentID";
    TreeViewEx1.subNodes_Load(null, "{00000000-0000-0000-0000-000000000000}");
 * 读取
    for (int i = 0; i < TreeViewEx1.TreeViewCheckNodeValues.Count; i++)
    {
        Response.Write(TreeViewEx1.TreeViewCheckNodeValues[i] + "<br>");
    }   
 * 载入结点
    TreeViewEx1.TreeViewCheckStatus = false;
    List<string> ls = new List<string>();
    ls.Add("397b8d71-acec-4851-bd9e-8d6065831e2f");
    TreeViewEx1.TreeViewNodeValuesForCheck = ls;
    TreeViewEx1.TreeViewCheckStatusByList = true;
*/
namespace CHub.ControlLibrary
{
    public partial class TreeViewEx : ClassLibrary.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #region "设置公共属性"
        /// <summary>
        /// 取得或设置CheckALL的标题
        /// </summary>
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue(""), Description("取得或设置CheckALL的标题")]
        public string CheckALLTitle
        {
            get
            {
                return chkALL_TreeViewEx.Text;
            }
            set
            {
                chkALL_TreeViewEx.Text = value;
            }
        }
        /// <summary>
        /// 设置TreeView是否有CheckBox,Leaf为只在叶结点,None为没有,Parent为父结点,Root为为根,All为所有结点
        /// </summary>
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue(""), Description("设置TreeView是否有CheckBox,Leaf为只在叶结点,None为没有,Parent为父结点,Root为为根,All为所有结点")]
        public TreeNodeTypes ShowCheckBoxes
        {
            get
            {
                return tvMain_TreeViewEx.ShowCheckBoxes;
            }
            set
            {
                tvMain_TreeViewEx.ShowCheckBoxes = value;
            }
        }
        /// <summary>
        /// 设置载入到TreeView的SQL语句,默认下SQL中的第一个字段为ID,第二个为名字
        /// </summary>
        string _TreeViewSQL = "";
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue(""), Description("设置载入到TreeView的SQL语句,默认下SQL中的第一个字段为ID,第二个为名字")]
        public string TreeViewSQL
        {
            get
            {
                return _TreeViewSQL;
            }
            set
            {
                _TreeViewSQL = value;
            }
        }
        /// <summary>
        /// 用于父子结点关联的字段
        /// </summary>
        string _TreeViewSQLParentID = "";
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue(""), Description("用于父子结点关联的字段")]
        public string TreeViewSQLParentID
        {
            get
            {
                return _TreeViewSQLParentID;
            }
            set
            {
                _TreeViewSQLParentID = value;
            }
        }
        /// <summary>
        /// 取得用户勾选的记录
        /// </summary>
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue(""), Description("取得用户勾选的记录")]
        public List<string> TreeViewCheckNodeValues
        {
            get
            {
                List<string> lst = new List<string>();
                for (int i = 0; i < tvMain_TreeViewEx.CheckedNodes.Count; i++)
                {
                    lst.Add(tvMain_TreeViewEx.CheckedNodes[i].Value);
                }
                return lst;
            }
        }
        /// <summary>
        /// 设置CheckBox的状态
        /// </summary>
        [Bindable(true),
        Category("IDIO_Property"),
        DefaultValue(""), Description("设置CheckBox的状态")]
        public bool TreeViewCheckStatus
        {
            set
            {
                subTreeViewEx_DoCheck(null, value);
            }
        }
        private List<string> _TreeViewNodeValuesForCheck = new List<string>();
        /// <summary>
        /// 设置或取得用户想要做勾选操作的记录
        /// </summary>
        public List<string> TreeViewNodeValuesForCheck
        {
            get
            {
                return _TreeViewNodeValuesForCheck;
            }
            set
            {
                _TreeViewNodeValuesForCheck = value;
            }
        }
        public bool TreeViewCheckStatusByList
        {
            set
            {
                subTreeViewEx_DoCheck(null, value, TreeViewNodeValuesForCheck);
            }
        }
        /// <summary>
        /// 控件是否可用
        /// </summary>
        public bool Enable
        {
            set
            {
                tvMain_TreeViewEx.Enabled = value;
            }
        }
        #endregion
        #region "载入栏目结构"
        /// <summary>
        /// 这个是载入数据库操作，这个可重写，因为每个数据库都可能不一样
        /// </summary>
        /// <param name="pNode">结果</param>
        /// <param name="strParentID">父ID</param>
        public virtual void subNodes_Load(TreeNode pNode, string strParentID)
        {
            string strSQL = "";
            strSQL = TreeViewSQL.ToLower().Replace("where", " where " + TreeViewSQLParentID + "='" + strParentID + "' and ");
            DataSet ds = new DataSet();
            ds = objClassDbAccess.funDataset_SQLExecuteNonQuery(strSQL);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TreeNode tN = new TreeNode();
                tN.Text = ds.Tables[0].Rows[i][1].ToString();
                tN.Value = ds.Tables[0].Rows[i][0].ToString();
                tN.SelectAction = TreeNodeSelectAction.None;
                //tN.Expanded = true;
                if (pNode == null)
                {
                    tvMain_TreeViewEx.Nodes.Add(tN);
                    subNodes_Load(tN, ds.Tables[0].Rows[i][0].ToString());
                }
                else
                {
                    pNode.ChildNodes.Add(tN);
                    subNodes_Load(tN, ds.Tables[0].Rows[i][0].ToString());
                }
            }
        }
        #endregion
        #region "勾选用户指定的记录"
        /// <summary>
        /// 是否勾选用户指定的记录
        /// </summary>
        /// <param name="pNode">开始结点</param>
        /// <param name="blnCheck">是否勾选</param>
        /// <param name="lsCheck"></param>
        private void subTreeViewEx_DoCheck(TreeNode pNode, bool blnCheck, List<string> lsCheck)
        {
            if (pNode == null)
            {
                for (int i = 0; i < tvMain_TreeViewEx.Nodes.Count; i++)
                {
                    if (blnExistsInList(lsCheck, tvMain_TreeViewEx.Nodes[i].Value))
                    {
                        tvMain_TreeViewEx.Nodes[i].Checked = blnCheck;
                    }
                    if (tvMain_TreeViewEx.Nodes[i].ChildNodes.Count > 0)
                    {
                        subTreeViewEx_DoCheck(tvMain_TreeViewEx.Nodes[i], blnCheck, lsCheck);
                    }
                }
            }
            else
            {
                for (int i = 0; i < pNode.ChildNodes.Count; i++)
                {
                    if (blnExistsInList(lsCheck, pNode.ChildNodes[i].Value))
                    {
                        pNode.ChildNodes[i].Checked = blnCheck;
                    }
                    if (pNode.ChildNodes.Count > 0)
                    {
                        subTreeViewEx_DoCheck(pNode.ChildNodes[i], blnCheck, lsCheck);
                    }
                }
            }
        }
        private bool blnExistsInList(List<string> lsCheck, string strTmp)
        {
            for (int i = 0; i < lsCheck.Count; i++)
            {
                if (lsCheck[i].ToString().ToLower() == strTmp)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        #region "当点击选中所有时操作"
        protected void chkALL_TreeViewEx_CheckedChanged(object sender, EventArgs e)
        {
            subTreeViewEx_DoCheck(null, chkALL_TreeViewEx.Checked);
        }
        /// <summary>
        /// 设置是否选中CheckBox
        /// </summary>
        /// <param name="pNode">以什么结点开始</param>
        private void subTreeViewEx_DoCheck(TreeNode pNode, bool blnCheck)
        {
            if (pNode == null)
            {
                for (int i = 0; i < tvMain_TreeViewEx.Nodes.Count; i++)
                {
                    switch (tvMain_TreeViewEx.ShowCheckBoxes)
                    {
                        case TreeNodeTypes.Leaf:
                            if (tvMain_TreeViewEx.Nodes[i].ChildNodes.Count <= 0)
                            {
                                tvMain_TreeViewEx.Nodes[i].Checked = blnCheck;
                            }
                            break;
                        case TreeNodeTypes.All:
                            tvMain_TreeViewEx.Nodes[i].Checked = blnCheck;
                            break;
                        case TreeNodeTypes.None:
                            break;
                        case TreeNodeTypes.Parent:
                            if (tvMain_TreeViewEx.Nodes[i].ChildNodes.Count > 0)
                            {
                                tvMain_TreeViewEx.Nodes[i].Checked = blnCheck;
                            }
                            break;
                        case TreeNodeTypes.Root:
                            tvMain_TreeViewEx.Nodes[0].Checked = blnCheck;
                            break;
                    }
                    if (tvMain_TreeViewEx.Nodes[i].ChildNodes.Count > 0)
                    {
                        subTreeViewEx_DoCheck(tvMain_TreeViewEx.Nodes[i], blnCheck);
                    }
                }
            }
            else
            {
                for (int i = 0; i < pNode.ChildNodes.Count; i++)
                {
                    switch (tvMain_TreeViewEx.ShowCheckBoxes)
                    {
                        case TreeNodeTypes.Leaf:
                            if (pNode.ChildNodes.Count <= 0)
                            {
                                pNode.ChildNodes[i].Checked = blnCheck;
                            }
                            break;
                        case TreeNodeTypes.All:
                            pNode.ChildNodes[i].Checked = blnCheck;
                            break;
                        case TreeNodeTypes.None:
                            break;
                        case TreeNodeTypes.Parent:
                            if (pNode.ChildNodes.Count > 0)
                            {
                                pNode.ChildNodes[i].Checked = blnCheck;
                            }
                            break;
                        case TreeNodeTypes.Root:
                            tvMain_TreeViewEx.Nodes[0].Checked = blnCheck;
                            break;
                    }
                    if (pNode.ChildNodes.Count > 0)
                    {
                        subTreeViewEx_DoCheck(pNode.ChildNodes[i], blnCheck);
                    }
                }
                switch (tvMain_TreeViewEx.ShowCheckBoxes)
                {
                    case TreeNodeTypes.Leaf:
                        if (pNode.ChildNodes.Count <= 0)
                        {
                            pNode.Checked = blnCheck;
                        }
                        break;
                    case TreeNodeTypes.All:
                        pNode.Checked = blnCheck;
                        break;
                    case TreeNodeTypes.None:
                        break;
                    case TreeNodeTypes.Parent:
                        if (pNode.ChildNodes.Count > 0)
                        {
                            pNode.Checked = blnCheck;
                        }
                        break;
                    case TreeNodeTypes.Root:
                        tvMain_TreeViewEx.Nodes[0].Checked = blnCheck;
                        break;
                }
            }
        }
        #endregion
    }
}