using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR00200BACK;
using PMR00200COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI.Log;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using R_Cache;

namespace PMR00200SERVICE
{
    public class PMR00210PrintController : R_ReportControllerBase
    {
        private PMR00200PrintLogger _logger;
        private R_ReportFastReportBackClass _ReportCls;
        private PMR00200PrintParamDTO _Parameter;
        private readonly ActivitySource _activitySource;
        #region instantiation

        public PMR00210PrintController(ILogger<PMR00200PrintLogger> logger)
        {
            PMR00200PrintLogger.R_InitializeLogger(logger);
            _logger = PMR00200PrintLogger.R_GetInstanceLogger();
            _activitySource = PMR00200Activity.R_InitializeAndGetActivitySource(nameof(PMR00210PrintController));


            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion
        #region Event Handler

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "PMR01001.frx");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            //poData.Add(GenerateDataPrint(_Parameter));
            pcDataSourceName = "ResponseDataModel";
        }

        private void _ReportCls_R_SetNumberAndDateFormat(ref R_ReportFormatDTO poReportFormat)
        {
            poReportFormat.DecimalSeparator = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_SEPARATOR;
            poReportFormat.GroupSeparator = R_BackGlobalVar.REPORT_FORMAT_GROUP_SEPARATOR;
            poReportFormat.DecimalPlaces = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_PLACES;
            poReportFormat.ShortDate = R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE;
            poReportFormat.ShortTime = R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME;
        }

        #endregion

        [HttpPost]
        public R_DownloadFileResultDTO DownloadResultPrintPost(PMR00200PrintParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("DownloadResultPrintPost");

            _logger.LogInfo("Start - Post DownloadResultPrintPost Status");
            R_Exception loException = new R_Exception();
            PMR00200PrintLogKey loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new PMR00200PrintLogKey
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };
                _logger.LogInfo("Set GUID Param - Post DownloadResultPrintPost Status");
                //R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End - Post COA Status");
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult DepositReportListGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity("DepositReport");
            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            PMR00200PrintLogKey loResultGUID = null;
            try
            {
                // Deserialize the GUID from the cache
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<PMR00200PrintLogKey>(R_DistributedCache.Cache.Get(pcGuid));
                _logger.LogDebug("Deserialized GUID: {pcGuid}", pcGuid);

                // Get Parameter
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);

                _Parameter = loResultGUID.poParam;

                _logger.LogDebug("Deserialized GUID: {pcGuid}", pcGuid);
                _logger.LogDebug("Deserialized Parameters: {@Parameters}", _Parameter);

                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
                _logger.LogInfo("Data retrieval successful. Generating report.");

                _logger.LogInfo("Report generated successfully.");
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End - Deposit Report Generation");
            return loRtn;
        }

        #region Helper
        //private PMR01001PrintResultWithBaseHeaderPrintDTO GenerateDataPrint(PMR01000PrintParamDTO poParam)
        //{
        //    using Activity activity = _activitySource.StartActivity("GenerateDataPrint");

        //    var loEx = new R_Exception();
        //    PMR01001PrintResultWithBaseHeaderPrintDTO loRtn = new PMR01001PrintResultWithBaseHeaderPrintDTO();
        //    var loParam = new BaseHeaderDTO();

        //    System.Globalization.CultureInfo loCultureInfo =
        //        new System.Globalization.CultureInfo(R_BackGlobalVar.REPORT_CULTURE);

        //    try
        //    {
        //        _logger.LogInfo("_logger.LogInfo(\"Start - Generating data for Print\");\n data for Deposit report.");

        //        //Add Resources
        //        loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
        //            "Page", loCultureInfo);
        //        loRtn.BaseHeaderColumn.Of =
        //            R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
        //        loRtn.BaseHeaderColumn.Print_Date =
        //            R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
        //        loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
        //            "Print_By", loCultureInfo);

        //        PMR01001PrintColoumnDTO loColumnObject = new PMR01001PrintColoumnDTO();
        //        var loColumn = AssignValuesWithMessages(typeof(PMR01000BackResources.Resources_Dummy_Class),
        //            loCultureInfo, loColumnObject);

        //        // Set base header data
        //        _logger.LogDebug("Deserialized Print Parameters: {@PrintParameters}");

        //        loParam.CCOMPANY_NAME = R_BackGlobalVar.COMPANY_ID.ToUpper();
        //        loParam.CPRINT_CODE = "001";
        //        loParam.CPRINT_NAME = "Deposit List Report";
        //        loParam.CUSER_ID = R_BackGlobalVar.USER_ID.ToUpper();

        //        // Create an instance of PMR01000PrintGOAResultDTo
        //        PMR01001PrintResultDTO loData = new PMR01001PrintResultDTO()
        //        {
        //            Title = "Deposit Type List",
        //            Header = "Deposit Type List",
        //            Column = new PMR01001PrintColoumnDTO(),
        //            Data = new List<PMR01001DataResultDTO>(),
        //            HeaderParam = poParam
        //        };

        //        // Create an instance of PMR01000Cls
        //        var loCls = new PMR01000Cls();
        //        poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
        //        // Get print data for Group Of Account report
        //        var loCollection = loCls.GetPrintDataResult(poParam);
        //        _logger.LogInfo("Data generation successful. Processing data for printing.");


        //        // Process the data and create a formatted list
        //        var loTempData = loCollection
        //        .GroupBy(data1a => new
        //        {
        //            data1a.CDEPOSIT_ID,
        //            data1a.CDEPOSIT_NAME,
        //        }).Select(data1b => new PMR01001DataResultDTO()
        //        {
        //            CDEPOSIT_ID = data1b.Key.CDEPOSIT_ID,
        //            CDEPOSIT_NAME = data1b.Key.CDEPOSIT_NAME,
        //            Detail1 = data1b.GroupBy(data2a => new
        //            {
        //                data2a.CBUILDING_ID,
        //                data2a.CDEPOSIT_TYPE,

        //            }).Select(data2b => new PMR01001DataResultChild1DTO()
        //            {
        //                CBUILDING_ID = data2b.Key.CBUILDING_ID,
        //                CDEPOSIT_TYPE = data2b.Key.CDEPOSIT_TYPE,
        //                Detail2 = data2b.GroupBy(data2b => new
        //                {
        //                    data2b.CCUSTOMER_NAME,
        //                    data2b.CTRANSACTION_TYPE,
        //                    data2b.CREFERENCE_NO,
        //                    data2b.CSTATUS,
        //                    data2b.CUNIT_DESC,
        //                    data2b.CINVOICE_NO,
        //                    data2b.CDEPOSIT_DATE,
        //                    data2b.CPAYMENT_STATUS,
        //                    data2b.CCURRENCY_CODE,
        //                    data2b.NDEPOSIT_AMOUNT,
        //                    data2b.NDEPOSIT_BALANCE,
        //                }).Select(data3b => new PMR01001DataResultChild2DTO()
        //                {
        //                    CCUSTOMER_NAME = data3b.Key.CCUSTOMER_NAME,
        //                    CTRANSACTION_TYPE = data3b.Key.CTRANSACTION_TYPE,
        //                    CREFERENCE_NO = data3b.Key.CREFERENCE_NO,
        //                    CSTATUS = data3b.Key.CSTATUS,
        //                    CUNIT_DESC = data3b.Key.CUNIT_DESC,
        //                    CINVOICE_NO = data3b.Key.CINVOICE_NO,
        //                    CDEPOSIT_DATE = data3b.Key.CDEPOSIT_DATE,
        //                    CPAYMENT_STATUS = data3b.Key.CPAYMENT_STATUS,
        //                    CCURRENCY_CODE = data3b.Key.CCURRENCY_CODE,
        //                    NDEPOSIT_AMOUNT = data3b.Key.NDEPOSIT_AMOUNT,
        //                    NDEPOSIT_BALANCE = data3b.Key.NDEPOSIT_BALANCE,
        //                }).ToList()
        //            }).ToList()
        //        }).ToList();

        //        // Set the generated data in loRtn
        //        _logger.LogInfo("Data processed successfully. Generating print output.");
        //        loData.Data = loTempData;
        //        loRtn.Data1 = loData;
        //        loRtn.BaseHeaderData = loParam;

        //        _logger.LogInfo("Print output generated successfully. Saving print file.");
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //        _logger.LogError(loEx);
        //    }

        //    loEx.ThrowExceptionIfErrors();
        //    return loRtn;
        //    _logger.LogInfo("End - Data Generation for Print");
        //}
        #endregion

        private object AssignValuesWithMessages(Type poResourceType, CultureInfo poCultureInfo, object poObject)
        {
            object loObj = Activator.CreateInstance(poObject.GetType());
            var loGetPropertyObject = poObject.GetType().GetProperties();

            foreach (var property in loGetPropertyObject)
            {
                string propertyName = property.Name;
                string message = R_Utility.R_GetMessage(poResourceType, propertyName, poCultureInfo);
                property.SetValue(loObj, message);
            }

            return loObj;
        }
    }
}
