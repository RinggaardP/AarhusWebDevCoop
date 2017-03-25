using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AarhusWebDevCoop.ViewModels;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;
using System.Net.Mail;

namespace AarhusWebDevCoop.Controllers
{


   

   


    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurface
        public ActionResult Index()
        {

            return PartialView("ContactForm", new ContactForm());
        }



        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm model)
        {
            if (!ModelState.IsValid)
            {
               
                return CurrentUmbracoPage();
            }
            
            /*create new mail to send based on the form*/
            MailMessage message = new MailMessage();
            message.To.Add("jule.p3t@gmail.com");
            message.Subject = model.Subject;
            message.From = new MailAddress(model.Email, model.Name);
            message.Body = model.Message;

            /*create new documents based on the form*/
            IContent comment = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "message");
            comment.SetValue("messageName", model.Name);
            comment.SetValue("email", model.Email);
            comment.SetValue("subject", model.Subject);
            comment.SetValue("messageContent", model.Message);

            //Services.ContentService.SaveAndPublishWithStatus(comment);
            Services.ContentService.Save(comment);


            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("juletheforumtroll@gmail.com", "CocaCola2017");
                smtp.EnableSsl = true;

                //send mail
                smtp.Send(message);
            }
            TempData["success"] = true;
            return RedirectToCurrentUmbracoPage();
        }
    }
}