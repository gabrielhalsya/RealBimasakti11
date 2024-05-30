using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace PMR00200COMMON.Print_DTO
{
    public class PMR00200PrintDisplayDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public PMR00200SummaryColumnDTO Column { get; set; }
        public PMR00200PrintParamDTO HeaderParam{ get; set; }
        public List<PMR00200DataResultDTO> Data { get; set; }

    }
    //public class PMR01001PrintResultDTO
    //{
    //    public string Title { get; set; }
    //    public string Header { get; set; }
    //    public PMR01001PrintColoumnDTO Column { get; set; }
    //    public PMR01000PrintParamDTO HeaderParam { get; set; }
    //    public List<PMR01001DataResultDTO> Data { get; set; }

    //    public List<PMR01000ResultPrintSPDTO> DataResult { get; set; }

    //    public PMR01000PrintParamDTO Param { get; set; }
    //}

}
