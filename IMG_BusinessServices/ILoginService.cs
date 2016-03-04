using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMG_BusinessServices
{
   public interface ILoginService
    {
        void ValidateUser(string userName, string password);
    }
}
