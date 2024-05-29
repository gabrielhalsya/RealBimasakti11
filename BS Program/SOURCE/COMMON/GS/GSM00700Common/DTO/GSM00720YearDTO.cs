using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GSM00700Common.DTO
{
    public class GSM00720YearDTO : R_APIResultBaseDTO
    {
        public string CYEAR { get; set; }

    }

    public class GSM00720YearListDTO : R_APIResultBaseDTO
    {
        public List<GSM00720YearDTO> Data { get; set; }
    }
}
