using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using FI.WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FI.WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        // GET: Beneficiario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Incluir(long? IdCliente)
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                model.Id = bo.Incluir(new Beneficiario()
                {
                    Nome = model.Nome,
                    CPF = model.CPF,
                    IdCliente = model.IdCliente,
                });

                return Json("Cadastro de beneficiário realizado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Alterar(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join( Environment.NewLine, erros));
            }
            else
            {
                bo.Alterar(new Beneficiario()
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    CPF = model.CPF,
                    IdCliente = model.IdCliente,
                });

                return Json("Dados de beneficiário alterado com sucesso");
            }
        }

        public ActionResult Alterar (long? id)
        {
            if (id.HasValue)
            {
                BoBeneficiario bo = new BoBeneficiario();
                Beneficiario beneficiario = bo.Consultar(id.Value);
                Models.BeneficiarioModel model = null;

                if (beneficiario != null)
                {
                    model = new BeneficiarioModel()
                    {
                        Id = beneficiario.Id,
                        CPF = beneficiario.CPF,
                        Nome = beneficiario.Nome,
                        IdCliente = beneficiario.IdCliente
                    };
                }

                return View(model);
            }
            else
            {
                return null;
            }
        }

        public JsonResult BeneficiarioList(long idCliente, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Beneficiario> beneficiarios = new BoBeneficiario().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), idCliente, out qtd);

                return Json(new { Result = "OK", Records = beneficiarios, TotalRecordCount = qtd });
            }
            catch (Exception ex) 
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public PartialViewResult Listagem(long? IdCliente)
        {
            return PartialView("Listagem");
        }

        public PartialViewResult PopUpBeneficiarios(long? IdCliente)
        {
            return PartialView("PopUpBeneficiarios");
        }
    }
}