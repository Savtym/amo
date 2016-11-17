using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharp_lab1
{
    class Program
    {
        static private int n;
        static private int xCeiling;
        static private double xFractional;
        static private double R;
        static private int n_forTask2;
        static double a = 1.4;
        static double b = 11.7;
        private const double exp = Math.E;

        static private double getExponentFromSeries(double x, double eps)
        {
            int k = 1;
            double U = 1;
            double result = U;
            double resultCeiling = Math.Pow(Math.E, xCeiling);
            eps /= resultCeiling;
            while (true)
            {
                U *= (xFractional / k);
                result += U;
                if (Math.Abs(U) < eps)
                {
                    R = U;
                    n = k + 1;
                    break;
                }
                k++;
            }
            return Math.Abs(Math.Exp(x) - result * resultCeiling);
        }

        static public void Task_1(double x)
        {
            xCeiling = (int) Math.Floor(x);
            xFractional = x - (int) x;
            Console.WriteLine("{0,-8}{1,5}\t{2,-25}{3,16}", "eps", "n", "abs_error", "last (R)");
            for (double eps = 0.01; eps > 0.00000000000001; eps *= 0.001)
            {
                double absError = getExponentFromSeries(x, eps);
                Console.WriteLine("{0,-8}{1,5}\t{2,-25}{3,16}", eps, n, absError, R);
                if (eps == Math.Pow(10, -8))
                {
                    n_forTask2 = n;
                }
            }
        }

        static public void Task_2(double a, double h)
        {
            int n = n_forTask2;
            Console.WriteLine("{0,-10}{1,21}{2,16}", "x_i", "abs_error", "last (R)");
            for (int i = 0; i <= 10; i++)
            {
                double x = a + h * i;
                double resultCeiling = Math.Pow(Math.E, (int)x);
                xFractional = x - (int)x;
                double result = 1;
                double U = 1;
                int k = 1;

                for (int j = 0; j < n; ++j)
                {
                    U *= (xFractional / k);
                    result += U;
                    ++k;
                }
                Console.WriteLine("{0,-10}{1,21:e}{2,16:e}", x, Math.Abs(Math.Exp(x) - result * resultCeiling), U);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("TASK 1");
            Task_1((a + b) / 2);
            Console.WriteLine("\nTASK 2");
            Task_2(a, (b - a) / 10);
        }
    }
}
