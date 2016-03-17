using IMG_DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMG_BusinessEntities;
namespace IMG_BusinessServices
{
   public  class LoginService:ILoginService
    {

       private IVisaCardDAO _visaCArdDao;

       public LoginService(IVisaCardDAO _visaCArdDao)
       {
           this._visaCArdDao = _visaCArdDao;
       }

     


        public int RegisterUser(Users user)
        {
            return _visaCArdDao.RegisterUser(user);
        }

        public Users ValidateUser(string userName, string password)
        {          
            return _visaCArdDao.ValidateUser(userName, password);         
        }


        public bool ProcessUser(Users user, string userName, string password)
        {
            Boolean validUser;
            if (user.userName == userName && user.password == password)
            {
                validUser = true;
            }
            else
            {
                validUser = false;
            }
            return validUser;
        }
    }
}
