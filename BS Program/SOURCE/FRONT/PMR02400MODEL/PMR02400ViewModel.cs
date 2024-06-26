﻿using PMR02400COMMON;
using PMR02400COMMON.DTO_s;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Interfaces;
using PMR02400FrontResources;


namespace PMR02400MODEL
{
    public class PMR02400ViewModel
    {
        public PMR02400Model _PMR02400model = new PMR02400Model();
        public ObservableCollection<PropertyDTO> _properties = new ObservableCollection<PropertyDTO>();
        public PMR02400ParamDTO _ReportParam = new PMR02400ParamDTO();
        public List<ReportTypeDTO> _radioReportTypeList { get; set; } = new List<ReportTypeDTO>();
        public List<GeneralTypeDTO> _radioCutOffList { get; set; } = new List<GeneralTypeDTO>();
        public List<GeneralTypeDTO> _radioPeriodList { get; set; } = new List<GeneralTypeDTO>();
        public List<MonthDTO> _monthList { get; set; } = Enumerable.Range(1, 12).Select(i => new MonthDTO
        {
            CTYPE_CODE = i.ToString("D2"),
            CTYPE_NAME = DateTimeFormatInfo.InvariantInfo.GetMonthName(i)
        }).ToList();

        public DateTime _InitToday = new DateTime();
        public PeriodYearDTO _PeriodYear = new PeriodYearDTO();
        public int _YearFromPeriod { get; set; } = 0;
        public int _YearToPeriod { get; set; } = 0;
        public string _MonthFromPeriod { get; set; } = "";
        public string _MonthToPeriod { get; set; } = "";
        public string _Report_Type { get; set; } = "";
        public string _DateBasedOn { get; set; } = "";
        public DateTime _DateCutOff { get; set; } = DateTime.Now;

        public async Task InitProcess(R_ILocalizer<Resources_Dummy_Class> poParamLocalizer)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                //generate type for radio
                _radioReportTypeList = new List<ReportTypeDTO> {
                    new ReportTypeDTO { CTYPE_CODE = "1", CTYPE_NAME = poParamLocalizer["_radio_Summary"] },
                    new ReportTypeDTO { CTYPE_CODE = "2", CTYPE_NAME = poParamLocalizer ["_radio_Detail"] },
                };

                _radioCutOffList = new List<GeneralTypeDTO>
                {
                    new GeneralTypeDTO { CTYPE_CODE = "1", CTYPE_NAME = poParamLocalizer["_radio_CutOff"] },
                };

                _radioPeriodList = new List<GeneralTypeDTO>
                {
                    new GeneralTypeDTO { CTYPE_CODE = "2", CTYPE_NAME = poParamLocalizer["_radio_Period"] },
                };


                //get init data
                _properties = new ObservableCollection<PropertyDTO>(await GetPropertyAsync());
                if (_properties.Count > 0)
                {
                    _ReportParam.CPROPERTY_ID = _properties.FirstOrDefault().CPROPERTY_ID;
                    _ReportParam.CPROPERTY_NAME = _properties.FirstOrDefault().CPROPERTY_NAME;
                }

                _InitToday = await GetTodayAsync();
                _PeriodYear = await GetPeriodYearAsync();
                if (_InitToday == null)
                {
                    _DateCutOff = _InitToday;
                    _InitToday = DateTime.Now;
                }
                //set default data
                _MonthFromPeriod = _InitToday.ToString("MM");
                _MonthToPeriod = _InitToday.ToString("MM");
                _YearToPeriod = _InitToday.Year;
                _YearFromPeriod = _InitToday.Year;

                _ReportParam.CREPORT_TYPE = "1";
                _DateBasedOn = "1";

                _ReportParam.CFROM_CUSTOMER_ID = "";
                _ReportParam.CFROM_CUSTOMER_NAME = "";
                _ReportParam.CTO_CUSTOMER_ID = "";
                _ReportParam.CTO_CUSTOMER_NAME = "";
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
                var loResult = await _PMR02400model.GetPropertyListAsync();
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
                var loData = await _PMR02400model.GetTodayDateAsync();
                loRtn = loData.DTODAY;
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
                loRtn = await _PMR02400model.GetPeriodYearRecordAsync(new PeriodYearDTO() { CMODE = "", CYEAR = "" });
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
