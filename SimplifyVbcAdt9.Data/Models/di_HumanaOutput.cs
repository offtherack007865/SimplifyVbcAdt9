using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class di_HumanaOutput
    {
        public di_HumanaOutput()
        {
            IsOk = true;
            ErrorMessage = string.Empty;
            di_HumanaOutputColumnsList =
                new List<di_HumanaOutputColumns>();
        }
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public List<di_HumanaOutputColumns>
            di_HumanaOutputColumnsList
            { get; set; }
    }
}
