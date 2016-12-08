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
using System.Collections.Generic;

namespace CHub.ClassLibrary
{
    public class ProductRowData
    {
        /// <summary>
        /// 如果这个选件不在数据库中，那么用来进行判断的MLFB
        /// </summary>
        public string MLFB;

        /// <summary>
        /// 每行选件文本，格式为产品名,产品名,产品名...，此文本主要用来做有效性判断时使用
        /// </summary>
        public string OptionsText;

        /// <summary>
        /// 每个产品的MLFB和选件的信息，第一个为电机，其后是配件
        /// </summary>
        public List<ClassLibrary.ProductRowItem> lstProduct;
        /// <summary>
        /// 用户需要的数量(这个纯粹是用户填写的数量，真正运算是使用的ProductRowItem中的ProductCount)
        /// </summary>
        public int QTY;
        /// <summary>
        /// LP的折扣(界面上手动填写的折扣率)
        /// </summary>
        //public decimal DiscountFactor;
        public decimal LPFactor;
        /// <summary>
        /// MLFB不存在时，用来进一步判断的电压
        /// </summary>
        public decimal Voltage;

        /// <summary>
        /// TP的折扣(这个是根据MLFB号在CHub_Info_TPFactor表中得到的折扣)
        /// </summary>
        public decimal TPFactor;

        /// <summary>
        /// 全部MLFB和选件的LP(还未计算折扣的)
        /// </summary>
        public decimal TotalLP;
        /// <summary>
        /// 全部MLFB和选件的TP(TP不用再打折扣了)
        /// </summary>
        public decimal TotalTP;

        /// <summary>
        /// 将折扣加上后的表价
        /// </summary>
        public decimal TotalDisLP;
        /// <summary>
        /// 将折扣加上后的成本价(这个按照LP*TPFactor得到TP之后，再乘以界面上填写的Factor得到的价格)
        /// </summary>
        public decimal TotalDisTP;

        /// <summary>
        /// 此行数据是否是有效数据，True：有效；False：无效
        /// </summary>
        public bool IsValid;
        ///// <summary>
        ///// 此行数据从MLFB号就已经找不到，从而错误，True：正确；False：错误
        ///// </summary>
        //public bool IsRight;

        ///// <summary>
        ///// LP数据库设置的折扣
        ///// </summary>
        //public decimal LPFactor;
        ///// <summary>
        ///// ZXY模块使用
        ///// </summary>
        //public List<string> lstVItem = new List<string>();

        /// <summary>
        /// 这个MLFB对应的SPR号
        /// </summary>
        public string SPR;
        /// <summary>
        /// SPR中如果MLFB不存在时，用来进一步判断的电压
        /// </summary>
        public decimal SPRVoltage;
        /// <summary>
        /// SPR中，每个MLFB号的设计费(只用在1PQ4上面)
        /// </summary>
        public decimal SPROthersPerUnit;
        /// <summary>
        /// 整个SPR中，单独加的一次设计费(只用在1PQ4上面)
        /// </summary>
        public decimal SPROthersPerItem;
        /// <summary>
        /// 如果这个选件不在数据库中，那么用来进行判断的MLFB
        /// </summary>
        public string SPRMLFB;
        /// <summary>
        /// 每个MLFB的V70索引号
        /// </summary>
        public string V70Index;
    }
}
