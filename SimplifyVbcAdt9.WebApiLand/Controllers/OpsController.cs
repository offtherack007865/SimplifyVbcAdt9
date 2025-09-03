using log4net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using SimplifyVbcAdt9.Data.Models;

namespace SimplifyVbcAdt9.WebApiLand.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OpsController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(OpsController));

        public OpsController(SimplifyVbcAdt8Context inputSimplifyVbcAdt8Context)
        {
            MyContext = inputSimplifyVbcAdt8Context;

            log.Info($"Start of OpsController Connection String:  {MyContext.MyConnectionString}");

        }
        public SimplifyVbcAdt8Context MyContext { get; set; }


        // GET /api/Ops/qy_GetHumanaCensusConfig?inputApplicationName=SimplifyVbcAdt&inputType=Default&inputProcessName=HumanaCensusAdt&inputNameFilter=NULL&inputUser=AppUser
        [HttpGet]
        public qy_GetHumanaCensusConfigOutput
                    qy_GetHumanaCensusConfig
                    (
                       [FromQuery] string inputApplicationName
                      , [FromQuery] string inputType
                      , [FromQuery] string inputProcessName
                      , [FromQuery] string inputNameFilter
                      , [FromQuery] string inputUser
                    )
        {
            qy_GetHumanaCensusConfigOutput
                returnOutput =
                    new qy_GetHumanaCensusConfigOutput();

            string sql = $"adt.qy_GetHumanaCensusConfig @inputApplicationName, @inputType, @inputProcessName, @inputNameFilter, @inputUser";

            List<SqlParameter> parms = new List<SqlParameter>();

            /* @inputApplicationName [varchar] (128)
  ,@inputType [varchar] (50)  
  ,@inputProcessName [varchar] (128)
  ,@inputNameFilter [varchar] (128)
  ,@inputUser [varchar] (50)
             */

            // @inputApplicationName [varchar] (128)
            SqlParameter parm =
                new SqlParameter
                {
                    ParameterName = "@inputApplicationName",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Size = 128,
                    Value = inputApplicationName
                };
            parms.Add(parm);

            // ,@inputType [varchar] (50) 
            parm =
                new SqlParameter
                {
                    ParameterName = "@inputType",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Size = 50,
                    Value = inputType
                };
            parms.Add(parm);

            // ,@inputProcessName [varchar] (128)
            parm =
                new SqlParameter
                {
                    ParameterName = "@inputProcessName",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Size = 128,
                    Value = inputProcessName
                };
            parms.Add(parm);

            // @inputNameFilter [varchar] (128)
            parm =
                new SqlParameter
                {
                    ParameterName = "@inputNameFilter",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Size = 128,
                    Value = inputNameFilter
                };
            parms.Add(parm);

            // @inputUser [varchar] (128)
            parm =
                new SqlParameter
                {
                    ParameterName = "@inputUser",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Size = 50,
                    Value = inputUser
                };
            parms.Add(parm);


            try
            {
                returnOutput.qy_GetHumanaCensusConfigOutputColumnsList =
                    MyContext
                    .qy_GetHumanaCensusConfigOutputColumnsList
                    .FromSqlRaw<qy_GetHumanaCensusConfigOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }

        // GET /api/Ops/dd_HumanaCensus
        [HttpGet]
        public dd_HumanaCensusOutput
                    dd_HumanaCensus
                    (
                    )
        {
            dd_HumanaCensusOutput
                returnOutput =
                    new dd_HumanaCensusOutput();

            string sql = $"adt.dd_HumanaCensus";

            List<SqlParameter> parms = new List<SqlParameter>();

            try
            {
                returnOutput.dd_TruncateHumanaCensusOutputColumnsList =
                    MyContext
                    .dd_TruncateHumanaCensusOutputColumnsList
                    .FromSqlRaw<dd_HumanaCensusOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }

        // GET /api/Ops/di_FinalizeHumanaCensus?inputFullFilename=test.txt
        [HttpGet]
        public di_FinalizeHumanaCensusOutput
                    di_FinalizeHumanaCensus
                    (
                        string inputFullFilename
                    )
        {
            di_FinalizeHumanaCensusOutput
                returnOutput =
                    new di_FinalizeHumanaCensusOutput();

            string sql = $"adt.di_FinalizeHumanaCensus @inputFullFilename";

            List<SqlParameter> parms = new List<SqlParameter>();
            SqlParameter parm =
                new SqlParameter
                {
                    ParameterName = "@inputFullFilename",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Size = 300,
                    Value = inputFullFilename
                };
            parms.Add(parm);

            try
            {
                returnOutput.di_FinalizeHumanaCensusOutputColumnsList =
                    MyContext
                    .di_FinalizeHumanaCensusOutputColumnsList
                    .FromSqlRaw<di_FinalizeHumanaCensusOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }

        // GET /api/Ops/qy_GetHumanaCensusMaster
        [HttpGet]
        public qy_GetHumanaCensusMasterOutput
                    qy_GetHumanaCensusMaster
                    (
                    )
        {
            qy_GetHumanaCensusMasterOutput
                returnOutput =
                    new qy_GetHumanaCensusMasterOutput();

            string sql = $"adt.qy_GetHumanaCensusMaster";

            List<SqlParameter> parms = new List<SqlParameter>();

            try
            {
                returnOutput.qy_GetHumanaCensusAdtMasterOutputColumnsList =
                    MyContext
                    .qy_GetHumanaCensusMasterOutputColumnsList
                    .FromSqlRaw<qy_GetHumanaCensusMasterOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }







































        // GET /api/Ops/qy_GetPointClickCareConfig
        [HttpGet]
        public qy_GetPointClickCareConfigOutput
                    qy_GetPointClickCareConfig
                    ()
        {
            qy_GetPointClickCareConfigOutput
                returnOutput =
                    new qy_GetPointClickCareConfigOutput();

            string sql = $"adt.qy_GetPointClickCareConfig";

            List<SqlParameter> parms = new List<SqlParameter>();

            try
            {
                returnOutput.qy_GetPointClickCareConfigOutputColumnsList =
                    MyContext
                    .qy_GetPointClickCareConfigOutputColumnsList
                    .FromSqlRaw<qy_GetPointClickCareConfigOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }

        // GET /api/Ops/dd_PointClickCare
        [HttpGet]
        public dd_PointClickCareOutput
                    dd_PointClickCare
                    (
                    )
        {
            dd_PointClickCareOutput
                returnOutput =
                    new dd_PointClickCareOutput();

            string sql = $"adt.dd_PointClickCare";

            List<SqlParameter> parms = new List<SqlParameter>();

            try
            {
                returnOutput.dd_PointClickCareOutputColumnsList =
                    MyContext
                    .dd_PointClickCareOutputColumnsList
                    .FromSqlRaw<dd_PointClickCareOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }

        // GET /api/Ops/di_PointClickCare?inputFullFilename=test.txt
        [HttpGet]
        public di_PointClickCareOutput
                    di_PointClickCare
                    (
                        string inputFullFilename
                    )
        {
            di_PointClickCareOutput
                returnOutput =
                    new di_PointClickCareOutput();

            string sql = $"adt.di_PointClickCare @inputFullFilename";

            List<SqlParameter> parms = new List<SqlParameter>();
            SqlParameter parm =
                new SqlParameter
                {
                    ParameterName = "@inputFullFilename",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Size = 300,
                    Value = inputFullFilename
                };
            parms.Add(parm);

            try
            {
                returnOutput.di_PointClickCareOutputColumnsList =
                    MyContext
                    .di_PointClickCareOutputColumnsList
                    .FromSqlRaw<di_PointClickCareOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }

        // GET /api/Ops/qy_GetPointClickCare
        [HttpGet]
        public qy_GetPointClickCareOutput
                    qy_GetPointClickCare
                    (
                    )
        {
            qy_GetPointClickCareOutput
                returnOutput =
                    new qy_GetPointClickCareOutput();

            string sql = $"adt.qy_GetPointClickCare";

            List<SqlParameter> parms = new List<SqlParameter>();

            try
            {
                returnOutput.qy_GetPointClickCareOutputColumnsList =
                    MyContext
                    .qy_GetPointClickCareOutputColumnsList
                    .FromSqlRaw<qy_GetPointClickCareOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }










        // GET /api/Ops/qy_GetEthinConfig
        [HttpGet]
        public qy_GetEthinConfigOutput
                    qy_GetEthinConfig
                    ()
        {
            qy_GetEthinConfigOutput
                returnOutput =
                    new qy_GetEthinConfigOutput();

            string sql = $"adt.qy_GetEthinConfig";

            List<SqlParameter> parms = new List<SqlParameter>();

            try
            {
                returnOutput.qy_GetEthinConfigOutputColumnsList =
                    MyContext
                    .qy_GetEthinConfigOutputColumnsList
                    .FromSqlRaw<qy_GetEthinConfigOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }

        // GET /api/Ops/dd_Ethin
        [HttpGet]
        public dd_EthinOutput
                    dd_Ethin
                    (
                    )
        {
            dd_EthinOutput
                returnOutput =
                    new dd_EthinOutput();

            string sql = $"adt.dd_Ethin";

            List<SqlParameter> parms = new List<SqlParameter>();

            try
            {
                returnOutput.dd_EthinOutputColumnsList =
                    MyContext
                    .dd_EthinOutputColumnsList
                    .FromSqlRaw<dd_EthinOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }

        // GET /api/Ops/di_Ethin
        [HttpGet]
        public di_EthinOutput
                    di_Ethin
                    (
                    )
        {
            di_EthinOutput
                returnOutput =
                    new di_EthinOutput();

            string sql = $"adt.di_Ethin";

            List<SqlParameter> parms = new List<SqlParameter>();

            try
            {
                returnOutput.di_EthinOutputColumnsList =
                    MyContext
                    .di_EthinOutputColumnsList
                    .FromSqlRaw<di_EthinOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }

        // GET /api/Ops/qy_GetEthin
        [HttpGet]
        public qy_GetEthinOutput
                    qy_GetEthin
                    (
                    )
        {
            qy_GetEthinOutput
                returnOutput =
                    new qy_GetEthinOutput();

            string sql = $"adt.qy_GetEthin";

            List<SqlParameter> parms = new List<SqlParameter>();

            try
            {
                returnOutput.qy_GetEthinOutputColumnsList =
                    MyContext
                    .qy_GetEthinOutputColumnsList
                    .FromSqlRaw<qy_GetEthinOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }























        // GET /api/Ops/qy_GetHumanaConfig
        [HttpGet]
        public qy_GetHumanaConfigOutput
                    qy_GetHumanaConfig
                    ()
        {
            qy_GetHumanaConfigOutput
                returnOutput =
                    new qy_GetHumanaConfigOutput();

            string sql = $"adt.qy_GetHumanaConfig";

            List<SqlParameter> parms = new List<SqlParameter>();

            try
            {
                returnOutput.qy_GetHumanaConfigOutputColumnsList =
                    MyContext
                    .qy_GetHumanaConfigOutputColumnsList
                    .FromSqlRaw<qy_GetHumanaConfigOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }

        // GET /api/Ops/dd_Humana
        [HttpGet]
        public dd_HumanaOutput
                    dd_Humana
                    (
                    )
        {
            dd_HumanaOutput
                returnOutput =
                    new dd_HumanaOutput();

            string sql = $"adt.dd_Humana";

            List<SqlParameter> parms = new List<SqlParameter>();

            try
            {
                returnOutput.dd_HumanaOutputColumnsList =
                    MyContext
                    .dd_HumanaOutputColumnsList
                    .FromSqlRaw<dd_HumanaOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }

        // GET /api/Ops/di_Humana
        [HttpGet]
        public di_HumanaOutput
                    di_Humana
                    (
                    )
        {
            di_HumanaOutput
                returnOutput =
                    new di_HumanaOutput();

            string sql = $"adt.di_Humana";

            List<SqlParameter> parms = new List<SqlParameter>();

            try
            {
                returnOutput.di_HumanaOutputColumnsList =
                    MyContext
                    .di_HumanaOutputColumnsList
                    .FromSqlRaw<di_HumanaOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }

        // GET /api/Ops/qy_GetHumana
        [HttpGet]
        public qy_GetHumanaOutput
                    qy_GetHumana
                    (
                    )
        {
            qy_GetHumanaOutput
                returnOutput =
                    new qy_GetHumanaOutput();

            string sql = $"adt.qy_GetHumana";

            List<SqlParameter> parms = new List<SqlParameter>();

            try
            {
                returnOutput.qy_GetHumanaOutputColumnsList =
                    MyContext
                    .qy_GetHumanaOutputColumnsList
                    .FromSqlRaw<qy_GetHumanaOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }



























        // GET /api/Ops/dd_HumanaObs
        [HttpGet]
        public dd_HumanaObsOutput
                    dd_HumanaObs
                    (
                    )
        {
            dd_HumanaObsOutput
                returnOutput =
                    new dd_HumanaObsOutput();

            string sql = $"adt.dd_HumanaObs";

            List<SqlParameter> parms = new List<SqlParameter>();

            try
            {
                returnOutput.dd_HumanaObsOutputColumnsList =
                    MyContext
                    .dd_HumanaObsOutputColumnsList
                    .FromSqlRaw<dd_HumanaObsOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }

        // GET /api/Ops/di_HumanaObs
        [HttpGet]
        public di_HumanaObsOutput
                    di_HumanaObs
                    (
                    )
        {
            di_HumanaObsOutput
                returnOutput =
                    new di_HumanaObsOutput();

            string sql = $"adt.di_HumanaObs";

            List<SqlParameter> parms = new List<SqlParameter>();

            try
            {
                returnOutput.di_HumanaObsOutputColumnsList =
                    MyContext
                    .di_HumanaObsOutputColumnsList
                    .FromSqlRaw<di_HumanaObsOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }

        // GET /api/Ops/qy_GetHumanaObs
        [HttpGet]
        public qy_GetHumanaObsOutput
                    qy_GetHumanaObs
                    (
                    )
        {
            qy_GetHumanaObsOutput
                returnOutput =
                    new qy_GetHumanaObsOutput();

            string sql = $"adt.qy_GetHumanaObs";

            List<SqlParameter> parms = new List<SqlParameter>();

            try
            {
                returnOutput.qy_GetHumanaObsOutputColumnsList =
                    MyContext
                    .qy_GetHumanaObsOutputColumnsList
                    .FromSqlRaw<qy_GetHumanaObsOutputColumns>
                    (
                          sql
                        , parms.ToArray()
                    )
                    .ToList();
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;

                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  InnerException:  {ex.InnerException.Message}";
                }
                returnOutput.ErrorMessage = myErrorMessage;
                return returnOutput;
            }
            return returnOutput;
        }












































    }
}
