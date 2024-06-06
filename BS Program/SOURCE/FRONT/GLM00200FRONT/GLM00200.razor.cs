using GLM00200Common;
using GLM00200Model;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GLM00200Front
{
    public partial class GLM00200 : R_Page
    {
        private GLM00200ViewModel _journalVM = new GLM00200ViewModel();
        private GLM00200RecurryEntryViewModel _journalProcessVM = new GLM00200RecurryEntryViewModel();
        private R_Grid<JournalDTO> _gridJournal;
        private R_ConductorGrid _conJournal;

        private R_TabPage _tabPage_RecurringEntry; //ref TabPage tab2
        private R_TabPage _tabPage_RecurringActualJournal; //ref TabPage tab2
        private R_TabStrip _tabStrip_Recurring; //ref Tabstrip

        private R_Grid<JournalDetailGridDTO> _gridJournalDet;
        [Inject] IJSRuntime JS { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _journalVM.GetInitData();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region TabPage
        private void BeforeOpenTabPage_RecurringEntry(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = _journalVM.Journal;
                eventArgs.TargetPageType = typeof(GLM00210);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void BeforeOpenTabPage_RecurringActualJournal(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = _journalVM.Journal;
                eventArgs.TargetPageType = typeof(GLM00220);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion


        #region TAB Predefined
        private void Predef_RecurringEntry(R_InstantiateDockEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = _journalVM.Journal;
                eventArgs.TargetPageType = typeof(GLM00210);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void AfterPredef_RecurringEntry(R_AfterOpenPredefinedDockEventArgs eventArgs)
        { }
        private void Predef_ActualJournalList(R_InstantiateDockEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = _journalVM.Journal;
                eventArgs.TargetPageType = typeof(GLM00220);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private void AfterPredef_ActualJournalList(R_AfterOpenPredefinedDockEventArgs eventArgs)
        { }
        #endregion

        #region Search
        public async Task SearchAllAsync()
        {
            var loEx = new R_Exception();
            try
            {
                _journalVM.Parameter.CSEARCH_TEXT = "";
                await _gridJournal.R_RefreshGrid(null);
                if (_journalVM.JournalGrid.Count == 0)
                {
                    _journalVM.JournaDetailGrid.Clear();
                    loEx.Add("", "Data Not Found!");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }
        public async Task SearchWithFilterAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(_journalVM.Parameter.CSEARCH_TEXT))
                {
                    loEx.Add("", "Please input keyword to search!");
                    goto EndBlock;
                }
                else
                {
                    if (_journalVM.Parameter.CSEARCH_TEXT.Length < 3)
                    {
                        loEx.Add("", "Minimum search keyword is 3 characters!");
                        goto EndBlock;
                    }
                }

                await _gridJournal.R_RefreshGrid(null);
                if (_journalVM.JournalGrid.Count == 0)
                {
                    _journalVM.JournaDetailGrid.Clear();
                    loEx.Add("", "Data Not Found!");
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        #endregion

        #region JournalGrid
        private async Task JournalGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _journalVM.ShowAllJournals();
                eventArgs.ListEntityResult = _journalVM.JournalGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void JournalGrid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private string lcCommitLabel = "Commit";
        private async Task JournalGrid_Display(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (JournalDTO)eventArgs.Data;
                _journalVM.Journal = loData;
                await _gridJournalDet.R_RefreshGrid(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        #endregion

        #region Approve & Commit
        private async Task BtnJournal_Approve()
        {
            var loEx = new R_Exception();
            try
            {
                //get data
                var loCurrentData = (JournalDTO)_conJournal.R_GetCurrentData();
                await _journalProcessVM.GetJournal(new JournalDTO() { CJRN_ID = loCurrentData.CREC_ID });
                var loData = _journalProcessVM.Journal;

                //validate allo approve
                if (loData.LALLOW_APPROVE == false)
                {
                    loEx.Add("", "You don’t have right to approve this journal!");
                    goto EndBlock;
                }

                //validate start date
                if (int.Parse(loData.CSTART_DATE) < int.Parse(_journalVM.CSOFT_PERIOD_START_DATE.CSTART_DATE))
                {
                    loEx.Add("", "Cannot Approve Recurring Journal with Starting Date before Soft Close Period!");
                    goto EndBlock;
                }
                //var loValidate = await R_MessageBox.Show("", _localizer["Q03"], R_eMessageBoxButtonType.YesNo);
                var loValidate = await R_MessageBox.Show("", "Are you sure want to Approve this Recurring Journal?", R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.No)
                {
                    //if no cancel
                    goto EndBlock;
                }
                else //if yes continue process
                {
                    //convert data to param
                    var loParam = R_FrontUtility.ConvertObjectToObject<GLM00200UpdateStatusDTO>(loData);
                    loParam.CNEW_STATUS = "20";
                    loParam.LAUTO_COMMIT = _journalVM.GL_SYSTEM_PARAM.LCOMMIT_APVJRN;
                    loParam.LUNDO_COMMIT = false;

                    //update status
                    await _journalProcessVM.UpdateJournalStatusAsync(loParam);

                    //get new data
                    await _journalProcessVM.GetJournal(new JournalDTO() { CJRN_ID = loParam.CREC_ID });

                    //refresh grid
                    await _gridJournal.R_RefreshGrid(null);

                    //set data to grid
                    await _gridJournal.R_SetCurrentData(_journalProcessVM.Journal);

                    //display message
                    await R_MessageBox.Show("", "Recurring Journal Approved Successfully!", R_eMessageBoxButtonType.OK);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task BtnJournal_Commit()
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loResult;
            try
            {
                //get data
                var loCurrentData = (JournalDTO)_conJournal.R_GetCurrentData();
                await _journalProcessVM.GetJournal(new JournalDTO() { CJRN_ID = loCurrentData.CREC_ID });
                var loData = _journalProcessVM.Journal;

                //confirmation
                if (loData.CSTATUS == "80")
                {
                    if (await R_MessageBox.Show("", "Are you sure want to undo committed this journal? ", R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.No)
                    {
                        goto EndBlock;
                    }
                }
                else
                {
                    if (await R_MessageBox.Show("", "Are you sure want to commit this journal? ", R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.No)
                    {
                        goto EndBlock;
                    }
                }

                //convert data to param
                var loParam = R_FrontUtility.ConvertObjectToObject<GLM00200UpdateStatusDTO>(loData);

                //set new status
                loParam.CNEW_STATUS = loData.CSTATUS == "80" ? (_journalVM.GSM_TRANSACTION_CODE.LAPPROVAL_FLAG ? "10" : "00") : "80";
                loParam.LAUTO_COMMIT = false;
                loParam.LUNDO_COMMIT = loData.CSTATUS == "80";

                //update journal status
                await _journalProcessVM.UpdateJournalStatusAsync(loParam);

                //get new data
                await _journalProcessVM.GetJournal(new JournalDTO() { CJRN_ID = loParam.CREC_ID });

                //refresh grid
                await _gridJournal.R_RefreshGrid(null);

                //set to grid
                await _gridJournal.R_SetCurrentData(_journalProcessVM.Journal);

                //show message
                await R_MessageBox.Show("", _journalVM.Journal.CSTATUS == "80" ? "Recurring Journal Committed Successfully!" : "Recurring Journal Undo Committed Successfully!", R_eMessageBoxButtonType.OK);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region JournalDetailGrid
        private async Task JournalDetGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (JournalDTO)eventArgs.Parameter;
                await _journalVM.ShowAllJournalDetail(loData);
                eventArgs.ListEntityResult = _journalVM.JournaDetailGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region DepartmentLookup
        private void Dept_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = new GSL00700ParameterDTO();
                eventArgs.TargetPageType = typeof(GSL00700);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void Dept_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTempResult = R_FrontUtility.ConvertObjectToObject<GSL00700DTO>(eventArgs.Result);
                _journalVM.Parameter.CDEPT_CODE = loTempResult.CDEPT_CODE;
                _journalVM.Parameter.CDEPT_NAME = loTempResult.CDEPT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Download
        private async Task TemplateBtn_OnClick()
        {
            try
            {
                //var loValidate = await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifTemplate"), R_eMessageBoxButtonType.YesNo);
                var loValidate = await R_MessageBox.Show("", "Are you sure download this template?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await _journalVM.DownloadTemplate();

                    var saveFileName = "GL_RECURRING_JOURNAL_UPLOAD.xlsx";

                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.data);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


    }
}
