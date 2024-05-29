//using GSM00700Back;
//using GSM00700Common.DTO.Upload_DTO;
//using GSM00700Common;
//using Microsoft.AspNetCore.Mvc;
//using R_BackEnd;
//using R_Common;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using GSM00700Common.DTO.Upload_DTO_GSM00720;

//namespace GSM00700Service
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class GSM00720UploadController : ControllerBase, IUPLOAD00720
//    {
//        [HttpPost]

//        public IAsyncEnumerable<GSM00720UploadCashFlowPlanDTO> GetUploadListGSM00720()
//        {
//            R_Exception loException = new R_Exception();
//            IAsyncEnumerable<GSM00720UploadCashFlowPlanDTO> loRtn = null;
//            List<GSM00720UploadCashFlowPlanDTO> loParam = new List<GSM00720UploadCashFlowPlanDTO>();
//            GSM00720UploadCashFlowPlanPlanCls loCls = new GSM00720UploadCashFlowPlanPlanCls();
//            List<GSM00720UploadCashFlowPlanDTO> loTempRtn = null;

//            try
//            {
//                loParam = R_Utility.R_GetStreamingContext<List<GSM00720UploadCashFlowPlanDTO>>(ContextConstantGSM00700.UPLOAD_CashFlowPlan_STREAMING_CONTEXT);

//                foreach (var iten in loParam)
//                {
//                    iten.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
//                }
//                { }
//                //loTempRtn = loCls.GetGSM00720UploadCashFlowPlanList(loParam);


//                loRtn = GetUploadFloorStream(loTempRtn);
//            }
//            catch (Exception ex)
//            {
//                loException.Add(ex);
//            }

//            loException.ThrowExceptionIfErrors();

//            return loRtn;
//        }
//        [HttpPost]
//        public IAsyncEnumerable<GSM00720UploadCashFlowPlanErrorDTO> GetErrorListGSM00720()
//        {
//            R_Exception loException = new R_Exception();
//            IAsyncEnumerable<GSM00720UploadCashFlowPlanErrorDTO> loRtn = null;
//            GSM00720UploadCashFlowPlanValidateCls loCls = new GSM00720UploadCashFlowPlanValidateCls();
//            List<GSM00720UploadCashFlowPlanErrorDTO> loTempRtn = null;

//            try
//            {
//                string lcKeyGuid = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.UPLOAD_CENTER_ERROR_GUID_STREAMING_CONTEXT);

//                loTempRtn = loCls.GetErrorProcess(R_BackGlobalVar.COMPANY_ID, R_BackGlobalVar.USER_ID, lcKeyGuid);

//                loRtn = GetErrorProcessStream(loTempRtn);
//            }
//            catch (Exception ex)
//            {
//                loException.Add(ex);
//            }

//            loException.ThrowExceptionIfErrors();

//            return loRtn;
//        }

//        [HttpPost]
//        public GSM00720UploadCashFlowPlanCheckResultDTO CheckUploadGSM00720()
//        {
//            R_Exception loException = new R_Exception();
//            GSM00720UploadCashFlowPlanCheckResultDTO loRtn = new GSM00720UploadCashFlowPlanCheckResultDTO();
//            GSM00720UploadCashFlowPlanCheckUsedParameterDTO loParam = new GSM00720UploadCashFlowPlanCheckUsedParameterDTO();
//            GSM00720UploadCashFlowPlanPlanCls loCls = new GSM00720UploadCashFlowPlanPlanCls();

//            try
//            {
//                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
//                loParam = R_Utility.R_GetContext<GSM00720UploadCashFlowPlanCheckUsedParameterDTO>(ContextConstantGSM00700.UPLOAD_CashFlowPlan_CHECK_IS_CashFlowPlan_USED_CONTEXT);

//                //loRtn.Data = loCls.CheckIsCashFlowPlanUsed(loParam);
//            }
//            catch (Exception ex)
//            {
//                loException.Add(ex);
//            }

//            loException.ThrowExceptionIfErrors();

//            return loRtn;
//        }

//        private async IAsyncEnumerable<GSM00720UploadCashFlowPlanDTO> GetUploadFloorStream(List<GSM00720UploadCashFlowPlanDTO> poParameter)
//        {
//            foreach (GSM00720UploadCashFlowPlanDTO item in poParameter)
//            {
//                yield return item;
//            }
//        }
//        private async IAsyncEnumerable<GSM00720UploadCashFlowPlanErrorDTO> GetErrorProcessStream(List<GSM00720UploadCashFlowPlanErrorDTO> poParameter)
//        {
//            foreach (GSM00720UploadCashFlowPlanErrorDTO item in poParameter)
//            {
//                yield return item;
//            }
//        }
//    }
//}
