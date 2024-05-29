using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM04000Common
{
    public class GSM04100ListDTO : R_APIResultBaseDTO
    {
        public List<GSM04100StreamDTO> Data { get; set; }
    }
}
