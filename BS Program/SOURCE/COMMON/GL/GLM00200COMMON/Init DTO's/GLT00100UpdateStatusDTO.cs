using System;
using System.Collections.Generic;
using System.Text;

namespace GLM00200Common
{
    public class GLM00200UpdateStatusDTO
    {
        public string CREC_ID { get; set; }
        public string CNEW_STATUS { get; set; }
        public bool LAUTO_COMMIT { get; set; }
        public bool LUNDO_COMMIT { get; set; }
    }
}
