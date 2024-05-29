using Lookup_GLBACK;
using Lookup_GLCOMMON;
using Lookup_GLCOMMON.DTO;
using Lookup_GLCOMMON.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;

namespace Lookup_GLSERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class PublicLookupGLController : ControllerBase, IPublicLookupGL
    {
        [HttpPost]
        public GLLGenericList<GLL00100DTO> GLL00100ReferenceNoLookUp(GLL00100ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GLLGenericList<GLL00100DTO>? loRtn = null;

            try
            {
                var loCls = new PublicLookupGLCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CLANGUAGE = R_BackGlobalVar.CULTURE;

                var loResult = loCls.ReferenceNoLookUp(poParameter);

                loRtn = new GLLGenericList<GLL00100DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

#pragma warning disable CS8603 // Possible null reference return.
            return loRtn;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
