using System;
using System.Collections.Generic;
using System.Text;
using GSM00700Common.DTO;
using GSM00700Common.DTO.Report_DTO_GSM00700;
using R_CommonFrontBackAPI;

namespace GSM00700Common
{
    public interface IGSM00700 : R_IServiceCRUDBase<GSM00700DTO>
    {
        IAsyncEnumerable<GSM00700DTO> GetAllCashFlowGroupStream();

        //GSM00700ListDTO GetAllCashFlowGroupList();
        GSM00700CashFlowGroupTypeListDTO GetListCashFlowGroupType();
        IAsyncEnumerable<GSM00700DTO> GetPrintCashFlow(GSM00700PrintCashFlowParameterDTo poParameter);

        IAsyncEnumerable<GSM00700DTO> GetYearFromPrint();
        IAsyncEnumerable<GSM00700DTO> GetYearToPrint();
    }
}
