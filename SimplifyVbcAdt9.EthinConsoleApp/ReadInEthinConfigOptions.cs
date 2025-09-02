using Microsoft.Extensions.Configuration;
using SimplifyVbcAdt9.Data;
using SimplifyVbcAdt9.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.EthinConsoleApp
{
    public class ReadInEthinConfigOptions
    {
        public ReadInEthinConfigOptions(IConfiguration myConfig)
        {
            MyConfig = myConfig;
        }

        public Microsoft.Extensions.Configuration.IConfiguration MyConfig { get; set; }

        public EthinConfigOptions ReadIn()
        {
            EthinConfigOptions
                returnConfigOptions =
                new EthinConfigOptions();

            returnConfigOptions.ConfigOptionsBaseWebUrl =
                MyConfig.GetValue<string>(MyConstants.BaseWebUrl);

            return returnConfigOptions;
        }
    }

}
