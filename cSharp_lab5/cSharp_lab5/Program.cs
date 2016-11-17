using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharp_lab5
{
    class Program
    {
        const double A = 2.0;
        const double B = 8.0;
        const double MAX_SECOND_DERIVATIVE = 51.7132;

        private struct Point
        {
            public double x;
            public double y;
        }

        private static double func(double x)
        {
            return 0.5 * Math.Pow(Math.E, Math.Pow(x, 1 / 3)) * Math.Sin(3 * x);
        }

        private static double g(double t)
        {
            return func((B + A) / 2 + t * (B - A) / 2);
        }

        private static double phi(int n, double x)
        {
            double tnNext,
                tn = x,
                tnPrev = 1;
            if (n == 0)
            {
                tn = 1;
            }
            for (int i = 1; i < n; ++i)
            {
                tnNext = x * tn * (2 * i + 1) / (i + 1) - tnPrev * (i) / (i + 1);
                tnPrev = tn;
                tn = tnNext;
            }
            return tn;
        }

        private static double getCoefK(int k, int n)
        {
            double h = 2.0 / n;
            double x = -1;
            double topIntegral = 0;
            double bottomIntegral = 0;
            for (int i = 1; i < n; ++i)
            {
                x += h;
                topIntegral += g(x) * phi(k, x) * h;
                bottomIntegral += phi(k, x) * phi(k, x) * h;
            }
            topIntegral += h * (g(-1) * phi(k, -1) + g(1) * phi(k, 1)) / 2;
            bottomIntegral += h * (phi(k, -1) * phi(k, -1) + phi(k, 1) * phi(k, 1)) / 2;
            return topIntegral / bottomIntegral;
        }

        private static double getStep(double eps)
        {
            return Math.Sqrt(12 * eps / ((B - A) * MAX_SECOND_DERIVATIVE));
        }

        private static double[] findCoefs(int n)
        {
            double[] alpha = new double[n];
            double eps = 1e-5;
            int k = (int)(2.0 / getStep(eps));
            for (int i = 0; i < n; ++i)
            {
                alpha[i] = getCoefK(i, k);
            }
            return alpha;
        }

        private static double chebyshevPolynomial(double x, double[] alpha, int n)
        {
            double t = (2 * x - (B + A)) / (B - A);
            double y = 0;
            for (int i = 0; i < n; ++i)
                y += alpha[i] * phi(i, t);
            return y;
        }

        static void getPolynomialNumber(double eps)
        {
            int NumNodes = 1000;
            int k = (int)(B - A) * NumNodes;
            double step = (B - A) / ((B - A) * NumNodes);
            double x, fi, sum, tmp, lastSquaresApproximation;
            double[] alpha;
            int polynomialPower = 2;
            Point[] points;
            Array points1 = new Array[0];
            Point pt;
            do
            {
                points.clear();
                alpha = findCoefs(polynomialPower);
                lastSquaresApproximation = 0;
                x = A;
                sum = 0;
                for (int i = 0; i <= k; ++i)
                {
                    fi = chebyshevPolynomial(x, alpha, polynomialPower);
                    tmp = func(x) - fi;
                    sum += tmp * tmp;
                    pt.x = x;
                    pt.y = fi;
                    points.push_back(pt);
                    x += step;
                }
                lastSquaresApproximation = Math.Sqrt(sum / (k + 1));
                ++polynomialPower;
            } while (lastSquaresApproximation > eps);

            /*for (int i = 0; i < points.size(); i += 30)
                cout << points[i].x << " " << points[i].y << endl;*/

            Console.WriteLine("Amount of Chebyshev polynomials: {0}", polynomialPower);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Function: 0.5 * e ^ x ^ (1 / 3) * sin(3x) [2; 8]");
            getPolynomialNumber(0.01);
        }
    }
}
