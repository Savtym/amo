using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharp_lab2
{
    class Program
    {
        //private const double x1 = -2.0455;
        private const double a1 = -2.1;
        private const double b1 = -2.0;
        private const double m1 = -8.41615;
        private const double M1 = -11.0888;

        //private const double x2 = -0.405;
        private const double a2 = -0.5;
        private const double b2 = -0.3;
        private const double m2 = 1.38734;
        private const double M2 = 1.87758;

        static double n1;
        static double n2;
        static double absError;

        static private double myFunc(double x)
        {
            double xCube = x * x * x;
            return x * xCube + 2 * xCube + Math.Sin(x) + 0.5;
        }

        static private double myFuncTangent(double x)
        {
            double xSquare = x * x;
            return 4 * x * xSquare + 6 * xSquare + Math.Cos(x);
        }

        static private double iteratorFunc(double x, double lamda)
        {
            return x - lamda * myFunc(x);
        }

        static public double getByIterationMethod(double a, double m, double M, double eps)
        {
            double x = a;
            double lamda = 1 / M;
            double q = 1 - (m / M);
            n1 = 0;
            absError = (q * Math.Abs(iteratorFunc(x, lamda) - x)) / (1 - q);
            while (absError > eps)
            {
                x = iteratorFunc(x, lamda);
                n1++;
                absError = (q * Math.Abs(iteratorFunc(x, lamda) - x)) / (1 - q);
            }
            return x;
        }

 

        static public double getByTangentMethod(double a, double b, double m, double eps)
        {
            double dx;
            if (myFunc(a) > 0)
            {
                dx = a;
            }
            else {
                dx = b;
            }
            n2 = 0;
            absError = Math.Abs(myFunc(dx));
            for (int i = 0; absError > eps; i++, n2++)
            {
                dx = dx - myFunc(dx) / myFuncTangent(dx);
                absError = Math.Abs(myFunc(dx)) / m;
            }
            return dx;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("\tIteration method:");
            Console.WriteLine("{0,16}{1,16}{2,26}", "eps", "x", "abs_error");
            for (double eps = 0.01; eps > 0.00000000000001; eps *= 0.001)
            {
                Console.WriteLine("{0,16:e}\t{1,-20}{2,16:e}", eps, getByIterationMethod(a1, m1, M1, eps), absError);
                Console.WriteLine("{0,16:e}\t{1,-20}{2,16:e}", eps, getByIterationMethod(a2, m2, M2, eps), absError);
            }
            Console.WriteLine("\n\tTangent method:");
            Console.WriteLine("{0,16}{1,16}{2,26}", "eps", "x", "abs_error");
            for (double eps = 0.01; eps > 0.00000000000001; eps *= 0.001)
            {
                Console.WriteLine("{0,16:e}\t{1,-20}{2,16:e}", eps, getByTangentMethod(a1, b1, m1, eps), absError);
                Console.WriteLine("{0,16:e}\t{1,-20}{2,16:e}", eps, getByTangentMethod(a2, b2, m1, eps), absError);
            }
            Console.WriteLine("\n\tSpeed tables:");
            Console.WriteLine("{0,16}{1,16}{2,16}", "eps", "I", "II");
            for (double eps = 0.01; eps > 0.00000000000001; eps *= 0.001)
            {
                getByIterationMethod(a1, m1, M1, eps);
                getByTangentMethod(a2, b2, eps);
                Console.WriteLine("{0,16:e} {1,16}{2,16}", eps, n1, n2);
            }
        }
    }
}
