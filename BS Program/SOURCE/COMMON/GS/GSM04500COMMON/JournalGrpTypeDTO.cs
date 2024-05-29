using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM04500Common
{
    public class JournalGrpTypeDTO
    {
        public string CCODE { get; set; }
        public string CDESCRIPTION { get; set; }
    }
    public class GSM04500JournalGroupTypeListDTO : R_APIResultBaseDTO
    {
        public List<JournalGrpTypeDTO> Data { get; set; }
    }
}
