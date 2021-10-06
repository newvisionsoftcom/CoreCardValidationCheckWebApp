using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCardValidationCheckWebApp.Helper
{
    public class Constants
    {

        //Properties
        public const string CategoryName = "CategoryName";
        public const string CategoryId = "CategoryId";

        public const string Action = "";

        // SQL
        public const string SQLDBConnectionString = "DefaultConnection";
        
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

        //
    }
}
