using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GLT00100Common.DTOs
{
    public class CurrencyCodeDTO
    {
        public string CCURRENCY_CODE { get; set; }
        public string CCURRENCY_NAME { get; set; }
    }

    public class CurrencyCodeListDTO : R_APIResultBaseDTO
    {
        public List<CurrencyCodeDTO> Data { get; set; }
    }
}
