#define __DEBUG_PRINT__
#ifndef DllExport
#define DllExport __declspec( dllexport )
#endif
#define EAST_NORM_COLS 320//512
#define MAX_COUNT_OBJECT_BOX 4096 // so luong vung text/object toi da
typedef unsigned char byte;// byte C#
typedef unsigned short ushort;// ushort C#
extern "C" DllExport short TestEast();
extern "C" DllExport short Load_Model(const char *szPrototxt1, const char *szPrototxt2, const char *szPrototxt3, const char *szCaffemodel);
extern "C" DllExport ushort FaceKit(ushort rows, ushort cols, int widthStep, byte *rgbData,
	 ushort *x0, ushort *y0, ushort *x1, ushort *y1, ushort *x2, ushort *y2, ushort *x3, ushort *y3);
							  // ham xuat cho C# call
//extern "C" DllExport short Load_PB_Model(const char *szModelFileName);
//extern "C" DllExport ushort RGBForLocatingObject(ushort rows, ushort cols, int widthStep, byte *rgbData,
//	ushort *countObjectBox, ushort *aVertices_x, ushort *aVertices_y); #pragma once
