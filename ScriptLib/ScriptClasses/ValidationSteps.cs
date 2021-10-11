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
    public class ValidationSteps
    {
        private readonly ISQLDapper _sqlDapper;
        DynamicParameters para = new DynamicParameters();
        ValidationStepsModel result = new ValidationStepsModel();
        Response responseResult = new Response();
        int resultQueryStatus = 0;

        public ValidationSteps(ISQLDapper dapper)
        {
            _sqlDapper = dapper;
        }       
        public List<CategoryModel> GetCategoryList()
        {
            para.Add("Action", "GetCategoryList");
            List<CategoryModel> listCat = _sqlDapper.GetAll<CategoryModel>(Constants.SVValidationSteps, para);
            return listCat;
        }
        public List<ComplexietyModel> GetComplexietyList()
        {
            para.Add("Action", "GetComplexietyList");
            List<ComplexietyModel> listComplex = _sqlDapper.GetAll<ComplexietyModel>(Constants.SVValidationSteps, para);
            return listComplex;
        }
        public List<ExistingProcedureModel> GetExistingProcedureList()
        {
            para.Add("Action", "GetExistingProcedureList");
            List<ExistingProcedureModel> listExitPro = _sqlDapper.GetAll<ExistingProcedureModel>(Constants.SVValidationSteps, para);
            return listExitPro;
        }
        public List<ValidationStepsModel> LoadValidationSteps()
        {
            para.Add("Action", "GelAll");
            var result = _sqlDapper.GetAll<ValidationStepsModel>(Constants.SVValidationSteps, para);
            return (result);
        }
        public ValidationStepsModel GetValidationSteps(int Id)
        {
            if (Id != 0)
            {
                para.Add("Action", "GelById");
                para.Add("TaskActivityId", Id);
                result = _sqlDapper.Get<ValidationStepsModel>(Constants.SVValidationSteps, para);
            }
            return result;
        }
        public Response InsertUpdate(ValidationStepsModel objModel)
        {
            para.Add("@GroupId", objModel.GroupId);
            para.Add("@TaskActivityId", objModel.TaskActivityId);
            para.Add("@TaskActivityName", objModel.TaskActivityName);
            para.Add("@CategoryId", objModel.CategoryId);
            para.Add("@ComplexietyId", objModel.ComplexietyId);
            para.Add("@ExistingProcedureId", objModel.ExistingProcedureId);
            para.Add("@FrequencyId", objModel.FrequencyId);

            //Insert Record
            if (objModel.TaskActivityId == 0)
            {
                para.Add("Action", "GelById");
                result = _sqlDapper.Get<ValidationStepsModel>(Constants.SVValidationSteps, para);
                if (result != null)
                {
                    responseResult.Message = "Record is alredy present";
                    responseResult.Status = false;
                }
                else
                {
                    para.Add("Action", "Insert");
                    resultQueryStatus = _sqlDapper.Create<ValidationStepsModel>(Constants.SVValidationSteps, para);
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
                resultQueryStatus = _sqlDapper.Create<ValidationStepsModel>(Constants.SVValidationSteps, para);
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
            para.Add("TaskActivityId", Id);

            resultQueryStatus = _sqlDapper.Create<ValidationStepsModel>(Constants.SVValidationSteps, para);
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
