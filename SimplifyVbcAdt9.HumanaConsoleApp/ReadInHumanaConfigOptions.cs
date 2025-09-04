using Microsoft.Extensions.Configuration;
using SimplifyVbcAdt9.Data;
using SimplifyVbcAdt9.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.HumanaConsoleApp
{
    public class ReadInHumanaConfigOptions
    {
        public ReadInHumanaConfigOptions(IConfiguration myConfig)
        {
            MyConfig = myConfig;
        }

        public Microsoft.Extensions.Configuration.IConfiguration MyConfig { get; set; }

        public HumanaConfigOptions ReadIn()
        {
            HumanaConfigOptions
                returnConfigOptions =
                new HumanaConfigOptions();

            returnConfigOptions.ConfigOptionsBaseWebUrl =
                MyConfig.GetValue<string>(MyConstants.BaseWebUrl);

            return returnConfigOptions;
        }
    }

}
