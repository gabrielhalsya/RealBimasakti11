﻿using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM04500Common
{
    public class UploadFileDTO : R_APIResultBaseDTO
    {
        public byte[] FileBytes { get; set; }
    }
}
