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
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Text;


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
            //throw new Exception("Custome error");
            //throw new Exception();
            ViewBag.TaskActivityName_Sort = String.IsNullOrEmpty(sortOrder) ? "TaskActivityName_Sort" : "";   
            List<ValidationStepsModel> lstValidationSteps = new List<ValidationStepsModel>();
            lstValidationSteps = objValidationStepsTracking.GetValidationSteps();

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
                lstValidationSteps = lstValidationSteps.Where(s => s.TaskActivityName.ToLower().Contains(searchString.ToLower())).ToList();
            }
            switch (sortOrder)
            {
                case "TaskActivityName_Sort":
                    lstValidationSteps = lstValidationSteps.OrderByDescending(s => s.TaskActivityName).ToList();
                    break;                
            }
            result.ValidationStepsList = lstValidationSteps;
            return View(result);
        }      

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

        // Call from UI to take multiple script ID's and other details to execute 
        [HttpPost]
        public ActionResult RunScripts(ValidationStepsTrackingModel valdata)    
        {
            List<ValitationScriptsModel> listValitationScripts = new List<ValitationScriptsModel>();
            List<int> ScriptIdValues = new List<int>();
            List<ValidationStepsTrackingModel> lstValStepTrack = new List<ValidationStepsTrackingModel>();

            foreach (int TaskActivityId in valdata.TaskActivityIdValues)
            {
                listValitationScripts = objValidationStepsTracking.GetValidationScript_ByValidationStepsID(TaskActivityId);

                if (listValitationScripts != null)
                {
                    foreach (var obj in listValitationScripts)
                    {
                        ValidationStepsTrackingModel objValStepTrack = new ValidationStepsTrackingModel();
                        objValStepTrack.ScriptId = obj.ScriptId;
                        objValStepTrack.TaskActivityId = obj.TaskActivityId;
                        objValStepTrack.ScriptName = obj.ScriptName;
                        lstValStepTrack.Add(objValStepTrack);
                    }
                }
            }

            foreach (var obj in lstValStepTrack)
            {
                ExecuteSingleScript(obj);               
            }
            return Json(new { success = "success", responseText = responseResult.Message });
        }


        // Call from UI to take Single script execute and also from code for multiple script 
        public void ExecuteSingleScript(ValidationStepsTrackingModel objModel)
        {         
            //  Add entry for new script
            objModel.Status = "Processing";          
            objValidationStepsTracking.SaveScriptResult(objModel);

            // Execute script and take retuen message and pass to save method for saving response
            string scriptResult = RunPowerScript(objModel);           
            Random rnd = new Random();
            Int32 number = rnd.Next(0, 8);
            Thread.Sleep(1000 * 1 * number);            

            //  Update script time and status message 
            objModel.Status = "Finished";
            objModel.PawerShellScriptResponse = scriptResult;
            objValidationStepsTracking.SaveScriptResult(objModel);
        }

        // Code to execute script file
        public string RunPowerScript(ValidationStepsTrackingModel objModel)
        {
            var CurrentDirectory = Directory.GetCurrentDirectory() + "\\wwwroot\\UploadFile\\"+ objModel.TaskActivityId + "\\";       
            string scriptpath = Path.Combine(CurrentDirectory, objModel.ScriptName);
            var process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.FileName = @"C:\windows\system32\windowspowershell\v1.0\powershell.exe";
            process.StartInfo.Arguments = "\"&'" + scriptpath + "'\"";
            process.Start();
            string scriptoutput = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return scriptoutput;
        }

        public ActionResult DownloadReport()
        {
            List<ValidationStepsTrackingModel> lstValidationStepsTracking = new List<ValidationStepsTrackingModel>();
            lstValidationStepsTracking = objValidationStepsTracking.GetReport();

            StringBuilder str = new StringBuilder();
            str.Append("<table>");    
            str.Append("<tr>");
            str.Append("<td><b>Script Report</b></td>");        
            str.Append("</tr>");
            str.Append("<tr></br></br></tr>");
            str.Append("</table>");

            str.Append("<table border=`" + "1px" + "`b>");
            str.Append("<tr>");
            str.Append("<td><b>TaskActivityName</b></td>");
            str.Append("<td><b>ScriptName</b></td>");
            str.Append("<td><b>PowerShellScriptResponse</b></td>");
            str.Append("<td><b>Start Time</b></td>");
            str.Append("<td><b>End Time</b></td>");
            str.Append("<td><b>Status</b></td>");
            str.Append("</tr>");

            foreach (ValidationStepsTrackingModel val in lstValidationStepsTracking)
            {
                str.Append("<tr>");
                str.Append("<td>" + val.TaskActivityName.ToString() + "</td>");
                str.Append("<td>" + val.ScriptName.ToString() + "</td>");
                str.Append("<td>" + val.PawerShellScriptResponse.ToString() + "</td>");
                str.Append("<td>" + val.StartTimeStamp.ToString() + "</td>");
                str.Append("<td>" + val.EndTimestamp.ToString() + "</td>");
                str.Append("<td>" + val.Status.ToString() + "</td>");
                str.Append("</tr>");
            }
            str.Append("</table>");
            byte[] temp = System.Text.Encoding.UTF8.GetBytes(str.ToString());
            return File(temp, "application/vnd.ms-excel", "ScriptReport.xlsx");
        }


        //ok 
        //public string RunPowerScript_old()
        //{

        //    var CurrentDirectory = Directory.GetCurrentDirectory() + "\\wwwroot\\UploadFile\\1\\";

        //    //if (File.Exists("PowerShell1.ps1"))
        //   // {
        //     //   File.GetAttributes("PowerShell1.ps1");
        //        string strCmdText = Path.Combine(CurrentDirectory, "PowerShell1.ps1");


        //        string strCmdText1 = Path.Combine(@"D:\PowerShell1.ps1");

        //        var process = new Process();
        //        process.StartInfo.UseShellExecute = false;
        //        process.StartInfo.RedirectStandardOutput = true;
        //        process.StartInfo.FileName = @"C:\windows\system32\windowspowershell\v1.0\powershell.exe";
        //        process.StartInfo.Arguments = "\"&'" + strCmdText + "'\"";

        //        process.Start();
        //        string output = process.StandardOutput.ReadToEnd();
        //        process.WaitForExit();
        //        //using (StreamWriter outfile = new StreamWriter("StandardOutput.txt", true))
        //        //{
        //        //    outfile.Write(s);
        //        //}

        //    return output;

        //    //}
        //}
    }
}
