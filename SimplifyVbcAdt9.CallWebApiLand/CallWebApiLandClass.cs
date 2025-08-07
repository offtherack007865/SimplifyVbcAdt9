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









    }
}
