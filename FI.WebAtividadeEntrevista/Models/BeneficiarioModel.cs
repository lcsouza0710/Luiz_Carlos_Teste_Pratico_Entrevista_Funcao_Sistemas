using FI.WebAtividadeEntrevista.Models.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FI.WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
    {
        public long Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [CustomValidation(typeof(BrazilianCPFAttribute), "Validate")]
        [Required]
        public string CPF { get; set; }

        public long IdCliente { get; set; }
    }
}