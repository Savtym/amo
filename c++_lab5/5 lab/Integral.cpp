#include "Header.h"
#include <iostream>
#include <stdio.h>  
#include <math.h>

double integral(double a, double b,int ind1, int ind2)
{
	int n = 4, i;
	double s_ab = phi(ind1, a)*phi(ind2, a) + phi(ind1, b)*phi(ind2, b);
	double h;
	double s_1, s_2;
	double res = 0;
	double prev_res;

	do
	{
		h = (b - a) / n;
		s_1 = 0;
		s_2 = 0;
		prev_res = res;
		res = 0;
		for (i = 1; i < n; i += 2)
		{
			s_1 += phi(ind1, (a + i * h))*phi(ind2, (a + i * h));
		}
		for (i = 2; i < n; i += 2)
		{
				s_2 += phi(ind1, (a + i * h))*phi(ind2, (a + i * h));
		}
		res = (h / 3) * (s_ab + 2 * s_2 + 4 * s_1);
		n *= 2;
	} while (abs(prev_res - res) > 0.00001);
	return (res);
}

double integral1(double a, double b, int ind1)
{
	int n = 4, i;
	double s_ab = phi(ind1, (a))*g((a)) + phi(ind1, (b))*g((b));
	double h;
	double s_1, s_2;
	double res = 0;
	double prev_res;

	do
	{
		h = (b - a) / n;
		s_1 = 0;
		s_2 = 0;
		prev_res = res;
		res = 0;
		for (i = 1; i < n; i += 2)
		{
			s_1 += phi(ind1, (a + i * h))*g((a + i * h));
		}
		for (i = 2; i < n; i += 2)
		{
			s_2 += phi(ind1, (a + i * h))*g((a + i * h));
		}
		res = (h / 3) * (s_ab + 2 * s_2 + 4 * s_1);
		n *= 2;
	} while (abs(prev_res - res) > 0.00001);
	return (res);
}



void Roots(double **Arr, double *x, int _row, int _col, const int size) 
{
	for (int i = 0; i < size; i++) 
	{
		x[i] = Arr[i][size] / Arr[i][i];
	}
}