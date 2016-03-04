using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IUploadFileService
    {
        string GetExcelConnection(string fileLocation, string fileExtension);

        OleDbConnection GetOleDbConnection(string excelConnectionString);

        String[] getTableNames(OleDbConnection oleDbExcelCon, string excelConnectionString);

        DataSet queryExcelTables(string excelConnectionString, string RadioButtonSheetSelect);
    }
}
