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
    public class ValidationStepsTrackingController : Controller
    {
        private readonly ILogger<ExistingProcedureController> _logger;
        private readonly ISQLDapper _sqlDapper;

        DynamicParameters para = new DynamicParameters();
        ValidationStepsTrackingModel result = new ValidationStepsTrackingModel();
        static Response responseResult;
        ValidationStepsTracking objValidationStepsTracking;

        public ValidationStepsTrackingController(ILogger<ExistingProcedureController> logger, ISQLDapper dapper)
        {
            _logger = logger;
            _sqlDapper = dapper;
            objValidationStepsTracking = new ValidationStepsTracking(_sqlDapper);
            responseResult = new Response();
        }
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewBag.TaskActivityName_Sort = String.IsNullOrEmpty(sortOrder) ? "TaskActivityName_Sort" : "";
            ViewBag.CategoryName_Sort = String.IsNullOrEmpty(sortOrder) ? "CategoryName_Sort" : "";

            List<ValidationStepsModel> lstValidationSteps = new List<ValidationStepsModel>();

            lstValidationSteps = GetValidationSteps();

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
                lstValidationSteps = lstValidationSteps.Where(s => s.TaskActivityName.Contains(searchString)
                                      || s.CategoryName.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "TaskActivityName_Sort":
                    lstValidationSteps = lstValidationSteps.OrderByDescending(s => s.TaskActivityName).ToList();
                    break;

                case "CategoryName_Sort":
                    lstValidationSteps = lstValidationSteps.OrderByDescending(s => s.CategoryName).ToList();
                    break;
            }
            result.ValidationStepsList = lstValidationSteps;
            return View(result);   
        }
        private List<ValidationStepsModel> GetValidationSteps()
        {
            return objValidationStepsTracking.GetValidationSteps();
        }

        //private List<ValitationScriptsModel> GetValidationScript_ByValidationStepsID(int Id)
        //{
        //    return objValidationStepsTracking.GetValidationScript_ByValidationStepsID(Id);
        //}

        [HttpPost]
        public ActionResult GetValidationScript_ByValidationStepsID(int Id)
        {
            List<ValitationScriptsModel> listValitationScripts = new List<ValitationScriptsModel>();

            if (Id != 0)
            {
                listValitationScripts = objValidationStepsTracking.GetValidationScript_ByValidationStepsID(Id);
            }      
            return PartialView("_ValitationScriptsView", listValitationScripts);
        }
    }
}
