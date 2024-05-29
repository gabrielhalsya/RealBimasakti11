using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Lookup_GLCOMMON.DTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_GLModel.ViewModel.GLL00100
{
    public class LookupGLL00100ViewModel : R_ViewModel<GLL00100DTO>
    {
        private readonly PublicLookupModel _model = new PublicLookupModel();
        public ObservableCollection<GLL00100DTO> ReferenceNoGrid = new ObservableCollection<GLL00100DTO>();

        public async Task GetReferenceNoList(GLL00100ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GLL00100ReferenceNoLookUpAsync(poParam);

                ReferenceNoGrid = new ObservableCollection<GLL00100DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}