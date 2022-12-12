using Okr.Services.Identity.Model.Base;

namespace Okr.Services.Identity.Model
{
    public class UserModel:BaseModel
    {
        public string name { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
}
