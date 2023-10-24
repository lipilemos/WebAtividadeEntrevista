using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Beneficiario
    /// </summary>
    public class BeneficiarioModel
    {
        public long Id { get; set; }
        
        /// <summary>
        /// Cidade
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Digite um CPF válido no formato 999.999.999-99 (somente números)")]
        public string CPF { get; set; }

        /// <summary>
        /// IDCliente
        /// </summary>
        public long IDCliente { get; set; }
    }    
}