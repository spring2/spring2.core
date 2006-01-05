
using Microsoft.Build;
using Microsoft.Build.Framework;
using Microsoft.Build.Shared;
using Microsoft.Build.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Text;


namespace Spring2.Core.MSBuild.CustomTask {
    public class ExecuteSqlScriptFolder : Task {
        //private string sqlCmdExe = "sqlcmd.exe";
        private string sqlFlags = "";
        private string sqlUser = "";
        private string sqlPassword = "";
        private string sqlDatabase = "";
        private string sqlServer = "";
        private string sqlScriptPath = "";

        //public string SqlCmdExe {
        //    get { return sqlCmdExe; }
        //    set { sqlCmdExe = value; }
        //}

        public string SqlFlags {
            get { return sqlFlags; }
            set { sqlFlags = value; }
        }

        [Required]
        public string SqlUser {
            get { return sqlUser; }
            set { sqlUser = value; }
        }

        [Required]
        public string SqlPassword {
            get { return sqlPassword; }
            set { sqlPassword = value; }
        }

        [Required]
        public string SqlDatabase {
            get { return sqlDatabase; }
            set { sqlDatabase = value; }
        }

        [Required]
        public string SqlServer {
            get { return sqlServer; }
            set { sqlServer = value; }
        }

        [Required]
        public string SqlScriptPath {
            get { return sqlScriptPath; }
            set { sqlScriptPath = value; }
        }

        public override bool Execute() {
            bool result = true;
            ArrayList pendingSqlScripts = GetSqlScriptsToProcess(sqlScriptPath, ".sql", ".log");

            foreach (string s in pendingSqlScripts) {
                string cnString = "";
                cnString = "server=" + sqlServer + "; database=" + sqlDatabase + "; user id=" + sqlUser + "; password=" + sqlPassword + "  ";
                if (RunSqlCmd(cnString, s)) {
                    WriteLogFile(s);
                }
            }

            return result;
        }

        private void WriteLogFile(string script) {
            StreamWriter logFile = File.CreateText(script.Replace(".sql", ".log"));
            string logContents = script + " executed successfully at " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            logFile.Write(logContents);
            logFile.Flush();
            logFile.Close();
        }

        private ArrayList GetSqlScriptsToProcess(string path, string scriptExt, string logExt) {
            string[] sqlFiles = Directory.GetFiles(path, "*" + scriptExt);
            string[] logFiles = Directory.GetFiles(path, "*" + logExt);

            ArrayList sqlToDoFiles = new ArrayList();

            foreach (string s in sqlFiles) {
                DateTime lastTouched = File.GetLastWriteTime(s);
                DateTime lastLogged = lastTouched.AddDays(-1);
                try {
                    lastLogged = File.GetLastWriteTime(s.Replace(scriptExt, logExt));
                } catch {
                    lastLogged = lastTouched;
                }
                if (lastTouched >= lastLogged) {
                    sqlToDoFiles.Add(s);
                }
            }
            return sqlToDoFiles;
        }

        private bool RunSqlCmd(string cnString, string scriptFile) {
            bool result = true;
            try {
                SqlConnection cn = new SqlConnection(cnString);
                cn.Open();
                try {
                    SqlConnection.ClearPool(cn);

                    StreamReader sqlScript = File.OpenText(scriptFile);
                    string tmpSql = sqlScript.ReadToEnd();
                    sqlScript.Close();
                    string[] separators = {"GO\r\n", "go\r\n"};
                    string[] individualScripts = tmpSql.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    SqlCommand cmd;

                    foreach (string s in individualScripts) {
                        SqlTransaction trans = cn.BeginTransaction("ExecutingSqlScriptTask");
                        try {
                            cmd = new SqlCommand(s, cn, trans);
                            int rows = cmd.ExecuteNonQuery();
                            trans.Commit();
                        } catch (Exception ex) {
                            Debug.Write("MSBuild task : ExecuteSqlScriptFolder - failed with exception : " + ex.Message);
                            trans.Rollback("ExecutingSqlScriptTask");
                            result = false;
                        }
                    }

                } catch (Exception ex2) {
                    Debug.Write("Failed to properly read or split the SQL script.", ex2.Message);
                    result = false; 
                }
            } catch (Exception ex3){
                Debug.Write("Failed to establish a connection to the SQL server.", ex3.Message);
                result = false;
            }
            return result;
        }

    }
}
