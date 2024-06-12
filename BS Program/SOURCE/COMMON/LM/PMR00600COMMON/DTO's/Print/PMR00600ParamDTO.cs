using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PMR00600COMMON.DTO_s.Print
{
    public class PMR00600ParamDTO : PMR00600SPParamDTO
    {
        public string CPROPERTY_NAME { get; set; }

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

        public string CBUILDING_DISPLAY => CFROM_BUILDING_ID != CTO_BUILDING_ID
        ? $"{CFROM_BUILDING_NAME} ({CFROM_BUILDING_ID}) - {CTO_BUILDING_NAME} ({CTO_BUILDING_ID})"
        : $"{CFROM_BUILDING_NAME} ({CFROM_BUILDING_ID})";

        public string CDEPT_REPORT_DISPLAY => CFROM_DEPT_CODE != CTO_DEPT_CODE
        ? $"{CFROM_DEPT_NAME} ({CFROM_DEPT_CODE}) - {CTO_DEPT_NAME} ({CTO_DEPT_CODE})"
        : $"{CFROM_DEPT_NAME} ({CFROM_DEPT_CODE})";

        public string CTENANT_DISPLAY => CFROM_TENANT_ID != CTO_TENANT_ID
        ? $"{CFROM_TENANT_NAME} ({CFROM_TENANT_ID}) - {CTO_TENANT_NAME} ({CTO_TENANT_ID})"
        : $"{CFROM_TENANT_NAME} ({CFROM_TENANT_ID})";

        public bool LTENANT { get; set; }
        public bool LSERVICE { get; set; }
        public bool LSTATUS { get; set; }
        public bool LINVOICE { get; set; }
        public string CGRUP_BY { get; set; }
    }
}
