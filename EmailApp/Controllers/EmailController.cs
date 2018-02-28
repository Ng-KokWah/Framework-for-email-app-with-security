using EmailApp.Models;
using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace EmailApp.Controllers
{
    public class EmailController : Controller
    {
        public static string sender = "";
        public static string password = "";

        public ActionResult SendEmail()
        {
            //string email = sender;
            //ViewBag.Message("Hi "+email+"! Write your email below!");
            //System.Diagnostics.Debug.WriteLine("first load "+sender + " " + password);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendEmail(EmailModel em)
        {
            //System.Diagnostics.Debug.WriteLine("post load " + sender + " " + password);
            //"smtp.mail.yahoo.com" for yahoo
            //"smtp.live.com" for hotmail
            //all on port 587 & requires ssl
            string[] temp = sender.Split('@');
            SmtpClient smtpClient = new SmtpClient();
            if (temp[1].Contains("gmail"))
            {
                smtpClient = new SmtpClient("smtp.gmail.com");
            }
            else if(temp[1].Contains("yahoo"))
            {
                smtpClient = new SmtpClient("smtp.mail.yahoo.com");
            }
            else
            {
                smtpClient = new SmtpClient("smtp.live.com");
            }
            //most mail like gmail requires ssl to be enabled to send emails else it would fail
            smtpClient.EnableSsl = true;
            // set smtp-client with basicAuthentication
            smtpClient.UseDefaultCredentials = false;
            //set the credentials to login to the email, like the username and password
            System.Net.NetworkCredential basicAuthenticationInfo = new
               System.Net.NetworkCredential(cleanUpInput(sender), cleanUpInput(password));
            smtpClient.Credentials = basicAuthenticationInfo;

            //add the to and from addresses
            MailAddress from = new MailAddress(cleanUpInput(sender));
            MailAddress to = new MailAddress(cleanUpInput(em.Recipient));
            MailMessage mail = new System.Net.Mail.MailMessage(from, to);
            if (!(String.IsNullOrEmpty(em.cc) || String.IsNullOrWhiteSpace(em.cc)))
            {
                //cc
                MailAddress copy = new MailAddress(cleanUpInput(em.cc));
                mail.CC.Add(copy);
            }
            if (!(String.IsNullOrEmpty(em.bcc) || String.IsNullOrWhiteSpace(em.bcc)))
            {
                //bcc
                MailAddress Bcopy = new MailAddress(cleanUpInput(em.bcc));
                mail.Bcc.Add(Bcopy);
            }
            /*// add ReplyTo only if needed
            MailAddress replyTo = new MailAddress(sender);
            mail.ReplyToList.Add(replyTo);
            */
            // set subject and encoding type
            mail.Subject = cleanUpInput(em.subject);
            mail.SubjectEncoding = System.Text.Encoding.UTF8;

            // set message and encoding type
            mail.Body = cleanUpInput(em.body);
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            // if pure text set to false if html codes are involved set to true
            mail.IsBodyHtml = false;

            smtpClient.Send(mail);
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "{some text for type}", "alert('{Text come to here}'); ", true);
            return View();
        }

        public ActionResult EmailLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmailLogin(LoginModel lm)
        {
            //System.Diagnostics.Debug.WriteLine(lm.email + " " + lm.password);
            sender = cleanUpInput(lm.email);
            password = cleanUpInput(lm.password);
            return RedirectToAction("SendEmail");
        }

        ////OWASP Top 10: Sql Injection & XSS (Cross-site Scripting) Attack
        //Protection against: Sql Injection & XSS (Cross-site Scripting) Attack
        //Method: Input Sanitization, Clean up the data before making the query to the database
        public string cleanUpInput(string phrase)
        {
            var sanitizer = new HtmlSanitizer();
            string cleanedInput = sanitizer.Sanitize(phrase);
            cleanedInput = Regex.Replace(cleanedInput, @"[^0-9a-zA-Z ///@/:/./,]+", "");
            return cleanedInput;
        }
    }
}