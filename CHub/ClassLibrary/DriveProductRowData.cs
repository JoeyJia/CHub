using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHub.ClassLibrary
{
    /// <summary>
    /// 得到每行的内容和价格
    /// </summary>
    public class DriveProductRowData
    {
        /// <summary>
        /// 每行选件文本，格式为产品名,产品名,产品名...，此文本主要用来做有效性判断时使用
        /// </summary>
        public string OptionsText;

        /// <summary>
        /// 每个产品的MLFB和选件的信息，第一个为电机，其后是配件
        /// </summary>
        public List<ClassLibrary.DriveProductRowItem> lstProduct;
        ///// <summary>
        ///// 选择的MLFB和选件的总数量
        ///// </summary>
        //public int TotalProductCount;
        /// <summary>
        /// 用户需要的数量(这个纯粹是用户填写的数量，真正运算是使用的ProductRowItem中的ProductCount)
        /// </summary>
        public int QTY;

        /// <summary>
        /// 产品折扣率
        /// </summary>
        public decimal LPFactor;//DiscountFactor;
        ///// <summary>
        ///// 计算出来的产品价格(还未计算折扣的)
        ///// </summary>
        //public decimal TotalPrice;

        /// <summary>
        /// 计算出来的表价(还未计算折扣的)
        /// </summary>
        public decimal TotalLP;
        /// <summary>
        /// 计算出来的成本价(还未计算折扣的)
        /// </summary>
        public decimal TotalTP;

        /// <summary>
        /// 将折扣加上后的表价
        /// </summary>
        public decimal TotalDisLP;
        ///// <summary>
        ///// 将折扣加上后的成本价
        ///// </summary>
        //public decimal TotalDisTP;

        /// <summary>
        /// 此行数据是否是有效数据，True：有效；False：无效
        /// </summary>
        public bool IsValid;

        /// <summary>
        /// TP的折扣(这个是根据MLFB号在Drive_Info_TPFactor表中得到的折扣)
        /// </summary>
        public decimal TPFactor;
        /// <summary>
        /// ZXY模块使用
        /// </summary>
        public List<string> lstVItem = new List<string>();
    }
}