using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace PCNWUserManagementApp.ViewModels
{
    public class UserViewModel
    {
        public int Userid { get; set; }
        [Required]
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string ImagePath { get; set; } = "abcd";
        
        public string ProjectName { get; set; }
        
        public string projectFile { get; set; } = "abcdPro";
        public string UserType { get; set; }
    }
}
