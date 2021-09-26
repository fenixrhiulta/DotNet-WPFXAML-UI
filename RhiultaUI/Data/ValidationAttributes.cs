using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RhiultaUI
{
    public class MinAndMaxString : StringLengthAttribute
    {
        public MinAndMaxString() : this(20)
        {
        }
        public MinAndMaxString(int maximumLength) : base(maximumLength)
        {
            base.ErrorMessage = "O campo deve ter entre {2} e {1} caracteres.";
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class IsBoleto : ValidationAttribute
    {
        public IsBoleto()
        {
            ErrorMessage = "Esta não é uma linha digitável válida.";
        }

        public override bool IsValid(object value)
        {
            string linhaDigitavel = value.ToString();

            if (linhaDigitavel.Length != 44)
            {
                base.ErrorMessage = "O campo deve ter entre {2} e {1} caracteres.";
                return false;
            }

            return true;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NotNull : RequiredAttribute
    {
        public NotNull()
        {
            this.ErrorMessage = "Necessário o preenchimento deste campo.";
        }

        public override bool IsValid(object value)
        {
            string vl = value == null ? " " : value.ToString();


            if (vl.IsNullOrWhiteSpace())
            {
                return false;
            }
            else
            {
                return true;
            }

            return true;

        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class IsCNPJ : ValidationAttribute
    {
        public IsCNPJ()
        {
            ErrorMessage = "Este não é um CNPJ válido.";
        }

        public override bool IsValid(object value)
        {
            string cnpj = value.ToString();
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");
            if (cnpj.Length != 14) return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class IsCEP : ValidationAttribute
    {
        public IsCEP()
        {
            ErrorMessage = "Este não é um CEP válido.";
        }

        public override bool IsValid(object value)
        {
            return false;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class OnlyWord : ValidationAttribute
    {
        public OnlyWord()
        {
            ErrorMessage = "Caracteres especiais não são permitidos.";
        }

        public override bool IsValid(object value)
        {
            return (Regex.IsMatch(value.ToString(), @"^[a-zA-Z0-9 ]+$")) ? true : false;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class NotAllowSpace : ValidationAttribute
    {
        public NotAllowSpace()
        {
            ErrorMessage = "Espaços não são permitidos.";
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;
            return (!Regex.IsMatch(value.ToString(), @"\s+")) ? true : false;
        }
    }
}