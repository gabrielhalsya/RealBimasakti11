using BaseHeaderReportCOMMON;
using PMR00100Common.DTOs;
using PMR00100Common.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMR00100Common.Model
{
    public static class GenerateDataModel
    {
        public static LOOStatusReportResultDTO DefaultData()
        {
            LOOStatusReportResultDTO loData = new LOOStatusReportResultDTO()
            {
                title = "LOO Status",
                Header = new LOOStatusHeaderDTO()
                {
                    CPROPERTY = "12345 - Jakarta Cyber Punk",
                    CDEPT = "D001 - Sales Department",
                    CSALESMAN = "S001 - John Doe",
                    CPERIOD = "Januari 2023 - Desember 2024"
                },
                Column = new LOOStatusColumnDTO(),
                DataLOOStatus = new List<LOOStatusDetail1DTO>()
            };
            List<PMR00100DTO> loCollection = new List<PMR00100DTO>();

            int lnData1 = 3;
            int lnData2 = 3;
            int lnData3 = 3;
            int lnData4 = 5;
            for (int a = 1; a < lnData1; a++)
            {
                for (int b = 1; b < lnData2; b++)
                {
                    for (int c = 1; c < lnData3; c++)
                    {
                        for (int d = 1; d < lnData3; d++)
                        {
                            loCollection.Add(new PMR00100DTO()
                            {
                                CTRANS_NAME = $"Trans Type {a}",

                                CSALESMAN_ID = $"SL0{b}",
                                CSALESMAN_NAME = $"Sales {b}",

                                CREF_NO = $"REF-00{c}",
                                CREF_DATE = $"1 Jan 202{c}",
                                CTENURE = $"{c}Month, {c}Days",
                                CGLACCOUNT_NAME = $"GL Addount {c}",
                                CAGREEMENT_STATUS_NAME = $"Signed{c}",
                                CTRANS_STATUS_NAME = $"Inprogress{c}",
                                NREVISION_COUNT = c,
                                NTOTAL_PRICE = c * 1000.0m,
                                CTAX = $"PPN {c}%",
                                CTENANT_ID = $"TEN-0{c}",
                                CTENANT_NAME = $"Tenant Name {c}",
                                CTC_MESSAGE = $"TC Message TC Message {c}",

                                CUNIT_DETAIL_ID = $"Unit",
                                CUNIT_DETAIL_NAME = $"{d}",
                                NUNIT_DETAIL_GROSS_AREA_SIZE = d * 100.0m,
                                NUNIT_DETAIL_NET_AREA_SIZE = d * 80.0m,
                                NUNIT_DETAIL_COMMON_AREA_SIZE = d * 20.0m,
                                NUNIT_DETAIL_PRICE = d * 1500.0m,
                                NUNIT_TOTAL_GROSS_AREA_SIZE = d * 200.0m,
                                NUNIT_TOTAL_NET_AREA_SIZE = d * 160.0m,
                                NUNIT_TOTAL_COMMON_AREA_SIZE = d * 40.0m,
                                NUNIT_TOTAL_PRICE = d * 3000.0m,

                                CCHARGE_DETAIL_TYPE_NAME = $"Charge Type {d}",
                                CCHARGE_DETAIL_UNIT_NAME = $"Unit Name {d}",
                                CCHARGE_DETAIL_CHARGE_NAME = $"Charge Name {d}",
                                CCHARGE_DETAIL_TAX_NAME = $"Tax Name {d}",
                                CCHARGE_DETAIL_START_DATE = $"Start Date {d}",
                                CCHARGE_DETAIL_END_DATE = $"End Date {d}",
                                CCHARGE_DETAIL_TENURE = $"Tenure {d}",
                                CCHARGE_DETAIL_FEE_METHOD = $"Fee Method {d}",
                                NCHARGE_DETAIL_FEE_AMOUNT = d * 500.0m,
                                NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT = d * 550.0m,
                                NCHARGE_DETAIL_SUBTOTAL_CALCULATED_FEE_AMOUNT = d * 600.0m,
                                CDEPOSIT_DETAIL_ID = $"DepositID-{d}",
                                CDEPOSIT_DETAIL_DATE = $"Deposit Date {c}",
                                NDEPOSIT_DETAIL_AMOUNT = c * 200.0m,
                                CDEPOSIT_DETAIL_DESCRIPTION = $"Deposit Description {c}"

                            });
                        }
                    }
                }
            }

            var loTempData = loCollection
        .GroupBy(data1a => new
        {
            data1a.CTRANS_NAME, // First level of grouping
        })
        .Select(data1b => new LOOStatusDetail1DTO()
        {
            CTRANS_NAME = data1b.Key.CTRANS_NAME,
            LOOStatusDetail2 = data1b.GroupBy(data2a => new
            {
                data2a.CSALESMAN_ID, // Second level of grouping
                data2a.CSALESMAN_NAME,
            }).Select(data2b => new LOOStatusDetail2DTO()
            {
                CSALESMAN_ID = data2b.Key.CSALESMAN_ID,
                CSALESMAN_NAME = data2b.Key.CSALESMAN_NAME,
                LOOStatusDetail3 = data2b.GroupBy(data3a => new
                {
                    data3a.CREF_NO, // Third level of grouping
                    data3a.CREF_DATE,
                    data3a.CTENURE,
                    data3a.CGLACCOUNT_NAME,
                    data3a.CAGREEMENT_STATUS_NAME,
                    data3a.CTRANS_STATUS_NAME,
                    data3a.NREVISION_COUNT,
                    data3a.NTOTAL_PRICE,
                    data3a.CTAX,
                    data3a.CTENANT_ID,
                    data3a.CTENANT_NAME,
                    data3a.CTC_MESSAGE,
                }).Select(data3b => new LOOStatusDetail3DTO()
                {
                    CREF_NO = data3b.Key.CREF_NO,
                    CREF_DATE = data3b.Key.CREF_DATE,
                    CTENURE = data3b.Key.CTENURE,
                    CGLACCOUNT_NAME = data3b.Key.CGLACCOUNT_NAME,
                    CAGREEMENT_STATUS_NAME = data3b.Key.CAGREEMENT_STATUS_NAME,
                    CTRANS_STATUS_NAME = data3b.Key.CTRANS_STATUS_NAME,
                    NREVISION_COUNT = data3b.Key.NREVISION_COUNT,
                    NTOTAL_PRICE = data3b.Key.NTOTAL_PRICE,
                    CTAX = data3b.Key.CTAX,
                    CTENANT_ID = data3b.Key.CTENANT_ID,
                    CTENANT_NAME = data3b.Key.CTENANT_NAME,
                    CTC_MESSAGE = data3b.Key.CTC_MESSAGE,
                    LOOStatusDetailUnit = data3b.GroupBy(dataUnit => new
                    {
                        dataUnit.CUNIT_DETAIL_ID,
                        dataUnit.CUNIT_DETAIL_NAME,
                        dataUnit.NUNIT_DETAIL_GROSS_AREA_SIZE,
                        dataUnit.NUNIT_DETAIL_NET_AREA_SIZE,
                        dataUnit.NUNIT_DETAIL_COMMON_AREA_SIZE,
                        dataUnit.NUNIT_DETAIL_PRICE,
                        dataUnit.NUNIT_TOTAL_GROSS_AREA_SIZE,
                        dataUnit.NUNIT_TOTAL_NET_AREA_SIZE,
                        dataUnit.NUNIT_TOTAL_COMMON_AREA_SIZE,
                        dataUnit.NUNIT_TOTAL_PRICE,
                    }).Select(selectUnit => new LOOStatusDetailUnitDTO()
                    {
                        CUNIT_DETAIL_ID = selectUnit.Key.CUNIT_DETAIL_ID,
                        CUNIT_DETAIL_NAME = selectUnit.Key.CUNIT_DETAIL_NAME,
                        NUNIT_DETAIL_GROSS_AREA_SIZE = selectUnit.Key.NUNIT_DETAIL_GROSS_AREA_SIZE,
                        NUNIT_DETAIL_NET_AREA_SIZE = selectUnit.Key.NUNIT_DETAIL_NET_AREA_SIZE,
                        NUNIT_DETAIL_COMMON_AREA_SIZE = selectUnit.Key.NUNIT_DETAIL_COMMON_AREA_SIZE,
                        NUNIT_DETAIL_PRICE = selectUnit.Key.NUNIT_DETAIL_PRICE,
                        NUNIT_TOTAL_GROSS_AREA_SIZE = selectUnit.Key.NUNIT_TOTAL_GROSS_AREA_SIZE,
                        NUNIT_TOTAL_NET_AREA_SIZE = selectUnit.Key.NUNIT_TOTAL_NET_AREA_SIZE,
                        NUNIT_TOTAL_COMMON_AREA_SIZE = selectUnit.Key.NUNIT_TOTAL_COMMON_AREA_SIZE,
                        NUNIT_TOTAL_PRICE = selectUnit.Key.NUNIT_TOTAL_PRICE,
                    }).ToList(),
                    LOOStatusDetailCharge = data3b
                    .GroupBy(dataCharge => new
                    {
                        dataCharge.CCHARGE_DETAIL_TYPE_NAME,
                        dataCharge.CCHARGE_DETAIL_UNIT_NAME,
                        dataCharge.CCHARGE_DETAIL_CHARGE_NAME,
                        dataCharge.CCHARGE_DETAIL_TAX_NAME,
                        dataCharge.CCHARGE_DETAIL_START_DATE,
                        dataCharge.CCHARGE_DETAIL_END_DATE,
                        dataCharge.CCHARGE_DETAIL_TENURE,
                        dataCharge.CCHARGE_DETAIL_FEE_METHOD,
                        dataCharge.NCHARGE_DETAIL_FEE_AMOUNT,
                        dataCharge.NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT,
                        dataCharge.NCHARGE_DETAIL_SUBTOTAL_CALCULATED_FEE_AMOUNT,
                        dataCharge.CDEPOSIT_DETAIL_ID,
                        dataCharge.CDEPOSIT_DETAIL_DATE,
                        dataCharge.NDEPOSIT_DETAIL_AMOUNT,
                        dataCharge.CDEPOSIT_DETAIL_DESCRIPTION,
                    })
                    .Select(selectCharge => new LOOStatusDetailChargeDTO()
                    {
                        CCHARGE_DETAIL_TYPE_NAME = selectCharge.Key.CCHARGE_DETAIL_TYPE_NAME,
                        LOOStatusDetailChargeUnit = selectCharge.GroupBy(chargeUnit => new
                        {
                            chargeUnit.CCHARGE_DETAIL_UNIT_NAME,
                        }).Select(selectChargeUnit => new LOOStatusDetailChargeTypeUnitDTO()
                        {
                            CCHARGE_DETAIL_UNIT_NAME = selectChargeUnit.Key.CCHARGE_DETAIL_UNIT_NAME,
                            LOOStatusDetailChargeTypeUnitCharge = selectChargeUnit.Select(charge => new LOOStatusDetailChargeTypeUnitChargeDTO()
                            {
                                CCHARGE_DETAIL_CHARGE_NAME = charge.CCHARGE_DETAIL_CHARGE_NAME,
                                CCHARGE_DETAIL_TAX_NAME = charge.CCHARGE_DETAIL_TAX_NAME,
                                CCHARGE_DETAIL_START_DATE = charge.CCHARGE_DETAIL_START_DATE,
                                CCHARGE_DETAIL_END_DATE = charge.CCHARGE_DETAIL_END_DATE,
                                CCHARGE_DETAIL_TENURE = charge.CCHARGE_DETAIL_TENURE,
                                CCHARGE_DETAIL_FEE_METHOD = charge.CCHARGE_DETAIL_FEE_METHOD,
                                NCHARGE_DETAIL_FEE_AMOUNT = charge.NCHARGE_DETAIL_FEE_AMOUNT,
                                NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT = charge.NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT,
                                NCHARGE_DETAIL_SUBTOTAL_CALCULATED_FEE_AMOUNT = charge.NCHARGE_DETAIL_SUBTOTAL_CALCULATED_FEE_AMOUNT,
                                CDEPOSIT_DETAIL_ID = charge.CDEPOSIT_DETAIL_ID,
                                CDEPOSIT_DETAIL_DATE = charge.CDEPOSIT_DETAIL_DATE,
                                NDEPOSIT_DETAIL_AMOUNT = charge.NDEPOSIT_DETAIL_AMOUNT,
                                CDEPOSIT_DETAIL_DESCRIPTION = charge.CDEPOSIT_DETAIL_DESCRIPTION,
                            }).ToList(),
                        }).ToList(),
                    }).ToList(),
                    LOOStatusDetailDeposit = data3b
                    .GroupBy(dataDeposit => new
                    {
                        dataDeposit.CDEPOSIT_DETAIL_ID,
                        dataDeposit.CDEPOSIT_DETAIL_DATE,
                        dataDeposit.NDEPOSIT_DETAIL_AMOUNT,
                        dataDeposit.CDEPOSIT_DETAIL_DESCRIPTION,
                    }).Select(selectDeposit => new LOOStatusDetailDepositDTO()
                    {
                        CDEPOSIT_DETAIL_ID = selectDeposit.Key.CDEPOSIT_DETAIL_ID,
                        CDEPOSIT_DETAIL_DATE = selectDeposit.Key.CDEPOSIT_DETAIL_DATE,
                        NDEPOSIT_DETAIL_AMOUNT = selectDeposit.Key.NDEPOSIT_DETAIL_AMOUNT,
                        CDEPOSIT_DETAIL_DESCRIPTION = selectDeposit.Key.CDEPOSIT_DETAIL_DESCRIPTION,
                    }).ToList(),
                }).ToList()
            }).ToList()
        }).ToList();

            var loTempDataTotalTrans = loCollection
                     .GroupBy(data => data.CTRANS_NAME)
                     .Select(group => new LOOStatusTotalTransDTO()
                     {
                         CTRANS_NAME = group.Key,
                         COL_TOTAL_TRANS = group.Select(data => data.CTRANS_CODE).Distinct().Count(),
                     })
                     .ToList();

            var loTempDataTotalSales = loCollection
                 .GroupBy(data => data.CSALESMAN_ID)
                 .Select(group => new LOOStatusTotalSalesDTO()
                 {
                     CSALESMAN_ID = group.Key,
                     COL_TOTAL_SALESMAN = group.Select(data => data.CSALESMAN_ID).Distinct().Count()
                 })
                 .ToList();


            loData.LOOStatusTotalSales = loTempDataTotalSales;
            loData.LOOStatusTotalTrans = loTempDataTotalTrans;
            loData.DataLOOStatus = loTempData;
            return loData;
        }


        public static PMR00100LOOStatusResultWithBaseHeaderDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "001",
                CPRINT_NAME = "LOO Status",
                CUSER_ID = "AKC"
            };
            PMR00100LOOStatusResultWithBaseHeaderDTO loRtn = new PMR00100LOOStatusResultWithBaseHeaderDTO();
            loRtn.BaseHeaderData = loParam;
            loRtn.PMR00100PrintData = DefaultData();
            return loRtn;
        }
    }
}
