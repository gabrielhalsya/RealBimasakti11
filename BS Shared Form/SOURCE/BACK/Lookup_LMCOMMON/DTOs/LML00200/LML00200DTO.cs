using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs
{
    public class LML00200DTO
    {
        public string? CCHARGES_ID { get; set; }
        public string? CCHARGES_NAME { get; set; }
        public string? CCHARGES_TYPE { get; set; } = "";
        public string? CCHARGES_TYPE_DESCR { get; set; }  = "";
        public string? COTHER_TAX_ID { get; set; }  = "";
        public string? COTHER_TAX_NAME { get; set; }  = "";
        public decimal NOTHER_TAX_PCT { get; set; }
        public string? CUOM { get; set; } = "";

    }
}

