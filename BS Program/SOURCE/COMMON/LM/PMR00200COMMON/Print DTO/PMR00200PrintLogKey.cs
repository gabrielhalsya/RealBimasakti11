using R_CommonFrontBackAPI.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00200COMMON.Print_DTO
{
    public class PMR00200PrintLogKey
    {
        public PMR00200PrintParamDTO poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
    }
}
