using ScriptLib;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using ScriptLib.Models;
using ScriptLib.SQLHelper;
using ScriptLib.Helper;

namespace ScriptLib.ScriptClasses
{
    public class ValidationStepsTracking
    {
        private readonly ISQLDapper _sqlDapper;
        DynamicParameters para = new DynamicParameters();
        ValidationStepsTrackingModel result = new ValidationStepsTrackingModel();
        Response responseResult = new Response();
        int resultQueryStatus = 0;
        public ValidationStepsTracking(ISQLDapper dapper)
        {
            _sqlDapper = dapper;
        }
        public List<ValidationStepsModel> GetValidationSteps()
        {
            para.Add("Action", "GetValidationSteps");
            var result = _sqlDapper.GetAll<ValidationStepsModel>(Constants.SVValidationStepsTracking, para);
            return (result);
        }
        public List<ValitationScriptsModel> GetValidationScript_ByValidationStepsID(int Id)
        {
            para.Add("Action", "GetValidationScript_ByValidationStepsID");
            para.Add("TaskActivityId", Id);
            var result = _sqlDapper.GetAll<ValitationScriptsModel>(Constants.SVValidationStepsTracking, para);
            return (result);
        }
    }
}
