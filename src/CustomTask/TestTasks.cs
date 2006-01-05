using System;
using System.Collections.Generic;
using System.Text;

namespace Spring2.Core.MSBuild.CustomTask {
    class TestTasks {

        private void TestExecuteSqlScriptFolder() {
            ExecuteSqlScriptFolder testTask = new ExecuteSqlScriptFolder();
            testTask.SqlUser = "sa";
            testTask.SqlPassword = "1qaz2wsx";
            testTask.SqlDatabase = "JustMenus";
            testTask.SqlServer = "BAUXITE";
            testTask.SqlScriptPath = "c:\\Data\\work\\seamlessweb\\justmenus\\src\\sql\\proc\\";
            testTask.Execute();
        }
    }
}
