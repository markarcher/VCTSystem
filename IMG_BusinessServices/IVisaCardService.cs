using IMG_BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IMG_BusinessServices
{
   public  interface IVisaCardService
    {

       int SaveNewMember(VisaCard visaCard);

       int SaveListOfNewMember(List<VisaCard> visaCardList);

       List<VisaCard> CheckIfAgentCodeAlreadyExists(List<VisaCard> visaCardList);

       List<VisaCard> filterAgentCodes(List<VisaCard> listOfExistingAgentCodes, List<VisaCard> visaCardList);
    }
}
