using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingLib.Math;

namespace UnitTesting.kerra
{
    public class PrimeTest
    {
        private readonly BasicCalc _calculator;

        public PrimeTest()
        {
            _calculator = new BasicCalc();
        }

        [Fact]
        public void IsPrime_LimitValueShouldReturnTrue() //граничное значение
        {
            var result = _calculator.IsPrime(2);
            Assert.Equal(true, result);
        }

        [Fact]
        public void IsPrime_LimitValueShouldReturnFalse() //граничное значение
        {
            var result = _calculator.IsPrime(1);
            Assert.Equal(false, result);
        }

        [Fact]
        public void IsPrime_EquivalentValueShouldReturnTrue() //эквивалентное значение
        {
            var result = _calculator.IsPrime(2);
            Assert.Equal(true, result);
        }

        [Fact]
        public void IsPrime_EquivalentValueShouldReturnFalse() //эквивалентное значение
        {
            var result = _calculator.IsPrime(6);
            Assert.Equal(false, result);
        }

        [Fact]
        public void IsPrime_GenerateManyValues()
        {

        }
    }

    public class Numbers
    {
        public int[] TestNumbers(int length)
        {
            Random random = new Random();
            int[] numbers = new int[length];

            for (int i = 0; i < numbers.Length; i++)
                numbers[i] = random.Next();

            return numbers;
        }
    }
}
