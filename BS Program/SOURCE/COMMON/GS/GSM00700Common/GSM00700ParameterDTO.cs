using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00700Common
{
    public class GSM00700ParameterDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CCASH_FLOW_GROUP_CODE { get; set; }
        public string CUSER_ID { get; set; }
        public string CCASH_FLOW_CODE { get; set; }
        public string CCYEAR { get; set; }

        public string CFROM_CASH_FOW_FLAG { get; set; }
        public string CFROM_CASH_FLOW_CODE { get; set; }
        public string CFROM_YEAR { get; set; }
        public string CTO_CASH_FLOW_CODE { get; set; }
        public string CTO_YEAR { get; set; }
        public string CUSER_LOGIN_ID { get; set; }


        public string CCASH_FLOW_GROUP { get; set; }
        public string CYEAR { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CCURRENCY_RATE { get; set; }
        public Int32 INO_PERIOD_FROM { get; set; }
        public Int32 INO_PERIOD_TO { get; set; }


        public string CCASH_FLOW_GROUP_FROM { get; set; }
        public string CCASH_FLOW_GROUP_TO { get; set; }
        public string CYEAR_FROM { get; set; }
        public string CYEAR_TO { get; set; }
        public string CPERIOD_FROM { get; set; }
        public string CPERIOD_TO { get; set; }
        public string CSHORT_BY { get; set; }
        public bool LPRINT_LOCAL { get; set; }
        public bool LPRINT_BASE { get; set; }


    }

    public class GSM00700PrintDTO
    {
        public string CCASH_FLOW_GROUP_FROM { get; set; }
        public string CCASH_FLOW_GROUP_TO { get; set; }
        public string CYEAR_FROM { get; set; }
        public string CYEAR_TO { get; set; }
        public string CPERIOD_FROM { get; set; }
        public string CPERIOD_TO { get; set; }
        public string CSHORT_BY { get; set; } = "01";
        public bool LPRINT_LOCAL { get; set; } = true;
        public bool LPRINT_BASE { get; set; } = true;
    }
}
