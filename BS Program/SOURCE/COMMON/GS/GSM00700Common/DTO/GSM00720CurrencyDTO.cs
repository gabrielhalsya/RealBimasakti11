using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GSM00700Common.DTO
{
    public class GSM00720CurrencyDTO : R_APIResultBaseDTO
    {
        public string CLOCAL_CURRENCY_CODE { get; set; }
        public string CBASE_CURRENCY_CODE { get; set; }
    }

    public class GSM00720CurrencyListDTO : R_APIResultBaseDTO
    {
        public List<GSM00720CurrencyDTO> Data { get; set; }
    }
}
