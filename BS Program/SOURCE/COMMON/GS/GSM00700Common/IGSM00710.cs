using GSM00700Common.DTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00700Common
{
    public interface IGSM00710 : R_IServiceCRUDBase<GSM00710DTO>
    {
        //GSM00710ListDTO GetAllCashFlowList();
        GSM00710CashFlowTypeListDTO GetListCashFlowType();

        IAsyncEnumerable<GSM00710DTO> GetAllCashFlowStream();
    }
}
