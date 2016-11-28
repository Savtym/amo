#pragma once
#include <math.h>
#include <iostream>
#include <vector>
#include <windows.h>

#define A 0
#define B 16
#define MAX_SECOND_DERIVATIVE 51.7132

using namespace std;


struct Point
{
	double x;
	double y;
};


double f(double x);
double phi(int n, double x);
double  g(double t);


void getPolynomialNumber(double eps, vector<Point> &points);
void PointFunc(vector<Point> &points);
double integral(double a, double b, int ind1, int ind2);
double integral1(double a, double b, int ind1);
void Roots(double **Arr, double *x, int _row, int _col, const int size); 