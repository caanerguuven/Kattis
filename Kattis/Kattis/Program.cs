using System;
using System.Collections.Generic;
using System.Linq;

namespace Kattis
{
    class Program
    {
        public static readonly char[] operationList = new char[] { '*', '-', '+' };
        static void Main(string[] args)
        {
            List<string> formulaList = new List<string>();
            formulaList.Add("+ 3 4");
            formulaList.Add("+ 3 4 - 1 2 * 0 c");
            formulaList.Add("* - 6 + x - 6 - - 9 6 * 0 c");
            formulaList.Add("+ 3 4 6 5 - 5 2 1 * 1 2 5");

            formulaList.ForEach(formula => {
                RunFormula(formula);
            });
            
            Console.ReadLine();

        }

        private static void RunFormula(string formula) {
            var formulasToBeProcessed = formula.Split(operationList).Where(x => !string.IsNullOrWhiteSpace(x.Trim()) && x.Trim().Length > 1).Select(s => s);

            foreach (var selectedProcess in formulasToBeProcessed)
            {
                var _currentFormula = formula.Split(new string[] { selectedProcess }, StringSplitOptions.None).ToList();

                var currentFormulaLeft = _currentFormula.First().Trim();
                var currentFormulaRight = _currentFormula.Last().Trim();

                char lastChar = currentFormulaLeft[currentFormulaLeft.Length - 1];
                var formulaValues = selectedProcess.ToCharArray().Where(x => !char.IsWhiteSpace(x)).ToList();
                bool exceptionNumber = formulaValues.Any(s => !char.IsNumber(s));
                if (exceptionNumber)
                {
                    continue;
                }

                var intResult = doMathProcess(lastChar, formulaValues);

                currentFormulaLeft = currentFormulaLeft.Remove(currentFormulaLeft.Length - 1);

                formula = currentFormulaLeft + intResult.ToString()+ " " + currentFormulaRight;
            }
            Console.WriteLine(formula);
        }

        public static int doMathProcess(char mathProcess, List<char> formulaValues) {
            var intResult = 0;
            if (mathProcess == '+')
            {
                foreach (var charItem in formulaValues)
                {
                    intResult += Convert.ToInt32(charItem.ToString());
                }
            }
            else if (mathProcess == '-')
            {
                for (int i = 0; i < formulaValues.Count(); i++)
                {
                    var value = Convert.ToInt32(formulaValues[i].ToString());
                    if (i == 0)
                    {
                        intResult += value;
                    }
                    else
                    {
                        intResult -= value;
                    }
                }
            }
            else if (mathProcess == '*')
            {
                intResult = 1;
                foreach (var charItem in formulaValues)
                {
                    intResult *= Convert.ToInt32(charItem.ToString());
                }
            }

            return intResult;
        }

    }
}
