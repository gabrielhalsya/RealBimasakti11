using Microsoft.AspNetCore.Components;
using PMT05000COMMON.DTO_s;
using PMT05000MODEL.ViewModel_s;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Interfaces;
using PMT05000FrontResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00200;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System.Collections.ObjectModel;
using BlazorClientHelper;
using Lookup_PMModel.ViewModel.LML00700;

namespace PMT05000FRONT
{
    public partial class PMT05001 : R_Page, R_ITabPage
    {
        private PMT05000ViewModel _agreementChrgDiscViewModel = new PMT05000ViewModel();

        private R_ConductorGrid _conAgrChrgDisc;

        private R_Grid<AgreementChrgDiscDetailDTO> _GridAgrChrgDisc;

        [Inject] IClientHelper _clientHelper { get; set; }

        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }

        public async Task RefreshTabPageAsync(object poParam)
        {
            R_Exception loEx = new();
            try
            {
                await _agreementChrgDiscViewModel.InitAsync(_localizer);
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID = poParam as string;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
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

        private async Task AgrChrgDiscList_GetList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _agreementChrgDiscViewModel.GetAgreementChrgDiscListAsync();
                eventArgs.ListEntityResult = _agreementChrgDiscViewModel._AgreementChrgDiscDetailList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void AgrChrgDiscList_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Result = eventArgs.Data as AgreementChrgDiscDetailDTO;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task BtnRefresh_Click()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _agreementChrgDiscViewModel.GetAgreementChrgDiscListAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task BtnProcess_ClickAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _agreementChrgDiscViewModel.ProcessAgreementChrgDiscAsync("UNDO");
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
            eventArgs.Parameter = new LML00200ParameterDTO()
            {
                CPROPERTY_ID = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID,
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_ID = _clientHelper.UserId,
            };
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
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_ID = loTempResult.CCHARGES_ID;
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
                    var loResult = await loLookupViewModel.GetUnitCharges(new LML00200ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CPROPERTY_ID = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID,
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_ID, // property that bindded to search textbox
                    }); //retrive single record

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
            eventArgs.Parameter = new LML00700ParameterDTO()
            {
                CPROPERTY_ID = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID,
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_ID = _clientHelper.UserId,
            };
            eventArgs.TargetPageType = typeof(LML00700);
        }

        private async Task AfterOpen_lookupDiscountAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_validationDeptFromResult"], R_eMessageBoxButtonType.OK);
                return;
            }
            else
            {
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_CODE = loTempResult.CDISCOUNT_CODE;
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_TYPE = loTempResult.CDISCOUNT_TYPE;
            }
        }

        private async Task OnLostFocus_LookupDiscountAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_CODE))
                {

                    LookupLML00700ViewModel loLookupViewModel = new(); //use GSL's model
                    var loResult = await loLookupViewModel.GetDiscount(new LML00700ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CPROPERTY_ID = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID,
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_ID, // property that bindded to search textbox
                    }); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                "_ErrLookup01"));
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_CODE = ""; //kosongin bind textbox name kalo gaada
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_TYPE = ""; //kosongin bind textbox name kalo gaada
                    }
                    else
                    {
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_CODE = loResult.CDISCOUNT_CODE;
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_TYPE = loResult.CDISCOUNT_TYPE;
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

        private void BeforeOpen_lookupBuilding(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL02200ParameterDTO()
            {
                CPROPERTY_ID = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID,
                CSEARCH_TEXT = ""
            };
            eventArgs.TargetPageType = typeof(GSL02200);
        }

        private async Task AfterOpen_lookupBuildingAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL02200DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_ID = loTempResult.CBUILDING_ID;
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_NAME = loTempResult.CBUILDING_NAME;
            }
        }

        private async Task OnLostFocus_LookupBuilding()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_ID))
                {

                    LookupGSL02200ViewModel loLookupViewModel = new LookupGSL02200ViewModel(); //use GSL's model
                    var loParam = new GSL02200ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {

                        CPROPERTY_ID = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID,
                        CSEARCH_TEXT = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_ID, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetBuilding(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_NAME = ""; //kosongin bind textbox name kalo gaada
                    }
                    else
                    {
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_ID = loResult.CBUILDING_ID;
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_NAME = loResult.CBUILDING_NAME; //assign bind textbox name kalo ada
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        #endregion


    }
}
