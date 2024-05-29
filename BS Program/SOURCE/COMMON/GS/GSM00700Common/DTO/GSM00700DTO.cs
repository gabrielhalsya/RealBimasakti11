using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00700Common.DTO
{
    public class GSM00700DTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }

        public string CCASH_FLOW_GROUP_CODE { get; set; }
        public string CCASH_FLOW_GROUP_NAME { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }

        //this for combo box
        public string CCASH_FLOW_GROUP_TYPE_DESCR { get; set; }
        public string CCASH_FLOW_GROUP_TYPE { get; set; }  

        public string CCASH_FLOW_GROUP_FROM { get; set;}
        public string CCASH_FLOW_GROUP_TO { get; set; }
        public string CYEAR_FROM { get; set; }
        public string CYEAR_TO { get; set; }
        public string CPERIOD_FROM { get; set; }
        public string CPERIOD_TO { get; set; }
        public string CSHORT_BY { get; set; }
        public bool LPRINT_LOCAL { get; set; }
        public bool LPRINT_BASE { get; set; }
        public string CUSER_LOGIN_ID { get; set; }
        public string CCYEAR { get; set; }
        public string CYEAR { get; set; }
        public string CCASH_FLOW_CODE { get; set; }
        public string CCASH_FLOW_NAME { get; set; }
        public string CCASH_FLOW_TYPE { get; set; }
        public string CCASH_FLOW_TYPE_DESCR { get; set; }
        public string CPERIOD_NO { get; set; }
        public decimal NLOCAL_AMOUNT { get; set; }
        public decimal NBASE_AMOUNT { get; set; }
    }
    public class GSM00700ListDTO : R_APIResultBaseDTO
    {
        public List<GSM00700DTO> Data { get; set; }

    }
}
