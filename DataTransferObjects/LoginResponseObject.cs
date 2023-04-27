using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
   public class LoginResponseObject
    {
        public LoginResponseObject(int Id)
        {
            this.Id = Id;
            Truth = true;
        }
        public LoginResponseObject(Boolean Truth)
        {
            this.Truth = Truth;
            Id = 0;
        }
        public int Id { get; set; }
        public Boolean Truth { get; set; }

    }
}
