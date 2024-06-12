using BaseHeaderReportCOMMON;
using PMR00600COMMON.DTO_s.Print;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00600COMMON.DTO_s.Model
{
    public static class PMR00600DummyData
    {
        public static PMR00600PrintDisplayDTO PMR00600PrintDislpayWithBaseHeader()
        {
            PMR00600PrintDisplayDTO loRtn = new PMR00600PrintDisplayDTO();

            loRtn.BaseHeaderData = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "Overtime",
                CUSER_ID = "GHC",
            };
            loRtn.ReportDataDTO = new PMR00600ReportDataDTO();
            loRtn.ReportDataDTO.Header = "Header";
            loRtn.ReportDataDTO.Title = "Overtime Report";
            loRtn.ReportDataDTO.Label = new PMR00600LabelDTO();
            loRtn.ReportDataDTO.Param = new PMR00600ParamDTO()
            {
                CCOMPANY_ID = "RCD",
                CPROPERTY_ID = "ProeprtyID",
                CPROPERTY_NAME = "Property Name",
                CFROM_PERIOD = "20240612",
                CTO_PERIOD = "20240712",
                CFROM_BUILDING_ID = "BuildingID001",
                CTO_BUILDING_ID = "BuildingID002",
                CFROM_DEPT_CODE = "dept001",
                CTO_DEPT_CODE = "dept002",
                CREPORT_TYPE = "Summary",
                CFROM_TENANT_ID = "Tenant01",
                CTO_TENANT_ID = "Tenant02",
                CFROM_SERVICE_ID = "Serv1",
                CTO_SERVICE_ID = "serv2",
                CSTATUS = "status",
                CINVOICE = "invoice",
                CLANG_ID = "en",
            };
            loRtn.ReportDataDTO.Data = new List<PMR00600DataDTO>();
            for (int i = 1; i <= 20; i++)
            {
                loRtn.ReportDataDTO.Data.Add(new PMR00600DataDTO
                {
                    CCOMPANY_ID = $"COMP{i:D2}",
                    CPROPERTY_ID = $"PROP{i:D2}",
                    CBUILDING_ID = $"BLD{i:D2}",
                    CBUILDING_NAME = $"Building Name {i}",
                    CDEPT_CODE = $"DPT{i:D2}",
                    CDEPT_NAME = $"Department Name {i}",
                    CTENANT_ID = $"TEN{i:D2}",
                    CTENANT_NAME = $"Tenant Name {i}",
                    CAGREEMENT_NO = $"AGR{i:D2}",
                    CCHARGE_ID = $"CHG{i:D2}",
                    CCHARGE_NAME = $"Charge Name {i}",
                    NJAN_AMOUNT = i * 1000,
                    NFEB_AMOUNT = i * 2000,
                    NMAR_AMOUNT = i * 3000,
                    NAPR_AMOUNT = i * 4000,
                    NMAY_AMOUNT = i * 5000,
                    NJUN_AMOUNT = i * 6000,
                    NJUL_AMOUNT = i * 7000,
                    NAUG_AMOUNT = i * 8000,
                    NSEP_AMOUNT = i * 9000,
                    NOCT_AMOUNT = i * 10000,
                    NNOV_AMOUNT = i * 11000,
                    NDEC_AMOUNT = i * 12000
                });
            };
            return loRtn;
        }
    }
}
