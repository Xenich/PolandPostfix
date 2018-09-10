using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PolandPostfix
{

    // 6 2 - 7 * 3 5 / + 6 / - (эквивалентное выражение в инфиксной нотации: ((6-2)*7+3/5)/6
    class PolandPostfix
    {
        private static string[] splt = new string[] { " ", "+", "-", "*", "/", "^", "sin" , "sqrt",")","("};
        private static string[] operators = new string[] { "+", "-", "*", "/", "^", "sin", "sqrt" };
                // словарь функция-её арность
        private static Dictionary<string, int> qwant = new Dictionary<string, int>()
        {
            {"+",2}, {"-",2}, {"*",2}, {"/",2},{"^",2}, {"sin",1}, {"sqrt", 1}
        };
        private static Stack<double> stack = new Stack<double>();       // стек для чисел - операндов

        public static double Calculate(string str)
        {
            string[] s = Razbor(str);
            for (int i = 0; i < s.Length; i++)
            {
                bool foundedInOps=false;    // указывает наткнулись ли мы на оператор
                foreach (string op in operators)
                {
                    if (s[i] == op)
                    {
                        foundedInOps = true;
                        break;
                    }
                }
                if (foundedInOps)           // 1) Если на вход подан знак операции (наткнулись на оператор), то соответствующая операция выполняется над требуемым количеством значений (кол-во берётся из словаря qwant), извлечённых из стека, взятых в порядке добавления. Результат выполненной операции кладётся на вершину стека
                {
                    double[] operands = new double[qwant[s[i]]];
                    for (int j = 0; j < qwant[s[i]]; j++)
                    {
                        try
                        {
                            operands[j] = stack.Pop();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    stack.Push(Calculate(s[i], operands));      // Результат выполненной операции кладётся на вершину стека

                }
                else
                    stack.Push(Convert.ToDouble(s[i]));     // 2) Если на вход подан операнд, он помещается на вершину стека
                                                            // 3) Если входной набор символов обработан не полностью, перейти к шагу 1.
            }

            return stack.Pop();                             // После полной обработки входного набора символов результат вычисления выражения лежит на вершине стека.
        }

        /// <summary>
        /// Функция, выполняющая разбор строки с обратной польской записью (постфиксной нотации) на составляющие - операнды и операторы (строку преобразуем в массив). 
        /// </summary>
        /// <param name="str">выражение-строка, записанное в постфиксной нотации</param>
        /// <returns>массив операндов и операторов следующих в очерёдности постфиксной нотации</returns>
        private static string[] Razbor(string str)       // разбор обратной польской записи (постфиксной нотации) на составляющие - операнды и операторы
        {           
            Queue<string> queue = new Queue<string>();
            int start = 0;
            while (start < str.Length)
            {
                int finded=str.Length;          // индекс первого элемента, входящего в разбираемую строку, из массива splt
                string findedSplitSrtring="";   // сам элемент
                foreach (string s in splt)      // проверяем каждый элемент из массива
                {
                    int f=str.IndexOf(s, start);
                    if (f!=-1 && f < finded )
                    {
                        finded = f;
                        findedSplitSrtring=s;
                    }
                }
                if(finded!=start)
                    queue.Enqueue(str.Substring(start, finded - start));
                if (findedSplitSrtring != " " && findedSplitSrtring != "")
                    queue.Enqueue(findedSplitSrtring);
                start = finded + findedSplitSrtring.Length;         // продолжаем разбор с позиции, следующей за найденным элементом
            }
            return queue.ToArray();
        }

        private static double Calculate(string _operator, double[] operands )
        {
            double result=0;
            switch (_operator)
            {
                case "+":
                    result = operands[1] + operands[0];
                    break;
                case "-":
                    result = operands[1] - operands[0];
                    break;
                case "*":
                    result = operands[1] * operands[0];
                    break;
                case "/":
                    result = operands[1] / operands[0];
                    break;
                case "sin":
                    result = Math.Sin(operands[0]);
                    break;
                case "^":
                    result = Math.Pow(operands[1], operands[0]);
                    break;
                case "sqrt":
                    result = Math.Pow(operands[0], 0.5);
                    break;
            }
            return result;
        }
    }
}
