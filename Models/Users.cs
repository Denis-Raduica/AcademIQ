using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AcademIQ.Models
{
    
    public class Users : IdentityUser
    {



        public string UserID => Id;
        
        public string? Name { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public string? ContactInfo { get; set; }
        

        
       


    }
}
