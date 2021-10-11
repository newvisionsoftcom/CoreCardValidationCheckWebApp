using ScriptLib.Helper;
using ScriptLib.Models;
using ScriptLib.SQLHelper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ScriptLib.ScriptClasses;
using ScriptLib;


namespace CoreCardValidationCheckWebApp.Controllers
{
    public class ValidationStepsController : Controller
    {
        private readonly ILogger<ExistingProcedureController> _logger;
        private readonly ISQLDapper _sqlDapper;        

        DynamicParameters para = new DynamicParameters();
        ValidationStepsModel result = new ValidationStepsModel();
        static Response responseResult;
        ValidationSteps objValidationSteps;

        public ValidationStepsController(ILogger<ExistingProcedureController> logger, ISQLDapper dapper)
        {
            _logger = logger;
            _sqlDapper = dapper;
            objValidationSteps = new ValidationSteps(_sqlDapper);
            responseResult = new Response();
        }

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewBag.TaskActivityName_Sort = String.IsNullOrEmpty(sortOrder) ? "TaskActivityName_Sort" : "";
            ViewBag.CategoryName_Sort = String.IsNullOrEmpty(sortOrder) ? "CategoryName_Sort" : "";

            var datagride = LoadValidationSteps().AsQueryable();

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

        //public IActionResult Index_ok()
        //{
        //    var result = LoadValidationSteps();
        //    return View(result);
        //}

        private List<ValidationStepsModel> LoadValidationSteps()           
        {
            return objValidationSteps.LoadValidationSteps();
        }

        [HttpPost]
        public ActionResult GetValidationSteps(int Id)
        {            
            if (Id != 0)
            {               
               result = objValidationSteps.GetValidationSteps(Id);
            }
            result.CategoryNameList = objValidationSteps.GetCategoryList();
            result.ComplexietyList = objValidationSteps.GetComplexietyList();
            result.ExistingProcedureList = objValidationSteps.GetExistingProcedureList();
            return PartialView("_AddValidationStepsView", result);
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            responseResult = objValidationSteps.Delete(Id);
            return Json(new { success = responseResult.Status, responseText = responseResult.Message });
        }

        [HttpPost]
        public ActionResult InsertUpdate(ValidationStepsModel objModel)
        {
            responseResult = objValidationSteps.InsertUpdate(objModel);
            return Json(new { success = responseResult.Status, responseText = responseResult.Message });
        }
    }
}
