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
using PMR00200COMMON.Print_DTO;
using System.Reflection;
using BaseHeaderReportCOMMON;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using PMR00200PrintLogKey = PMR00200COMMON.PMR00200PrintLogKey;

namespace PMR00200SERVICE
{
    public class PMR00210PrintController : R_ReportControllerBase
    {
        private PMR00200PrintLogger _logger;
        private R_ReportFastReportBackClass _ReportCls;
        private PMR00200PrintParamDTO _Parameter;
        private readonly ActivitySource _activitySource;

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

        #region reporthelper

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "PMR00200Summary.frx");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateDataPrint(_Parameter));
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
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
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
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End - Print LOI Status");
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult LOIStatsSummary_ReportListGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

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
            _logger.LogInfo("End - LOI Status Report Generation");
            return loRtn;
        }

        #region Helper

        private PMR00200PrintDislpayWithBaseHeaderDTO GenerateDataPrint(PMR00200PrintParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            var loEx = new R_Exception();
            PMR00200PrintDislpayWithBaseHeaderDTO loRtn = new PMR00200PrintDislpayWithBaseHeaderDTO();
            var loParam = new BaseHeaderDTO();

            System.Globalization.CultureInfo loCultureInfo =
                new System.Globalization.CultureInfo(R_BackGlobalVar.REPORT_CULTURE);

            try
            {
                _logger.LogInfo("_logger.LogInfo(\"Start - Generating data for Print\");\n data for LOI Status report.");

                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                    "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of =
                    R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date =
                    R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                    "Print_By", loCultureInfo);

                PMR00200SummaryColumnDTO loColumnObject = new PMR00200SummaryColumnDTO();
                var loColumn = AssignValuesWithMessages(typeof(PMR00200BackResources.Resources_Dummy_Class),
                    loCultureInfo, loColumnObject);

                // Set base header data
                _logger.LogDebug("Deserialized Print Parameters: {@PrintParameters}");

                loParam.CCOMPANY_NAME = R_BackGlobalVar.COMPANY_ID.ToUpper();
                loParam.CPRINT_CODE = "001";
                loParam.CPRINT_NAME = "LOI Status Report";
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID.ToUpper();

                // Create an instance of PMR01000PrintGOAResultDTo
                PMR00200PrintDisplayDTO loData = new PMR00200PrintDisplayDTO()
                {
                    Title = "LOI Status",
                    Header = "LOI Status Summary",
                    Column = new PMR00200SummaryColumnDTO(),
                    Data = new List<PMR00200DataResultDTO>(),
                    HeaderParam = poParam
                };

                // Create an instance of PMR01000Cls
                var loCls = new PMR00200Cls();
                poParam.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                // Get print data for Group Of Account report
                var loCollData = loCls.GetReportData(poParam);
                _logger.LogInfo("Data generation successful. Processing data for printing.");


                // Process the data and create a formatted list
                var loTempData = loCollData.GroupBy(data1a => new
                {
                    data1a.CTRANS_CODE,
                    data1a.CTRANS_NAME,
                }).Select(data1b => new PMR00200DataResultDTO()
                {
                    CTRANS_CODE = data1b.Key.CTRANS_CODE,
                    CTRANS_NAME = data1b.Key.CTRANS_NAME,
                    Detail1 = data1b.GroupBy(data2a => new
                    {
                        data2a.CSALESMAN_ID,
                        data2a.CSALESMAN_NAME,

                    }).Select(data2b => new PMR00200DataRes1()
                    {
                        CSALESMAN_ID = data2b.Key.CSALESMAN_ID,
                        CSALESMAN_NAME = data2b.Key.CSALESMAN_NAME,
                        Detail2 = data2b.GroupBy(data2b => new
                        {
                            data2b.CREF_NO,
                            data2b.CREF_DATE,
                            data2b.CTENURE,
                            data2b.NTOTAL_GROSS_AREA_SIZE,
                            data2b.NTOTAL_NET_AREA_SIZE,
                            data2b.NTOTAL_COMMON_AREA_SIZE,
                            data2b.CTRANS_STATUS_NAME,
                            data2b.NTOTAL_PRICE,
                            data2b.CTAX,
                            data2b.CTENANT_ID,
                            data2b.CTENANT_NAME,
                            data2b.CTC_MESSAGE,
                        }).Select(data3b => new PMR00200DataRes2()
                        {
                            CREF_NO = data3b.Key.CREF_NO,
                            CREF_DATE = data3b.Key.CREF_DATE,
                            CTENURE = data3b.Key.CTENURE,
                            NTOTAL_GROSS_AREA_SIZE = data3b.Key.NTOTAL_GROSS_AREA_SIZE,
                            NTOTAL_NET_AREA_SIZE = data3b.Key.NTOTAL_NET_AREA_SIZE,
                            NTOTAL_COMMON_AREA_SIZE = data3b.Key.NTOTAL_COMMON_AREA_SIZE,
                            CTRANS_STATUS_NAME = data3b.Key.CTRANS_STATUS_NAME,
                            NTOTAL_PRICE = data3b.Key.NTOTAL_PRICE,
                            CTAX = data3b.Key.CTAX,
                            CTENANT_ID = data3b.Key.CTENANT_ID,
                            CTENANT_NAME = data3b.Key.CTENANT_NAME,
                            CTC_MESSAGE = data3b.Key.CTC_MESSAGE,
                        }).ToList()
                    }).ToList()
                }).ToList();

                // Set the generated data in loRtn
                _logger.LogInfo("Data processed successfully. Generating print output.");
                loData.Data = loTempData;
                loRtn.SummaryData = loData;
                loRtn.BaseHeaderData = loParam;

                _logger.LogInfo("Print output generated successfully. Saving print file.");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
            _logger.LogInfo("End - Data Generation for Print");
        }

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

        #endregion


    }
}
