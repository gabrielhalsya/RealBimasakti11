using PMR00200COMMON;
using PMR00200COMMON.DTO_s;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMR00200MODEL
{
    public class PMR00210Model : R_BusinessObjectServiceClientBase<PMR00200DTO>, IPMR00200
    {
        

        public PMR00210Model(string pcHttpClientName = PMR00200ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = PMR00200ContextConstant.DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                PMR00200ContextConstant.DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public IAsyncEnumerable<PMR00200DTO> GetReportData()
        {
            throw new NotImplementedException();
        }
        public async Task<List<PMR00200DTO>> GetReportDataAsync()
        {
            var loEx = new R_Exception();
            List<PMR00200DTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = PMR00200ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMR00200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00200.GetReportData),
                    PMR00200ContextConstant.DEFAULT_MODULE, _SendWithContext,
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
