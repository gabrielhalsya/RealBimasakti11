
using GLM00200Common;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GLM00200Model
{
    public class GLM00200ViewModel : R_ViewModel<JournalParamDTO>
    {
        private GLM00200Model _model = new GLM00200Model();
        
        private PublicLookupModel _lookupGSModel = new PublicLookupModel();
        public RecurringJournalListParamDTO Parameter { get; set; } = new RecurringJournalListParamDTO();
        public ObservableCollection<JournalDTO> JournalGrid { get; set; } = new ObservableCollection<JournalDTO>();
        public JournalDTO Journal { get; set; } = new JournalDTO();
        public ObservableCollection<JournalDetailGridDTO> JournaDetailGrid { get; set; } = new ObservableCollection<JournalDetailGridDTO>();
        public VAR_IUNDO_COMMIT_JRN_DTO IUNDO_COMMIT_JRN { get; set; } = new VAR_IUNDO_COMMIT_JRN_DTO();
        public VAR_GSM_COMPANY_DTO GSM_COMPANY { get; set; } = new VAR_GSM_COMPANY_DTO();
        public VAR_GL_SYSTEM_PARAM_DTO GL_SYSTEM_PARAM { get; set; } = new VAR_GL_SYSTEM_PARAM_DTO();
        public VAR_GSM_PERIOD_DTO GSM_PERIOD { get; set; } = new VAR_GSM_PERIOD_DTO();
        public VAR_GSM_TRANSACTION_CODE_DTO GSM_TRANSACTION_CODE { get; set; } = new VAR_GSM_TRANSACTION_CODE_DTO();
        public VAR_PERIOD_DT_INFO_DTO CSOFT_PERIOD_START_DATE { get; set; } = new VAR_PERIOD_DT_INFO_DTO();
        public List<VAR_STATUS_DTO> STATUS_LIST { get; set; } = new List<VAR_STATUS_DTO>();
        #region ComboBox ViewModel
        public List<KeyValuePair<string, string>> PeriodMonthList { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("01", "01"),
            new KeyValuePair<string, string>("02", "02"),
            new KeyValuePair<string, string>("03", "03"),
            new KeyValuePair<string, string>("04", "04"),
            new KeyValuePair<string, string>("05", "05"),
            new KeyValuePair<string, string>("06", "06"),
            new KeyValuePair<string, string>("07", "07"),
            new KeyValuePair<string, string>("08", "08"),
            new KeyValuePair<string, string>("09", "09"),
            new KeyValuePair<string, string>("10", "10"),
            new KeyValuePair<string, string>("11", "11"),
            new KeyValuePair<string, string>("12", "12")
        };
        #endregion

        #region Init
        public async Task GetInitData()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult =  await _model.GetInitDataAsync();
                loResult.STATUS_LIST.Add(new VAR_STATUS_DTO() { CCODE = "", CNAME = "All" });

                STATUS_LIST = loResult.STATUS_LIST;
                IUNDO_COMMIT_JRN = loResult.IUNDO_COMMIT_JRN;
                GSM_COMPANY = loResult.COMPANY_INFO;
                GL_SYSTEM_PARAM = loResult.GL_SYSTEM_PARAM;
                GSM_PERIOD = loResult.PERIOD_YEAR;
                GSM_TRANSACTION_CODE = loResult.GSM_TRANSACTION_CODE;
                CSOFT_PERIOD_START_DATE = loResult.SOFT_PERIOD_START_DATE;


                var loLookupDept = await _lookupGSModel.GSL00700GetDepartmentListAsync(new GSL00700ParameterDTO());
                var loDept = loLookupDept.FirstOrDefault();
                Parameter.CDEPT_CODE = loDept.CDEPT_CODE;
                Parameter.CDEPT_NAME = loDept.CDEPT_NAME;

                Parameter.CPERIOD_YYYY = int.Parse(loResult.GL_SYSTEM_PARAM.CSOFT_PERIOD_YY);
                Parameter.CPERIOD_MM = loResult.GL_SYSTEM_PARAM.CSTART_PERIOD_MM;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion


        #region GRID Journal
        public async Task ShowAllJournals()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                Parameter.CPERIOD_YYYYMM = Parameter.CPERIOD_YYYY + Parameter.CPERIOD_MM;
                var loResult = await _model.GetAllRecurringListAsync(Parameter);

                foreach (var loItem in loResult)
                {
                    if (DateTime.TryParseExact(loItem.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime pdParsedStartDate))
                    {
                        loItem.DSTART_DATE = pdParsedStartDate;
                    }

                    if (DateTime.TryParseExact(loItem.CNEXT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime pdParsedNextDate))
                    {
                        loItem.DNEXT_DATE = pdParsedNextDate;
                    }

                }
                
                JournalGrid = new ObservableCollection<JournalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task ShowAllJournalDetail(JournalDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetAllJournalDetailListAsync(poParam);

                JournaDetailGrid = new ObservableCollection<JournalDetailGridDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region template
        public async Task<UploadByte> DownloadTemplate()
        {
            var loEx = new R_Exception();
            UploadByte loResult = null;

            try
            {
                loResult = await _model.DownloadTemplateAsync();
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
