using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Runtime.CompilerServices;
using R_OpenTelemetry;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using PMT05000COMMON;
using PMT05000COMMON.DTO_s;
using PMT05000BACK;
namespace PMT05000SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMT05000Controller : ControllerBase, IPMM05000
    {
        private LogerPMT05000 _logger;
        
        private readonly ActivitySource _activitySource;

        public PMT05000Controller(ILogger<PMT05000Controller> logger)
        {
            //initiate
            LogerPMT05000.R_InitializeLogger(logger);
            _logger = LogerPMT05000.R_GetInstanceLogger();
            _activitySource = PMT05000Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> poList)
        {
            foreach (T loEntity in poList)
            {
                yield return loEntity;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<AgreementChrgDiscDetailDTO> GetAgreementChargesDiscountList(AgreementChrgDiscParamDTO poParam)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public AgreementChrgDiscResultDTO ProcessAgreementChargeDiscount(AgreementChrgDiscParamDTO popaParam)
        {
            throw new NotImplementedException();
        }

        #region logger
        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);
        #endregion
    }
}
