using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM04500Common
{
    public class ExcelDTO
    {
        public string JournalGroup { get; set; }
        public string JournalGroupName { get; set; }
        public bool EnableAccrual { get; set; }
        public string Notes { get; set; }

    }


    public class ExcelValidateDTO :ExcelDTO
    {
        public int No { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorFlag { get; set; } = "Y";
    }

    public class TempTableDTO :ExcelDTO
    {
        public int No { get; set; }

    }

    public class GSM04500ListUploadErrorValidateDTO : R_APIResultBaseDTO
    {
        public List<ExcelValidateDTO> Data { get; set; }
    }
}
