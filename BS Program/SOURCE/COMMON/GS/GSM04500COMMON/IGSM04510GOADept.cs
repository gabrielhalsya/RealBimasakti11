using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace GSM04500Common
{
    public interface IGSM04510GOADept : R_IServiceCRUDBase<GOADeptDTO>
    {
        IAsyncEnumerable<GOADeptDTO> GetJournalGrpGOADeptList();
    }

}
