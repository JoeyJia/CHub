using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace CHub.ClassLibrary
{
    public class ProductRowItem
    {
        /// <summary>
        /// 每种产品名称
        /// </summary>
        public string ProductText;
        ///// <summary>
        ///// 每种产品的说明
        ///// </summary>
        //public string ProductDeacription;
        /// <summary>
        /// 产品的数量
        /// </summary>
        public int ProductCount;
        /////// <summary>
        /////// 产品折扣率
        /////// </summary>
        ////public decimal DiscountFactor;
        ///// <summary>
        ///// 产品价格(数据库中的)
        ///// </summary>
        //public decimal ProductPrice;
        /// <summary>
        /// 产品的表价格
        /// </summary>
        public decimal ItemLP;
        /// <summary>
        /// 产品的成本价
        /// </summary>
        public decimal ItemTP;
        ///// <summary>
        ///// 是否需要折扣，False：不需要折扣；True：需要折扣
        ///// </summary>
        //public bool IsDiscountLP;
        ///// <summary>
        ///// 是否需要折扣，False：不需要折扣；True：需要折扣
        ///// </summary>
        //public bool IsDiscountTP;
        ///// <summary>
        ///// 产品的折扣率(有可能选件需要折扣，有可能不需要折扣)
        ///// </summary>
        //public decimal ItemDiscount;
        ///// <summary>
        ///// LP的折扣
        ///// </summary>
        //public decimal LPFactor;
        ///// <summary>
        ///// TP的折扣
        ///// </summary>
        //public decimal TPFactor;

        /// <summary>
        /// 此MLFB或者Options数据是否是正确数据，True：正确；False：错误
        /// </summary>
        public bool IsRight;
        /// <summary>
        /// 如果是错误的，保存错误信息
        /// </summary>
        public string ErrorInfo;
        /// <summary>
        /// 此选件是否是特殊选件，True：是特殊选件；False：不是特殊选件
        /// </summary>
        public bool IsSpecialOption = false;
        /// <summary>
        /// 如果是特殊选件，那么用来乘以价格的百分比
        /// </summary>
        public decimal SpecialOptionPercentage = 0;
    }
}
