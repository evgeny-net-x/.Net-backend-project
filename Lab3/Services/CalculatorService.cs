using System;
namespace Backend3.Services
{
    public class CalculatorService: ICalculatorService
    {
        public int Add(int a, int b) => a + b;
        public int Sub(int a, int b) => a - b;
        public int Mul(int a, int b) => a * b;
        public int Div(int a, int b) => a / b;
    }
}
