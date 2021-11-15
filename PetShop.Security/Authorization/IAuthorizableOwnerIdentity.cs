namespace PetShop.Security.Authorization
{
    public interface IAuthorizableOwnerIdentity
    {
        long getAuthorizedOwnerId();
        string getAuthorizedOwnerName();
    }
}