using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GSM00700Common.DTO
{
    public class GSM00700CashFlowGroupTypeDTO  : R_APIResultBaseDTO
    {
        public string CCODE { get; set; }
        public string CCASH_FLOW_GROUP_TYPE { get; set; }
        public string CDESCRIPTION { get; set; }
        
    }

    public class GSM00700CashFlowGroupTypeListDTO : R_APIResultBaseDTO

    {
        public List<GSM00700CashFlowGroupTypeDTO> Data { get; set; }

    }
}
