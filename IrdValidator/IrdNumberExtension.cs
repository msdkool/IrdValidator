using System;
using System.Linq;

namespace IrdValidator
{
    public static class IrdNumberExtension
    {
        public static bool IsIrdNumberValid(this string irdNumber, long lowerLimit = 10000000, long upperLimit = 150000000, string firstCheckDigit = "32765432", string secondCheckDigit = "74325276")
        {
            long irdNo;

            if (string.IsNullOrWhiteSpace(irdNumber))
                return false;

            irdNumber = irdNumber.GetNumbers();
            var irdFlag = long.TryParse(irdNumber, out irdNo);

            if (!irdFlag)
                return false;

            if (irdNo < lowerLimit || irdNo > upperLimit)
                return false;

            var trimmedIrdNumber = irdNumber.Trim();
            var irdLength = trimmedIrdNumber.Length;
            var checkDigit = int.Parse(trimmedIrdNumber[irdLength - 1].ToString());
            var newIrdNumber = trimmedIrdNumber.Remove(irdLength - 1).PadLeft(8, '0');

            var calculatedCheckDigit = GetCalculatedCheckDigit(newIrdNumber, firstCheckDigit);

            if (calculatedCheckDigit >= 0 && calculatedCheckDigit <= 9)
                return calculatedCheckDigit == checkDigit;

            var secondCalculatedCheckDigit = GetCalculatedCheckDigit(newIrdNumber, secondCheckDigit);
            if (secondCalculatedCheckDigit >= 0 && secondCalculatedCheckDigit <= 9)
                return secondCalculatedCheckDigit == checkDigit;

            return false;
        }

        private static int GetCalculatedCheckDigit(string ird, string weights)
        {
            int sum = 0;
            for (int i = 0; i < weights.Length; i++)
                sum += int.Parse(ird[i].ToString()) * int.Parse(weights[i].ToString());

            var remainder = sum % 11;

            return remainder == 0 ? remainder : (11 - remainder);
        }

        public static string GetNumbers(this string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            var txt = new string(text.Where(p => char.IsDigit(p)).ToArray());
            return txt;
        }
    }
}
