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
            loRtn.ReportDataDTO.Data = new List<PMR00601DataDTO>()
            {
                new PMR00601DataDTO
                {
                    CCOMPANY_ID = "COMP01",
                    CPROPERTY_ID = "PROP01",
                    CBUILDING_ID = "BLD01",
                    CBUILDING_NAME = "Building Jakarta",
                    CDEPT_CODE = "DPT_A",
                    CDEPT_NAME = "Department Jakarta Pusat",
                    CTENANT_ID = "MU01",
                    CTENANT_NAME = "Muhammad Andika Putra",
                    CINVOICE_NO = "INV-REF-AGRE-1",
                    CINVOICE_DATE = "1 Jan 2024",
                    NINVOICE_AMOUNT = 100000000,
                    NINVOICE_TAX_AMOUNT = 100000000,
                    NINVOICE_TOTAL_AMOUNT = 200000000,
                    CAGREEMENT_NO = "REF-AGRE-1",
                    CAGREEMENT_DATE = "1 Jan 2024",
                    CCHARGE_ID = "CHG01",
                    CCHARGE_NAME = "Overtime Weekday(s)",
                    CUNIT_ID = "Unit_A",
                    CUNIT_NAME = "Unit Sederhana",
                    COVERTIME_DATE = "1 Jan 2024",
                    CTIME_IN = "18:00",
                    CTIME_OUT = "21:00",
                    CTENURE = "3H 0M",
                    NUNIT_PRICE = 100000000,
                    NUNIT_ACTUAL_AMOUNT = 100000000,
                    CNOTE = "Every Hour",
                    CTRANS_STATUS_CODE = "01",
                    CTRANS_STATUS_NAME = "Closed"
                },
                new PMR00601DataDTO
                {
                    CCOMPANY_ID = "COMP01",
                    CPROPERTY_ID = "PROP01",
                    CBUILDING_ID = "BLD01",
                    CBUILDING_NAME = "Building Jakarta",
                    CDEPT_CODE = "DPT_A",
                    CDEPT_NAME = "Department Jakarta Pusat",
                    CTENANT_ID = "MU01",
                    CTENANT_NAME = "Muhammad Andika Putra",
                    CINVOICE_NO = "INV-REF-AGRE-1",
                    CINVOICE_DATE = "1 Jan 2024",
                    NINVOICE_AMOUNT = 100000000,
                    NINVOICE_TAX_AMOUNT = 100000000,
                    NINVOICE_TOTAL_AMOUNT = 200000000,
                    CAGREEMENT_NO = "REF-AGRE-1",
                    CAGREEMENT_DATE = "1 Jan 2024",
                    CCHARGE_ID = "CHG01",
                    CCHARGE_NAME = "Overtime Weekday(s)",
                    CUNIT_ID = "Unit_A",
                    CUNIT_NAME = "Unit Sederhana",
                    COVERTIME_DATE = "1 Jan 2024",
                    CTIME_IN = "18:00",
                    CTIME_OUT = "21:00",
                    CTENURE = "3H 0M",
                    NUNIT_PRICE = 100000000,
                    NUNIT_ACTUAL_AMOUNT = 100000000,
                    CNOTE = "Every Hour",
                    CTRANS_STATUS_CODE = "01",
                    CTRANS_STATUS_NAME = "Closed"
                },
                new PMR00601DataDTO
                {
                    CCOMPANY_ID = "COMP01",
                    CPROPERTY_ID = "PROP01",
                    CBUILDING_ID = "BLD01",
                    CBUILDING_NAME = "Building Jakarta",
                    CDEPT_CODE = "DPT_A",
                    CDEPT_NAME = "Department Jakarta Pusat",
                    CTENANT_ID = "MU01",
                    CTENANT_NAME = "Muhammad Andika Putra",
                    CINVOICE_NO = "INV-REF-AGRE-1",
                    CINVOICE_DATE = "1 Jan 2024",
                    NINVOICE_AMOUNT = 100000000,
                    NINVOICE_TAX_AMOUNT = 100000000,
                    NINVOICE_TOTAL_AMOUNT = 200000000,
                    CAGREEMENT_NO = "REF-AGRE-1",
                    CAGREEMENT_DATE = "1 Jan 2024",
                    CCHARGE_ID = "CHG01",
                    CCHARGE_NAME = "Overtime Weekday(s)",
                    CUNIT_ID = "Unit_A",
                    CUNIT_NAME = "Unit Sederhana",
                    COVERTIME_DATE = "1 Jan 2024",
                    CTIME_IN = "18:00",
                    CTIME_OUT = "21:00",
                    CTENURE = "3H 0M",
                    NUNIT_PRICE = 100000000,
                    NUNIT_ACTUAL_AMOUNT = 100000000,
                    CNOTE = "Every Hour",
                    CTRANS_STATUS_CODE = "01",
                    CTRANS_STATUS_NAME = "Closed"
                },
                new PMR00601DataDTO
                {
                    CCOMPANY_ID = "COMP01",
                    CPROPERTY_ID = "PROP01",
                    CBUILDING_ID = "BLD01",
                    CBUILDING_NAME = "Building Jakarta",
                    CDEPT_CODE = "DPT_A",
                    CDEPT_NAME = "Department Jakarta Pusat",
                    CTENANT_ID = "MU01",
                    CTENANT_NAME = "Muhammad Andika Putra",
                    CINVOICE_NO = "INV-REF-AGRE-1",
                    CINVOICE_DATE = "1 Jan 2024",
                    NINVOICE_AMOUNT = 100000000,
                    NINVOICE_TAX_AMOUNT = 100000000,
                    NINVOICE_TOTAL_AMOUNT = 100000000,
                    CAGREEMENT_NO = "REF-AGRE-1",
                    CAGREEMENT_DATE = "1 Jan 2024",
                    CCHARGE_ID = "CHG01",
                    CCHARGE_NAME = "Overtime Weekday(s)",
                    CUNIT_ID = "Unit_A",
                    CUNIT_NAME = "Unit Sederhana",
                    COVERTIME_DATE = "1 Jan 2024",
                    CTIME_IN = "18:00",
                    CTIME_OUT = "21:00",
                    CTENURE = "3H 0M",
                    NUNIT_PRICE = 100000000,
                    NUNIT_ACTUAL_AMOUNT = 100000000,
                    CNOTE = "Every Hour",
                    CTRANS_STATUS_CODE = "01",
                    CTRANS_STATUS_NAME = "Closed"
                },
                // Data untuk tenant kedua
                new PMR00601DataDTO
                {
                    CCOMPANY_ID = "COMP01",
                    CPROPERTY_ID = "PROP01",
                    CBUILDING_ID = "BLD01",
                    CBUILDING_NAME = "Building Jakarta",
                    CDEPT_CODE = "DPT_A",
                    CDEPT_NAME = "Department Jakarta Pusat",
                    CTENANT_ID = "AU01",
                    CTENANT_NAME = "Aurelius Irvin",
                    CINVOICE_NO = "INV-REF-AGRE-2",
                    CINVOICE_DATE = "1 Jan 2024",
                    NINVOICE_AMOUNT = 100000000,
                    NINVOICE_TAX_AMOUNT = 100000000,
                    NINVOICE_TOTAL_AMOUNT = 100000000,
                    CAGREEMENT_NO = "REF-AGRE-2",
                    CAGREEMENT_DATE = "1 Jan 2024",
                    CCHARGE_ID = "CHG01",
                    CCHARGE_NAME = "Overtime Weekday(s)",
                    CUNIT_ID = "Unit_B",
                    CUNIT_NAME = "Unit Besar",
                    COVERTIME_DATE = "1 Jan 2024",
                    CTIME_IN = "18:00",
                    CTIME_OUT = "21:00",
                    CTENURE = "3H 0M",
                    NUNIT_PRICE = 100000000,
                    NUNIT_ACTUAL_AMOUNT = 100000000,
                    CNOTE = "Every Hour",
                    CTRANS_STATUS_CODE = "01",
                    CTRANS_STATUS_NAME = "Closed"
                },
                new PMR00601DataDTO
                {
                    CCOMPANY_ID = "COMP01",
                    CPROPERTY_ID = "PROP01",
                    CBUILDING_ID = "BLD01",
                    CBUILDING_NAME = "Building Jakarta",
                    CDEPT_CODE = "DPT_A",
                    CDEPT_NAME = "Department Jakarta Pusat",
                    CTENANT_ID = "AU01",
                    CTENANT_NAME = "Aurelius Irvin",
                    CINVOICE_NO = "INV-REF-AGRE-2",
                    CINVOICE_DATE = "1 Jan 2024",
                    NINVOICE_AMOUNT = 100000000,
                    NINVOICE_TAX_AMOUNT = 100000000,
                    NINVOICE_TOTAL_AMOUNT = 100000000,
                    CAGREEMENT_NO = "REF-AGRE-2",
                    CAGREEMENT_DATE = "1 Jan 2024",
                    CCHARGE_ID = "CHG01",
                    CCHARGE_NAME = "Overtime Weekday(s)",
                    CUNIT_ID = "Unit_B",
                    CUNIT_NAME = "Unit Besar",
                    COVERTIME_DATE = "1 Jan 2024",
                    CTIME_IN = "18:00",
                    CTIME_OUT = "21:00",
                    CTENURE = "3H 0M",
                    NUNIT_PRICE = 100000000,
                    NUNIT_ACTUAL_AMOUNT = 100000000,
                    CNOTE = "Every Hour",
                    CTRANS_STATUS_CODE = "01",
                    CTRANS_STATUS_NAME = "Closed"
                }
            };
            return loRtn;
        }
    }
}
