using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using GSM04000Common;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using R_CommonFrontBackAPI;
using R_BlazorFrontEnd.Helpers;

namespace GSM04000Model
{
    public class GSM04100ViewModel : R_ViewModel<GSM04100DTO>
    {
        private GSM04100Model _model = new GSM04100Model();
        public ObservableCollection<GSM04100StreamDTO> _DepartmentUserList { get; set; } = new ObservableCollection<GSM04100StreamDTO>();
        public ObservableCollection<GSM04100DTO> _UsersToAssignList { get; set; } = new ObservableCollection<GSM04100DTO>();
        public GSM04100DTO _DepartmentUser { get; set; } = new GSM04100DTO();
        public GSM04100DTO _UserToAssign { get; set; } = new GSM04100DTO();
        public string _DepartmentCode { get; set; } = "";
        public string _DepartmentName { get; set; } = "";
        public const string CPROGRAM_CODE = "GSM04000";
        public async Task GetDeptUserListByDeptCode()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, _DepartmentCode);
                var loResult = await _model.GetGSM04100ListByDeptCodeAsync();
                _DepartmentUserList = new ObservableCollection<GSM04100StreamDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetDepartmentUser(GSM04100DTO poDept)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, _DepartmentCode);
                GSM04100DTO loParam = new GSM04100DTO();
                loParam = poDept;
                var loResult = await _model.R_ServiceGetRecordAsync(loParam);
                _DepartmentUser = R_FrontUtility.ConvertObjectToObject<GSM04100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetUserToAssignList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROGRAM_CODE, CPROGRAM_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, _DepartmentCode);
                var loResult = await _model.GetUserToAssignListAsync();
                _UsersToAssignList = new ObservableCollection<GSM04100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task AssignUserToDept(GSM04100DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROGRAM_CODE, CPROGRAM_CODE);
                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                _UserToAssign = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteUserDepartment(GSM04100StreamDTO poDept)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM04100DTO>(poDept);
                await _model.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
