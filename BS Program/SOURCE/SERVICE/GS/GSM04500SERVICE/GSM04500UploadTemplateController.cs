using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GSM04500Back;
using GSM04500Common;
using GSM04500Common.Logs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace GSM04500Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM04500UploadTemplateController : ControllerBase, IGSM04500Template 
    {
        private LoggerGSM04500 _loggerGSM04500;

        public GSM04500UploadTemplateController(ILogger<GSM04500UploadTemplateController> logger)
        {
            LoggerGSM04500.R_InitializeLogger(logger);
            _loggerGSM04500 = LoggerGSM04500.R_GetInstanceLogger();
        }

        [HttpPost]
        public UploadFileDTO DownloadTemplateFile()
        {
            string lcMethodName = nameof(DownloadTemplateFile);
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loException = new R_Exception();
            var loRtn = new UploadFileDTO();

            try
            {
                Assembly loAsm = Assembly.Load("BIMASAKTI_GS_API");
                var lcResourceFile = "BIMASAKTI_GS_API.Template.Journal Group.xlsx";

                using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                {
                    var ms = new MemoryStream();
                    resFilestream.CopyTo(ms);
                    var bytes = ms.ToArray();

                    loRtn.FileBytes = bytes;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM04500.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _loggerGSM04500.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));

            return loRtn;
        }

    }
}
