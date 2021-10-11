using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptLib.Helper
{
    public class Constants
    {
        // public const int resultQueryStatus = 0;
        public const string InsertMessage = "Record Inserted Successfully";
        public const string UpdateMessage = "Record Updated Successfully";
        public const string DeleteMessage = "Record Deleted Successfully";
        public const string FailMessage = "Fail";

        //Properties
        public const string CategoryName = "CategoryName";
        public const string CategoryId = "CategoryId";

        public const string Action = "";

        // SQL
        public const string SQLDBConnectionString = "DefaultConnection";
       // public const string SQLDBConnectionString = "Data Source=PARIKSHITT;Initial Catalog=CoreCard;User ID=sa;Password=newvision@123";
        
        
        //Category
        public const string SVCategoryGetById = "SVCategoryGetById";
        public const string SVGetCategoryList = "SVGetCategoryList";
        public const string SVGetCategory_UPS = "SVGetCategory_UPS";
        public const string SVGetCategory_DEL = "SVGetCategory_DEL";



        //Category Table
        public const string SVCategory = "SVCategory";

        //Existing Procedure Table
        public const string SVExistingProcedure = "SVExistingProcedure";

        //Existing Procedure Table
        public const string SVValidationSteps = "SVValidationSteps";

        //Valitation Scripts Table
        public const string SVValitationScripts = "SVValitationScripts";

        //Validation StepsTracking Table
        public const string SVValidationStepsTracking = "SVValidationStepsTracking";
                

        //
    }
}
