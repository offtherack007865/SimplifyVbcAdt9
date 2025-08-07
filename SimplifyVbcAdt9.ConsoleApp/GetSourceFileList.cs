
using SimplifyVbcAdt9.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.ConsoleApp
{
    public class GetSourceFileList
    {
        public GetSourceFileList(qy_GetHumanaCensusConfigOutputColumns inputConfigOptions)
        {
            MyConfigOptions = inputConfigOptions;
        }
        public qy_GetHumanaCensusConfigOutputColumns MyConfigOptions { get; set; }

        public List<string> MyOutputListOfFullFilenames { get; set; }

        public void DoIt()
        {
            MyOutputListOfFullFilenames = Directory.GetFiles(MyConfigOptions.ReadDirectory, $"*{MyConfigOptions.InputFilenameContainsString}*.xlsx").ToList();
        }
    }
}
