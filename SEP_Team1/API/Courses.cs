using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEP_Team1.API
{
    public class Courses
    {
        public int code { get; set; }
        public List<Course> data { get; set; }
        public String message;


    }
}