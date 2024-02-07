using EmailService.Models.Dto;
using MimeKit;
using MailKit.Net.Smtp;


namespace EmailService.Services
{
    public class MailsService
    {
        private readonly string _email;
        private readonly string _password;
        private readonly IConfiguration _configuration;

        public MailsService(IConfiguration configuration)
        {
            _configuration = configuration;
            _email = _configuration.GetValue<string>("EmailSettings:Email");
            _password = _configuration.GetValue<string>("EmailSettings:Password");
        }

        public async Task sendEmail(UserMessageDto user, string Message)
        {
            MimeMessage message1 = new MimeMessage();

            message1.From.Add(new MailboxAddress("ArtBbase", _email));

            message1.To.Add(new MailboxAddress(user.Name, user.Email));

            message1.Subject = "Welcome to The ArtBbase";

            var body = new TextPart("html")
            {
                Text = Message.ToString()
            };

            message1.Body = body;

            var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);

            client.Authenticate(_email, _password);

            await client.SendAsync(message1);

            await client.DisconnectAsync(true);
        }
    }
}
