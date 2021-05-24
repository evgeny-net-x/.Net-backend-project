using System;
using Backend3.Models;
using Backend3.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend3.Controllers
{
    public class QuizController: Controller
    {
        private readonly IGenerateIntegerService generateIntegerService;
        private readonly ICalculatorService calculatorService;

        public QuizController(IGenerateIntegerService generateIntegerService, ICalculatorService calculatorService)
        {
            this.generateIntegerService = generateIntegerService;
            this.calculatorService = calculatorService;
        }

        private QuizExprModel GenerateExpr()
        {
            Char op = '+';
            int prob = generateIntegerService.GetNumber(1, 4);
            switch (prob)
            {
                case 1: op = '+'; break;
                case 2: op = '-'; break;
                case 3: op = '*'; break;
                case 4: op = '/'; break;
            }

            QuizExprModel expr = new QuizExprModel
            {
                A = generateIntegerService.GetNumber(),
                B = generateIntegerService.GetNumber(),
                Op = op
            };

            return expr;
        }

        public IActionResult Test()
        {
            var model = new QuizViewModel();
            model.Expr = GenerateExpr();

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Test(QuizAction action, QuizViewModel model)
        {
            if (!this.ModelState.IsValid)
                return this.View(model);

            if (action == QuizAction.Next)
            {
                this.ModelState.Remove("Expr.A");
                this.ModelState.Remove("Expr.B");
                this.ModelState.Remove("Expr.Op");
                this.ModelState.Remove("Expr.Result");

                model.Terms.Add(model.Expr.Clone());
                model.Expr = GenerateExpr();

                return this.View(model);
            } else if (action == QuizAction.Finish)
            {
                model.Terms.Add(model.Expr);

                var resultModel = new QuizResultViewModel();
                resultModel.Terms = model.Terms;

                for (var i = 0; i < resultModel.Terms.Count; i++)
                {
                    Boolean is_equal = resultModel.Terms[i].Result == resultModel.Terms[i].CalculateResult();
                    resultModel.IsCorrect.Add(is_equal);
                    if (is_equal)
                        resultModel.CorrectAnswersCount++;
                }

                return this.View("Result", resultModel);
            } else
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }
    }
}
