using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var context = new NijatPortfolioEntities();

            ViewBag.Contacts = context.Contacts.Where(m => m.ShowOnContactPage == true).ToList<Contact>();
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserMessage message)
        {
            if (ModelState.IsValid)
            {
                var context = new NijatPortfolioEntities();

                if (IsEmail(message.UserMail))
                {
                    context.UserMessages.Add(new UserMessage()
                    {
                        UserName = message.UserName,
                        UserMail = message.UserMail,
                        UserMsg = message.UserMsg,
                        MessageDate = DateTime.Now
                    });
                    context.SaveChanges();

                    //Email(userName, userMail, userMessage);
                }
            }
            
            return View();
        }

        public static void Email(string username, string email, string userMessage)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("nijatshj@code.edu.az");
                message.To.Add(new MailAddress("nicat.ceferov14@gmail.com"));
                message.Subject = "Test";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = $"<p>{userMessage}\n\n{email}</p>";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("nijatshj@code.edu.az", "0554152312nc");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }

        private bool IsEmail(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}