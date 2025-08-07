using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.PointClickCareConsoleApp
{
    public class ReadInConfigOptions
    {
        public ReadInConfigOptions(Microsoft.Extensions.Configuration.IConfiguration myConfig)
        {
            MyConfig = myConfig;
        }

        public Microsoft.Extensions.Configuration.IConfiguration MyConfig { get; set; }

        public SimplifyVbcAdt9.Data.Models.ConfigOptions ReadIn()
        {
            SimplifyVbcAdt9.Data.Models.ConfigOptions
                returnConfigOptions =
                new SimplifyVbcAdt9.Data.Models.ConfigOptions();

            returnConfigOptions.BaseWebUrl =
                MyConfig.GetValue<string>(SimplifyVbcAdt9.Data.MyConstants.BaseWebUrl);
            return returnConfigOptions;

        }
    }
}
