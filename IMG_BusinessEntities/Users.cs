using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMG_BusinessEntities
{
    public class Users
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string fullName { get; set; }
        public string department { get; set; }
        public int roleId { get; set; }
    }
}
