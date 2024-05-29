using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00100Common.DTOs
{
    public class StatusDTO
    {
        public string CCODE { get; set; }
        public string CNAME { get; set; }
    }

    public class StatusListDTO : R_APIResultBaseDTO
    {
        public List<StatusDTO> Data { get; set; }

    }
}
