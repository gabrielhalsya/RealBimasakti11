using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00100Common.DTOs
{
    public class GLT00100JournalGridDTO 
    {
        public bool LSELECTED { get; set; }
        public string CSEARCH_TEXT { get; set; }
        public string CPERIOD { get; set; }
        public DateTime DREVERSE_DATE { get; set; }
        public string CSOFT_PERIOD_MM { get; set; }
        public int ISOFT_PERIOD_YY { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CREVERSE_DATE { get; set; }

        public string CREF_DATE { get; set; }
        public DateTime DREF_DATE { get; set; }
        public string CREF_PRD { get; set; }
        public string CREC_ID { get; set; }
        public bool LALLOW_APPROVE { get; set; }
        public string CNEXT_PRD { get; set; }
        public string CSTATUS { get; set; }
        public string CREF_NO { get; set; }
        public int IFREQUENCY { get; set; }
        public int IPERIOD { get; set; }
        public string CSTART_DATE { get; set; }
        public string CNEXT_DATE { get; set; }
        public string CTRANS_DESC { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public string CSTATUS_NAME { get; set; }
        public string CDOC_NO { get; set; }
        public DateTime DDOC_NO { get; set; }
        public string CDOC_DATE { get; set; }
        public DateTime DDOC_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public bool LCOMMIT_APRJRN { get; set; }
        public bool LUNDO_COMMIT { get; set; }
    }

    public class GLT00100JournalGridListDTO : R_APIResultBaseDTO
    {
        public List<GLT00100JournalGridDTO> Data { get; set; }

    }
}
