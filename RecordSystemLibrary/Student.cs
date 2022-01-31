namespace RecordSystemLibrary
{
    public class Student
    {
        public int Id { get; set; }
        public int MyProperty { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string EmailAddress { get; set; }
        public string MobilePhone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Address { get; set; }
        public City City { get; set; }
    }
}
