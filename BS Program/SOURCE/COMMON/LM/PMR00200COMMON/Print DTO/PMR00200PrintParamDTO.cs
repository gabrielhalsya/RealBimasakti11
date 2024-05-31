using PMR00200COMMON.DTO_s;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PMR00200COMMON
{
    public class PMR00200PrintParamDTO : PMR00200ParamDTO
    {
        public string CPROPERTY_NAME { get; set; }
        public string CDEPT_REPORT_DISPLAY => CFROM_DEPARTMENT_ID != CTO_DEPARTMENT_ID
        ? $"{CFROM_DEPARTMENT_NAME} ({CFROM_DEPARTMENT_ID}) - {CTO_DEPARTMENT_NAME} ({CTO_DEPARTMENT_ID})"
        : $"{CFROM_DEPARTMENT_NAME} ({CFROM_DEPARTMENT_ID})";
        public string CSALESMAN_REPORT_DISPLAY => CFROM_SALESMAN_ID != CTO_SALESMAN_ID
        ? $"{CFROM_SALESMAN_NAME} ({CFROM_SALESMAN_ID}) - {CTO_SALESMAN_NAME} ({CTO_SALESMAN_ID})"
        : $"{CFROM_SALESMAN_NAME} ({CFROM_SALESMAN_ID})";
        public string CPERIOD_DISPLAY
        {
            get
            {
                DateTime loFromDate = DateTime.ParseExact(CFROM_PERIOD, "yyyyMM", CultureInfo.InvariantCulture);
                DateTime loToDate = DateTime.ParseExact(CTO_PERIOD, "yyyyMM", CultureInfo.InvariantCulture);

                return (loFromDate.Year != loToDate.Year || loFromDate.Month != loToDate.Month)
                    ? $"{loFromDate:MMMM yyyy} – {loToDate:MMMM yyyy}"
                    : $"{loFromDate:MMMM yyyy}";
            }
        }
        public string CIS_OUTSTANDING
        {
            get
            {
                return LIS_OUTSTANDING ? "Yes" : "No";
            }
        }
    }
}