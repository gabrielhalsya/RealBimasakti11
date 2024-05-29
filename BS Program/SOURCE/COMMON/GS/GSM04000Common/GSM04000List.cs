using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM04000Common
{
    public class GSM04000List<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}
