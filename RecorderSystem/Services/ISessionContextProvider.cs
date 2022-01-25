namespace RecorderSystem.Services
{
    public interface ISessionContextProvider
    {
        bool IsLoggedIn { get; set; }
    }
}
