using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARK_Project.Calculator
{
    public class Calculator
    {
        public double Calculate(double valueLeft, string currencyLeft, string currencyRight)
        {
            throw new NotImplementedException();
        }
    }

    public interface ICalculator
    {
        double Calculate(double valueLeft, string currencyLeft, string currencyRight);
    }
}
