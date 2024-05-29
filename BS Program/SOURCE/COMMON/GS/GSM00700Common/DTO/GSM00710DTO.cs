using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GSM00700Common.DTO
{
    public class GSM00710DTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; } 
        public string CUSER_ID { get; set; }

        public string CSEQUENCE { get; set; }
        public string CCASH_FLOW_CODE { get; set; }
        public string CCASH_FLOW_NAME { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CCASH_FLOW_GROUP_CODE { get; set; }
        public string CCASH_FLOW_GROUP_NAME { get; set; }

        //this for combo box

        public string CCASH_FLOW_TYPE_DESCR { get; set; }
        public string CCASH_FLOW_TYPE { get; set; }
    }
    public class GSM00710ListDTO : R_APIResultBaseDTO
    {
        public List<GSM00710DTO> Data { get; set; }
    }
}
