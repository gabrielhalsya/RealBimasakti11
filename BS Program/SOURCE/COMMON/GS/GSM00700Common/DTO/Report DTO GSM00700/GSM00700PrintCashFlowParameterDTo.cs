using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00700Common.DTO.Report_DTO_GSM00700
{
    public class GSM00700PrintCashFlowParameterDTo
    {
        public string CCASH_FLOW_GROUP_FROM { get; set; } = "";
        public string CUSER_LOGIN_ID { get; set; }
        public string CCASH_FLOW_GROUP_FROM_NAME { get; set; }
        public string CCASH_FLOW_GROUP_TO { get; set; } = "";
        public string CCASH_FLOW_GROUP_TO_NAME { get; set; }
        public string CYEAR_FROM { get; set; } = "";
        public string CYEAR_TO { get; set; } = "";
        public string CPERIOD_FROM { get; set; } = "";
        public string CPERIOD_TO { get; set; } = "";
        public string CSHORT_BY { get; set; } = "01";
        public bool LPRINT_LOCAL { get; set; }
        public bool LPRINT_BASE { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string Period { get; set; }
        public string SortBy { get; set; }

        public int YearFrom { get; set; } = 0;
        public int YearTo { get; set; } = 0;
        public int PeriodFrom { get; set; } = 0;
        public int PeriodTo { get; set; } = 0;
    }
}
