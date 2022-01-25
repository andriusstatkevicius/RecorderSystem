namespace RecorderSystem.Services
{
    public class SessionContextProvider : ISessionContextProvider
    {
        public bool IsLoggedIn { get; set; }
        public SessionContextProvider()
        {

        }
    }
}
