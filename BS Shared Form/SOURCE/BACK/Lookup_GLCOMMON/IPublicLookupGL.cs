using Lookup_GLCOMMON.DTO;
using Lookup_GLCOMMON.DTOs;

namespace Lookup_GLCOMMON
{
    public interface IPublicLookupGL
    {
        GLLGenericList<GLL00100DTO> GLL00100ReferenceNoLookUp(GLL00100ParameterDTO poParameter);
    }
}