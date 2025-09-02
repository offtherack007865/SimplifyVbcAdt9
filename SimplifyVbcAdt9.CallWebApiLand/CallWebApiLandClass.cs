using log4net;
using Newtonsoft.Json;
using SimplifyVbcAdt9.Data.Models;

namespace SimplifyVbcAdt9.CallWebApiLand
{
    public class CallWebApiLandClass
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CallWebApiLandClass));

        public CallWebApiLandClass
        (
            string inputBaseWebApiUrl
        )
        {
            MyBaseWebApiUrl = inputBaseWebApiUrl;
        }
        public string MyBaseWebApiUrl { get; set; }

        // GET /api/Ops/qy_GetHumanaCensusConfig?inputApplicationName=SimplifyVbcAdt&inputType=Default&inputProcessName=HumanaCensusAdt&inputNameFilter=NULL&inputUser=AppUser
        public qy_GetHumanaCensusConfigOutput
                    qy_GetHumanaCensusConfig
                    (
                        string inputApplicationName
                      , string inputType
                      , string inputProcessName
                      , string inputNameFilter
                      , string inputUser
                    )
        {
            qy_GetHumanaCensusConfigOutput
                returnOutput =
                    qy_GetHumanaCensusConfigAsync
                    (
                        inputApplicationName
                        , inputType
                        , inputProcessName
                        , inputNameFilter
                        , inputUser
                    )
                    .Result;

            return returnOutput;
        }

        public async Task<qy_GetHumanaCensusConfigOutput>
                        qy_GetHumanaCensusConfigAsync
                        (
                            string inputApplicationName
                            , string inputType
                            , string inputProcessName
                            , string inputNameFilter
                            , string inputUser
                        )
        {
            log.Info($"In qy_GetNrcConfigAsync");
            qy_GetHumanaCensusConfigOutput
                returnOutput =
                    new qy_GetHumanaCensusConfigOutput();

            string myCompleteUrl = $"{MyBaseWebApiUrl}/api/Ops/qy_GetHumanaCensusConfig?inputApplicationName={inputApplicationName}&inputType={inputType}&inputProcessName={inputProcessName}&inputNameFilter={inputNameFilter}&inputUser={inputUser}";
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromHours(1);

                    var result = await client.GetAsync(myCompleteUrl);
                    var response = await result.Content.ReadAsStringAsync();
                    returnOutput = JsonConvert.DeserializeObject<qy_GetHumanaCensusConfigOutput>(response);
                }
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;
                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  Inner Exception:  {ex.InnerException.Message}";
                }
                return returnOutput;
            }

            return returnOutput;
        }


        // GET /api/Ops/dd_HumanaCensus
        public dd_HumanaCensusOutput
                    dd_HumanaCensus
                    (
                    )
        {
            dd_HumanaCensusOutput
                returnOutput =
                    dd_HumanaCensusAsync
                    (
                    )
                    .Result;

            return returnOutput;
        }

        public async Task<dd_HumanaCensusOutput>
                        dd_HumanaCensusAsync
                        (
                        )
        {
            log.Info($"In dd_HumanaCensusAsync");
            dd_HumanaCensusOutput
                returnOutput =
                    new dd_HumanaCensusOutput();

            string myCompleteUrl = $"{MyBaseWebApiUrl}/api/Ops/dd_HumanaCensus";
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromHours(1);

                    var result = await client.GetAsync(myCompleteUrl);
                    var response = await result.Content.ReadAsStringAsync();
                    returnOutput = JsonConvert.DeserializeObject<dd_HumanaCensusOutput>(response);
                }
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;
                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  Inner Exception:  {ex.InnerException.Message}";
                }
                return returnOutput;
            }

            return returnOutput;
        }


        // GET /api/Ops/di_FinalizeHumanaCensus?inputFullFilename=test.txt
        public di_FinalizeHumanaCensusOutput
                    di_FinalizeHumanaCensus
                    (
                       string inputFullFilename
                    )
        {
            di_FinalizeHumanaCensusOutput
                returnOutput =
                    di_FinalizeHumanaCensusAsync
                    (
                        inputFullFilename
                    )
                    .Result;

            return returnOutput;
        }

        public async Task<di_FinalizeHumanaCensusOutput>
                        di_FinalizeHumanaCensusAsync
                        (
                            string inputFullFilename
                        )
        {
            log.Info($"In di_FinalizeHumanaCensusAsync");
            di_FinalizeHumanaCensusOutput
                returnOutput =
                    new di_FinalizeHumanaCensusOutput();

            string myCompleteUrl = $"{MyBaseWebApiUrl}/api/Ops/di_FinalizeHumanaCensus?inputFullFilename={inputFullFilename}";
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromHours(1);

                    var result = await client.GetAsync(myCompleteUrl);
                    var response = await result.Content.ReadAsStringAsync();
                    returnOutput = JsonConvert.DeserializeObject<di_FinalizeHumanaCensusOutput>(response);
                }
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;
                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  Inner Exception:  {ex.InnerException.Message}";
                }
                return returnOutput;
            }

            return returnOutput;
        }








        // GET /api/Ops/qy_GetHumanaCensusMaster
        public qy_GetHumanaCensusMasterOutput
                    qy_GetHumanaCensusMaster
                    (
                    )
        {
            qy_GetHumanaCensusMasterOutput
                returnOutput =
                    qy_GetHumanaCensusMasterAsync
                    (
                    )
                    .Result;

            return returnOutput;
        }

        public async Task<qy_GetHumanaCensusMasterOutput>
                        qy_GetHumanaCensusMasterAsync
                        (
                        )
        {
            log.Info($"In qy_GetHumanaCensusMasterAsync");
            qy_GetHumanaCensusMasterOutput
                returnOutput =
                    new qy_GetHumanaCensusMasterOutput();

            string myCompleteUrl = $"{MyBaseWebApiUrl}/api/Ops/qy_GetHumanaCensusMaster";
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromHours(1);

                    var result = await client.GetAsync(myCompleteUrl);
                    var response = await result.Content.ReadAsStringAsync();
                    returnOutput = JsonConvert.DeserializeObject<qy_GetHumanaCensusMasterOutput>(response);
                }
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;
                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  Inner Exception:  {ex.InnerException.Message}";
                }
                return returnOutput;
            }

            return returnOutput;
        }










        // GET /api/Ops/qy_GetPointClickCareConfig
        public qy_GetPointClickCareConfigOutput
                    qy_GetPointClickCareConfig
                    ()
        {
            qy_GetPointClickCareConfigOutput
                returnOutput =
                    qy_GetPointClickCareConfigAsync
                    (
                    )
                    .Result;

            return returnOutput;
        }

        public async Task<qy_GetPointClickCareConfigOutput>
                        qy_GetPointClickCareConfigAsync
                        (
                        )
        {
            log.Info($"In qy_GetNrcConfigAsync");
            qy_GetPointClickCareConfigOutput
                returnOutput =
                    new qy_GetPointClickCareConfigOutput();

            string myCompleteUrl = $"{MyBaseWebApiUrl}/api/Ops/qy_GetPointClickCareConfig";
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromHours(1);

                    var result = await client.GetAsync(myCompleteUrl);
                    var response = await result.Content.ReadAsStringAsync();
                    returnOutput = JsonConvert.DeserializeObject<qy_GetPointClickCareConfigOutput>(response);
                }
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;
                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  Inner Exception:  {ex.InnerException.Message}";
                }
                return returnOutput;
            }

            return returnOutput;
        }


        // GET /api/Ops/dd_PointClickCare
        public dd_PointClickCareOutput
                    dd_PointClickCare
                    (
                    )
        {
            dd_PointClickCareOutput
                returnOutput =
                    dd_PointClickCareAsync
                    (
                    )
                    .Result;

            return returnOutput;
        }

        public async Task<dd_PointClickCareOutput>
                        dd_PointClickCareAsync
                        (
                        )
        {
            log.Info($"In dd_PointClickCareAsync");
            dd_PointClickCareOutput
                returnOutput =
                    new dd_PointClickCareOutput();

            string myCompleteUrl = $"{MyBaseWebApiUrl}/api/Ops/dd_PointClickCare";
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromHours(1);

                    var result = await client.GetAsync(myCompleteUrl);
                    var response = await result.Content.ReadAsStringAsync();
                    returnOutput = JsonConvert.DeserializeObject<dd_PointClickCareOutput>(response);
                }
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;
                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  Inner Exception:  {ex.InnerException.Message}";
                }
                return returnOutput;
            }

            return returnOutput;
        }


        // GET /api/Ops/di_PointClickCare?inputFullFilename=test.txt
        public di_PointClickCareOutput
                    di_PointClickCare
                    (
                       string inputFullFilename
                    )
        {
            di_PointClickCareOutput
                returnOutput =
                    di_PointClickCareAsync
                    (
                        inputFullFilename
                    )
                    .Result;

            return returnOutput;
        }

        public async Task<di_PointClickCareOutput>
                        di_PointClickCareAsync
                        (
                            string inputFullFilename
                        )
        {
            log.Info($"In di_PointClickCareAsync");
            di_PointClickCareOutput
                returnOutput =
                    new di_PointClickCareOutput();

            string myCompleteUrl = $"{MyBaseWebApiUrl}/api/Ops/di_PointClickCare?inputFullFilename={inputFullFilename}";
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromHours(1);

                    var result = await client.GetAsync(myCompleteUrl);
                    var response = await result.Content.ReadAsStringAsync();
                    returnOutput = JsonConvert.DeserializeObject<di_PointClickCareOutput>(response);
                }
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;
                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  Inner Exception:  {ex.InnerException.Message}";
                }
                return returnOutput;
            }

            return returnOutput;
        }








        // GET /api/Ops/qy_GetPointClickCare
        public qy_GetPointClickCareOutput
                    qy_GetPointClickCare
                    (
                    )
        {
            qy_GetPointClickCareOutput
                returnOutput =
                    qy_GetPointClickCareAsync
                    (
                    )
                    .Result;

            return returnOutput;
        }

        public async Task<qy_GetPointClickCareOutput>
                        qy_GetPointClickCareAsync
                        (
                        )
        {
            log.Info($"In qy_GetPointClickCareAsync");
            qy_GetPointClickCareOutput
                returnOutput =
                    new qy_GetPointClickCareOutput();

            string myCompleteUrl = $"{MyBaseWebApiUrl}/api/Ops/qy_GetPointClickCare";
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromHours(1);

                    var result = await client.GetAsync(myCompleteUrl);
                    var response = await result.Content.ReadAsStringAsync();
                    returnOutput = JsonConvert.DeserializeObject<qy_GetPointClickCareOutput>(response);
                }
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;
                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  Inner Exception:  {ex.InnerException.Message}";
                }
                return returnOutput;
            }

            return returnOutput;
        }

        // GET /api/Ops/qy_GetEthinConfig
        public qy_GetEthinConfigOutput
                    qy_GetEthinConfig
                    ()
        {
            qy_GetEthinConfigOutput
                returnOutput =
                    qy_GetEthinConfigAsync
                    (
                    )
                    .Result;

            return returnOutput;
        }

        public async Task<qy_GetEthinConfigOutput>
                        qy_GetEthinConfigAsync
                        (
                        )
        {
            log.Info($"In qy_GetNrcConfigAsync");
            qy_GetEthinConfigOutput
                returnOutput =
                    new qy_GetEthinConfigOutput();

            string myCompleteUrl = $"{MyBaseWebApiUrl}/api/Ops/qy_GetEthinConfig";
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromHours(1);

                    var result = await client.GetAsync(myCompleteUrl);
                    var response = await result.Content.ReadAsStringAsync();
                    returnOutput = JsonConvert.DeserializeObject<qy_GetEthinConfigOutput>(response);
                }
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;
                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  Inner Exception:  {ex.InnerException.Message}";
                }
                return returnOutput;
            }

            return returnOutput;
        }


        // GET /api/Ops/dd_Ethin
        public dd_EthinOutput
                    dd_Ethin
                    (
                    )
        {
            dd_EthinOutput
                returnOutput =
                    dd_EthinAsync
                    (
                    )
                    .Result;

            return returnOutput;
        }

        public async Task<dd_EthinOutput>
                        dd_EthinAsync
                        (
                        )
        {
            log.Info($"In dd_EthinAsync");
            dd_EthinOutput
                returnOutput =
                    new dd_EthinOutput();

            string myCompleteUrl = $"{MyBaseWebApiUrl}/api/Ops/dd_Ethin";
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromHours(1);

                    var result = await client.GetAsync(myCompleteUrl);
                    var response = await result.Content.ReadAsStringAsync();
                    returnOutput = JsonConvert.DeserializeObject<dd_EthinOutput>(response);
                }
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;
                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  Inner Exception:  {ex.InnerException.Message}";
                }
                return returnOutput;
            }

            return returnOutput;
        }


        // GET /api/Ops/di_Ethin
        public di_EthinOutput
                    di_Ethin
                    (
                    )
        {
            di_EthinOutput
                returnOutput =
                    di_EthinAsync
                    (
                    )
                    .Result;

            return returnOutput;
        }

        public async Task<di_EthinOutput>
                        di_EthinAsync
                        (
                        )
        {
            log.Info($"In di_EthinAsync");
            di_EthinOutput
                returnOutput =
                    new di_EthinOutput();

            string myCompleteUrl = $"{MyBaseWebApiUrl}/api/Ops/di_Ethin";
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromHours(1);

                    var result = await client.GetAsync(myCompleteUrl);
                    var response = await result.Content.ReadAsStringAsync();
                    returnOutput = JsonConvert.DeserializeObject<di_EthinOutput>(response);
                }
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;
                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  Inner Exception:  {ex.InnerException.Message}";
                }
                return returnOutput;
            }

            return returnOutput;
        }

        // GET /api/Ops/qy_GetEthin
        public qy_GetEthinOutput
                    qy_GetEthin
                    (
                    )
        {
            qy_GetEthinOutput
                returnOutput =
                    qy_GetEthinAsync
                    (
                    )
                    .Result;

            return returnOutput;
        }

        public async Task<qy_GetEthinOutput>
                        qy_GetEthinAsync
                        (
                        )
        {
            log.Info($"In qy_GetEthinAsync");
            qy_GetEthinOutput
                returnOutput =
                    new qy_GetEthinOutput();

            string myCompleteUrl = $"{MyBaseWebApiUrl}/api/Ops/qy_GetEthin";
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromHours(1);

                    var result = await client.GetAsync(myCompleteUrl);
                    var response = await result.Content.ReadAsStringAsync();
                    returnOutput = JsonConvert.DeserializeObject<qy_GetEthinOutput>(response);
                }
            }
            catch (Exception ex)
            {
                returnOutput.IsOk = false;
                string myErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    myErrorMessage = $"{myErrorMessage}.  Inner Exception:  {ex.InnerException.Message}";
                }
                return returnOutput;
            }

            return returnOutput;
        }
    }
}
