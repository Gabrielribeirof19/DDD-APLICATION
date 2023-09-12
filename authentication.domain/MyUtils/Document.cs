using System.Text.RegularExpressions;

namespace authentication.domain.MyUtils
{
    public static class Document
    {
        private static readonly string[] cnpjInvalid =
        {
        "00000000000000", "11111111111111", "22222222222222", "33333333333333",
        "44444444444444", "55555555555555", "66666666666666", "77777777777777",
        "88888888888888", "99999999999999",
    };
        public static bool IsCNPJ(string? value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            value = value.Trim();
            value = value.Replace(".", "").Replace("-", "").Replace("/", "");
            if (value.Length != 14 || !long.TryParse(value, out var parsed))
                return false;
            if (cnpjInvalid.Contains(value))
                return false;
            tempCnpj = value.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;
            digito = digito + resto;
            return value.EndsWith(digito);
        }

        private static readonly string[] cpfInvalid =
        {
        "00000000000", "11111111111", "22222222222",
        "33333333333", "44444444444", "55555555555",
        "66666666666", "77777777777", "88888888888", "99999999999"
    };

        // https://github.com/lira92/flunt.br/tree/develop/src/Flunt.Br/Validations
        public static bool IsCPF(string? value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            value = value.Trim();
            value = value.Replace(".", "").Replace("-", "");
            if (!long.TryParse(value, out var parsed))
                return false;
            if (value.Length != 11 || cpfInvalid.Contains(value))
                return false;
            tempCpf = value.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito = digito + resto;
            return value.EndsWith(digito);
        }

        public static bool IsCEP(string value)
        {
            if (value.IndexOf("-") > 0)
            {
                return new Regex(@"^\d{5}-\d{3}$", RegexOptions.Singleline).Match(value).Success;
            }

            return new Regex(@"^\d{8}$", RegexOptions.Singleline).Match(value).Success;
        }

        public static (string, string) GetCPFFormats(string value)
        {
            string onlyNumbers = value.Replace(".", "").Replace("-", "");

            if (IsCPF(onlyNumbers))
            {
                string withDots =
                    onlyNumbers.Substring(0, 3) + "." +
                    onlyNumbers.Substring(3, 3) + "." +
                    onlyNumbers.Substring(6, 3) + "-" +
                    onlyNumbers.Substring(9, 2);

                return (onlyNumbers, withDots);
            }

            return ("", "");
        }

    }
}