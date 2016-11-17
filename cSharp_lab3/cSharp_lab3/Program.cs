using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharp_lab3
{
    class Program
    {
        public double[,] matrix = new double[,] {
            {  137, 7, 18, 12, 139 },
            { 3, 241, 20, 14, 222 },
            {  5, 7, 57, 11, 111 },
            {  1, 18, 6, 276, 83 }
        };

        static public double[] gaussJordanMethod(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            double[] result = new double[n];
            for (int k = 0; k < n; k++)
            {
                double coef = matrix[k, k];
                matrix[k, k] = 1;
                for (int j = k + 1; j < n; j++)
                {
                    matrix[k, j] /= coef;
                }
                matrix[k, n] = matrix[k, n] / coef;
                for (int i = 0; i < n; i++)
                {
                    if (i != k)
                    {
                        double s = matrix[i, k];
                        matrix[i, k] = 0;
                        for (int j = k + 1; j < matrix.GetLength(1) - 1; j++)
                        {
                            matrix[i, j] -= s * matrix[k, j];
                        }
                        matrix[i, n] -= s * matrix[k, n];
                    }
                }
                result[k] = matrix[k, n];
            }
            for (int i = 0; i < n; i++)
            {
                result[i] = matrix[i, n];
            }
            return result;
        }
        static private bool converge(double[] xk, double[] xkp, int n, double eps)
        {
            bool result = false;
            double norm = 0;
            for (int i = 0; i < n; i++)
            {
                norm += (xk[i] - xkp[i]) * (xk[i] - xkp[i]);
            }
            if (Math.Sqrt(norm) >= eps)
            {
                result = true;
            }
            return result;
        }

        static public double[] gaussSeidelMethod(double[,] matrix, double eps)
        {
            int n = matrix.GetLength(0);
            double[] curVector = new double[n];
            double[] x = new double[n];
            double[] p = new double[n];
            do
            {
                for (int i = 0; i < n; i++)
                {
                    p[i] = x[i];
                    x[i] = matrix[i, n] / matrix[i, i];
                    for (int j = 0; j < n; j++)
                    {
                        if (j == i)
                        {
                            continue;
                        }
                        x[i] -= (matrix[i, j] * x[j]) / matrix[i, i];
                    }
                }
            } while (converge(x, p, n, eps));

            return x;
        }

        static public void printMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        static public void printAnswer(double[] vector)
        {
            for (int i = 0; i < vector.GetLength(0); i++)
            {
                Console.Write(vector[i] + "\t");
            }
        }

        static void Main(string[] args)
        {
            Program obj = new Program();
            Program obj2 = new Program();
            Console.WriteLine("\tMatrix:");
            printMatrix(obj.matrix);
            Console.WriteLine();
            Console.Write("Answer gaussJordan:\t");
            printAnswer(gaussJordanMethod(obj.matrix));
            Console.Write("Answer Gauss-Seidel:\t");
            printAnswer(gaussSeidelMethod(obj2.matrix, 0.01));
            Console.WriteLine();
        }
    }
}
