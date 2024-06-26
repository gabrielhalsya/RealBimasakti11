﻿using BlazorClientHelper;
using GLT00100COMMON;
using GLT00100MODEL;
using GLTR00100COMMON;
using GLTR00100FRONT;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid.Columns;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System.Globalization;

namespace GLT00100FRONT
{
    public partial class GLT00110 : R_Page
    {
        private GLT00110ViewModel _JournalEntryViewModel = new();
        private R_Conductor _conductorRef;
        private R_ConductorGrid _conductorDetailRef;
        private R_Grid<GLT00101DTO> _gridDetailRef;

        #region Private Property
        private string _lcLabelSubmit = "Submit";
        private string _lcLabelCommit = "Commit";
        private bool _EnableEdit = false;
        private bool _EnableDelete = false;
        private bool _EnableSubmit = false;
        private bool _EnableApprove = false;
        private bool _EnableCommit = false;
        private bool _EnableHaveRecId = false;
        private bool _EnableSetOther = false;
        #endregion

        [Inject] IClientHelper _clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                //get universal data
                await _JournalEntryViewModel.GetAllUniversalData();

                //get param
                GLT01100FrontPredefinedParamDTO loParam = R_FrontUtility.ConvertObjectToObject<GLT01100FrontPredefinedParamDTO>(poParameter);

                //validate if page call from another app
                if (string.IsNullOrWhiteSpace(loParam.PARAM_CALLER_ID))
                {
                    //if some data selected in prev tab
                    if (!string.IsNullOrWhiteSpace(loParam.PARAM_FROM_INTERNAL_JOURNAL_LIST.CREC_ID))
                    {
                        var loData = new GLT00110DTO() { CREC_ID = loParam.PARAM_FROM_INTERNAL_JOURNAL_LIST.CREC_ID };
                        await _conductorRef.R_GetEntity(loData);
                    }
                    else
                    {
                        //set date
                        _JournalEntryViewModel.RefDate = _JournalEntryViewModel.VAR_TODAY.DTODAY;
                        _JournalEntryViewModel.DocDate = _JournalEntryViewModel.VAR_TODAY.DTODAY;
                    }
                }
                else
                {
                    //set param to viewmodelvariableclass
                    _JournalEntryViewModel.ExternalParam = loParam;
                    switch (loParam.PARAM_CALLER_ACTION)
                    {
                        case "VIEW":
                            await JournalForm_ExternalViewModeAsync(new GLT00110DTO() { CREC_ID = loParam.PARAM_FROM_INTERNAL_JOURNAL_LIST.CREC_ID });
                            break;
                        case "VIEW_ONLY":
                            await JournalForm_ExternalViewOnlyModeAsync(new GLT00110DTO() { CREC_ID = loParam.PARAM_FROM_INTERNAL_JOURNAL_LIST.CREC_ID });
                            break;
                        case "NEW":
                            await JournalForm_ExternalAddModeAsync();
                            break;
                        default:
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        Endblock:
            R_DisplayException(loEx);
        }

        #region Locking
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
        private const string DEFAULT_MODULE_NAME = "GL";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (GLT00110DTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "CBT01100",
                        Table_Name = "GLT_TRANS_HD",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "CBT01100",
                        Table_Name = "GLT_TRANS_HD",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
                    };

                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }
        #endregion

        private R_AddButton _addButton;
        private R_Button _btnCopy;
        private R_EditButton _editButton;
        private R_Button _deleteButton;
        private R_CancelButton _cancelButton;
        private R_SaveButton _saveButton;
        private R_Lookup _btnPrint;
        private R_Button _btnSubmit;
        private R_Button _btnApprove;
        private R_Button _btnCommit;


        #region External action mode
        private async Task JournalForm_ExternalAddModeAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _conductorRef.Add();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task JournalForm_ExternalViewModeAsync(GLT00110DTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _conductorRef.R_GetEntity(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task JournalForm_ExternalViewOnlyModeAsync(GLT00110DTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {

                await _conductorRef.R_GetEntity(poParam);
                _addButton.Enabled = false;
                _btnCopy.Enabled = false;
                _editButton.Enabled = false;
                _deleteButton.Enabled = false;
                _cancelButton.Enabled = false;
                _saveButton.Enabled = false;
                _btnPrint.Enabled = false;
                _btnSubmit.Enabled = false;
                _btnApprove.Enabled = false;
                _btnCommit.Enabled = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #endregion

        #region Form

        private async Task JournalForm_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _JournalEntryViewModel.GetJournal((GLT00110DTO)eventArgs.Data);
                eventArgs.Result = _JournalEntryViewModel.Journal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private R_TextBox _txt_CDEPT_CODE;
        private R_TextBox _txt_CDOC_NO;
        private R_Lookup _lookup_CDEPT_CODE;
        private R_DatePicker<DateTime?> _datePicker_DocDate;
        private R_ComboBox<GLT00100GSCurrencyDTO, string> _comboBox_Currency;
        private R_NumericTextBox<decimal> _numericTxt_NLBASE_RATE;
        private R_NumericTextBox<decimal> _numericTxt_NBBASE_RATE;
        private R_NumericTextBox<decimal> _numericTxt_NLCURRENCY_RATE;
        private R_NumericTextBox<decimal> _numericTxt_NBCURRENCY_RATE;
        private async Task JournalForm_AfterAddAsync(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                //get object form
                var loData = (GLT00110DTO)eventArgs.Data;

                //if add trough external program
                if (!string.IsNullOrWhiteSpace(_JournalEntryViewModel.ExternalParam.PARAM_CALLER_ID) && _JournalEntryViewModel.ExternalParam.PARAM_CALLER_ACTION == "NEW")
                {
                    //set journal form from callerparam
                    _txt_CDEPT_CODE.Enabled = false;
                    loData.CDEPT_CODE = _JournalEntryViewModel.ExternalParam.PARAM_DEPT_CODE;
                    loData.CDEPT_NAME = _JournalEntryViewModel.ExternalParam.PARAM_DEPT_NAME;
                    loData.CDOC_NO = _JournalEntryViewModel.ExternalParam.PARAM_DOC_NO;
                    _JournalEntryViewModel.RefDate = _JournalEntryViewModel.VAR_TODAY.DTODAY;

                    //handle if param_doc_date like yyyyMM,or yyyyMMdd
                    _JournalEntryViewModel.DocDate = string.IsNullOrWhiteSpace(_JournalEntryViewModel.ExternalParam.PARAM_DOC_DATE) ? null : (_JournalEntryViewModel.ExternalParam.PARAM_DOC_DATE.Length == 6 ? DateTime.ParseExact(_JournalEntryViewModel.ExternalParam.PARAM_DOC_DATE, "yyyyMM", CultureInfo.InvariantCulture) : DateTime.ParseExact(_JournalEntryViewModel.ExternalParam.PARAM_DOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture));

                    loData.CCURRENCY_CODE = _JournalEntryViewModel.ExternalParam.PARAM_CURRENCY_CODE;
                    loData.NLBASE_RATE = _JournalEntryViewModel.ExternalParam.PARAM_LC_BASE_RATE;
                    loData.NLCURRENCY_RATE = _JournalEntryViewModel.ExternalParam.PARAM_LC_RATE;
                    loData.NBBASE_RATE = _JournalEntryViewModel.ExternalParam.PARAM_BC_BASE_RATE;
                    loData.NBCURRENCY_RATE = _JournalEntryViewModel.ExternalParam.PARAM_BC_RATE;
                    loData.CTRANS_DESC = _JournalEntryViewModel.ExternalParam.PARAM_DESCRIPTION;
                    loData.CCREATE_BY = _clientHelper.UserId;
                    loData.CUPDATE_BY = _clientHelper.UserId;
                    loData.CCREATE_DATE = _JournalEntryViewModel.VAR_TODAY.ToString();
                    loData.CUPDATE_DATE = _JournalEntryViewModel.VAR_TODAY.ToString();

                    //if call from DEPOSIT MANAGER (PMT05000) then add 1 record detail data
                    if (_JournalEntryViewModel.ExternalParam.PARAM_CALLER_ID == "PMT05500")
                    {
                        //disable form
                        _txt_CDOC_NO.Enabled = false;
                        _lookup_CDEPT_CODE.Enabled = false;
                        _datePicker_DocDate.Enabled = false;
                        _comboBox_Currency.Enabled = false;
                        _numericTxt_NLBASE_RATE.Enabled = false;
                        _numericTxt_NLCURRENCY_RATE.Enabled = false;
                        _numericTxt_NBBASE_RATE.Enabled = false;
                        _numericTxt_NBCURRENCY_RATE.Enabled = false;

                        //add new record data
                        GLT00101DTO loDetailRecordData = new();
                        loDetailRecordData.CINPUT_TYPE = 'A';
                        loDetailRecordData.INO = 1;
                        loDetailRecordData.CBSIS = _JournalEntryViewModel.ExternalParam.PARAM_BSIS;
                        loDetailRecordData.CGLACCOUNT_NO = _JournalEntryViewModel.ExternalParam.PARAM_GLACCOUNT_NO;
                        loDetailRecordData.CGLACCOUNT_NAME = _JournalEntryViewModel.ExternalParam.PARAM_GLACCOUNT_NAME;
                        loDetailRecordData.CCENTER_CODE = _JournalEntryViewModel.ExternalParam.PARAM_CENTER_CODE;
                        loDetailRecordData.CCENTER_NAME = _JournalEntryViewModel.ExternalParam.PARAM_CENTER_NAME;
                        loDetailRecordData.CDBCR = _JournalEntryViewModel.ExternalParam.PARAM_DBCR.ToString();
                        loDetailRecordData.NDEBIT = _JournalEntryViewModel.ExternalParam.PARAM_DBCR == 'D' ? _JournalEntryViewModel.ExternalParam.PARAM_AMOUNT : 0;
                        loDetailRecordData.NCREDIT = _JournalEntryViewModel.ExternalParam.PARAM_DBCR == 'C' ? _JournalEntryViewModel.ExternalParam.PARAM_AMOUNT : 0;
                        loDetailRecordData.CDETAIL_DESC = _JournalEntryViewModel.ExternalParam.PARAM_DESCRIPTION;
                        loDetailRecordData.CDOCUMENT_NO = _JournalEntryViewModel.ExternalParam.PARAM_DOC_NO;
                        loDetailRecordData.CDOCUMENT_DATE = _JournalEntryViewModel.ExternalParam.PARAM_DOC_DATE;
                        loDetailRecordData.DDOCUMENT_DATE = DateTime.ParseExact(_JournalEntryViewModel.ExternalParam.PARAM_DOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);

                        //assign to grid binding
                        _JournalEntryViewModel.JournalDetailGrid.Add(loDetailRecordData);
                    }
                }
                else
                {
                    //===Normal Afteradd
                    //store & clear grid detail to temp
                    _JournalEntryViewModel.JournalDetailGridTemp = new(_gridDetailRef.DataSource.ToList()); ;//store detail to temp
                    _gridDetailRef.DataSource.Clear();

                    //set default data
                    loData.CCREATE_BY = _clientHelper.UserId;
                    loData.CUPDATE_BY = _clientHelper.UserId;
                    loData.CCREATE_DATE = _JournalEntryViewModel.VAR_TODAY.ToString();
                    loData.CUPDATE_DATE = _JournalEntryViewModel.VAR_TODAY.ToString();
                    _JournalEntryViewModel.RefDate = _JournalEntryViewModel.VAR_TODAY.DTODAY;
                    _JournalEntryViewModel.DocDate = _JournalEntryViewModel.VAR_TODAY.DTODAY;
                    loData.CCURRENCY_CODE = _JournalEntryViewModel.VAR_GSM_COMPANY.CLOCAL_CURRENCY_CODE;
                    loData.CBASE_CURRENCY_CODE = _JournalEntryViewModel.VAR_GSM_COMPANY.CBASE_CURRENCY_CODE;
                    loData.CDEPT_CODE = _JournalEntryViewModel.VAR_GL_SYSTEM_PARAM.CCLOSE_DEPT_CODE;
                    loData.CDEPT_NAME = _JournalEntryViewModel.VAR_GL_SYSTEM_PARAM.CCLOSE_DEPT_NAME;

                    //convert data as param
                    var loParam = R_FrontUtility.ConvertObjectToObject<GLT00110LastCurrencyRateDTO>(loData);

                    //call last currency
                    var loResult = await _JournalEntryViewModel.GetLastCurrency(loParam);

                    //set currencies
                    if (loResult is null)
                    {
                        loData.NLBASE_RATE = 1;
                        loData.NLCURRENCY_RATE = 1;
                        loData.NBBASE_RATE = 1;
                        loData.NBCURRENCY_RATE = 1;
                    }
                    else
                    {
                        loData.NLBASE_RATE = loResult.NLBASE_RATE_AMOUNT;
                        loData.NLCURRENCY_RATE = loResult.NLCURRENCY_RATE_AMOUNT;
                        loData.NBBASE_RATE = loResult.NBBASE_RATE_AMOUNT;
                        loData.NBCURRENCY_RATE = loResult.NBCURRENCY_RATE_AMOUNT;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task JournalForm_RDisplay(R_DisplayEventArgs eventArgs)
        {
            _EnableSetOther = eventArgs.ConductorMode != R_eConductorMode.Normal;
            var loData = (GLT00110DTO)eventArgs.Data;
            if (!string.IsNullOrWhiteSpace(loData.CSTATUS))
            {
                _lcLabelCommit = loData.CSTATUS == "80" ? "Undo Commit" : "Commit";
                _lcLabelSubmit = loData.CSTATUS == "10" ? "Undo Submit" : "Submit";
                _EnableEdit = loData.CSTATUS == "00";
                _EnableDelete = loData.CSTATUS == "00";
                _EnableSubmit = loData.CSTATUS == "00" || loData.CSTATUS == "10";
                _EnableApprove = loData.CSTATUS == "10" && _JournalEntryViewModel.VAR_GSM_TRANSACTION_CODE.LAPPROVAL_FLAG;
                _EnableCommit =
                   ((loData.CSTATUS == "20")
                    || (loData.CSTATUS == "80" && _JournalEntryViewModel.VAR_IUNDO_COMMIT_JRN.IOPTION != 1))
                    && int.Parse(loData.CREF_PRD) >= int.Parse(_JournalEntryViewModel.VAR_GL_SYSTEM_PARAM.CSOFT_PERIOD);
                _EnableHaveRecId = !string.IsNullOrWhiteSpace(loData.CREC_ID);

            }

            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                //refresh grid if got crecid
                if (!string.IsNullOrWhiteSpace(loData.CREC_ID))
                {
                    _JournalEntryViewModel.RefDate = JournalForm_ParseDateHelper(loData.CREF_DATE) ?? _JournalEntryViewModel.VAR_TODAY.DTODAY;
                    _JournalEntryViewModel.DocDate = JournalForm_ParseDateHelper(loData.CDOC_DATE);
                    await _gridDetailRef.R_RefreshGrid(loData);
                }

                //set update date & create date
                loData.CUPDATE_DATE = loData.DUPDATE_DATE.HasValue ? loData.DUPDATE_DATE.ToString() : "";
                loData.CCREATE_DATE = loData.DCREATE_DATE.HasValue ? loData.DCREATE_DATE.ToString() : "";

            }
        }

        DateTime? JournalForm_ParseDateHelper(string pcDateStr)
        {
            var loEx = new R_Exception();
            try
            {
                if (pcDateStr != null && DateTime.TryParseExact(pcDateStr, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ldParsedDate))
                    return ldParsedDate;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return null;
        }

        private void JournalForm_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GLT00110DTO)eventArgs.Data;
                if (string.IsNullOrWhiteSpace(loParam.CDEPT_CODE))
                {
                    loEx.Add("", "Please select Department");
                }

                if (string.IsNullOrWhiteSpace(loParam.CREF_NO) && !_JournalEntryViewModel.VAR_GSM_TRANSACTION_CODE.LINCREMENT_FLAG)
                {
                    loEx.Add("", "Reference No. is required!");
                }

                if (string.IsNullOrWhiteSpace(loParam.CDOC_NO))
                {
                    loEx.Add("", "Document No. is required");
                }

                //if (loParam.DDOC_DATE == null)
                if (_JournalEntryViewModel.DocDate == null)
                {
                    loEx.Add("", "Document Date is required");
                }

                if (_JournalEntryViewModel.RefDate < DateTime.ParseExact(_JournalEntryViewModel.VAR_CCURRENT_PERIOD_START_DATE.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture))
                {
                    loEx.Add("", "Reference Date cannot be before Current Period!");
                }

                if (_JournalEntryViewModel.RefDate > _JournalEntryViewModel.VAR_TODAY.DTODAY)
                {
                    loEx.Add("", "Reference Date cannot be after today!");
                }

                if (_JournalEntryViewModel.DocDate < DateTime.ParseExact(_JournalEntryViewModel.VAR_CCURRENT_PERIOD_START_DATE.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture))
                {
                    loEx.Add("", "Document Date cannot be before Current Period!");
                }

                if (string.IsNullOrWhiteSpace(loParam.CDOC_NO))
                {
                    loEx.Add("", "Please input Document No.!");
                }

                //if (loParam.DDOC_DATE > _JournalEntryViewModel.VAR_TODAY.DTODAY)
                if (_JournalEntryViewModel.DocDate > _JournalEntryViewModel.VAR_TODAY.DTODAY)
                {
                    loEx.Add("", "Document Date cannot be after today!");
                }

                if (_JournalEntryViewModel.DocDate < DateTime.ParseExact(_JournalEntryViewModel.VAR_CCURRENT_PERIOD_START_DATE.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture))
                //if (loParam.DDOC_DATE < DateTime.ParseExact(_JournalEntryViewModel.VAR_CCURRENT_PERIOD_START_DATE.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture))
                {
                    loEx.Add("", "Document Date cannot be before Current Period!");
                }

                if (string.IsNullOrEmpty(loParam.CTRANS_DESC))
                {
                    loEx.Add("", "Description is required!");
                }

                if (_gridDetailRef.DataSource.Count == 0)
                {
                    loEx.Add("", "Please input Journal Detail!");
                }

                if ((loParam.NDEBIT_AMOUNT > 0 || loParam.NCREDIT_AMOUNT > 0) && loParam.NDEBIT_AMOUNT != loParam.NCREDIT_AMOUNT)
                {
                    loEx.Add("", "Total Debit Amount must be equal to Total Credit Amount");
                }

                if (loParam.NDEBIT_AMOUNT == 0 || loParam.NCREDIT_AMOUNT == 0)
                {
                    loEx.Add("", "Total Debit Amount or Total Credit Amount cannot be 0!");
                }

                if (loParam.NPRELIST_AMOUNT > 0 && loParam.NPRELIST_AMOUNT != loParam.NDEBIT_AMOUNT)
                {
                    loEx.Add("", "Journal amount is not equal to Prelist!");
                }

                if (loParam.NLBASE_RATE <= 0)
                {
                    loEx.Add("", "Local Currency Base Rate must be greater than 0!");
                }

                if (loParam.NLCURRENCY_RATE <= 0)
                {
                    loEx.Add("", "Local Currency Rate must be greater than 0!");
                }

                if (loParam.NBBASE_RATE <= 0)
                {
                    loEx.Add("", "Base Currency Base Rate must be greater than 0!");
                }

                if (loParam.NBCURRENCY_RATE <= 0)
                {
                    loEx.Add("", "Base Currency Rate must be greater than 0!");
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task JournalForm_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loHeaderData = R_FrontUtility.ConvertObjectToObject<GLT00110DTO>(eventArgs.Data);
                var loParam = new GLT00110HeaderDetailDTO { HeaderData = loHeaderData };
                var loDetailData = _gridDetailRef.DataSource;
                var loMappingDetail = loDetailData.Select(item => new GLT00111DTO
                {
                    CGLACCOUNT_NO = item.CGLACCOUNT_NO,
                    CCENTER_CODE = item.CCENTER_CODE,
                    CDBCR = item.CDBCR.FirstOrDefault(),
                    NAMOUNT = item.NDEBIT + item.NCREDIT,
                    CDETAIL_DESC = item.CDETAIL_DESC,
                    CDOCUMENT_NO = item.CDOCUMENT_NO,
                    CDOCUMENT_DATE = item.CDOCUMENT_DATE,
                    CINPUT_TYPE = item.CINPUT_TYPE
                }).ToList();
                loParam.DetailData = loMappingDetail;
                await _JournalEntryViewModel.SaveJournal(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _JournalEntryViewModel.Journal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task JournalForm_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var res = await R_MessageBox.Show("", "You haven’t saved your changes. Are you sure want to cancel? [Yes/No]",
                    R_eMessageBoxButtonType.YesNo);
                if (res == R_eMessageBoxResult.No)
                {
                    eventArgs.Cancel = true;
                }
                else
                {
                    _JournalEntryViewModel.JournalDetailGrid = _JournalEntryViewModel.JournalDetailGridTemp; //assign detail back from temp
                    _JournalEntryViewModel.RefDate = string.IsNullOrWhiteSpace(_JournalEntryViewModel.Data.CREF_DATE) ? _JournalEntryViewModel.VAR_TODAY.DTODAY : DateTime.ParseExact(_JournalEntryViewModel.Data.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                    _JournalEntryViewModel.DocDate = string.IsNullOrWhiteSpace(_JournalEntryViewModel.Data.CDOC_DATE) ? null : DateTime.ParseExact(_JournalEntryViewModel.Data.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                    if (eventArgs.ConductorMode == R_eConductorMode.Add)
                    {
                        _gridDetailRef.DataSource.Clear();
                    }
                    await Close(false, false);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }

        private async Task CopyJournalEntryProcessAsync()
        {
            var loEx = new R_Exception();
            try
            {
                var loData = _JournalEntryViewModel.Journal; ;
                DateTime? ParseDate(string dateStr)
                {
                    if (dateStr != null && DateTime.TryParseExact(dateStr, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                        return parsedDate;
                    return null;
                }

                await _conductorRef.Add();
                if (_conductorRef.R_ConductorMode == R_eConductorMode.Add)
                {
                    var loHeaderData = (GLT00110DTO)_conductorRef.R_GetCurrentData();
                    loHeaderData.CDEPT_CODE = loData.CDEPT_CODE;
                    loHeaderData.CDEPT_NAME = loData.CDEPT_NAME;
                    loHeaderData.CDOC_NO = loData.CDOC_NO;
                    loHeaderData.CREF_DATE = loData.CREF_DATE;
                    loHeaderData.CDOC_DATE = loData.CDOC_DATE;
                    loHeaderData.CTRANS_DESC = loData.CTRANS_DESC;
                    loHeaderData.CSTATUS = loData.CSTATUS;
                    loHeaderData.CCURRENCY_CODE = loData.CCURRENCY_CODE;
                    loHeaderData.NPRELIST_AMOUNT = loData.NPRELIST_AMOUNT;
                    loHeaderData.NDEBIT_AMOUNT = loData.NDEBIT_AMOUNT;
                    loHeaderData.NCREDIT_AMOUNT = loData.NCREDIT_AMOUNT;
                    _JournalEntryViewModel.JournalDetailGrid = _JournalEntryViewModel.JournalDetailGridTemp;
                    _JournalEntryViewModel.RefDate = ParseDate(loHeaderData.CREF_DATE) ?? DateTime.MinValue;
                    _JournalEntryViewModel.DocDate = ParseDate(loHeaderData.CDOC_DATE) ?? DateTime.MinValue;

                    await _txt_CDEPT_CODE.FocusAsync();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }

        private async Task BtnDelete_OnClick()
        {
            var loEx = new R_Exception();
            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure want to delete this journal?", R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.No)
                    goto EndBlock;

                var loData = (GLT00110DTO)_conductorRef.R_GetCurrentData();
                await _JournalEntryViewModel.DeleteJournal(loData);
                await _conductorRef.R_GetEntity(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Onchange Value

        private async Task RefreshLastCurrency()
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (GLT00110DTO)_conductorRef.R_GetCurrentData();
                var loParam = R_FrontUtility.ConvertObjectToObject<GLT00110LastCurrencyRateDTO>(loData);
                var loResult = await _JournalEntryViewModel.GetLastCurrency(loParam);

                if (loResult is null)
                {
                    loData.NLBASE_RATE = 1;
                    loData.NLCURRENCY_RATE = 1;
                    loData.NBBASE_RATE = 1;
                    loData.NBCURRENCY_RATE = 1;
                }
                else
                {
                    loData.NLBASE_RATE = loResult.NLBASE_RATE_AMOUNT;
                    loData.NLCURRENCY_RATE = loResult.NLCURRENCY_RATE_AMOUNT;
                    loData.NBBASE_RATE = loResult.NBBASE_RATE_AMOUNT;
                    loData.NBCURRENCY_RATE = loResult.NBCURRENCY_RATE_AMOUNT;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RefDate_OnChange(DateTime poParam)
        {
            var loEx = new R_Exception();
            try
            {
                _JournalEntryViewModel.RefDate = poParam;
                if (_JournalEntryViewModel.Data.CCURRENCY_CODE != _JournalEntryViewModel.VAR_GSM_COMPANY.CLOCAL_CURRENCY_CODE
                    || _JournalEntryViewModel.Data.CCURRENCY_CODE != _JournalEntryViewModel.VAR_GSM_COMPANY.CBASE_CURRENCY_CODE)
                {
                    await RefreshLastCurrency();
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Currency_OnChange(string poParam)
        {
            var loEx = new R_Exception();
            try
            {
                _JournalEntryViewModel.Data.CCURRENCY_CODE = poParam;
                await RefreshLastCurrency();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Detail

        private async Task JournalDet_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.Parameter != null)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<GLT00101DTO>(eventArgs.Parameter);
                    await _JournalEntryViewModel.GetJournalDetailList(loParam);
                }
                eventArgs.ListEntityResult = _JournalEntryViewModel.JournalDetailGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool _EnableCenterList = true;

        private void JournalDet_RDisplay(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var data = (GLT00101DTO)eventArgs.Data;
                var loHeaderData = (GLT00110DTO)_conductorRef.R_GetCurrentData();
                if (data != null)
                {
                    if (data.NDEBIT > 0 && data.NCREDIT == 0)
                    {
                        data.CDBCR = "D";
                    }
                    else if (data.NCREDIT > 0 && data.NDEBIT == 0)
                    {
                        data.CDBCR = "C";
                    }
                    else
                    {
                        data.CDBCR = "";
                    }
                    data.NAMOUNT = data.NCREDIT + data.NDEBIT;
                }

                //if (data != null && _conductorRef.R_ConductorMode != R_eConductorMode.Normal)
                //{
                //    _EnableCenterList = (data.CBSIS == "B" && _JournalEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_BS == true) || (data.CBSIS == "I" && _JournalEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_IS == true);
                //}

                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (_gridDetailRef.DataSource.Count > 0)
                    {
                        loHeaderData.NDEBIT_AMOUNT = _gridDetailRef.DataSource.Sum(x => x.NDEBIT);
                        loHeaderData.NCREDIT_AMOUNT = _gridDetailRef.DataSource.Sum(x => x.NCREDIT);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void JournalDet_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }

        private void JournalDet_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (GLT00101DTO)eventArgs.Data;
                if (string.IsNullOrWhiteSpace(loData.CGLACCOUNT_NO))
                {
                    loEx.Add("", "Account No. is required!");
                }

                if (string.IsNullOrWhiteSpace(loData.CCENTER_CODE) && (loData.CBSIS == "B" && _JournalEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_BS == true) || (loData.CBSIS == "I" && _JournalEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_BS == true))
                {
                    loEx.Add("", $"Center Code is required for Account No. {loData.CGLACCOUNT_NO}!");
                }

                if (loData.NDEBIT == 0 && loData.NCREDIT == 0)
                {
                    loEx.Add("", "Journal amount cannot be 0!");
                }

                if (loData.NDEBIT > 0 && loData.NCREDIT > 0)
                {
                    loEx.Add("", "Journal amount can only be either Debit or Credit!");
                }
                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    if (_JournalEntryViewModel.JournalDetailGrid.Any(item => item.CGLACCOUNT_NO == loData.CGLACCOUNT_NO))
                    {
                        loEx.Add("", $"Account No. {loData.CGLACCOUNT_NO} already exists!");
                    }
                }


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        R_GridLookupColumn _gridLookup_GLACCOUNT;
        R_GridComboBoxColumn _gridCombobox_CENTER;
        R_GridTextColumn _gridText_CDETAIL_DESC;
        R_GridTextColumn _gridText_CDOCUMENT_NO;
        R_GridDatePickerColumn _gridDatePicker_DDOCUMENT_DATE;
        R_GridNumericColumn _gridNumeric_NDEBIT;
        R_GridNumericColumn _gridNumeric_NCREDIT;

        private void JournalDet_SetAdd(R_SetEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (_JournalEntryViewModel.ExternalParam.PARAM_CALLER_ID != null || _JournalEntryViewModel.ExternalParam.PARAM_CALLER_ID != "")
                {
                    if (!char.IsWhiteSpace(_JournalEntryViewModel.ExternalParam.PARAM_DBCR) || _JournalEntryViewModel.ExternalParam.PARAM_DBCR != 'D')
                    {
                        _gridNumeric_NDEBIT.Enabled = false;

                    }
                    if (!char.IsWhiteSpace(_JournalEntryViewModel.ExternalParam.PARAM_DBCR) || _JournalEntryViewModel.ExternalParam.PARAM_DBCR != 'C')
                    {
                        _gridNumeric_NCREDIT.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void JournalDet_SetEdit(R_SetEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (GLT00101DTO)_conductorDetailRef.R_GetCurrentData();
                if (loData.CINPUT_TYPE == 'A')
                {
                    _gridLookup_GLACCOUNT.Enabled = false;
                    _gridCombobox_CENTER.Enabled = false;
                    _gridText_CDETAIL_DESC.Enabled = false;
                    _gridText_CDOCUMENT_NO.Enabled = false;
                    _gridDatePicker_DDOCUMENT_DATE.Enabled = false;
                }
                if (char.IsWhiteSpace(_JournalEntryViewModel.ExternalParam.PARAM_DBCR) || _JournalEntryViewModel.ExternalParam.PARAM_DBCR == 'D')
                {
                    _gridNumeric_NCREDIT.Enabled = false;
                    _gridNumeric_NDEBIT.Enabled = false;
                }


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void JournalDet_BeforeAdd(R_BeforeAddEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(_JournalEntryViewModel.Data.CTRANS_DESC) || string.IsNullOrWhiteSpace(_JournalEntryViewModel.Data.CDOC_NO))
                {
                    loEx.Add("", "Journal Description is requred");
                }

                if (string.IsNullOrWhiteSpace(_JournalEntryViewModel.Data.CDOC_NO))
                {
                    loEx.Add("", "Please input Document No. first before input Journal Detail!");
                }

                if (_JournalEntryViewModel.DocDate == null)
                {
                    loEx.Add("", "Please input Document Date first before input Journal Detail!");
                }

                if (_JournalEntryViewModel.DocDate.Value < DateTime.ParseExact(_JournalEntryViewModel.VAR_CCURRENT_PERIOD_START_DATE.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture))
                {
                    loEx.Add("", "Document Date cannot be before Current Period!");
                }

                if (string.IsNullOrWhiteSpace(_JournalEntryViewModel.Data.CTRANS_DESC))
                {
                    loEx.Add("", "Please input Document No. first before input Journal Detail!");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void JournalDet_BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loGridData = (GLT00101DTO)eventArgs.Data;
                if (loGridData != null)
                {
                    if (loGridData.CINPUT_TYPE == 'A')
                    {
                        loEx.Add("", "Selected Journal cannot be deleted!");
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void JournalDet_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (GLT00101DTO)eventArgs.Data;

                loData.INO = _JournalEntryViewModel.JournalDetailGrid.Count + 1;
                loData.CDETAIL_DESC = _JournalEntryViewModel.Data.CTRANS_DESC;
                loData.CDOCUMENT_NO = string.IsNullOrWhiteSpace(_JournalEntryViewModel.Data.CDOC_NO) ? "" : _JournalEntryViewModel.Data.CDOC_NO;
                loData.CDOCUMENT_DATE = _JournalEntryViewModel.DocDate == null ? "" : _JournalEntryViewModel.DocDate.Value.ToString("yyyyMMdd");
                loData.DDOCUMENT_DATE = _JournalEntryViewModel.DocDate.Value;
                loData.CCENTER_CODE = "";
                loData.CCENTER_NAME = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void JournalDet_Before_Open_Lookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            string lcNewProgramCode = int.Parse(_JournalEntryViewModel.RefDate.ToString("yyyyMMdd")) < int.Parse(_JournalEntryViewModel.VAR_GL_SYSTEM_PARAM.CSTART_PERIOD + "01") ? "GLM00101" : "GLM00100";
            var param = new GSL00500ParameterDTO
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CPROGRAM_CODE = lcNewProgramCode,
                CUSER_ID = _clientHelper.UserId,
                CUSER_LANGUAGE = _clientHelper.CultureUI.TwoLetterISOLanguageName,
                CBSIS = "",
                CDBCR = "",
                CCENTER_CODE = "",
                CPROPERTY_ID = "",
                LCENTER_RESTR = false,
                LUSER_RESTR = false
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00500);
        }

        private void JournalDet_After_Open_Lookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loTempResult = (GSL00500DTO)eventArgs.Result;
            if (loTempResult == null)
                return;
            var loDetailData = (GLT00101DTO)eventArgs.ColumnData;
            loDetailData.CGLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
            loDetailData.CGLACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
            loDetailData.CBSIS = loTempResult.CBSIS.Trim();
            if ((loTempResult.CBSIS == "I" && !_JournalEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_IS)
                || (loTempResult.CBSIS == "B" && !_JournalEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_BS))
            {
                loDetailData.CCENTER_CODE = "";
                loDetailData.CCENTER_NAME = "";
                _EnableCenterList = false;
            }

        }

        private async Task JournalDet_AfterDelete()
        {
            var loEx = new R_Exception();
            try
            {
                await _gridDetailRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void JournalDet_Saving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (GLT00101DTO)eventArgs.Data;
                if (loData.NDEBIT > 0 && loData.NCREDIT == 0)
                {
                    loData.CDBCR = "D";
                }
                else if (loData.NCREDIT > 0 && loData.NDEBIT == 0)
                {
                    loData.CDBCR = "C";
                }
                else
                {
                    loData.CDBCR = "";
                }
                loData.NAMOUNT = loData.NDEBIT + loData.NCREDIT;

                if (!_EnableCenterList)
                {
                    loData.CCENTER_CODE = "";
                }
                loData.CINPUT_TYPE = 'M';

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void JournalDet_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (GLT00101DTO)eventArgs.Data;
                loData.CDETAIL_DESC = _JournalEntryViewModel.Data.CTRANS_DESC;
                loData.CDOCUMENT_NO = string.IsNullOrWhiteSpace(_JournalEntryViewModel.Data.CDOC_NO) ? "" : _JournalEntryViewModel.Data.CDOC_NO;
                loData.CDOCUMENT_DATE = string.IsNullOrWhiteSpace(_JournalEntryViewModel.Data.CDOC_DATE) ? "" : _JournalEntryViewModel.DocDate.Value.ToString("yyyyMMdd");
                loData.DDOCUMENT_DATE = _JournalEntryViewModel.DocDate.Value;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Process
        private async Task SubmitJournalProcess()
        {
            R_Exception loEx = new R_Exception();
            R_eMessageBoxResult loResult;
            try
            {
                //get data
                var loData = (GLT00110DTO)_conductorRef.R_GetCurrentData();

                //validate 
                if (loData.CSTATUS == "00" && int.Parse(loData.CREF_PRD) < int.Parse(_JournalEntryViewModel.VAR_GL_SYSTEM_PARAM.CSOFT_PERIOD))
                {
                    loEx.Add("", "Cannot Submit Journal with date before Soft Close Period!");
                    goto EndBlock;
                }

                //confirmation
                string lcMessage = loData.CSTATUS == "10" ? "Undo submit" : "Submit";
                string lcConfirmationMessage = $"Are you sure want to {lcMessage} this journal? [Yes/No]";

                loResult = await R_MessageBox.Show("", lcConfirmationMessage, R_eMessageBoxButtonType.YesNo);
                if (loResult == R_eMessageBoxResult.No)
                {
                    goto EndBlock;
                }

                //convert data to param
                var loParam = R_FrontUtility.ConvertObjectToObject<GLT00100UpdateStatusDTO>(loData);
                loParam.LUNDO_COMMIT = false;
                loParam.LAUTO_COMMIT = false;
                loParam.CNEW_STATUS = loData.CSTATUS == "00" ? "10" : "00";

                //update status
                await _JournalEntryViewModel.UpdateJournalStatus(loParam);

                //get new record data
                await _conductorRef.R_GetEntity(new GLT00110DTO() { CREC_ID = loParam.CREC_ID });
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task ApproveJournalProcess()
        {
            var loEx = new R_Exception();
            try
            {
                //get data
                var loData = (GLT00110DTO)_conductorRef.R_GetCurrentData();

                //validate allow approval
                if (loData.LALLOW_APPROVE == false)
                {
                    loEx.Add("", "You don’t have right to approve this journal!");
                    goto EndBlock;
                }

                //apprval confirmation
                if (await R_MessageBox.Show("", "Are you sure want to approve this journal? [Yes/No] ",
                        R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.No)
                {
                    goto EndBlock;
                }

                //convert data to param
                var loParam = R_FrontUtility.ConvertObjectToObject<GLT00100UpdateStatusDTO>(loData);
                loParam.CNEW_STATUS = "20";
                loParam.LAUTO_COMMIT = _JournalEntryViewModel.VAR_GL_SYSTEM_PARAM.LCOMMIT_APVJRN;
                loParam.LUNDO_COMMIT = false;

                //update status
                await _JournalEntryViewModel.UpdateJournalStatus(loParam);

                //gettin new entity
                await _conductorRef.R_GetEntity(new GLT00110DTO() { CREC_ID = loData.CREC_ID });

                //display message
                if (_JournalEntryViewModel.Journal.CSTATUS == "20")
                    await R_MessageBox.Show("", "Journal Approved Successfully!", R_eMessageBoxButtonType.OK);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task CommitJournalProcess()
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loResult;
            try
            {
                //get data
                var loData = (GLT00110DTO)_conductorRef.R_GetCurrentData();

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
                var loParam = R_FrontUtility.ConvertObjectToObject<GLT00100UpdateStatusDTO>(loData);

                //set new status
                loParam.CNEW_STATUS = loData.CSTATUS == "80" ? (_JournalEntryViewModel.VAR_GSM_TRANSACTION_CODE.LAPPROVAL_FLAG ? "10" : "00") : "80";
                loParam.LAUTO_COMMIT = false;
                loParam.LUNDO_COMMIT = loData.CSTATUS == "80";

                //update journal status
                await _JournalEntryViewModel.UpdateJournalStatus(loParam);

                //get new data
                await _conductorRef.R_GetEntity(new GLT00110DTO() { CREC_ID = loData.CREC_ID });

                //show message
                await R_MessageBox.Show("", _JournalEntryViewModel.Data.CSTATUS == "80" ? "Journal Committed Successfully!" : "Journal Undo Committed Successfully!", R_eMessageBoxButtonType.OK);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }


        #endregion

        #region Print

        private void Before_Open_lookupPrint(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loData = (GLT00110DTO)_conductorRef.R_GetCurrentData();
            var param = new GLTR00100DTO()
            {
                CREC_ID = loData.CREC_ID
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GLTR00100);
        }

        #endregion

        protected override Task<object> R_Set_Result_PredefinedDock()
        {
            var lcResult = _conductorRef.R_GetCurrentData();

            return Task.FromResult<object>(lcResult);
        }

        #region lookupDept
        private void Before_Open_lookupDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00700ParameterDTO
            {
                CUSER_ID = _clientHelper.UserId,
                CCOMPANY_ID = _clientHelper.CompanyId
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }

        private void After_Open_lookupDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loData = (GLT00110DTO)_conductorRef.R_GetCurrentData();
            loData.CDEPT_CODE = loTempResult.CDEPT_CODE;
            loData.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }

        private async Task OnLostFocus_LookupDept()
        {
            var loEx = new R_Exception();

            try
            {
                if (_JournalEntryViewModel.Data.CDEPT_CODE.Length > 0)
                {

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel(); //use GSL's model
                    var loParam = new GSL00700ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CSEARCH_TEXT = _JournalEntryViewModel.Data.CDEPT_CODE, // property that bindded to search textbox
                    };


                    var loResult = await loLookupViewModel.GetDepartment(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _JournalEntryViewModel.Data.CDEPT_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _JournalEntryViewModel.Data.CDEPT_CODE = loResult.CDEPT_CODE;
                    _JournalEntryViewModel.Data.CDEPT_NAME = loResult.CDEPT_NAME; //assign bind textbox name kalo ada
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
    }
}
