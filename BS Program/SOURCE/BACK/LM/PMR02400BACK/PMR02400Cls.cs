﻿using PMR02400COMMON;
using PMR02400COMMON.DTO_s;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_BackEnd;
using R_Common;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;

namespace PMR02400BACK
{
    public class PMR02400Cls
    {
        private PMR02400Logger _logger;

        private readonly ActivitySource _activitySource;

        public PMR02400Cls()
        {
            _logger = PMR02400Logger.R_GetInstanceLogger();
            _activitySource = PMR02400Activity.R_GetInstanceActivitySource();
        }

        public List<PMR02400SPResultDTO> GetSummaryData(PMR02400SpParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<PMR02400SPResultDTO> loRtn = null;
            R_Db loDB = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery = "";
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PMR02400_GET_REPORT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poParam.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CCUT_OFF", DbType.String, int.MaxValue, poParam.CCUT_OFF);
                loDB.R_AddCommandParameter(loCmd, "@FROM_CPERIOD", DbType.String, int.MaxValue, poParam.FROM_CPERIOD);
                loDB.R_AddCommandParameter(loCmd, "@CTO_CPERIOD", DbType.String, int.MaxValue, poParam.CTO_CPERIOD);
                loDB.R_AddCommandParameter(loCmd, "@CREPORT_TYPE", DbType.String, int.MaxValue, poParam.CREPORT_TYPE);
                loDB.R_AddCommandParameter(loCmd, "@CFROM_CUSTOMER_ID", DbType.String, int.MaxValue, poParam.CFROM_CUSTOMER_ID);
                loDB.R_AddCommandParameter(loCmd, "@CTO_CUSTOMER_ID", DbType.String, int.MaxValue, poParam.CTO_CUSTOMER_ID);
                loDB.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, int.MaxValue, poParam.CLANG_ID);
               

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PMR02400SPResultDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public List<PMR02401SPResultDTO> GetDetailData(PMR02400SpParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<PMR02401SPResultDTO> loRtn = null;
            R_Db loDB = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery = "";
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PMR02400_GET_REPORT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poParam.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CCUT_OFF", DbType.String, int.MaxValue, poParam.CCUT_OFF);
                loDB.R_AddCommandParameter(loCmd, "@FROM_CPERIOD", DbType.String, int.MaxValue, poParam.FROM_CPERIOD);
                loDB.R_AddCommandParameter(loCmd, "@CTO_CPERIOD", DbType.String, int.MaxValue, poParam.CTO_CPERIOD);
                loDB.R_AddCommandParameter(loCmd, "@CREPORT_TYPE", DbType.String, int.MaxValue, poParam.CREPORT_TYPE);
                loDB.R_AddCommandParameter(loCmd, "@CFROM_CUSTOMER_ID", DbType.String, int.MaxValue, poParam.CFROM_CUSTOMER_ID);
                loDB.R_AddCommandParameter(loCmd, "@CTO_CUSTOMER_ID", DbType.String, int.MaxValue, poParam.CTO_CUSTOMER_ID);
                loDB.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, int.MaxValue, poParam.CLANG_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PMR02401SPResultDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public PrintLogoResultDTO GetBaseHeaderLogoCompany(string pcCompanyId)
        {
            using Activity activity = _activitySource.StartActivity("GetBaseHeaderLogoCompany");
            var loEx = new R_Exception();
            PrintLogoResultDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_ReportConnectionString");
                var loCmd = loDb.GetCommand();


                var lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, pcCompanyId);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _logger.LogDebug("SELECT dbo.RFN_GET_COMPANY_LOGO({@CCOMPANY_ID}) as CLOGO", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PrintLogoResultDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #region log method helper

        private void ShowLogDebug(string pcQuery, DbParameterCollection poParam)
        {
            var paramValues = string.Join(", ", poParam.Cast<DbParameter>().Select(p => $"{p.ParameterName} : '{p.Value}'"));
            _logger.LogDebug($"EXEC {pcQuery} {paramValues}");
        }

        private void ShowLogError(Exception poException)
        {
            _logger.LogError(poException);
        }

        #endregion
    }
}
