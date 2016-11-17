using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharp_lab4
{
    class Program
    {
        private const double epsMax = 0.0000000001;
        private const double a = 1.0;
        private const double b = 23.0;

        static double integralFunction(double x)
        {
            var ln = Math.Log(x);
            return ln * ln / x;
        }

        static double secondDerivative(double x)
        {
            var ln = Math.Log(x);
            return ((2 * (ln * ln - 3 * ln + 1)) / (x * x * x));
        }

        static double getStep(double eps)
        {
            double max = 0;
            double cur;
            for (double i = a; i <= b; i += 0.01)
            {
                cur = Math.Abs(secondDerivative(i));
                if (cur > max)
                {
                    max = cur;
                }
            }
            return Math.Sqrt((12 * eps) / ((b - a) * max));
        }

        static double idefiniteIntegral(double x)
        {
            var ln = Math.Log(x);
            return ((ln * ln * ln) / 3);
        }

        static double exactValue(double a, double b)
        {
            return idefiniteIntegral(b) - idefiniteIntegral(a);
        }

        static double trapezoid(double a, double b, double h, Func<double, double> f)
        {
	        double psum = f(a) + f(b);
            int n = (int) ((b - a) / h);
	        for (int i = 1; i < n; ++i)
		        psum = psum + 2.0*f(a + i* h);

                psum = (h / 2.0)*psum;

	        return psum;
        }

        static void runge(double a, double b, double eps, Func<double, double> f, double abs_error)
        {

            double n = 1 / Math.Sqrt(eps);
                double R;
                double val_n, val_2n;
	        do
	        {
		        val_n = trapezoid(a, b, (b - a) / n, f);
		        val_2n = trapezoid(a, b, (b - a) /(2*n), f);
		        R = Math.Abs(val_n - val_2n) / 3;
		        n *= 2;
	        } while (R > eps);

	        double ex_value = exactValue(a, b);

            Console.WriteLine("{0,-16:e} {1,-12:e} {2,-16:e}", abs_error, (b - a) / n, Math.Abs(val_2n - ex_value));
        }


        static void Main(string[] args)
        {
            // Task 1
            List<double> errors = new List<double>();
            double value, ex_value, h, error;
            Console.WriteLine("Trapezoid method\nEps\t\tStep\t\tExact value\t     Error");
            for (double epsBuf = 0.1; epsBuf > epsMax; epsBuf *= 0.01)
            {
                h = getStep(epsBuf);
                value = trapezoid(a, b, h, integralFunction);
                ex_value = exactValue(a, b);
                error = Math.Abs(value - ex_value);
                Console.WriteLine("{0,-12} {1,-12:e} {2,-20} {3,-16:e}", epsBuf, h, ex_value, error);
                errors.Add(error);
            }

            //Task 2
            Console.WriteLine("\n\nRunge principle\nExpected error\t    Step\t    Prodused error");
            double eps = 0.1;
            for (int i = 0; i < errors.Count(); ++i)
            {
                runge(a, b, eps, integralFunction, errors[i]);
                eps *= 0.01;
            }
        }
    }
}
