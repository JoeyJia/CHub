using System;
using System.Collections.Generic;
using System.Text;


namespace IdioSoft.Public
{
    /// <summary>
    /// �û��˺���
    /// </summary>
    public class LoginUserInfo
    {
        //objFrmDocument.SearchSQL = "SELECT ID, FileName, FileSize, UploadDate, UploadUser FROM View_Company_ContactDocumentInfo where ContactID='" + RecordID + "'";
        //objFrmDocument.InsertSQL = "INSERT INTO TCM_CompanyContactDocumentInfo (ID, ContactID, FileName, FileSize, UploadDate, UploadUser) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
        //objFrmDocument.Category = "\\Company\\Contact";
        //objFrmDocument.DeleteSQL = "Delete TCM_CompanyContactDocumentInfo where ID='{0}'";
        //RecordID
        #region "�û���Ϣ"
        public enum UpFileKey
        {
            SearchSQL=1,
            InsertSQL=2,
            Category=3,
            DeleteSQL=4,
            RecordID=5
        }
        /// <summary>
        /// �û�ID
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
        /// ��¼����
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
        /// ����
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
        /// �û����ƣ�Ӣ�ģ�
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
        /// �û����ƣ����ģ�
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
        /// �û�Email
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
        /// �û��Ա�
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
        /// �û���������Name
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
        /// �û�ϵͳȨ��
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
        /// �û�ҳ����ʾ��¼��
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

        #region "δ�õ���"
        ///// <summary>
        ///// ��߼���������
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
        ///// �Ӹ߼���������ش�
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
        ///// �߼���������
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
        ///// ��������ڶ���߼�����ʱ��ģ�鶨��
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
        ///// �߼�����SQL���
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
        ///// �߼�����TabName
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
        ///// �������������˶�Ӧ�����˵�Ҫ�����SQL
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
        /// �ж��û��Ƿ���ĳһ������Ȩ��
        /// </summary>
        /// <param name="LimitID">ĳһ�����Ķ�ӦȨ��ID</param>
        /// <param name="UserLimit">�û�Ȩ���б�</param>
        /// <returns>������Ȩ�޷���true,����false</returns>
        public bool funBln_Limited(string LimitID, string UserLimit)
        {
            string strTempUserLimit;
            strTempUserLimit = "," + UserLimit + ",";//�����Ϊ�˷�ֹ�û�Ȩ�����߱���û�д���,�ţ����ԣ������ٸ����ϡ���Ϊ�������Ҳ�������������
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
