using PMM07500COMMON;
using PMM07500COMMON.DTO_s;
using PMM07500COMMON.DTO_s.stamp_code;
using PMM07500COMMON.Interfaces;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM07500MODEL
{
    public class PMM07500InitModel : R_BusinessObjectServiceClientBase<PMM07500GridDTO>, IPMM07500General
    {
        public PMM07500InitModel(string pcHttpClientName = PMM07500ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = PMM07500ContextConstant.DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                PMM07500ContextConstant.DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public IAsyncEnumerable<CurrencyDTO> GetCurrencyList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<CurrencyDTO>> GetCurrencyListAsync()
        {
            var loEx = new R_Exception();
            List<CurrencyDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = PMM07500ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CurrencyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM07500General.GetCurrencyList),
                    PMM07500ContextConstant.DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<List<PropertyDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PropertyDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = PMM07500ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM07500General.GetPropertyList),
                    PMM07500ContextConstant.DEFAULT_MODULE, _SendWithContext,
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
