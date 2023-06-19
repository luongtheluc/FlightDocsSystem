using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.DataAccess.Repository.IRepository;
using MimeKit;

namespace FlightDocsSystem.DataAccess.Repository
{
    public class SendMailRepository : ISendMailRepository
    {
        public SendMailRepository()
        {

        }

        public async Task SendEmailAsync(EmailDTO request, string filepath = null!)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("thelucpro1306@gmail.com"));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;

            if (filepath != null)
            {
                var image = new MimePart("image", "jpeg")
                {
                    Content = new MimeContent(File.OpenRead(filepath)),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = Path.GetFileName(filepath)
                };
                var multipart = new Multipart("mixed");
                multipart.Add(image);
                multipart.Add(new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = request.Body,
                });
                email.Body = multipart;
            }
            else
            {
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = request.Body,

                };
            }
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("thelucpro1306@gmail.com", "maqnlxwowiroxaow");
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

    }
    public class EmailDTO
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string? Body { get; set; }
    }

}