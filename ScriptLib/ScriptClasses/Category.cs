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
    public class Category
    {
        private readonly ISQLDapper _sqlDapper;
        DynamicParameters para = new DynamicParameters();       
        CategoryModel result = new CategoryModel();
        Response responseResult = new Response();
        int resultQueryStatus = 0;

        public Category(ISQLDapper dapper)
        {         
            _sqlDapper = dapper;
        }
        public List<CategoryModel> LoadCategory()
        {
            para.Add("Action", "GelAll");
            var result = _sqlDapper.GetAll<CategoryModel>(Constants.SVCategory, para);
            return (result);
        }
        public CategoryModel GetCategory(int Id)
        {
            if (Id != 0)
            {
                para.Add("Action", "GelById");
                para.Add("CategoryId", Id);
                result = _sqlDapper.Get<CategoryModel>(Constants.SVCategory, para);
            }
            return result;
        }
        public Response InsertUpdate(CategoryModel objModel)
        {
            para.Add("CategoryId", objModel.CategoryId);
            para.Add("CategoryName", objModel.CategoryName);

            //Insert Record
            if (objModel.CategoryId == 0)
            {
                para.Add("Action", "GelById");
                result = _sqlDapper.Get<CategoryModel>(Constants.SVCategory, para);
                if (result != null)
                {
                    responseResult.Message = "Record is alredy present";
                    responseResult.Status = false;
                }
                else
                {
                    para.Add("Action", "Insert");                    
                    resultQueryStatus = _sqlDapper.Create<CategoryModel>(Constants.SVCategory, para);
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
                resultQueryStatus = _sqlDapper.Create<CategoryModel>(Constants.SVCategory, para);
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
            para.Add("CategoryId", Id);         

            resultQueryStatus = _sqlDapper.Create<CategoryModel>(Constants.SVCategory, para);
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
