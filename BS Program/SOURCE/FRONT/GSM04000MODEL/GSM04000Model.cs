using GSM04000Common;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSM04000Model
{
    public class GSM04000Model : R_BusinessObjectServiceClientBase<GSM04000DTO>, IGSM04000
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_CHECKPOINT_NAME = "api/GSM04000";
        private const string DEFAULT_MODULE = "GS";

        public GSM04000Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public IAsyncEnumerable<GSM04000DTO> GetGSM04000List()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GSM04000DTO>> GetGSM04000ListAsync()
        {
            var loEx = new R_Exception();
            List<GSM04000DTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM04000DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04000.GetGSM04000List),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GSM04000ActiveInactiveDTO RSP_GS_ACTIVE_INACTIVE_DEPTMethod()
        {
            throw new NotImplementedException();
        }

        public async Task RSP_GS_ACTIVE_INACTIVE_DEPTMethodAsync()
        {
            R_Exception loEx = new R_Exception();
            GSM04000ActiveInactiveDTO loRtn = new GSM04000ActiveInactiveDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GSM04000ActiveInactiveDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04000.RSP_GS_ACTIVE_INACTIVE_DEPTMethod),
                    DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

        }

        public GSM04000CheckUserExistResultDTO CheckIsUserDeptExist()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM04000CheckUserExistResultDTO> CheckIsUserDeptExistAsync()
        {
            R_Exception loEx = new R_Exception();
            GSM04000CheckUserExistResultDTO loRtn = new GSM04000CheckUserExistResultDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GSM04000CheckUserExistResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04000.CheckIsUserDeptExist),
                    DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            return loRtn;
        }

        public GSM04000DeleteAssignedUserWhenEveryoneTrueDTO DeleteDeptUserWhenChangingEveryone()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM04000DeleteAssignedUserWhenEveryoneTrueDTO> DeleteDeptUserWhenChaningEveryoneAsync()
        {
            R_Exception loEx = new R_Exception();
            GSM04000DeleteAssignedUserWhenEveryoneTrueDTO loRtn = new GSM04000DeleteAssignedUserWhenEveryoneTrueDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GSM04000DeleteAssignedUserWhenEveryoneTrueDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04000.DeleteDeptUserWhenChangingEveryone),
                    DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            return loRtn;
        }

        public UploadFileDTO DownloadTemplateDeptartment()
        {
            throw new NotImplementedException();
        }

        public async Task<UploadFileDTO> DownloadTemplateDeptartmentAsync()
        {
            var loEx = new R_Exception();
            UploadFileDTO loResult = new UploadFileDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<UploadFileDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04000.DownloadTemplateDeptartment),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

    }
}
