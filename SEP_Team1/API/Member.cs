using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEP_Team1.API
{
    public class Member
    {
        public string Id { get; set; }
        public string fullname { get; set; }
        public string birthday { get; set; }
        public string firstname { get; set; }
        public string lastname
        {
            get; set;
        }
    }
}