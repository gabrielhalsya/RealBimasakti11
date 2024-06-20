using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using PMR02400COMMON.DTO_s.Print;
using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace PMR02400COMMON.DTO_s.Model
{
    public static class PMR02400DummyData
    {
        public static PMR02400PrintDisplayDTO PMR02400PrintDislpayWithBaseHeader()
        {
            PMR02400PrintDisplayDTO loRtn = new PMR02400PrintDisplayDTO();

            loRtn.BaseHeaderData = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "Overtime",
                CUSER_ID = "GHC",
            };
            loRtn.ReportDataDTO = new PMR02400ReportDataDTO();
            loRtn.ReportDataDTO.Header = "PM";
            loRtn.ReportDataDTO.Title = "Penalty";
            loRtn.ReportDataDTO.Label = new PMR02400LabelDTO();
            loRtn.ReportDataDTO.Param = new PMR02400ParamDTO()
            {
                CCOMPANY_ID = "RCD",
                CPROPERTY_ID = "ProeprtyID",
                CPROPERTY_NAME = "Property Name",
                CREPORT_TYPE = "1",
                CREPORT_TYPE_DISPLAY = "Summary",
                LIS_BASED_ON_CUTOFF = true,
                CCUT_OFF = "",
                FROM_CPERIOD = "20240612",
                CTO_CPERIOD = "20240612",
                CFROM_CUSTOMER_ID = "BuildingID001",
                CFROM_CUSTOMER_NAME = "BuildingName001",
                CTO_CUSTOMER_ID = "BuildingID002",
                CTO_CUSTOMER_NAME = "BuildingName002",
                CLANG_ID = "en",
                CREPORT_CULTURE = "en",
                CUSER_ID = "ghc",
            };
            loRtn.ReportDataDTO.Data = new List<PMR02400DataDTO>();

            //setting based on display
            if (loRtn.ReportDataDTO.Param.LIS_BASED_ON_CUTOFF)
            {
                loRtn.ReportDataDTO.Param.CBASED_ON_DISPLAY = DateTime.TryParseExact(loRtn.ReportDataDTO.Param.CCUT_OFF, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime poCutOffDate)
                    ? $"{poCutOffDate:dd MMM yyyy}"
                    : "";
            }
            else
            {
                var fromPeriod = DateTime.TryParseExact(loRtn.ReportDataDTO.Param.FROM_CPERIOD + "01", "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate);
                var toPeriod = DateTime.TryParseExact(loRtn.ReportDataDTO.Param.CTO_CPERIOD + "01", "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime toDate);

                loRtn.ReportDataDTO.Param.CBASED_ON_DISPLAY = fromPeriod != toPeriod ? $"{fromDate:dd MMM yyyy} - {toDate:dd MMM yyyy}" : $"{fromDate:dd MMM yyyy}";
            }

            //generate dummya data
            string[] tenantIds = { "C1", "C2" };
            string[] tenantNames = { "PT Pasti Bisa ", "PT SEMANGAT" };
            string[] agreementNos = { "A", "A", "A" };
            string[] unitDescriptions = { "Unit 101", "Unit 102", "Unit 103" };
            string[] currencyCodes = { "IDR", "IDR" };
            string[] currencyNames = { "Rupiah", "Rupiah" };
            decimal[][] amounts = {
                new decimal[] { 100000000, 20000000, 50000000, 30000000 },
                new decimal[] { 150000000, 50000000, 50000000, 50000000 },
                new decimal[] { 200000000, 100000000, 50000000, 50000000 }
            };
            for (int tenantIndex = 0; tenantIndex < tenantIds.Length; tenantIndex++)
            {
                for (int agreementIndex = 0; agreementIndex < agreementNos.Length; agreementIndex++)
                {
                    loRtn.ReportDataDTO.Data.Add(new PMR02400DataDTO
                    {
                        CCOMPANY_ID = "C1",
                        CPROPERTY_ID = "P1",
                        CTENANT_ID = tenantIds[tenantIndex],
                        CTENANT_NAME = tenantNames[tenantIndex],
                        CAGREEMENT_NO = agreementNos[agreementIndex],
                        CUNIT_DESCRIPTION = unitDescriptions[agreementIndex],
                        CBUILDING_ID = "B1",
                        CBUILDING_NAME = "Building One",
                        CCURRENCY_CODE = currencyCodes[tenantIndex],
                        CCURRENCY_NAME = currencyNames[tenantIndex],
                        NINVOICE_AMOUNT = amounts[agreementIndex][0],
                        NREDEEMED_AMOUNT = amounts[agreementIndex][1],
                        NPAID_AMOUNT = amounts[agreementIndex][2],
                        NOUTSTANDING_AMOUNT = amounts[agreementIndex][3]
                    });
                }
            }
            return loRtn;

        }
    }
}