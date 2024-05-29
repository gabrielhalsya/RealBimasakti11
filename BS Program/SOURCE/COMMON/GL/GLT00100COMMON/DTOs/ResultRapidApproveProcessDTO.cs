using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00100Common.DTOs
{
    public class ResultRapidApproveProcessDTO
    {
        public bool LSUCCESSED { get; set; }
        public string CREC_ID { get; set; }
    }

    public class ResultRapidApproveProcessListDTO : R_APIResultBaseDTO
    {
        public List<ResultRapidApproveProcessDTO> Data { get; set; }
    }
}
