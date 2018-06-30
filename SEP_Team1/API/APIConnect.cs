using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using SEP_Team1.Models;

namespace SEP_Team1.API
{
    public class APIConnect
    {
        public string strConnection = System.Configuration.ConfigurationManager.ConnectionStrings["sep21t21ConnectionString"].ToString();
        private string urlAddress = "https://entool.azurewebsites.net/SEP21";
        private string urlConnect;
        private string data;
        public StudentInfos studentinfos = null;
        public Members members = null;
        public Logins logins = null;
        private string Url(string url)
        {
          
              HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }
                string data = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                return data;
            }
            return "";
        }

        public Logins Check_Login(string email, string pass)
        {
            //url address
            urlConnect = "https://entool.azurewebsites.net/SEP21/Login?Username={0}&Password={1}";
            urlConnect = string.Format(urlConnect, email, pass);

            data = Url(urlConnect);
            if (data != "")
            {
                //parse data json
                var login = new Login();

                //get data json type array

                try
                {
                    logins = JsonConvert.DeserializeObject<Logins>(data);


                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                //
                return logins;
            }
            urlConnect = "";
            return null;
        }
        public Members GetMember(string id)
        {
            //urlConnect = urlAddress + "/GetMembers?courseID={0}";
            // Get list student database

            //-------------------------------
            urlConnect = "https://entool.azurewebsites.net/SEP21/GetMembers?courseID={0}";
            urlConnect = string.Format(urlConnect, id);

            data = Url(urlConnect);
            if (data != "")
            {
                //tao noi luu tru vao model
                var member = new List<Member>();
                //parse data json
                //get data json type array

                try
                {
                    members = JsonConvert.DeserializeObject<Members>(data);

                    //foreach (var detail in liststudent)
                    //{
                    //    foreach (var item in members.data)
                    //    {
                    //        if (detail.MSSV == item.Id)
                    //        {
                    //            countdelete++;
                    //        }
                    //    }
                    //    if (countdelete==0)
                    //    {
                    //        db.SinhViens.DeleteOnSubmit(detail);

                    //    }
                    //    countdelete = 0;
                    //}

                    //try
                    //{
                    //    db.SubmitChanges();
                    //}
                    //catch (Exception e)
                    //{
                    //    Console.WriteLine(e);

                    //}

                    //foreach (var item in members.data)
                    //{
                    //    SaveMember(item, id);
                    //}
                    //conn.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                //
                return members;

            }
            return null;
        }

        public Courses TestCourse(string id)
        {

            urlConnect = "https://entool.azurewebsites.net/SEP21/GetCourses?lecturerID={0}";
            urlConnect = string.Format(urlConnect, id);
            data = Url(urlConnect);
            if (data != "")
            {
                //parse data json
                var course = new List<Course>();

                //get data json type array
                Courses courses = null;
                try
                {
                    courses = JsonConvert.DeserializeObject<Courses>(data);
                    foreach (var item in courses.data)
                    {

                      
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                //
                return courses;

            }
            return null;
        }
        public StudentInfos Getstudent(string id)
        {
            //url address
            urlConnect = urlAddress+ "/GetStudent?code={0}";
            urlConnect = string.Format(urlConnect, id);

            data = Url(urlConnect);
            if (data != "")
            {
                //tao noi luu tru vao model
                var studentinfo = new StudentInfo();
                //parse data json
                //get data json type array

                try
                {
                    studentinfos = JsonConvert.DeserializeObject<StudentInfos>(data);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                //
                return studentinfos;

            }
            return null;
        }
    }
}