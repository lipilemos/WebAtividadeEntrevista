﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Beneficiarios
    /// </summary>
    internal class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal long Incluir(DML.Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            
            parametros.Add(new System.Data.SqlClient.SqlParameter("NOME", beneficiario.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IDCLIENTE", beneficiario.IDCliente));

            DataSet ds = base.Consultar("FI_SP_IncBeneficiario", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal DML.Beneficiario Consultar(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", Id));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiario", parametros);
            List<DML.Beneficiario> cli = Converter(ds);

            return cli.FirstOrDefault();
        }

        internal bool VerificarExistencia(string CPF)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF));

            DataSet ds = base.Consultar("FI_SP_VerificaClienteV2", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        internal List<Beneficiario> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("iniciarEm", iniciarEm));
            parametros.Add(new System.Data.SqlClient.SqlParameter("quantidade", quantidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("campoOrdenacao", campoOrdenacao));
            parametros.Add(new System.Data.SqlClient.SqlParameter("crescente", crescente));

            DataSet ds = base.Consultar("FI_SP_PesqBeneficiario", parametros);
            List<DML.Beneficiario> cli = Converter(ds);

            int iQtd = 0;

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);

            qtd = iQtd;

            return cli;
        }

        /// <summary>
        /// Lista todos os beneficiarios
        /// </summary>
        internal List<DML.Beneficiario> Listar(long IDCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("IDCliente", IDCliente));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiarioCliente", parametros);
            List<DML.Beneficiario> cli = Converter(ds);

            return cli;
        }

        /// <summary>
        /// Inclui um novo Beneficiario
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Alterar(DML.Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", beneficiario.Id));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IDCliente", beneficiario.IDCliente));

            base.Executar("FI_SP_AltBeneficiario", parametros);
        }


        /// <summary>
        /// Excluir Beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal void Excluir(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            base.Executar("FI_SP_DelBeneficiario", parametros);
        }

        private List<DML.Beneficiario> Converter(DataSet ds)
        {
            List<DML.Beneficiario> lista = new List<DML.Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Beneficiario ben = new DML.Beneficiario();
                    ben.Id = row.Field<long>("Id");
                    ben.Nome = row.Field<string>("Nome");
                    ben.CPF = row.Field<string>("CPF");
                    ben.IDCliente = row.Field<long>("IDCliente");
                    lista.Add(ben);
                }
            }

            return lista;
        }
    }
}
