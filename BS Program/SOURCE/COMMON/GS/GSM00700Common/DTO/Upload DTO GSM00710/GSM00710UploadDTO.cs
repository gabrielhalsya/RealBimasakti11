using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00710Common.DTO.Upload_DTO_GSM00710
{
    public class GSM00710UploadDTO
    {
        //result
        public int NO { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CCASHFLOW_GROUP_CODE { get; set; }
        public string CCASHFLOW_GROUP_NAME { get; set; }
        public string CSEQ { get; set; }
        public string CCASHFLOW_CODE { get; set; }
        public string CCASH_FLOW_NAME { get; set; }
        public string CCASHFLOW_TYPE { get; set; }  
        public string ErrorMessage { get; set; }
        public bool Var_Exists { get; set; }
        public bool Var_Overwrite { get; set; }
    }

    public class GSM00710UploadExcelDTO
    {
        public string DisplaySeq { get; set; }
        public string CashFlowCode { get; set; }
        public string CashFlowName { get; set; }
        public string CashFlowType { get; set; }
        public string Notes { get; set; }
        public string ErrorMessage { get; set; }

    }

    public class GSM00710UploadRequestDTO
    {
        public int NO { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CCASHFLOW_GROUP_CODE { get; set; }
        public string CSEQ { get; set; }
        public string CCASHFLOW_CODE { get; set; }
        public string CCASH_FLOW_NAME { get; set; }
        public string CCASHFLOW_TYPE { get; set; }
    }

    public class GSM00710UploadErrorValidateDTO
    {
        public int NO { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CCASHFLOW_GROUP_CODE { get; set; }
        public string CSEQ { get; set; }
        public string CCASHFLOW_CODE { get; set; }
        public string CCASH_FLOW_NAME { get; set; }
        public string CCASHFLOW_TYPE { get; set; }
        public string ErrorMessage { get; set; }
        public bool ErrorFlag { get; set; }
        public bool Var_Exists { get; set; }
        public bool Var_Overwrite { get; set; }
    }

    public class GSM00710UploadErrorValidateParamDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CKEY_GUID { get; set; }
    }
}
