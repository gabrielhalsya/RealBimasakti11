using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM00700Common.DTO.Report_DTO_GSM00700;
using R_CommonFrontBackAPI.Log;

namespace GSM00700Service.DTO
{
    public class GSM00700LogPrintDTO
    {
        public GSM00700PrintCashFlowParameterDTo poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }

    }
}
