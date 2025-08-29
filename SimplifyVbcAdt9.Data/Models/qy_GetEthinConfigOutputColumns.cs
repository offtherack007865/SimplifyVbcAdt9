using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class qy_GetEthinConfigOutputColumns
    {
        public string ReadDirectory { get; set; }
        public string InputFilenameContainsString { get; set; }
        public int StartLineNumber { get; set; }
        public int StartColumnNumber { get; set; }
        public string InputFileArchiveDirectory { get; set; }
        public string OutputFileArchiveDirectory { get; set; }
        public string ToSimpleFileMoverReadDirectory { get; set; }
        public string SimplifyVbcAdtBaseWebApiUrl { get; set; }
        public string ExcelTemplateFullFilename { get; set; }
        public string ImportArchiveFolder { get; set; }
        public string BulkInsertConnectionString { get; set; }
        public string BulkInsertDbName { get; set; }
        public string BulkInsertDbTableName { get; set; }
        public string BulkInsertBaseWebApiUrl { get; set; }
        public string EmailBaseWebApiUrl { get; set; }
        public string EmailFromAddress { get; set; }

    }
}
