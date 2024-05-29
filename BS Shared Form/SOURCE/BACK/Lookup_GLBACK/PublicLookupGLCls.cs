using Lookup_GLCOMMON.DTO;
using R_BackEnd;
using R_Common;
using System.Data;
using System.Data.Common;

namespace Lookup_GLBACK
{
    public class PublicLookupGLCls
    {
        public List<GLL00100DTO> ReferenceNoLookUp(GLL00100ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<GLL00100DTO>? loResult = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {

                loDb = new();
                loCommand = loDb.GetCommand();
                lcQuery = "RSP_GL_GET_SYSTEM_PARAM";
                loCommand.CommandType = CommandType.StoredProcedure;
                loCommand.CommandText = lcQuery;


                loDb.R_AddCommandParameter(loCommand,"@CUSER_ID",DbType.String,10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand,"@CCOMPANY_ID",DbType.String,8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand,"@CTRANS_CODE",DbType.String,6, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand,"@CPERIOD",DbType.String,6, poEntity.CPERIOD);
                loDb.R_AddCommandParameter(loCommand,"@CLANGUAGE_ID",DbType.String,2, poEntity.CLANGUAGE);


                loResult = loDb.SqlExecObjectQuery<GLL00100DTO>(lcQuery, loDb.GetConnection("R_DefaultConnectionString"), true);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

#pragma warning disable CS8603 // Possible null reference return.
            return loResult;
#pragma warning restore CS8603 // Possible null reference return.
        }

    }
}
