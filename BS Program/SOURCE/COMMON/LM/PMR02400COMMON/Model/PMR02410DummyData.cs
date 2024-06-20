using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using PMR02400COMMON.DTO_s.Print;
using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace PMR02400COMMON.DTO_s.Model
{
    public static class PMR02410DummyData
    {
        public static PMR02410PrintDisplayDTO PMR02410PrintDislpayWithBaseHeader()
        {
            PMR02410PrintDisplayDTO loRtn = new PMR02410PrintDisplayDTO();

            loRtn.BaseHeaderData = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "Overtime",
                CUSER_ID = "GHC",
            };
            loRtn.ReportDataDTO = new PMR02410ReportDataDTO();
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
                CCUT_OFF = "20240612",
                FROM_CPERIOD = "20240612",
                CTO_CPERIOD = "20240712",
                CFROM_CUSTOMER_ID = "BuildingID001",
                CFROM_CUSTOMER_NAME = "BuildingName001",
                CTO_CUSTOMER_ID = "BuildingID002",
                CTO_CUSTOMER_NAME = "BuildingName002",
                CLANG_ID = "en",
                CREPORT_CULTURE = "en",
                CUSER_ID = "ghc",
            };

            loRtn.ReportDataDTO.Data = new List<PMR02410DataDTO>();

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
            string[] tenantIds = { "T1", "T2" };
            string[] tenantNames = { "Tenant One", "Tenant Two" };
            string[] agreementNos = { "A1", "A2", "A3" };
            string[] unitDescriptions = { "Unit 101", "Unit 102", "Unit 103" };
            string[] invoiceNos = { "INV1", "INV2", "INV3" };
            string[] invoiceDescriptions = { "Invoice Description 1", "Invoice Description 2", "Invoice Description 3" };
            string[] dueDates = { "20240601", "20240602", "20240603" };
            int[] lateDays = { 0, 5, 10 };
            string[] currencyCodes = { "USD", "USD" };
            string[] currencyNames = { "US Dollar", "US Dollar" };
            decimal[][] amounts = {
                new decimal[] { 100000000, 20000000, 50000000, 30000000 },
                new decimal[] { 150000000, 50000000, 50000000, 50000000 },
                new decimal[] { 200000000, 100000000, 50000000, 50000000 }
            };
            //add to list
            for (int tenantIndex = 0; tenantIndex < tenantIds.Length; tenantIndex++)
            {
                for (int agreementIndex = 0; agreementIndex < agreementNos.Length; agreementIndex++)
                {
                    for (int invoiceIndex = 0; invoiceIndex < invoiceNos.Length; invoiceIndex++)
                    {
                        loRtn.ReportDataDTO.Data.Add(new PMR02410DataDTO
                        {
                            CCOMPANY_ID = "C1",
                            CPROPERTY_ID = "P1",
                            CTENANT_ID = tenantIds[tenantIndex],
                            CTENANT_NAME = tenantNames[tenantIndex],
                            CAGREEMENT_NO = agreementNos[agreementIndex],
                            CUNIT_DESCRIPTION = unitDescriptions[agreementIndex],
                            CBUILDING_ID = "B1",
                            CBUILDING_NAME = "Building One",
                            CINVOICE_NO = invoiceNos[invoiceIndex],
                            CINVOICE_DESCRIPTION = invoiceDescriptions[invoiceIndex],
                            CDUE_DATE = dueDates[invoiceIndex],
                            ILATE_DAYS = lateDays[invoiceIndex],
                            CCURRENCY_CODE = currencyCodes[tenantIndex],
                            CCURRENCY_NAME = currencyNames[tenantIndex],
                            NINVOICE_AMOUNT = amounts[invoiceIndex][0],
                            NREDEEMED_AMOUNT = amounts[invoiceIndex][1],
                            NPAID_AMOUNT = amounts[invoiceIndex][2],
                            NOUTSTANDING_AMOUNT = amounts[invoiceIndex][3]
                        });
                    }
                }
            }

            foreach (var item in loRtn.ReportDataDTO.Data)
            {
                item.CDUE_DATE_DISPLAY = DateTime.TryParseExact(item.CDUE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime poCutOffDate) ? poCutOffDate.ToString("dd MMM yyyy", CultureInfo.InvariantCulture) : "";
            }

            return loRtn;
        }
    }
}