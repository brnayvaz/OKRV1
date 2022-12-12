using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Okr.Web.Models
{
    public class UserBusModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDay { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set;}
    }
}