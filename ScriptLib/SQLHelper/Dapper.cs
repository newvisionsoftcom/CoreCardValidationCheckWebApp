using Dapper;
using Microsoft.Extensions.Configuration;
using ScriptLib.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
//using CoreCardValidationCheckWebApp.Helper;
using System.Configuration;

namespace ScriptLib.SQLHelper
{
    public class SQLDapper : ISQLDapper
    {
        private readonly IConfiguration _config;
        private string Connectionstring = Constants.SQLDBConnectionString;
        private const string connstring = "Data Source=PARIKSHITT;Initial Catalog=CoreCard;User ID=sa;Password=newvision@123";
        public SQLDapper(IConfiguration config)
        {
            _config = config;
        }
        public void Dispose()
        {

        }

        public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            //using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));

            using IDbConnection db = new SqlConnection(connstring);
            return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
        }

        //public List<T> GetAll<T>(string sp, DynamicParameters parms=null, CommandType commandType = CommandType.StoredProcedure)
        public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            // using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            //using IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection1"].ConnectionString);
            //
            //string con = ConfigurationManager.AppSettings["DefaultConnection2"].ConnectionString();
            //using IDbConnection db = new SqlConnection(con);

            // using IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection1"].ConnectionString);

            //using IDbConnection db = new SqlConnection("Data Source=PARIKSHITT;Initial Catalog=CoreCard;User ID=sa;Password=newvision@123");

            using IDbConnection db = new SqlConnection(connstring);
            return db.Query<T>(sp, parms, commandType: commandType).ToList();
        }

        public int Delete(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(connstring);
            //return db.Query<int>(sp, parms, commandType: commandType).Count();

            return db.Execute(sp, parms, commandType: commandType);
        }


        //public DbConnection GetDbconnection()
        //{
        //    return new SqlConnection(_config.GetConnectionString(Connectionstring));
        //}

        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(connstring);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }


        public int Create<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            int result;
            using IDbConnection db = new SqlConnection(connstring);
            result = db.Execute(sp, parms, commandType: commandType);
            return result;
        }

        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(connstring);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }
    }
}

