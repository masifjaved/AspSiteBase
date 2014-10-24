using AspSiteBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AspSiteBase.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // Cache output of this action for 60 seconds
        [OutputCache(Duration = 60)]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Gallery()
        {
            return View();
        }


        [Recaptcha.RecaptchaControlMvc.CaptchaValidator]
        public ActionResult Contact(bool captchaValid)
        {
            if (Request.HttpMethod == "POST")
            {
                //the controller was hit with POST
                string ip = Request.UserHostAddress;
                string name = Request.Form["name"];
                string telephone = Request.Form["telephone"];
                string email = Request.Form["email"];
                string msg = Request.Form["msg"];
                string created = DateTime.Now.ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("<h1>Email from mysite.co.uk contact form</h1>");
                sb.Append("<table  cellpadding=10px cellspacing=10px>");
                sb.AppendFormat("<tr><td>Name:    </td><td> {0}</td></tr>", name);
                sb.AppendFormat("<tr><td>Telephone:    </td><td> {0}</td></tr>", telephone);
                sb.AppendFormat("<tr><td>Email:    </td><td> {0}</td></tr>", email);
                sb.AppendFormat("<tr><td>Message:    </td><td> {0}</td></tr>", msg.Replace("\n", "<br />"));
                sb.Append("<tr><td colspan=2>&nbsp;</td></tr>");
                sb.AppendFormat("<tr><td>IP:    </td><td> {0}</td></tr>", ip);
                sb.Append("</table>");

                if (telephone == "")
                {
                    ViewBag.msg = "Error! Required fields cannot be left blank";
                    ViewBag.status = 2;
                }
                else if (!captchaValid)
                {
                    ViewBag.msg = "Error! Invalid Captcha";
                    ViewBag.status = 2;
                }
                else
                {

                    try
                    {
                        bool status = Insert2Db(name, telephone, email, msg, ip);

                        SmtpClient smtpClient = new SmtpClient("mail.mywebsite.co.uk", 25);
                        //info@
                        //apEihw3k!
                        smtpClient.Credentials = new System.Net.NetworkCredential("test@mywebsite.co.uk", "mypassword");
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.EnableSsl = false;
                        MailMessage mail = new MailMessage();

                        mail.From = new MailAddress("test@mywebsite.co.uk", "mywebsite.co.uk");
                        mail.To.Add(new MailAddress("test@mywebsite.co.uk"));
                        //mail.CC.Add(new MailAddress("info@mywebsite.co.uk"));
                        mail.Body = sb.ToString();
                        mail.Subject = "Contact Form from Mywebsite website";
                        mail.IsBodyHtml = true;
                        //following line disabled - uncommit to send emails
                        //smtpClient.Send(mail);

                        //Insert Form into database

                        ViewBag.msg = "Successfuly sent. Someone will be in touch within 1 working day.";
                        ViewBag.status = 3;
                    }
                    catch (Exception ex)
                    {
                        ViewBag.msg = "Error! Sorry, Information cannot be sent this time. Please contact us at info@";
                        ViewBag.status = 2;
                    }
                }
            }
            else
            {
                ViewBag.status = 1;
            }
            return View();
        }



        public bool Insert2Db(string name, string telephone, string email, string msg, string ip)
        {
            Contactus contactus = new Contactus();
            contactus.Name = name;
            contactus.Telephone = telephone;
            contactus.Email = email;
            contactus.Msg = msg;
            contactus.Ip = ip;
            contactus.Created = DateTime.Now;
            contactus.Status = StatusTypes.Deleted;

            db.Contacts.Add(contactus);
            db.SaveChanges();

            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}