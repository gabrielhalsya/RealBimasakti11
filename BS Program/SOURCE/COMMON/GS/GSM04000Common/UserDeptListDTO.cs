using R_APICommonDTO;
using System.Collections.Generic;

namespace GSM04000Common
{
    public class UserDeptListDTO: R_APIResultBaseDTO
    {
        public IAsyncEnumerable<GSM04100DTO> Data { get; set; }
    }
}