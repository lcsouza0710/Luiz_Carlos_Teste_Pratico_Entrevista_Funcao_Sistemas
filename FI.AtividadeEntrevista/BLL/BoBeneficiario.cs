using FI.AtividadeEntrevista.DAL;
using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        public long Incluir(Beneficiario beneficiario)
        {
            DAOBeneficiario benef = new DAOBeneficiario();
            return benef.Incluir(beneficiario);
        }

        public void Alterar(Beneficiario beneficiario)
        {
            DAOBeneficiario benef = new DAOBeneficiario();
            benef.Alterar(beneficiario);
        }

        public Beneficiario Consultar(long id)
        {
            DAOBeneficiario benef = new DAOBeneficiario();
            return benef.Consultar(id);
        }

        public void Excluir(long id)
        {
            DAOBeneficiario benef = new DAOBeneficiario();
            benef.Excluir(id);
        }

        public List<Beneficiario> Listar()
        {
            DAOBeneficiario benef = new DAOBeneficiario();
            return benef.Listar();
        }

        public List<Beneficiario> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, long idCliente, out int qtd)
        {
            DAOBeneficiario benef = new DAOBeneficiario();
            return benef.Pesquisa(iniciarEm, quantidade, campoOrdenacao, crescente, idCliente, out qtd);
        }

        public bool VerificarExistencia(string CPF, long idCliente)
        {
            DAOBeneficiario benef = new DAOBeneficiario();
            return benef.VerificarExistencia(CPF, idCliente);
        }
    }
}
