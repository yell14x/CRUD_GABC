using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        SqlConnection con = null;
        // GET: Employee
        public ActionResult Index()
        {
            DataSet ds = null;
            List<Employee> elist = null;

            try
            {
                
            Employee cobj = null;


                con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
                SqlCommand cmd = new SqlCommand(@"Select_all_employees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open(); 
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                elist = new List<Employee>();
                for (int ctr = 0; ctr < ds.Tables[0].Rows.Count; ctr++)
                {
                    Employee eobj = new Employee();
                    eobj.ID = Convert.ToInt32(ds.Tables[0].Rows[ctr]["id"].ToString());
                    eobj.FirstName = ds.Tables[0].Rows[ctr]["First_name"].ToString();
                    eobj.MiddleName = ds.Tables[0].Rows[ctr]["Middle_name"].ToString();
                    eobj.LastName = ds.Tables[0].Rows[ctr]["Last_name"].ToString();
                    eobj.DateHired = Convert.ToDateTime(ds.Tables[0].Rows[ctr]["date_hired"].ToString());
                    eobj.EmpImage =  ds.Tables[0].Rows[ctr]["emp_image"].ToString();
                    
                    elist.Add(eobj);
                }
            }
            catch 
                {
                    return null;
                }
            finally
            {
                con.Close();
            }
            return View(elist);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            DataSet ds = null;
            List<Employee> elist = null;

            Employee eobj = new Employee();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());

            SqlCommand cmd = new SqlCommand(@"Select_specific_employees", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            elist = new List<Employee>();
            string br_name = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if ((ds.Tables[0].Rows[i]["br_name"].ToString() == null) || (ds.Tables[0].Rows[i]["br_name"].ToString() == ""))
                {
                    br_name = "not yet assigned";
                }
                else {
                    br_name = ds.Tables[0].Rows[i]["br_name"].ToString();
                }
                //                    Employee cobj1 = new Employee();
                eobj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
                eobj.FirstName = ds.Tables[0].Rows[i]["First_name"].ToString();
                eobj.MiddleName = ds.Tables[0].Rows[i]["Middle_name"].ToString();
                eobj.LastName = ds.Tables[0].Rows[i]["Last_name"].ToString();
                eobj.DateHired = Convert.ToDateTime(ds.Tables[0].Rows[i]["date_hired"].ToString());
                eobj.EmpImage = ds.Tables[0].Rows[i]["emp_image"].ToString();
                eobj.Br_Assigned = br_name;
                elist.Add(eobj);
            }
            return View(eobj);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee eitems)//(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
                SqlCommand cmd = new SqlCommand(@"Add_New_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                string result = "";

                cmd.Parameters.AddWithValue("@first_name", eitems.FirstName);
                cmd.Parameters.AddWithValue("@middle_name", eitems.MiddleName);
                cmd.Parameters.AddWithValue("@last_name", eitems.LastName);
                cmd.Parameters.AddWithValue("@date_hired", eitems.DateHired);
                cmd.Parameters.AddWithValue("@emp_image", eitems.EmpImage);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ViewBag.Message = "Employee Added : " + eitems.FirstName + " " + eitems.LastName;
                eitems.FirstName = "";
                eitems.LastName = "";
                eitems.DateHired = DateTime.Today;
                eitems.EmpImage = "";
                return View();

            }
            catch
            {
                ViewBag.Message = "Error Encountered, Kindly double check the fields.";
                return RedirectToAction("index");
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            DataSet ds = null;
            List<Employee> elist = null;

            Employee eobj = new Employee();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());

            SqlCommand cmd = new SqlCommand(@"Select_specific_employees", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            elist = new List<Employee>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                eobj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
                eobj.FirstName = ds.Tables[0].Rows[i]["First_name"].ToString();
                eobj.MiddleName = ds.Tables[0].Rows[i]["Middle_name"].ToString();
                eobj.LastName = ds.Tables[0].Rows[i]["Last_name"].ToString();
                eobj.DateHired = Convert.ToDateTime(ds.Tables[0].Rows[i]["date_hired"].ToString());
                eobj.EmpImage = ds.Tables[0].Rows[i]["emp_image"].ToString();

                elist.Add(eobj);
            }
            return View(eobj);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee editemp)
        {
            try
            {
                // TODO: Add update logic here
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
                SqlCommand cmd = new SqlCommand(@"Update_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                string result = "";

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@first_name", editemp.FirstName);
                cmd.Parameters.AddWithValue("@middle_name", editemp.MiddleName);
                cmd.Parameters.AddWithValue("@last_name", editemp.LastName);
                cmd.Parameters.AddWithValue("@date_hired", editemp.DateHired);
                
                cmd.Parameters.AddWithValue("@emp_image", editemp.EmpImage);
                //result =cmd.ExecuteScalar().ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ViewBag.Message = "Updated Employee :" + editemp.FirstName +" " +editemp.LastName;
                return View();

            }
            catch
            {
                ViewBag.Message = "Error Encountered. Kindly double check fields";
                return View();

                //return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {

            DataSet ds = null;
            List<Employee> elist = null;

            Employee eobj = new Employee();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());

            SqlCommand cmd = new SqlCommand(@"Select_specific_employees", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            elist = new List<Employee>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //                    Employee cobj1 = new Employee();
                eobj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
                eobj.FirstName = ds.Tables[0].Rows[i]["First_name"].ToString();
                eobj.MiddleName = ds.Tables[0].Rows[i]["Middle_name"].ToString();
                eobj.LastName = ds.Tables[0].Rows[i]["Last_name"].ToString();
                eobj.DateHired = Convert.ToDateTime(ds.Tables[0].Rows[i]["date_hired"].ToString());
                eobj.EmpImage = ds.Tables[0].Rows[i]["emp_image"].ToString();

                elist.Add(eobj);
            }
            return View(eobj);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Employee delemp)
        {
            try
            {
                // TODO: Add delete logic here
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
                SqlCommand cmd = new SqlCommand(@"Delete_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                string result = "";

                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ViewBag.Message = "Deleted Employee id:" + id;
                return View();
            }
            catch
            {
                ViewBag.Message = "Error Encountered. Kindly double check fields";
                return RedirectToAction("Index");
                //  return View();
            }
        }
    }
}
