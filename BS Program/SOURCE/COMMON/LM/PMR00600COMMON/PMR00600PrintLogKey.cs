using PMR00600COMMON.DTO_s.Print;
using PMR00600COMMON.DTO_s.PrintDetail;
using R_CommonFrontBackAPI.Log;

namespace PMR00600COMMON
{
    public class PMR00600PrintLogKey
    {
        public PMR00600ParamDTO poParamSummary { get; set; }
        public PMR00601ParamDTO poParamDetail{ get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
    }
}
