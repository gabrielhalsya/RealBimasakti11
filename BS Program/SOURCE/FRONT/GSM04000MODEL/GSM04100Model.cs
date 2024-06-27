using GSM04000Common;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSM04000Model
{
    public class GSM04100Model : R_BusinessObjectServiceClientBase<GSM04100DTO>, IGSM04100
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_CHECKPOINT_NAME = "api/GSM04100";
        private const string DEFAULT_MODULE = "GS";


        public GSM04100Model(
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

        #region GetDepartmentUserList
        public GSM04100ListDTO GetUserDeptListByDeptCode()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM04100ListDTO> GetGSM04100ListByDeptCodeAsync()
        {
                var loEx = new R_Exception();
                GSM04100ListDTO loResult = null;

                try
                {
                    loResult= new GSM04100ListDTO();
                    R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                    loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM04100ListDTO>(
                        _RequestServiceEndPoint,
                        nameof(IGSM04100.GetUserDeptListByDeptCode),
                        DEFAULT_MODULE, _SendWithContext,
                        _SendWithToken);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }
                loEx.ThrowExceptionIfErrors();
                return loResult;
        }

        #endregion

        #region GetUserListToAssign
        public IAsyncEnumerable<GSM04100DTO> GetUserToAssignList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GSM04100DTO>> GetUserToAssignListAsync()
        {
            var loEx = new R_Exception();
            List<GSM04100DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM04100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04100.GetUserToAssignList),
                    DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
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
