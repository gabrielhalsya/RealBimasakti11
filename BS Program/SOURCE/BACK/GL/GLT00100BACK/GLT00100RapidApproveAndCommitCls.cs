using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using GLT00100Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GLT00100Back
{
    public class GLT00100RapidApproveAndCommitCls : R_IBatchProcess
    {
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            var loDb = new R_Db();
         
            try
            {
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("", "Connection to database failed");
                    goto EndBlock;
                }

                var loTask = Task.Run(() =>
                {
                    _BatchProcess(poBatchProcessPar);
                });

                while (!loTask.IsCompleted)
                {
                    Thread.Sleep(100);
                }

                if (loTask.IsFaulted)
                {
                    loException.Add(loTask.Exception.InnerException != null ?
                        loTask.Exception.InnerException :
                        loTask.Exception);

                    goto EndBlock;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            R_Exception loExceptionDt;
            string lcQuery = "";
            string lcStatusProcess = null;
            string loStatusFinish = null;
            string lsError = "";
            string lcQueryMessage;
            var loDb = new R_Db();
            DbCommand loCommand = null;
            DbConnection loConn = null;
            int lcStep = 0;
            int lcErrorCount = 0;
            int lcTotal;
            bool llStatusApprove;
            string lcCompany;
            string lcUserId;
            string lcGuid;
            var loTempListForProcess = R_NetCoreUtility.R_DeserializeObjectFromByte<List<GLT00100JournalGridDTO>>(poBatchProcessPar.BigObject);
            try
            {
                await Task.Delay(100);

                lcTotal = loTempListForProcess.Count;
                lcCompany = poBatchProcessPar.Key.COMPANY_ID;
                lcUserId = poBatchProcessPar.Key.USER_ID;
                lcGuid = poBatchProcessPar.Key.KEY_GUID;
                loCommand = loDb.GetCommand();
                loConn = loDb.GetConnection();

                lcQuery = @"RSP_WRITEUPLOADPROCESSSTATUS";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CoId", DbType.String, 50, lcCompany);
                loDb.R_AddCommandParameter(loCommand, "@UserId", DbType.String, 50, lcUserId);
                loDb.R_AddCommandParameter(loCommand, "@KeyGUID", DbType.String, 50, lcGuid);
                loDb.R_AddCommandParameter(loCommand, "@Step", DbType.Int32, 256, lcStep);
                loDb.R_AddCommandParameter(loCommand, "@Status", DbType.String, 500, "Start Processing Journal");
                loDb.R_AddCommandParameter(loCommand, "@Finish", DbType.Int32, 20, 0);
                loDb.SqlExecNonQuery(loConn, loCommand, false);


                lcStep = 1;
                foreach (var item in loTempListForProcess)
                {
                    loExceptionDt = new R_Exception();

                    try
                    {
                        lcStatusProcess = string.Format("Process data {0} of  {1} ..", lcStep, lcTotal);
                        lcQueryMessage = string.Format("EXEC RSP_WRITEUPLOADPROCESSSTATUS @CoId, @UserId, @KeyGUID, {0}, '{1}', 0", lcStep, lcStatusProcess);
                        loCommand.CommandText = lcQueryMessage;
                        loCommand.CommandType = CommandType.Text;
                        loDb.SqlExecNonQuery(loConn, loCommand, false);

                        llStatusApprove = ProcessEachApproveOrCommit(lcCompany, lcUserId, item, lcGuid, loConn);
                        if (llStatusApprove == false)
                        {
                            lcErrorCount += 1;
                            lsError = string.Format("Failed to process Master Ref. No. CREF_NO {0} !! ", item.CREF_NO);
                        }
                        else
                        {
                            lsError = string.Format("Processing Master Ref. No. CREF_NO {0} !!", item.CREF_NO);

                        }

                        lcQueryMessage = string.Format("EXEC RSP_WRITEUPLOADPROCESSSTATUS @CoId, @UserId, @KeyGUID, {0}, '{1}', 0", lcStep, lsError);
                        loCommand.CommandText = lcQueryMessage;
                        loCommand.CommandType = CommandType.Text;
                        loDb.SqlExecNonQuery(loConn, loCommand, false);

                    }
                    catch (Exception ex)
                    {
                        loExceptionDt.Add(ex);
                    }
                    //UNHANDLED Error
                    if (loExceptionDt.Haserror)
                    {
                        lcQueryMessage = $"INSERT INTO GST_UPLOAD_ERROR_STATUS (CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE)" +
                                         $"VALUES " +
                                         $"( '{lcCompany}', '{lcUserId}','{lcGuid}', {lcStep}, '{loExceptionDt.ErrorList.FirstOrDefault().ErrDescp}') ;";

                        loCommand.CommandText = lcQueryMessage;
                        loCommand.CommandType = CommandType.Text;
                        loDb.SqlExecNonQuery(loConn, loCommand, false);

                        lcQueryMessage = string.Format(
                            "EXEC RSP_WRITEUPLOADPROCESSSTATUS @CoId, @UserId, @KeyGUID, {0}, '{1}', 0",
                            lcStep, loExceptionDt.ErrorList.FirstOrDefault().ErrDescp);

                        loCommand.CommandText = lcQueryMessage;
                        loCommand.CommandType = CommandType.Text;
                        loDb.SqlExecNonQuery(loConn, loCommand, false);
                    }

                    lcStep += 1;
                }

                var flag = (lcErrorCount == 0) ? 1 : 9;
                if (flag == 1)
                {
                    loStatusFinish = "Finish Processing Reversing Journal!";
                }
                else
                {
                    loStatusFinish = "Finish Processing Reversing Journal but fail!";
                }

                var lcQueryFinish = $@"EXEC RSP_WRITEUPLOADPROCESSSTATUS @CoId, @UserId, @KeyGUID, '{lcStep}', '{loStatusFinish}', '{flag}'";
                loCommand.CommandText = lcQueryFinish;
                loCommand.CommandType = CommandType.Text;
                loDb.SqlExecNonQuery(loConn, loCommand, false);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                }
                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
            }

            if (loException.Haserror)
            {
                lcQueryMessage = @"INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE)
                                 VALUES 
                                 ( '{poBatchProcessPar.Key.COMPANY_ID}', '{poBatchProcessPar.Key.USER_ID}','{poBatchProcessPar.Key.KEY_GUID}', {100}, '{loException.ErrorList[0].ErrDescp}' );";
                loCommand.CommandText = lcQueryMessage;
                loCommand.CommandType = CommandType.Text;
                loDb.SqlExecNonQuery(loConn, loCommand, false);


                lcQuery = @"EXEC RSP_WriteUploadProcessStatus '{poBatchProcessPar.Key.COMPANY_ID}','{poBatchProcessPar.Key.USER_ID}','{poBatchProcessPar.Key.KEY_GUID}',
                          100, '{loException.ErrorList[0].ErrDescp}', 9";
                loDb.SqlExecNonQuery(lcQuery);
            }

        }

        public bool ProcessEachApproveOrCommit(string Company, string UserId, GLT00100JournalGridDTO poData, string pcGuid, DbConnection poConn)
        {
            var loEx = new R_Exception();
            DbConnection loConn = null;
            DbCommand loCommand = null;
            R_Db loDb = new R_Db();
            bool lbRtn = false;
            string lcQuery;
            try
            {
                loConn = poConn;
                loCommand = loDb.GetCommand();

                lcQuery = @"RSP_GL_UPDATE_JOURNAL_STATUS";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, Company);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, UserId);
                loDb.R_AddCommandParameter(loCommand, "@CAPPROVE_BY", DbType.String, 50, UserId);
                loDb.R_AddCommandParameter(loCommand, "@CJRN_ID_LIST", DbType.String, 50, poData.CREC_ID);
                loDb.R_AddCommandParameter(loCommand, "@CNEW_STATUS", DbType.String, 50, poData.CSTATUS);
                loDb.R_AddCommandParameter(loCommand, "@LAUTO_COMMIT", DbType.Boolean, 50, poData.LCOMMIT_APRJRN);
                loDb.R_AddCommandParameter(loCommand, "@LUNDO_COMMIT", DbType.Boolean, 50, 0);

                loDb.SqlExecNonQuery(loConn, loCommand, false);
                lbRtn = true;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
            return lbRtn;
        }


    }
}
