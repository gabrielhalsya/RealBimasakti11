using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM04000Common
{
    public class GSM04000ExcelGridDTO : GSM04000DTO

    {
        public int INO { get; set; }
        public string CVALID { get; set; }
        public string CNOTES { get; set; }
        public string CNON_ACTIVE_DATE{ get; set; } //YYYYMMDD
        public DateTime DNON_ACTIVE_DATE_DISPLAY{ get; set; } //YYYYMMDD
    }

    public class GSM04000ListExcelGridDTO : R_APIResultBaseDTO
    {
        public List<GSM04000ExcelGridDTO> Data { get; set; }   
    }
    

}
