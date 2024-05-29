using Lookup_GLCOMMON.DTO;
using Lookup_GLModel.ViewModel.GLL00100;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_GLFRONT;

public partial class GLL00100 : R_Page
{
    private R_Grid<GLL00100DTO> _gridRef;
    private readonly LookupGLL00100ViewModel _viewModel = new();

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            await _gridRef.R_RefreshGrid(poParameter);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GLL00100ParameterDTO)eventArgs.Parameter;
            await _viewModel.GetReferenceNoList(loParam);

            eventArgs.ListEntityResult = _viewModel.ReferenceNoGrid;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task Button_OnClickOkAsync()
    {
        var loData = _gridRef.GetCurrentData();
        await Close(true, loData);
    }

    public async Task Button_OnClickCloseAsync()
    {
        await Close(true, null);
    }
}