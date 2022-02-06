using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using RecordSystemLibrary;

namespace RecorderSystem.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly AppSettings _appSettings;

        public EmailSenderService(IOptions<AppSettings> settings)
        {
            _appSettings = settings.Value ?? throw new ArgumentException(nameof(settings));
        }

        public void SendEmail(Student student, List<ExamRegistration> examRegistrations)
        {
            if (!_appSettings.Credentials.IsConfigured)
                return;

            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress(_appSettings.Credentials.User, _appSettings.Credentials.UserName));
            mailMessage.To.Add(new MailboxAddress(student.FirstName, student.EmailAddress));
            mailMessage.Subject = "Successful registration for driving/theory exam";
            mailMessage.Body = new TextPart("plain")
            {
                Text = FormBody(student, examRegistrations)
            };

            using var smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 465, true);
            smtpClient.Authenticate(_appSettings.Credentials.UserName, _appSettings.Credentials.Password);
            smtpClient.Send(mailMessage);
            smtpClient.Disconnect(true);
        }

        private string FormBody(Student student, List<ExamRegistration> examRegistrations)
        {
            var body = $"Dear {student.FirstName},\nYou have successfully been registered for exams:\n";
            examRegistrations.ForEach(x => body += $"{x.ExamType}, id: {x.Id}, taken at: {x.TakenAt}\n");
            return body;
        }
    }
}
