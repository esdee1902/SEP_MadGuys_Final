using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEP_Team1.API
{
    public class Members
    {
        public int code { get; set; }
        public List<Member> data { get; set; }
        public String message;

    }
}