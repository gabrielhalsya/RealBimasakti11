using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM00700Common;
using GSM00700Common.DTO;
using R_CommonFrontBackAPI;
using GSM00700Back;
using GSM00700Back.Activity;
using R_BackEnd;
using R_Common;
using Microsoft.Extensions.Logging;

namespace GSM00700Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM00710Controller : ControllerBase, IGSM00710
    {
        private readonly ActivitySource _activitySource;
        private LogGSM00700Common _logger;

        public GSM00710Controller(ILogger<GSM00710Controller> logger)
        {
            LogGSM00700Common.R_InitializeLogger(logger);
            _logger = LogGSM00700Common.R_GetInstanceLogger();
            _activitySource = GSM00710Activity.R_InitializeAndGetActivitySource(nameof(GSM00710Controller));
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM00710DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM00710DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity(nameof(R_ServiceGetRecord));
            _logger.LogInfo("Begin || GetRecordCashFlow(Controller)");
            var loException = new R_Exception();
            GSM00700DBParameter loDbPar = new GSM00700DBParameter();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM00710DTO>();
            try
            {
                var loCls = new GSM00710Cls();

                _logger.LogInfo("Set Parameter || GetRecordCashFlow(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetRecordCashFlowCls || GetRecordCashFlow(Controller)");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetRecordCashFlow(Controller)");
            return loRtn;
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<GSM00710DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM00710DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity(nameof(R_ServiceSave));
            _logger.LogInfo("Begin || ServiceSaveCashFlow(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM00710DTO> loRtn = null;
            GSM00710Cls loCls;


            try
            {
                loCls = new GSM00710Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM00710DTO>();

                _logger.LogInfo("Set Parameter || GetRecordCashFlow(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run ServiceSaveCashFlowCls || GetRecordCashFlow(Controller)");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            };
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetRecordCashFlow(Controller)");
            return loRtn;

        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM00710DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity(nameof(R_ServiceDelete));
            _logger.LogInfo("Begin || ServiceDeleteCashFlow(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM00710Cls loCls;
            try
            {
                _logger.LogInfo("Set Parameter || ServiceDeleteCashFlowController)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
             
                loCls = new GSM00710Cls();

                _logger.LogInfo("Run ServiceDeleteCls || ServiceDeleteCashFlow(Controller)");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            };
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || ServiceDeleteCashFlow(Controller)");
            return loRtn;
        }
        //[HttpPost]
        //public GSM00710ListDTO GetAllCashFlowList()
        //{
        //    R_Exception loException = new R_Exception();
        //    GSM00710ListDTO loRtn = null;
        //    List<GSM00710DTO> loResult;
        //    GSM00700DBParameter loDbPar;
        //    GSM00710Cls loCls;

        //    try
        //    {
        //        loDbPar = new GSM00700DBParameter();
        //        loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
        //        loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
        //        loDbPar.CCASH_FLOW_GROUP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CCASH_FLOW_GROUP_CODE);

        //        //loDbPar.CCOMPANY_ID = "RCD";
        //        //loDbPar.CUSER_ID = "HPC";
        //        //loDbPar.CCASH_FLOW_GROUP_CODE = "CF001";

        //        loCls = new GSM00710Cls();
        //        loResult = loCls.GetCashFlowList(loDbPar);
        //        loRtn = new GSM00710ListDTO() { Data = loResult };
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //    }

        //    loException.ThrowExceptionIfErrors();

        //    return loRtn;
        //}
        [HttpPost]
        public GSM00710CashFlowTypeListDTO GetListCashFlowType()
        {
            using Activity activity = _activitySource.StartActivity(nameof(GetListCashFlowType));
            _logger.LogInfo("Begin || GetListCashFlowType(Controller)");
            R_Exception loException = new R_Exception();
            GSM00710CashFlowTypeListDTO loRtn = null;
            List<GSM00710CashFlowTypeDTO> loResult;
            GSM00700DBParameter loDbPar;
            GSM00710Cls loCls;

            try
            {
                loDbPar = new GSM00700DBParameter();
                _logger.LogInfo("Set Parameter || GetListCashFlowType(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loDbPar.CCOMPANY_ID = "RCD";  
                loCls = new GSM00710Cls();
                _logger.LogInfo("Run GetListCashFlowTypeCls || GetListCashFlowType(Controller)");
                loResult = loCls.CashFlowType(loDbPar);
                loRtn = new GSM00710CashFlowTypeListDTO() { Data = loResult };

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetListCashFlowType(Controller)");
            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<GSM00710DTO> GetAllCashFlowStream()
        {
            using Activity activity = _activitySource.StartActivity(nameof(GetAllCashFlowStream));
            _logger.LogInfo("Begin || GetAllCashFlowStream(Controller)");
            R_Exception loException = new R_Exception();
            GSM00700DBParameter loDbPar;
            List<GSM00710DTO> loRtnTmp;
            GSM00710Cls loCls;
            IAsyncEnumerable<GSM00710DTO> loRtn = null;
            try
            {
                loDbPar = new GSM00700DBParameter();
                _logger.LogInfo("Set Parameter || GetAllCashFlowStream(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CCASH_FLOW_GROUP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CCASH_FLOW_GROUP_CODE);

                loCls = new GSM00710Cls();
                _logger.LogInfo("Run GetAllCashFlowListCls || GetAllCashFlowStream(Controller)");
                loRtnTmp = loCls.GetCashFlowList(loDbPar);

                _logger.LogInfo("Run GetAllCashFlowStream || GetAllCashFlowStream(Controller)");
                loRtn = GetAllCashFlowStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAllCashFlowStream(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM00710DTO> GetAllCashFlowStream(List<GSM00710DTO> poParameter)
        {
            foreach (GSM00710DTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
