using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptLib.SQLHelper
{
    public interface ISQLDapper : IDisposable
    {
        //T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);  ok
        //List<T> GetAll<T>(string sp, DynamicParameters parms=null, CommandType commandType = CommandType.StoredProcedure);
        //List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure); ok
        int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        int Delete(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        //object Delete<T>(string sVCategoryGetById, DynamicParameters para);



        //Use below 
        T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        // For Insert,Update,Delete
        int Create<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
    }
}
