﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02400COMMON.DTO_s.Print
{
    public class PMR02400ReportDataDTO
    {
        public string Title {  get; set; }
        public string Header { get; set; }
        public PMR02400LabelDTO Label { get; set; }
        public PMR02400ParamDTO Param { get; set; }
        public List<PMR02400DataDTO> Data { get; set; }
    }
}
