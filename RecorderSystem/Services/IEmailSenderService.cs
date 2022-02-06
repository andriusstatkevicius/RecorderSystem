using RecordSystemLibrary;

namespace RecorderSystem.Services
{
    public interface IEmailSenderService
    {
        void SendEmail(Student student, List<ExamRegistration> examRegistrations);
    }
}
