using Microsoft.AspNetCore.Identity;

namespace PostTradesThree.Domain
{
    public class PttUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
