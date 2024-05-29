using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using GLT00100Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Transactions;
using System.Windows.Input;

namespace GLT00100Back
{
    public class InitCLS
    {
        public VAR_GSM_COMPANYDTO GetJournalCompany(GLT00100ParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            VAR_GSM_COMPANYDTO loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_GS_GET_COMPANY_INFO";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<VAR_GSM_COMPANYDTO>(loReturnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public VAR_GL_SYSTEM_PARAMDTO GetSystemParam(GLT00100ParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            VAR_GL_SYSTEM_PARAMDTO loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_GL_GET_SYSTEM_PARAM";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 50, poParameter.CLANGUAGE_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<VAR_GL_SYSTEM_PARAMDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public List<VAR_USER_DEPARTMENTDTO> GetDeptList(GLT00100ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<VAR_USER_DEPARTMENTDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_DEPT_LOOKUP_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType= CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<VAR_USER_DEPARTMENTDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public VAR_CCURRENT_PERIOD_START_DATEDTO GetCurrentPeriodStartDate(GLT00100ParameterDTO poParameter, VAR_GL_SYSTEM_PARAMDTO poData)
        {
            R_Exception loException = new R_Exception();
            VAR_CCURRENT_PERIOD_START_DATEDTO loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_GS_GET_PERIOD_DT_INFO";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CYEAR", DbType.String, 50, poData.CCURRENT_PERIOD_YY);
                loDb.R_AddCommandParameter(loCommand, "@CPERIOD_NO", DbType.String, 50, poData.CCURRENT_PERIOD_MM);
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<VAR_CCURRENT_PERIOD_START_DATEDTO>(loReturnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public VAR_CSOFT_PERIOD_START_DATEDTO GetSoftPeriodStartDate(GLT00100ParameterDTO poParameter, VAR_GL_SYSTEM_PARAMDTO poData)
        {
            R_Exception loException = new R_Exception();
            VAR_CSOFT_PERIOD_START_DATEDTO loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_GS_GET_PERIOD_DT_INFO";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CYEAR", DbType.String, 50, poData.CSOFT_PERIOD_YY);
                loDb.R_AddCommandParameter(loCommand, "@CPERIOD_NO", DbType.String, 50, poData.CSOFT_PERIOD_MM);
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<VAR_CSOFT_PERIOD_START_DATEDTO>(loReturnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public VAR_IUNDO_COMMIT_JRNDTO GetIOption(GLT00100ParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            VAR_IUNDO_COMMIT_JRNDTO loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_GL_GET_SYSTEM_ENABLE_OPTION_INFO";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@COPTION_CODE", DbType.String, 50, "GL014001");
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<VAR_IUNDO_COMMIT_JRNDTO>(loReturnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public VAR_GSM_TRANSACTION_CODEDTO GetTranscode(GLT00100ParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            VAR_GSM_TRANSACTION_CODEDTO loReturn = null;
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");
                DbCommand loCmd = loDb.GetCommand();

                //get transcode
                string lcQuery = "RSP_GS_GET_TRANS_CODE_INFO";
                string lcTransCode = "000000";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 11, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 11, lcTransCode);
                var loResult = loDb.SqlExecQuery(loConn, loCmd, true);
                loReturn = R_Utility.R_ConvertTo<VAR_GSM_TRANSACTION_CODEDTO>(loResult).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        public VAR_GSM_PERIODDTO GetPeriod(GLT00100ParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            VAR_GSM_PERIODDTO loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_GS_GET_PERIOD_YEAR_RANGE";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CYEAR", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCommand, "@CMODE", DbType.String, 50, "");
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<VAR_GSM_PERIODDTO>(loReturnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public List<StatusDTO> GetStatus(GLT00100ParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            List<StatusDTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn = null;
            DbCommand loCmd;
            string lcQuery = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_GS_GET_GSB_CODE_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                string lcApp = "BIMASAKTI";
                string lcGLStat = "_GL_JOURNAL_STATUS";

                loDb.R_AddCommandParameter(loCmd, "@CAPPLICATION", DbType.String, lcApp.Length + 1, lcApp);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCLASS_ID", DbType.String, 50, lcGLStat);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParameter.CLANGUAGE_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<StatusDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public List<CurrencyCodeDTO> GetCurrency(GLT00100ParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            List<CurrencyCodeDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GS_GET_CURRENCY_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<CurrencyCodeDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public List<GetCenterDTO> GetCenterList(GLT00100ParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            List<GetCenterDTO> loRtn = null;
            R_Db loDb;
            DbConnection loConn = null;
            DbCommand loCmd;
            string lcQuery = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_GS_GET_CENTER_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<GetCenterDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public GSM_TRANSACTION_APPROVALDTO GetTransactionApproval(GLT00100ParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            GSM_TRANSACTION_APPROVALDTO loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery =
                    @"SELECT TOP 1 1 as CRESULT
                      FROM GSM_TRANSACTION_APPROVAL (NOLOCK)
                      WHERE CCOMPANY_ID = @CCOMPANY_ID
                      AND   CTRANS_CODE = @CTRANS_CODE
                      AND   CDEPT_CODE  = @CDEPT_CODE
                      AND   CUSER_ID    = @CUSER_ID";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 50, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<GSM_TRANSACTION_APPROVALDTO>(loReturnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }
    }
}
