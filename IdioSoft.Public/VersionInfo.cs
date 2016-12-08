using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdioSoft.Public
{
    public static class VersionInfo
    {
        #region "版本信息"
        static string _Version = "1.00beta";
        public static string Version
        {
            get
            {
                return _Version;
            }
        }
        #endregion

    }
}
