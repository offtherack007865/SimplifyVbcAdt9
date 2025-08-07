using log4net;
using SimplifyVbcAdt9.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.PointClickCareConsoleApp
{
    public class MainOps
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainOps));
        public MainOps
                (
                    qy_GetHumanaCensusConfigOutputColumns inputConfigOps
                    , List<string> inputFullFilenameList
                )
        {
            MyConfigOptions = inputConfigOps;
            MyFullFilenameList = inputFullFilenameList;
        }
        public qy_GetHumanaCensusConfigOutputColumns MyConfigOptions { get; set; }
        public string MyFullArchiveFilename { get; set; }
        public List<string> MyFullFilenameList { get; set; }
    }
}
