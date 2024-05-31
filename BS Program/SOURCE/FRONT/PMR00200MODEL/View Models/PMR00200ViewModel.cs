using PMR00200COMMON;
using PMR00200COMMON.DTO_s;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PMR00200MODEL.View_Models
{
    public class PMR00200ViewModel
    {
        public PMR00200Model _PMR00200model = new PMR00200Model();
        public PMR00210Model _PMR00210model = new PMR00210Model();
        public ObservableCollection<PropertyDTO> _properties = new ObservableCollection<PropertyDTO>();
        public ObservableCollection<PeriodDtDTO> _fromPeriods = new ObservableCollection<PeriodDtDTO>();
        public ObservableCollection<PeriodDtDTO> _toPeriods = new ObservableCollection<PeriodDtDTO>();
        public PMR00200ParamDTO _ReportParam = new PMR00200ParamDTO();

        public List<ReportTypeDTO> _radioReportTypeList { get; set; } = new List<ReportTypeDTO>
        {
            new ReportTypeDTO { CTYPE = "S", CTYPE_NAME = "Summary" },
            new ReportTypeDTO { CTYPE= "D", CTYPE_NAME = "Detail" },
        };

        public DateTime _InitToday = new DateTime();
        public PeriodYearDTO _PeriodYear = new PeriodYearDTO();

        public int _YearFromPeriod = 0;
        public int _YearToPeriod = 0;
        public string _MonthFromPeriod = "";
        public string _MonthToPeriod = "";
        public string _ReportType = "";

        public async Task InitProcess()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _properties = new ObservableCollection<PropertyDTO>(await GetPropertyAsync());
                _InitToday = await GetTodayAsync();
                _PeriodYear = await GetPeriodYearAsync();

                //get period
                string loCurrentYearPeriod = _InitToday.ToString("yyyy");
                var loCurrentPeriods = await GetPeriodDtAsync(loCurrentYearPeriod);
                _fromPeriods = new ObservableCollection<PeriodDtDTO>(loCurrentPeriods);
                _toPeriods = new ObservableCollection<PeriodDtDTO>(loCurrentPeriods);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task<List<PropertyDTO>> GetPropertyAsync()
        {
            R_Exception loEx = new R_Exception();
            List<PropertyDTO> loRtn = null;
            try
            {
                var loResult = await _PMR00200model.GetPropertyListAsync();
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public async Task<DateTime> GetTodayAsync()
        {
            R_Exception loEx = new R_Exception();
            DateTime loRtn = new DateTime();
            try
            {
                var loData = await _PMR00200model.GetTodayDateAsync();
                loRtn = loData.DTODAY;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public async Task<List<PeriodDtDTO>> GetPeriodDtAsync(string pcYear)
        {
            R_Exception loEx = new R_Exception();
            List<PeriodDtDTO> loRtn = null;
            try
            {
                R_FrontContext.R_SetStreamingContext(PMR00200ContextConstant.CYEAR, pcYear);
                var loResult = await _PMR00200model.GetPeriodDtListAsync();
                loRtn = loResult.ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;

        }

        public async Task<PeriodYearDTO> GetPeriodYearAsync()
        {
            R_Exception loEx = new R_Exception();
            PeriodYearDTO loRtn = null;
            try
            {
                loRtn = await _PMR00200model.GetPeriodYearRecordAsync(new PeriodYearDTO() { CMODE = "", CYEAR = ""});
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

    }
}
