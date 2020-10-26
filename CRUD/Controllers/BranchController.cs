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
    public class BranchController : Controller
    {
        // GET: Branch
        SqlConnection con = null;
        public ActionResult Index()
        {
            //
            DataSet ds = null;
            List<Branch> blist = null;

            try
            {

                Employee cobj = null;


                con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
                SqlCommand cmd = new SqlCommand(@"Select_All_branches", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                blist = new List<Branch>();
                string boolval = "";
                for (int ctr = 0; ctr < ds.Tables[0].Rows.Count; ctr++)
                {
                    Branch bobj = new Branch();
                    if (Convert.ToString(ds.Tables[0].Rows[ctr]["isactive"].ToString()) == "0")
                    { boolval = "false"; }
                    else { boolval = "true"; }
                    bobj.ID = Convert.ToInt32(ds.Tables[0].Rows[ctr]["id"].ToString());
                    bobj.BR_Code = ds.Tables[0].Rows[ctr]["br_id"].ToString();
                    bobj.br_name = ds.Tables[0].Rows[ctr]["br_name"].ToString();
                    bobj.br_address = ds.Tables[0].Rows[ctr]["br_address"].ToString();
                    bobj.br_brgy = ds.Tables[0].Rows[ctr]["br_brgy"].ToString();
                    bobj.br_city = ds.Tables[0].Rows[ctr]["br_city"].ToString();
                    bobj.br_mngr = ds.Tables[0].Rows[ctr]["manager"].ToString();
                    bobj.DateOpened = Convert.ToDateTime(ds.Tables[0].Rows[ctr]["date_opened"].ToString());
                    bobj.permitno = ds.Tables[0].Rows[ctr]["br_permit"].ToString();
                    bobj.isActive = Convert.ToBoolean(boolval);

                    blist.Add(bobj);
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
            return View(blist);
            //return View();
        }

        // GET: Branch/Details/5
        public ActionResult Details(int id)
        {


            DataSet dsemp = null;
            List<Branch_Managers> bmlist = null;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            SqlCommand cmdemp = new SqlCommand(@"Get_EmployeeNames", con);
            cmdemp.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataAdapter daemp = new SqlDataAdapter();
            daemp.SelectCommand = cmdemp;
            dsemp = new DataSet();
            daemp.Fill(dsemp);
            List<Branch> blist = new List<Branch>();
            List<SelectListItem> getemployees = null;
            getemployees = new List<SelectListItem>();
            string boolval = "";
            for (int e = 0; e < dsemp.Tables[0].Rows.Count; e++)
            {
                SelectListItem bobj = new SelectListItem();
                bobj.Value = Convert.ToString(dsemp.Tables[0].Rows[e]["id"]);
                bobj.Text = Convert.ToString(dsemp.Tables[0].Rows[e]["EmployeeName"]);
                getemployees.Add(bobj);

            };



            List<SelectListItem> mySkills = new List<SelectListItem>()
            {
            };
            ViewBag.MySkills = getemployees;
            DataSet ds = null;
            List<Branch> elist = null;

            Branch eobj = new Branch();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());

            SqlCommand cmd = new SqlCommand(@"Select_Specific_Branches", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            elist = new List<Branch>();
            string val = "";
            string strDDLValue = "";
            strDDLValue = Request.Form["MySkills"];

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["isActive"].ToString() == "1")
                { val = "true"; }
                else { val = "false"; }

                //                    Employee cobj1 = new Employee();
                eobj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["id"]);
                eobj.BR_Code = ds.Tables[0].Rows[i]["br_id"].ToString();
                eobj.br_name = ds.Tables[0].Rows[i]["br_name"].ToString();
                eobj.br_address = ds.Tables[0].Rows[i]["br_address"].ToString();
                eobj.br_city = ds.Tables[0].Rows[i]["br_city"].ToString();
                eobj.br_brgy = ds.Tables[0].Rows[i]["br_brgy"].ToString();
                eobj.br_mngr = ds.Tables[0].Rows[i]["manager"].ToString();
                eobj.permitno = ds.Tables[0].Rows[i]["br_permit"].ToString();
                eobj.DateOpened = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date_Opened"].ToString());
                eobj.isActive = Convert.ToBoolean(val);

                elist.Add(eobj);
            }
            return View(eobj);

        }

        // GET: Branch/Create
        public ActionResult Create()
        {
            DataSet ds = null;
            List<Branch_Managers> bmlist = null;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            SqlCommand cmd = new SqlCommand(@"Get_EmployeeNames", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            List<Branch> blist = new List<Branch>();
            List<SelectListItem> getemployees = null;
            getemployees = new List<SelectListItem>();
            string boolval = "";
            if (ds.Tables[0].Rows.Count != 0)
            {
                for (int ctr = 0; ctr < ds.Tables[0].Rows.Count; ctr++)
                {
                    SelectListItem bobj = new SelectListItem();
                    bobj.Value = Convert.ToString(ds.Tables[0].Rows[ctr]["id"]);
                    bobj.Text = Convert.ToString(ds.Tables[0].Rows[ctr]["EmployeeName"]);
                    getemployees.Add(bobj);

                };



                List<SelectListItem> mySkills = new List<SelectListItem>()
                {

                };
            }
            if (getemployees.Count == 0)
            {
                ViewBag.Message = String.Format("Add Employee first");
                SelectListItem bobj = new SelectListItem();
                bobj.Value = "0";
                bobj.Text = "Add employee first";
                getemployees.Add(bobj);
                ViewBag.MySkills = getemployees;
                return View();

            }
            else
            {

                ViewBag.MySkills = getemployees;

                return View();
            }
        }

        // POST: Branch/Create
        [HttpPost]
        public ActionResult Create(Branch createbranch)
        {
            DataSet ds = null;
            List<Branch_Managers> bmlist = null;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            SqlCommand cmd1 = new SqlCommand(@"Get_EmployeeNames", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd1;
            ds = new DataSet();
            da.Fill(ds);
            List<Branch> blist = new List<Branch>();
            List<SelectListItem> getemployees = null;
            getemployees = new List<SelectListItem>();
            string boolval = "";
            if (ds.Tables[0].Rows.Count != 0)
            {
                for (int ctr = 0; ctr < ds.Tables[0].Rows.Count; ctr++)
                {
                    SelectListItem bobj = new SelectListItem();
                    bobj.Value = Convert.ToString(ds.Tables[0].Rows[ctr]["id"]);
                    bobj.Text = Convert.ToString(ds.Tables[0].Rows[ctr]["EmployeeName"]);
                    getemployees.Add(bobj);

                };



                List<SelectListItem> mySkills = new List<SelectListItem>()
                {

                };
            }
            if (getemployees.Count == 0)
            {
                ViewBag.Message = String.Format("Add Employee first");
                SelectListItem bobj = new SelectListItem();
                bobj.Value = "0";
                bobj.Text = "Add employee first";
                getemployees.Add(bobj);
                ViewBag.MySkills = getemployees;
                return View();


            }
            else
            {

                ViewBag.MySkills = getemployees;
                string strDDLValue = "";
                strDDLValue = Request.Form["MySkills"];
                if ((strDDLValue == "") || (strDDLValue == null))
                {
                    ViewBag.Message = String.Format("Add Employee first");
                    return View();
                }
                else
                {
                    try
                    {

                        // TODO: Add insert logic here
                        con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
                        SqlCommand cmd = new SqlCommand(@"Add_New_Branch", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        string result = "";
                        //cmd.Parameters.AddWithValue("@CustomerID", 0);  
                        cmd.Parameters.AddWithValue("@br_id", createbranch.BR_Code);
                        cmd.Parameters.AddWithValue("@br_name", createbranch.br_name);
                        cmd.Parameters.AddWithValue("@br_address", createbranch.br_address);
                        cmd.Parameters.AddWithValue("@br_brgy", createbranch.br_brgy);
                        cmd.Parameters.AddWithValue("@br_city", createbranch.br_city);
                        cmd.Parameters.AddWithValue("@br_mngr", strDDLValue);
                        //                cmd.Parameters.AddWithValue("@br_mngr", createbranch.Emp_List);
                        cmd.Parameters.AddWithValue("@date_opened", createbranch.DateOpened);
                        cmd.Parameters.AddWithValue("@br_permit", createbranch.permitno);
                        cmd.Parameters.AddWithValue("@isactive", createbranch.isActive);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        //result = cmd.ExecuteScalar().ToString();
                        ViewBag.Message = String.Format("Employee Added Successfully");

                    }
                    catch
                    {
                        ViewBag.Message = String.Format("Add Employee first");
                        return View();
                    }
                }
                return View();


            }

        }

        // GET: Branch/Edit/5
        public ActionResult Edit(int id)
        {


            DataSet dsemp = null;
            List<Branch_Managers> bmlist = null;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            SqlCommand cmdemp = new SqlCommand(@"Get_EmployeeNames", con);
            cmdemp.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataAdapter daemp = new SqlDataAdapter();
            daemp.SelectCommand = cmdemp;
            dsemp = new DataSet();
            daemp.Fill(dsemp);
            List<Branch> blist = new List<Branch>();
            List<SelectListItem> getemployees = null;
            DataSet ds = null;
            List<Branch> elist = null;

            Branch eobj = new Branch();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());

            SqlCommand cmd = new SqlCommand(@"Select_Specific_Branches", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            elist = new List<Branch>();
            string val = "";
            string strDDLValue = "";
            strDDLValue = Request.Form["MySkills"];

            string id_mngr = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["isActive"].ToString() == "1")
                { val = "true"; }
                else { val = "false"; }

                //                    Employee cobj1 = new Employee();
                eobj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["id"]);
                eobj.BR_Code = ds.Tables[0].Rows[i]["br_id"].ToString();
                eobj.br_name = ds.Tables[0].Rows[i]["br_name"].ToString();
                eobj.br_address = ds.Tables[0].Rows[i]["br_address"].ToString();
                eobj.br_city = ds.Tables[0].Rows[i]["br_city"].ToString();
                eobj.br_brgy = ds.Tables[0].Rows[i]["br_brgy"].ToString();
                eobj.permitno = ds.Tables[0].Rows[i]["br_permit"].ToString();
                eobj.br_mngr = ds.Tables[0].Rows[i]["br_mngr"].ToString();//strDDLValue;// 
                id_mngr = ds.Tables[0].Rows[i]["br_mngr"].ToString();
                eobj.DateOpened = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date_Opened"].ToString());
                eobj.br_mngrname = ds.Tables[0].Rows[i]["manager"].ToString();
                eobj.isActive = Convert.ToBoolean(val);
                //(new SelectListItem { Text = ds.Tables[0].Rows[i]["br_mngr"].ToString(), Value = ds.Tables[0].Rows[i]["br_mngr_name"].ToString(), Selected = (ds.Tables[0].Rows[i]["br_mngr"].ToString() == eobj.br_brgy) });

                elist.Add(eobj);

            }

            getemployees = new List<SelectListItem>();
            string boolval = "";
            for (int e = 0; e < dsemp.Tables[0].Rows.Count; e++)
            {
                SelectListItem bobj = new SelectListItem();

                if (id_mngr == Convert.ToString(dsemp.Tables[0].Rows[e]["id"]))
                {
                    bobj.Selected = true;
                }
                bobj.Value = Convert.ToString(dsemp.Tables[0].Rows[e]["id"]); 
                bobj.Text = Convert.ToString(dsemp.Tables[0].Rows[e]["EmployeeName"]);
                
                getemployees.Add(bobj);

            };



            List<SelectListItem> mySkills = new List<SelectListItem>()
            {
            };
            ViewBag.MySkills = getemployees;

            return View(eobj);
        }

        // POST: Branch/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Branch editbranch)
        {
            try
            {
                string strDDLValue = "";
                strDDLValue = Request.Form["MySkills"];

                // TODO: Add update logic here
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
                SqlCommand cmd = new SqlCommand(@"Update_branch", con);
                cmd.CommandType = CommandType.StoredProcedure;
                string result = "";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@br_id", editbranch.BR_Code);
                cmd.Parameters.AddWithValue("@br_name", editbranch.br_name);
                cmd.Parameters.AddWithValue("@br_address", editbranch.br_address);
                cmd.Parameters.AddWithValue("@br_brgy", editbranch.br_brgy);
                cmd.Parameters.AddWithValue("@br_city", editbranch.br_city);
                cmd.Parameters.AddWithValue("@br_mngr", strDDLValue);//editbranch.br_mngr);
                cmd.Parameters.AddWithValue("@date_opened", editbranch.DateOpened);
                cmd.Parameters.AddWithValue("@br_permit", editbranch.permitno);
                cmd.Parameters.AddWithValue("@isActive", editbranch.isActive);

                //result =cmd.ExecuteScalar().ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //cmd.ExecuteScalar();         
                ViewBag.Message = "Branch Updated";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Branch/Delete/5
        public ActionResult Delete(int id)
        {
            DataSet ds = null;
            List<Branch> elist = null;

            Branch eobj = new Branch();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());

            SqlCommand cmd = new SqlCommand(@"Select_specific_branches", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            elist = new List<Branch>();
            string val = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //                    Employee cobj1 = new Employee();

                if (ds.Tables[0].Rows[i]["isActive"].ToString() == "1")
                { val = "true"; }
                else { val = "false"; }
                eobj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["id"]);
                eobj.BR_Code = ds.Tables[0].Rows[i]["br_id"].ToString();
                eobj.br_name = ds.Tables[0].Rows[i]["br_name"].ToString();
                eobj.br_address = ds.Tables[0].Rows[i]["br_address"].ToString();
                eobj.br_city = ds.Tables[0].Rows[i]["br_city"].ToString();
                eobj.br_brgy = ds.Tables[0].Rows[i]["br_brgy"].ToString();
                eobj.br_mngr = ds.Tables[0].Rows[i]["br_mngr"].ToString();
                eobj.permitno = ds.Tables[0].Rows[i]["br_permit"].ToString();
                eobj.DateOpened = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date_Opened"].ToString());
                eobj.isActive = Convert.ToBoolean(val);

                elist.Add(eobj);
            }
            return View(eobj);
            //return View();
        }

        // POST: Branch/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Branch delbranch)
        {
            try
            {
                // TODO: Add delete logic here
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
                SqlCommand cmd = new SqlCommand(@"Delete_Branch", con);
                cmd.CommandType = CommandType.StoredProcedure;
                string result = "";
                //cmd.Parameters.AddWithValue("@CustomerID", 0);  
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }
    }
}
