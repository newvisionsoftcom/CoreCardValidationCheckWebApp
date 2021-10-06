using CoreCardValidationCheckWebApp.Helper;
using CoreCardValidationCheckWebApp.Models;
using CoreCardValidationCheckWebApp.SQLHelper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCardValidationCheckWebApp.Controllers
{
    public class ExistingProcedureController : Controller
    {
        private readonly ILogger<ExistingProcedureController> _logger;
        private readonly ISQLDapper _sqlDapper;

        DynamicParameters para = new DynamicParameters();
        string responseText = "";
        Boolean success = true;
        ExistingProcedureModel result = new ExistingProcedureModel();

        public ExistingProcedureController(ILogger<ExistingProcedureController> logger, ISQLDapper dapper)
        {
            _logger = logger;
            _sqlDapper = dapper;
        }

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            //var result = LoadExistingProcedure();
            //return View(result);
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
                datagride = datagride.Where(s => s.ExistingProcedureName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ExistingProcedureName_Sort":
                    datagride = datagride.OrderByDescending(s => s.ExistingProcedureName);
                    break;
            }
            return View(datagride.ToList());   // ok
        }
        private List<ExistingProcedureModel> LoadExistingProcedure()
        {
           // var para = new DynamicParameters();           
            para.Add("Action", "GelAll");

            var result = _sqlDapper.GetAll<ExistingProcedureModel>(Constants.SVExistingProcedure, para);
            return result;
        }

        [HttpPost]
        public ActionResult GetExistingProcedureById(int Id)
        {
            //var result = new ExistingProcedureModel();
            if (Id != 0)
            {
               // var para = new DynamicParameters();
                para.Add("Action", "GelById");
                para.Add("@ExistingProcedureId", Id);
                result = _sqlDapper.Get<ExistingProcedureModel>(Constants.SVExistingProcedure, para);
            }
            return PartialView("_AddEditExistingProcedureView", result);
        }

        [HttpPost]
        public ActionResult DeleteById(int Id)
        {
            para.Add("Action", "Delete");
            para.Add("@ExistingProcedureId", Id);
            result = _sqlDapper.Update<ExistingProcedureModel>(Constants.SVExistingProcedure, para);

            //var resultView = LoadExistingProcedure();
            //return View(resultView);

            return Json(new { success = true, responseText = "Record Deleted" });
        }

        [HttpPost]
        public ActionResult Update(ExistingProcedureModel fm)
        {
            //var para = new DynamicParameters();
            //string responseText = "";
            //Boolean success = true;
            //var result = new ExistingProcedureModel();

            para.Add("ExistingProcedureId", fm.ExistingProcedureId);
            para.Add("ExistingProcedureName", fm.ExistingProcedureName);

            //Insert Record
            if (fm.ExistingProcedureId == 0)
            {
                para.Add("Action", "GelById");
                result = _sqlDapper.Get<ExistingProcedureModel>(Constants.SVExistingProcedure, para);
                if (result != null)
                {
                    responseText = "Record is alredy present";
                    success = false;
                }
                else
                {
                    para.Add("Action", "Insert");
                    result = _sqlDapper.Update<ExistingProcedureModel>(Constants.SVExistingProcedure, para);
                    responseText = "Inserted successfully";
                }
            }
            else
            {   //Update Record
                para.Add("Action", "Update");
                result = _sqlDapper.Update<ExistingProcedureModel>(Constants.SVExistingProcedure, para);

                responseText = "Updated successfully";
            }
            return Json(new { success = success, responseText = responseText });
        }
    }
}
