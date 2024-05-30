using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00200COMMON.Print_DTO
{
    public class PMR00200DataResultDTO
    {
        public string CTRANS_CODE { get; set; }
        public string CTRANS_NAME { get; set; }

        public List<PMR00200DataRes1> Detail1 { get; set; }

    }
}
