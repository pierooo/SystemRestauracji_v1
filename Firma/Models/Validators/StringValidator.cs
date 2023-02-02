using System;
using System.Linq;

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
            catch (Exception) { return "Zły String"; }
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
            catch (Exception) { return "Zły String"; }
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
    }
}
