using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gufi.WebApi.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o Email caralio")]

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o Senha plis")]

        [DataType(DataType.Password)]
        public string Senha { get; set; } 
    }
}
