using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00700Common.DTO.Upload_DTO_GSM00720
{
    public class GSM00720UploadDTO
    {
        //result
        public int NO { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CCASHFLOW_GROUP_CODE { get; set; }
        public string CCASHFLOW_GROUP_NAME { get; set; }
        public string CCASH_FLOW_CODE { get; set; }
        public string CCASH_FLOW_NAME { get; set; }
        public string CCYEAR { get; set; }
        public string CPERIOD_NO { get; set; }
        public string NLOCAL_AMOUNT { get; set; }
        public string NBASE_AMOUNT { get; set; }
        public string ErrorMessage { get; set; }
        public bool Var_Exists { get; set; }
        public bool Var_Overwrite { get; set; }
    }

    public class GSM00720UploadExcelDTO
    {
        public string PeriodNo { get; set; }
        public string LocalAmount { get; set; }
        public string BaseAmount { get; set; }
        public string Notes { get; set; }
        public string ErrorMessage { get; set; }

    }

    public class GSM00720UploadRequestDTO
    {
        public int NO { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CCASHFLOW_GROUP_CODE { get; set; }
        public string CCASH_FLOW_CODE { get; set; }
        public string CCYEAR { get; set; }
        public string CPERIOD_NO { get; set; }
        public string NLOCAL_AMOUNT { get; set; }
        public string NBASE_AMOUNT { get; set; }
    }

    public class GSM00720UploadErrorValidateDTO
    {
        public int NO { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CCASHFLOW_GROUP_CODE { get; set; }
        public string CCASH_FLOW_CODE { get; set; }
        public string CCYEAR { get; set; }
        public string CPERIOD_NO { get; set; }
        public string NLOCAL_AMOUNT { get; set; }
        public string NBASE_AMOUNT { get; set; }
        public bool ErrorFlag { get; set; }
        public string ErrorMessage { get; set; }
        public bool Var_Exists { get; set; }
        public bool Var_Overwrite { get; set; }
    }

    public class GSM00720UploadErrorValidateParamDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CKEY_GUID { get; set; }
    }
}
