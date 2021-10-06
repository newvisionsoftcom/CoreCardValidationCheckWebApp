using CoreCardValidationCheckWebApp.Helper;
using CoreCardValidationCheckWebApp.Models;
using CoreCardValidationCheckWebApp.SQLHelper;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        public ValitationScriptsController(ILogger<ValitationScriptsController> logger, ISQLDapper dapper)
        {
            _logger = logger;
            _sqlDapper = dapper;
        }

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        // public IActionResult Index()
        {
            //var result = LoadValitationScripts();
            //return View(result);
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
            // var para = new DynamicParameters();           
            para.Add("Action", "GelAll");

            var result = _sqlDapper.GetAll<ValitationScriptsModel>(Constants.SVValitationScripts, para);
            return result;
        }


        [HttpPost]
        public ActionResult GetExistingProcedureById(int Id)
        {
            if (Id != 0)
            {
                para.Add("Action", "GelById");
                para.Add("@ScriptId", Id);
                result = _sqlDapper.Get<ValitationScriptsModel>(Constants.SVValitationScripts, para);
            }

            para.Add("Action", "GetValidationStepsList");
            List<ValidationStepsModel> listCat = _sqlDapper.GetAll<ValidationStepsModel>(Constants.SVValitationScripts, para);
            result.ValidationStepsList = listCat;

            //para.Add("Action", "GetComplexietyList");
            //List<ComplexietyModel> listComplex = _sqlDapper.GetAll<ComplexietyModel>(Constants.SVValidationSteps, para);
            //result.ComplexietyList = listComplex;

            //para.Add("Action", "GetExistingProcedureList");
            //List<ExistingProcedureModel> listExitPro = _sqlDapper.GetAll<ExistingProcedureModel>(Constants.SVValidationSteps, para);
            //result.ExistingProcedureList = listExitPro;

            ////para.Add("Action", "GetFrequencyList");
            ////List<FrequencyModel> listFreq = _sqlDapper.GetAll<FrequencyModel>(Constants.SVValidationSteps, para);
            ////result.FrequencyList = listFreq;

            return PartialView("_AddValitationScriptsView", result);
        }

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

                    para.Add("TaskActivityId", data.TaskActivityId);
                    para.Add("ScriptId", data.ScriptId);
                    para.Add("ScriptName", formFile.FileName);
                    para.Add("ScriptPath", formFile.FileName);

                    if (data.ScriptId == 0)
                    {
                        para.Add("Action", "Insert");
                        result = _sqlDapper.Insert<ValitationScriptsModel>(Constants.SVValitationScripts, para);
                    }
                    else
                    {
                        para.Add("Action", "Update");
                        result = _sqlDapper.Update<ValitationScriptsModel>(Constants.SVValitationScripts, para);
                    }
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

                return new ObjectResult(new { status = "success" });
            }
            return new ObjectResult(new { status = "fail" });

        }

        [HttpPost]
        public ActionResult DeleteById(int Id)
        {
            para.Add("Action", "Delete");
            para.Add("@ScriptId", Id);
            result = _sqlDapper.Update<ValitationScriptsModel>(Constants.SVValitationScripts, para);

            return Json(new { success = true, responseText = "Record Deleted" });
        }

    }
}
