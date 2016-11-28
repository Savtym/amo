#include "Header.h"

int main()
{
	
	vector<Point> points_for_Polinom;
	getPolynomialNumber(0.01, points_for_Polinom);

	for (int i = 0; i < points_for_Polinom.size(); i ++){
		cout << points_for_Polinom[i].x << " ; " << points_for_Polinom[i].y << endl;
	}

	getchar();
	return 0;
}
