using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class di_EthinOutput
    {
        public di_EthinOutput() 
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            di_EthinOutputColumnsList =
                new List<di_EthinOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<di_EthinOutputColumns>
            di_EthinOutputColumnsList
            { get; set; }
    }
}
