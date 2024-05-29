using GLM00200Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLM00200Common
{
    public class InitALLDTO
    {
        public DateTime DTODAY { get; set; }
        public VAR_GSM_COMPANY_DTO COMPANY_INFO { get; set; }
        public VAR_GL_SYSTEM_PARAM_DTO GL_SYSTEM_PARAM { get; set; }

        public VAR_PERIOD_DT_INFO_DTO CURRENT_PERIOD_START_DATE { get; set; }
        public VAR_PERIOD_DT_INFO_DTO SOFT_PERIOD_START_DATE { get; set; }

        public VAR_IUNDO_COMMIT_JRN_DTO IUNDO_COMMIT_JRN { get; set; }
        public VAR_GSM_TRANSACTION_CODE_DTO GSM_TRANSACTION_CODE { get; set; }

        public List<VAR_CURRENCY_DTO> CURRENCY_LIST { get; set; }
        public List<VAR_STATUS_DTO> STATUS_LIST { get; set; }

        public VAR_GSM_PERIOD_DTO PERIOD_YEAR { get; set; }
    }
}
