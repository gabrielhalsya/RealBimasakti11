using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GLT00100Common;
using GLT00100Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;
using static GLT00100Common.DTOs.VAR_GL_SYSTEM_PARAMDTO;

namespace GLT00100Model
{
    public class GLT00100Model : R_BusinessObjectServiceClientBase<GLT00100DTO>, IGLT00100
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GLT00100";
        private const string DEFAULT_MODULE = "GL";
        public GLT00100Model() :
            base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }

        public async Task<GLT00100JournalGridListDTO> GetJournalListAsync(string lcPeriod, string lcSearch, string lcDeptCode, string lcStatus)
        {
            R_Exception loEx = new R_Exception();
            GLT00100JournalGridListDTO loResult = new GLT00100JournalGridListDTO();
            try
            {
                R_FrontContext.R_SetContext(ContextConstant.CPERIOD, lcPeriod);
                R_FrontContext.R_SetContext(ContextConstant.CSEARCH_TEXT, lcSearch);
                R_FrontContext.R_SetContext(ContextConstant.CDEPT_CODE, lcDeptCode);
                R_FrontContext.R_SetContext(ContextConstant.CSTATUS, lcStatus);
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loResTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLT00100JournalGridDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100.GetJournalList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = loResTemp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<GLT00100JournalGridDetailListDTO> GetAllJournalDetailListAsync(string lcCrecId)
        {
            R_Exception loEx = new R_Exception();
            GLT00100JournalGridDetailListDTO loResult = new GLT00100JournalGridDetailListDTO();
            try
            {
                R_FrontContext.R_SetContext(ContextConstant.CREC_ID, lcCrecId);
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loResTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLT00100JournalGridDetailDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100.GetAllJournalDetailList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = loResTemp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        

        public async Task ProcessReversingJournalAsync(GLT00100JournalGridDTO poData)
        {
            R_Exception loEx = new R_Exception();
            GLT00100JournalGridListDTO loResult = new GLT00100JournalGridListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLT00100JournalGridListDTO, GLT00100JournalGridDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100.ProcessReversingJournal),
                    poData,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        public async Task JournalProcessAsync(GLT00100JournalGridDTO poData)
        {
            R_Exception loEx = new R_Exception();
            GLT00100JournalGridListDTO loResult = new GLT00100JournalGridListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLT00100JournalGridListDTO, GLT00100JournalGridDTO>(
                _RequestServiceEndPoint,
                nameof(IGLT00100.JournalProcesStatus),
                poData,
                DEFAULT_MODULE,
                _SendWithContext,
                _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        public async Task UndoReversingJournalProcessAsync(GLT00100JournalGridDTO poData)
        {
            R_Exception loEx = new R_Exception();
            GLT00100JournalGridListDTO loResult = new GLT00100JournalGridListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLT00100JournalGridListDTO, GLT00100JournalGridDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100.UndoReversingJournal),
                    poData,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }

        #region InitialProcess
        public async Task<VAR_GSM_COMPANYDTO> GetCompanyDTOAsync()
        {
            var loEx = new R_Exception();
            VAR_GSM_COMPANYDTO loResult = new VAR_GSM_COMPANYDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<VAR_GSM_COMPANYDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100.GetCompanyDTO),
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
        public async Task<StatusListDTO> GetStatusListAsync()
        {
            var loEx = new R_Exception();
            StatusListDTO loResult = new StatusListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<StatusListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100.GetStatusList),
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
        public async Task<CurrencyCodeListDTO> GetCurrencyCodeListAsync()
        {
            var loEx = new R_Exception();
            CurrencyCodeListDTO loResult = new CurrencyCodeListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<CurrencyCodeListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100.GetCurrencyCodeList),
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
        public async Task<GetCenterListDTO> GetCenterListAsync()
        {
            var loEx = new R_Exception();
            GetCenterListDTO loResult = new GetCenterListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GetCenterListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100.GetCenterList),
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
        
        public async Task<VAR_GL_SYSTEM_PARAMDTO> GetSystemParamAsync()
        {
            var loEx = new R_Exception();
            VAR_GL_SYSTEM_PARAMDTO loResult = new VAR_GL_SYSTEM_PARAMDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<VAR_GL_SYSTEM_PARAMDTO>(
                   _RequestServiceEndPoint,
                   nameof(IGLT00100.GetSystemParam),
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
        public async Task<VAR_USER_DEPARTMENT_LISTDTO> GetDeptListAsync()
        {
            var loEx = new R_Exception();
            VAR_USER_DEPARTMENT_LISTDTO loResult = new VAR_USER_DEPARTMENT_LISTDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<VAR_USER_DEPARTMENT_LISTDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100.GetDeptList),
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
        public async Task<VAR_CCURRENT_PERIOD_START_DATEDTO> GetCurrentPeriodStartDateAsync(VAR_GL_SYSTEM_PARAMDTO poData)
        {
            var loEx = new R_Exception();
            VAR_CCURRENT_PERIOD_START_DATEDTO loResult = new VAR_CCURRENT_PERIOD_START_DATEDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<VAR_CCURRENT_PERIOD_START_DATEDTO, VAR_GL_SYSTEM_PARAMDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100.GetCurrentPeriodStartDate),
                    poData,
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
        public async Task<VAR_CSOFT_PERIOD_START_DATEDTO> GetSoftPeriodStartDateAsync(VAR_GL_SYSTEM_PARAMDTO poData)
        {
            var loEx = new R_Exception();
            VAR_CSOFT_PERIOD_START_DATEDTO loResult = new VAR_CSOFT_PERIOD_START_DATEDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<VAR_CSOFT_PERIOD_START_DATEDTO, VAR_GL_SYSTEM_PARAMDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100.GetSoftPeriodStartDate),
                    poData,
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
        public async Task<VAR_IUNDO_COMMIT_JRNDTO> GetIOptionAsync()
        {
            var loEx = new R_Exception();
            VAR_IUNDO_COMMIT_JRNDTO loResult = new VAR_IUNDO_COMMIT_JRNDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<VAR_IUNDO_COMMIT_JRNDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100.GetIOption),
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
        public async Task<VAR_GSM_TRANSACTION_CODEDTO> GetLincementLapprovalAsync()
        {
            var loEx = new R_Exception();
            VAR_GSM_TRANSACTION_CODEDTO loResult = new VAR_GSM_TRANSACTION_CODEDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<VAR_GSM_TRANSACTION_CODEDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100.GetLincementLapproval),
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
        public async Task<VAR_GSM_PERIODDTO> GetPeriodAsync()
        {
            var loEx = new R_Exception();
            VAR_GSM_PERIODDTO loResult = new VAR_GSM_PERIODDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<VAR_GSM_PERIODDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100.GetPeriod),
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
        public async Task<GSM_TRANSACTION_APPROVALDTO> GetTransactionApprovalAsync()
        {
            var loEx = new R_Exception();
            GSM_TRANSACTION_APPROVALDTO loResult = new GSM_TRANSACTION_APPROVALDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM_TRANSACTION_APPROVALDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLT00100.GetTransactionApproval),
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
        #endregion

        #region NotImplementedException
        public IAsyncEnumerable<GLT00100JournalGridDTO> GetJournalList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<GLT00100JournalGridDetailDTO> GetAllJournalDetailList()
        {
            throw new NotImplementedException();
        }
        public VAR_GSM_COMPANYDTO GetCompanyDTO()
        {
            throw new NotImplementedException();
        }
        public VAR_GL_SYSTEM_PARAMDTO GetSystemParam()
        {
            throw new NotImplementedException();
        }
        public VAR_USER_DEPARTMENT_LISTDTO GetDeptList()
        {
            throw new NotImplementedException();
        }
        public VAR_CCURRENT_PERIOD_START_DATEDTO GetCurrentPeriodStartDate(VAR_GL_SYSTEM_PARAMDTO poData)
        {
            throw new NotImplementedException();
        }
        public VAR_CSOFT_PERIOD_START_DATEDTO GetSoftPeriodStartDate(VAR_GL_SYSTEM_PARAMDTO poData)
        {
            throw new NotImplementedException();
        }
        public VAR_IUNDO_COMMIT_JRNDTO GetIOption()
        {
            throw new NotImplementedException();
        }
        public VAR_GSM_TRANSACTION_CODEDTO GetLincementLapproval()
        {
            throw new NotImplementedException();
        }
        public VAR_GSM_PERIODDTO GetPeriod()
        {
            throw new NotImplementedException();
        }
        public StatusListDTO GetStatusList()
        {
            throw new NotImplementedException();
        }
        public CurrencyCodeListDTO GetCurrencyCodeList()
        {
            throw new NotImplementedException();
        }

        public GetCenterListDTO GetCenterList()
        {
            throw new NotImplementedException();
        }

        public GSM_TRANSACTION_APPROVALDTO GetTransactionApproval()
        {
            throw new NotImplementedException();
        }

        public GLT00100JournalGridDTO JournalProcesStatus(GLT00100JournalGridDTO poData)
        {
            throw new NotImplementedException();
        }
        public GLT00100JournalGridDTO ProcessReversingJournal(GLT00100JournalGridDTO poData)
        {
            throw new NotImplementedException();
        }
        public GLT00100JournalGridDTO UndoReversingJournal(GLT00100JournalGridDTO poData)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
