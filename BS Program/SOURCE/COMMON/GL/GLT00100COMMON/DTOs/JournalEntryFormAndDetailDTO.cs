using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00100Common.DTOs
{
    public class JournalEntryFormAndDetailDTO
    {
        public GLT00100DTO MainData { get; set; }
        public List<GLT00100JournalGridDetailDTO> DetailList { get; set; }
    }
}
