using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00200COMMON.Print_DTO
{
    public class PMR00200DataRes1
    {
        public string CSALESMAN_ID { get; set; }
        public string CSALESMAN_NAME { get; set; }
        public List<PMR00200DataRes2> Detail2 { get; set; }
    }
}
