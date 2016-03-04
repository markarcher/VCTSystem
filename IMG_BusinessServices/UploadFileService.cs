using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
   public class UploadFileService: IUploadFileService
    {
       public string GetExcelConnection(string fileLocation, string fileExtension)
       {
           string excelConnectionString = string.Empty;

           excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                   fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
           //connection String for xls file format.
           if (fileExtension == ".xls")
           {
               excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
               fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
           }
           //connection String for xlsx file format.
           else if (fileExtension == ".xlsx")
           {
               excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
               fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
           }else if(fileExtension == ".ods"){
               excelConnectionString = "ods";
           }
           return excelConnectionString;
       }

       public OleDbConnection GetOleDbConnection(string excelConnectionString)
       {      
           OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
           if (excelConnection.State != ConnectionState.Open)
           {
               try
               {
                   excelConnection.Open();
               }
               catch (OleDbException )
               {
                   throw; 
               }
           }
           return excelConnection;
       }

       public String[] getTableNames(OleDbConnection excelConnection, string excelConnectionString)
       {
           DataTable dt = new DataTable();

           dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
           if (dt == null)
           {
               return null;
           }

           String[] excelSheets = new String[dt.Rows.Count];
           int t = 0;
           //excel data saves in temp file here.
           foreach (DataRow row in dt.Rows)
           {
               excelSheets[t] = row["TABLE_NAME"].ToString();
               t++;
           }
           return excelSheets;
       }

       public DataSet queryExcelTables(string excelConnectionString, string sheetName)
       {
           OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
           DataSet ds = new DataSet();

          
               string query = string.Format("Select * from [{0}]", sheetName);
               using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
               {
                   dataAdapter.Fill(ds);
               }

            
          excelConnection1.Close();
           return ds;

       }
    }
}
