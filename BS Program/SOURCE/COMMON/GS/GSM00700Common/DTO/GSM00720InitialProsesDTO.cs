using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GSM00700Common.DTO
{
    public class GSM00720InitialProsesDTO : R_APIResultBaseDTO
    {
        //public string CBASE_CURRENCY_CODE { get; set; }
        //public string CLOCAL_CURRENCY_CODE { get; set; }
        public Int32 NUM { get; set; } = 0;
    }

    public class GSM00720InitialProsesListDTO : R_APIResultBaseDTO
    {
        public List<GSM00720InitialProsesDTO> Data { get; set; }
    }
}
