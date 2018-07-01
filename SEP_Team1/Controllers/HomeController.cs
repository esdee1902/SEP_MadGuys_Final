using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEP_Team1.Models;
using SEP_Team1.API;
using System.Data.Entity;
using System.Web.Routing;
using Microsoft.Ajax.Utilities;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using OfficeOpenXml;
using System.Text;

namespace SEP_Team1.Controllers
{
    public class HomeController : Controller
    {
        public static string strConnection = System.Configuration.ConfigurationManager.ConnectionStrings["sep21t21ConnectionString"].ToString();
        public    DataClasses1DataContext db = new DataClasses1DataContext(strConnection);
        Utility ut = new Utility();
        SqlCommand cmd;
        SqlDataAdapter sda;
        SqlCommandBuilder scd;
        static string strConection = System.Configuration.ConfigurationManager.ConnectionStrings["sep21t21ConnectionString"].ToString();
        SqlConnection conn = new SqlConnection(strConection);
        APIConnect connect = new APIConnect();
        public ActionResult Index()
        {
            if (Session["MaGV"].ToString().Length < 2)
            {
                return RedirectToAction("Login", "Home");
            }
            Session["CreateAttendance"] = "";
            string maGV = Session["MaGV"] as string;
            var monhoc = connect.TestCourse(maGV);
            //  var maMH = monhoc.
            var khoahoc = db.KhoaHocs.ToList();
             var maKH = khoahoc.Select(kh => kh.maKH);
            var buoihoc = db.BangBuoiHocs.Where(x=>x.maKH==maGV).ToList();
            ViewBag.Days = buoihoc.Count();
            //ViewBag.nKhoaHoc = khoahoc.Count();
            ViewBag.Lesson = khoahoc;
            ViewBag.Subject = monhoc;
            return View(khoahoc);
        }
        public ActionResult Course(string id)
        {
            if (Session["MaGV"].ToString().Length < 2)
            {
                return RedirectToAction("Login");
            }
            Session["CreateAttendance"] = "";
            //
            Session["MaKH"] = id;
            ViewBag.IDmaKH = id;
            string maGV = Session["MaGV"] as string;
            var monhoc = connect.TestCourse(maGV);
            ViewBag.Subject = monhoc;
            var ikhoahoc = db.KhoaHocs.Where(kh => kh.maMH == id).ToList();
            var maKH = ikhoahoc.Select(kh => kh.maKH);

            ViewBag.iLesson = ikhoahoc;
            var buoihoc = db.BangBuoiHocs.Where(bh => maKH.Contains(bh.maKH)).ToList();
            ViewBag.Classes = buoihoc.Count();
            var sinhvien = db.SinhViens.ToList();
            ViewBag.Student = sinhvien.Count();
            return View();
        }
        public ActionResult ViewAttendance(string Buoithu)
        {
            if (Session["MaGV"].ToString().Length < 2)
            {
                return RedirectToAction("Login");
            }
            CreateSessionID creates = new CreateSessionID();
            string ids = Session["MaKH"].ToString();
            int getIDs = (creates.GetLastID("DiemDanh", "sessionID", ids));
          
            Session["SessionIDs"] = (getIDs + 1);
            if (getIDs > 0)
            {
                string id = Session["MaKH"] as string;
                if (Buoithu == "" || Buoithu == null)
                {
                    Buoithu = "1";
                }
                var query = (from pro in db.DiemDanhs where pro.MaKH == ids select pro).ToList();
                ViewBag.Date = query;
                // Session["maKH"] = id;
                var monhoc = connect.TestCourse(id);
                var sinhvien = (from pro in db.SinhViens where pro.maKH == id select pro).ToList();
                var buoihoc = db.BangBuoiHocs.Where(bh => bh.maKH == id).ToList();
                var buoihocnew = buoihoc.OrderByDescending(x => x.Id).ToArray();
                //string mbh = Convert.ToString(buoihocnew[0]);

                int sessionid = int.Parse(Buoithu);
                Session["SessionExcel"] = Buoithu;

                var diemdanh = (from pro in db.DiemDanhs where pro.MaKH == id && pro.sessionID == sessionid select pro).ToList();


                //ViewBag.Session = new SelectList(list,"","");
                ViewBag.Student = sinhvien;
                ViewBag.Diemdanh = diemdanh;
                ViewBag.CountStudent = sinhvien.Count();
                ViewBag.Buoihoc = buoihoc;
                ViewBag.Subject = monhoc;

            }
            else
            {
                string maKH = Session["MaKH"].ToString();
                //this.dataGridView2.DataSource = null;
                return RedirectToAction("Course", new RouteValueDictionary(
             new { controller = "Home", action = "Course", Id = maKH }));
            }

            return View();
        }
        //[HttpGet]
        public ActionResult CreateAttendance(string maKH)
        {
            if (Session["MaGV"].ToString().Length < 2)
            {
                return RedirectToAction("Login");
            }
            Session["CreateAttendance"] = "";
            try
            {
                if (maKH.Length <1 || maKH == null || maKH == "")
                {
                    maKH = Session["MaKH"].ToString();
                }
            }
            catch (NullReferenceException)
            {
                maKH = Session["MaKH"].ToString();
            }
            
          var query = (from pro in db.BangBuoiHocs where pro.maKH == maKH select pro.day).ToList();
            Session["MABH"] = query;
            var s = DateTime.Now.DayOfWeek.ToString();
            bool exist = query.Any(item => item == s);
            if (exist == true)
            {
                ViewBag.ListAttendance = connect.GetMember(maKH).data;
                Session["CreateAttendance"] = "";
                return View();
            }
            else
            {
                // tbao không có buổi học
               // ViewBag.msg = "There is no session today";
                Session["CreateAttendance"] = "There is no session today";
                return RedirectToAction("ViewListStudent",new RouteValueDictionary(
             new { controller = "Home", action = "ViewListStudent", maKHs = maKH}));
            }
            //return View();
        }
     
        public ActionResult ViewListStudent(string maKHs)
        {
            if (Session["MaGV"].ToString().Length < 2)
            {
                return RedirectToAction("Login");
            }

            maKHs = Session["MaKH"].ToString();
            ViewBag.ListAttendance = connect.GetMember(maKHs).data;

            return View();
        }
 
        public ActionResult ListStudent(string id)
        {
            if (Session["MaGV"].ToString().Length < 2)
            {
                return RedirectToAction("Login");
            }
            var item = connect.GetMember(id).data;
     

            string maGV = Session["MaGV"] as string;
            var monhoc = connect.TestCourse(maGV);
            var sinhvien = db.SinhViens.Where(v => v.maKH == id).ToList();
            ViewBag.Student = sinhvien;
            ViewBag.Subject = monhoc;

            return View();
        }
        public ActionResult diemDanh( string session)
        {
            if (Session["MaGV"].ToString().Length < 2)
            {
                return RedirectToAction("Login");
            }
            CreateSessionID creates = new CreateSessionID();
            string ids = Session["MaKH"].ToString();
            int getIDs = (creates.GetLastID("DiemDanh", "sessionID", ids));
            if (getIDs > 0)
            {
                if (session == "" || session == null)
                {
                    session = "1";
                }
                var query = (from pro in db.DiemDanhs where pro.MaKH == ids select pro).ToList();
                ViewBag.Date = query;
                Session["SessionID"] = session;
                string maGV = Session["MaGV"] as string;
                //   Session["maKH"] = id;
                string id = Session["MaKH"].ToString();
                var monhoc = connect.TestCourse(maGV);
                var sinhvien = connect.GetMember(id).data;
                var buoihoc = db.BangBuoiHocs.Where(bh => bh.maKH == id).ToList();
                var buoihocnew = buoihoc.OrderByDescending(x => x.Id).ToArray();
                string mbh = Convert.ToString(buoihocnew[0]);
                int buoi = int.Parse(session);
                var diemdanh = (from pro in db.DiemDanhs where pro.MaKH == id && pro.sessionID == buoi select pro).ToList();
                CreateSessionID create = new CreateSessionID();

                int getID = (create.GetLastID("DiemDanh", "sessionID", id));

                ViewBag.Student = sinhvien;
                ViewBag.Diemdanh = diemdanh;
              //  ViewBag.CountStudent = sin;
                ViewBag.Buoihoc = buoihoc;
                ViewBag.Subject = monhoc;

            }
            else
            {
                string maKH = Session["MaKH"].ToString();
                //this.dataGridView2.DataSource = null;
                return RedirectToAction("Course", new RouteValueDictionary(
             new { controller = "Home", action = "Course", Id = maKH }));
            }
            return View();
        }

        public ActionResult Change(string id)
        {
            string makh = Session["MaKH"].ToString();
            int session = int.Parse(Session["SessionID"].ToString());
          
            var attendance = (from pro in db.DiemDanhs where pro.MaKH == makh && pro.sessionID == session && pro.MSSV== id select pro).FirstOrDefault();
            if (attendance.diemDanh1 == true)
            {
                attendance.diemDanh1 = false;
            }
            else
            {
                attendance.diemDanh1 = true;
            }
            conn = ut.OpenDb();
            conn.Open();
            cmd = new SqlCommand("select *from DiemDanh", conn);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update DiemDanh set diemDanh = '" + attendance.diemDanh1 + "' Where MaKH = '" + makh + "' and sessionID ='"+session+ "' and MSSV ='" + id + "'";
            cmd.ExecuteNonQuery();
            Utility.OpenConnection();
            conn.Close();
        
           
            return RedirectToAction("diemDanh", new { Buoithu = Session["SessionID"].ToString() });
        }

        public ActionResult Edit(string bhocc)
        {
            string maGV = Session["MaGV"] as string;
            string maKh = Session["MaKH"] as string;

            var Fch = Request.Form.AllKeys.Where(k => k != "bhocc");
            string day = DateTime.Now.DayOfWeek.ToString();
            var query = (from pro in db.DiemDanhs where pro.MaKH == maKh select pro).ToList();
            ViewBag.Date = query;
            //var query = (from pro in db.BangBuoiHocs where pro.maKH == maKh  select pro.maBH).FirstOrDefault();
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("HH:mm:ss");
            CreateSessionID create = new CreateSessionID();
            int getID = int.Parse(Session["SessionID"].ToString());
            DiemDanh push = null;
            foreach (var fea in Fch)
            {
                {
                    string x = Request.Form[fea.Trim()];
                    var diemDanh1 = Request.Form[fea.Trim()] != "false";


                    conn = ut.OpenDb();
                    conn.Open();
                    cmd = new SqlCommand("select *from DiemDanh", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update  DiemDanh  set diemDanh = '" + diemDanh1 + "' Where MSSV = '" + fea.Trim() + "' and  MaKH = '" + maKh + "' and  sessionID = '" + getID + "'";
                    //values('" + query + "',N'" + maKh + "',N'" + fea.Trim().ToString() + "','" + date + "','" + time + "','" + diemDanh1 + "','" + getID + "')";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();



                }
            }


            return RedirectToAction("Course","Home", new {id= Session["MaKH"].ToString() });
        }
        [HttpPost]
        public ActionResult Ed(string bhoc)
        {
            string maGV = Session["MaGV"] as string;
            string maKh = Session["MaKH"] as string;
         
            var Fch = Request.Form.AllKeys.Where(k => k != "bhoc");
            string day = DateTime.Now.DayOfWeek.ToString();
            var query = (from pro in db.BangBuoiHocs where pro.maKH == maKh && pro.day == day select pro.maBH).FirstOrDefault();
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("HH:mm:ss");
            CreateSessionID create = new CreateSessionID();
            int getID = (create.GetLastID("DiemDanh", "sessionID", maKh) + 1);
            foreach (var fea in Fch)
            {
                {
                    var diemDanh1 = Request.Form[fea.Trim()] != "false";
                    conn = ut.OpenDb();
                    conn.Open();
                    cmd = new SqlCommand("select *from DiemDanh", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into DiemDanh  values('" + query + "',N'" + maKh + "',N'" + fea.Trim().ToString() + "','" + date + "','" + time + "','" + diemDanh1 + "','" + getID + "')";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    //sessionID = int.Parse(Session["SessionIDs"].ToString()),
                    //MaKH = maKh,
                    //Date = DateTime.Now.Date,
                    //Time = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")),
                    //maBH = Session["MABH"].ToString(),
                    //MSSV = fea.Trim(),
                    //diemDanh1 = Request.Form[fea.Trim()] != "false"


                }
            }


           
            var monhoc = connect.TestCourse(maGV);

            //var diemdanh = db.DiemDanhs.Include("SinhVien").Include("BangBuoiHoc").Where(dd => dd.maBH == bhoc);

            //ViewBag.Diemdanh = diemdanh.ToList();

            ViewBag.Subject = monhoc;
            return RedirectToAction("ViewAttendance", new RouteValueDictionary(
             new { controller = "Home", action = "ViewAttendance", Buoithu = getID.ToString() }));
        }
       
        [HttpPost]
        public ActionResult Check(string bhoc)
        {
            

            var diemdanh = db.DiemDanhs.Include("SinhVien").Include("BangBuoiHoc").Where(dd => dd.maBH == bhoc);

            ViewBag.Diemdanh = diemdanh.ToList();

            //ViewBag.Subject = monhoc;
            return PartialView();
        }
        public ActionResult Login()
        {
            return View();
        } 
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var item = connect.Check_Login(username, password);
            try
            {
                if (item.data.id != null && item.data.id != "")
                {
                    Session["hoten"] = username;
                    Session["MaGV"] = item.data.id;
                    return RedirectToAction("Index", "Home");
                 
                }
            }
            catch (NullReferenceException)
            {
                ViewBag.mgs = "Incorrect account or password";
                return RedirectToAction("Login");
            }

            return View();

        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session["hoten"] = null;
            Session["MaGV"] = null;
            return RedirectToAction("Login");
        }
        //Export
        [HttpGet]
        public void ExportToExcel(string id)
        {
            int sessionex = int.Parse(Session["SessionExcel"].ToString());
            List<DiemDanh> diemDanh = (from pro in db.DiemDanhs where pro.MaKH == id && pro.sessionID == sessionex select pro).ToList();
            var sinhVien = connect.GetMember(Session["MaKH"].ToString()).data;
            List<DiemDanhViewModel> emplist = new List<DiemDanhViewModel>();
            foreach (var dd in diemDanh)
            {

                foreach (var sv in sinhVien)
                {
                    if (sv.Id.Trim() == dd.MSSV.Trim())
                    {
                        DiemDanhViewModel ddModel = new DiemDanhViewModel
                        {
                            MSSV = sv.Id,
                            firstname = sv.firstname,
                            lastname = sv.lastname,
                            birthday = DateTime.Parse(sv.birthday),
                            DiemDanh = dd.diemDanh1,
                        };
                        emplist.Add(ddModel);
                    }

                }


            }
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Course";
            ws.Cells["B1"].Value = id;

            ws.Cells["A2"].Value = "Date";
            ws.Cells["B2"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);


            ws.Cells["A6"].Value = "ID";
            ws.Cells["B6"].Value = "Last Name";
            ws.Cells["C6"].Value = "First Name";
            ws.Cells["D6"].Value = "Birhday";
            ws.Cells["E6"].Value = "Attendance";

            int rowStart = 7;
            foreach (var item in emplist)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.MSSV;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.lastname;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.firstname;
                ws.Cells[string.Format("D{0}", rowStart)].Value = string.Format("{0:dd/MM/yyyy}", item.birthday);
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.DiemDanh;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }
        public ActionResult StudentProfile(string id)
        {
            string maGV = Session["MaGV"] as string;

            var model = connect.Getstudent(id);

            return View(model);

        }
    }
    
}
    
