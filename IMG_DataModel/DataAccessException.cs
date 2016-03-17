using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMG_DataModel
{
    class DataAccessException:Exception
    {
        public DataAccessException()
        {

        }
        public DataAccessException(string message)
            : base(message)
        {
           
        }
        public DataAccessException(string message, Exception inner)
            : base(message, inner)
        {

        }

    }
}
