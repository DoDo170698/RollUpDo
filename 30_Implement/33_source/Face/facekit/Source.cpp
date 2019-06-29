#include "param.h"
#include <opencv2/opencv.hpp>
#include <iostream>
#include <opencv2/core/types_c.h>
#define M_PI 3.14
using namespace std;
using namespace cv;
using namespace cv::dnn;

#define EPS  1e-5

DllExport short Test();
struct FaceBox
{
	int x, y, w, h;
	float angle, scale, score;

	Rect getBox() { return Rect(x, y, w, h); }

	FaceBox(int x_, int y_, int w_, int h_, float a_, float s_, float c_)
		: x(x_), y(y_), w(w_), h(h_), angle(a_), scale(s_), score(c_)
	{}
};


bool xyValid(int _x, int _y, cv::Mat _img)
{
	if (_x >= 0 && _x < _img.cols && _y >= 0 && _y < _img.rows)
		return true;
	else
		return false;
}


cv::Mat preprocessImg(cv::Mat _img)
{
	cv::Mat mean(_img.size(), CV_32FC3, cv::Scalar(104, 117, 123));
	cv::Mat imgF;
	_img.convertTo(imgF, CV_32FC3);
	return imgF - mean;
}

cv::Mat preprocessImg(cv::Mat _img, int _dim)
{
	cv::Mat imgNew;
	cv::resize(_img, imgNew, cv::Size(_dim, _dim));
	cv::Mat mean(imgNew.size(), CV_32FC3, cv::Scalar(104, 117, 123));
	cv::Mat imgF;
	imgNew.convertTo(imgF, CV_32FC3);
	return imgF - mean;
}


cv::Mat resizeImg(cv::Mat _img, float _scale)
{
	cv::Mat aux;
	cv::resize(_img, aux, cv::Size(int(_img.cols / _scale), int(_img.rows / _scale)));
	return aux;
}

cv::Mat padImg(cv::Mat _img)
{
	int row = std::min(int(_img.rows * 0.2), 100);
	int col = std::min(int(_img.cols * 0.2), 100);
	cv::Mat aux;
	cv::copyMakeBorder(_img, aux, row, row, col, col, cv::BORDER_CONSTANT);
	return aux;
}


bool compareFaceByScore(const FaceBox &box1, const FaceBox &box2)
{
	return box1.score > box2.score;
}


float IoU(FaceBox &box1, FaceBox &box2)
{
	int xOverlap = std::max(0, std::min(box1.x + box1.w - 1, box2.x + box2.w - 1) - std::max(box1.x, box2.x) + 1);
	int yOverlap = std::max(0, std::min(box1.y + box1.h - 1, box2.y + box2.h - 1) - std::max(box1.y, box2.y) + 1);
	int intersection = xOverlap * yOverlap;
	int unio = box1.w * box1.h + box2.w * box2.h - intersection;

	return float(intersection) / unio;
}


std::vector<FaceBox> NMS(std::vector<FaceBox> &_faces, bool _local, float _threshold)
{
	if (_faces.size() == 0)
		return _faces;

	std::sort(_faces.begin(), _faces.end(), compareFaceByScore);
	vector<int> flag(_faces.size(), 0);
	for (int i = 0; i < _faces.size(); i++)
	{
		if (flag[i])
			continue;
		for (int j = i + 1; j < _faces.size(); j++)
		{
			if (_local && abs(_faces[i].scale - _faces[j].scale) > EPS)
				continue;

			if (IoU(_faces[i], _faces[j]) > _threshold)
				flag[j] = 1;
		}
	}

	std::vector<FaceBox> faces_nms;
	for (int i = 0; i < _faces.size(); i++)
	{
		if (!flag[i])
			faces_nms.push_back(_faces[i]);
	}
	return faces_nms;
}


std::vector<FaceBox> TransformBoxes(cv::Mat _img, cv::Mat _imgPad, std::vector<FaceBox> &_faces)
{
	int row = (_imgPad.rows - _img.rows) / 2;
	int col = (_imgPad.cols - _img.cols) / 2;

	std::vector<FaceBox> newFaceBoxes;
	for (int i = 0; i < _faces.size(); i++)
	{
		if (_faces[i].w > 0 && _faces[i].h > 0)
			newFaceBoxes.push_back(FaceBox(_faces[i].x - col, _faces[i].y - row, _faces[i].w, _faces[i].w, _faces[i].angle, _faces[i].scale, _faces[i].score));
	}
	return newFaceBoxes;
}



std::vector<FaceBox> PCN_1(cv::Mat _img, cv::Mat _paddedImg, cv::dnn::Net _net, float _thresh, int _minFaceSize)
{
	std::vector<FaceBox> faceBoxes_1;
	int row = (_paddedImg.rows - _img.rows) / 2;
	int col = (_paddedImg.cols - _img.cols) / 2;
	int netSize = 24;
	int minFace = _minFaceSize * 1.4; // - size 20 + 40%
	float currentScale = minFace / float(netSize);
	int stride = 8;

	cv::Mat resizedImg = resizeImg(_img, currentScale);

	while (std::min(resizedImg.rows, resizedImg.cols) >= netSize)
	{
		// - Set input for net
		Mat inputMat = preprocessImg(resizedImg);
		Mat inputBlob = blobFromImage(inputMat, 1.0, Size(), Scalar(), false, false);
		_net.setInput(inputBlob);

		std::vector<String> outputBlobNames = { "cls_prob", "rotate_cls_prob", "bbox_reg_1" };
		std::vector<cv::Mat> outputBlobs;

		_net.forward(outputBlobs, outputBlobNames);
		cv::Mat scoresData = outputBlobs[0];
		cv::Mat rotateProbsData = outputBlobs[1];
		cv::Mat regressionData = outputBlobs[2];

		// scoresData.ptr<float>(0, 1)  ---->  image 0, channel 1
		Mat scoresMat(scoresData.size[2], scoresData.size[3], CV_32F, scoresData.ptr<float>(0, 1));
		Mat reg_1_Mat(regressionData.size[2], regressionData.size[3], CV_32F, regressionData.ptr<float>(0, 0));
		Mat reg_2_Mat(regressionData.size[2], regressionData.size[3], CV_32F, regressionData.ptr<float>(0, 1));
		Mat reg_3_Mat(regressionData.size[2], regressionData.size[3], CV_32F, regressionData.ptr<float>(0, 2));
		Mat rotateProbsMat(rotateProbsData.size[2], rotateProbsData.size[3], CV_32F, rotateProbsData.ptr<float>(0, 1));

		float w = netSize * currentScale;

		for (int i = 0; i < scoresData.size[2]; i++)
		{
			for (int j = 0; j < scoresData.size[3]; j++)
			{
				if (scoresMat.at<float>(i, j) < _thresh)
				{
					continue;
				}

				float score = scoresMat.at<float>(i, j);
				float sn = reg_1_Mat.at<float>(i, j);
				float xn = reg_2_Mat.at<float>(i, j);
				float yn = reg_3_Mat.at<float>(i, j);

				int x = int(j * currentScale * stride - 0.5 * sn * w + sn * xn * w + 0.5 * w) + col;
				int y = int(i * currentScale * stride - 0.5 * sn * w + sn * yn * w + 0.5 * w) + row;
				int wh = int(w * sn);

				if (xyValid(x, y, _paddedImg) && xyValid(x + wh - 1, y + wh - 1, _paddedImg))
				{
					if (rotateProbsMat.at<float>(i, j) > 0.5)
					{
						faceBoxes_1.push_back(FaceBox(x, y, wh, wh, 0, currentScale, score));
						//rectangle(_paddedImg, Rect(x, y, wh, wh), Scalar(0, 255, 0), 2);
					}
					else
					{
						faceBoxes_1.push_back(FaceBox(x, y, wh, wh, 180, currentScale, score));
						//rectangle(_paddedImg, Rect(x, y, wh, wh), Scalar(255, 0, 0), 2);
					}
				}


			}
		}

		resizedImg = resizeImg(resizedImg, 1.414);
		currentScale = float(_img.rows) / resizedImg.rows;
	}

	return faceBoxes_1;
}


std::vector<FaceBox> PCN_2(cv::Mat _img, cv::Mat _img180, cv::dnn::Net _net, float _threshold, int _dim, std::vector<FaceBox> _faces)
{
	//_dim = 24 ---> network size
	if (_faces.size() == 0)
		return _faces;

	std::vector<cv::Mat> dataList;
	int height = _img.rows;
	for (int i = 0; i < _faces.size(); i++)
	{
		if (abs(_faces[i].angle) < EPS)
			dataList.push_back(preprocessImg(_img(_faces[i].getBox()), _dim));
		else
		{
			int y2 = _faces[i].y + _faces[i].h - 1;
			dataList.push_back(preprocessImg(_img180(cv::Rect(_faces[i].x, height - 1 - y2, _faces[i].w, _faces[i].h)), _dim));
		}
	}

	std::vector<FaceBox> faceBoxes_2;

	// - Process the dataList vector
	for (int dataNr = 0; dataNr<dataList.size(); dataNr++)
	{
		Mat inputBlob = blobFromImage(dataList[dataNr], 1.0, Size(), Scalar(), false, false);
		_net.setInput(inputBlob);
		std::vector<String> outputBlobNames = { "cls_prob", "rotate_cls_prob", "bbox_reg_2" };
		std::vector<cv::Mat> outputBlobs;

		_net.forward(outputBlobs, outputBlobNames);
		cv::Mat scoresData = outputBlobs[0];
		cv::Mat rotateProbsData = outputBlobs[1];
		cv::Mat regressionData = outputBlobs[2];

		Mat scoresMat(scoresData.size[1], scoresData.size[0], CV_32F, scoresData.ptr<float>(0, 0)); // - using channel 0
		Mat regMat(regressionData.size[1], regressionData.size[0], CV_32F, regressionData.ptr<float>(0, 0));
		Mat rotateProbsMat(rotateProbsData.size[1], rotateProbsData.size[0], CV_32F, rotateProbsData.ptr<float>(0, 0));

		float score = scoresMat.at<float>(1, 0);
		if (score < _threshold)
			continue;

		float sn = regMat.at<float>(0, 0);// reg->data_at(i, 0, 0, 0);
		float xn = regMat.at<float>(1, 0);// reg->data_at(i, 1, 0, 0);
		float yn = regMat.at<float>(2, 0);//reg->data_at(i, 2, 0, 0);


		int cropX = _faces[dataNr].x;
		int cropY = _faces[dataNr].y;
		int cropW = _faces[dataNr].w;
		if (abs(_faces[dataNr].angle)  > EPS)
			cropY = height - 1 - (cropY + cropW - 1);
		int w = int(sn * cropW);
		int x = int(cropX - 0.5 * sn * cropW + cropW * sn * xn + 0.5 * cropW);
		int y = int(cropY - 0.5 * sn * cropW + cropW * sn * yn + 0.5 * cropW);
		float maxRotateScore = 0;
		int maxRotateIndex = 0;
		for (int j = 0; j < 3; j++)
		{
			float rotateProb = rotateProbsMat.at<float>(j, 0);
			if (rotateProb > maxRotateScore)
			{
				maxRotateScore = rotateProb;//rotateProb->data_at(i, j, 0, 0);
				maxRotateIndex = j;
			}
		}

		if (xyValid(x, y, _img) && xyValid(x + w - 1, y + w - 1, _img)) //(Legal(x, y, img) && Legal(x + w - 1, y + w - 1, img))
		{
			float angle = 0;
			if (abs(_faces[dataNr].angle)  < EPS)
			{
				if (maxRotateIndex == 0)
					angle = 90;
				else if (maxRotateIndex == 1)
					angle = 0;
				else
					angle = -90;
				faceBoxes_2.push_back(FaceBox(x, y, w, w, angle, _faces[dataNr].scale, score));
				//ret.push_back(Window2(x, y, w, w, angle, _faces[i].scale, prob->data_at(i, 1, 0, 0)));
			}
			else
			{
				if (maxRotateIndex == 0)
					angle = 90;
				else if (maxRotateIndex == 1)
					angle = 180;
				else
					angle = -90;
				faceBoxes_2.push_back(FaceBox(x, height - 1 - (y + w - 1), w, w, angle, _faces[dataNr].scale, score));
			}
		}
	}
	return faceBoxes_2;
}



std::vector<FaceBox> PCN_3(cv::Mat _img, cv::Mat _img180, cv::Mat _img90, cv::Mat _imgNeg90, cv::dnn::Net _net, float _threshold, int _dim, std::vector<FaceBox> _faces)
{
	if (_faces.size() == 0)
		return _faces;

	std::vector<cv::Mat> dataList;
	int height = _img.rows;
	int width = _img.cols;
	for (int i = 0; i < _faces.size(); i++)
	{
		if (abs(_faces[i].angle) < EPS)
			dataList.push_back(preprocessImg(_img(cv::Rect(_faces[i].x, _faces[i].y, _faces[i].w, _faces[i].h)), _dim));
		else if (abs(_faces[i].angle - 90) < EPS)
		{
			dataList.push_back(preprocessImg(_img90(cv::Rect(_faces[i].y, _faces[i].x, _faces[i].h, _faces[i].w)), _dim));
		}
		else if (abs(_faces[i].angle + 90) < EPS)
		{
			int x = _faces[i].y;
			int y = width - 1 - (_faces[i].x + _faces[i].w - 1);
			dataList.push_back(preprocessImg(_imgNeg90(cv::Rect(x, y, _faces[i].w, _faces[i].h)), _dim));
		}
		else
		{
			int y2 = _faces[i].y + _faces[i].h - 1;
			dataList.push_back(preprocessImg(_img180(cv::Rect(_faces[i].x, height - 1 - y2, _faces[i].w, _faces[i].h)), _dim));
		}
	}

	std::vector<FaceBox> faceBoxes_3;

	for (int dataNr = 0; dataNr<dataList.size(); dataNr++)
	{
		Mat inputBlob = blobFromImage(dataList[dataNr], 1.0, Size(), Scalar(), false, false);
		_net.setInput(inputBlob);
		std::vector<String> outputBlobNames = { "cls_prob", "rotate_reg_3", "bbox_reg_3" };
		std::vector<cv::Mat> outputBlobs;

		_net.forward(outputBlobs, outputBlobNames);
		cv::Mat scoresData = outputBlobs[0];
		cv::Mat rotateProbsData = outputBlobs[1];
		cv::Mat regressionData = outputBlobs[2];

		Mat scoresMat(scoresData.size[1], scoresData.size[0], CV_32F, scoresData.ptr<float>(0, 0)); // - using channel 0
		Mat regMat(regressionData.size[1], regressionData.size[0], CV_32F, regressionData.ptr<float>(0, 0));
		Mat rotateProbsMat(rotateProbsData.size[1], rotateProbsData.size[0], CV_32F, rotateProbsData.ptr<float>(0, 0));

		// - score = prob->data_at(i, 1, 0, 0)
		float score = scoresMat.at<float>(1, 0);
		if (score < _threshold)
			continue;

		float sn = regMat.at<float>(0, 0);// reg->data_at(i, 0, 0, 0);
		float xn = regMat.at<float>(1, 0);// reg->data_at(i, 1, 0, 0);
		float yn = regMat.at<float>(2, 0);//reg->data_at(i, 2, 0, 0);

		int cropX = _faces[dataNr].x;
		int cropY = _faces[dataNr].y;
		int cropW = _faces[dataNr].w;
		cv::Mat imgTmp = _img;
		if (abs(_faces[dataNr].angle - 180)  < EPS)
		{
			cropY = height - 1 - (cropY + cropW - 1);
			imgTmp = _img180;
		}
		else if (abs(_faces[dataNr].angle - 90)  < EPS)
		{
			std::swap(cropX, cropY);
			imgTmp = _img90;
		}
		else if (abs(_faces[dataNr].angle + 90)  < EPS)
		{
			cropX = _faces[dataNr].y;
			cropY = width - 1 - (_faces[dataNr].x + _faces[dataNr].w - 1);
			imgTmp = _imgNeg90;
		}

		int w = int(sn * cropW);
		int x = int(cropX - 0.5 * sn * cropW + cropW * sn * xn + 0.5 * cropW);
		int y = int(cropY - 0.5 * sn * cropW + cropW * sn * yn + 0.5 * cropW);
		float angleRange_ = 45;
		float angle = angleRange_ * rotateProbsMat.at<float>(0, 0);
		if (xyValid(x, y, imgTmp) && xyValid(x + w - 1, y + w - 1, imgTmp))
		{
			if (abs(_faces[dataNr].angle)  < EPS)
				faceBoxes_3.push_back(FaceBox(x, y, w, w, angle, _faces[dataNr].scale, score));
			else if (abs(_faces[dataNr].angle - 180)  < EPS)
			{
				faceBoxes_3.push_back(FaceBox(x, height - 1 - (y + w - 1), w, w, 180 - angle, _faces[dataNr].scale, score));
			}
			else if (abs(_faces[dataNr].angle - 90)  < EPS)
			{
				faceBoxes_3.push_back(FaceBox(y, x, w, w, 90 - angle, _faces[dataNr].scale, score));
			}
			else
			{
				faceBoxes_3.push_back(FaceBox(width - y - w, x, w, w, -90 + angle, _faces[dataNr].scale, score));
			}
		}
	}

	return faceBoxes_3;
}



cv::Point RotatePoint(int x, int y, float centerX, float centerY, float angle)
{
	x -= centerX;
	y -= centerY;
	float theta = -angle * M_PI / 180;
	int rx = int(centerX + x * std::cos(theta) - y * std::sin(theta));
	int ry = int(centerY + x * std::sin(theta) + y * std::cos(theta));
	return cv::Point(rx, ry);
}

void DrawLine(cv::Mat img, std::vector<cv::Point> pointList)
{
	int thick = 2;
	cv::Scalar cyan = CV_RGB(0, 0, 255);
	cv::Scalar red = CV_RGB(255, 0, 0);
	cv::Scalar blue = CV_RGB(0, 255, 0);
	cv::Scalar blue1 = CV_RGB(255, 255, 0);
	cv::line(img, pointList[0], pointList[1], cyan, thick);
	cv::line(img, pointList[1], pointList[2], red, thick);
	cv::line(img, pointList[2], pointList[3], blue, thick);
	cv::line(img, pointList[3], pointList[0], blue1, thick);
}

void DrawFace(cv::Mat img, FaceBox face, ushort &x0, ushort &y0, ushort &x1, ushort &y1, ushort &x2, ushort &y2, ushort &x3, ushort &y3 )
{
	int facex1 = face.x;
	int facey1 = face.y;
	int facex2 = face.w + face.x - 1;
	int facey2 = face.w + face.y - 1;
	int centerX = (facex1 + facex2) / 2;
	int centerY = (facey1 + facey2) / 2;
	std::vector<cv::Point> pointList;
	cv::Point a0 = RotatePoint(facex1, facey1, centerX, centerY, face.angle);
	cv::Point a1 = RotatePoint(facex1, facey2, centerX, centerY, face.angle);
	cv::Point a2 = RotatePoint(facex2, facey2, centerX, centerY, face.angle);
	cv::Point a3 = RotatePoint(facex2, facey1, centerX, centerY, face.angle);
	x0 = a0.x; y0 = a0.y;
	x1 = a1.x; y1 = a1.y;
	x2 = a2.x; y2 = a2.y;
	x3 = a3.x; y3 = a3.y;
	pointList.push_back(RotatePoint(facex1, facey1, centerX, centerY, face.angle));
	pointList.push_back(RotatePoint(facex1, facey2, centerX, centerY, face.angle));
	pointList.push_back(RotatePoint(facex2, facey2, centerX, centerY, face.angle));
	pointList.push_back(RotatePoint(facex2, facey1, centerX, centerY, face.angle));
	DrawLine(img, pointList);
}


// ------------------------------------------------------------------------------------------------------------
DllExport short Load_Model(const char *szPrototxt1, const char *szPrototxt2, const char *szPrototxt3, const char *szCaffemodel);
DllExport ushort FaceKit(ushort rows, ushort cols, int widthStep, byte *rgbData,
	 ushort *x0, ushort *y0, ushort *x1, ushort *y1, ushort *x2, ushort *y2, ushort *x3, ushort *y3);

// Bien tong the, chi thay doi duy nhat 1 lan goi
Net netModel1, netModel2, netModel3;
unsigned char bModel_Loaded = 0;

DllExport short Load_Model(const char *szPrototxt1, const char *szPrototxt2, const char *szPrototxt3, const char *szCaffemodel)
{
	if (bModel_Loaded == 0)
	{
		netModel1 = dnn::readNet(szPrototxt1, szCaffemodel);
		netModel2 = dnn::readNet(szPrototxt2, szCaffemodel);
		netModel3 = dnn::readNet(szPrototxt3, szCaffemodel);
		// can phai kiem tra truong hop khi loat not ok, tam thoi chua kiem tra
		// neu load not ok thi return -1
		bModel_Loaded = 1; // load dung 1 lan
	}//if

	return 1;
}// Load_PB_Model

DllExport ushort FaceKit(ushort rows, ushort cols, int widthStep, byte *rgbData,
	 ushort *x0, ushort *y0, ushort *x1, ushort *y1, ushort *x2, ushort *y2, ushort *x3, ushort *y3)
{
	/*if (bModel_Loaded == 0) return -1;*/
	Mat matRGB = Mat(rows, cols, CV_8UC3, rgbData, widthStep).clone();
	Mat paddedImg = padImg(matRGB);

	cv::Mat img180, img90, imgNeg90;
	cv::flip(paddedImg, img180, 0);
	cv::transpose(paddedImg, img90);
	cv::flip(img90, imgNeg90, 0);

	float thresholds[] = { 0.37, 0.43, 0.95 };
	int minFaceSize = 40;
	std::vector<FaceBox> faces = PCN_1(matRGB, paddedImg, netModel1, thresholds[0], minFaceSize);
	//faces = NMS(faces, true, 0.8);
	faces = NMS(faces, true, 0.1);
	faces = PCN_2(paddedImg, img180, netModel2, thresholds[1], 24, faces);
	/*faces = NMS(faces, true, 0.8);*/
	faces = NMS(faces, true, 0.1);
	faces = PCN_3(paddedImg, img180, img90, imgNeg90, netModel3, thresholds[2], 48, faces);
	//faces = NMS(faces, false, 0.3);
	faces = NMS(faces, false, 0.3);
	
	std::vector<FaceBox> preList = TransformBoxes(matRGB, paddedImg, faces);
	//*countObjectBox = (ushort)preList.size();
	for (int i = 0; i < preList.size(); i++)
	{
		DrawFace(matRGB, preList[i], x0[i], y0[i], x1[i], y1[i], x2[i], y2[i], x3[i], y3[i]);
	}
	return (ushort)preList.size();
}//RGBForLocatingObject

DllExport short TestEast();
DllExport short TestEast()
{
	int i, j;
	char sz[1024];
	FILE *fp = NULL;
	// Khai bao bien for output
	ushort countObjectBox;
	ushort x0[50] = { 0 };  ushort y0[50] = { 0 };
	ushort x1[50] = { 0 };  ushort y1[50] = { 0 };
	ushort x2[50] = { 0 };  ushort y2[50] = { 0 };
	ushort x3[50] = { 0 };  ushort y3[50] = { 0 };
	ushort ok;

	// load model
	Load_Model("pcn_model/PCN-1.prototxt", "pcn_model/PCN-2.prototxt", "pcn_model/PCN-3.prototxt", "pcn_model/PCN.caffemodel");
#ifdef __DEBUG_PRINT__
	printf("\tAfter calling Load_Model\n");
#endif
	if (bModel_Loaded == 0)
	{
		printf("can not load \n");
		return -1;
	}// if

	fopen_s(&fp, ".\\listEast.lst", "rt");
	if (fp == NULL)
	{
		printf("can not read listEast.lst\n");
		return -1;
	}//if

#ifdef __DEBUG_PRINT__
	printf("\tAfter openning listEast.lst\n");
#endif
	// Doc tung file name
	while (1)
	{
		sz[0] = '\0';
		if (fgets(sz, 1023, fp) == NULL)
			break;

		if (sz[(int)strlen(sz) - 1] == 0x0d || sz[(int)strlen(sz) - 1] == 0x0a)
			sz[(int)strlen(sz) - 1] = '\0';


		printf("File:%s\n", sz);

		Mat matRGB = imread(sz);
		if (matRGB.empty())
		{
			printf("Can not read image file %s\n", sz);
			continue;
		}//if

		IplImage IplRGB = matRGB;

		countObjectBox =  FaceKit((ushort)IplRGB.height, (ushort)IplRGB.width, IplRGB.widthStep, (byte*)IplRGB.imageData,
			 x0, y0, x1, y1, x2, y2, x3, y3);
		// Ve ket qua len bo dem anh
		for (i = 0; i < countObjectBox; i++)
		{
				cv::Point p0 = cv::Point(x0[i],y0[i]);
				cv::Point p1 = cv::Point(x1[i],y1[i]);
				cv::Point p2 = cv::Point(x2[i], y2[i]);
				cv::Point p3 = cv::Point(x3[i], y3[i]);
				line(matRGB, p0, p1, Scalar(0, 255, 0), 1);
				line(matRGB, p1, p2, Scalar(0, 255, 0), 1);
				line(matRGB, p2, p3, Scalar(0, 255, 0), 1);
				line(matRGB, p3, p0, Scalar(0, 255, 0), 1);
		}// for
		 // hien thi anh
		imshow(sz, matRGB);
	}//while
	fclose(fp); fp = NULL;

	waitKey();
	return 1;
} // TestEast
int main()
{
	Net net_1 = readNet("pcn_model/PCN-1.prototxt", "pcn_model/PCN.caffemodel");
	Net net_2 = readNet("pcn_model/PCN-2.prototxt", "pcn_model/PCN.caffemodel");
	Net net_3 = readNet("pcn_model/PCN-3.prototxt", "pcn_model/PCN.caffemodel");

	string a = "image/0.jpg";
	Mat img = imread(a);
	Mat paddedImg = padImg(img);

	cv::Mat img180, img90, imgNeg90;
	cv::flip(paddedImg, img180, 0);
	cv::transpose(paddedImg, img90);
	cv::flip(img90, imgNeg90, 0);

	float thresholds[] = { 0.37, 0.43, 0.95 };

	cv::TickMeter tm;
	tm.reset();
	tm.start();

	int minFaceSize = 40;
	std::vector<FaceBox> faces = PCN_1(img, paddedImg, net_1, thresholds[0], minFaceSize);
	//faces = NMS(faces, true, 0.8);
	faces = NMS(faces, true, 0.1);
	faces = PCN_2(paddedImg, img180, net_2, thresholds[1], 24, faces);
	/*faces = NMS(faces, true, 0.8);*/
	faces = NMS(faces, true, 0.1);
	faces = PCN_3(paddedImg, img180, img90, imgNeg90, net_3, thresholds[2], 48, faces);
	//faces = NMS(faces, false, 0.3);
	faces = NMS(faces, false, 0.3);

	tm.stop();
	std::cout << "Time Cost: " << tm.getTimeMilli() << " ms" << std::endl;
	
	std::vector<FaceBox> preList = TransformBoxes(img, paddedImg, faces);
	ushort x0[50] = { 0 };  ushort y0[50] = { 0 };
	ushort x1[50] = { 0 };  ushort y1[50] = { 0 };
	ushort x2[50] = { 0 };  ushort y2[50] = { 0 };
	ushort x3[50] = { 0 };  ushort y3[50] = { 0 };
	for (int i = 0; i < preList.size(); i++)
	{
		DrawFace(img, preList[i],x0[i],y0[i], x1[i], y1[i], x2[i], y2[i], x3[i], y3[i]);
		cout << x0[i] << " " << y0[i] << " " << x1[i] << " " << y1[i] << " " << x2[i] << " " << y2[i] << " " << x3[i] << " " << y3[i] << endl;
	}
	imshow("IMG", img);
	waitKey();


	return 1;

}

