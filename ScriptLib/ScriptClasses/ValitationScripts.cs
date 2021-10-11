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
    public class ValitationScripts
    {
        private readonly ISQLDapper _sqlDapper;
        DynamicParameters para = new DynamicParameters();
        ValitationScriptsModel result = new ValitationScriptsModel();
        Response responseResult = new Response();
        int resultQueryStatus = 0;
        public ValitationScripts(ISQLDapper dapper)
        {
            _sqlDapper = dapper;
        }
        public List<ValitationScriptsModel> LoadValitationScripts()
        {
            para.Add("Action", "GelAll");
            var result = _sqlDapper.GetAll<ValitationScriptsModel>(Constants.SVValitationScripts, para);
            return result;
        }
        public ValitationScriptsModel GetExistingProcedure(int Id)
        {
            if (Id != 0)
            {
                para.Add("Action", "GelById");
                para.Add("@ScriptId", Id);
                result = _sqlDapper.Get<ValitationScriptsModel>(Constants.SVValitationScripts, para);
            }
            return result;
        }
        public List<ValidationStepsModel> GetValidationSteps()
        {
            para.Add("Action", "GetValidationStepsList");
            List<ValidationStepsModel> listCat = _sqlDapper.GetAll<ValidationStepsModel>(Constants.SVValitationScripts, para);
            return listCat;
        }

        public Response InsertUpdate(ValitationScriptsModel objModel)
        {
            para.Add("TaskActivityId", objModel.TaskActivityId);
            para.Add("ScriptId", objModel.ScriptId);
            para.Add("ScriptName", objModel.ScriptName);
            para.Add("ScriptPath", objModel.ScriptPath);

            //Insert Record
            if (objModel.ScriptId == 0)
            {
                //para.Add("Action", "GelById");
                //result = _sqlDapper.Get<ValitationScriptsModel>(Constants.SVValidationSteps, para);
                //if (result != null)
                //{
                //    responseResult.Message = "Record is alredy present";
                //    responseResult.Status = false;
                //}
                //else
                //{
                para.Add("Action", "Insert");
                resultQueryStatus = _sqlDapper.Create<ValitationScriptsModel>(Constants.SVValitationScripts, para);
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
                //}
            }
            else
            {   //Update Record
                para.Add("Action", "Update");
                resultQueryStatus = _sqlDapper.Create<ValitationScriptsModel>(Constants.SVValitationScripts, para);
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
            para.Add("ScriptId", Id);

            resultQueryStatus = _sqlDapper.Create<ValitationScriptsModel>(Constants.SVValitationScripts, para);
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
