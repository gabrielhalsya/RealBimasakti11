﻿using GSM04000Common;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Reflection;
using System.Data.Common;
using System.Diagnostics;

namespace GSM04000Back
{
    public class GSM04000Cls : R_BusinessObject<GSM04000DTO>
    {
        private RSP_GS_MAINTAIN_DEPARTMENTResources.Resources_Dummy_Class _rspDept = new();

        private RSP_GS_UPLOAD_DEPARTMENTResources.Resources_Dummy_Class _rspUploadDept = new();

        private readonly ActivitySource _activitySource;

        private LoggerGSM04000 _logger;

        public GSM04000Cls()
        {
            _logger = LoggerGSM04000.R_GetInstanceLogger();
            _activitySource = GSM04000Activity.R_GetInstanceActivitySource();
        }

        protected override void R_Deleting(GSM04000DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            string lcQuery = "";
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = "RSP_GS_MAINTAIN_DEPARTMENT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, int.MaxValue, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_NAME", DbType.String, int.MaxValue, poEntity.CDEPT_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE", DbType.String, int.MaxValue, poEntity.CCENTER_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CMANAGER_NAME", DbType.String, int.MaxValue, poEntity.CMANAGER_NAME);
                loDb.R_AddCommandParameter(loCmd, "@LEVERYONE", DbType.Boolean, int.MaxValue, poEntity.LEVERYONE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, int.MaxValue, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, int.MaxValue, "DELETE");
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);

                try
                {
                    ShowLogDebug(lcQuery, loCmd.Parameters);
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                    ShowLogError(loEx);
                }
                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                    {
                        loConn.Close();
                    }

                    loConn.Dispose();
                }
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        protected override GSM04000DTO R_Display(GSM04000DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            GSM04000DTO loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GS_GET_DEPT_DETAIL";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, int.MaxValue, poEntity.CDEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<GSM04000DTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        protected override void R_Saving(GSM04000DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            string lcQuery;
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn = null;
            string lcAction = "";
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = "RSP_GS_MAINTAIN_DEPARTMENT";

                switch (poCRUDMode)
                {
                    case eCRUDMode.AddMode:
                        lcAction = "ADD";
                        break;

                    case eCRUDMode.EditMode:
                        lcAction = "EDIT";
                        break;
                }

                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, int.MaxValue, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_NAME", DbType.String, int.MaxValue, poNewEntity.CDEPT_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE", DbType.String, int.MaxValue, poNewEntity.CCENTER_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CMANAGER_NAME", DbType.String, int.MaxValue, poNewEntity.CMANAGER_NAME);
                loDb.R_AddCommandParameter(loCmd, "@LEVERYONE", DbType.Boolean, int.MaxValue, poNewEntity.LEVERYONE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, int.MaxValue, poNewEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, int.MaxValue, lcAction);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poNewEntity.CUSER_ID);
                try
                {
                    ShowLogDebug(lcQuery, loCmd.Parameters);
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }
                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                    {
                        loConn.Close();
                    }

                    loConn.Dispose();
                }
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        public List<GSM04000DTO> GetDeptList(GSM04000ListDBParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<GSM04000DTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GS_GET_DEPT_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_LOGIN_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<GSM04000DTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public void RSP_GS_ACTIVE_INACTIVE_DEPTMethodCls(GSM04000ActiveInactiveParam poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            string lcQuery = "";
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_GS_ACTIVE_INACTIVE_DEPT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, int.MaxValue, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, int.MaxValue, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);
                ShowLogDebug(lcQuery, loCmd.Parameters);
                loDb.SqlExecNonQuery(loConn, loCmd, true);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        public bool CheckIsUserDeptExist(GSM04000DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loException = new R_Exception();
            bool loRtn = true;
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("");
                string lcQuery = $"SELECT TOP 1 1 FROM GSM_DEPT_USER (NOLOCK) WHERE CCOMPANY_ID = @CCOMPANY_ID AND CDEPT_CODE = @CDEPT_CODE";
                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, int.MaxValue, poEntity.CDEPT_CODE);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loResult = loDb.SqlExecQuery(loConn, loCmd, true);

                loRtn = loResult == null ? false : true;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        public void DeleteAssignedUserDept(GSM04000DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            string lcQuery = "";
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                R_ExternalException.R_SP_Init_Exception(loConn);
                lcQuery = "DELETE GSM_DEPT_USER WHERE CCOMPANY_ID = @CCOMPANY_ID AND CDEPT_CODE = @CDEPT_CODE";
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, int.MaxValue, poEntity.CDEPT_CODE);
                try
                {
                    ShowLogDebug(lcQuery, loCmd.Parameters);
                    loDb.SqlExecNonQuery(loConn, loCmd, true);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }
                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        #region logmethodhelper

        private void ShowLogDebug(string query, DbParameterCollection parameters)
        {
            var paramValues = string.Join(", ", parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'"));
            _logger.LogDebug($"EXEC {query} {paramValues}");
        }

        private void ShowLogError(Exception ex)
        {
            _logger.LogError(ex);
        }

        #endregion
    }
}
