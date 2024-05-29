using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GLT00100Common.DTOs
{
    public class GLT00100DTO
    {
        public string CCREATE_DATE { get; set; }
        public string CUPDATE_DATE { get; set; }
        public string CSOFT_PERIOD_YY { get; set; }
        public string NTRANS_AMOUNT { get; set; }
        public bool LUNDO_COMMIT { get; set; }
        public bool LALLOW_APPROVE { get; set; }
        public string CSTATUS_NAME { get; set; }
        public string CREVERSE_DATE { get; set; }
        public DateTime DREVERSE_DATE { get; set; }
        public decimal NCREDIT_AMOUNT { get; set; }
        public decimal NDEBIT_AMOUNT { get; set; }
        public string CSOFT_PERIOD_MM { get; set; }
        public int ISOFT_PERIOD_YY { get; set; }
        public string CUSER_ID { get; set; }
        public string CJRN_ID { get; set; }
        public string CACTION { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CDEPT_CODE { get; set; }

        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime DREF_DATE { get; set; }
        public string CDOC_NO { get; set; }
        public string CDOC_DATE { get; set; }
        public DateTime DDOC_DATE { get; set; }
        public int IFREQUENCY { get; set; }
        public int IAPPLIED { get; set; }
        public int IPERIOD { get; set; }
        public string CSTART_DATE { get; set; }
        public string CNEXT_DATE { get; set; }
        public string CLAST_DATE { get; set; }
        public string CTRANS_DESC { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public bool LFIX_RATE { get; set; }
        public Decimal NLBASE_RATE { get; set; }
        public Decimal NLCURRENCY_RATE { get; set; }
        public Decimal NBBASE_RATE { get; set; }
        public Decimal NBCURRENCY_RATE { get; set; }
        public Decimal NPRELIST_AMOUNT { get; set; }
        public Decimal NNTRANS_AMOUNT_C { get; set; }
        public Decimal NNTRANS_AMOUNT_D { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CLANGUAGE_ID { get; set; }

        public string CDEPT_NAME { get; set; }
        public int IYEAR { get; set; }
        public string CMONTH { get; set; }
        public string CPERIOD { get; set; }
        public string CSEARCH_TEXT { get; set; }
        public string CSTATUS { get; set; } = "";
        public string CREC_ID { get; set; } = "";
        public bool LCOMMIT_APRJRN { get; set; }
        public List<GLT00100JournalGridDetailDTO> DetailList { get; set; }
        public bool LREVERSE { get; set; }
    }
}
