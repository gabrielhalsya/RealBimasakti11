using Microsoft.Extensions.Logging;
using PMM03700COMMON.DTO_s;
using PMM03700MODEL;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace PMM03700FRONT
{
    public partial class PopupMoveTenant : R_Page
    {
        private R_ConductorGrid _conTenantToMoveRef;
        private R_Grid<TenantGridDTO> _gridTenantToMove;
        private PMM03710ViewModel _viewModelTC = new PMM03710ViewModel();

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModelTC.TenantClass = (TenantClassificationDTO)poParameter;
                await TenantClassDetail(poParameter);//get detail
                await TenantClassForMoveTenant_GetList(poParameter);
                await _gridTenantToMove.R_RefreshGrid(poParameter);//refresh grid param
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task TenantClassForMoveTenant_GetList(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (TenantClassificationDTO)poParam;
                _viewModelTC._propertyId = loParam.CPROPERTY_ID;
                _viewModelTC._tenantClassificationGroupId = loParam.CTENANT_CLASSIFICATION_GROUP_ID;
                _viewModelTC._tenantClassificationId = loParam.CTENANT_CLASSIFICATION_ID;
                await _viewModelTC.GetTenantClassListForMove();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task TenantClassDetail(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(poParam);
                _viewModelTC._propertyId = loParam.CPROPERTY_ID;
                _viewModelTC._tenantClassificationGroupId = loParam.CTENANT_CLASSIFICATION_GROUP_ID;
                await _viewModelTC.GetTenantClassRecordForMove(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(eventArgs.Parameter);
                await _viewModelTC.GetTenantListToMove(loParam);
                eventArgs.ListEntityResult = _viewModelTC.TenantToMoveList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region ProcesButton
        public async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();
            try
            {
                //await _conTenantToMoveRef.R_SaveBatch();
                if (string.IsNullOrWhiteSpace(_viewModelTC._toTenantClassificationId))
                {
                    await R_MessageBox.Show("", "Please select Tennat Classification Destination", R_eMessageBoxButtonType.OK);
                    goto Endblock;
                }
                _viewModelTC._fromTenantClassificationId = _viewModelTC.TenantClassForMoveTenant.CTENANT_CLASSIFICATION_ID;
                await _viewModelTC.MoveTenant(_gridTenantToMove.DataSource.ToList());
                await this.Close(true, _viewModelTC.TenantClass);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            Endblock:
            loEx.ThrowExceptionIfErrors();
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, _viewModelTC.TenantClass);
        }
        #endregion
        #region Save Batch
        //private async Task R_BeforeSaveBatchAsync(R_BeforeSaveBatchEventArgs events)
        //{

        //    var loEx = new R_Exception();
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(_viewModelTC._toTenantClassificationId))
        //        {
        //            await R_MessageBox.Show("", "Please select Tennat Classification Destination", R_eMessageBoxButtonType.OK);
        //        }
        //        events.Cancel = string.IsNullOrWhiteSpace(_viewModelTC._toTenantClassificationId);
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }
        //    R_DisplayException(loEx);
        //}
        //private async Task R_ServiceSaveBatchAsync(R_ServiceSaveBatchEventArgs eventArgs)
        //{
        //    var loEx = new R_Exception();

        //    try
        //    {
        //        //set origin tenantclass
        //        _viewModelTC._fromTenantClassificationId = _viewModelTC.TenantClassForMoveTenant.CTENANT_CLASSIFICATION_ID;
        //        //await _viewModelTC.MoveTenant();
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();
        //}
        //private async Task R_AfterSaveBatchAsync(R_AfterSaveBatchEventArgs eventArgs)
        //{
        //    await this.Close(true, _viewModelTC.TenantClass);
        //}
        #endregion


    }
}
