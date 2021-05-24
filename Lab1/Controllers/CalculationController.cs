using Backend1.Services;
using Backend1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend1.Controllers
{
    public class CalculationController : Controller
    {
        private readonly int range = 10000;
        private readonly IGenerateIntegerService generateIntegerService;
        private readonly ICalculatorService calculatorService;

        public CalculationController(IGenerateIntegerService generateIntegerService, ICalculatorService calculatorService)
        {
            this.generateIntegerService = generateIntegerService;
            this.calculatorService = calculatorService;
        }

        public ActionResult PassUsingViewData()
        {
            int a = this.generateIntegerService.GetNumber(-range, range);
            int b = this.generateIntegerService.GetNumber(-range, range);
            if (b == 0)
                b = 1;

            this.ViewData["A"] = a;
            this.ViewData["B"] = b;
            this.ViewData["Add"] = a + b;
            this.ViewData["Sub"] = a - b;
            this.ViewData["Mul"] = a * b;
            this.ViewData["Div"] = a / b;

            return this.View();
        }

        public ActionResult PassUsingViewBag()
        {
            int a = this.generateIntegerService.GetNumber(-range, range);
            int b = this.generateIntegerService.GetNumber(-range, range);
            if (b == 0)
                b = 1;

            this.ViewBag.A = a;
            this.ViewBag.B = b;
            this.ViewBag.Add = a + b;
            this.ViewBag.Sub = a - b;
            this.ViewBag.Mul = a * b;
            this.ViewBag.Div = a / b;

            return this.View();
        }

        public ActionResult PassUsingModel()
        {
            int a = this.generateIntegerService.GetNumber(-range, range);
            int b = this.generateIntegerService.GetNumber(-range, range);
            if (b == 0)
                b = 1;

            var model = new CalculationViewModel
            {
                A = a,
                B = b,
                Add = a + b,
                Sub = a - b,
                Mul = a * b,
                Div = a / b
            };

            return this.View(model);
        }

        public ActionResult PassUsingServiceInjection()
        {
            return this.View();
        }
    }
}
