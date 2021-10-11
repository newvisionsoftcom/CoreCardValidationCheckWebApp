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
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ISQLDapper _sqlDapper;

        DynamicParameters para = new DynamicParameters();
        CategoryModel result = new CategoryModel();
        static Response responseResult;
        Category objCategory;
        public CategoryController(ILogger<CategoryController> logger, ISQLDapper dapper)
        {
            _logger = logger;
            _sqlDapper = dapper;
            objCategory = new Category(_sqlDapper);
            responseResult = new Response();
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
            return View(datagride.ToList());   
        }

        private List<CategoryModel> LoadCategory()
        {                       
            return objCategory.LoadCategory();
        }

        [HttpPost]
        public ActionResult GetCategory(int Id)
        {
            if (Id != 0)
            {
                result = objCategory.GetCategory(Id);
            }
            return PartialView("_AddEditCategoryView", result);
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {          
            responseResult = objCategory.Delete(Id);
            return Json(new { success = responseResult.Status, responseText = responseResult.Message });
        }

        [HttpPost]
        public ActionResult InsertUpdate(CategoryModel objModel)
        {
            responseResult = objCategory.InsertUpdate(objModel);    
            return Json(new { success = responseResult.Status, responseText = responseResult.Message });
        }       
    }
}
