#include<stdio.h>
#include<conio.h>
#include<string.h>
#include<math.h>
#include<iostream>
#include<fstream>

#define eps 0.000001

using namespace std;

//Basic geometrical objects

typedef struct{
   double x, y, z;
} TPoint;

typedef TPoint TVector; 

typedef struct {
   double A, B, C, D;
} TPlane; //Plane Ax + By + Cz + D = 0

typedef struct {
   TPlane p1, p2;
} TLine;  //Direct line   //Introducing straight through the intersection of planes//

typedef struct {
   TPoint A, B;
} TLineSeg;  //Line Segment

typedef struct {
   TPoint source;
   TVector direction;
} TRay;  //Ray

typedef struct {
   TPoint A, B, C;
} TTriangle; //Triangle

typedef struct {
   TPoint vertex;
   TTriangle base;
} TPyramid;  //Triangle Pyramid

char SignChar(double); //Print sign of the number

double Deter2 (double, double, //Determinant 2d order
	           double, double);

double Deter3 (double, double, double, //Determinant 3d order
	           double, double, double,
	           double, double, double);


double Deter4 (double, double, double, double, //Determinant 4th order
	           double, double, double, double,
	           double, double, double, double,
	           double, double, double, double);





void inppoint(TPoint *); //Input point from keyboard
void outpoint(ofstream& fout, TPoint *); //Point output

void inpvec(TVector*);  //Vector input
void outvec(ofstream&, TVector *); //Vector output

void inpplane(TPlane *); //Plane input
void outplane(ofstream&,TPlane *); //Plane output

int correctplane(TPlane *); //Is the plane correctly assigned?
int correctLine(TLine *);  //Is the line correctly assigned?

int equpoints(TPoint p1, TPoint p2); //Are points equal?
int equplanes(TPlane plane11,TPlane plane22); //Are planes equal?


double DistPoints(TPoint *,TPoint *); //Distance betweeen points

void TurnXPoint(TPoint *,double phi,TPoint *); //Turn point around axis Ox (angle phi)

void TurnYPoint(TPoint *,double phi,TPoint *); //Turn point around axis Oy (angle phi)

void TurnZPoint(TPoint *,double phi,TPoint *); //Turn point around axis Oz (angle phi)

double norm (TVector*);  //Vector length

double ScalarMult(TVector *,TVector *); //Scalar Multiplication

double MixedMult(TVector *,TVector *,TVector *); //Mixed Multiplication of Vectors

void VectorMult(TVector*,TVector*,TVector*); //Vector Multiplication

void Scale (TPoint *,double, TPoint *); //Multiply vector by scalar

void AddVect(TVector*,TVector*,TVector*); //Sum of vectors

void SubVect(TVector*,TVector*,TVector*); //Vectors subtraction

void MovePoint(TPoint *,TVector *,TPoint *); //Move point alongside vector

void normplane(TPlane *); //Normalization of the plane

double GetPlane1(ofstream&,TPoint *,TPoint *,TPoint *,TPlane *); //Draw the plane through 3 points

int PointToPlane(TPoint  *, TPlane *); //Does the point belong to the plane?

double DistPointPlane(TPoint *, TPlane *, int *); //Distance from point to the plane

int Cut3Planes(TPlane *,TPlane *,TPlane *,TPoint *); //Point of intersction of 3 planes

int LineCutPlane(TLine *,TPlane *,TPoint *); //Point of intersection of the line and the plane

void ReadLineSgm(TLineSeg *); //Segment input
void OutLineSgm (ofstream&,TLineSeg *); //Segment output

int PointToLine(TPoint *,TPlane *,TPlane *); //Does the point belong to the line?

double StrSgmLenght(TLineSeg se); //Length of the segment

double GetPyramid2(TPoint p1,TPoint p2,TPoint p3, TPoint p4,TPyramid *); //Create the pyramid with vertex p1 and base <p2,p3,p4>

double PyrVolume(TPyramid *); //Pyramid volume

double GetTangent(TPoint *,TPlane *,TLine *); //Draw perpendicular from point to plane

double GetPyramid1(TPlane p1,TPlane p2,TPlane p3,TPlane p4,TPyramid Pyr); //Create pyramid with lateral edges p1,p2,p3 & base p4

void ProjectPointToPlane(TPoint *,TPlane *,TPoint*); //Project point to plane

double TriSquare(TTriangle *t); //Area of the triangle
