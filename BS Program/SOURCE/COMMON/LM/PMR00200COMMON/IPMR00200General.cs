using PMR00200COMMON.DTO_s;
using System;
using System.Collections.Generic;

namespace PMR00200COMMON
{
    public interface IPMR00200General
    {
        PMR00200Result<TodayDTO> GetTodayDate();
        IAsyncEnumerable<PropertyDTO> GetPropertyList();
        IAsyncEnumerable<PeriodDtDTO> GetPeriodList();
        PMR00200Result<PeriodYearDTO> GetPeriodYearRecord(PeriodYearDTO poParam);
    }
}
