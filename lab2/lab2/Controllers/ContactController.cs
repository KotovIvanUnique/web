using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;


namespace lab2.Controllers
{
    public class ContactController : Controller
    {
        private readonly SMTPSettings _options;

        public ContactController(IOptions<SMTPSettings> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(Message message)
        {
            var email = _options.Email;
            var password = _options.Password;
            var port = int.Parse(_options.SMTPPort);
            var server = _options.SMTPServer;

           TryValidateModel(message);
            
            var mimMessage = new MimeMessage();

            mimMessage.From.Add(new MailboxAddress(message.Email, email));
            mimMessage.To.Add(new MailboxAddress("Admin E-mail", email));
            mimMessage.Subject = message.Subject;
            mimMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)

            {
                Text = message.Text
            };

            using (var smtpclient = new SmtpClient())
            {
                await smtpclient.ConnectAsync(server, port, false);
                await smtpclient.AuthenticateAsync(email, password);
                await smtpclient.SendAsync(mimMessage);
                await smtpclient.DisconnectAsync(true);
            }

            return RedirectToAction("Index");
        }
    }
}