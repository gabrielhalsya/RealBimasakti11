using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using GSM04000Common;
using GSM04000Model;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Helpers;
using System;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd;
using Microsoft.AspNetCore.Components.Forms;
using R_BlazorFrontEnd.Controls.Enums;
using System.Collections.ObjectModel;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Excel;
using BlazorClientHelper;
using Microsoft.JSInterop;
using R_APICommonDTO;

namespace GSM04000Front
{
    public partial class GSM04000PopupUpload : R_Page
    {
        private GSM04000ViewModelUploadDept _deptUploadViewModel = new GSM04000ViewModelUploadDept();
        private R_Grid<GSM04000ExcelGridDTO> _gridDeptExcelRef;
        private R_ConductorGrid _conGridDeptExcelRef;
        [Inject] private R_IExcel _excelProvider { get; set; }
        [Inject] private IClientHelper _clientHelper { get; set; }
        private R_eFileSelectAccept[] _accepts = { R_eFileSelectAccept.Excel };
        public byte[] _fileByte = null;
        private bool _isFileHasData { get; set; }
        #region invoke
        // Create Method Action StateHasChange
        private void StateChangeInvoke()
        {
            StateHasChanged();
        }
        // Create Method Action For Error Unhandle
        private void ShowErrorInvoke(R_Exception poEx)
        {
            this.R_DisplayException(poEx);
        }
        public async Task ShowSuccessInvoke()
        {
            var loValidate = await R_MessageBox.Show("", "Upload Data Successfully", R_eMessageBoxButtonType.OK);
            if (loValidate == R_eMessageBoxResult.OK)
            {
                await this.Close(true, true);
            }
        }
        #endregion invoke

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _deptUploadViewModel._stateChangeAction = StateChangeInvoke;
                _deptUploadViewModel._showErrorAction = ShowErrorInvoke;
                _deptUploadViewModel._actionDataSetExcel = SaveValidatedDataToExcel;

                _deptUploadViewModel._ccompanyId = _clientHelper.CompanyId;
                _deptUploadViewModel._cuserId = _clientHelper.UserId;

                _deptUploadViewModel._showSuccessAction = async () =>
                {
                    await ShowSuccessInvoke();
                };
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task UploadExcel(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                _deptUploadViewModel._sourceFileName = eventArgs.File.Name;
                //import from excel
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                var loFileByte = loMS.ToArray();

                //add filebyte to DTO
                var loExcel = _excelProvider;

                var loDataSet = loExcel.R_ReadFromExcel(loFileByte, new string[] { "Department" });

                var loResult = R_FrontUtility.R_ConvertTo<GSM04000ExcelToUploadDTO>(loDataSet.Tables[0]);

                _isFileHasData = loResult.Count > 0 ? true : false;

                await _gridDeptExcelRef.R_RefreshGrid(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task DeptExcelGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (List<GSM04000ExcelToUploadDTO>)eventArgs.Parameter;
                await _deptUploadViewModel.ConvertGridExelToGridDTO(loData);
                eventArgs.ListEntityResult = _deptUploadViewModel._DepartmentExcelGridData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        public async Task OnClick_ButtonOK()
        {
            var loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure want to import data?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _deptUploadViewModel.SaveBulk_DeptExcelData();

                    if (_deptUploadViewModel._visibleError)
                    {
                        await R_MessageBox.Show("", "Department uploaded successfully!", R_eMessageBoxButtonType.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        public async Task OnClick_ButtonSaveExcelAsync()
        {
            var loValidate = await R_MessageBox.Show("", "Are you sure want to save to excel ?", R_eMessageBoxButtonType.YesNo);

            if (loValidate == R_eMessageBoxResult.Yes)
            {
                await SaveValidatedDataToExcel();
            }
        }
        // Create Method Action For Download Excel if Has Error
        private async Task SaveValidatedDataToExcel()
        {
            var loByte = _excelProvider.R_WriteToExcel(_deptUploadViewModel._excelDataset);
            var lcName = $"{_clientHelper.CompanyId}" + ".xlsx";

            await _JSRuntime.downloadFileFromStreamHandler(lcName, loByte);
        }
        public async Task OnClick_ButtonClose()
        {
            await this.Close(true, false);
        }

 
    }
}
