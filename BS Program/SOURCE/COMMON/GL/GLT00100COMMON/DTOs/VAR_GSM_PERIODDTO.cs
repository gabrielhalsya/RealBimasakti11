using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00100Common.DTOs
{
    public class VAR_GSM_PERIODDTO : R_APIResultBaseDTO
    {
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }
    }
}
