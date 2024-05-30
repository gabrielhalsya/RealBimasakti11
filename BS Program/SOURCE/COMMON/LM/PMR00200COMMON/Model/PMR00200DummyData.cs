using BaseHeaderReportCOMMON;
using PMR00200COMMON.Print_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMR00200COMMON.Model
{
    public static class PMR00200DummyData
    {
        public static PMR00200PrintDisplayDTO DefaultData()
        {
            PMR00200PrintParamDTO printParamDTO = new PMR00200PrintParamDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = "",
                CFROM_DEPARTMENT_ID = "",
                CFROM_DEPARTMENT_NAME = "",
                CTO_DEPARTMENT_ID = "",
                CTO_DEPARTMENT_NAME = "",
                CFROM_SALESMAN_ID = "",
                CFROM_SALESMAN_NAME = "",
                CTO_SALESMAN_ID = "",
                CTO_SALESMAN_NAME = "",
                CFROM_PERIOD = "",
                CTO_PERIOD = "",
                LIS_OUTSTANDING = true,
            };
            PMR00200PrintDisplayDTO loData = new PMR00200PrintDisplayDTO()
            {
                Title = "LOI Status Summary",
                Header = "",
                HeaderParam = printParamDTO,
                Column = new PMR00200SummaryColumnDTO()
            };

            int countData1 = 2, countData2 = 2, countData3 = 3;
            List<PMR00200ResultPrintSPDTO> loCollData = new List<PMR00200ResultPrintSPDTO>();
            for (int a = 1; a <= countData1; a++)
            {
                for (int b = 1; b <= countData2; b++)
                {
                    for (int c = 1; c <= countData3; c++)
                    {
                        loCollData.Add(new PMR00200ResultPrintSPDTO()
                        {
                            CTRANS_NAME = $"Trans {a}",
                            CSALESMAN_ID = $"SM{b}",
                            CSALESMAN_NAME = $"Muhammad Andika Putra{b}",
                            CREF_NO = $"Ref00{c}",
                            CREF_DATE = $"{c} Jan 2024",
                            CTENURE = $"{c}Year(s), {c}Month(s), {c}Day(s)",
                            NTOTAL_GROSS_AREA_SIZE = 25,
                            NTOTAL_NET_AREA_SIZE = 22,
                            NTOTAL_COMMON_AREA_SIZE = 20,
                            CTRANS_STATUS_NAME = $"Onprogress",
                            NTOTAL_PRICE = 180000000,
                            CTAX = "PPN 11%",
                            CTENANT_ID = "MU001",
                            CTENANT_NAME = "Muhammad Andika Putra",
                            CTC_MESSAGE = "This is dummy data for Term & Con."
                        });
                    }
                }
            }

            var loTempData = loCollData.GroupBy(data1a => new
            {
                data1a.CTRANS_CODE,
                data1a.CTRANS_NAME,
            }).Select(data1b => new PMR00200DataResultDTO()
            {
                CTRANS_CODE = data1b.Key.CTRANS_CODE,
                CTRANS_NAME = data1b.Key.CTRANS_NAME,
                Detail1 = data1b.GroupBy(data2a => new
                {
                    data2a.CSALESMAN_ID,
                    data2a.CSALESMAN_NAME,

                }).Select(data2b => new PMR00200DataRes1()
                {
                    CSALESMAN_ID = data2b.Key.CSALESMAN_ID,
                    CSALESMAN_NAME = data2b.Key.CSALESMAN_NAME,
                    Detail2 = data2b.GroupBy(data2b => new
                    {
                        data2b.CREF_NO,
                        data2b.CREF_DATE,
                        data2b.CTENURE,
                        data2b.NTOTAL_GROSS_AREA_SIZE,
                        data2b.NTOTAL_NET_AREA_SIZE,
                        data2b.NTOTAL_COMMON_AREA_SIZE,
                        data2b.CTRANS_STATUS_NAME,
                        data2b.NTOTAL_PRICE,
                        data2b.CTAX,
                        data2b.CTENANT_ID,
                        data2b.CTENANT_NAME,
                        data2b.CTC_MESSAGE,
                    }).Select(data3b => new PMR00200DataRes2()
                    {
                        CREF_NO = data3b.Key.CREF_NO,
                        CREF_DATE = data3b.Key.CREF_DATE,
                        CTENURE = data3b.Key.CTENURE,
                        NTOTAL_GROSS_AREA_SIZE = data3b.Key.NTOTAL_GROSS_AREA_SIZE,
                        NTOTAL_NET_AREA_SIZE = data3b.Key.NTOTAL_NET_AREA_SIZE,
                        NTOTAL_COMMON_AREA_SIZE = data3b.Key.NTOTAL_COMMON_AREA_SIZE,
                        CTRANS_STATUS_NAME = data3b.Key.CTRANS_STATUS_NAME,
                        NTOTAL_PRICE = data3b.Key.NTOTAL_PRICE,
                        CTAX = data3b.Key.CTAX,
                        CTENANT_ID = data3b.Key.CTENANT_ID,
                        CTENANT_NAME = data3b.Key.CTENANT_NAME,
                        CTC_MESSAGE = data3b.Key.CTC_MESSAGE,
                    }).ToList()
                }).ToList()
            }).ToList();

            loData.Data = loTempData;
            return loData;
        }

        public static PMR00200PrintDislpayWithBaseHeaderDTO PMR00200PrintDislpayWithBaseHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "LOI Status",
                CUSER_ID = "GHC",
            };
            PMR00200PrintDislpayWithBaseHeaderDTO loRtn = new PMR00200PrintDislpayWithBaseHeaderDTO();

            loRtn.BaseHeaderData = loParam;
            loRtn.SummaryData = DefaultData();

            return loRtn;
        }

    }
}

