using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAtividadeEntrevista.Utils
{
    public class Utils
    {
        public static bool ValidarCPF(string digitos)
        {
            var digito = "";
            double soma = 0;
            double resto = 0;

            //digitos = Regex.Replace(digitos, @"[^\d]", "");

            if (digitos.Length != 11)
                return false;

            string[] rpt = {"11111111111","22222222222","33333333333","44444444444",
                            "55555555555","66666666666","77777777777","88888888888",
                            "99999999999","00000000000"};

            if (rpt.Contains(digitos)) return false;

            var cpf = digitos.Select(char.GetNumericValue).ToArray();

            for (int i = 0; i < 9; i++)
                soma += cpf[i] * (10 - i);

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito = resto.ToString();
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += cpf[i] * (11 - i);

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito = string.Concat(digito, resto);

            return digitos.EndsWith(digito);
        }
    }
}