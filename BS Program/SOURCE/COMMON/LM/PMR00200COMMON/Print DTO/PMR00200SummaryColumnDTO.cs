using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00200COMMON.Print_DTO
{
    public class PMR00200SummaryColumnDTO
    {
        public string COLUMN_CTRANS_NAME { get; set; } = "Transaction Name";
        public string COLUMN_CSALESMAN_ID_NAME { get; set; } = "Salesman";
        public string COLUMN_CREF_NO { get; set; } = "LOI No.";
        public string COLUMN_CREF_DATE { get; set; } = "LOI Date";
        public string COLUMN_CTENURE { get; set; } = "Tenure";
        public string COLUMN_GRAND_TOT_AREA { get; set; } = "Grand Total Area";
        public string COLUMN_NTOTAL_GROSS_AREA_SIZE { get; set; } = "Gross";
        public string COLUMN_NTOTAL_NET_AREA_SIZE { get; set; } = "Net";
        public string COLUMN_NTOTAL_COMMON_AREA_SIZE { get; set; } = "Common";
        public string COLUMN_CTRANS_STATUS_NAME { get; set; } = "Transaction Status";
        public string COLUMN_NTOTAL_PRICE { get; set; } = "Total Rent Price";
        public string COLUMN_CTAX { get; set; } = "Tax";
        public string COLUMN_CTENANT_NAME { get; set; } = "Tenant";
        public string COLUMN_CTC_MESSAGE { get; set; } = "Term & Condition";
        public string LABEL_CPROPERTY_NAME { get; set; } = "Property";
        public string LABEL_CDEPARTMENT_NAME { get; set; } = "Department";
        public string LABEL_CSALESMAN_NAME { get; set; } = "Salesman";
        public string LABEL_CPERIOD { get; set; } = "Period";
        public string LABEL_LIS_OUTSTANDING { get; set; } = "Outstanding Agreement Only";
        public string LABEL_TOTAL_AGGR_BASED_ON { get; set; } = "Total Agreement Based On";
        public string LABEL_GRAND_TOTAL_AGGR { get; set; } = "Grand Total of Agreements";
    }
}
