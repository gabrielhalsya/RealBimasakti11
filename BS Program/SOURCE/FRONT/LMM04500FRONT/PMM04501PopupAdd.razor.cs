using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Microsoft.AspNetCore.Components;
using PMM04500COMMON.DTO_s;
using PMM04500MODEL;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace PMM04500FRONT
{
    public partial class PMM04501PopupAdd : R_Page
    {
        private R_ConductorGrid _conGridPricing;
        private R_Grid<PricingBulkSaveDTO> _gridPricing;
        private PMM04500ViewModel _pricingAdd_ViewModel = new();

        [Inject] private IClientHelper? _clientHelper { get; set; }

        [Inject] R_PopupService PopupService { get; set; }

        public R_DatePicker<DateTime?> _validDateForm;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                PricingDTO loParam = R_FrontUtility.ConvertObjectToObject<PricingDTO>(poParameter);

                _pricingAdd_ViewModel._propertyId = loParam.CPROPERTY_ID;
                _pricingAdd_ViewModel._unitTypeCategoryId = loParam.CUNIT_TYPE_CATEGORY_ID;
                _pricingAdd_ViewModel._unitTypeCategoryName = loParam.CUNIT_TYPE_CATEGORY_NAME;
                if (!string.IsNullOrWhiteSpace(loParam.CVALID_DATE) || !string.IsNullOrEmpty(loParam.CVALID_DATE))
                {
                    _pricingAdd_ViewModel._validDateForm = DateTime.ParseExact(loParam.CVALID_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                await _pricingAdd_ViewModel.GetPriceType();//get list for price type
                await _pricingAdd_ViewModel.GetChargesType();//get list for charges type
                await _gridPricing.R_RefreshGrid(null);//refresh grid param
                await _validDateForm.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task PricingAdd_GetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _pricingAdd_ViewModel.GetPricingForSaveList();

                var loCheckedData = RemoveIDBeforeDisplay(new List<PricingBulkSaveDTO>(_pricingAdd_ViewModel._pricingSaveList), _pricingAdd_ViewModel._validDateForm.Value);
                _pricingAdd_ViewModel._pricingSaveList = new ObservableCollection<PricingBulkSaveDTO>(loCheckedData);
                eventArgs.ListEntityResult = _pricingAdd_ViewModel._pricingSaveList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void PricingAdd_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = R_FrontUtility.ConvertObjectToObject<PricingBulkSaveDTO>(eventArgs.Data);
        }

        private async Task PricingAdd_AfterAddAsync(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = (PricingBulkSaveDTO)eventArgs.Data;
                loData.DUPDATE_DATE = DateTime.Now;
                loData.DCREATE_DATE = DateTime.Now;
                await _pricingAdd_ViewModel.GetPriceType();//get newest list for price type 
                await _pricingAdd_ViewModel.GetChargesType();//get newest list for charges type
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void PricingAdd_CellValueChanged(R_CellValueChangedEventArgs eventArgs)
        {
            if (eventArgs.ColumnName == nameof(PricingBulkSaveDTO.CCHARGES_TYPE))
            {
                //get current selected charges type
                _pricingAdd_ViewModel._currentSelectedChargesType = eventArgs.Value.ToString();
            }
        }

        private async Task PricingAddForm_ValidDateValueChangedAsync(DateTime? poDate)
        {
            R_Exception loEx = new R_Exception();
            GFF00900ParameterDTO loParam = null;
            R_PopupResult loResult = null;
            try
            {
                //Approval before saving
                if (poDate < DateTime.Now.Date)
                {
                    //run validate
                    var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel
                    {
                        ACTIVATE_INACTIVE_ACTIVITY_CODE = "PMM04501"
                    };
                    await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync();

                    //check if activity code no need approval
                    if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                    {
                        _pricingAdd_ViewModel._validDateForm = poDate; //set value
                        await _gridPricing.R_RefreshGrid(null);

                        goto EndBlock;
                    }
                    else
                    {
                        //if its need approval then run approval form
                        loParam = new GFF00900ParameterDTO()
                        {
                            Data = loValidateViewModel.loRspActivityValidityList,
                            IAPPROVAL_CODE = "PMM04501"
                        };
                        loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParam);

                        //if not success show message
                        if (!loResult.Success || !(bool)loResult.Result)
                        {
                            var loMsgResult = await R_MessageBox.Show("", "Valid Date must be greater than Today", R_eMessageBoxButtonType.OK);
                            _pricingAdd_ViewModel._validDateForm = DateTime.Now; //set value
                            await _gridPricing.R_RefreshGrid(null);
                            goto EndBlock;//end process if null
                        }
                        else
                        {
                            //if success set value as normal
                            _pricingAdd_ViewModel._validDateForm = poDate; //set value
                            _pricingAdd_ViewModel._validDate = poDate.Value.ToString("yyyyMMdd");
                            await _gridPricing.R_RefreshGrid(null);
                        }
                    }
                }
                else //if no approval case
                {
                    _pricingAdd_ViewModel._validDateForm = poDate; //set value
                    //assign date to string for saving
                    _pricingAdd_ViewModel._validDate = poDate.Value.ToString("yyyyMMdd");
                    //if success set value as normal
                    await _gridPricing.R_RefreshGrid(null);
                }
                //~End approval
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private List<PricingBulkSaveDTO> RemoveIDBeforeDisplay(List<PricingBulkSaveDTO> poParam, DateTime pdDate)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.ForEach(p => p.CVALID_INTERNAL_ID = "");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                throw;
            }
            loEx.ThrowExceptionIfErrors();
            return poParam;
        }

        #region LookupColumn

        private void PricingAdd_BeforeLookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loParam = new LML00200ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CPROPERTY_ID = _pricingAdd_ViewModel._propertyId,
                CCHARGE_TYPE_ID = _pricingAdd_ViewModel._currentSelectedChargesType,
                CUSER_ID = _clientHelper.UserId,
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LML00200);
        }

        private void PricingAdd_AfterLookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            switch (eventArgs.ColumnName)
            {
                case nameof(PricingBulkSaveDTO.CCHARGES_NAME):
                    var loResult = R_FrontUtility.ConvertObjectToObject<LML00200DTO>(eventArgs.Result);
                    ((PricingBulkSaveDTO)eventArgs.ColumnData).CCHARGES_NAME = loResult.CCHARGES_NAME;
                    ((PricingBulkSaveDTO)eventArgs.ColumnData).CCHARGES_ID = loResult.CCHARGES_ID;
                    break;
            }
        }

        #endregion

        #region button

        private async Task PricingAdd_CancelAsync()
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

        private async Task PricingAdd_Process()
        {
            R_Exception loEx = new();
            try
            {
                _pricingAdd_ViewModel._validDate = _pricingAdd_ViewModel._validDateForm.Value.ToString("yyyyMMdd");
                await _pricingAdd_ViewModel.SavePricing(_gridPricing.DataSource.ToList());
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
