namespace RecorderSystem.Entities
{
    public class StudentRegistrationInput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string EmailAddress { get; set; }
        public string MobilePhone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}
