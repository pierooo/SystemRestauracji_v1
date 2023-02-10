using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SystemRestauracji.Models.Validators
{
    public class StringValidator : Validator
    {
        public static string CheckIfStartsWithUpper(string value)
        {
            try
            {
                if (!char.IsUpper(value, 0))
                {
                    return "Rozpocznij duza litera!";
                }
            }
            catch (Exception) { return "Proszę rozpocznij z wielkiej litery"; }
            return null;
        }

        public static string CheckIfCapitalLetters(string wartosc)
        {
            try
            {
                if (!wartosc.All(c => char.IsUpper(c)))
                {
                    return "KOD musi byc drukowanymi!";

                }

            }
            catch (Exception) { return "Proszę wprowadź drukowanymi"; }
            return null;
        }
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public static string IsValidEmailRegex(string email)
        {
            Regex regex = new Regex(@"^([a-z0-9]+)\.?([a-z0-9]+)@([a-z]+)\.[a-z]{2,3}$");
            try
            {
                {
                    var isMatch = regex.IsMatch(email);
                    if (!isMatch)
                    {
                        return "Prosze wprowadz poprawny Mail ! ";
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception) { return "Proszę wprowadź mail małymi literami"; }

        }

    }
}
