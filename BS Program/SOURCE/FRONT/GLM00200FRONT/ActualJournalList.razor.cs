using BlazorClientHelper;
using GLM00200Common;
using GLM00200Model;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GLM00200Front
{
    public partial class ActualJournalList : R_Page
    {
        private GLM00200ActualJournalListViewModel _journalVM = new GLM00200ActualJournalListViewModel();
        private R_Grid<JournalDetailActualGridDTO> _gridJournalDet;
        private R_Conductor _conJournalNavigator;
        [Inject] IClientHelper _clientHelper { get; set; }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<JournalDTO>(poParameter);

                if (!string.IsNullOrWhiteSpace(loParam.CREC_ID))
                {
                    loParam.CJRN_ID = loParam.CREC_ID;
                    await _conJournalNavigator.R_GetEntity(loParam);
                }

                await _journalVM.GetInitData();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #region Header Data
        private async Task JournalForm_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (JournalDTO)eventArgs.Data;
                await _journalVM.GetlJournal(loParam);
                eventArgs.Result = _journalVM.Journal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private async Task JournalForm_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (eventArgs.Data != null)
                    {
                        var loData = (JournalDTO)eventArgs.Data;
                        if (!string.IsNullOrWhiteSpace(loData.CREF_NO) || !string.IsNullOrWhiteSpace(loData.CDEPT_CODE))
                        {
                            await _gridJournalDet.R_RefreshGrid(loData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Detail
        private async Task JournalDetGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<RecurringJournalListParamDTO>(eventArgs.Parameter);
                await _journalVM.ShowAllJournalDetail(loParam);
                eventArgs.ListEntityResult = _journalVM.JournaDetailActualGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion
    }
}
