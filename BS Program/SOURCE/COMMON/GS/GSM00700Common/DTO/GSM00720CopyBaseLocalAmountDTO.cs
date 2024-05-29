using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GSM00700Common.DTO
{
    public class GSM00720CopyBaseLocalAmountDTO :  R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CCASH_FLOW_CODE { get; set; }
        public string CCASH_FLOW_GROUP { get; set; }
        public string CYEAR { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CCURENCY_RATE { get; set; }
        public Int32 INO_PERIOD_FROM { get; set; }
        public Int32 INO_PERIOD_TO { get; set; }
        public string CCASH_FLOW_NAME { get; set; }
        public Int32 Code { get; set; }
        public string Desc { get; set; }
    }

    public class GSM00720CopyBaseAmountListDTO : R_APIResultBaseDTO
    {
        public List<GSM00720CopyBaseLocalAmountDTO> Data { get; set; }
    }

    public class GSM00720CopyLocalAmountListDTO : R_APIResultBaseDTO
    {
        public List<GSM00720CopyBaseLocalAmountDTO> Data { get; set; }
    }
}
    