using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00100Common.DTOs
{
    public class GLT00100ParameterDTO : R_APIResultBaseDTO
    {
        public string CSTATUS_NAME { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CPERIOD_YYYYMM { get; set; }
        public int CPERIOD_YYYY { get; set; }
        public string CPERIOD_MM { get; set; }
        public string CSTATUS { get; set; }
        public string CSEARCH_TEXT { get; set; }
        public string CLANGUAGE_ID { get; set; }
        public bool LSHOW_ALL { get; set; }
        public string CREC_ID { get; set; }
        public string CCURRENT_PERIOD_YY { get; set; }
        public string CCURRENT_PERIOD_MM { get; set; }
        public string CSOFT_PERIOD_YY { get; set; }
        public string CSOFT_PERIOD_MM { get; set; }
        public string CPERIOD { get; set; }
        public bool LCOMMIT_APRJRN { get; set; }
        public string CREF_NO { get; set; }
        public bool LUNDO_COMMIT { get; set; }
    }
}
