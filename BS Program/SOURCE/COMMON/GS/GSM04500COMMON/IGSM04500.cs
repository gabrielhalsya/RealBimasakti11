using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace GSM04500Common
{
    public interface IGSM04500 : R_IServiceCRUDBase<JournalDTO>
    {
        IAsyncEnumerable<JournalDTO> GetJournalGroupList();
        IAsyncEnumerable<PropertyDTO> GetPropertyList();
        GSM04500JournalGroupTypeListDTO GetAllJournalGroupTypeList();
    }

}
