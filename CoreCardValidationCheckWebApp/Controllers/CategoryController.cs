using CoreCardValidationCheckWebApp.Helper;
using CoreCardValidationCheckWebApp.Models;
using CoreCardValidationCheckWebApp.SQLHelper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCardValidationCheckWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ISQLDapper _sqlDapper;
        //public static readonly string Connectionstring = "User Id=sa;Password=newvision@123;Server=PARIKSHITT;Database=CoreCard ;";


        DynamicParameters para = new DynamicParameters();
        string responseText = "";
        Boolean success = true;
        CategoryModel result = new CategoryModel();

        public CategoryController(ILogger<CategoryController> logger, ISQLDapper dapper)
        {
            _logger = logger;
            _sqlDapper = dapper;
        }
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)       
        {
            ViewBag.CategoryName_Sort = String.IsNullOrEmpty(sortOrder) ? "CategoryName_Sort" : "";

            var datagride = LoadCategory().AsQueryable();

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
                datagride = datagride.Where(s => s.CategoryName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "CategoryName_Sort":
                    datagride = datagride.OrderByDescending(s => s.CategoryName);
                    break;
            }
            return View(datagride.ToList());   // ok
        }

        private List<CategoryModel> LoadCategory()
        {
            para.Add("Action", "GelAll");

            var result = _sqlDapper.GetAll<CategoryModel>(Constants.SVCategory, para);
            return (result);
        }

        [HttpPost]
        public ActionResult GetCategoryById(int Id)
        {
            if (Id != 0)
            {
                para.Add("Action", "GelById");
                para.Add("@CategoryId", Id);
                result = _sqlDapper.Get<CategoryModel>(Constants.SVCategory, para);
            }

            return PartialView("_AddEditCategoryView", result);
        }

        [HttpPost]
        public ActionResult DeleteById(int Id)
        {
            para.Add("Action", "Delete");
            para.Add("@CategoryId", Id);
            result = _sqlDapper.Update<CategoryModel>(Constants.SVCategory, para);
            return Json(new { success = true, responseText = "Record Deleted" });
        }

        //remove
        [HttpPost]
        public ActionResult DeleteCategoryById(int Id)
        {
            var para = new DynamicParameters();
            para.Add(Constants.CategoryId, Id);
            var result = _sqlDapper.Delete(Constants.SVGetCategory_DEL, para);


            var resultView = LoadCategory();
            return View(resultView);
        }


        [HttpPost]
        public ActionResult Update(ValidationStepsModel fm)
        {
            para.Add(Constants.CategoryId, fm.CategoryId);
            para.Add(Constants.CategoryName, fm.CategoryName);

            //Insert Record
            if (fm.CategoryId == 0)
            {
                para.Add("Action", "GelById");
                result = _sqlDapper.Get<CategoryModel>(Constants.SVCategory, para);
                if (result != null)
                {
                    responseText = "Record is alredy present";
                    success = false;
                }
                else
                {
                    para.Add("Action", "Insert");
                    result = _sqlDapper.Update<CategoryModel>(Constants.SVCategory, para);
                    responseText = "Inserted successfully";
                }
            }
            else
            {   //Update Record
                para.Add("Action", "Update");
                result = _sqlDapper.Update<CategoryModel>(Constants.SVCategory, para);

                responseText = "Updated successfully";
            }
            return Json(new { success = success, responseText = responseText });
        }

        // remove 
        [HttpPost]
        public ActionResult UpdateCategory(CategoryModel fm)
        {
            //if (ModelState.IsValid)
            //{
            //Write your database insert code / activities  
            //return RedirectToAction("Index");
            //var result = _sqlDapper.GetAll<CategoryModel>(Constants.SVGetCategoryList, new Dapper.DynamicParameters());
            //return View(result);

            var para = new DynamicParameters();
            string responseText = "";
            Boolean success = true;

            if (fm.CategoryId == 0)
            {
                para.Add(Constants.CategoryId, fm.CategoryId);
                para.Add(Constants.CategoryName, fm.CategoryName);
                var recordpresent = _sqlDapper.Get<CategoryModel>(Constants.SVCategoryGetById, para);
                if (recordpresent != null)
                {
                    responseText = "Record is alredy present!";
                    success = false;                }
                else
                {
                    para.Add(Constants.CategoryId, fm.CategoryId);
                    para.Add(Constants.CategoryName, fm.CategoryName);
                    var result = _sqlDapper.Update<CategoryModel>(Constants.SVGetCategory_UPS, para);
                    responseText = "Your message successfuly sent!";
                }
            }
            else
            {
                // var para = new DynamicParameters(); 
                para.Add(Constants.CategoryId, fm.CategoryId);
                para.Add(Constants.CategoryName, fm.CategoryName);
                var result = _sqlDapper.Update<CategoryModel>(Constants.SVGetCategory_UPS, para);

                responseText = "Your message successfuly sent!";
            }
            return Json(new { success = success, responseText = responseText });
        }
    }
}
