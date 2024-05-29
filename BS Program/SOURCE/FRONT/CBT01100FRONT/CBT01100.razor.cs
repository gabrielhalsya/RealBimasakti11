using BlazorClientHelper;
using CBT01100COMMON;
using CBT01100MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBT01100FRONT
{
    public partial class CBT01100 : R_Page
    {
        private CBT01100ViewModel _TransactionListViewModel = new();
        private CBT01110ViewModel _TransactionEntryViewModel = new();
        private R_Conductor _conductorRef;
        private R_Grid<CBT01100DTO> _gridRef;
        private R_Grid<CBT01101DTO> _gridDetailRef;

        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _TransactionListViewModel.GetAllUniversalData();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Search
        public async Task OnclickSearch()
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrEmpty(_TransactionListViewModel.JournalParam.CDEPT_CODE))
                {
                    loEx.Add(new Exception("Please input keyword to search!"));
                    goto EndBlock;
                }
                if (string.IsNullOrEmpty(_TransactionListViewModel.JournalParam.CSEARCH_TEXT))
                {
                    loEx.Add(new Exception("Please input keyword to search!"));
                    goto EndBlock;
                }
                if (!string.IsNullOrEmpty(_TransactionListViewModel.JournalParam.CSEARCH_TEXT)
                    && _TransactionListViewModel.JournalParam.CSEARCH_TEXT.Length < 3)
                {
                    loEx.Add(new Exception("Minimum search keyword is 3 characters!"));
                    goto EndBlock;
                }
                _TransactionEntryViewModel.JournalDetailGrid.Clear();
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        public async Task OnClickShowAll()
        {
            var loEx = new R_Exception();
            try
            {
                //reset detail
                _TransactionEntryViewModel.JournalDetailGrid.Clear();
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        #endregion

        #region JournalGrid
        private async Task JournalGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _TransactionListViewModel.GetJournalList();
                eventArgs.ListEntityResult = _TransactionListViewModel.JournalGrid;
                if (_TransactionListViewModel.JournalGrid.Count <= 0)
                {
                    loEx.Add("", "Data Not Found!");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task JournalGrid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                eventArgs.Result = (CBT01100DTO)eventArgs.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task JournalGrid_Display(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (CBT01100DTO)eventArgs.Data;
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    _TransactionListViewModel.CommitLabel = loData.CSTATUS == "80" ? _localizer["_UndoCommit"] : _localizer["_Commit"];
                    if (!string.IsNullOrWhiteSpace(loData.CREC_ID))
                    {
                        _TransactionEntryViewModel._CREC_ID = loData.CREC_ID;
                        await _gridDetailRef.R_RefreshGrid(null);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task ApproveJournalProcess()
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (CBT01100DTO)_conductorRef.R_GetCurrentData();
                if (!loData.LALLOW_APPROVE)
                {
                    loEx.Add("", "You don’t have right to approve this journal!");
                    goto EndBlock;
                }

                var loParam = R_FrontUtility.ConvertObjectToObject<CBT01100UpdateStatusDTO>(loData);
                loParam.LAUTO_COMMIT = _TransactionEntryViewModel.VAR_GL_SYSTEM_PARAM.LCOMMIT_APVJRN;
                loParam.LUNDO_COMMIT = false;
                loParam.CNEW_STATUS = "20";

                await _TransactionEntryViewModel.UpdateJournalStatus(loParam);
                await _gridRef.R_RefreshGrid(null);
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
            R_eMessageBoxResult loValidate;

            try
            {
                var loData = (CBT01100DTO)_conductorRef.R_GetCurrentData();

                if (loData.CSTATUS == "80")
                {
                    if (_TransactionEntryViewModel.VAR_IUNDO_COMMIT_JRN.IOPTION == 3)
                    {
                        loValidate = await R_MessageBox.Show("", "Are you sure want to undo committed this journal? ", R_eMessageBoxButtonType.YesNo);
                        if (loValidate == R_eMessageBoxResult.No)
                        { goto EndBlock; }
                        _TransactionListViewModel.CommitLabel = @_localizer["_Commit"];
                    }
                }
                else
                {
                    loValidate = await R_MessageBox.Show("", "Are you sure want to commit this journal? ", R_eMessageBoxButtonType.YesNo);
                    if (loValidate == R_eMessageBoxResult.No)
                        goto EndBlock;
                    _TransactionListViewModel.CommitLabel = @_localizer["_Commit"];
                }

                var loParam = R_FrontUtility.ConvertObjectToObject<CBT01100UpdateStatusDTO>(loData);
                loParam.LAUTO_COMMIT = _TransactionEntryViewModel.VAR_GL_SYSTEM_PARAM.LCOMMIT_APVJRN;
                loParam.LUNDO_COMMIT = loData.CSTATUS == "80" ? true : false;
                loParam.CNEW_STATUS = loData.CSTATUS == "80" ? "20" : "80";

                await _TransactionEntryViewModel.UpdateJournalStatus(loParam);
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region JournalGridDetail
        private async Task JournalGridDetail_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                // var loParam = R_FrontUtility.ConvertObjectToObject<CBT01101DTO>(eventArgs.Parameter);
                await _TransactionEntryViewModel.GetJournalDetailList();
                eventArgs.ListEntityResult = _TransactionEntryViewModel.JournalDetailGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region lookupDept
        private async void BeforeOpen_lookupDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var param = new GSL00700ParameterDTO
                {
                    CUSER_ID = clientHelper.UserId,
                    CCOMPANY_ID = clientHelper.CompanyId
                };
                eventArgs.Parameter = param;
                eventArgs.TargetPageType = typeof(GSL00700);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private void AfterOpen_lookupDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _TransactionListViewModel.JournalParam.CDEPT_CODE = loTempResult.CDEPT_CODE;
            _TransactionListViewModel.JournalParam.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }
        private async Task OnLostFocus_LookupDept()
        {
            var loEx = new R_Exception();

            try
            {
                LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel(); //use GSL's model
                var loParam = new GSL00700ParameterDTO // use match param as GSL's dto, send as type in search texbox
                {
                    CSEARCH_TEXT = _TransactionListViewModel.JournalParam.CDEPT_CODE, // property that bindded to search textbox
                };


                var loResult = await loLookupViewModel.GetDepartment(loParam); //retrive single record 

                //show result & show name/related another fields
                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    _TransactionListViewModel.JournalParam.CDEPT_NAME = ""; //kosongin bind textbox name kalo gaada
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                    _TransactionListViewModel.JournalParam.CDEPT_NAME = loResult.CDEPT_NAME; //assign bind textbox name kalo ada
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Predefine Journal Entry
        private void Predef_JournalEntry(R_InstantiateDockEventArgs eventArgs)
        {
            var loParam = (CBT01100DTO)_conductorRef.R_GetCurrentData();
            eventArgs.TargetPageType = typeof(CBT01110);
            eventArgs.Parameter = loParam;
        }
        private async Task AfterPredef_JournalEntry(R_AfterOpenPredefinedDockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion
    }
}

