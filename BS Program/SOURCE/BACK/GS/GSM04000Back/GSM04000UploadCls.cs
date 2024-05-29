using GSM04000Common;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;
using RSP_GS_MAINTAIN_DEPARTMENTResources;
using RSP_GS_UPLOAD_DEPARTMENTResources;

namespace GSM04000Back
{
    public class GSM04000UploadCls : R_IBatchProcess
    {
        private RSP_GS_UPLOAD_DEPARTMENTResources.Resources_Dummy_Class _rspUploadDept = new();
        
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            var loDb = new R_Db();

            try
            {
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("01", "Database Connection Failed");
                    goto EndBlock;
                }

                var loTask = Task.Run(() =>
                {
                    _BatchProcessAsync(poBatchProcessPar);
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

        public async Task _BatchProcessAsync(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbCommand loCmd = null;
            DbConnection loConn = null;
            var lcQuery = "";
            try
            {
                // must delay for wait this method is completed in syncronous
                await Task.Delay(100);

                //getting data
                loDb = new R_Db();
                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<GSM04000ExcelGridDTO>>(poBatchProcessPar.BigObject);

                //convert to dto
                var loObject = loTempObject.Select(loTemp => new GSM04000ExcelToUploadDTO
                {
                    No = loTemp.INO,
                    DepartmentCode = loTemp.CDEPT_CODE,
                    DepartmentName = loTemp.CDEPT_NAME,
                    CenterCode = loTemp.CCENTER_CODE,
                    ManagerName = loTemp.CMANAGER_CODE,
                    Everyone = loTemp.LEVERYONE,
                    Active = loTemp.LACTIVE,
                    NonActiveDate = loTemp.CNON_ACTIVE_DATE,
                }).ToList();

                //get command & connection
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery += $"CREATE TABLE #DEPARTMENT " +
                              $"( No INT, " +
                              $"DepartmentCode VARCHAR(254), " +
                              $"DepartmentName VARCHAR(254), " +
                              $"CenterCode VARCHAR(254)," +
                              $"ManagerName VARCHAR(254)," +
                              $"Everyone BIT," +
                              $"Active BIT," +
                              $"NonActiveDate VARCHAR(8))";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<GSM04000ExcelToUploadDTO>((SqlConnection)loConn, "#DEPARTMENT", loObject);

                lcQuery = "RSP_GS_UPLOAD_DEPARTMENT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

                loDb.SqlExecNonQuery(loConn, loCmd, false);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (!(loConn.State == ConnectionState.Closed))
                        loConn.Close();
                    loConn.Dispose();
                    loConn = null;
                }

                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }

            if (loException.Haserror)
            {

                lcQuery = $"INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE) VALUES " +
                    $"('{poBatchProcessPar.Key.COMPANY_ID}', '{poBatchProcessPar.Key.USER_ID}','{poBatchProcessPar.Key.KEY_GUID}', -1, '{loException.ErrorList[0].ErrDescp}')";
                loDb.SqlExecNonQuery(lcQuery);

                lcQuery = $"EXEC RSP_WriteUploadProcessStatus '{poBatchProcessPar.Key.COMPANY_ID}', '{poBatchProcessPar.Key.USER_ID}', '{poBatchProcessPar.Key.KEY_GUID}', 100, '{loException.ErrorList[0].ErrDescp}', 9";

                loDb.SqlExecNonQuery(lcQuery);
            }
        }

    }
}
