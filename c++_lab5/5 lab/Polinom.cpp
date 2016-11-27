#include "Header.h"
#include <math.h> 
double f(double x)
{
	return x * exp(sin(x / 1.5)) - 20;
}

double g(double t)
{
	return f((B + A) / 2 + t * (B - A) / 2);
}

double phi(int n, double x)
{
	double Tn_next, Tn = x, Tn_prev = 1;

	if (n == 0)
		return 1;

	for (int i = 1; i < n; ++i)
	{
		Tn_next = x * Tn*(2 * i + 1) / (i + 1) - Tn_prev*(i) / (i + 1);
		Tn_prev = Tn;
		Tn = Tn_next;
	}

	return Tn;
}

double **getCoef_K(int n)
{	
	double **Arr = new double *[n];
	for (int i = 0; i < n; i++) {
		Arr[i] = new double[n + 1];
		for (int j = 0; j < n; j++){
			Arr[i][j] = integral(-1, 1, i, j);
		}
		Arr[i][n] = integral1(-1, 1, i);
	}
	return Arr;
}

double* findCoefs(int n)
{
	double *alpha = new double[n];
	Roots(getCoef_K(n), alpha, 0, 0, n); 
	return alpha;
}

double Polynomial(double x, double* alpha, int n)
{

	double t = (2 * x - (B + A)) / (B - A);
	double y = 0;
	for (int i = 0; i < n; ++i)
		y += alpha[i] * phi(i, t);
	return y;
}

void getPolynomialNumber(double eps, vector<Point> &points)
{
	int NumNodes = 150;
	int k = 150;
	double step = (1 / (double)NumNodes);
	double x, fi, *alpha, sum, tmp, last_squares_approximation;
	int polynomialPower = 2;
	Point pt;
	do {
		points.clear();
		alpha = findCoefs(polynomialPower);
		last_squares_approximation = 0;
		x = A;
		sum = 0;
		for (int i = 0; i <= k; ++i)
		{
			fi = Polynomial(x, alpha, polynomialPower);
			tmp = f(x) - fi;
			sum += tmp * tmp;
			pt.x = x;
			pt.y = fi;
			points.push_back(pt);
			x += step;
		}
		last_squares_approximation = sqrt(sum / (k + 1));
		++polynomialPower;

	} while (last_squares_approximation > eps);
	cout << endl << "Amount of polynomials: " << polynomialPower << endl;
}

void PointFunc(vector<Point> &points) {

	double step = 0.005;
	double x;
	Point pt;
	x = A;
	points.clear();
	while (x <= B)
	{
		pt.x = x;
		pt.y = f(x);
		points.push_back(pt);
		x += step;
	}
}