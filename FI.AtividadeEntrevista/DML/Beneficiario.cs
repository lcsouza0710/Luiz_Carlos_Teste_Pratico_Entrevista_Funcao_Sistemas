using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.DML
{
    /// <summary>
    /// Classe de beneficiário que represente o registro na tablea Beneficiario do banco de dados
    /// </summary>
    public class Beneficiario
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Id do Cliente
        /// </summary>
        public long IdCliente { get; set; }
    }
}
