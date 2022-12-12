using Okr.Services.Identity.Model;

namespace Okr.Services.Identity.Repository
{
    public interface IUserService
    {
        UserModel CreateUser(UserModel userModel);
        IList<UserModel> GetAllUser();
        string GetTokenLoginUser(UserModel userModel);


    }
}
