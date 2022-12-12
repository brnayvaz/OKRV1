using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Okr.Entities;
using Okr.Services.Identity.DbContext;
using Okr.Services.Identity.Mapping;
using Okr.Services.Identity.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Okr.Services.Identity.Repository
{
    public class UserService : IUserService
    {
        private UserDbContext userDbContext { get; set; }
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UserService(IServiceProvider provider, IMapper mapper, IConfiguration config)
        {

            userDbContext = provider.GetService<UserDbContext>();
            _mapper = mapper;
            _config = config;
        }

        public UserModel CreateUser(UserModel userModel)
        {
            var userEntitiy = _mapper.Map<User>(userModel);
            var userCreated = userDbContext.Add(userEntitiy).Entity;
            userDbContext.SaveChanges();

            return _mapper.Map<UserModel>(userCreated);

        }

        public IList<UserModel> GetAllUser()
        {

            var userList = userDbContext.Users.ToList();
            return _mapper.Map<IList<UserModel>>(userList);

        }

        public string GetTokenLoginUser(UserModel userModel)
        {
            var userEntitiy = userDbContext.Users.FirstOrDefault(c => c.name == userModel.name && c.password == userModel.password);


            if (userEntitiy != null)
            {
                userModel.password = "";
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config["Secret"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        //new Claim(JwtRegisteredClaimNames.Sub, "ClientId"),
                        new Claim(ClaimTypes.Name, userEntitiy.name),
                        new Claim(ClaimTypes.Role, userEntitiy.role),
                        new Claim("ClientInfo", JsonConvert.SerializeObject(userModel))
                    }),
                    //Expire token after some time	                
                    Expires = DateTime.UtcNow.AddDays(7),
                    //Let's also sign token credentials for a security aspect, this is important!!!	                
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return tokenString;

            }
            else
            {
                throw new Exception("Login işlemi başarısız");


            }
        }
    }
}
