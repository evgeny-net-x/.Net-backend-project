using System;
namespace Backend3.Services
{
    public class GenerateIntegerService: IGenerateIntegerService
    {
        public int GetNumber(int begin, int end) => new Random().Next(begin, end);
    }
}
