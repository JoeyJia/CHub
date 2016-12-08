using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IdioSoft.Public
{
    public static class PublicModule
    {
        public static string CurrentFY
        {
            get
            {
                string strCurrentFY = "";
                if (DateTime.Now.Month >= 10)
                {
                    strCurrentFY = "FY" + DateTime.Now.AddYears(0).ToString("yy") + "/" + DateTime.Now.AddYears(1).ToString("yy");
                }
                else
                {
                    strCurrentFY = "FY" + DateTime.Now.AddYears(-1).ToString("yy") + "/" + DateTime.Now.AddYears(0).ToString("yy");
                }
                return strCurrentFY;
            }
        }
    }
}
