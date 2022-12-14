using NUnit.Framework;
using System;
using System.Linq;

namespace CalculatorUnitTests {
    class CalculatorUnitTests {
        [TestFixture]
        public class TestCalculator {
            [Test]
            public void TestStringToArray() {
                string numbers = "0,1,2,3,4,555";
                var arrayNumbers = numbers.Split(',');

                // LinQ
                var sum = arrayNumbers.Sum(x => Convert.ToInt32(x));

                // Foreach classique
                var sommeWithForeach = 0;
                foreach (var cell in arrayNumbers) {
                    sommeWithForeach += Convert.ToInt32(cell);
                }
                Assert.AreEqual(sommeWithForeach, 565);
                Assert.AreEqual(sum, 565);
            }


            [TestCase("")]
            [TestCase(null)]
            [Test]
            public void AddReturnZeroWhenSuppliedEmptyOrNullString(string numbers) {
                var result = Calculator.Add(numbers);
                Assert.That(result, Is.EqualTo(0));
            }

            [TestCase("0", 0)]
            [TestCase("1", 1)]
            [TestCase("2", 2)]
            [TestCase("3", 3)]
            [Test]
            public void AddReturnNumberWhenSuppliedAsSingleNumbers(string number, int expectedResult) {
                var result = Calculator.Add(number);
                Assert.That(result, Is.EqualTo(expectedResult));
            }

            [TestCase("0", 0)]
            [TestCase("1", 1)]
            [TestCase("2", 2)]
            [TestCase("3", 3)]
            [TestCase("0,1,2,3,4,555", 565)]
            [Test]
            public void AddReturnNumberWhenSuppliedSingleNumberInString(string number, int expectedResult) {
                var result = Calculator.Add(number);
                Assert.That(result, Is.EqualTo(expectedResult));
            }

            [TestCase("1,2,3", 6)]
            [TestCase("3\n2", 5)]
            [TestCase("1\n2,3", 6)]
            [TestCase("1\n2\n3,4,5", 15)]
            [Test]
            public void AddReturSumWhenSuppliedNumbersInStringWithNewLineAsDelimiter(string numbers, int expectedResult) {
                var result = Calculator.Add(numbers);
                Assert.That(result, Is.EqualTo(expectedResult));
            }

            [TestCase("0,1", 1)]
            [TestCase("0,1,1", 2)]
            [TestCase("0,2", 2)]
            [TestCase("0,2,2", 4)]
            [TestCase("0,3", 3)]
            [TestCase("0,3,2", 5)]
            [TestCase("0,3,3", 6)]
            [Test]
            public void AddReturnSumWhenSuppliedMultipleNumbersInString(string numbers, int expectedResult) {
                var result = Calculator.Add(numbers);
                Assert.That(result, Is.EqualTo(expectedResult));
            }

            [TestCase("0,3,1001", 3)]
            [TestCase("0,3,1000", 1003)]
            [Test]
            public void AddReturnSumByIgnoringMoreThanThousandWhenSuppliedMultipleNumbersInString(string numbers, int expectedResult) {
                var result = Calculator.Add(numbers);
                Assert.That(result, Is.EqualTo(expectedResult));
            }

            [TestCase("//*\n1*2", 3)]
            [TestCase("//;\n1;2", 3)]
            [TestCase("//;\n1;2;3;4;5;6;7;8;9;10", 55)]
            [Test]
            public void AddWhenGivenDefinedDelimiterUsesThatDelimiter(string input, int expectation) {
                var result = Calculator.Add(input);

                Assert.That(result, Is.EqualTo(expectation));
            }

            [TestCase("1,-1", -1)]
            [TestCase("1,-3", -3)]
            [Test]
            public void AddThrowArgumentExceptionWhenSuppliedStringDoesNotMeetRule(string numbers, int beyondRuleNumber) {
                var exception = Assert.Throws<ArgumentException>(() => Calculator.Add(numbers));

                Assert.That(exception.Message, Is.EqualTo(string.Format("string contains [{0}], which does not meet rule. Entered number should not be negative.", beyondRuleNumber)));
            }

        }
    }
}