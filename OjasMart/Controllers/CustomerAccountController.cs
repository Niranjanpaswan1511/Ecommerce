using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using OjasMart.Models;

namespace OjasMart.Controllers
{
    public class CustomerAccountController : Controller
    {
        PropertyClass objp = new PropertyClass();
        LogicClass objL = new LogicClass();
        // GET: CustomerAccount
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult VerifyOTP()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult AddToCart()
        {
            return View();
        }
        public ActionResult WishList()
        {

            if (Session["username"] != null)
            {


                objp.CustomerId = Convert.ToString(Session["username"]);
                objp.Action = "81";
                DataSet ds = objL.GetCustomerDashBoard(objp, "proc_BindCustomerDashBoard");
                if (ds != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objp.dt = ds.Tables[0];
                    }

                }
            }
            else
            {
                return RedirectToAction("Login");
            }


            return View(objp);
        }
        public ActionResult CustomerDashBoard()
        {
            try
            {
                if(Session["mCode"]==null)
                {

                }
                objp.CustomerId = Convert.ToString(Session["mCode"]);
                objp.Action = "1";
                DataSet ds = objL.GetCustomerDashBoard(objp, "proc_BindCustomerDashBoard");
                if(ds != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objp.SSName = Convert.ToString(ds.Tables[0].Rows[0]["Name"]);
                        objp.ContactNo = Convert.ToString(ds.Tables[0].Rows[0]["MobileNo"]);
                        objp.Address = Convert.ToString(ds.Tables[0].Rows[0]["Address"]);
                    }
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        objp.Description = Convert.ToString(ds.Tables[1].Rows[0]["TotPurchase"]);
                        objp.ItemName = Convert.ToString(ds.Tables[1].Rows[0]["TotItem"]);
                        objp.todayPoAmt = Convert.ToString(ds.Tables[1].Rows[0]["TotAmt"]);
                        objp.Status = Convert.ToString(ds.Tables[1].Rows[0]["Status"]);
                    }

                }
            }
            catch(Exception ex)
            { }
            return View(objp);
        }
        public JsonResult UpdateCustomerprofile(PropertyClass al)
        {
            string msg = "";
            al.Action = "9";

            al.dt = objL.GetMyprofile(al);
            if (al.dt != null && al.dt.Rows.Count > 0)
            {
                msg = al.dt.Rows[0]["msg"].ToString();
            }
            else
            {
                msg = "Not Update Profile! Try again later.";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        //VANDANA
        public ActionResult MyProfile()
        {
            try
            {
                if (Session["mCode"] == null)
                {

                }
                objp.CustomerId = Convert.ToString(Session["mCode"]);
                objp.Action = "1";
                DataSet ds = objL.GetCustomerDashBoard(objp, "proc_BindCustomerDashBoard");
                if (ds != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objp.CustomerId = Convert.ToString(ds.Tables[0].Rows[0]["CustomerId"]);
                        objp.SSName = Convert.ToString(ds.Tables[0].Rows[0]["Name"]);
                        objp.ContactNo = Convert.ToString(ds.Tables[0].Rows[0]["MobileNo"]);
                        objp.Address = Convert.ToString(ds.Tables[0].Rows[0]["Address"]);
                        objp.EmailAddress = Convert.ToString(ds.Tables[0].Rows[0]["EmailAddress"]);
                    }
                }
                objp.Action = "8";
                objp.dtAddress = objL.GetCustomerDashBoardnew(objp, "proc_BindCustomerDashBoard");
            }
            catch (Exception ex)
            { }
            return View(objp);

        }
        public ActionResult MyOrders()
        {
            try
            {
                if (Session["mCode"] == null)
                {

                }
                objp.CustomerId = Convert.ToString(Session["mCode"]);
                objp.Action = "2";
                DataSet ds = objL.GetCustomerDashBoard(objp, "proc_BindCustomerDashBoard");
                if (ds != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objp.dt = ds.Tables[0];
                    }
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        objp.dt1 = ds.Tables[1];
                    }
                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        objp.dtcombooffer = ds.Tables[2];
                    }
                }

            }
            catch(Exception ex)
            { }
            return View(objp);
        }

        public JsonResult ReturnOrder(string OrderId, string Reason)
        {
            try
            {
                objp.OrderId = OrderId.Trim();
                objp.Description = Reason.Trim();
                objp.Action = "3";
                DataTable dt = objL.InsertCancelRequest(objp, "proc_CancelOrder");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.strId = dt.Rows[0]["id"].ToString();
                    objp.msg = dt.Rows[0]["msg"].ToString();
                    if (objp.strId == "1")
                    {
                        objp.PayMode = dt.Rows[0]["payMode"].ToString();
                    }
                }
                else
                {
                    objp.strId = "0";
                }
            }
            catch (Exception ex)
            {
                objp.strId = "0";
                objp.msg = ex.Message;
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CancelOrder(string OrderId,string Reason)
        {
            try
            {                
                objp.OrderId = OrderId.Trim();
                objp.Description = Reason.Trim();
                objp.Action = "1";
                DataTable dt = objL.InsertCancelRequest(objp, "proc_CancelOrder");
                if (dt != null && dt.Rows.Count > 0)
                {
                    objp.strId = dt.Rows[0]["id"].ToString();
                    objp.msg = dt.Rows[0]["msg"].ToString();
                    if(objp.strId=="1")
                    {
                        objp.PayMode= dt.Rows[0]["payMode"].ToString();
                    }
                }
                else
                {
                    objp.strId = "0";
                }
            }
            catch (Exception ex)
            {
                objp.strId = "0";
                objp.msg = ex.Message;
            }
            return Json(objp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _pUserSideMenu()
        {
            try
            {
               
            }
            catch (Exception ex)
            { }
            return View(objp);
        }
        public ActionResult _pUserHeader()
        {
            try
            {
                if (Session["mCode"] == null)
                {

                }
                objp.CustomerId = Convert.ToString(Session["mCode"]);
                objp.Action = "1";
                DataSet ds = objL.GetCustomerDashBoard(objp, "proc_BindCustomerDashBoard");
                if (ds != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        objp.SSName = Convert.ToString(ds.Tables[0].Rows[0]["Name"]);
                        objp.ContactNo = Convert.ToString(ds.Tables[0].Rows[0]["MobileNo"]);
                        objp.Address = Convert.ToString(ds.Tables[0].Rows[0]["Address"]);
                    }                    
                }
            }
            catch (Exception ex)
            { }
            return View(objp);
        }

        public JsonResult ForgotPassword(string Mobile)
        {
            string Msg = "";
            PropertyClass al = new PropertyClass();
            al.UserName = Mobile;
            al.Action = "1";
            DataTable dt = objL.ForGotPassword(al, "Proc_ForGotPassword");
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["msg"].ToString() == "True")
                {
                    string pass = dt.Rows[0]["Password"].ToString();
                    al.strOtp = pass;
                    //al.strOtp = SendOTP2(Mobile);
                    al.strmsg = "True";
                    //SendSMS(Mobile, "Your KBDSHP password is : " + pass);
                    //Msg = "Your Password is sent to your registered contact No.,Please check and use it for furthur login";
                }
                else
                {
                    al.strmsg = "Invalid User!please again check your contact details";
                }
            }
            else
            {
                al.strmsg = "Invalid User!please again check your contact details";
            }
            return Json(al, JsonRequestBehavior.AllowGet);
        }

        SendSMS sms = new SendSMS();
        public string SendOTP2(string Mobile)
        {
            Session["OTP"] = "";
            string OTP = new Random().Next(0000, 9999).ToString();
            Session["TempMobile"] = Mobile;
            Session["OTP"] = OTP;
            string Msg = "Dear Customer, Your OTP is " + OTP + " Do not share This OTP with any One and can be used only once [AIDBOK]";
            sms.sendSMS1(Mobile, Msg);
            return OTP;
        }

        public string UpdatePass(string mob, string newPass)
        {
            string msg = "";
            PropertyClass al = new PropertyClass();
            al.Action = "2";
            al.UserName = mob;
            al.Password = newPass;
            DataTable dt = objL.ForGotPassword(al, "Proc_ForGotPassword");
            if (dt != null && dt.Rows.Count > 0)
            {
                msg = "Password changed!";
            }
            else
            {
                msg = "Unable to Update";
            }

            return msg;
        }

			
       public ActionResult MyWallet()
        {
            string CustomerId = Convert.ToString(Session["mCode"]);
            DataTable ds = objL.getBonus(CustomerId);
            if (ds.Rows.Count > 0)
            {
                ViewBag.RefererAmt =ds.Rows[0]["ReferAmt"].ToString();
            }
            return View();
        }
			
        public ActionResult MyReferrerCode()
        {
            objp.CustomerId = Convert.ToString(Session["mCode"]);
            objp.Action = "5";
            DataSet ds = objL.GetCustomerDashBoard(objp, "proc_BindCustomerDashBoard");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.RefererCode = Convert.ToString(ds.Tables[0].Rows[0]["RefererCode"]);
                ViewBag.RefererAmt = Convert.ToString(ds.Tables[0].Rows[0]["RefererAmt"]);
            }
            return View();
        }
        public ActionResult TrackOrder()
        {
            return View();
        }

        public JsonResult Track_Order(string OrderId, string MobileNumber)
        {
            commonResponse res = new commonResponse();
            objp.CustomerId = OrderId;
            objp.MobileNo = MobileNumber;
            objp.Action = "6";
            try
            {
                DataSet ds = objL.GetCustomerDashBoard(objp, "proc_BindCustomerDashBoard");
                if (ds != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        res.Status = true;
                    }
                    else
                    {
                        res.Status = false;
                    }
                }
            }
            catch (Exception ex)
            {
                res.Status = false;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ViewOrder(string OrderId)
        {
            objp.CustomerId = OrderId;
            objp.Action = "7";
            DataSet ds = objL.GetCustomerDashBoard(objp, "proc_BindCustomerDashBoard");
            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    objp.dt = ds.Tables[0];
                }
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    objp.dt1 = ds.Tables[1];
                }
            }

            return View(objp);
        }


        public ActionResult Level()
        {
            try
            {
                if (Session["mCode"] == null)
                {

                }
                objp.CustomerId = Convert.ToString(Session["mCode"]);
                objp.Action = "2";
                DataSet ds = objL.GetLevelDashBoard(objp, "proc_GetLevel");
                if(ds != null)
                {
                    objp.dtLevel = ds.Tables[0];
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }




    
    }
}