using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00100Common.DTOs
{
    public class PrintParamDTO
    {
        public string CCOMPANY_ID { get; set; }

        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }

        public string CFROM_DEPARTMENT_ID { get; set; }
        public string CFROM_DEPARTMENT_NAME { get; set; }


        public string CTO_DEPARTMENT_ID { get; set; }
        public string CTO_DEPARTMENT_NAME { get; set; }

        public string CFROM_SALESMAN_ID { get; set; }
        public string CFROM_SALESMAN_NAME { get; set; }

        public string CTO_SALESMAN_ID { get; set; }
        public string CTO_SALESMAN_NAME { get; set; }

        public string CFROM_PERIOD { get; set; }
        public string CTO_PERIOD { get; set; }

        public string CFROM_PERIOD_NAME { get; set; }
        public string CTO_PERIOD_NAME { get; set; }

        public string CLANG_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }

        public string CSALESMAN_ID { get; set; }
        public string CSALESMAN_NAME { get; set; }
    }
}
