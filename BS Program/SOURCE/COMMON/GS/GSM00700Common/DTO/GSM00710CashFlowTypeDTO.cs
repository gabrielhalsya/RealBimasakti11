using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GSM00700Common.DTO
{
    public class GSM00710CashFlowTypeDTO : R_APIResultBaseDTO
    {
        public string CCODE { get; set; }
        public string CDESCRIPTION { get; set; }

    }
    public class GSM00710CashFlowTypeListDTO : R_APIResultBaseDTO
    {
        public List<GSM00710CashFlowTypeDTO> Data { get; set; }
    }
}
