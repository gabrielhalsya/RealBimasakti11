using GLM00200Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace GLM00200Common
{
    public interface IGLM00200 
    {
        RecordResultDTO<InitALLDTO> GetInitData();
        RecordResultDTO<JournalDTO> GetJournalData(JournalDTO poEntity);
        RecordResultDTO<JournalDTO> SaveJournalData(ParemeterRecordWithCRUDModeResultDTO<JournalParamDTO> poEntity);
        RecordResultDTO<GLM00200UpdateStatusDTO> UpdateStatusJournalData(GLM00200UpdateStatusDTO poEntity);
        RecordResultDTO<CurrencyRateResult> GetLastCurrency(CurrencyRateResult poEntity);

        IAsyncEnumerable<JournalDTO> GetAllRecurringList();
        IAsyncEnumerable<JournalDetailGridDTO> GetAllJournalDetailList();
        IAsyncEnumerable<JournalDetailActualGridDTO> GetAllActualJournalDetailList();

        UploadByte DownloadTemplate();
        //IAsyncEnumerable<JournalGridDTO> GetFilteredRecurringList();
    }
}
