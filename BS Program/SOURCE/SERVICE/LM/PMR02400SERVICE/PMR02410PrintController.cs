using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR02400BACK;
using PMR02400COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI.Log;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using R_Cache;
using PMR02400COMMON.DTO_s;
using System.Reflection;
using BaseHeaderReportCOMMON;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using PMR02400PrintLogKey = PMR02400COMMON.PMR02400PrintLogKey;
using PMR02400COMMON.DTO_s.Print;

namespace PMR02400SERVICE
{
    public class PMR02410PrintController : R_ReportControllerBase
    {
        private PMR02400PrintLogger _logger;
        private R_ReportFastReportBackClass _ReportCls;
        private PMR02400ParamDTO _Parameter;
        private readonly ActivitySource _activitySource;

        public PMR02410PrintController(ILogger<PMR02400PrintLogger> logger)
        {
            PMR02400PrintLogger.R_InitializeLogger(logger);
            _logger = PMR02400PrintLogger.R_GetInstanceLogger();
            _activitySource = PMR02400Activity.R_InitializeAndGetActivitySource(nameof(PMR02410PrintController));


            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }

        #region reporthelper

        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "PMR02400Summary.frx");
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
        public R_DownloadFileResultDTO DownloadResultPrintPost(PMR02400ParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            _logger.LogInfo("Start - Post DownloadResultPrintPost Status");
            R_Exception loException = new R_Exception();
            PMR02400PrintLogKey loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new PMR02400PrintLogKey
                {
                    poParamSummary = poParameter,
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
        public FileStreamResult PenaltySummary_ReportListGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            PMR02400PrintLogKey loResultGUID = null;
            try
            {
                // Deserialize the GUID from the cache
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<PMR02400PrintLogKey>(R_DistributedCache.Cache.Get(pcGuid));
                _logger.LogDebug("Deserialized GUID: {pcGuid}", pcGuid);

                // Get Parameter
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);

                _Parameter = loResultGUID.poParamSummary;

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

        private PMR02400PrintDisplayDTO GenerateDataPrint(PMR02400ParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            var loEx = new R_Exception();
            PMR02400PrintDisplayDTO loRtn = new PMR02400PrintDisplayDTO();
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

                PMR02400LabelDTO loColumnObject = new PMR02400LabelDTO();
                var loColumn = AssignValuesWithMessages(typeof(PMR02400BackResources.Resources_Dummy_Class), loCultureInfo, loColumnObject);

                // Set base header data
                _logger.LogDebug("Deserialized Print Parameters: {@PrintParameters}");

                loParam.CCOMPANY_NAME = poParam.CCOMPANY_ID.ToUpper();
                loParam.CPRINT_CODE = "PMR002400";
                loParam.CPRINT_NAME = PMR02400ContextConstant.CPROGRAM_NAME;
                loParam.CUSER_ID = poParam.CUSER_ID.ToUpper();
                
                // Create an instance of PMR01000Cls
                var loCls = new PMR02400Cls();

                _logger.LogInfo("Set BaseHeader Report");
                loParam.BLOGO_COMPANY = loCls.GetBaseHeaderLogoCompany(poParam.CCOMPANY_ID).CLOGO;

                // Create an instance 
                PMR02400ReportDataDTO loData = new PMR02400ReportDataDTO()
                {
                    Title = PMR02400ContextConstant.CPROGRAM_NAME,
                    Header = PMR02400ContextConstant.CPROGRAM_NAME,
                    Label = (PMR02400LabelDTO)loColumn,
                    Param = poParam,
                    Data = new List<PMR02400DataDTO>(),
                };
                
                // Get print data for Group Of Account report
                var loCollData = loCls.GetSummaryData(poParam);
                _logger.LogInfo("Data generation successful. Processing data for printing.");

                // Set the generated data in loRtn
                _logger.LogInfo("Data processed successfully. Generating print output.");
                var loMappingData = R_Utility.R_ConvertCollectionToCollection<PMR02400SPResultDTO,PMR02400DataDTO>(loCollData);
                loData.Data = new List<PMR02400DataDTO>(loMappingData);
                loRtn.BaseHeaderData = loParam;
                loRtn.ReportDataDTO = loData;

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
