using System;
using Backend2.Models;
using Backend2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend2.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly ICalculatorService calculatorService;

        public CalculatorController(ICalculatorService calculatorService)
        {
            this.calculatorService = calculatorService;
        }

        public int ValidateFirstOperand(String a)
        {
            this.ViewBag.A = a;
            if (String.IsNullOrEmpty(a))
            {
                this.ViewBag.AError = "First operand is required";
                return 0;
            }

            try
            {
                return Convert.ToInt32(a);
            } catch
            {
                this.ViewBag.AError = "Operand have to be a number";
                return 0;
            }
        }

        public int ValidateSecondOperand(String b)
        {
            this.ViewBag.B = b;
            if (String.IsNullOrEmpty(b))
            {
                this.ViewBag.BError = "Second operand is required";
                return 0;
            }

            int bNum = 0;
            try
            {
                bNum = Convert.ToInt32(b);
            }
            catch
            {
                this.ViewBag.BError = "Operand have to be a number";
                return 0;
            }

            if (bNum == 0)
                this.ViewBag.BError = "Division by zero is forbidden";

            return bNum;
        }

        public int Calculate(Char op, int a, int b)
        {
            switch (op)
            {
                case '+': return calculatorService.Add(a, b);
                case '-': return calculatorService.Sub(a, b);
                case '*': return calculatorService.Mul(a, b);
                case '/': return calculatorService.Div(a, b);
                default: return 0;
            }
        }

        public ActionResult ManualInSingleAction()
        {
            if (this.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                this.ViewBag.op = this.Request.Form["op"][0][0];
                int a = this.ValidateFirstOperand(this.Request.Form["a"]);
                int b = this.ValidateSecondOperand(this.Request.Form["b"]);
                if (String.IsNullOrEmpty(this.ViewBag.AError) && String.IsNullOrEmpty(this.ViewBag.BError))
                    this.ViewBag.Result = this.Calculate(this.ViewBag.op, a, b);

            }

            return this.View();
        }


        public ActionResult ManualInSeparateActions()
        {
            return this.View();
        }

        [HttpPost, ActionName("ManualInSeparateActions")]
        public ActionResult ManualInSeparateActionsPost()
        {
            this.ViewBag.op = this.Request.Form["op"][0][0];
            int a = this.ValidateFirstOperand(this.Request.Form["a"]);
            int b = this.ValidateSecondOperand(this.Request.Form["b"]);
            if (String.IsNullOrEmpty(this.ViewBag.AError) && String.IsNullOrEmpty(this.ViewBag.BError))
                this.ViewBag.Result = this.Calculate(this.ViewBag.op, a, b);

            return this.View();
        }


        public ActionResult ModelBindingInParameters()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult ModelBindingInParameters(String a, String b, char op)
        {
            this.ViewBag.op = op;
            int aNum = this.ValidateFirstOperand(a);
            int bNum = this.ValidateSecondOperand(b);
            if (String.IsNullOrEmpty(this.ViewBag.AError) && String.IsNullOrEmpty(this.ViewBag.BError))
                this.ViewBag.Result = this.Calculate(op, aNum, bNum);

            return this.View();
        }


        public ActionResult ModelBindingInSeparateModel()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult ModelBindingInSeparateModel(CalculatorResultModel model)
        {
            this.ViewBag.op = model.op;
            this.ViewBag.A = model.a;
            this.ViewBag.B = model.b;

            if (this.ModelState.IsValid)
            {
                int a = Convert.ToInt32(model.a); //this.ValidateFirstOperand(model.a);
                int b = Convert.ToInt32(model.b); //this.ValidateSecondOperand(model.b);
                this.ViewBag.Result = this.Calculate(model.op, a, b);
            }

            return this.View();
        }
    }
}
