using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR00200BACK;
using PMR00200COMMON;
using PMR00200COMMON.DTO_s;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PMR00200SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMR00210Controller : ControllerBase, IPMR00200
    {
        private PMR00200Logger _logger;

        private readonly ActivitySource _activitySource;

        public PMR00210Controller(ILogger<PMR00210Controller> logger)
        {
            //initiate
            PMR00200Logger.R_InitializeLogger(logger);
            _logger = PMR00200Logger.R_GetInstanceLogger();
            _activitySource = PMR00200Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        [HttpPost]
        public IAsyncEnumerable<PMR00200DTO> GetReportData()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PMR00200DTO> loRtnTemp = null;
            PMR00200Cls loCls;
            try
            {
                loCls = new PMR00200Cls();
                ShowLogExecute();

                loRtnTemp = loCls.GetReportData(new PMR00200ParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CFROM_DEPARTMENT_ID = R_Utility.R_GetStreamingContext<string>(PMR00200ContextConstant.CFROM_DEPARTMENT_ID),
                    CTO_DEPARTMENT_ID = R_Utility.R_GetStreamingContext<string>(PMR00200ContextConstant.CTO_DEPARTMENT_ID),
                    CFROM_SALESMAN_ID = R_Utility.R_GetStreamingContext<string>(PMR00200ContextConstant.CFROM_SALESMAN_ID),
                    CTO_SALESMAN_ID = R_Utility.R_GetStreamingContext<string>(PMR00200ContextConstant.CTO_SALESMAN_ID),
                    CFROM_PERIOD = R_Utility.R_GetStreamingContext<string>(PMR00200ContextConstant.CFROM_PERIOD),
                    CTO_PERIOD = R_Utility.R_GetStreamingContext<string>(PMR00200ContextConstant.CTO_PERIOD),
                    LIS_OUTSTANDING = R_Utility.R_GetStreamingContext<bool>(PMR00200ContextConstant.LIS_OUTSTANDING),
                    CLANG_ID = R_BackGlobalVar.USER_ID,
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

        //stream list helper
        private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> poList)
        {
            foreach (T loEntity in poList)
            {
                yield return loEntity;
            }
        }

        #region logger

        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);

        #endregion
    }
}
