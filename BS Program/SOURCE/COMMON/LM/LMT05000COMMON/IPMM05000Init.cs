using PMT05000COMMON.DTO_s;
using System.Collections.Generic;

namespace PMT05000COMMON
{
    public interface IPMM05000Init
    {
        IAsyncEnumerable<PropertyDTO> GetProperty();
        IAsyncEnumerable<GSB_CodeInfoDTO> GetGSBCodeInfo();
        IAsyncEnumerable<GSPeriodDT_DTO> GetGSPeriodDT();
    }
}
