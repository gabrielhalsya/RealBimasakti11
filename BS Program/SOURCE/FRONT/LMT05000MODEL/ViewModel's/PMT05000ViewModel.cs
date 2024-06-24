using PMT05000COMMON;
using PMT05000COMMON.DTO_s;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Interfaces;
using PMT05000FrontResources;
using System.Linq;
using R_BlazorFrontEnd;

namespace PMT05000MODEL.ViewModel_s
{
    public class PMT05000ViewModel
    {
        private PMT05000Model _initModel = new PMT05000Model();
        private PMT05001Model _model = new PMT05001Model();

        public ObservableCollection<PropertyDTO> _PropertyList { get; set; } = new ObservableCollection<PropertyDTO>();

        public ObservableCollection<GSB_CodeInfoDTO> _ChargeTypeList { get; set; } = new ObservableCollection<GSB_CodeInfoDTO>();

        public ObservableCollection<GSPeriodDT_DTO> _MonthPeriodList { get; set; } = new ObservableCollection<GSPeriodDT_DTO>();

        public ObservableCollection<AgreementTypeDTO> _AgreementTypeList { get; set; } = new ObservableCollection<AgreementTypeDTO>();

        public ObservableCollection<AgreementChrgDiscDetailDTO> _AgreementChrgDiscDetailList { get; set; } = new ObservableCollection<AgreementChrgDiscDetailDTO>();


        public AgreementChrgDiscParamDTO _AgreementChrgDiscProcessParam { get; set; } = new AgreementChrgDiscParamDTO();

        public AgreementChrgDiscListParamDTO _AgreementChrgDiscListParam { get; set; }

        public string _monthPeriod = "";
        public int _yearPeriod = DateTime.Now.Year;
        public string _discountName = "", _chargesName = "";

        #region init

        public async Task InitAsync(R_ILocalizer<Resources_Dummy_Class> poParamLocalizer)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _PropertyList = new ObservableCollection<PropertyDTO>(await GetPropertyList());
                _ChargeTypeList = new ObservableCollection<GSB_CodeInfoDTO>(await GetGSBCodeList());
                _MonthPeriodList = new ObservableCollection<GSPeriodDT_DTO>(await GetPeriodDTList());
                _AgreementTypeList = new ObservableCollection<AgreementTypeDTO>
                {
                    new AgreementTypeDTO{CAGREEMENT_TYPE="A", CAGREEMENT_TYPE_DISPLAY=poParamLocalizer["_labelAgreementTypeAll"]},
                    new AgreementTypeDTO{CAGREEMENT_TYPE="O", CAGREEMENT_TYPE_DISPLAY=poParamLocalizer["_labelAgreementTypeOwner"] },
                    new AgreementTypeDTO{CAGREEMENT_TYPE="L", CAGREEMENT_TYPE_DISPLAY=poParamLocalizer["_labelAgreementTypeLease"]},
                };

                //set default
                _AgreementChrgDiscProcessParam.CPROPERTY_ID = _PropertyList.Count > 0 ? _PropertyList.FirstOrDefault().CPROPERTY_ID : "";
                _AgreementChrgDiscProcessParam.CAGREEMENT_TYPE = _AgreementTypeList.FirstOrDefault().CAGREEMENT_TYPE;
                _AgreementChrgDiscProcessParam.LALL_BUILDING = true;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task<List<PropertyDTO>> GetPropertyList()
        {
            R_Exception loEx = new R_Exception();
            List<PropertyDTO> loRtn = new List<PropertyDTO>();
            try
            {
                loRtn = new List<PropertyDTO>();
                loRtn = await _initModel.GetPropertyListAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public async Task<List<GSB_CodeInfoDTO>> GetGSBCodeList()
        {
            R_Exception loEx = new R_Exception();
            List<GSB_CodeInfoDTO> loRtn = null;
            try
            {
                loRtn = await _initModel.GetGSBCodeInfoAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public async Task<List<GSPeriodDT_DTO>> GetPeriodDTList()
        {
            R_Exception loEx = new R_Exception();
            List<GSPeriodDT_DTO> loRtn = null;
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPMT05000.CYEAR, _yearPeriod.ToString());
                loRtn = await _initModel.GetGSPeriodDTAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;

        }

        #endregion

        public async Task GetAgreementChrgDiscListAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loRtn = await _model.GetAgreementChargesDiscountListAsync();
                _AgreementChrgDiscDetailList = new ObservableCollection<AgreementChrgDiscDetailDTO>(loRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessAgreementChrgDiscAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _model.ProcessAgreementChargeDiscountAsync(_AgreementChrgDiscProcessParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

    }
}
