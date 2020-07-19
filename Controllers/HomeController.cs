using Admination.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admination.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Feedback(string email) 
        {
            if (email == null)
            {
                return View();
            }
            else
            {
                EmailInfo em = new EmailInfo();
                em.From = "ahihi";
                return View(em);
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Feedback(EmailInfo mod)
        {
            if (ModelState.IsValid) 
            {
                var client = new SmtpClient();
                client.EnableSsl = true;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("vytran.bkdn@gmail.com", "vytran123654");
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                var msg = new MailMessage();
                msg.From = new MailAddress(mod.From);
                msg.To.Add(new MailAddress("vytran4795@gmail.com"));
                string sub = "Feedback: " + mod.Subject;
                msg.Subject = sub;
                string ms = "From: " + mod.From + " Message: " + mod.Message;
                msg.Body = ms;

                client.Send(msg);
            }

            return View("");
        }

        public ActionResult ResetPassword() 
        {          
            return View();
        }

        [HttpPost]
        public ActionResult ResetConfirm([Bind(Include = "Email")]ResetPw rs)
        {
            if (ModelState.IsValid)
            {               
                Random rd = new Random();
                int i = rd.Next(100000, 999999);
                // --------------------------------------

                string npw = i.ToString();
                JavaFloristEntities db = new JavaFloristEntities();
                //SQL here***
                //***

                // --------------------------------------
                var client = new SmtpClient();
                client.EnableSsl = true;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("vytran.bkdn@gmail.com", "vytran123654");
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                var msg = new MailMessage();
                msg.From = new MailAddress("Java Florit Online");
                msg.To.Add(new MailAddress(rs.Email)); //email which received code            
                msg.Subject = "Reset Account Password";
                msg.Body = "Your new password: " + npw;

                client.Send(msg);

                return View("");
            }

            return View("");
        }

    }
}