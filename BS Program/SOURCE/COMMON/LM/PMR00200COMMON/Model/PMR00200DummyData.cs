using BaseHeaderReportCOMMON;
using PMR00200COMMON.Print_DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PMR00200COMMON.Model
{
    public static class PMR00200DummyData
    {
        public static PMR00200PrintDislpayDTO PMR00200PrintDislpayWithBaseHeader()
        {
            PMR00200PrintDislpayDTO loRtn = new PMR00200PrintDislpayDTO();
            loRtn.BaseHeaderData = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "PMR00200",
                CPRINT_NAME = "LOI Status",
                CUSER_ID = "GHC",
            };
            loRtn.SummaryData = new PMR00200ReportDataDTO();
            loRtn.SummaryData.Header = "PM";
            loRtn.SummaryData.Title = "LOI Status";
            loRtn.SummaryData.Column = new PMR00200LabelDTO();
            loRtn.SummaryData.HeaderParam = new PMR00200ParamDTO()
            {
                CCOMPANY_ID = "RCD",
                CPROPERTY_ID = "Property id",
                CPROPERTY_NAME = "Property Name",
                CFROM_DEPARTMENT_ID = "D1",
                CFROM_DEPARTMENT_NAME = "Dept From",
                CTO_DEPARTMENT_ID = "D2",
                CTO_DEPARTMENT_NAME = "Dept To",
                CFROM_SALESMAN_ID = "S1",
                CFROM_SALESMAN_NAME = "SALESMAN From",
                CTO_SALESMAN_ID = "S1",
                CTO_SALESMAN_NAME = "SALESMAN To",
                CFROM_PERIOD = "202401",
                CTO_PERIOD = "202402",
                LIS_OUTSTANDING = true,
            };
            DateTime loFromDate = DateTime.ParseExact(loRtn.SummaryData.HeaderParam.CFROM_PERIOD, "yyyyMM", CultureInfo.InvariantCulture);
            DateTime loToDate = DateTime.ParseExact(loRtn.SummaryData.HeaderParam.CTO_PERIOD, "yyyyMM", CultureInfo.InvariantCulture);
            loRtn.SummaryData.HeaderParam.CPERIOD_DISPLAY = (loFromDate.Year != loToDate.Year || loFromDate.Month != loToDate.Month)
                ? $"{loFromDate:MMMM yyyy} – {loToDate:MMMM yyyy}"
                : $"{loFromDate:MMMM yyyy}";
            loRtn.SummaryData.Data = new List<PMR00200DataDTO>();

            int countData1 = 2, countData2 = 2, countData3 = 3;
            List<PMR00200DataDTO> loCollData = new List<PMR00200DataDTO>();
            for (int a = 1; a <= countData1; a++)
            {
                for (int b = 1; b <= countData2; b++)
                {
                    for (int c = 1; c <= countData3; c++)
                    {
                        loRtn.SummaryData.Data.Add(new PMR00200DataDTO()
                        {
                            CTRANS_NAME = $"Trans {a}",
                            CSALESMAN_ID = $"SM{b}",
                            CSALESMAN_NAME = $"Muhammad Andika Putra{b}",
                            CREF_NO = $"Ref00{c}",
                            CREF_DATE = $"{c} Jan 2024",
                            CTENURE = $"{c} Year(s), {c} Month(s), {c} Day(s)",
                            NTOTAL_GROSS_AREA_SIZE = 25,
                            NTOTAL_NET_AREA_SIZE = 22,
                            NTOTAL_COMMON_AREA_SIZE = 20,
                            CTRANS_STATUS_NAME = $"In Progress",
                            NTOTAL_PRICE = 180000000,
                            CTAX = "PPN 11%",
                            CTENANT_ID = "MU001",
                            CTENANT_NAME = "Muhammad Andika Putra",
                            CTC_MESSAGE = "This is dummy data for Term & Con."
                        });
                    }
                }
            }
            return loRtn;
        }

    }
}

