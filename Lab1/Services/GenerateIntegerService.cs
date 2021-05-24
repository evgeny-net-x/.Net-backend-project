using System;

namespace Backend1.Services
{
    public class GenerateIntegerService: IGenerateIntegerService
    {
        public int GetNumber(int begin, int end)
        {
            Random rand = new Random();
            return rand.Next(begin, end);
        }
    }
}
