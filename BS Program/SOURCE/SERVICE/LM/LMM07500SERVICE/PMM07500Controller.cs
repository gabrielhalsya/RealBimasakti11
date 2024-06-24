using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Runtime.CompilerServices;
using R_OpenTelemetry;
using System.Diagnostics;
using System.Reflection;
using PMM07500COMMON.Interfaces;
using PMM07500COMMON.DTO_s.Stamp_Code;
using PMM07500COMMON;
using PMM07500BACK;

namespace PMM07500SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMM07500Controller : ControllerBase, IPMM07500
    {
        private LoggerPMM07500 _logger;

        private readonly ActivitySource _activitySource;

        public PMM07500Controller(ILogger<LoggerPMM07500> logger)
        {
            //initiate
            LoggerPMM07500.R_InitializeLogger(logger);
            _logger = LoggerPMM07500.R_GetInstanceLogger();
            _activitySource = PMM07500Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        [HttpPost]
        public IAsyncEnumerable<PMM07500GridDTO> GetStampList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PMM07500GridDTO> loRtnTemp = null;
            PMM07500Cls loCls;
            try
            {
                loCls = new PMM07500Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetStampRateList(new PMM07500ParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMM07500ContextConstant.CPROPERTY_ID),
                    CLANGUAGE_ID = R_BackGlobalVar.CULTURE
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return StreamListHelper(loRtnTemp);
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMM07500GridDTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PMM07500GridDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMM07500GridDTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PMM07500GridDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMM07500GridDTO> poParameter)
        {
            throw new NotImplementedException();
        }


        #region helper

        private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> poList)
        {
            foreach (T loEntity in poList)
            {
                yield return loEntity;
            }
        }

        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);
        #endregion
    }

}
