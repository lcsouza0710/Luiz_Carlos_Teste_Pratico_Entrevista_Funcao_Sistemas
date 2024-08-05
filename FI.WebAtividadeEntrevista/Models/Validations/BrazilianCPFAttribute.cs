using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using Microsoft.Ajax.Utilities;

namespace FI.WebAtividadeEntrevista.Models.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class BrazilianCPFAttribute : ValidationAttribute
    {
        public static ValidationResult Validate(object value)
        {
            string stringValue = value.ToString().Replace(".", "").Replace("-", "");

            if (stringValue.Length != 11)
            {
                return new ValidationResult("CPF incompleto");
            }
            else
            {
                string dataToValidate = stringValue.Substring(0, 10);

                if (validateDigit(dataToValidate))
                {
                    dataToValidate = stringValue;

                    if (validateDigit(stringValue, false))
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult("CPF inválido");
                    }
                }
                else
                {
                    return new ValidationResult("CPF invalido");
                }
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }

        protected static bool validateDigit(string value, bool firstDigit = true)
        {
            const int FACTOR = 11;
            bool result = true;
            int multiplier = firstDigit ? 10 : 11;
            int total = 0;
            int referenceDigit = Convert.ToByte(value.Substring(value.Length - 1, 1));
            int verifierDigit = 0;
            char[] digits = value.Remove(value.Length-1, 1).ToCharArray();
            int modValue;

            for (int i = 0; i < digits.Length; i++)
            {
                total += (Convert.ToInt32(digits[i].ToString()) * multiplier);

                multiplier--;
            }

            modValue = total % FACTOR;

            if (modValue <= 2)
            {
                verifierDigit = 0;
            }
            else
            {
                verifierDigit = FACTOR - modValue;
            }

            result = referenceDigit.Equals(verifierDigit);
            return result;
        }
    }
}