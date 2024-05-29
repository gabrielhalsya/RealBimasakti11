using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00700Common.DTO
{
    public class GSM00720DTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CCASH_FLOW_GROUP_CODE { get; set; }
        
        public string CCYEAR { get; set; }
        public string CYEAR { get; set; }
        public string CPERIOD_NO { get; set; }
        public decimal NBASE_AMOUNT { get; set; }
        public decimal NLOCAL_AMOUNT { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CCASH_FLOW_CODE { get; set; }
        public string CCASH_FLOW_NAME { get; set; }
        public string CFROM_CASH_FLOW_FLAG { get; set; }


    }
    public class GSM00720ListDTO : R_APIResultBaseDTO
    {
        public List<GSM00720DTO> Data { get; set; }
    }
}
