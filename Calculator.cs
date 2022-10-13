using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace CalculatorUnitTests {
    internal class Calculator {
        private const string POSITIVE_AND_NEGATIVE_NUMBERS_REGEX = @"[^0-9-]+";

        internal static object Add(string numbers) {
            int sum = 0;
            if (string.IsNullOrEmpty(numbers)) return 0;
            string[] numbersList = Regex.Split(numbers, POSITIVE_AND_NEGATIVE_NUMBERS_REGEX);
            foreach (string numberString in numbersList) {
                AddNumberToSumIfPossible(ref sum, numberString);
            }
            return sum;
        }

        private static void AddNumberToSumIfPossible(ref int sum, string numberString) {
            int currentResult;
            if (CheckIfNumberIsValid(out currentResult, numberString)) {
                if (currentResult < 0) {
                    throw new ArgumentException(GetIsNegativeExceptionMessage(currentResult));
                }
                sum += currentResult;
            }
        }

        private static string GetIsNegativeExceptionMessage(int currentResult) {
            return ($"string contains [{currentResult}], which does not meet rule. Entered number should not be negative.");
        }

        private static bool CheckIfNumberIsValid(out int currentResult, string numberString) {
            return Int32.TryParse(numberString, out currentResult) && currentResult <= 1000;
        }
    }
}