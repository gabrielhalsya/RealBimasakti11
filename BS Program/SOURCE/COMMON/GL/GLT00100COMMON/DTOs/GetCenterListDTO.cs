using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00100Common.DTOs
{
    public class GetCenterDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CCENTER_CODE { get; set; }
        public string CCENTER_NAME { get; set; }
        public string LACTIVE { get; set; }
        public string CCREATE_BY { get; set; }
        public string DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public string DUPDATE_DATE { get; set; }
    }
    public class GetCenterListDTO : R_APIResultBaseDTO
    {
        public List<GetCenterDTO> Data { get; set; }
    }
}
