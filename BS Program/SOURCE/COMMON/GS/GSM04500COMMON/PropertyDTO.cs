using R_APICommonDTO;
using System.Collections.Generic;

namespace GSM04500Common
{
    public class PropertyDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CPROPERTY_ID { get; set; }

    }
    public class PropertyResult : R_APIResultBaseDTO
    {
        public List<PropertyDTO> data { get; set; }
    }


}
