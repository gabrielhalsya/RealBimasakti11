using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00700Common.DTO.Report_DTO_GSM00700
{
    public class GSM00700PrintCashFlowColoumnDTO
    {
        public string COLOUMN_CODE { get; set; } = "Code";
        public string COLOUMN_NAME { get; set; } = "Name";
        public string COLOUMN_TYPE { get; set; } = "Type";
        public string COLOUMN_PLAN { get; set; } = "Plan";
        public string COLOUMN_YEAR { get; set; } = "Year";
        public string COLOUMN_PERIOD { get; set; } = "Period";
        public string COLOUMN_LOCAL_AMOUNT { get; set; } = "Local Amount";
        public string COLOUMN_BASE_AMOUNT { get; set; } = "Base Amount";
    }
}
