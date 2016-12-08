using System;
using System.Collections.Generic;
using System.Text;


namespace IdioSoft.Public
{
    /// <summary>
    /// 用户账号类
    /// </summary>
    public class LoginUserInfo
    {
        //objFrmDocument.SearchSQL = "SELECT ID, FileName, FileSize, UploadDate, UploadUser FROM View_Company_ContactDocumentInfo where ContactID='" + RecordID + "'";
        //objFrmDocument.InsertSQL = "INSERT INTO TCM_CompanyContactDocumentInfo (ID, ContactID, FileName, FileSize, UploadDate, UploadUser) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
        //objFrmDocument.Category = "\\Company\\Contact";
        //objFrmDocument.DeleteSQL = "Delete TCM_CompanyContactDocumentInfo where ID='{0}'";
        //RecordID
        #region "用户信息"
        public enum UpFileKey
        {
            SearchSQL=1,
            InsertSQL=2,
            Category=3,
            DeleteSQL=4,
            RecordID=5
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        string _ID = "";
        public string ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        /// <summary>
        /// 登录名称
        /// </summary>
        string _LoginName = "";
        public string LoginName
        {
            get
            {
                return _LoginName;
            }
            set
            {
                _LoginName = value;
            }
        }
        /// <summary>
        /// 密码
        /// </summary>
        string _Password = "";
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
            }
        }
        /// <summary>
        /// 用户名称（英文）
        /// </summary>
        string _UserNameEn = "";
        public string UserNameEn
        {
            get
            {
                return _UserNameEn;
            }
            set
            {
                _UserNameEn = value;
            }
        }
        /// <summary>
        /// 用户名称（中文）
        /// </summary>
        string _UserNameCn = "";
        public string UserNameCn
        {
            get
            {
                return _UserNameCn;
            }
            set
            {
                _UserNameCn = value;
            }
        }
        /// <summary>
        /// 用户Email
        /// </summary>
        string _Email = "";
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
            }
        }
        /// <summary>
        /// 用户性别
        /// </summary>
        string _Gender = "";
        public string Gender
        {
            get
            {
                return _Gender;
            }
            set
            {
                _Gender = value;
            }
        }
        /// <summary>
        /// 用户所属区域Name
        /// </summary>
        string _RegionName = "";
        public string RegionName
        {
            get
            {
                return _RegionName;
            }
            set
            {
                _RegionName = value;
            }
        }
        /// <summary>
        /// DutyID
        /// </summary>
        int _DutyID = 0;
        public int DutyID
        {
            get
            {
                return _DutyID;
            }
            set
            {
                _DutyID = value;
            }
        }
        /// <summary>
        /// 用户系统权限
        /// </summary>
        string _SystemLimited = "";
        public string SystemLimited
        {
            get
            {
                return _SystemLimited;
            }
            set
            {
                _SystemLimited = value;
            }
        }
        /// <summary>
        /// 用户页面显示记录数
        /// </summary>
        int _PageSize = 10;
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = value;
            }
        }

        List<cMaterial> _PasteData;
        public List<cMaterial> PasteData
        {
            get
            {
                return _PasteData;
            }
            set
            {
                _PasteData = value;
            }
        }

        #region "未用到的"
        ///// <summary>
        ///// 向高级搜索方向传
        ///// </summary>
        //string _AdvancedSearchSQLTo = "";
        //public string AdvancedSearchSQLTo
        //{
        //    get
        //    {
        //        return _AdvancedSearchSQLTo;
        //    }
        //    set
        //    {
        //        _AdvancedSearchSQLTo = value;
        //    }
        //}
        ///// <summary>
        ///// 从高级搜索方向回传
        ///// </summary>
        //string _AdvancedSearchSQLFrom = "";
        //public string AdvancedSearchSQLFrom
        //{
        //    get
        //    {
        //        return _AdvancedSearchSQLFrom;
        //    }
        //    set
        //    {
        //        _AdvancedSearchSQLFrom = value;
        //    }
        //}
        ///// <summary>
        ///// 高级搜索链接
        ///// </summary>
        //string _AdvancedSearchUrl = "";
        //public string AdvancedSearchUrl
        //{
        //    get
        //    {
        //        return _AdvancedSearchUrl;
        //    }
        //    set
        //    {
        //        _AdvancedSearchUrl = value;
        //    }
        //}
        ///// <summary>
        ///// 这个是用于定义高级搜索时的模块定义
        ///// </summary>
        //string _AdvancedSearchAbUrl = "";
        //public string AdvancedSearchAbUrl
        //{
        //    get
        //    {
        //        return _AdvancedSearchAbUrl;
        //    }
        //    set
        //    {
        //        _AdvancedSearchAbUrl = value;
        //    }
        //}
        ///// <summary>
        ///// 高价搜索SQL语句
        ///// </summary>
        //string _AdvancedSearchQuery = "";
        //public string AdvancedSearchQuerySQL
        //{
        //    get
        //    {
        //        return _AdvancedSearchQuery;
        //    }
        //    set
        //    {
        //        _AdvancedSearchQuery = value;
        //    }
        //}
        ///// <summary>
        ///// 高级搜索TabName
        ///// </summary>
        //string _AdvancedSearchTabName = "";
        //public string AdvancedSearchTabName
        //{
        //    get
        //    {
        //        return _AdvancedSearchTabName;
        //    }
        //    set
        //    {
        //        _AdvancedSearchTabName = value;
        //    }
        //}
        ///// <summary>
        ///// Group By
        ///// </summary>
        //string _GroupBySQL = "";
        //public string GroupBySQL
        //{
        //    get
        //    {
        //        return _GroupBySQL;
        //    }
        //    set
        //    {
        //        _GroupBySQL = value;
        //    }
        //}
        ///// <summary>
        ///// 定义搜索下拉菜对应下拉菜单要载入的SQL
        ///// </summary>
        //Dictionary<string, string> _DicFieldSearchSQL;
        //public Dictionary<string, string> DicFieldSearchSQL
        //{
        //    get
        //    {
        //        return _DicFieldSearchSQL;
        //    }
        //    set
        //    {
        //        _DicFieldSearchSQL = value;
        //    }
        //}
        #endregion


        /// <summary>
        /// 判断用户是否有某一操作的权限
        /// </summary>
        /// <param name="LimitID">某一操作的对应权限ID</param>
        /// <param name="UserLimit">用户权限列表</param>
        /// <returns>如是有权限返回true,否则false</returns>
        public bool funBln_Limited(string LimitID, string UserLimit)
        {
            string strTempUserLimit;
            strTempUserLimit = "," + UserLimit + ",";//这个是为了防止用户权限两边本身没有带上,号，所以，单独再给加上。因为加上这个也并不会出现问题
            if (strTempUserLimit.IndexOf("," + LimitID + ",") >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }

    public class cMaterial
    {
        public string MLFB { get; set; }
        public string SPR { get; set; }
        public string Qty { get; set; }
        public string DiscountFactor { get; set; }
        public string Voltage { get; set; }
        public string V70Index { get; set; }
    }
}
