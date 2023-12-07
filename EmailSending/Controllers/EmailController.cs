using EmailSending.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;

namespace EmailSending.Controllers
{
    public class EmailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Email model)
        {
            //using (MailMessage message = new MailMessage(model.Gmail,model.To))
            //{
            //    message.Subject = model.Subject;
            //    message.Body = model.Body;
            //    message.IsBodyHtml = false;
            //    using (SmtpClient smtp = new SmtpClient())
            //    {
            //        smtp.Host = "smtp.gmail.com";
            //        smtp.EnableSsl = true;
            //        NetworkCredential Netcre=new NetworkCredential(model.Gmail,model.To);
            //        smtp.Credentials = Netcre;
            //        smtp.Port = 587;
            //        smtp.Send(message);
            //        ViewBag.Message = "Email Send Successfully";
            //    }
            //}

            //if(!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com");
               
               client.Authenticate(model.Gmail, model.Password);

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $"<p>{model.To}</p><p>{model.Subject}</p><p>{model.Body}</p>",
                    TextBody = "{model.To}\r\n{model.Subject}\r\n{model.Body}\r\n\n{Regards \n Sujit S Kamble}"
                };


                var message = new MimeMessage
                {
                    Body=bodyBuilder.ToMessageBody() 
                };

                
                message.From.Add(new MailboxAddress("Noreply SSK .net developer", model.Gmail));
                message.To.Add(new MailboxAddress("This is Testing Mode", model.To));
                message.Subject = model.Subject;
                client.Send(message);
                client.Disconnect(true);
            }
            TempData["Message"] = "Thank you!";
                return View();  
        }
    }
}
