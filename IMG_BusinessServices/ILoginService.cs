using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMG_BusinessEntities;
namespace IMG_BusinessServices
{
   public interface ILoginService
    {
        Users ValidateUser(string userName, string password);

        int RegisterUser(Users user);

        bool ProcessUser(Users user, string userName, string password);
    }
}
