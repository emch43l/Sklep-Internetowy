using Microsoft.AspNetCore.Identity;

namespace Sklep_Internetowy.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
