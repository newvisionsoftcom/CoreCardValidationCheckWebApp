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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CoreCardValidationCheckWebApp.Controllers
{
    public class ValitationScriptsController : Controller
    {
        private readonly ILogger<ValitationScriptsController> _logger;
        private readonly ISQLDapper _sqlDapper;

        DynamicParameters para = new DynamicParameters();
        string responseText = "";
        Boolean success = true;
        ValitationScriptsModel result = new ValitationScriptsModel();

        static Response responseResult;
        ValitationScripts objValitationScripts;

        public ValitationScriptsController(ILogger<ValitationScriptsController> logger, ISQLDapper dapper)
        {
            _logger = logger;
            _sqlDapper = dapper;
            objValitationScripts = new ValitationScripts(_sqlDapper);
            responseResult = new Response();
        }

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)    
        {
            
            ViewBag.TaskActivityName_Sort = String.IsNullOrEmpty(sortOrder) ? "TaskActivityName_Sort" : "";

            var datagride = LoadValitationScripts().AsQueryable();

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
                datagride = datagride.Where(s => s.TaskActivityName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "TaskActivityName_Sort":
                    datagride = datagride.OrderByDescending(s => s.TaskActivityName);
                    break;
            }
            return View(datagride.ToList());   // ok

            //int pageSize = 3;
            //int pageNumber1 = (pageNumber ?? 1);
            //return View(datagride.ToPagedList(pageNumber1, pageSize));

            //int pageSize = 3;
            //return View(PaginatedList<ValitationScriptsModel>.CreateAsync(datagride, pageNumber ?? 1, pageSize));
        }
        private List<ValitationScriptsModel> LoadValitationScripts()
        {
            return objValitationScripts.LoadValitationScripts();
        }

        [HttpPost]
        public ActionResult GetExistingProcedure(int Id)
        {
            if (Id != 0)
            {
                result = objValitationScripts.GetExistingProcedure(Id);
            }
            result.ValidationStepsList = objValitationScripts.GetValidationSteps();
            return PartialView("_AddValitationScriptsView", result);
        }

        // remove
        [HttpPost("FileUpload")]
        public async Task<IActionResult> UploadFileMethod2(List<IFormFile> files, ValitationScriptsModel data)
        {
            long size = files.Sum(f => f.Length);

            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    // full path to file in temp location
                    //  var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
                    var filePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/UploadFile",
                        formFile.FileName);

                    filePaths.Add(filePath);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            // process uploaded files          
            return Ok(new { count = files.Count, size, filePaths });
        }

        // [HttpPost("FileUpload")]
        // public IActionResult OnPostMyUploader(List<IFormFile> fileData, string data)  ok
        // public IActionResult OnPostMyUploader(List<IFormFile> fileData)
        public IActionResult OnPostMyUploader(List<IFormFile> fileData, ValitationScriptsModel data)
        {
            var dirpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadFile/" + data.TaskActivityId);

            if (fileData != null)
            {
                if (!Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);
                }
                var filePaths = new List<string>();
                foreach (var formFile in fileData)
                {
                    if (formFile.Length > 0)
                    {
                        // full path to file in temp location
                        //  var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
                        var filePath = Path.Combine(
                            dirpath,
                            formFile.FileName);

                        filePaths.Add(filePath);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            formFile.CopyToAsync(stream);
                        }
                    }

                    //para.Add("TaskActivityId", data.TaskActivityId);
                    //para.Add("ScriptId", data.ScriptId);
                    //para.Add("ScriptName", formFile.FileName);
                    //para.Add("ScriptPath", formFile.FileName);

                    //added 
                    data.ScriptName = formFile.FileName;
                    data.ScriptPath = formFile.FileName;
                    responseResult = objValitationScripts.InsertUpdate(data);

                    //if (data.ScriptId == 0)
                    //{
                    //    responseResult = objValitationScripts.InsertUpdate(data);
                    //    //return Json(new { success = responseResult.Status, responseText = responseResult.Message });

                    //    //para.Add("Action", "Insert");
                    //    //result = _sqlDapper.Insert<ValitationScriptsModel>(Constants.SVValitationScripts, para);
                    //}
                    //else
                    //{
                    //    responseResult = objValitationScripts.InsertUpdate(data);
                    //    //return Json(new { success = responseResult.Status, responseText = responseResult.Message });

                    //    //para.Add("Action", "Update");
                    //    //result = _sqlDapper.Update<ValitationScriptsModel>(Constants.SVValitationScripts, para);
                    //}
                }

                //ok
                //if (fileData != null)
                //{
                //    var filePaths = new List<string>();
                //    foreach (var formFile in fileData)
                //    {
                //        if (formFile.Length > 0)
                //        {
                //            // full path to file in temp location
                //            //  var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
                //            var filePath = Path.Combine(
                //                Directory.GetCurrentDirectory(), "wwwroot/UploadFile",
                //                formFile.FileName);

                //            filePaths.Add(filePath);
                //            using (var stream = new FileStream(filePath, FileMode.Create))
                //            {
                //                formFile.CopyToAsync(stream);
                //            }
                //        }
                //    }

                //return new ObjectResult(new { status = "success" });  ok 
            }

            //return new ObjectResult(new { status = "fail" }); ok 
            return new ObjectResult(new { status = responseResult.Status });
        }

        //[HttpPost]
        //public ActionResult InsertUpdate(ValitationScriptsModel objModel)
        //{
        //    responseResult = objValitationScripts.InsertUpdate(objModel);
        //    return Json(new { success = responseResult.Status, responseText = responseResult.Message });
        //}

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            responseResult = objValitationScripts.Delete(Id);
            return Json(new { success = responseResult.Status, responseText = responseResult.Message });

            //para.Add("Action", "Delete");
            //para.Add("@ScriptId", Id);
            //result = _sqlDapper.Update<ValitationScriptsModel>(Constants.SVValitationScripts, para);

            //return Json(new { success = true, responseText = "Record Deleted" });
        }

    }
}
