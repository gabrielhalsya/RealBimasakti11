using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00700Common.DTO.Report_DTO_GSM00700
{
    public class GSM00700Data
    {
        public string CCASH_FLOW_GROUP_CODE { get; set; }
        public string CCASH_FLOW_GROUP_NAME { get; set; }
        public string CCASH_FLOW_GROUP_TYPE { get; set; }
        public string CCASH_FLOW_GROUP_TYPE_DESCR { get; set; }

        public List<GSM00710Data> GSM00710Data { get; set; }
    }

    public class GSM00710Data
    {
        public string CYEAR { get; set; }
        public string CCASH_FLOW_CODE { get; set; }
        public string CCASH_FLOW_NAME { get; set; }
        public string CCASH_FLOW_TYPE { get; set; }
        public string CCASH_FLOW_TYPE_DESCR { get; set; }

        public List<GSM00720Data> GSM00720Data { get; set; }
    }
    public class GSM00720Data
    {
        public string CPERIOD_NO { get; set; }
        public decimal NLOCAL_AMOUNT { get; set; }
        public decimal NBASE_AMOUNT { get; set; }
        public string CYEAR { get; set; }
    }
}
