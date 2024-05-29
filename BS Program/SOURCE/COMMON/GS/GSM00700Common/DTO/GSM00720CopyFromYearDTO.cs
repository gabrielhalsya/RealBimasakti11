using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GSM00700Common.DTO
{
    public class GSM00720CopyFromYearDTO : R_APIResultBaseDTO
    {

        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CFROM_CASH_FLOW_FLAG { get; set; } = "00";
        public string CFROM_CASH_FLOW_CODE { get; set; } 
        public string CFROMGOUP { get; set; }
        public string CFROM_YEAR { get; set; }
        public string CTO_CASH_FLOW_CODE { get; set; }
        public string CTO_YEAR { get; set; }
        public string CUSER_LOGIN_ID { get; set; }
        public string Desc { get; set; }
        public string Code { get; set; }
        public string CashFlowName { get; set; }

    }

    public class GSM00720CopyFromYearListDTO : R_APIResultBaseDTO
    {
        public List<GSM00720CopyFromYearDTO> Data { get; set; }
    }
}
