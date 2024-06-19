using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using PMR02400MODEL;
using PMR02400FrontResources;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Interfaces;
using R_BlazorFrontEnd.Exceptions;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00600;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Helpers;

namespace PMR02400FRONT
{
    public partial class PMR02400 : R_Page
    {
        private PMR02400ViewModel _viewModel = new();

        private R_Conductor _conductorRef;
        [Inject] IClientHelper _clientHelper { get; set; }
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {

            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.InitProcess(_localizer);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #region PropertyDropdown
        public void ComboboxPropertyValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel._ReportParam.CPROPERTY_ID = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;
                _viewModel._ReportParam.CPROPERTY_NAME =_viewModel._properties.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel._ReportParam.CPROPERTY_ID).CPROPERTY_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion
        private void BeforeOpen_lookupFromTenant(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00600ParameterDTO()
            { CCOMPANY_ID = _clientHelper.CompanyId, CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID, CCUSTOMER_TYPE = "01", CUSER_ID = _clientHelper.UserId, CSEARCH_TEXT = "" };
            eventArgs.TargetPageType = typeof(LML00600);
        }
        private async Task AfterOpen_lookupFromTenantAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00600DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel._ReportParam.CFROM_CUSTOMER_ID = loTempResult.CTENANT_ID;
                _viewModel._ReportParam.CFROM_CUSTOMER_NAME = loTempResult.CTENANT_NAME;
            }
        }
        private async Task OnLostFocus_LookupFromTenant()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CFROM_CUSTOMER_ID))
                {

                    LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel(); //use GSL's model
                    var loParam = new LML00600ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID,
                        CCUSTOMER_TYPE = "01",
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel._ReportParam.CFROM_CUSTOMER_ID, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetTenant(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CFROM_CUSTOMER_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel._ReportParam.CFROM_CUSTOMER_ID = loResult.CTENANT_ID;
                    _viewModel._ReportParam.CFROM_CUSTOMER_NAME = loResult.CTENANT_NAME; //assign bind textbox name kalo ada
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }
        private void BeforeOpen_lookupToTenant(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00600ParameterDTO()
            { CCOMPANY_ID = _clientHelper.CompanyId, CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID, CCUSTOMER_TYPE = "01", CUSER_ID = _clientHelper.UserId, CSEARCH_TEXT = "" };
            eventArgs.TargetPageType = typeof(LML00600);
        }
        private async Task AfterOpen_lookupToTenantAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00600DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel._ReportParam.CTO_CUSTOMER_ID = loTempResult.CTENANT_ID;
                _viewModel._ReportParam.CTO_CUSTOMER_NAME = loTempResult.CTENANT_NAME;
            }
        }
        private async Task OnLostFocus_LookupToTenant()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CTO_CUSTOMER_ID))
                {

                    LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel(); //use GSL's model
                    var loParam = new LML00600ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID,
                        CCUSTOMER_TYPE = "01",
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel._ReportParam.CTO_CUSTOMER_ID, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetTenant(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CTO_CUSTOMER_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel._ReportParam.CTO_CUSTOMER_ID = loResult.CTENANT_ID;
                    _viewModel._ReportParam.CTO_CUSTOMER_NAME = loResult.CTENANT_NAME; //assign bind textbox name kalo ada
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }

        private async Task OnclickBtn_Print() { }
    }
}
