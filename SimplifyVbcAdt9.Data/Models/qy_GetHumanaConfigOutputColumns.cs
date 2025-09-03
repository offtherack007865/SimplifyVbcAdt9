using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifyVbcAdt9.Data.Models
{
    public class qy_GetHumanaConfigOutputColumns
    {
        public string ReadDirectory { get; set; }
        public string InputFilenameContainsString { get; set; }
        public string LineToLookForSearchString { get; set; }
        public string SearchString { get; set; }
        public string InputFileArchiveDirectory { get; set; }
        public string OutputFileToSummitAdtProcessingReadDirectory { get; set; }
        public string OutputFileToAthenaReadDirectory { get; set; }
        public string OutputFileToLightbeamReadDirectory { get; set; }
        public string OutputFileToSummitAdtProcessingArchiveDirectory { get; set; }
        public string OutputFileToAthenaArchiveDirectory { get; set; }
        public string OutputFileToLightbeamArchiveDirectory { get; set; }
        public string SimplifyVbcAdtBaseWebApiUrl { get; set; }
        public string ExcelTemplateFullFilename { get; set; }
        public string ImportArchiveFolder { get; set; }
        public string BulkInsertConnectionString { get; set; }
        public string BulkInsertDbName { get; set; }
        public string BulkInsertDbTableName { get; set; }
        public string BulkInsertBaseWebApiUrl { get; set; }
        public string EmailBaseWebApiUrl { get; set; }
        public string EmailFromAddress { get; set; }
        public string Emailees { get; set; }
    }
}
