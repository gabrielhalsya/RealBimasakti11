using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00500;
using Microsoft.AspNetCore.Components;
using PMR00600COMMON;
using PMR00600COMMON.DTO_s;
using PMR00600COMMON.DTO_s.Print;
using PMR00600FrontResources;
using PMR00600MODEL.View_Models;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PMR00600FRONT
{
    public partial class PMR00600 : R_Page
    {
        private PMR00600ViewModel _viewModel = new();

        private R_Conductor _conductorRef;
        [Inject] IClientHelper _clientHelper { get; set; }
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        private R_RadioGroup<ReportTypeDTO, string> _radioReportType;
        private R_RadioGroup<GroupTypeDTO, string> _radioGroupingType;
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
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region lookupFromDept
        private void BeforeOpen_lookupFromDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL00700ParameterDTO();
            eventArgs.TargetPageType = typeof(GSL00700);
        }
        private async Task AfterOpen_lookupFromDeptAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_validationDeptFromResult"], R_eMessageBoxButtonType.OK);
                return;
            }
            _viewModel._ReportParam.CFROM_DEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel._ReportParam.CFROM_DEPT_NAME = loTempResult.CDEPT_CODE;
        }
        private async Task OnLostFocus_LookupFromDept()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CFROM_DEPT_CODE))
                {

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel(); //use GSL's model
                    var loParam = new GSL00700ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CSEARCH_TEXT = _viewModel._ReportParam.CFROM_DEPT_CODE, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetDepartment(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CFROM_DEPT_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel._ReportParam.CFROM_DEPT_CODE = loResult.CDEPT_CODE;
                    _viewModel._ReportParam.CFROM_DEPT_NAME = loResult.CDEPT_NAME; //assign bind textbox name kalo ada
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }
        private void BeforeOpen_lookupToDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL00700ParameterDTO();
            eventArgs.TargetPageType = typeof(GSL00700);
        }
        private async Task AfterOpen_lookupToDeptAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_validationDeptToResult"], R_eMessageBoxButtonType.OK);
                return;
            }
            _viewModel._ReportParam.CTO_DEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel._ReportParam.CTO_DEPT_NAME = loTempResult.CDEPT_CODE;
        }
        private async Task OnLostFocus_LookupToDept()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CTO_DEPT_CODE))
                {

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel(); //use GSL's model
                    var loParam = new GSL00700ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CSEARCH_TEXT = _viewModel._ReportParam.CTO_DEPT_CODE, // property that bindded to search textbox
                    };

                    var loResult = await loLookupViewModel.GetDepartment(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CTO_DEPT_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel._ReportParam.CTO_DEPT_CODE = loResult.CDEPT_CODE;
                    _viewModel._ReportParam.CTO_DEPT_NAME = loResult.CDEPT_NAME; //assign bind textbox name kalo ada
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

        #region lookupBuilding
        private void BeforeOpen_lookupFromBuilding(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL02200ParameterDTO()
            { CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID };
            eventArgs.TargetPageType = typeof(GSL02200);
        }
        private async Task AfterOpen_lookupFromBuildingAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL02200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_validationBuildingFromResult"], R_eMessageBoxButtonType.OK);
                return;
            }
            _viewModel._ReportParam.CFROM_BUILDING_ID = loTempResult.CBUILDING_ID;
            _viewModel._ReportParam.CFROM_BUILDING_NAME = loTempResult.CBUILDING_NAME;
        }
        private async Task OnLostFocus_LookupFromBuilding()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CFROM_BUILDING_ID))
                {

                    LookupGSL02200ViewModel loLookupViewModel = new LookupGSL02200ViewModel(); //use GSL's model
                    var loParam = new GSL02200ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CSEARCH_TEXT = _viewModel._ReportParam.CFROM_BUILDING_ID, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetBuilding(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CFROM_BUILDING_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel._ReportParam.CFROM_BUILDING_ID = loResult.CBUILDING_ID;
                    _viewModel._ReportParam.CFROM_BUILDING_NAME = loResult.CBUILDING_NAME; //assign bind textbox name kalo ada
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }
        private void BeforeOpen_lookupToBuilding(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL02200ParameterDTO()
            { CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID };
            eventArgs.TargetPageType = typeof(GSL02200);
        }
        private async Task AfterOpen_lookupToBuildingAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL02200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_validationBuildingToResult"], R_eMessageBoxButtonType.OK);
                return;
            }
            _viewModel._ReportParam.CTO_BUILDING_ID = loTempResult.CBUILDING_ID;
            _viewModel._ReportParam.CTO_BUILDING_NAME = loTempResult.CBUILDING_NAME;
        }
        private async Task OnLostFocus_LookupToBuilding()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel._ReportParam.CTO_BUILDING_ID))
                {

                    LookupGSL02200ViewModel loLookupViewModel = new LookupGSL02200ViewModel(); //use GSL's model
                    var loParam = new GSL02200ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CSEARCH_TEXT = _viewModel._ReportParam.CTO_BUILDING_ID, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetBuilding(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel._ReportParam.CFROM_BUILDING_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel._ReportParam.CTO_BUILDING_ID = loResult.CBUILDING_ID;
                    _viewModel._ReportParam.CTO_BUILDING_NAME = loResult.CBUILDING_NAME; //assign bind textbox name kalo ada
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

        #region print
        private void OnclickBtn_Print()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loParam = new PMR00600ParamDTO()
                {
                    //CLANG_ID = _clientHelper.Culture.Name,
                    //CCOMPANY_ID = _clientHelper.CompanyId,
                    //CPROPERTY_ID = _viewModel._ReportParam.CPROPERTY_ID,
                    //CPROPERTY_NAME = _viewModel._properties.Where(x => x.CPROPERTY_ID == _viewModel._ReportParam.CPROPERTY_ID).FirstOrDefault().CPROPERTY_NAME,
                    //Cfrom DEPARTMENT_CODE = _viewModel._ReportParam.CFROM_DEPARTMENT_CODE,
                    //CFROM_DEPT_NAME = _viewModel._ReportParam.CFROM_DEPT_NAME,
                    //CTO_DEPT_CODE = _viewModel._ReportParam.CTO_DEPT_CODE,
                    //CTO_DEPT_NAME = _viewModel._ReportParam.CTO_DEPT_NAME,
                    //CFROM_SALESMAN_ID = _viewModel._ReportParam.CFROM_SALESMAN_ID,
                    //CFROM_SALESMAN_NAME = _viewModel._ReportParam.CFROM_SALESMAN_NAME,
                    //CTO_SALESMAN_ID = _viewModel._ReportParam.CTO_SALESMAN_ID,
                    //CTO_SALESMAN_NAME = _viewModel._ReportParam.CTO_SALESMAN_NAME,
                    //CFROM_PERIOD = _viewModel._YearFromPeriod + _viewModel._MonthFromPeriod, //yyyyMM
                    //CTO_PERIOD = _viewModel._YearToPeriod + _viewModel._MonthToPeriod, //yyyyMM
                    //LIS_OUTSTANDING = _viewModel._ReportParam.LIS_OUTSTANDING,
                };

                //validation
                //if (string.IsNullOrWhiteSpace(loParam.CPROPERTY_ID))
                //{
                //    loEx.Add("", _localizer["_validationEmptyProperty"]);
                //}
                //if (string.IsNullOrWhiteSpace(loParam.CFROM_DEPT_CODE))
                //{
                //    loEx.Add("", _localizer["_validationEmptyFromDept"]);
                //}
                //if (string.IsNullOrWhiteSpace(loParam.CTO_DEPT_CODE))
                //{
                //    loEx.Add("", _localizer["_validationEmptyToDept"]);
                //}
                //if (string.IsNullOrWhiteSpace(loParam.CFROM_SALESMAN_ID))
                //{
                //    loEx.Add("", _localizer["_validationEmptyFromSalesman"]);
                //}
                //if (string.IsNullOrWhiteSpace(loParam.CTO_SALESMAN_ID))
                //{
                //    loEx.Add("", _localizer["_validationEmptyToSalesman"]);
                //}
                //if (int.Parse(loParam.CTO_PERIOD) > int.Parse(loParam.CFROM_PERIOD))
                //{
                //    loEx.Add("", _localizer["_validationHigherPeriod"]);
                //}

                //if (_viewModel._ReportType == "D")
                //{
                //    Overtime_PrintDetail(loParam);
                //}
                //else
                //{
                //    Overtime_PrintSummaryAsync(loParam);
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Overtime_PrintSummaryAsync(PMR00600ParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _reportService.GetReport(
                    "R_DefaultServiceUrlPM",
                    "PM",
                    "rpt/PMR00210Print/DownloadResultPrintPost",
                    "rpt/PMR00210Print/OvertimeSummary_ReportListGet",
                    poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void Overtime_PrintDetail(PMR00600ParamDTO poParam)
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
        #endregion


    }
}

