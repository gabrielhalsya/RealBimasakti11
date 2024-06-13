using BaseHeaderReportCOMMON;
using PMR00600COMMON.DTO_s.PrintDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00600COMMON.Model
{
    public class PMR00601DummyData
    {
        public static PMR00601PrintDisplayDTO PMR00601PrintDislpayWithBaseHeader()
        {
            PMR00601PrintDisplayDTO loRtn = new PMR00601PrintDisplayDTO();

            loRtn.BaseHeaderData = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "Overtime",
                CUSER_ID = "GHC",
            };
            loRtn.ReportDataDTO = new PMR00601ReportDataDTO();
            loRtn.ReportDataDTO.Header = "Header";
            loRtn.ReportDataDTO.Title = "Overtime Detail Report";
            loRtn.ReportDataDTO.Label = new PMR00601LabelDTO();
            loRtn.ReportDataDTO.Param = new PMR00601ParamDTO()
            {
                CCOMPANY_ID = "RCD",
                CPROPERTY_ID = "ProeprtyID",
                CPROPERTY_NAME = "Property Name",
                CFROM_PERIOD = "20240612",
                CTO_PERIOD = "20240712",
                CFROM_BUILDING_ID = "BuildingID001",
                CFROM_BUILDING_NAME = "BuildingName001",
                CTO_BUILDING_ID = "BuildingID002",
                CTO_BUILDING_NAME = "BuildingName002",
                CFROM_DEPT_CODE = "dept001",
                CFROM_DEPT_NAME = "deptName001",
                CTO_DEPT_CODE = "dept002",
                CTO_DEPT_NAME = "deptName002",
                CREPORT_TYPE = "Summary",
                CFROM_TENANT_ID = "Tenant01",
                CFROM_TENANT_NAME = "TenantName001",
                CTO_TENANT_ID = "Tenant02",
                CTO_TENANT_NAME = "TenantName002",
                CFROM_SERVICE_ID = "Serv1",
                CFROM_SERVICE_NAME = "ServName1",
                CTO_SERVICE_ID = "serv2",
                CTO_SERVICE_NAME = "ServName2",
                CSTATUS = "1", //"2"
                CSTATUS_DISPLAY = "Open",
                CINVOICE = "1", //"2"
                CINVOICE_DISPLAY = "Involved",
                CLANG_ID = "en",
                LINVOICE = true,
                LSERVICE = true,
                LSTATUS = true,
                LTENANT = true

            };
            loRtn.ReportDataDTO.Data = new List<PMR00601DataDTO>();

            var tenants = new List<string> { "Muhammad Andika Putra", "Aurelius Irvin" };
            var invoices = new List<string> { "INV-001", "INV-002", "INV-003", "INV-004" };
            var agreements = new List<string> { "AGR-001", "AGR-002", "AGR-003", "AGR-004" };
            var charges = new List<string> { "CHG01", "CHG02" };

            int amount = 100000000;

            for (int b = 1; b <= 2; b++) // Buildings
            {
                for (int d = 1; d <= 2; d++) // Departments
                {
                    for (int t = 0; t < tenants.Count; t++) // Tenants
                    {
                        for (int i = 0; i < 2; i++) // Invoices per Tenant
                        {
                            for (int a = 0; a < 2; a++) // Agreements per Invoice
                            {
                                for (int c = 0; c < charges.Count; c++) // Charges per Agreement
                                {
                                    loRtn.ReportDataDTO.Data.Add(new PMR00601DataDTO
                                    {
                                        CCOMPANY_ID = "COMP001",
                                        CPROPERTY_ID = "PROP001",
                                        CBUILDING_ID = $"BLD_{b}",
                                        CBUILDING_NAME = $"Building {b}",
                                        CDEPT_CODE = $"DPT_{d}",
                                        CDEPT_NAME = $"Department {d}",
                                        CTENANT_ID = $"TENANT{t + 1}",
                                        CTENANT_NAME = tenants[t],
                                        CINVOICE_NO = invoices[i + (t * 2)],
                                        CINVOICE_DATE = "20240101",
                                        NINVOICE_AMOUNT = amount,
                                        NINVOICE_TAX_AMOUNT = amount,
                                        NINVOICE_TOTAL_AMOUNT = 2 * amount,
                                        CAGREEMENT_NO = agreements[a + (i * 2)],
                                        CAGREEMENT_DATE = "20240101",
                                        CCHARGE_ID = charges[c],
                                        CCHARGE_NAME = charges[c] == "CHG01" ? "Overtime Weekday(s)" : "Overtime Weekend(s)",
                                        CUNIT_ID = $"UNIT_{(c % 2 == 0 ? 'A' : 'B')}",
                                        CUNIT_NAME = $"Unit {(c % 2 == 0 ? "Sederhana" : "Mewah")}",
                                        COVERTIME_DATE = "20240101",
                                        CTIME_IN = "18:00",
                                        CTIME_OUT = "21:00",
                                        CTENURE = "3H 0M",
                                        NUNIT_PRICE = amount,
                                        NUNIT_ACTUAL_AMOUNT = amount,
                                        CNOTE = "Every Hour",
                                        CTRANS_STATUS_CODE = "01",
                                        CTRANS_STATUS_NAME = "Closed"
                                    });

                                    amount += 100000000; // Increase amount to ensure distinct values
                                }
                            }
                        }
                    }
                }
            }
            return loRtn;
        }
    }
}
