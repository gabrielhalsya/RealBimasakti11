using System;
using System.Collections.Generic;
using System.Text;
using GSM00700Common.DTO;
using R_CommonFrontBackAPI;

namespace GSM00700Common
{
    public interface IGSM00720 : R_IServiceCRUDBase<GSM00720DTO>

    {
        //GSM00720ListDTO GetAllCashFlowPlan();

        IAsyncEnumerable<GSM00720DTO> GetAllCashFlowPlanStream();
        IAsyncEnumerable<GSM00720YearDTO> GetYearStream();

        //GSM00720YearListDTO GetYearList();
        GSM00720CopyFromYearListDTO GetCopyFromYearList(GSM00700ParameterDTO poParamDto);
        GSM00720CopyBaseAmountListDTO GetCopyBaseAmountList(GSM00700ParameterDTO poParamDto);
        GSM00720CopyLocalAmountListDTO GetCopyLocalAmountList(GSM00700ParameterDTO poParamDto);
        GSM00720CurrencyDTO GetCurrencyList();

        GSM00720InitialProsesListDTO GetInitialProses();

        GSM00710TemplateCashFlowUserInterface GetTemplate();
        GSM00720TemplateCashFlowPlan GetTemplate720();

    }
}
