using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using IMG_BusinessServices;
using BusinessServices;
using IMG_BusinessEntities;
using IMG_DataModel;
using System.Data.SqlClient;


namespace IMG_TFS.Controllers
{
    public class VisaController : Controller
    {

        private IUploadFileService _uploadFileService;
        private IVisaCardService _visaCardService;
        public VisaController(IUploadFileService _uploadFileService, IVisaCardService _visaCardService)
        {
            this._uploadFileService = _uploadFileService;
            this._visaCardService = _visaCardService;
           
        }

      

        public ActionResult UploadExcelFile()
        {
            return View();
        }
        // GET: /Visa/
         [HttpPost]
        public ActionResult ProcessExcelFile(HttpPostedFileBase NAMEbtnUpload)
        {
            DataSet ds = new DataSet();
            string excelConnectionString = null;
             String[] ExcelSheetNames = new String[0];
             try
             {
                 if (NAMEbtnUpload.ContentLength > 0) 
                 {
                     string fileExtension = System.IO.Path.GetExtension(Request.Files["NAMEbtnUpload"].FileName);
                     string fileLocation = FileLocation(fileExtension);

                     excelConnectionString = _uploadFileService.GetExcelConnection(fileLocation, fileExtension);
                     if (excelConnectionString != "ods")
                     {
                         OleDbConnection oleDbExcelCon = null;
                         try
                         {
                             oleDbExcelCon = _uploadFileService.GetOleDbConnection(excelConnectionString);
                             ExcelSheetNames = _uploadFileService.getTableNames(oleDbExcelCon, excelConnectionString);
                             oleDbExcelCon.Close();
                         }
                         catch (Exception e)
                         {
                             Response.Write("<script>alert('" + Server.HtmlEncode(e.Message) + "')</script>");
                             Console.WriteLine(e);
                         }


                         UploadFile(fileExtension, ds);
                         TempData["SheetNames"] = ExcelSheetNames;
                     }

                 }
               
             }catch(Exception e){
                 Console.WriteLine(e);
                 //return RedirectToAction("AgentSupport");
             }

             return RedirectToAction("ProcessExcelSheet", new { ExcelConnString = excelConnectionString });
        }

         public ActionResult Visa()
         {
             return View();
         }


         public ActionResult ProcessExcelSheet(string ExcelConnString)
         {
             if (ExcelConnString == "ods") {
                 ViewBag.ods = "yes";
             }
             else
             {
                 var ExcelShtNames = (string[])TempData["SheetNames"];
                 Session["conn"] = ExcelConnString;
                 ViewBag.selection = ExcelShtNames;
                 int ExcelShtNamesLength = ExcelShtNames.Length;
                 if (ExcelShtNamesLength == 0)
                 {
                     ViewBag.NoSheetName = "yes";
                 }
             }
             return View();
         }


         public ActionResult AgentSupport()
         {
            
             return View();
         }

         public ActionResult Renewal()
         {
             return View();
         }

         public ActionResult ApplicationDetails()
         {
             return View();
         }

         public ActionResult EditDetails()
         {
             return View();
         }

         public ActionResult AccountingVerification()
         {
             return View();
         }
         public ActionResult DisplayExcelContents(string RadioButtonSheetSelect)
         {
             List<VisaCard> visaCardList = new List<VisaCard>();
             if (RadioButtonSheetSelect != null)
             {
                 DataSet ds = new DataSet();
              
                 string myConnectionStr = Session["conn"] as String;
                 ds = _uploadFileService.queryExcelTables(myConnectionStr, RadioButtonSheetSelect);

                

                 for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                 {

                     if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i][4].ToString()))
                     {
                        

                         VisaCard visaCard = new VisaCard
                         {
                             Account_No = ds.Tables[0].Rows[i][3].ToString(),
                             AgentCode = ds.Tables[0].Rows[i][4].ToString(),
                             LastName = ds.Tables[0].Rows[i][5].ToString(),
                             FirstName = ds.Tables[0].Rows[i][6].ToString(),
                             Addr1 = ds.Tables[0].Rows[i][7].ToString(),
                             Phone1 = ds.Tables[0].Rows[i][11].ToString(),
                             Phone2 = ds.Tables[0].Rows[i][12].ToString(),
                             Sex = ds.Tables[0].Rows[i][13].ToString(),
                             Birthday = ds.Tables[0].Rows[i][14].ToString(), 
                             TITLE_CODE = ds.Tables[0].Rows[i][33].ToString(),
                             MotherMaidenName = ds.Tables[0].Rows[i][34].ToString(),
                             MiddleName = ds.Tables[0].Rows[i][35].ToString(),
                             Birthplace = ds.Tables[0].Rows[i][36].ToString(),
                             MaritalStatus = ds.Tables[0].Rows[i][37].ToString(),
                             SpouseName = ds.Tables[0].Rows[i][38].ToString(),
                             PERMANENT_ADD1 = ds.Tables[0].Rows[i][41].ToString(),
                             ID_TYPE = ds.Tables[0].Rows[i][44].ToString(),
                             ID_NO = ds.Tables[0].Rows[i][45].ToString(),
                             TIN = ds.Tables[0].Rows[i][48].ToString(),
                             SSS = ds.Tables[0].Rows[i][49].ToString(),
                             OccupationDesc = ds.Tables[0].Rows[i][50].ToString(),
                             OccupationCode = ds.Tables[0].Rows[i][51].ToString(),
                             CurrentEmployer = ds.Tables[0].Rows[i][52].ToString(),
                             Nationality = ds.Tables[0].Rows[i][56].ToString(),
                             SourceOfFunds = ds.Tables[0].Rows[i][57].ToString(),
                         };
                         visaCardList.Add(visaCard);
                     }
                 }
               
               
             }
            List<VisaCard> listOfExistingAgentCodes = _visaCardService.CheckIfAgentCodeAlreadyExists(visaCardList);
            List<VisaCard> listOfNewVCApplication =  new List<VisaCard>();
            if (listOfExistingAgentCodes.Count != 0)
            {
                listOfNewVCApplication = _visaCardService.filterAgentCodes(listOfExistingAgentCodes, visaCardList);
                ViewBag.ListOfExistingAgents = "List of Existing Agents";              
            }
            else
            {
                listOfNewVCApplication.AddRange(visaCardList);
            }

            int noOfRowsAffected = _visaCardService.SaveListOfNewMember(listOfNewVCApplication);
            ViewBag.listOfAgentCodes = listOfExistingAgentCodes;
            ViewBag.list = listOfNewVCApplication;
         
            return View();
         }


         public ActionResult NewMembership()
         {
             return View();
         }


         public ActionResult SaveContentsOfExcelFile(List<VisaCard> listOfitems)
         {
           
             ViewBag.Name = listOfitems[0];
             return View();
         }

         public ActionResult VisaApplicationStatusReport()
         {
             return View();
         }


         public ActionResult CashCardApplication(FormCollection collection)
         {
             int noOfRowsAffected = 0;
             string firstName = collection["firstName"];
             string lastName = collection["lastName"];
             string middleName = collection["middleName"];
             VisaCard visaCard = new VisaCard
             {
                 LastName = lastName,
                 FirstName = firstName,
                 MiddleName = middleName
             };
             noOfRowsAffected = _visaCardService.SaveNewMember(visaCard);
             ViewBag.rowsAffected = noOfRowsAffected;
             ViewBag.lastName = visaCard.FirstName;
             return View();
         }

         private string FileLocation(string fileExtension)
        {
            string fileLocation = null;
            if (fileExtension == ".xls" || fileExtension == ".xlsx")
            {
                 fileLocation = Server.MapPath("~/Content/") + Request.Files["NAMEbtnUpload"].FileName;

                if (System.IO.File.Exists(fileLocation))
                {
                    System.IO.File.Delete(fileLocation);
                }
                Request.Files["NAMEbtnUpload"].SaveAs(fileLocation);             
            }
              
            return fileLocation;
        }

         private void UploadFile(string fileExtension, DataSet ds)
         {
             if (fileExtension.ToString().ToLower().Equals(".xml"))
             {
                 string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                 if (System.IO.File.Exists(fileLocation))
                 {
                     System.IO.File.Delete(fileLocation);
                 }
                 Request.Files["FileUpload"].SaveAs(fileLocation);
                 XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                 ds.ReadXml(xmlreader);
                 xmlreader.Close();
             }
         }
    }
}
