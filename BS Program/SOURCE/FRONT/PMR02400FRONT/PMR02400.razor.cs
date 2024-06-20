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
using System.Globalization;
using PMR02400COMMON.DTO_s;

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
                _viewModel._ReportParam.CPROPERTY_NAME = _viewModel._properties.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel._ReportParam.CPROPERTY_ID).CPROPERTY_NAME;
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

        private async Task OnclickBtn_Print()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                //validation
                if (string.IsNullOrWhiteSpace(_viewModel._ReportParam.CPROPERTY_ID))
                {
                    loEx.Add("", _localizer["_val1"]);
                    goto EndBlock;
                }
                if (_viewModel._DateBasedOn == "1" & _viewModel._DateCutOff == null)
                {
                    loEx.Add("", _localizer["_val2"]);
                    goto EndBlock;
                }

                if (string.IsNullOrEmpty(_viewModel._MonthFromPeriod) || string.IsNullOrEmpty(_viewModel._MonthToPeriod) || _viewModel._YearFromPeriod == 0 || _viewModel._YearToPeriod == 0)
                {
                    loEx.Add("", _localizer["_val3"]);
                    goto EndBlock;
                }

                if (_viewModel._DateBasedOn == "2" & int.Parse(_viewModel._YearFromPeriod + _viewModel._MonthFromPeriod) > int.Parse(_viewModel._YearToPeriod + _viewModel._MonthToPeriod))
                {
                    loEx.Add("", _localizer["_val4"]);
                    goto EndBlock;
                }

                if (string.IsNullOrWhiteSpace(_viewModel._ReportParam.CFROM_CUSTOMER_ID) || string.IsNullOrWhiteSpace(_viewModel._ReportParam.CTO_CUSTOMER_ID))
                {
                    loEx.Add("", _localizer["_val5"]);
                    goto EndBlock;
                }

                //combine data
                _viewModel._ReportParam.FROM_CPERIOD = _viewModel._YearFromPeriod + _viewModel._MonthFromPeriod;
                _viewModel._ReportParam.CTO_CPERIOD = _viewModel._YearToPeriod + _viewModel._MonthToPeriod;
                _viewModel._ReportParam.LIS_BASED_ON_CUTOFF = _viewModel._DateBasedOn == "1" ? true : false;
                _viewModel._ReportParam.CCUT_OFF = _viewModel._DateBasedOn == "1" ? _viewModel._DateCutOff.ToString("yyyyMMdd") : "";

                //setting based on display
                if (_viewModel._ReportParam.LIS_BASED_ON_CUTOFF)
                {
                    _viewModel._ReportParam.CBASED_ON_DISPLAY = DateTime.TryParseExact(_viewModel._ReportParam.CCUT_OFF, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime poCutOffDate)
                        ? poCutOffDate.ToString("dd MMM yyyy", CultureInfo.InvariantCulture)
                        : "";
                }
                else
                {
                    var fromPeriod = DateTime.TryParseExact(_viewModel._ReportParam.FROM_CPERIOD, "yyyyMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate);
                    var toPeriod = DateTime.TryParseExact(_viewModel._ReportParam.CTO_CPERIOD, "yyyyMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime toDate);
                    _viewModel._ReportParam.CBASED_ON_DISPLAY = fromPeriod != toPeriod ? $"{fromDate.ToString("MMM yyyy", CultureInfo.InvariantCulture)} - {toDate.ToString("MMM yyyy", CultureInfo.InvariantCulture)}" : $"{fromDate.ToString("MMM yyyy", CultureInfo.InvariantCulture)}";
                }
                _viewModel._ReportParam.CCOMPANY_ID = _clientHelper.CompanyId;
                _viewModel._ReportParam.CUSER_ID = _clientHelper.UserId;
                _viewModel._ReportParam.CREPORT_CULTURE = _clientHelper.ReportCulture.ToString();
                _viewModel._ReportParam.CLANG_ID = _clientHelper.Culture.TwoLetterISOLanguageName;

                _viewModel._ReportParam.CREPORT_TYPE_DISPLAY = _viewModel._ReportParam.CREPORT_TYPE == "1" ? _localizer["_radio_Summary"] : _localizer["_radio_Detail"];

                //rute report
                if (_viewModel._ReportParam.CREPORT_TYPE == "1")
                {
                    await Penalty_PrintSummary(_viewModel._ReportParam);
                }
                else if (_viewModel._ReportParam.CREPORT_TYPE == "2")
                {
                    await Penalty_PrintDetail(_viewModel._ReportParam);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task Penalty_PrintSummary(PMR02400ParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _reportService.GetReport(
                    "R_DefaultServiceUrlPM",
                    "PM",
                    "rpt/PMR02410Print/DownloadResultPrintPost",
                    "rpt/PMR02410Print/PenaltySummary_ReportListGet",
                    poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task Penalty_PrintDetail(PMR02400ParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _reportService.GetReport(
                    "R_DefaultServiceUrlPM",
                    "PM",
                    "rpt/PMR02420Print/DownloadResultPrintPost",
                    "rpt/PMR02420Print/PenaltyDetail_ReportListGet",
                    poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

    }
}
