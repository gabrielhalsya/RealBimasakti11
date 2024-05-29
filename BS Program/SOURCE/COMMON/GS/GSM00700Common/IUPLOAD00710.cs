using System;
using System.Collections.Generic;
using System.Text;
using GSM00710Common.DTO.Upload_DTO_GSM00710;

namespace GSM00700Common
{
    public interface IUPLOAD00710
    {
        IAsyncEnumerable<GSM00710UploadDTO> GetUploadListGSM00710();
        IAsyncEnumerable<GSM00710UploadErrorValidateDTO> GetErrorListGSM00710();
    }
}
