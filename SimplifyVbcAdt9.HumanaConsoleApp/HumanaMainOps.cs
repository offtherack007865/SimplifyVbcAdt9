using BulkInsert9.CallWebApiLand;
using BulkInsert9.Data.Models;
using log4net;
using SimplifyVbcAdt9.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.HumanaConsoleApp
{
    public class HumanaMainOps
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(HumanaMainOps));
        public HumanaMainOps(qy_GetHumanaConfigOutputColumns inputqy_GetHumanaConfigOutputColumns)
        {
            Myqy_GetHumanaConfigOutputColumns =
                inputqy_GetHumanaConfigOutputColumns;
        }

        public qy_GetHumanaConfigOutputColumns Myqy_GetHumanaConfigOutputColumns { get; set; }

    }
}
