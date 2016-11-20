#include "Header.h"



int main()
{
	HWND hwnd;
	char Title[1024];
	GetConsoleTitle((LPWSTR)Title, 1024);
	hwnd = FindWindow(NULL, (LPWSTR)Title); 
	HDC hdc = GetDC(hwnd);
	HDC hdc1 = GetDC(hwnd); 

	vector<Point> points_for_func;
	PointFunc(points_for_func);

	vector<Point> points_for_Polinom;
	getPolynomialNumber(0.01, points_for_Polinom);
	
	DrawGraph(hdc, points_for_Polinom, RGB(255, 255, 255));
	DrawGraph(hdc1, points_for_func, RGB(255, 255, 255));

	ReleaseDC(hwnd, hdc);
	ReleaseDC(hwnd, hdc1);

	getchar();
	return 0;
}
