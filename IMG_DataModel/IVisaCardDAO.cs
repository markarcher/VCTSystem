using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMG_BusinessEntities;

namespace IMG_DataModel
{
    public interface IVisaCardDAO
    {
        List<VisaCard> CheckIfAgentCodeAlreadyExists(List<VisaCard> visaCardList);

        int SaveNewMember(VisaCard visaCard);
    }
}
