namespace RealEstate.Core
{
    public interface IRequestContextProvider
    {
        //bool IsAdmin();
        //bool IsUser();

        //string? GetUserId();
        string? GetUserName();
        string? GetUserEmail();
    }
}
