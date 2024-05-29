using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GLT00100Common;
using GLT00100Common.DTOs;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;

namespace GLT00100Model
{
    public class GLT00100ViewModel : R_ViewModel<GLT00100DTO>, R_IProcessProgressStatus
    {
        private GLT00100Model _JournalModel = new GLT00100Model();
        public ObservableCollection<GLT00100JournalGridDTO> _JournalList = new ObservableCollection<GLT00100JournalGridDTO>();
        public GLT00100JournalGridDTO _Journal = new GLT00100JournalGridDTO();
        public ObservableCollection<GLT00100JournalGridDetailDTO> _JournaDetailList { get; set; } = new ObservableCollection<GLT00100JournalGridDetailDTO>();
        public ObservableCollection<GLT00100JournalGridDetailDTO> _JournaDetailListTemp { get; set; } = new ObservableCollection<GLT00100JournalGridDetailDTO>();
        public GLT00100DTO Journal = new GLT00100DTO();

        #region Collection
        public VAR_CCURRENT_PERIOD_START_DATEDTO CurrentPeriodStartCollection = new VAR_CCURRENT_PERIOD_START_DATEDTO();
        public VAR_CSOFT_PERIOD_START_DATEDTO SoftPeriodStartCollection = new VAR_CSOFT_PERIOD_START_DATEDTO();
        public VAR_GL_SYSTEM_PARAMDTO SystemParamCollection = new VAR_GL_SYSTEM_PARAMDTO();
        public VAR_GSM_COMPANYDTO CompanyCollection = new VAR_GSM_COMPANYDTO();
        public VAR_GSM_PERIODDTO GSMPeriodCollection = new VAR_GSM_PERIODDTO();
        public VAR_GSM_TRANSACTION_CODEDTO TransactionCodeCollection = new VAR_GSM_TRANSACTION_CODEDTO();
        public GSM_TRANSACTION_APPROVALDTO TransactionApprovalCollection = new GSM_TRANSACTION_APPROVALDTO();
        public VAR_IUNDO_COMMIT_JRNDTO IundoCollection = new VAR_IUNDO_COMMIT_JRNDTO();
        public List<StatusDTO> allStatusData = new List<StatusDTO>();
        public List<CurrencyCodeDTO> currencyData = new List<CurrencyCodeDTO>();
        public List<GetCenterDTO> CenterListData { get; set; } = new List<GetCenterDTO>();
        public List<GLT00100JournalGridDTO> loProcessRapidApproveOrCommitList = new List<GLT00100JournalGridDTO>();
        public List<GetMonthDTO> GetMonthList { get; set; }
        public List<VAR_USER_DEPARTMENTDTO> AllDeptData = new List<VAR_USER_DEPARTMENTDTO>();
        public Dictionary<string, string> statusMappings = new Dictionary<string, string>
        {
            [""] = "All",
            ["00"] = "Draft",
            ["10"] = "Submitted",
            ["20"] = "Approved",
            ["80"] = "Commited",
            ["99"] = "Deleted"
        };
        #endregion

        #region property
        public string LcCrecID = "";
        public string lcSearch = "";
        public string lcPeriod = "";
        public string lcStatus = "";
        public string lcDeptCode = "";
        public string COMPANYID;
        public string USERID;
        public string CCURRENT_PERIOD_YY = "";
        public string CCURRENT_PERIOD_MM = "";
        public string CSOFT_PERIOD_YY = "";
        public string CSOFT_PERIOD_MM = "";
        public string ProgressBarMessage = "";
        public int ProgressBarPercentage = 0;
        public string ResultProcessList = null;
        public string ResultFailedProcessList = null;

        public DateTime Drefdate = DateTime.Now;
        public DateTime Ddocdate = DateTime.Now;
        public DateTime Drevdate = DateTime.Now;
        public bool EnableDept = false;
        public bool buttonRapidApprove = true;
        public bool buttonRapidCommit = true;
        public bool EnableButton = false;
        public bool PredefineJournalList = false;
        public bool EnableDelete = false;
        public bool EnableSubmit = false;
        public bool EnableApprove = false;
        public bool EnableCommit = false;
        public bool EnablePrint = false;
        public bool EnableCopy = false;
        public string CSTATUS_TEMP { get; set; }
        public string CommitLabel = "Commit";
        public string SubmitLabel = "Submit";
        #endregion
        #region initial process
        public async Task GetCurrentPeriodStart(VAR_GL_SYSTEM_PARAMDTO poData)
        {
            var loEx = new R_Exception();

            try
            {
                poData.CCURRENT_PERIOD_MM = CCURRENT_PERIOD_MM;
                poData.CCURRENT_PERIOD_YY = CCURRENT_PERIOD_YY;
                var loReturn = await _JournalModel.GetCurrentPeriodStartDateAsync(poData);
                CurrentPeriodStartCollection = loReturn;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        public async Task GetSoftPeriodStart(VAR_GL_SYSTEM_PARAMDTO poData)
        {
            var loEx = new R_Exception();

            try
            {
                poData.CSOFT_PERIOD_MM = CSOFT_PERIOD_MM;
                poData.CSOFT_PERIOD_YY = CSOFT_PERIOD_YY;
                var loReturn = await _JournalModel.GetSoftPeriodStartDateAsync(poData);
                SoftPeriodStartCollection = loReturn;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        public async Task GetSystemParam()
        {
            var loEx = new R_Exception();

            try
            {
                var loreturn = await _JournalModel.GetSystemParamAsync();
                SystemParamCollection = loreturn;
                SystemParamCollection.ISOFT_PERIOD_YY = Convert.ToInt32(SystemParamCollection.CSOFT_PERIOD_YY);
                CCURRENT_PERIOD_MM = SystemParamCollection.CCURRENT_PERIOD_MM;
                CCURRENT_PERIOD_YY = SystemParamCollection.CCURRENT_PERIOD_YY;
                CSOFT_PERIOD_MM = SystemParamCollection.CSOFT_PERIOD_MM;
                CSOFT_PERIOD_YY = SystemParamCollection.CSOFT_PERIOD_YY;
                Data.CSOFT_PERIOD_MM = CSOFT_PERIOD_MM;
                Data.ISOFT_PERIOD_YY = SystemParamCollection.ISOFT_PERIOD_YY;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        public async Task GetGsmCompany()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _JournalModel.GetCompanyDTOAsync();
                CompanyCollection = loReturn;
                Journal.CCURRENCY_CODE = CompanyCollection.CLOCAL_CURRENCY_CODE;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        public async Task GetGSMPeriod()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _JournalModel.GetPeriodAsync();
                GSMPeriodCollection = loReturn;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        public async Task GetTransactionCode()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _JournalModel.GetLincementLapprovalAsync();
                TransactionCodeCollection = loReturn;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        public async Task GetIundo()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _JournalModel.GetIOptionAsync();
                IundoCollection = loReturn;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        public async Task GetStatusList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loReturn = await _JournalModel.GetStatusListAsync();
                allStatusData = loReturn.Data;
                allStatusData.Insert(0, new StatusDTO() { CCODE = "", CNAME = "All" });
                Data.CSTATUS = allStatusData.FirstOrDefault(x => x.CCODE == "").CCODE;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }
        public async Task GetDepartmentList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loReturn = await _JournalModel.GetDeptListAsync();
                AllDeptData = loReturn.Data;
                Data.CDEPT_CODE = AllDeptData.Select(m => m.CDEPT_CODE).FirstOrDefault();
                Data.CDEPT_NAME = AllDeptData.Select(m => m.CDEPT_NAME).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }
        public async Task GetCurrencyList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loReturn = await _JournalModel.GetCurrencyCodeListAsync();
                currencyData = loReturn.Data;

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }
        public async Task GetCenterList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loReturn = await _JournalModel.GetCenterListAsync();
                CenterListData = loReturn.Data;

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }
        public async Task GetTransactionApproval()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _JournalModel.GetTransactionApprovalAsync();
                TransactionApprovalCollection = loReturn;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        public async Task GetJournal(GLT00100DTO loParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _JournalModel.R_ServiceGetRecordAsync(loParam);
                Journal = loResult;

                Journal.DREF_DATE = DateTime.ParseExact(Journal.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                Journal.DDOC_DATE = DateTime.ParseExact(Journal.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                Journal.DREVERSE_DATE = DateTime.ParseExact(Journal.CREVERSE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                Journal.CUPDATE_DATE = Journal.DUPDATE_DATE.ToLongDateString();
                Journal.CCREATE_DATE = Journal.DCREATE_DATE.ToLongDateString();
                LcCrecID = Journal.CREC_ID;
                Data.CSTATUS = Journal.CSTATUS;
                await GetJournalDetailList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveJournal(GLT00100DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();
            try
            {
                poNewEntity.CTRANS_CODE = "000020";
                poNewEntity.LREVERSE = false;
                var loResult = await _JournalModel.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                Journal = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ShowAllJournals()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                lcPeriod = Data.ISOFT_PERIOD_YY + Data.CSOFT_PERIOD_MM;
                lcSearch = Data.CSEARCH_TEXT ?? "";
                lcDeptCode = Data.CDEPT_CODE;
                lcStatus = Data.CSTATUS ?? "";

                var loResult = await _JournalModel.GetJournalListAsync(lcPeriod, lcSearch, lcDeptCode, lcStatus);
                _JournalList = new ObservableCollection<GLT00100JournalGridDTO>(loResult.Data);
                foreach (var item in _JournalList)
                {
                    DateTime parsedCrefDate;
                    if (DateTime.TryParseExact(item.CREF_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedCrefDate))
                    {
                        item.DREF_DATE = parsedCrefDate;
                    }
                    DateTime parsedReverseDate;
                    if (DateTime.TryParseExact(item.CREVERSE_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedReverseDate))
                    {
                        item.DREVERSE_DATE = parsedReverseDate;
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetJournalDetailList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _JournalModel.GetAllJournalDetailListAsync(LcCrecID);

                if (loResult != null)
                {
                    var modifiedList = loResult.Data.Select((m, Index) =>
                    {
                        m.INO = Index + 1;
                        m.NAMOUNT = m.NDEBIT + m.NCREDIT;
                        if (m.CCENTER_CODE == null)
                        {
                            foreach (var item in CenterListData)
                            {
                                if (m.CCENTER_NAME == item.CCENTER_NAME)
                                {
                                    m.CCENTER_CODE = item.CCENTER_CODE;
                                }
                            }
                        }
                        return m;
                    }).ToList();

                    _JournaDetailList = new ObservableCollection<GLT00100JournalGridDetailDTO>(modifiedList);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region ProcessStatus
        public async Task ProcessCommitJournal(GLT00100JournalGridDTO poData)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                //LcCrecID = JournalEntity.CREC_ID;
                //lcDeptCode = JournalEntity.CDEPT_CODE;
                //LcCrefNo = JournalEntity.CREF_NO;
                await _JournalModel.ProcessReversingJournalAsync(poData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task ApproveJournal(GLT00100JournalGridDTO poData)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                CSTATUS_TEMP = _Journal.CSTATUS;
                poData.CSTATUS = "20";
                poData.LCOMMIT_APRJRN = SystemParamCollection.LCOMMIT_APRJRN;
                poData.CREC_ID = _Journal.CREC_ID;
                await _JournalModel.JournalProcessAsync(poData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task CommitJournal(GLT00100JournalGridDTO poData)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                CSTATUS_TEMP = Journal.CSTATUS;
                poData.CREC_ID = _Journal.CREC_ID;
                if (Journal.CSTATUS == "80")
                {
                    poData.CSTATUS = "20";
                    poData.LUNDO_COMMIT = true;
                }
                else
                {
                    poData.CSTATUS = "80";
                    poData.LUNDO_COMMIT = false;
                }
                await _JournalModel.JournalProcessAsync(poData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task SubmitJournal(GLT00100JournalGridDTO poData)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                CSTATUS_TEMP = Journal.CSTATUS;
                if (Journal.CSTATUS == "00")
                {
                    poData.CSTATUS = "10";
                }
                else
                {
                    poData.CSTATUS = "00";
                }

                await _JournalModel.JournalProcessAsync(poData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteJournal(GLT00100JournalGridDTO poData)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                CSTATUS_TEMP = Journal.CSTATUS;
                poData.CSTATUS = "99";
                poData.LCOMMIT_APRJRN = false;
                poData.LUNDO_COMMIT = false;
                await _JournalModel.JournalProcessAsync(poData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task UndoReversingJournal(GLT00100JournalGridDTO poData)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _JournalModel.UndoReversingJournalProcessAsync(poData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region "Rapid Approve and Commit"
        public async Task RapidApproveOrCommitJournal()
        {
            R_Exception loEx = new R_Exception();
            List<GLT00100JournalGridDTO> tempDataSelected = new List<GLT00100JournalGridDTO>();
            try
            {
                tempDataSelected = loProcessRapidApproveOrCommitList.Where(loData => loData.LSELECTED).ToList();

                if (loProcessRapidApproveOrCommitList.Count == 0)
                {
                    loEx.Add(new Exception("Please select Journal to process!"));
                    goto EndDetail;
                }

                loProcessRapidApproveOrCommitList = tempDataSelected;
                await ProcessDataSelected(COMPANYID, USERID);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndDetail:
            loEx.ThrowExceptionIfErrors();
        }
        public async Task ProcessDataSelected(string COMPANYID, string USERID)
        {
            var loEx = new R_Exception();
            List<GLT00100JournalGridDTO> loTemp = new List<GLT00100JournalGridDTO>();
            loTemp = loProcessRapidApproveOrCommitList;
            foreach (var item in loTemp)
            {
                if (item.CSTATUS == "80")
                {
                    item.CSTATUS = "20";
                }
                else
                {
                    item.CSTATUS = "80";
                }
            }

            try
            {
                var loUserParameters = new List<R_KeyValue>();
                //Instantiate ProcessClient
                R_ProcessAndUploadClient loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "GL",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlGL",
                    poProcessProgressStatus: this);

                //prepare Batch Parameter
                R_BatchParameter loDbPar = new R_BatchParameter();
                loDbPar.COMPANY_ID = COMPANYID;
                loDbPar.USER_ID = USERID;
                loDbPar.UserParameters = loUserParameters;
                loDbPar.ClassName = "GLT00100Back.GLT00100RapidApproveAndCommitCls";
                loDbPar.BigObject = loTemp;

                await loCls.R_BatchProcess<List<GLT00100JournalGridDTO>>(loDbPar, loTemp.Count);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            if (poProcessResultMode == eProcessResultMode.Success)
            {
                ProgressBarMessage = string.Format("Process Complete and success with GUID {0}", pcKeyGuid);
            }

            if (poProcessResultMode == eProcessResultMode.Fail)
            {
                ProgressBarMessage = string.Format("Process Complete but fail with GUID {0}", pcKeyGuid);
            }

            await Task.CompletedTask;
        }
        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            ProgressBarMessage = string.Format("Process Error with GUID {0}", pcKeyGuid);

            //StateChangeAction();

            await Task.CompletedTask;
        }
        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            ProgressBarMessage = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            ProgressBarPercentage = pnProgress;
            ProgressBarMessage = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            await Task.CompletedTask;
        }
        #endregion
    }
}
