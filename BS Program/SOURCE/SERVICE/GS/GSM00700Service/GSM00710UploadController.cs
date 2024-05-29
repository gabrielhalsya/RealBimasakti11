//using GSM00700Common;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using GSM00700Back;
//using GSM00700Common.DTO.Upload_DTO;
//using GSM00710Common.DTO.Upload_DTO_GSM00710;
//using R_BackEnd;
//using R_Common;


//namespace GSM00700Service
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class GSM00710UploadController : ControllerBase, IUPLOAD00710
//    {
//        //    [HttpPost]

//        //    public IAsyncEnumerable<GSM00710UploadCashFlowDTO> GetUploadListGSM00710()
//        //    {
//        //        R_Exception loException = new R_Exception();
//        //        IAsyncEnumerable<GSM00710UploadCashFlowDTO> loRtn = null;
//        //        List<GSM00710UploadCashFlowDTO> loParam = new List<GSM00710UploadCashFlowDTO>();
//        //        GSM00710UploadCashFlowCls loCls = new GSM00710UploadCashFlowCls();
//        //        List<GSM00710UploadCashFlowDTO> loTempRtn = null;

//        //        try
//        //        {
//        //            loParam = R_Utility.R_GetStreamingContext<List<GSM00710UploadCashFlowDTO>>(ContextConstantGSM00700.UPLOAD_CASHFLOW_STREAMING_CONTEXT);

//        //            foreach (var iten in loParam)
//        //            {
//        //                iten.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
//        //            }
//        //            { }
//        //            loTempRtn = loCls.GetGSM00710UploadCashFlowList(loParam);


//        //            loRtn = GetUploadFloorStream(loTempRtn);
//        //        }
//        //        catch (Exception ex)
//        //        {
//        //            loException.Add(ex);
//        //        }

//        //        loException.ThrowExceptionIfErrors();

//        //        return loRtn;
//        //    }

//        //    [HttpPost]
//        //    public IAsyncEnumerable<GSM00710UploadCashFlowErrorDTO> GetErrorListGSM00710()
//        //    {
//        //        R_Exception loException = new R_Exception();
//        //        IAsyncEnumerable<GSM00710UploadCashFlowErrorDTO> loRtn = null;
//        //        GSM00710UploadCashFlowValidateCls loCls = new GSM00710UploadCashFlowValidateCls();
//        //        List<GSM00710UploadCashFlowErrorDTO> loTempRtn = null;

//        //        try
//        //        {
//        //            string lcKeyGuid = R_Utility.R_GetStreamingContext<string>(ContextConstantGSM00700.UPLOAD_CENTER_ERROR_GUID_STREAMING_CONTEXT);

//        //            loTempRtn = loCls.GetErrorProcess(R_BackGlobalVar.COMPANY_ID, R_BackGlobalVar.USER_ID, lcKeyGuid);

//        //            loRtn = GetErrorProcessStream(loTempRtn);
//        //        }
//        //        catch (Exception ex)
//        //        {
//        //            loException.Add(ex);
//        //        }

//        //        loException.ThrowExceptionIfErrors();

//        //        return loRtn;
//        //    }

//        //    [HttpPost]
//        //    public GSM00710UploadCashFlowCheckRessultDTO CheckUploadGSM00710()
//        //    {
//        //        R_Exception loException = new R_Exception();
//        //        GSM00710UploadCashFlowCheckRessultDTO loRtn = new GSM00710UploadCashFlowCheckRessultDTO();
//        //        GSM00710UploadCashFlowCheckUsedParameterDTO loParam = new GSM00710UploadCashFlowCheckUsedParameterDTO();
//        //        GSM00710UploadCashFlowCls loCls = new GSM00710UploadCashFlowCls();

//        //        try
//        //        {

//        //            loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
//        //            loParam = R_Utility.R_GetContext<GSM00710UploadCashFlowCheckUsedParameterDTO>(ContextConstantGSM00700.UPLOAD_CASHFLOW_CHECK_IS_CASHFLOW_USED_CONTEXT);

//        //            loRtn.Data = loCls.CheckIsCashFlowUsed(loParam);
//        //        }
//        //        catch (Exception ex)
//        //        {
//        //            loException.Add(ex);
//        //        }

//        //        loException.ThrowExceptionIfErrors();

//        //        return loRtn;
//        //    }

//        //    private async IAsyncEnumerable<GSM00710UploadCashFlowDTO> GetUploadFloorStream(List<GSM00710UploadCashFlowDTO> poParameter)
//        //    {
//        //        foreach (GSM00710UploadCashFlowDTO item in poParameter)
//        //        {
//        //            yield return item;
//        //        }
//        //    }

//        //    private async IAsyncEnumerable<GSM00710UploadCashFlowErrorDTO> GetErrorProcessStream(List<GSM00710UploadCashFlowErrorDTO> poParameter)
//        //    {
//        //        foreach (GSM00710UploadCashFlowErrorDTO item in poParameter)
//        //        {
//        //            yield return item;
//        //        }
//        //    }
//        //}
//        [HttpPost]
//        public IAsyncEnumerable<GSM00710UploadDTO> GetUploadListGSM00710()
//        {
//            var loEx = new R_Exception();
//            IAsyncEnumerable<GSM00710UploadDTO> loRtn = null;
//            var loParameter = new GSM00710UploadDTO();
//            var LoCls = new GSM00710UploadCashFlowCls();
//            try
//            {
//                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
//                loParameter.CCASHFLOW_GROUP_CODE = R_Utility.R_GetStreamingContext<String>(ContextConstantGSM00700.CCASH_FLOW_GROUP_CODE);

//                //var loResult = LoCls.GetType(loParameter);
//                //loRtn = GetStream<GSM00710UploadDTO>(loResult);

//            }
//            catch (Exception ex)
//            {
//                loEx.Add(ex);
//            }
//            loEx.ThrowExceptionIfErrors();
//            return loRtn;

//        }
//        [HttpPost]
//        public IAsyncEnumerable<GSM00710UploadErrorValidateDTO> GetErrorListGSM00710()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
