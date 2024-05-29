using PMR00200COMMON;
using PMR00200COMMON.DTO_s;
using R_BackEnd;
using R_Common;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;

namespace PMR00200BACK
{
    public class PMR00200Cls
    {
        private PMR00200Logger _logger;

        private readonly ActivitySource _activitySource;

        public PMR00200Cls()
        {
            _logger = PMR00200Logger.R_GetInstanceLogger();
            _activitySource = PMR00200Activity.R_GetInstanceActivitySource();
        }

        public List<PMR00200DTO> GetReportData(PMR00200ParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<PMR00200DTO> loRtn = null;
            R_Db loDB=null;
            DbConnection loConn=null;
            DbCommand loCmd=null;
            string lcQuery="";
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PMR00200_GET_REPORT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poParam.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFROM_DEPARTMENT_ID", DbType.String, int.MaxValue, poParam.CFROM_DEPARTMENT_ID);
                loDB.R_AddCommandParameter(loCmd, "@CTO_DEPARTMENT_ID", DbType.String, int.MaxValue, poParam.CTO_DEPARTMENT_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFROM_SALESMAN_ID", DbType.String, int.MaxValue, poParam.CFROM_SALESMAN_ID);
                loDB.R_AddCommandParameter(loCmd, "@CTO_SALESMAN_ID", DbType.String, int.MaxValue, poParam.CTO_SALESMAN_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFROM_PERIOD", DbType.String, int.MaxValue, poParam.CFROM_PERIOD);
                loDB.R_AddCommandParameter(loCmd, "@CTO_PERIOD", DbType.String, int.MaxValue, poParam.CTO_PERIOD);
                loDB.R_AddCommandParameter(loCmd, "@LIS_OUTSTANDING", DbType.Boolean, 2, poParam.LIS_OUTSTANDING);
                loDB.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, int.MaxValue, poParam.CLANG_ID);
                
                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PMR00200DTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
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
