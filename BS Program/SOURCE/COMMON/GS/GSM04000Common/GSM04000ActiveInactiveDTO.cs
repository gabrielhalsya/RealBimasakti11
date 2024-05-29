using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace GSM04000Common
{
    public class GSM04000ActiveInactiveDTO : R_APIResultBaseDTO
    {
    }

    public class GSM04000ActiveInactiveParam
    {
        public string CCOMPANY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public bool LACTIVE { get; set; }
        public string CUSER_ID { get; set; }
    }

}
