using BulkInsert9.CallWebApiLand;
using BulkInsert9.Data.Models;

using log4net;

using SimplifyVbcAdt9.Data.Models;

using Spire.Xls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.EthinConsoleApp
{
    public class EthinMainOps
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(EthinMainOps));
        public EthinMainOps(qy_GetEthinConfigOutputColumns inputqy_GetEthinConfigOutputColumns)
        {
            Myqy_GetEthinConfigOutputColumns =
                inputqy_GetEthinConfigOutputColumns;
        }

        public qy_GetEthinConfigOutputColumns Myqy_GetEthinConfigOutputColumns { get; set; }

    }
}


