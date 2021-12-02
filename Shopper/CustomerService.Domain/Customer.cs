using Core.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerService.Domain
{
    public class Customer : BaseEntity
    {
        //[Required]
        //[MaxLength(50)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Pesel { get; set; }
        public string Password { get; set; }

        // [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }        
    }
    

}
