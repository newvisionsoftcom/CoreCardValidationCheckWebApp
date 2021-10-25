using ScriptLib.Helper;
using ScriptLib.Models;
using ScriptLib.SQLHelper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ScriptLib.ScriptClasses;
using ScriptLib;

namespace CoreCardValidationCheckWebApp.Controllers
{
    public class ExistingProcedureController : Controller
    {
        private readonly ILogger<ExistingProcedureController> _logger;
        private readonly ISQLDapper _sqlDapper;

        DynamicParameters para = new DynamicParameters();    
        ExistingProcedureModel result = new ExistingProcedureModel();
        static Response responseResult;
        ExistingProcedure objExistingProcedure;
        public ExistingProcedureController(ILogger<ExistingProcedureController> logger, ISQLDapper dapper)
        {
            _logger = logger;
            _sqlDapper = dapper;
            objExistingProcedure = new ExistingProcedure(_sqlDapper);
            responseResult = new Response();
        }

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewBag.ExistingProcedureName_Sort = String.IsNullOrEmpty(sortOrder) ? "ExistingProcedureName_Sort" : "";            

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
                datagride = datagride.Where(s => s.ExistingProcedureName.ToLower().Contains(searchString.ToLower()));
            }

            switch (sortOrder)
            {
                case "ExistingProcedureName_Sort":
                    datagride = datagride.OrderByDescending(s => s.ExistingProcedureName);
                    break;
            }
            return View(datagride.ToList());  
        }
        private List<ExistingProcedureModel> LoadExistingProcedure()
        {
            return objExistingProcedure.LoadExistingProcedure();
        }

        [HttpPost]
        public ActionResult GetExistingProcedure(int Id)
        {
            if (Id != 0)
            {
                result = objExistingProcedure.GetExistingProcedure(Id);
            }
            return PartialView("_AddEditExistingProcedureView", result);
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            responseResult = objExistingProcedure.Delete(Id);
            return Json(new { success = responseResult.Status, responseText = responseResult.Message });
        }

        [HttpPost]
        public ActionResult InsertUpdate(ExistingProcedureModel objModel)
        {
            responseResult = objExistingProcedure.InsertUpdate(objModel);
            return Json(new { success = responseResult.Status, responseText = responseResult.Message });
        }
    }
}
