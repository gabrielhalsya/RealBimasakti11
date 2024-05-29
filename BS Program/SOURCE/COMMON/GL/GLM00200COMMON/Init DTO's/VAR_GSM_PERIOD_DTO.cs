using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLM00200Common
{
    public class VAR_GSM_PERIOD_DTO
    {

        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }
    }

    public class PeriodFrontDTO
    {
        public string CPERIOD_MM_CODE { get; set; }
        public string CPERIOD_MM_TEXT { get; set; }
    }
}
