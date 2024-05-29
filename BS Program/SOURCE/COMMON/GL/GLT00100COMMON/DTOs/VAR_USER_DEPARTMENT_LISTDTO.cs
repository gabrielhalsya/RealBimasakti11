using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GLT00100Common.DTOs
{
    public class VAR_USER_DEPARTMENTDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CCENTER_CODE { get; set; }
        public string CCENTER_NAME { get; set; }
        public string CMANAGER_NAME { get; set; }
        public bool LEVERYONE { get; set; }
        public bool LACTIVE { get; set; }
        public string CACTIVE_BY { get; set; }
        public DateTime DACTIVE_DATE { get; set; }
        public string CINACTIVE_BY { get; set; }
        public DateTime DINACTIVE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }

    public class VAR_USER_DEPARTMENT_LISTDTO : R_APIResultBaseDTO
    {
        public List<VAR_USER_DEPARTMENTDTO> Data { get; set; }

    }
}
