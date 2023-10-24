using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        [HttpPost]
        public JsonResult IncluirBeneficiario(BeneficiarioModel model)
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
                bool valid = bo.ValidaCPF(model.CPF);
                if(!valid)
                    return Json("CPF inválido");


                bool exist = bo.VerificarExistencia(model.CPF);

                    if (!exist) {
                    model.Id = bo.Incluir(new Beneficiario()
                    {
                        Nome = model.Nome,
                        CPF = model.CPF,
                        IDCliente = model.IDCliente
                    });
                    return Json("Cadastro efetuado com sucesso");
                }
                else
                    return Json("CPF já cadastrado");
            }
        }
        [HttpPost]
        public JsonResult AlterarBeneficiario(BeneficiarioModel model)
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
                bo.Alterar(new Beneficiario()
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    CPF = model.CPF,
                    IDCliente = model.IDCliente
                });
                               
                return Json("Cadastro alterado com sucesso");
            }
        }
        [HttpGet]
        public ActionResult AlterarBeneficiario(long id)
        {
            BoBeneficiario bo = new BoBeneficiario();
            Beneficiario beneficiario = bo.Consultar(id);
            Models.BeneficiarioModel model = null;

            if (beneficiario != null)
            {
                model = new BeneficiarioModel()
                {
                    Id = beneficiario.Id,
                    CPF = beneficiario.CPF,
                    Nome = beneficiario.Nome,
                    IDCliente = beneficiario.IDCliente,
                };            
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult BeneficiarioList(long id)
        {
            try
            {
                List<Beneficiario> clientes = new BoBeneficiario().Listar(id);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = clientes.Count() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        
    }
}