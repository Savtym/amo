#include "Header.h"


void DrawGraph(HDC hDC, vector<Point> points, int color)
{
	for (int i = 0; i < points.size(); i++)
	{
		SetPixel(hDC, 20 * (points[i].x) + 300, 10 * (-points[i].y) + 200, color);
	}
}