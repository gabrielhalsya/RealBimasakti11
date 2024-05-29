using R_APICommonDTO;
using System;
using System.Collections.Generic;

namespace GSM04500Common
{
    public class JournalDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_ID { get; set; }

        public string CJRNGRP_TYPE { get; set; }
        public string CJRNGRP_CODE { get; set; }
        public string CJRNGRP_NAME { get; set; }
        public bool LACCRUAL { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string ErrorMessage { get; set; }

    }
    public class GSM04500ListDTO : R_APIResultBaseDTO
    {
        public List<JournalDTO> ListData { get; set; }
    }


}
