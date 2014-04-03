using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using System.Net;
using System.Net.Mail;
using LeagueManager.Models;

namespace LeagueManager.Controllers
{
    public class EmailController : Controller
    {
        //
        // GET: /Email/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LeagueManager.Models.MailerModel obj,HttpPostedFileBase fileUploader)
        {
            MailerModel MO = new MailerModel();
            if (ModelState.IsValid)
            {
                using (MailMessage mail = new MailMessage())
                {
                    string strMailFrom = MO.EmailFrom;
                    string strMailHost = MO.EmailHost;
                    string strMailPassword = MO.EmailPassword;
                    string strPort = MO.EmailPort;
                    string strEnableSSL = MO.EnableSsl;

                    mail.To.Add(obj.SendTo) ;
                    mail.From = new MailAddress(strMailFrom);
                    mail.Subject = obj.Subject;
                    mail.Body = obj.Body;       // This is code for the body of the message
                    if (fileUploader != null)
                    {
                        string strFileName= Path.GetFileName(fileUploader.FileName);
                        mail.Attachments.Add(new Attachment(fileUploader.InputStream,strFileName));
                    }
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = strMailHost;
                    smtp.Port = Convert.ToInt32(strPort);
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(strMailFrom, strMailPassword);
                    smtp.Send(mail);
                    ViewBag.Message = "Mail sent successfully to the selected user";
                    return View("Index",obj);

                }
            }
            return View();
        }


    }
}
