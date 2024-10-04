using TestingLib.Math;

namespace UnitTesting.kerra
{
    public class UnitTest
    {
        private readonly BasicCalc _calculator;

        public UnitTest()
        {
            _calculator = new BasicCalc();
        }

        [Fact]
        public void FindRootPositiveNumber_ShouldReturnCorrectRoot()
        {
            double result = _calculator.Sqrt(25.0);
            Assert.Equal(5, result);
        }

        [Fact]
        public void FindRootNegativeNumber_ArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Sqrt(-25.0));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(9, 3)]
        [InlineData(3, 1.7)]
        public void Sqrt_Theory(double a, double expectedResult)
        {
            double result = _calculator.Sqrt(a);
            Assert.Equal(expectedResult, result, 0.05);
        }

        [Theory]
        [InlineData(1, -3, 2, 2.0, 1.0)]
        [InlineData(1.0, -2.0, 1.0, 1.0, 1.0)]
        [InlineData(1.0, 0, 1.0, null, null)]
        public void SolveQuadraticEquation_ShouldReturnCorrectRoots(double a, double b, double c, double? expectedFirstRoot, double? expectedSecondRoot)
        {
            var (firstRoot, secondRoot) = _calculator.SolveQuadraticEquation(a, b, c);
            Assert.Equal(expectedFirstRoot, firstRoot);
            Assert.Equal(expectedSecondRoot, secondRoot);
        }

        [Fact]
        public void SolveQuadraticEquation_ArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.SolveQuadraticEquation(0, 2, 4));
        }
    }
}
