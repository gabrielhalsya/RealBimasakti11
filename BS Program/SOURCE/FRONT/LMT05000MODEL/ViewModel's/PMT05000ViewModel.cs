using PMT05000COMMON;
using PMT05000COMMON.DTO_s;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PMT05000MODEL.ViewModel_s
{
    public class PMT05000ViewModel
    {
        private PMT05000Model _initModel = new PMT05000Model();
        private PMT05001Model _model = new PMT05001Model();

        public ObservableCollection<PropertyDTO> _PropertyList { get; set; } = new ObservableCollection<PropertyDTO>();

        public ObservableCollection<GSB_CodeInfoDTO> _ChargeTypeList { get; set; } = new ObservableCollection<GSB_CodeInfoDTO>();

        public ObservableCollection<GSPeriodDT_DTO> _MonthPeriodList { get; set; } = new ObservableCollection<GSPeriodDT_DTO>();

        public ObservableCollection<AgreementTypeDTO> AgreementTypeList { get; set; } = new ObservableCollection<AgreementTypeDTO>();

        public ObservableCollection<AgreementChrgDiscDetailDTO> _AgreementChrgDiscDetailList { get; set; } = new ObservableCollection<AgreementChrgDiscDetailDTO>();


        public AgreementChrgDiscParamDTO _AgreementChrgDiscParam { get; set; } = new AgreementChrgDiscParamDTO();

        public async Task InitAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _PropertyList = new ObservableCollection<PropertyDTO>(await GetPropertyList());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task<List<PropertyDTO>> GetPropertyList() => await _initModel.GetPropertyListAsync();

        public async Task<List<GSB_CodeInfoDTO>> GetGSBCodeList() => await _initModel.GetGSBCodeInfoAsync();

        public async Task<List<GSPeriodDT_DTO>> GetPeriodDTList() => await _initModel.GetGSPeriodDTAsync();

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
                var loRtn = await _model.ProcessAgreementChargeDiscountAsync(_AgreementChrgDiscParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

    }
}
