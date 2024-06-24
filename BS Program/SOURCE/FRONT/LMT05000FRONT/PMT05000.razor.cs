using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel;
using Lookup_PMModel.ViewModel.LML00200;
using Microsoft.AspNetCore.Components;
using PMT05000COMMON.DTO_s;
using PMT05000FrontResources;
using PMT05000MODEL.ViewModel_s;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT05000FRONT
{
    public partial class PMT05000 : R_Page
    {
        private PMT05000ViewModel _agreementChrgDiscViewModel = new PMT05000ViewModel();

        private R_Conductor _conAgrChrgDisc;

        private R_Grid<AgreementChrgDiscDetailDTO> _GridAgrChrgDisc;

        private R_TabStrip _tabStripAgrChrgDisc; //ref Tabstrip

        private R_TabPage _tabUndoAgrChrgDisc; //tabpageNextPricing

        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }

        private bool _comboboxPropertyEnabled = true; //to prevent combobox while crudmode

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _agreementChrgDiscViewModel.InitAsync(_localizer);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        public async Task ComboboxPropertyValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;
                await Task.Delay(300);
                if (_conAgrChrgDisc.R_ConductorMode == R_eConductorMode.Normal && _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID != "")
                {
                    //sending property to another tab (will be catch at RefreshTabPageAsync)
                    switch (_tabStripAgrChrgDisc.ActiveTab.Id)
                    {
                        case nameof(PMT05001):
                            await _tabUndoAgrChrgDisc.InvokeRefreshTabPageAsync(_agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        public async Task OnChanged_YearPeriod()
        {
            var loEx = new R_Exception();
            try
            {
                var loData = await _agreementChrgDiscViewModel.GetPeriodDTList();
                _agreementChrgDiscViewModel._MonthPeriodList = new ObservableCollection<GSPeriodDT_DTO>(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void BeforeOpenTabPage_Undo(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID;
            eventArgs.TargetPageType = typeof(PMT05001);
        }

        private void AgrChrgDiscList_GetList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void BtnRefresh_Click()
        {
            R_Exception loEx = new R_Exception();
            try
            {

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void BtnProcess_Click()
        {
            R_Exception loEx = new R_Exception();
            try
            {

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region lookup

        private void BeforeOpen_lookupUnitCharges(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00200ParameterDTO();
            eventArgs.TargetPageType = typeof(LML00200);
        }

        private async Task AfterOpen_lookupUnitChargesAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_validationDeptFromResult"], R_eMessageBoxButtonType.OK);
                return;
            }
            else
            {
                _agreementChrgDiscViewModel._AgreementChrgDiscListParam.CCHARGES_ID = loTempResult.CCHARGES_ID;
            }
        }

        private async Task OnLostFocus_LookupUnitChargesAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_ID))
                {

                    LookupLML00200ViewModel loLookupViewModel = new(); //use GSL's model
                    var loParam = new LML00200ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CSEARCH_TEXT = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_ID, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetUnitCharges(loParam); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                "_ErrLookup01"));
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_ID = ""; //kosongin bind textbox name kalo gaada
                    }
                    else
                    {
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_ID = loResult.CCHARGES_ID;

                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }

        private void BeforeOpen_lookupDiscount(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00200ParameterDTO();
            eventArgs.TargetPageType = typeof(LML00200);
        }

        private async Task AfterOpen_lookupDiscountAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_validationDeptFromResult"], R_eMessageBoxButtonType.OK);
                return;
            }
            else
            {
                _agreementChrgDiscViewModel._AgreementChrgDiscListParam.CCHARGES_ID = loTempResult.CCHARGES_ID;
            }
        }

        private async Task OnLostFocus_LookupDiscountAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_ID))
                {

                    LookupLML00200ViewModel loLookupViewModel = new(); //use GSL's model
                    var loParam = new LML00200ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CSEARCH_TEXT = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_ID, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetUnitCharges(loParam); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                "_ErrLookup01"));
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_ID = ""; //kosongin bind textbox name kalo gaada
                    }
                    else
                    {
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_ID = loResult.CCHARGES_ID;

                    }
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
