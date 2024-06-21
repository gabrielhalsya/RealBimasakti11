using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR00200BACK;
using PMR00200COMMON;
using PMR00200COMMON.DTO_s;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI.Log;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using PMR00200COMMON.Print_DTO;
using PMR00200PrintLogKey = PMR00200COMMON.PMR00200PrintLogKey;


namespace PMR00200SERVICE
{
    public class PMR00210PrintController : R_ReportControllerBase
    {
        private PMR00200PrintLogger _logger;
        private R_ReportFastReportBackClass _ReportCls;
        private PMR00200ParamDTO _Parameter;
        private readonly ActivitySource _activitySource;
        PMR00200BackResources.Resources_Dummy_Class _backRes = new();


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
        public R_DownloadFileResultDTO DownloadResultPrintPost(PMR00200ParamDTO poParameter)
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
            _logger.LogInfo("End - Print Overtime");
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult LOIStatusSummary_ReportListGet(string pcGuid)
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
            _logger.LogInfo("End - Overtime Report Generation");
            return loRtn;
        }

        #region Helper

        private PMR00200PrintDislpayDTO GenerateDataPrint(PMR00200ParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            var loEx = new R_Exception();
            PMR00200PrintDislpayDTO loRtn = new PMR00200PrintDislpayDTO();
            var loParam = new BaseHeaderDTO();

            System.Globalization.CultureInfo loCultureInfo =
                new System.Globalization.CultureInfo(poParam.CREPORT_CULTURE);

            try
            {
                _logger.LogInfo("_logger.LogInfo(\"Start - Generating data for Print\");\n data for Overtime report.");

                //Add Resources
                loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                    "Page", loCultureInfo);
                loRtn.BaseHeaderColumn.Of =
                    R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_Date =
                    R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
                loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                    "Print_By", loCultureInfo);

                PMR00200LabelDTO loColumnObject = new PMR00200LabelDTO();
                var loColumn = AssignValuesWithMessages(typeof(PMR00200BackResources.Resources_Dummy_Class), loCultureInfo, loColumnObject);

                // Set base header data
                _logger.LogDebug("Deserialized Print Parameters: {@PrintParameters}");

                loParam.CCOMPANY_NAME = poParam.CCOMPANY_ID.ToUpper();
                loParam.CPRINT_CODE = "PMR002400";
                loParam.CPRINT_NAME = PMR00200ContextConstant.CPROGRAM_NAME;
                loParam.CUSER_ID = poParam.CUSER_ID.ToUpper();

                // Create an instance of PMR01000Cls
                var loCls = new PMR00200Cls();

                _logger.LogInfo("Set BaseHeader Report");
                loParam.BLOGO_COMPANY = loCls.GetBaseHeaderLogoCompany(poParam.CCOMPANY_ID).CLOGO;

                // Create an instance 
                PMR00200ReportDataDTO loData = new PMR00200ReportDataDTO()
                {
                    Title = PMR00200ContextConstant.CPROGRAM_NAME,
                    Header = PMR00200ContextConstant.CPROGRAM_NAME,
                    Column = (PMR00200LabelDTO)loColumn,
                    HeaderParam = poParam,
                    Data = new List<PMR00200DataDTO>(),
                };

                // Get print data for Group Of Account report
                var loCollData = loCls.GetReportData(poParam);
                _logger.LogInfo("Data generation successful. Processing data for printing.");

                // Set the generated data in loRtn
                _logger.LogInfo("Data processed successfully. Generating print output.");
                var loMappingData = R_Utility.R_ConvertCollectionToCollection<PMR00200SPResultDTO, PMR00200DataDTO>(loCollData);
                loData.Data = new List<PMR00200DataDTO>(loMappingData);
                loRtn.BaseHeaderData = loParam;
                loRtn.SummaryData = loData;

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
