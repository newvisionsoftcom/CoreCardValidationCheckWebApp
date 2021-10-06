using CoreCardValidationCheckWebApp.Helper;
using CoreCardValidationCheckWebApp.Models;
using CoreCardValidationCheckWebApp.SQLHelper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PagedList;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreCardValidationCheckWebApp.Controllers
{
    public class ValidationStepsController : Controller
    {
        private readonly ILogger<ExistingProcedureController> _logger;
        private readonly ISQLDapper _sqlDapper;

        DynamicParameters para = new DynamicParameters();
        string responseText = "";
        Boolean success = true;
        ValidationStepsModel result = new ValidationStepsModel();

        public ValidationStepsController(ILogger<ExistingProcedureController> logger, ISQLDapper dapper)
        {
            _logger = logger;
            _sqlDapper = dapper;
        }

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewBag.TaskActivityName_Sort = String.IsNullOrEmpty(sortOrder) ? "TaskActivityName_Sort" : "";
            ViewBag.CategoryName_Sort = String.IsNullOrEmpty(sortOrder) ? "CategoryName_Sort" : "";

            var datagride = LoadExistingProcedure().AsQueryable();

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
 
            if (!String.IsNullOrEmpty(searchString))
            {
                datagride = datagride.Where(s => s.TaskActivityName.Contains(searchString)
                                      || s.CategoryName.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                datagride = datagride.Where(s => s.TaskActivityName.Contains(searchString)
                                      || s.CategoryName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "TaskActivityName_Sort":
                    datagride = datagride.OrderByDescending(s => s.TaskActivityName);
                    break;
                    
                case "CategoryName_Sort":
                    datagride = datagride.OrderByDescending(s => s.CategoryName);
                    break;                  
            }
             return View(datagride.ToList());   // ok

            //int pageSize = 3;
            //int pageNumber1 = (pageNumber ?? 1);
            //return View(datagride.ToPagedList(pageNumber1, pageSize));

            //int pageSize = 3;
            //return View(PaginatedList<ValidationStepsModel>.CreateAsync(datagride, pageNumber ?? 1, pageSize));
        }

        public IActionResult Index_ok()
        {
            var result = LoadExistingProcedure();
            return View(result);
        }

        private List<ValidationStepsModel> LoadExistingProcedure()           
        {
            // var para = new DynamicParameters();           
            para.Add("Action", "GelAll");

            var result = _sqlDapper.GetAll<ValidationStepsModel>(Constants.SVValidationSteps, para);
            return (result);
        }

        [HttpPost]
        public ActionResult GetExistingProcedureById(int Id)
        {            
            if (Id != 0)
            {
                para.Add("Action", "GelById");
                para.Add("@TaskActivityId", Id);
                result = _sqlDapper.Get<ValidationStepsModel>(Constants.SVValidationSteps, para);
            }

            para.Add("Action", "GetCategoryList");          
            List<CategoryModel> listCat = _sqlDapper.GetAll<CategoryModel>(Constants.SVValidationSteps, para);
            result.CategoryNameList = listCat;

            para.Add("Action", "GetComplexietyList");
            List<ComplexietyModel> listComplex = _sqlDapper.GetAll<ComplexietyModel>(Constants.SVValidationSteps, para);
            result.ComplexietyList = listComplex;

            para.Add("Action", "GetExistingProcedureList");
            List<ExistingProcedureModel> listExitPro = _sqlDapper.GetAll<ExistingProcedureModel>(Constants.SVValidationSteps, para);
            result.ExistingProcedureList = listExitPro;

            return PartialView("_AddValidationStepsView", result);
        }

        [HttpPost]
        public ActionResult DeleteById(int Id)
        {
            para.Add("Action", "Delete");
            para.Add("@TaskActivityId", Id);
            result = _sqlDapper.Update<ValidationStepsModel>(Constants.SVValidationSteps, para);
            //var resultView = LoadExistingProcedure();
            //return View(resultView);

            return Json(new { success = true, responseText = "Record Deleted" });
        }

        [HttpPost]
        public ActionResult Update(ValidationStepsModel fm)
        {
            para.Add("@GroupId", fm.GroupId);
            para.Add("@TaskActivityId", fm.TaskActivityId);
            para.Add("@TaskActivityName", fm.TaskActivityName);
            para.Add("@CategoryId", fm.CategoryId);
            para.Add("@ComplexietyId", fm.ComplexietyId);
            para.Add("@ExistingProcedureId", fm.ExistingProcedureId);
            para.Add("@FrequencyId", fm.FrequencyId);
       
            //Insert Record
            if (fm.TaskActivityId == 0)
            {
                para.Add("Action", "GelById");
                result = _sqlDapper.Get<ValidationStepsModel>(Constants.SVValidationSteps, para);
                if (result != null)
                {
                    responseText = "Record is alredy present";
                    success = false;
                }
                else
                {
                    para.Add("Action", "Insert");
                    result = _sqlDapper.Update<ValidationStepsModel>(Constants.SVValidationSteps, para);
                    responseText = "Inserted successfully";
                }
            }
            else
            {   //Update Record
                para.Add("Action", "Update");
                result = _sqlDapper.Update<ValidationStepsModel>(Constants.SVValidationSteps, para);

                responseText = "Updated successfully";
            }
            return Json(new { success = success, responseText = responseText });
        }
    }
}
