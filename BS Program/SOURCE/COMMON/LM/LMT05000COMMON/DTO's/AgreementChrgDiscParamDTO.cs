using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT05000COMMON.DTO_s
{
    public class AgreementChrgDiscParamDTO : AgreementChrgDiscHeaderDTO
    {
        public string CDISCOUNT_CODE { get; set; }
        public List<AgreementChrgDiscDetailDTO> AgreementChrgDiscDetail { get; set; }
    }

    public class AgreementChrgDiscResultDTO : R_APIResultBaseDTO
    {
    }
}
