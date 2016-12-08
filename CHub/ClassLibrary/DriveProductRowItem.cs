using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHub.ClassLibrary
{
    /// <summary>
    /// 单个MLFB或者选件的信息
    /// </summary>
    public class DriveProductRowItem
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
        /// 产品的表价格(数据库中的)
        /// </summary>
        public decimal ItemLP;
        /// <summary>
        /// 产品的成本价(数据库中的)
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
    }
}