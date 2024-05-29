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
using R_BackEnd;
using R_Common;
using System.Reflection;
using GSM00700Back.Activity;
using Microsoft.Extensions.Logging;

namespace GSM00700Service
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM00720Controller : ControllerBase, IGSM00720
    {
        private readonly ActivitySource _activitySource;
        private LogGSM00700Common _logger;
        
        public GSM00720Controller(ILogger<GSM00720Controller> logger)
        {
            LogGSM00700Common.R_InitializeLogger(logger);
            _logger = LogGSM00700Common.R_GetInstanceLogger();
            _activitySource = GSM00720Activity.R_InitializeAndGetActivitySource(nameof(GSM00720Controller));
        }
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM00720DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM00720DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity(nameof(R_ServiceGetRecord));
            _logger.LogInfo("Begin || GetRecordCashFlowPlan(Controller)");
            var loException = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<GSM00720DTO>();
            GSM00700DBParameter loDbPar = new GSM00700DBParameter();
            try
            {
                var loCls = new GSM00720Cls();
                _logger.LogInfo("Set Parameter || GetRecordCashFlowPlan(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CCASH_FLOW_GROUP_CODE = poParameter.Entity.CCASH_FLOW_GROUP_CODE;
                loDbPar.CYEAR = poParameter.Entity.CCYEAR;
           
                _logger.LogInfo("Run GetRecordCashFlowCls || GetRecordCashFlowPlan(Controller)");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetRecordCashFlowPlan(Controller)");
            return loRtn;
        }
        [HttpPost]
        public R_ServiceSaveResultDTO<GSM00720DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM00720DTO> poParameter)
         {
             using Activity activity = _activitySource.StartActivity(nameof(R_ServiceSave));
             _logger.LogInfo("Begin || SaveCashFlowPlan(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM00720DTO> loRtn = null;
            GSM00720Cls loCls;


            try
            {
                loCls = new GSM00720Cls();
                loRtn = new R_ServiceSaveResultDTO<GSM00720DTO>();
                _logger.LogInfo("Set Parameter || SaveCashFlowPlan(Controller)");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                //poParameter.Entity.CCOMPANY_ID = "RCD";
                //poParameter.Entity.CUSER_ID = "HPC";
                _logger.LogInfo("Run SaveCashFlowCls || SaveCashFlowPlan(Controller)");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            };
            EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || SaveCashFlowPlan(Controller)");
            return loRtn;
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM00720DTO> poParameter)
        {
            throw new NotImplementedException();
        }
        //[HttpPost]
        //public GSM00720ListDTO GetAllCashFlowPlan()
        //{
        //    R_Exception loException = new R_Exception();
        //    GSM00720ListDTO loRtn = null;
        //    List<GSM00720DTO> loResult;
        //    GSM00700DBParameter loDbPar;
        //    GSM00720Cls loCls;

        //    try
        //    {
        //        loDbPar = new GSM00700DBParameter();
        //        loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
        //        loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
        //        loDbPar.CCASH_FLOW_GROUP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CCASH_FLOW_GROUP_CODE);
        //        loDbPar.CCASH_FLOW_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CCASH_FLOW_CODE);
        //        loDbPar.CCYEAR = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CYEAR);

        //        //loDbPar.CCOMPANY_ID = "RCD";
        //        //loDbPar.CUSER_ID = "Admin";
        //        //loDbPar.CCASH_FLOW_GROUP_CODE = "CF0012";
        //        //loDbPar.CCASH_FLOW_CODE = "F001";
        //        //loDbPar.CCYEAR = "2023";y
        //        loCls = new GSM00720Cls();
        //        loResult = loCls.GetCashFlowPlan(loDbPar);
        //        loRtn = new GSM00720ListDTO() { Data = loResult };
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //    }

        //    loException.ThrowExceptionIfErrors();

        //    return loRtn;
        //}
        //[HttpPost]
        //public GSM00720YearListDTO GetYearList()
        //{
        //    R_Exception loException = new R_Exception();
        //    GSM00720YearListDTO loRtn = null;
        //    List<GSM00720YearDTO> loResult;
        //    GSM00700DBParameter loDbPar;
        //    GSM00720Cls loCls;
        //    try
        //    {
        //        loDbPar = new GSM00700DBParameter();
        //        loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

        //        //loDbPar.CCOMPANY_ID = "RCD";
        //        loCls = new GSM00720Cls();
        //        loResult = loCls.GetYearList(loDbPar);
        //        loRtn = new GSM00720YearListDTO() { Data = loResult };
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //    }

        //    loException.ThrowExceptionIfErrors();

        //    return loRtn;
        //}
        [HttpPost]
        public IAsyncEnumerable<GSM00720DTO> GetAllCashFlowPlanStream()
        {
            using Activity activity = _activitySource.StartActivity(nameof(GetAllCashFlowPlanStream));
            _logger.LogInfo("Begin || GetAllCashFlowPlanStream(Controller)");
            R_Exception loException = new R_Exception();
            GSM00700DBParameter loDbPar;
            List<GSM00720DTO> loRtnTmp;
            GSM00720Cls loCls;
            IAsyncEnumerable<GSM00720DTO> loRtn = null;
            try
            {
                    loDbPar = new GSM00700DBParameter();
                    _logger.LogInfo("Set Parameter || GetAllCashFlowPlanStream(Controller)");
                    loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                    loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                    loDbPar.CCASH_FLOW_GROUP_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CCASH_FLOW_GROUP_CODE);
                    loDbPar.CCASH_FLOW_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CCASH_FLOW_CODE);
                    loDbPar.CCYEAR = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CYEAR);

                loCls = new GSM00720Cls();
                _logger.LogInfo("Run GetAllCashFlowPlanCls || GetAllCashFlowPlanStream(Controller)");
                loRtnTmp = loCls.GetCashFlowPlan(loDbPar);

                _logger.LogInfo("Run GetAllCashFlowPlanStream || GetAllCashFlowPlanStream(Controller)");
                loRtn = GetAllCashFlowPlanStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetAllCashFlowPlanStream(Controller)");
            return loRtn;
        }
        [HttpPost]
        public IAsyncEnumerable<GSM00720YearDTO> GetYearStream()
        {
            using Activity activity = _activitySource.StartActivity(nameof(GetYearStream));
            _logger.LogInfo("Begin || GetYearCashFlowPlan(Controller)");
            R_Exception loException = new R_Exception();
            GSM00700DBParameter loDbPar;
            List<GSM00720YearDTO> loRtnTmp;
            GSM00720Cls loCls;
            IAsyncEnumerable<GSM00720YearDTO> loRtn = null;
            try
            {
                loDbPar = new GSM00700DBParameter();
                _logger.LogInfo("Set Parameter || GetYearCashFlowPlan(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                loCls = new GSM00720Cls();
                _logger.LogInfo("Run GetYearListCls|| GetYearCashFlowPlan(Controller)");
                loRtnTmp = loCls.GetYearList(loDbPar);
                _logger.LogInfo("Run GetYearStream || GetYearCashFlowPlan(Controller)");
                loRtn = GetYearStream(loRtnTmp);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetYearCashFlowPlan(Controller)");
            return loRtn;
        }
        [HttpPost]
        public GSM00720CopyFromYearListDTO GetCopyFromYearList(GSM00700ParameterDTO poParamDto)
        {
            using Activity activity = _activitySource.StartActivity(nameof(GetCopyFromYearList));
            _logger.LogInfo("Begin || GetCopyFromYearCashFlowPlan(Controller)");
            R_Exception loException = new R_Exception();
            GSM00720CopyFromYearListDTO loRtn = null;
            List<GSM00720CopyFromYearDTO> loResult;
            GSM00700DBParameter loDbPar;

            GSM00720Cls loCls;
            try
            {
                loDbPar = new GSM00700DBParameter();  
                _logger.LogInfo("Set Parameter || GetCopyFromYearCashFlowPlan(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbPar.CFROM_CASH_FOW_FLAG = poParamDto.CFROM_CASH_FOW_FLAG;
                loDbPar.CFROM_CASH_FLOW_CODE = poParamDto.CFROM_CASH_FLOW_CODE;
                loDbPar.CFROM_YEAR = poParamDto.CFROM_YEAR;
                loDbPar.CTO_CASH_FLOW_CODE = poParamDto.CTO_CASH_FLOW_CODE;
                loDbPar.CTO_YEAR = poParamDto.CTO_YEAR;

                loCls = new GSM00720Cls();
                _logger.LogInfo("Run GetCopyFromYearListCls|| GetCopyFromYearCashFlowPlan(Controller)");
                loResult = loCls.CopyFromYear(loDbPar);
                loRtn = new GSM00720CopyFromYearListDTO() { Data = loResult };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetCopyFromYearCashFlowPlan(Controller)");
            return loRtn;
        }
        [HttpPost]
        public GSM00720CopyBaseAmountListDTO GetCopyBaseAmountList(GSM00700ParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity(nameof(GetCopyBaseAmountList));
            _logger.LogInfo("Begin || GetCopyBaseAmountCashFlowPlan(Controller)");
            R_Exception loException = new R_Exception();
            GSM00720CopyBaseAmountListDTO loRtn = null;
            List<GSM00720CopyBaseLocalAmountDTO> loResult;
            GSM00700DBParameter loDbPar;
            GSM00720Cls loCls;

            try
            {
                loDbPar = new GSM00700DBParameter();
                _logger.LogInfo("Set Parameter || GetCopyBaseAmountCashFlowPlan(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
                //loDbPar.CFROM_CASH_FOW_FLAG = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.CFROM_CASH_FOW_FLAG);

                loDbPar.CCASH_FLOW_GROUP = poParameter.CCASH_FLOW_GROUP;
                loDbPar.CCASH_FLOW_CODE = poParameter.CCASH_FLOW_CODE;
                loDbPar.CYEAR = poParameter.CYEAR;
                loDbPar.CCURRENCY_CODE = poParameter.CCURRENCY_CODE;
                loDbPar.CCURRENCY_RATE = poParameter.CCURRENCY_RATE;
                loDbPar.INO_PERIOD_FROM = poParameter.INO_PERIOD_FROM;
                loDbPar.INO_PERIOD_TO = poParameter.INO_PERIOD_TO;

                loCls = new GSM00720Cls();
                _logger.LogInfo("Run GetCopyBaseAmountCashFlowPlan|| GetCopyBaseAmountCashFlowPlan(Controller)");
                loResult = loCls.UpdateBaseAmount(loDbPar);
                loRtn = new GSM00720CopyBaseAmountListDTO() { Data = loResult };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetCopyBaseAmountCashFlowPlan(Controller)");
            return loRtn;
        }
        [HttpPost]
        public GSM00720CopyLocalAmountListDTO GetCopyLocalAmountList(GSM00700ParameterDTO poParamDto)
        {
            using Activity activity = _activitySource.StartActivity(nameof(GetCopyLocalAmountList));
            _logger.LogInfo("Begin || GetCopyLocalAmountCashFlowPlan(Controller)");
            R_Exception loException = new R_Exception();
            GSM00720CopyLocalAmountListDTO loRtn = null;
            List<GSM00720CopyBaseLocalAmountDTO> loResult;
            GSM00700DBParameter loDbPar;
            GSM00720Cls loCls;

            try
            {
                loDbPar = new GSM00700DBParameter();
                _logger.LogInfo("Set Parameter || GetCopyLocalAmountCashFlowPlan(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

                loDbPar.CCASH_FLOW_GROUP = poParamDto.CCASH_FLOW_GROUP;
                loDbPar.CCASH_FLOW_CODE = poParamDto.CCASH_FLOW_CODE;
                loDbPar.CYEAR = poParamDto.CYEAR;
                loDbPar.CCURRENCY_CODE = poParamDto.CCURRENCY_CODE;
                loDbPar.CCURRENCY_RATE = poParamDto.CCURRENCY_RATE;
                loDbPar.INO_PERIOD_FROM = poParamDto.INO_PERIOD_FROM;
                loDbPar.INO_PERIOD_TO = poParamDto.INO_PERIOD_TO;

                loCls = new GSM00720Cls();
                _logger.LogInfo("Run GetCopyLocalAmountCashFlowPlanCls|| GetCopyLocalAmountCashFlowPlan(Controller)");
                loResult = loCls.UpdateLocalAmount(loDbPar);
                loRtn = new GSM00720CopyLocalAmountListDTO() { Data = loResult };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetCopyLocalAmountCashFlowPlan(Controller)");
            return loRtn;
        }
        [HttpPost]
        public GSM00720CurrencyDTO GetCurrencyList()
        {
            using Activity activity = _activitySource.StartActivity(nameof(GetCurrencyList));
            _logger.LogInfo("Begin || GetCurrencyCashFlowPlan(Controller)");
            R_Exception loException = new R_Exception();
            GSM00720CurrencyDTO loRtn = null;
            GSM00720CurrencyDTO loResult;
            GSM00700DBParameter loDbPar;
            GSM00720Cls loCls;

            try
            {
                loDbPar = new GSM00700DBParameter();
                _logger.LogInfo("Set Parameter || GetCurrencyCashFlowPlan(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                loCls = new GSM00720Cls();
                _logger.LogInfo("Run GetCurrencyCashFlowPLanCls|| GetCurrencyCashFlowPlan(Controller)");
                loResult = loCls.GetCurrency(loDbPar);
                loRtn = new GSM00720CurrencyDTO();
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetCurrencyCashFlowPlan(Controller)");
            return loRtn;
        }
        [HttpPost]
        public GSM00720InitialProsesListDTO GetInitialProses()
        {
            using Activity activity = _activitySource.StartActivity(nameof(GetInitialProses));
            _logger.LogInfo("Begin || GetInitialProsesCashFlowPlan(Controller)");
            R_Exception loException = new R_Exception();
            GSM00720InitialProsesListDTO loRtn = null;
            List<GSM00720InitialProsesDTO> loResult;
            GSM00700DBParameter loDbPar;
            GSM00720Cls loCls;
            try
            {
                loDbPar = new GSM00700DBParameter();
                _logger.LogInfo("Set Parameter || GetInitialProsesCashFlowPlan(Controller)");
                loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                //loDbPar.CCOMPANY_ID = "RCD";
                loCls = new GSM00720Cls();
                _logger.LogInfo("Run GetInitialProsesCashFlowPlanCls|| GetInitialProsesCashFlowPlan(Controller)");
                loResult = loCls.InitialProses(loDbPar);
                loRtn = new GSM00720InitialProsesListDTO() { Data = loResult };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetInitialProsesCashFlowPlan(Controller)");
            return loRtn;
        }
        [HttpPost]
        public GSM00710TemplateCashFlowUserInterface GetTemplate()
        {
            using Activity activity = _activitySource.StartActivity(nameof(GetTemplate));
            _logger.LogInfo("Start DownloadTemplateFile");
            var loException = new R_Exception();
            var loRtn = new GSM00710TemplateCashFlowUserInterface();

            try
            {
                Assembly loAsm = Assembly.Load("BIMASAKTI_GS_API");
                var lcResourceFile = "BIMASAKTI_GS_API.Template.Cash Flow Parameter.xlsx";

                using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                {
                    var ms = new MemoryStream();
                    resFilestream.CopyTo(ms);
                    var bytes = ms.ToArray();

                    loRtn.FileBytes = bytes;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || End DownloadTemplateFile(Controller)");
            return loRtn;

        }
        [HttpPost]
        public GSM00720TemplateCashFlowPlan GetTemplate720()
        {
            using Activity activity = _activitySource.StartActivity(nameof(GetTemplate720));
            _logger.LogInfo("Start DownloadTemplate720File");
            var loException = new R_Exception();
            var loRtn = new GSM00720TemplateCashFlowPlan();

            try
            {
                Assembly loAsm = Assembly.Load("BIMASAKTI_GS_API");
                var lcResourceFile = "BIMASAKTI_GS_API.Template.Cash Flow Plan.xlsx";

                using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                {
                    var ms = new MemoryStream();
                    resFilestream.CopyTo(ms);
                    var bytes = ms.ToArray();

                    loRtn.FileBytes = bytes;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || End DownloadTemplate720File(Controller)");
            return loRtn;

        }
        private async IAsyncEnumerable<GSM00720DTO> GetAllCashFlowPlanStream(List<GSM00720DTO> poParameter)
        {
            foreach (GSM00720DTO item in poParameter)
            {
                yield return item;
            }
        }

        private async IAsyncEnumerable<GSM00720YearDTO> GetYearStream(List<GSM00720YearDTO> poParameter)
        {
            foreach (GSM00720YearDTO item in poParameter)
            {
                yield return item;
            }
        }

    }
}
