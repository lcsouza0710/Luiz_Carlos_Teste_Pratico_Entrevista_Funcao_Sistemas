using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Beneficiario
    /// </summary>
    internal class DAOBeneficiario : AcessoDados
    {
        /// <summary>
        /// Classe de acesso a dados de Cliente
        /// </summary>
        /// <param name="beneficiario">Objeto beneficiário</param>
        internal long Incluir(Beneficiario beneficiario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("Nome", beneficiario.Nome));
            parametros.Add(new SqlParameter("CPF", beneficiario.CPF.Trim().Replace(".", "").Replace("-", "")));
            parametros.Add(new SqlParameter("IdCliente", beneficiario.IdCliente));

            DataSet ds = base.Consultar("FI_SP_IncBenef", parametros);
            long ret = 0;

            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Consulta o registro do beneficíario através de número de Id
        /// </summary>
        /// <param name="id">Id do beneficiário a ser consultado</param>
        /// <returns></returns>
        internal Beneficiario Consultar(long id)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("Id", id));

            DataSet ds = base.Consultar("FI_SP_ConsBenef", parametros);
            List<DML.Beneficiario> beneficiarios = Converter(ds);

            return beneficiarios.FirstOrDefault();
        }

        /// <summary>
        /// Verifica se há um beneficiário com o mesmo CPF, vinculado ao mesmo cliente
        /// </summary>
        /// <param name="CPF">Numero de CPF do beneficiário</param>
        /// <returns></returns>
        internal bool VerificarExistencia(string CPF, long idCliente)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("CPF", CPF.Trim().Replace(".", "").Replace("-", "")));
            parametros.Add(new SqlParameter("IdCliente", idCliente));

            DataSet ds = base.Consultar("FI_SP_VerificaBenef", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        internal List<Beneficiario> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, long idCliente, out int qtd)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("iniciarEm", iniciarEm));
            parametros.Add(new SqlParameter("quantidade", quantidade));
            parametros.Add(new SqlParameter("campoOrdenacao", campoOrdenacao));
            parametros.Add(new SqlParameter("crescente", crescente));
            parametros.Add(new SqlParameter("IdCliente", idCliente));

            DataSet ds = base.Consultar("FI_SP_PesqBeneficiario", parametros);
            List<Beneficiario> beneficiarios = Converter(ds);

            int iQtd = 0;

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);

            qtd = iQtd;

            return beneficiarios;
        }

        /// <summary>
        /// Lista todos os registros de beneficiários
        /// </summary>
        /// <returns></returns>
        internal List<Beneficiario> Listar()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("Id", 0));

            DataSet ds = base.Consultar("FI_SP_ConsCliente", parametros);
            List<Beneficiario> beneficiarios = Converter(ds);

            return beneficiarios;

        }

        /// <summary>
        /// Altera os dados do beneficário
        /// </summary>
        /// <param name="beneficiario">Objeto com os dados do beneficiário</param>
        internal void Alterar(Beneficiario beneficiario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("Nome", beneficiario.Nome));
            parametros.Add(new SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new SqlParameter("IdCliente", beneficiario.IdCliente));
            parametros.Add(new SqlParameter("Id", beneficiario.Id));

            base.Executar("FI_SP_AltBenef", parametros);
        }

        /// <summary>
        /// Exclui o registro do beneficiário
        /// </summary>
        /// <param name="id">Id do beneficário</param>
        internal void Excluir(long id)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter("Id", id));

            base.Executar("FI_SP_DelBenef", parametros);
        }

        private List<Beneficiario> Converter(DataSet ds)
        {
            List<Beneficiario> lista = new List<Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Beneficiario beneficiario = new Beneficiario();
                    beneficiario.Id = row.Field<long>("Id");
                    beneficiario.Nome = row.Field<string>("Nome");
                    beneficiario.CPF = row.Field<string>("CPF");
                    beneficiario.IdCliente = row.Field<long>("IdCliente");
                    lista.Add(beneficiario);
                }
            }

            return lista;
        }
    }
}
