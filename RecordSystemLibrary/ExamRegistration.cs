namespace RecordSystemLibrary
{
    public class ExamRegistration
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public ExamType ExamType { get; set; }
        public Student Student { get; set; }
        public DrivingCategory DrivingCategory { get; set; }
        public DateTime TakenAt { get; set; }
    }
}
