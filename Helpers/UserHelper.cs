using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JeffPaulin.Models;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel;

namespace JeffPaulin.Helpers
{
    public class UserHelper
    {
        private jpContext db = new jpContext();
        public async Task<User> getUser(string oid)
        {
            var guid = Guid.Parse(oid);
            var user = db.Users.Where(x => x.UserGuid == guid).FirstOrDefault();

            if (user != null)
            {
                return user;
            }
            var newUser = new User() { UserGuid = guid, CreatedDate = DateTime.UtcNow };
            await db.Users.AddAsync(newUser);
            await db.SaveChangesAsync();
            return newUser;
        }
    }
}
