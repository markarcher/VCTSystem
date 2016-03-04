using IMG_BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMG_DataModel;

namespace IMG_BusinessServices
{
   public  class VisaCardService:IVisaCardService
    {
       private IVisaCardDAO _visaCardDAO;

       public VisaCardService(IVisaCardDAO _visaCardDAO)
       {
           this._visaCardDAO = _visaCardDAO;
       }


       public List<VisaCard> CheckIfAgentCodeAlreadyExists(List<VisaCard> visaCardList)
       {         
            return _visaCardDAO.CheckIfAgentCodeAlreadyExists(visaCardList);
        }

       public List<VisaCard> filterAgentCodes(List<VisaCard> listOfExistingAgentCodes, List<VisaCard> visaCardList)
       {
           List<VisaCard> listOfFilteredAgentCodes = new List<VisaCard>();
           foreach (var item in visaCardList)
           {
               foreach(var existingAgentCode in listOfExistingAgentCodes ){
                   if(item.AgentCode == existingAgentCode.AgentCode){
                       listOfFilteredAgentCodes.Add(item);
                   }

               }
           }
           return listOfFilteredAgentCodes;
       }


       public int SaveNewMember(VisaCard visaCard)
       {       
           VisaCardDAO visaCardDao = new VisaCardDAO();
           return _visaCardDAO.SaveNewMember(visaCard);        
       }

       public int SaveListOfNewMember(List<VisaCard> visaCardList)
       {
           int noOfRowsAffected=0;
           VisaCardDAO visaCardDao = new VisaCardDAO();
           for (int i = 0; i < visaCardList.Count; i++)
           {             
               int rows = visaCardDao.SaveNewMember(visaCardList[i]);
               noOfRowsAffected += rows;
           }

           return noOfRowsAffected;
       }
    }

   
}
