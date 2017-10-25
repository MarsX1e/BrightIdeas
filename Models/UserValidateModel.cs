using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace BrightIdeas.Models
{
    public class UserValidateModel
    {
        [Required]
        [MinLength(2,ErrorMessage="Name has to be at least 2 letters.")]
        [RegularExpression(@"^[a-zA-Z ]+$",ErrorMessage="Name only contains letters and space")]
        public string Name {get;set;}
        [Required]
        [MinLength(2,ErrorMessage="Alias has to be at least 2 letters.")]
        [RegularExpression(@"^[a-zA-Z]+$",ErrorMessage="Alias only contains letters")]
        public string Alias {get;set;}
        [Required]
        [EmailAddress]
        public string Email {get;set;}
        [Required]
        [MinLength(8)]
        [RegularExpression(@"^.*(?=.*\d)(?=.*[a-zA-Z])(?=.*[~!*@#$%^&+=]).*$",ErrorMessage="Password has to have at least one number one letter and one special charactor.")]
        [DataType(DataType.Password)]
        public string Password {get;set;}
        [Compare("Password",ErrorMessage="Password and confirmation must match.")]
        public string cp{get;set;}
    }
}