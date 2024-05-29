using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using BaseHeaderReportCOMMON;
using GSM00700Common.DTO.Report_DTO_GSM00700;

namespace GSM00700Common.Model
{
    public static class GSM00700ModelDummyData
    {
        public static GSM00700PrintCashFlowResultDTo DefaultData()
        {
            GSM00700PrintCashFlowResultDTo loData = new GSM00700PrintCashFlowResultDTo()
            {
                Title = "Cash Flow",    
                Header = "UHUY",
                Column = new GSM00700PrintCashFlowColoumnDTO()
            };

            int Data1 = 4;
            int Data2 = 3;
            int Data3 = 3;

            List<GSM00700PrintCashFlowDTO> loCollection = new List<GSM00700PrintCashFlowDTO>();
            for (int a = 1; a < Data1; a++)
            {
               
                for (int b = 1; b < Data2; b++)
                {

                    for (int c = 1; c < Data3; c++)

                    {
                        loCollection.Add(new GSM00700PrintCashFlowDTO()
                        {
                            CCASH_FLOW_GROUP_CODE = $"Cash Flow Group {a}",
                            CCASH_FLOW_GROUP_NAME = $"Cash Flow Group Name{a}",
                            CCASH_FLOW_GROUP_TYPE = $"Cash Flow Group Type {a}",
                            CCASH_FLOW_GROUP_TYPE_DESCR = $"Cash Flow Group Type {a}",

                            CCASH_FLOW_CODE = $"Cash Flow Code {b}",
                            CCASH_FLOW_NAME = $"Cash Flow Name {b}",
                            CCASH_FLOW_TYPE = $"Cash Flow Type {b}",
                            CCASH_FLOW_TYPE_DESCR = $"Cash Flow Type {b}",
                            CCYEAR = $"Year {b}",

                            CPERIOD_NO = $"Period {c}",
                            NLOCAL_AMOUNT = 0.08m + (c * 0.02m),
                            NBASE_AMOUNT = 0.08m + (c * 0.02m)
                        });
                    }
                }
            }

            var loTempData = loCollection
                .GroupBy(data1a => new
                {
                    data1a.CCASH_FLOW_GROUP_CODE,
                    data1a.CCASH_FLOW_GROUP_NAME,
                    data1a.CCASH_FLOW_GROUP_TYPE,
                    data1a.CCASH_FLOW_GROUP_TYPE_DESCR, 
                })
                .Select(data1b => new GSM00700Data()
                {
                    CCASH_FLOW_GROUP_CODE = data1b.Key.CCASH_FLOW_GROUP_CODE,
                    CCASH_FLOW_GROUP_NAME = data1b.Key.CCASH_FLOW_GROUP_NAME,
                    CCASH_FLOW_GROUP_TYPE = data1b.Key.CCASH_FLOW_GROUP_TYPE,
                    CCASH_FLOW_GROUP_TYPE_DESCR = data1b.Key.CCASH_FLOW_GROUP_TYPE_DESCR,

                    GSM00710Data = data1b.GroupBy(data2a => new
                    {
                        data2a.CCYEAR,
                        data2a.CCASH_FLOW_CODE,
                        data2a.CCASH_FLOW_NAME,
                        data2a.CCASH_FLOW_TYPE,
                        data2a.CCASH_FLOW_TYPE_DESCR,

                    }).Select(data2b => new GSM00710Data()
                    {
                        CYEAR = data2b.Key.CCYEAR,
                        CCASH_FLOW_CODE = data2b.Key.CCASH_FLOW_CODE,
                        CCASH_FLOW_NAME = data2b.Key.CCASH_FLOW_NAME,
                        CCASH_FLOW_TYPE = data2b.Key.CCASH_FLOW_TYPE,
                        CCASH_FLOW_TYPE_DESCR = data2b.Key.CCASH_FLOW_TYPE_DESCR,
                        GSM00720Data = data2b.GroupBy(data3a => new
                        {
                            data3a.CPERIOD_NO,
                            data3a.NLOCAL_AMOUNT,
                            data3a.NBASE_AMOUNT,
                            data3a.CCYEAR,

                        }).Select(data3b => new GSM00720Data()
                        {
                            CPERIOD_NO = data3b.Key.CPERIOD_NO,
                            NLOCAL_AMOUNT = data3b.Key.NLOCAL_AMOUNT,
                            NBASE_AMOUNT = data3b.Key.NBASE_AMOUNT,
                            CYEAR = data3b.Key.CCYEAR,
                        }).ToList()
                    }).ToList()
                }).ToList();

            loData.Data = loTempData;
            return loData;
        }



    public static GSM00700PrintCashFlowResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
    {
        
        var loParam = new BaseHeaderDTO()
        {
            CCOMPANY_NAME = "PT Realta ssss",
            CPRINT_CODE = "001",
            CPRINT_NAME = "Unit Charges",
            CUSER_ID = "KIWKIW"
        };
        GSM00700PrintCashFlowResultWithBaseHeaderPrintDTO loRtn = new GSM00700PrintCashFlowResultWithBaseHeaderPrintDTO();
        loRtn.BaseHeaderData = loParam;
        loRtn.CenterData = DefaultData();
        return loRtn;

    }
}
}

