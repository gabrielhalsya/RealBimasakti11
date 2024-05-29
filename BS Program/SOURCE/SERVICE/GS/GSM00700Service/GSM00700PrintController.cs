using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BaseHeaderReportCOMMON;
using GSM00700Back;
using GSM00700Common.DTO.Report_DTO_GSM00700;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using R_Common;
using R_BackEnd;
using R_Cache;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using GSM00700Common;
using Microsoft.Extensions.Logging;
using GSM00700Service.DTO;
using R_CommonFrontBackAPI.Log;

namespace GSM00700Service
{
    //[ApiController]
    //[Route("api/[controller]/[action]")]
    public class GSM00700PrintController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private GSM00700PrintCashFlowParameterDTo _poParam;
        private LogGSM00700Common _logger;
        public GSM00700PrintController(ILogger<GSM00700Controller> logger)
        {
            LogGSM00700Common.R_InitializeLogger(logger);
            _logger = LogGSM00700Common.R_GetInstanceLogger();

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;

        }

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            //pcFileTemplate = "Reports\\GSM00700Report.frx";

            pcFileTemplate = System.IO.Path.Combine("Reports", "GSM00700Report.frx");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GeneratePrint(_poParam));
            pcDataSourceName = "ResponseDataModel";
        }

        [HttpPost]
        public R_DownloadFileResultDTO CashFlowPost(GSM00700PrintCashFlowParameterDTo poParameter)
        {
            R_Exception loException = new R_Exception();
            _logger.LogInfo("Start || PostCashFlowPrint");
            GSM00700LogPrintDTO loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new GSM00700LogPrintDTO
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };

                _logger.LogInfo("Set Guid Param || PostCashFlowPrint");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || PostCashFlowPrint");
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult CashFlowGet(string pcGuid)
        {
            R_Exception loException = new R_Exception();
            GSM00700LogPrintDTO loResultGUID = null;
            FileStreamResult loRtn = null;
            _logger.LogInfo("Start || GetCashFlowPrint");


            try
            {
                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<GSM00700LogPrintDTO>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                _poParam = loResultGUID.poParam;

                _logger.LogInfo("Read File || GetCashFlowPrint");
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);

            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetCashFlowPrint");
            return loRtn;
        }

        private GSM00700PrintCashFlowResultWithBaseHeaderPrintDTO GeneratePrint(
            GSM00700PrintCashFlowParameterDTo poParam)
        {
            var loEx = new R_Exception();
            GSM00700PrintCashFlowResultWithBaseHeaderPrintDTO loRtn = new GSM00700PrintCashFlowResultWithBaseHeaderPrintDTO();
            List<GSM00700Data>? loTempData = null;

            try
            {
                _logger.LogInfo("Set BaseHeader Report");
                var loParam = new BaseHeaderDTO();

                {
                    loParam.CCOMPANY_NAME = "PT Realta Chakradarma";
                    loParam.CPRINT_CODE = "001";
                    loParam.CPRINT_NAME = "Cash Flow Parameter";
                    loParam.CUSER_ID = poParam.CUSER_LOGIN_ID;
                };
                GSM00700PrintCashFlowResultDTo loData = new GSM00700PrintCashFlowResultDTo()
                {
                    Title = "Cash Flow", //dipanggil dari fadel
                    Header = "Cash Flow",
                    Column = new GSM00700PrintCashFlowColoumnDTO()
                };
                var loCls = new GSM00700Cls();
                _logger.LogInfo("Call Method GetPrintDataResult Report");

                var loCollection = loCls.GetPrintParam(poParam);



                if (loCollection.Count() != 0)
                {



                    loTempData = loCollection.GroupBy(data1a => new
                    {
                        data1a.CCASH_FLOW_GROUP_CODE,
                        data1a.CCASH_FLOW_GROUP_NAME,
                        data1a.CCASH_FLOW_GROUP_TYPE,  //ikut format, agar funsgsi sesuai harus ada grouping berdasarkan dari parent data agar tidak keluar
                        data1a.CCASH_FLOW_GROUP_TYPE_DESCR,
                    })
                   .Select(data1b => new GSM00700Data()
                   {
                       CCASH_FLOW_GROUP_CODE = data1b.Key.CCASH_FLOW_GROUP_CODE,
                       CCASH_FLOW_GROUP_NAME = data1b.Key.CCASH_FLOW_GROUP_NAME,
                       CCASH_FLOW_GROUP_TYPE = data1b.Key.CCASH_FLOW_GROUP_TYPE,
                       CCASH_FLOW_GROUP_TYPE_DESCR = data1b.Key.CCASH_FLOW_GROUP_TYPE_DESCR,

                       GSM00710Data = data1b.GroupBy(data2a => new
                       {
                           data2a.CCYEAR,
                           data2a.CCASH_FLOW_CODE,
                           data2a.CCASH_FLOW_NAME,
                           data2a.CCASH_FLOW_TYPE,
                           data2a.CCASH_FLOW_TYPE_DESCR,

                       }).Select(data2b => new GSM00710Data()
                       {
                           CYEAR = data2b.Key.CCYEAR,
                           CCASH_FLOW_CODE = data2b.Key.CCASH_FLOW_CODE,
                           CCASH_FLOW_NAME = data2b.Key.CCASH_FLOW_NAME,
                           CCASH_FLOW_TYPE = data2b.Key.CCASH_FLOW_TYPE,
                           CCASH_FLOW_TYPE_DESCR = data2b.Key.CCASH_FLOW_TYPE_DESCR,
                           GSM00720Data = data2b.GroupBy(data3a => new
                           {
                               data3a.CPERIOD_NO,
                               data3a.NLOCAL_AMOUNT,
                               data3a.NBASE_AMOUNT,
                               data3a.CCYEAR,

                           }).Select(data3b => new GSM00720Data()
                           {
                               CPERIOD_NO = data3b.Key.CPERIOD_NO,
                               NLOCAL_AMOUNT = data3b.Key.NLOCAL_AMOUNT,
                               NBASE_AMOUNT = data3b.Key.NBASE_AMOUNT,
                               CYEAR = data3b.Key.CCYEAR,
                           }).ToList()
                       }).ToList()
                   }).ToList();

                }
                else
                {
                    loTempData = new List<GSM00700Data>();
                }
                loData.Data = loTempData;
                _logger.LogInfo("Set Data Report");

                loRtn.BaseHeaderData = loParam;
                loRtn.CenterData = loData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;

























        }


    }
}

