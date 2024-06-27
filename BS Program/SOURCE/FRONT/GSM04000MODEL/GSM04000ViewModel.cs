using GSM04000Common;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using R_ContextFrontEnd;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GSM04000Model
{
    public class GSM04000ViewModel : R_ViewModel<GSM04000DTO>
    {
        private GSM04000Model _model = new GSM04000Model();
        public ObservableCollection<GSM04000DTO> _DepartmentList { get; set; } = new ObservableCollection<GSM04000DTO>();
        public GSM04000DTO _Department { get; set; } = new GSM04000DTO();
        public string _departmentCode { get; set; } = "";
        public bool _activeDept { get; set; }
        public bool _isUserDeptExist { get; set; }
        public string _sourceFileName { get; set; }

        #region Department
        public async Task GetDepartmentList()
        {
            R_Exception loEx = new R_Exception();
            List<GSM04000DTO> loResult = null;
            try
            {
                loResult = new List<GSM04000DTO>();
                loResult = await _model.GetGSM04000ListAsync();
                _DepartmentList = new ObservableCollection<GSM04000DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetDepartment(GSM04000DTO poDept)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                GSM04000DTO loParam = new GSM04000DTO();
                loParam = poDept;
                var loResult = await _model.R_ServiceGetRecordAsync(loParam);
                _Department = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveDepartment(GSM04000DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                _Department = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteDepartment(GSM04000DTO poDept)
        {
            var loEx = new R_Exception();

            try
            {
                GSM04000DTO loParam = new GSM04000DTO();
                loParam = poDept;
                await _model.R_ServiceDeleteAsync(poDept);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ActiveInactiveProcessAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetContext(ContextConstant.CDEPT_CODE, _departmentCode);
                R_FrontContext.R_SetContext(ContextConstant.LACTIVE, _activeDept);
                await _model.RSP_GS_ACTIVE_INACTIVE_DEPTMethodAsync();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task CheckIsUserDeptExistAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetContext(ContextConstant.CDEPT_CODE, _departmentCode);
                var loResult = await _model.CheckIsUserDeptExistAsync();
                _isUserDeptExist = loResult.UserDeptExist;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteAssignedUserWhenChangeEveryone()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetContext(ContextConstant.CDEPT_CODE, _departmentCode);
                var loResult = await _model.DeleteDeptUserWhenChaningEveryoneAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Template
        public async Task<UploadFileDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            UploadFileDTO loResult = null;

            try
            {
                loResult = await _model.DownloadTemplateDeptartmentAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        
    }

}

