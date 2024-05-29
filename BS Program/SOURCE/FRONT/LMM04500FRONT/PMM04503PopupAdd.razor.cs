using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using Microsoft.AspNetCore.Components;
using PMM04500COMMON;
using PMM04500COMMON.DTO_s;
using PMM04500MODEL;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM04500FRONT
{
    public partial class PMM04503PopupAdd : R_Page
    {
        private R_ConductorGrid _conGridPricingRate;
        private R_Grid<PricingRateBulkSaveDTO> _gridPricingRate;
        private PMM04501ViewModel _viewModel_PricingRate = new();

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                //get & parse param
                var loParam = R_FrontUtility.ConvertObjectToObject<PricingRateDTO>(poParameter);

                //set param to class variable
                _viewModel_PricingRate._pricingRateDateDisplay = !string.IsNullOrWhiteSpace(loParam.CRATE_DATE) ? DateTime.ParseExact(loParam.CRATE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture) : DateTime.Now;
                _viewModel_PricingRate._propertyId = loParam.CPROPERTY_ID ?? "";
                _viewModel_PricingRate._pricingRateDate = loParam.CRATE_DATE ?? "";

                await _gridPricingRate.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        #region grid events
        private async Task PricingRateAdd_GetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel_PricingRate.GetPricingRateAddList();

                eventArgs.ListEntityResult = _viewModel_PricingRate._pricingSaveList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void PricingRateAdd_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }

        #endregion

        #region form

        private async Task PricingRateAddForm_RateDateValueChangedAsync(DateTime? poDateParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel_PricingRate._pricingRateDateDisplay = poDateParam;
                _viewModel_PricingRate._pricingRateDate = poDateParam.Value.ToString("yyyyMMdd");
                await _gridPricingRate.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region button

        private async Task PricingRateAdd_CancelAsync()
        {
            R_Exception loEx = new();
            try
            {
                await this.Close(true, null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task PricingRateAdd_Process()
        {
            R_Exception loEx = new();
            try
            {
                await _viewModel_PricingRate.SavePricing();
                if (!loEx.HasError)
                {
                    R_eMessageBoxResult loMessageResult = await R_MessageBox.Show("", "Process Complete", R_eMessageBoxButtonType.OK);
                }
                await this.Close(true, null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion
    }
}
