using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace amoboe.ViewModels.Account
{
    public class RegisterVM
    {
        [Required]
        [MinLength(3, ErrorMessage = "Adiniz min 3 simvol olmalidir")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Adiniz min 3 simvol olmalidir")]

        public string Surname { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password),Compare(nameof(ConfirmPassword))]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
