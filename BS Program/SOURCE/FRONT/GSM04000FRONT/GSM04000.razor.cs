using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using GSM04000Common;
using GSM04000Model;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;

namespace GSM04000Front
{
    public partial class GSM04000 : R_Page
    {
        private GSM04000ViewModel _deptViewModel = new GSM04000ViewModel();
        private R_Grid<GSM04000DTO> _gridDeptRef;

        private GSM04100ViewModel _deptUserViewModel = new GSM04100ViewModel();
        private R_ConductorGrid _conGridDeptRef;

        private R_Grid<GSM04100StreamDTO> _gridDeptUserRef;
        private R_ConductorGrid _conGridDeptUserRef;

        [Inject] R_PopupService PopupService { get; set; }
        [Inject] IClientHelper _clientHelper { get; set; }

        [Inject] private R_ILocalizer<GSM04000FrontResources.Resources_Dummy_Class> _localizer { get; set; }


        private string _labelActiveInactive = "";
        public R_Popup btnAssignUser { get; set; }
        public R_Popup btnActiveInactive { get; set; }
        private bool _enableBtnAssignUser;
        private bool _enableBtnActiveInactive = true;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _gridDeptRef.R_RefreshGrid(null);
                _labelActiveInactive = _localizer["_lblDefaultActiveInactive"];
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region GridDept
        private async Task DeptGrid_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _deptViewModel.GetDepartmentList();
                eventArgs.ListEntityResult = _deptViewModel._DepartmentList;
                if (_deptViewModel._DepartmentList.Count == 0)
                {
                    _enableBtnActiveInactive = false;
                    _enableBtnAssignUser = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task DeptGrid_ServiceGetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM04000DTO)eventArgs.Data;
                await _deptViewModel.GetDepartment(loParam);

                eventArgs.Result = _deptViewModel._Department;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task DeptGrid_ValidationAsync(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            GFF00900ParameterDTO loParam = null;
            R_PopupResult loResult = null;
            try
            {
                //get data
                var loData = (GSM04000DTO)eventArgs.Data;

                switch (_conGridDeptRef.R_ConductorMode)
                {
                    case R_eConductorMode.Add:

                        //Approval before saving
                        if (loData.LACTIVE == true)
                        {
                            var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                            loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM04001";
                            await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync();

                            if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                            {
                                eventArgs.Cancel = false;
                            }
                            else
                            {
                                loParam = new GFF00900ParameterDTO()
                                {
                                    Data = loValidateViewModel.loRspActivityValidityList,
                                    IAPPROVAL_CODE = "GSM04001"
                                };
                                loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParam);
                                if (loResult.Success == false || (bool)loResult.Result == false)
                                {
                                    eventArgs.Cancel = true;
                                }
                            }
                        }
                        //~End approval
                        break;

                    //case validation when editmode
                    case R_eConductorMode.Edit:

                        //check when changing everyone then delete user
                        if (_deptViewModel._Department.LEVERYONE == false && loData.LEVERYONE == true)
                        {
                            await _deptViewModel.CheckIsUserDeptExistAsync();
                            if (_deptViewModel._isUserDeptExist)
                            {
                                var loConfirm = await R_MessageBox.Show("Delete Confirmation", "Changing Value Everyone will delete User for this Department", R_eMessageBoxButtonType.OKCancel);
                                if (loConfirm == R_eMessageBoxResult.Cancel)
                                {
                                    eventArgs.Cancel = true;
                                }
                                await _deptViewModel.DeleteAssignedUserWhenChangeEveryone();
                            }
                        }
                        //

                        if (string.IsNullOrEmpty(loData.CDEPT_CODE) || string.IsNullOrWhiteSpace(loData.CDEPT_CODE))
                        {
                            loEx.Add("001", "Department Code can not be empty");
                        }
                        if (string.IsNullOrEmpty(loData.CDEPT_NAME) || string.IsNullOrWhiteSpace(loData.CDEPT_NAME))
                        {
                            loEx.Add("002", "Department Name can not be empty");
                        }
                        if (string.IsNullOrEmpty(loData.CCENTER_CODE) || string.IsNullOrWhiteSpace(loData.CCENTER_CODE))
                        {
                            loEx.Add("003", "Center can not be empty");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            if (loEx.HasError)
                eventArgs.Cancel = true;
            loEx.ThrowExceptionIfErrors();
        }

        private void DeptGrid_Saving(R_SavingEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = (GSM04000DTO)eventArgs.Data;
                loData.CMANAGER_NAME = loData.CMANAGER_CODE;
                loData.CDEPT_CODE = string.IsNullOrWhiteSpace(loData.CDEPT_CODE) ? "" : loData.CDEPT_CODE;
                loData.CDEPT_NAME = string.IsNullOrWhiteSpace(loData.CDEPT_NAME) ? "" : loData.CDEPT_NAME;
                loData.CCENTER_CODE = string.IsNullOrWhiteSpace(loData.CCENTER_CODE) ? "" : loData.CCENTER_CODE;
                loData.CMANAGER_NAME = string.IsNullOrWhiteSpace(loData.CMANAGER_NAME) ? "" : loData.CMANAGER_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void DeptGrid_BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = (GSM04000DTO)eventArgs.Data;
                loData.CMANAGER_NAME = loData.CMANAGER_CODE;
                loData.CMANAGER_NAME = string.IsNullOrWhiteSpace(loData.CMANAGER_NAME) ? "" : loData.CMANAGER_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task DeptGrid_ServiceSaveAsync(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _deptViewModel.SaveDepartment((GSM04000DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _deptViewModel._Department;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task DeptGrid_ServiceDeleteAsync(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (GSM04000DTO)eventArgs.Data;
                await _deptViewModel.DeleteDepartment(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task DeptGrid_DisplayAsync(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM04000DTO>(eventArgs.Data);
                switch (eventArgs.ConductorMode)
                {
                    case R_eConductorMode.Normal:
                        //set to viewmodel parent
                        _deptViewModel._departmentCode = loParam.CDEPT_CODE;
                        _deptViewModel._activeDept = loParam.LACTIVE;

                        if (loParam.LACTIVE)
                        {
                            _labelActiveInactive = _localizer["_lblInactive"];
                            _deptViewModel._activeDept = false;
                        }
                        else
                        {
                            _labelActiveInactive = _localizer["_lblActive"];
                            _deptViewModel._activeDept = true;
                        }

                        if (loParam.LEVERYONE == false && loParam.LACTIVE == true)
                        {
                            _enableBtnAssignUser = true;
                        }
                        else { _enableBtnAssignUser = false; }

                        //set deptcode to view model child
                        _deptUserViewModel._DepartmentCode = loParam.CDEPT_CODE;
                        _deptUserViewModel._DepartmentName = loParam.CDEPT_NAME;

                        //refresh grid child
                        if (!string.IsNullOrWhiteSpace(_deptUserViewModel._DepartmentCode))
                        {
                            await _gridDeptUserRef.R_RefreshGrid(loParam);
                        }
                        break;

                    case R_eConductorMode.Edit:
                        if (loParam.LEVERYONE != true)
                        {
                            await R_MessageBox.Show("Confirmation", "changing this will delete all assinged user on this dept", R_eMessageBoxButtonType.OKCancel);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void DeptGrid_SetAdd(R_SetEventArgs eventArgs)
        {
            _enableBtnActiveInactive = !eventArgs.Enable;
        }
        private void DeptGrid_SetEdit(R_SetEventArgs eventArgs)
        {
            _enableBtnActiveInactive = !eventArgs.Enable;
        }
        private void DeptGrid_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (GSM04000DTO)eventArgs.Data;
                loData.LACTIVE = true;//set active=true as default
                loData.DCREATE_DATE = DateTime.Now;//set now date when adding data
                loData.DUPDATE_DATE = DateTime.Now;//set now date when adding data
                //_enableBtnActiveInactive = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region GridLookup

        private void Dept_Before_Open_Lookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {

            //membedakan columname dan mengarahkan tampil lookup
            switch (eventArgs.ColumnName)
            {
                case "CCENTER":
                    eventArgs.Parameter = new GSL00900ParameterDTO();
                    eventArgs.TargetPageType = typeof(GSL00900);
                    break;
                case "CMANAGER_NAME":
                    eventArgs.TargetPageType = typeof(GSL01000);
                    break;
            }

        }

        private void Dept_After_Open_Lookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                if (eventArgs.Result == null)
                {
                    return;
                }
                //mengambil result dari popup dan set ke data row
                switch (eventArgs.ColumnName)
                {
                    case "CCENTER":
                        var loCenterLookupresult = R_FrontUtility.ConvertObjectToObject<GSL00900DTO>(eventArgs.Result);
                        ((GSM04000DTO)eventArgs.ColumnData).CCENTER_CODE = loCenterLookupresult.CCENTER_CODE;
                        ((GSM04000DTO)eventArgs.ColumnData).CCENTER_NAME = loCenterLookupresult.CCENTER_NAME;
                        break;
                    case "CMANAGER_NAME":
                        var loManagerLookupresult = R_FrontUtility.ConvertObjectToObject<GSL01000DTO>(eventArgs.Result);
                        ((GSM04000DTO)eventArgs.ColumnData).CMANAGER_CODE = loManagerLookupresult.CUSER_ID;
                        ((GSM04000DTO)eventArgs.ColumnData).CMANAGER_NAME = loManagerLookupresult.CUSER_NAME;
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }

        #endregion//GridLookup

        #endregion//GridDept

        #region Template
        private async Task DownloadTemplateAsync()
        {

            var loEx = new R_Exception();
            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure download this template?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await _deptViewModel.DownloadTemplate();

                    var saveFileName = $"Department - {_clientHelper.CompanyId}.xlsx";

                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            R_DisplayException(loEx);
        }
        #endregion//Template

        #region Upload
        private void R_Before_Open_PopupUpload(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSM04000PopupUpload);
        }
        public async Task R_After_Open_PopupUpload(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _gridDeptRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        #endregion//Upload

        #region Assign User
        private void R_Before_Open_PopupAssignUser(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = _deptViewModel._departmentCode;
            eventArgs.TargetPageType = typeof(GSM04000PopupAssignUser);
        }
        private async Task R_After_Open_PopupAssignUser(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {

                var loTempResult = R_FrontUtility.ConvertObjectToObject<GSM04100DTO>(eventArgs.Result);
                if (loTempResult == null) //if there is no user select on popup
                {
                    await R_MessageBox.Show("", "no selected user",R_eMessageBoxButtonType.OK);
                    goto EndBlock;
                }
                await _deptUserViewModel.AssignUserToDept(new GSM04100DTO()
                {
                    CDEPT_CODE=_deptViewModel._departmentCode,
                    CUSER_ID = loTempResult.CUSER_ID
                },
                eCRUDMode.AddMode);
                await _gridDeptUserRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        #endregion//Assign User

        #region Active/Inactive

        private async Task R_Before_Open_Popup_ActivateInactiveAsync(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM04001"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await _deptViewModel.ActiveInactiveProcessAsync(); //Ganti jadi method ActiveInactive masing masing
                    await _gridDeptRef.R_RefreshGrid(null);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "GSM04001" //Uabh Approval Code sesuai Spec masing masing
                    };
                    eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        
        private async Task R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            var loData = (GSM04000DTO)_conGridDeptRef.R_GetCurrentData();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }
                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await _deptViewModel.ActiveInactiveProcessAsync();
                    await _deptViewModel.GetDepartment(loData);
                    await _gridDeptRef.R_SetCurrentData(_deptViewModel._Department);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
  
        #endregion//Active/Inactive

        #region DepartmentUser(CHILD)
        
        private async Task DeptUserGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _deptUserViewModel._DepartmentCode = _deptViewModel._departmentCode;
                await _deptUserViewModel.GetDeptUserListByDeptCode();
                eventArgs.ListEntityResult = _deptUserViewModel._DepartmentUserList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        
        private async Task DeptUserGrid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM04100DTO>(eventArgs.Data);
                loParam.CDEPT_CODE = _deptUserViewModel._DepartmentCode;
                await _deptUserViewModel.GetDepartmentUser(loParam);
                eventArgs.Result = _deptUserViewModel._DepartmentUser;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        
        private async Task DeptUserGrid_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (GSM04100StreamDTO)eventArgs.Data;
                await _deptUserViewModel.DeleteUserDepartment(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        
        #endregion//DepartmentUser(CHILD)
    }
}
