using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace GSM04500Common
{
    public interface IGSM04510GOA : R_IServiceCRUDBase<GOADTO>
    {
        IAsyncEnumerable<GOADTO> GetJournalGrpGOAList();
    }

}
