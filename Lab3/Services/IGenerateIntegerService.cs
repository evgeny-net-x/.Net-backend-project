using System;
namespace Backend3.Services
{
    public interface IGenerateIntegerService
    {
        int GetNumber(int begin=-100, int end=100);
    }
}
