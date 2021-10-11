using ScriptLib;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using ScriptLib.Models;
using ScriptLib.SQLHelper;
using ScriptLib.Helper;

namespace ScriptLib.ScriptClasses
{  
    public class ExistingProcedure
    {
        private readonly ISQLDapper _sqlDapper;
        DynamicParameters para = new DynamicParameters();
        ExistingProcedureModel result = new ExistingProcedureModel();
        Response responseResult = new Response();
        int resultQueryStatus = 0;

        public ExistingProcedure(ISQLDapper dapper)
        {
            _sqlDapper = dapper;
        }
        public List<ExistingProcedureModel> LoadExistingProcedure()
        {
            para.Add("Action", "GelAll");
            var result = _sqlDapper.GetAll<ExistingProcedureModel>(Constants.SVExistingProcedure, para);
            return (result);
        }
        public ExistingProcedureModel GetExistingProcedure(int Id)
        {
            if (Id != 0)
            {
                para.Add("Action", "GelById");
                para.Add("ExistingProcedureId", Id);
                result = _sqlDapper.Get<ExistingProcedureModel>(Constants.SVExistingProcedure, para);
            }
            return result;
        }
        public Response InsertUpdate(ExistingProcedureModel objModel)
        {
            para.Add("ExistingProcedureId", objModel.ExistingProcedureId);
            para.Add("ExistingProcedureName", objModel.ExistingProcedureName);

            //Insert Record
            if (objModel.ExistingProcedureId == 0)
            {
                para.Add("Action", "GelById");
                result = _sqlDapper.Get<ExistingProcedureModel>(Constants.SVExistingProcedure, para);
                if (result != null)
                {
                    responseResult.Message = "Record is alredy present";
                    responseResult.Status = false;
                }
                else
                {
                    para.Add("Action", "Insert");
                    resultQueryStatus = _sqlDapper.Create<ExistingProcedureModel>(Constants.SVExistingProcedure, para);
                    if (resultQueryStatus == 1)
                    {
                        responseResult.Message = Constants.InsertMessage;
                        responseResult.Status = true;
                    }
                    else
                    {
                        responseResult.Message = Constants.FailMessage;
                        responseResult.Status = false;
                    }
                }
            }
            else
            {   //Update Record
                para.Add("Action", "Update");
                resultQueryStatus = _sqlDapper.Create<ExistingProcedureModel>(Constants.SVExistingProcedure, para);
                if (resultQueryStatus == 1)
                {
                    responseResult.Message = Constants.UpdateMessage;
                    responseResult.Status = true;
                }
                else
                {
                    responseResult.Message = Constants.FailMessage;
                    responseResult.Status = false;
                }
            }
            return responseResult;
        }
        public Response Delete(int Id)
        {
            para.Add("Action", "Delete");
            para.Add("ExistingProcedureId", Id);

            resultQueryStatus = _sqlDapper.Create<ExistingProcedureModel>(Constants.SVExistingProcedure, para);
            if (resultQueryStatus == 1)
            {
                responseResult.Message = Constants.DeleteMessage;
                responseResult.Status = true;
            }
            else
            {
                responseResult.Message = Constants.FailMessage;
                responseResult.Status = false;
            }
            return responseResult;
        }
    }
}
