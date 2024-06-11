using System;
using System.Collections.Generic;
using System.Text;

namespace PMT05000COMMON.DTO_s
{
    public class AgreementChrgDiscDetailDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CDISCOUNT_CODE { get; set; }
        public string CINV_PERIOD_YEAR { get; set; }
        public string CINV_PERIOD_MONTH { get; set; }
        public string LALL_BUILDING { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CAGREEMENT_TYPE { get; set; }
        public string CDISCOUNT_STATUS { get; set; }
        public string CPROCESS_BY { get; set; }
        public string CUPDATE_BY { get; set; }
        public string DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public string DCREATE_DATE { get; set; }
    }

    public class AgreementChrgDiscDetailBulkDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CREF_NO { get; set; }
        public string CSEQ_NO { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CUNIT_ID { get; set; }
        public string CTENANT_ID { get; set; }
        public decimal NCHARGES_AMOUNT { get; set; }
        public decimal NCHARGE_DISCOUNT { get; set; }
        public decimal NNET_CHARGE { get; set; }
        public string CAGREEMENT_NO { get; set; }
        public string CSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
        public string CEXISTING_DISCOUNT_CODE { get; set; }
        public decimal NEXISTING_CHARGE_DISCOUNT { get; set; }
    }
}
