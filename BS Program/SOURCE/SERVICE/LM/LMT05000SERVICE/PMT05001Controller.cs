using PMT05000BACK;
using PMT05000COMMON;
using PMT05000COMMON.DTO_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PMT05000SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMT05001Controller : ControllerBase, IPMM05000Init
    {
        private LogerPMT05000 _logger;

        private readonly ActivitySource _activitySource;

        private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> poList)
        {
            foreach (T loEntity in poList)
            {
                yield return loEntity;
            }
        }

        public PMT05001Controller(ILogger<PMT05001Controller> logger)
        {
            //initiate
            LogerPMT05000.R_InitializeLogger(logger);
            _logger = LogerPMT05000.R_GetInstanceLogger();
            _activitySource = PMT05000Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        public IAsyncEnumerable<GSB_CodeInfoDTO> GetGSBCodeInfo()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            IAsyncEnumerable<GSB_CodeInfoDTO> loRtn = null;

            try
            {
                var loCls = new PMM05000InitCls();

                ShowLogExecute();
                //var loTempRtn = loCls.GetGSPeriodDT();

                //loRtn = StreamListData<GSB_CodeInfoDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);

            }

            loEx.ThrowExceptionIfErrors();
            ShowLogEnd();

            return loRtn;
        }

        public IAsyncEnumerable<GSPeriodDT_DTO> GetGSPeriodDT()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PropertyDTO> GetProperty()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PropertyDTO> loRtnTemp = null;
            PMM05000InitCls loCls;
            try
            {
                loCls = new PMM05000InitCls();
                ShowLogExecute();
                loRtnTemp = loCls.GetPropertyList(new PropertyParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return StreamListHelper(loRtnTemp);
        }

        #region logger
        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);
        #endregion
    }
}
