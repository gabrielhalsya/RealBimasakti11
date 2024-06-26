﻿using BaseHeaderReportCOMMON;
using GLM00200BACK;
using GLM00200COMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;
using System.Collections;
using System.Diagnostics;
using System.Globalization;

namespace GLM00200SERVICE
{
    public class GLM00200PrintController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private JournalDTO _AllGLM00200Parameter;
        private LoggerGLM00200Print _LoggerPrint;
        private readonly ActivitySource _activitySource;

        #region instantiate
        public GLM00200PrintController(ILogger<LoggerGLM00200Print> logger)
        {
            //Initial and Get Logger
            LoggerGLM00200Print.R_InitializeLogger(logger);
            _LoggerPrint = LoggerGLM00200Print.R_GetInstanceLogger();
            //_activitySource = GLR00200PrintActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GLM00200PrintController));

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "GLM00200.frx");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateDataPrint(_AllGLM00200Parameter));
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
        public R_DownloadFileResultDTO AllGLRecurringJournalPost(JournalDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("AllGLRecurringJournalPost");
            _LoggerPrint.LogInfo("Start AllGLRecurringJournalPost");
            R_Exception loException = new R_Exception();
            GLM00200PrintLogKeyDTO<JournalDTO> loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new GLM00200PrintLogKeyDTO<JournalDTO>
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };

                // Set Guid Param 
                _LoggerPrint.LogInfo("Set GUID Param AllGLRecurringJournalPost");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<GLM00200PrintLogKeyDTO<JournalDTO>>(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            _LoggerPrint.LogInfo("End AllGLRecurringJournalPost");
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult AllStreamGLRecurringJournalGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity("AllStreamGLRecurringJournalGet");
            R_Exception loException = new R_Exception();
            GLM00200PrintLogKeyDTO<JournalDTO> loResultGUID = null;
            _LoggerPrint.LogInfo("Start AllStreamGLRecurringJournalGet");
            FileStreamResult loRtn = null;
            try
            {
                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<GLM00200PrintLogKeyDTO<JournalDTO>>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                _AllGLM00200Parameter = loResultGUID.poParam;

                _LoggerPrint.LogInfo("Read File Report AllStreamGLRecurringJournalGet");
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF), R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _LoggerPrint.LogInfo("End AllStreamGLRecurringJournalGet");

            return loRtn;
        }

        #region Helper
        private GLM00200ResultWithBaseHeaderPrintDTO GenerateDataPrint(JournalDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GenerateDataPrint");
            var loEx = new R_Exception();
            GLM00200ResultWithBaseHeaderPrintDTO loRtn = new GLM00200ResultWithBaseHeaderPrintDTO();

            try
            {
                var loCls = new GLM00200Cls();

                _LoggerPrint.LogInfo("Call Method GetSummaryGLLedger And GetDetailGLLEdger Report");
                var loSummaryData = loCls.GLM00200HeaderReport(poParam);
                var loDetailData = loCls.GLM00200DetailListReport(poParam);

                GLM00200ResultPrintDTO loData = new GLM00200ResultPrintDTO() { LabelPrint = new JournalLabelPrint() };

                _LoggerPrint.LogInfo("Mapping Report Report");
                loSummaryData.CREF_DATE_Display = DateTime.TryParseExact(loSummaryData.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate) ? ldRefDate.ToString("dd-MM-yyyy") : "";
                loSummaryData.CSTART_DATE_Display = DateTime.TryParseExact(loSummaryData.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldStartDate) ? ldStartDate.ToString("dd-MM-yyyy") : "";
                loSummaryData.CNEXT_DATE_Display = DateTime.TryParseExact(loSummaryData.CNEXT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldNextDate) ? ldNextDate.ToString("dd-MM-yyyy") : "";
                loSummaryData.CLAST_DATE_Display = DateTime.TryParseExact(loSummaryData.CLAST_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldLastDate) ? ldLastDate.ToString("dd-MM-yyyy") : "";
                loSummaryData.CFIX_RATE = loSummaryData.LFIX_RATE == true ? "Yes" : "No";

                loDetailData.ForEach(x =>
                {
                    x.CREF_DATE_Display = DateTime.TryParseExact(x.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDDate) ? ldRefDDate.ToString("dd-MM-yyyy") : "";
                });

                _LoggerPrint.LogInfo("Set BaseHeader Report");
                // set Base Header Common
                var loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "PT Realta Chackradarma",
                    CPRINT_CODE = poParam.CCOMPANY_ID.ToUpper(),
                    CPRINT_NAME = "ACTUAL RECURRING JOURNAL",
                    CUSER_ID = poParam.CUSER_ID.ToUpper(),
                };
                var loBaseHeader = loCls.GetBaseHeaderLogoCompany();
                loParam.BLOGO_COMPANY = loBaseHeader.CLOGO;

                // Set ResponseModelData
                loData.HeaderData = loSummaryData;
                loData.DetailData = loDetailData;

                _LoggerPrint.LogInfo("Set Data Report");
                // Set All Return Data
                loRtn.BaseHeaderData = loParam;
                loRtn.Data = loData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _LoggerPrint.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        #endregion
    }
}