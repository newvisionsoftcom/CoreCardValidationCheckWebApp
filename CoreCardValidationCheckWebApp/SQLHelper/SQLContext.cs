﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCardValidationCheckWebApp.Models;

namespace CoreCardValidationCheckWebApp.SQLHelper
{
    public class SQLContext: DbContext
    {
        public SQLContext() { }
        public SQLContext(DbContextOptions<SQLContext> options) : base(options) { }
        public DbSet<Models.CategoryModel> CategoryModel { get; set; }
    }
}
