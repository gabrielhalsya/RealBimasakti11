using System;

namespace PMR02400COMMON
{
    public class PMR02401SPResultDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CAGREEMENT_NO { get; set; }
        public string CUNIT_DESCRIPTION { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CINVOICE_NO { get; set; }
        public string CINVOICE_DESCRIPTION { get; set; }
        public string CDUE_DATE { get; set; }
        public string ILATE_DAYS { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public string NINVOICE_AMOUNT { get; set; }
        public string NREDEEMED_AMOUNT { get; set; }
        public string NPAID_AMOUNT { get; set; }
        public string NOUTSTANDING_AMOUNT { get; set; }
    }
}
