#include "geometry.h"
#include<iostream>
#include<fstream>

using namespace std;

char SignChar(double x)
{
  if (x>=0) return ('+');
        else  return ('-');

}

double Deter2(double a1 ,double b1,double c1,double d1)
{
   return(a1*d1-c1*b1);
}

double Deter3 (double a1, double b1, double c1,
	      double d1, double e1, double f1,
	      double g1, double h1, double i1)
{
   return(a1*e1*i1+c1*d1*h1+g1*b1*f1-g1*e1*c1-b1*d1*i1-a1*f1*h1);
}

double Deter4( double a1, double b1, double c1, double d1,
	      double e1, double f1, double g1, double h1,
	      double i1, double j1, double k1, double l1,
	      double m1, double n1, double o1, double p1)
{
    double dea,deb,dec,ded;

    dea=Deter3(f1,g1,h1, j1,k1,l1, n1,o1,p1);
    deb=Deter3(e1,g1,h1, i1,k1,l1, m1,o1,p1);
    dec=Deter3(e1,f1,h1, i1,j1,l1, m1,n1,p1);
    ded=Deter3(e1,f1,g1, i1,j1,k1, m1,n1,o1);

    return(a1*dea-b1*deb+c1*dec-d1*ded);

}


void inppoint (TPoint *point) {
  int l;
  cout << "Input point coordinates x y z: "<<endl;
  		do{
			   cout<<"x: ";
			   l=scanf("%lf",&(point->x));
		   	   fflush(stdin);
		} while (l!=1);
		do{
			   cout<<"y: ";
			   l=scanf("%lf",&(point->y));
		   	   fflush(stdin);
		} while (l!=1);
		do{
			   cout<<"z: ";
			   l=scanf("%lf",&(point->z));
		   	   fflush(stdin);
		} while (l!=1);
}

void outpoint (ofstream& fout, TPoint *point) {
  fout << "x = " << point->x << " y = " <<  point->y << " z = " << point->z;
}

int equpoints( TPoint p1, TPoint p2)
{
    return (DistPoints(&p1,&p2) < eps);
}

double DistPoints(TPoint *p1,TPoint *p2) {
  TVector c;
  c.x=p1->x-p2->x;
  c.y=p1->y-p2->y;
  c.z=p1->z-p2->z;

  return(norm(&c));
}

void TurnXPoint(TPoint *M,double  phi,TPoint *nw)
{   //double r1,r;

    nw->x=M->x;
    nw->y=((M->y)*cos(phi))-(M->z*sin(phi));
    nw->z=M->y*sin(phi)+M->z*cos(phi);
}

void TurnYPoint(TPoint *M,double  phi,TPoint *nw)
{
    nw->x=M->x*cos(phi)+(M->z*sin(phi));;
    nw->y=M->y;
    nw->z=-M->x*sin(phi)+M->z*cos(phi);

}

void TurnZPoint(TPoint *M,double  phi,TPoint *nw)
{
    nw->x=M->x*cos(phi)-(M->y*sin(phi));
    nw->y=M->x*sin(phi)+(M->y*cos(phi));;
    nw->z=M->z;

}

void inpvec(TVector *vec) {
  cout << "Input vector coordinates x y z: "; cin >> vec->x >> vec->y >> vec->z;
}

void outvec (ofstream& fout, TVector *vec) {
  fout << "x = " << vec->x << " y = " <<  vec->y << " z = " << vec->z;
}

double norm(TVector *vec) {
  return(sqrt(pow(vec->x,2)+pow(vec->y,2)+pow(vec->z,2)));
}

double ScalarMult(TVector *a,TVector *b) {
  return(a->x*b->x+a->y*b->y+a->z*b->z);
}

double MixedMult(TVector *a,TVector *b,TVector *c) {
  return(Deter3(a->x,a->y,a->z,
		b->x,b->y,b->z,
		c->x,c->y,c->z)
	);
}

void VectorMult(TVector *a,TVector *b,TVector *c)
{
 c->x=(a->y)*(b->z) - (b->y)*(a->z);
 c->y=a->z*b->x - b->z*a->x;
 c->z=a->x*b->y - b->x*a->y;
}

void Scale(TPoint *M, double con,TPoint *nw)
{
  nw->x=(M->x)*con;
  nw->y=(M->y)*con;
  nw->z=(M->z)*con;

}


void AddVect(TVector *a,TVector *b,TVector *c)
{
 c->x=(a->x)+(b->x);
 c->y=a->y+b->y;
 c->z=a->z+b->z;
}

void SubVect(TVector *a,TVector *b,TVector *c)
{
 c->x=(a->x)-(b->x);
 c->y=a->y-b->y;
 c->z=a->z-b->z;

}


void MovePoint(TPoint *M,TVector *way,TPoint *nw)
{
 nw->x=M->x+way->x;
 nw->y=M->y+way->y;
 nw->z=M->z+way->z;

}

void inpplane (TPlane* plane)
{
  cout << "Input plane coordinates A B C D (Ax+By+Cz+D=0): "; 
  cin >> plane->A >> plane->B >> plane->C >> plane->D;
}

void outplane(ofstream& fout, TPlane *plane)
{
  fout << "Plane: (" << plane->A << ")*x + (" 
                     << plane->B << ")*y + ("
                     << plane->C << ")*z + ("
                     << plane->D << ") = 0" << endl;
}


int correctplane(TPlane *p)
{
  double bool1;
  if ((fabs(p->A)+fabs(p->B)+fabs(p->C))<0.00001) bool1=0; else bool1=1;
  return bool1;
}

int equplanes(TPlane plane11,TPlane plane22)
{
   double res;
   if( (fabs(plane11.A-plane22.A)<0.0001) && (fabs(plane11.B-plane22.B)<0.0001) &&
       (fabs(plane11.C-plane22.C)<0.0001) && (fabs(plane11.D-plane22.D)<0.0001))
      res=1;
   else res=0;
  return(res);
}

void normplane(TPlane *plane)
{
  double n,m;
  double ressucc;
  ressucc=correctplane(plane);
  if (ressucc==1)
      {if ((plane->D)>eps) n=-1; else n=1;
       m=n*sqrt(pow(plane->A,2)+pow(plane->B,2)+pow(plane->C,2));
       plane->A=plane->A/m;
       plane->B=plane->B/m;
       plane->C=plane->C/m;
       plane->D=plane->D/m;
      }

}


double GetPlane1(ofstream& fout,TPoint *M1,TPoint *M2,TPoint *M3,TPlane *pl)
{ double ress;
  TVector v12,v13,v23;
  v12.x=M1->x-M2->x;
  v12.y=M1->y-M2->y;
  v12.z=M1->z-M2->z;

  v13.x=M1->x-M3->x;
  v13.y=M1->y-M3->y;
  v13.z=M1->z-M3->z;
  VectorMult(&v12,&v13,&v23);
  if (fabs(norm(&v23))<eps) {fout << "\ntochki na odniy priamiy\n";

			   ress=0;
			     }
       else  { pl->A=v23.x;
	       pl->B=v23.y;
	       pl->C=v23.z;
	       pl->D=-pl->A*M1->x-pl->B*M1->y-pl->C*M1->z;
	       ress=1;
	     }

 return(ress);
 }


int PointToPlane(TPoint  *M, TPlane *p)
{
  double res1;
  res1=correctplane(p);
  if ( fabs(p->A*M->x+p->B*M->y+p->C*M->z+p->D)<0.00001) res1=1;
      else res1=0;

 return res1;
}

double DistPointPlane(TPoint *M, TPlane *pl, int *ResSucc)
{
  int r;

  r=correctplane(pl);
  
  if (r==0) { *ResSucc=0;
    return 0;
  }
  else {  normplane(pl); *ResSucc=1;
    return ((pl->A*M->x)+(pl->B*M->y)+(pl->C*M->z)+pl->D);
  }

}

int Cut3Planes(TPlane *ap,TPlane *bp,TPlane *cp,TPoint *p)
{
     double Det,DetX,DetY,DetZ,Norm;
     int r1,r2,r3, rer3;
    r1=correctplane(ap);
    r2=correctplane(bp);
    r3=correctplane(cp);
   if ((r1==0) && (r2==0) && (r3==0))
   //then
       rer3=0;
   else  {  Det=Deter3(ap->A,ap->B,ap->C,
                       bp->A,bp->B,bp->C,
                       cp->A,cp->B,cp->C);
            Norm=fabs(ap->A)+fabs(ap->B)+fabs(ap->C)+

                 fabs(bp->A)+fabs(bp->B)+fabs(bp->C)+

                 fabs(cp->A)+fabs(cp->B)+fabs(cp->C);

            if (fabs(Det)/Norm<pow(eps,3))
               //then
                 rer3=0;
                else
                    {  DetX=Deter3(ap->D,ap->B,ap->C,
                                   bp->D,bp->B,bp->C,
                                   cp->D,cp->B,cp->C);

                       DetY=Deter3(ap->A,ap->D,ap->C,
                                   bp->A,bp->D,bp->C,
                                   cp->A,cp->D,cp->C);
                       DetZ=Deter3(ap->A,ap->B,ap->D,
                                   bp->A,bp->B,bp->D,
                                   cp->A,cp->B,cp->D);
                       p->x=-DetX/Det;
                       p->y=-DetY/Det;
                       p->z=-DetZ/Det;
                       rer3=1;
                    }
         }
 return rer3;
}

int LineCutPlane (TLine *l,TPlane *p,TPoint *po)
{
  return Cut3Planes(&l->p1,&l->p2,p,po);

}


void ReadLineSgm(TLineSeg *p)
{
   printf("\n Введите первую точку : "); inppoint(&(p->A));
   printf("\n Введите вторую точку : "); inppoint(&(p->B));

}

void OutLineSgm(ofstream& fout,TLineSeg *p)
{
  fout << "\n Отрезок.Первая точка : "; outpoint(fout,&(p->A));
  fout << "\n         Вторая точка : "; outpoint(fout,&(p->B));

}

int correctLine(TLine *l)
{
	double bool1,bool2,bool3;
	bool2=correctplane(&(l->p1));
	bool3=correctplane(&(l->p2));
  if ((bool2==1) &&  (bool3==1) && (fabs((l->p1).A*(l->p2).B-(l->p1).B*(l->p2).A)+fabs((l->p1).B*(l->p2).C-(l->p1).C*(l->p2).B)+
      fabs((l->p1).C*(l->p2).A-(l->p1).A*(l->p2).C)>0.00001)) bool1=1; else bool1=0;
  return bool1;
}

int PointToLine(TPoint  *M, TPlane *p1,TPlane *p2)
{
   int re1,re2,re3;

   re1=PointToPlane(M,p1);
   re2=PointToPlane(M,p2);
   if ((re1==1) && (re2==1)) re3=1;
      else re3=0;

 return re3;
}

double StrSgmLenght(TLineSeg se)
{
   return(sqrt(pow((se.A.x-se.B.x),2)+pow((se.A.y-se.B.y),2)+pow((se.A.z-se.B.z),2)) );

}


double GetPyramid2(TPoint p1,TPoint p2,TPoint p3, TPoint p4,TPyramid *Pyr)
{
   double r1,r2,r3,r4,r5,r6;    double rer2;
   r1=equpoints(p1,p2);
   r2=equpoints(p1,p3);
   r3=equpoints(p1,p4);
   r4=equpoints(p2,p3);
   r5=equpoints(p2,p4);
   r6=equpoints(p3,p4);

   if ((r1==0) && (r2==0) && (r3==0) && (r4==0) && (r5==0) && (r6==0))
       {  Pyr->vertex=p1;
         (Pyr->base).A=p2;
         (Pyr->base).B=p3;
         (Pyr->base).C=p4;
         rer2=1;
        }
      else rer2=0;
   return(rer2);
}


double PyrVolume(TPyramid *p)
{
  return(fabs( ((p->base).A.x-(p->base).B.x)*((p->base).A.y-(p->base).C.y)*((p->base).A.z-(p->vertex).z)
              +((p->base).A.y-(p->base).B.y)*((p->base).A.z-(p->base).C.z)*((p->base).A.x-(p->vertex).x)
              +((p->base).A.z-(p->base).B.z)*((p->base).A.x-(p->base).C.x)*((p->base).A.y-(p->vertex).y)
              -((p->base).A.z-(p->base).B.z)*((p->base).A.y-(p->base).C.y)*((p->base).A.x-(p->vertex).x)
              -((p->base).A.y-(p->base).B.y)*((p->base).A.x-(p->base).C.x)*((p->base).A.z-(p->vertex).z)
              -((p->base).A.x-(p->base).B.x)*((p->base).A.z-(p->base).C.z)*((p->base).A.y-(p->vertex).y)
             )
          /6);
}

double GetTangent(TPoint *M,TPlane *pl,TLine *l)
{
   double r,ResSucc;
  r=correctplane(pl);
  if (r==0) ResSucc=0;
     else  {if (fabs(pl->A)>eps) //then
				  //begin
				     { (l->p1).A=pl->B;
				       (l->p1).B=-(pl->A);
				       (l->p1).C=0;
				       (l->p1).D=pl->B*M->x-pl->A*M->y;
				       (l->p2).A=pl->C;
				       (l->p2).B=0;
				       (l->p2).C=-(pl->A);
				       (l->p2).D=pl->C*M->x-pl->A*M->z;
				       ResSucc=1;

				     }//end

		else //begin
		     { (l->p1).A=0;
		       (l->p1).B=pl->C;
		       (l->p1).C=-(pl->B);
		       (l->p1).D=pl->C*M->x-pl->A*M->z;

		       if (fabs(pl->B)>eps)  //then begin
					     {
					     (l->p2).A=pl->B;
					     (l->p2).B=-(pl->A);
					     (l->p2).C=0;
					     (l->p2).D=pl->B*M->x-pl->A*M->y;
					     ResSucc=1;

					     }//end

			   else //C<>0
				{//begin
				  (l->p2).A=pl->C;
				  (l->p2).B=0;
				  (l->p2).C=-(pl->A);
				  (l->p2).D=pl->C*M->x-pl->A*M->z;

				  if (pl->C<eps) //then
						 ResSucc=0;
				    else   ResSucc=1;

				}//end
		   }//end
		     (l->p1).D=-(l->p1).D;
		     (l->p2).D=-(l->p2).D;
		     normplane(&l->p1);
		     normplane(&l->p2);   //ResSucc=1;
	    }//end

      return ResSucc;
}


double GetPyramid1(TPlane p1,TPlane p2,TPlane p3,TPlane p4,TPyramid Pyr)
{
   double ResSucc,r;

  ResSucc=Cut3Planes(&p1,&p2,&p3,&Pyr.vertex);
  if (ResSucc==1) //then
                 r=Cut3Planes(&p1,&p2,&p3,&Pyr.base.A);
  if (r==1)       //then
                 ResSucc=Cut3Planes(&p1,&p3,&p4,&Pyr.base.B);

  if (ResSucc==1) //then
                 r=Cut3Planes(&p2,&p3,&p4,&Pyr.base.C);
  if ((r==1) && (ResSucc==1))
            return r;
       else return 0;

}

void ProjectPointToPlane(TPoint *M,TPlane *pl,TPoint *nw)
{
      TLine l;   double r;
  r=GetTangent(M,pl,&l);
  if (r==1) LineCutPlane(&l,pl,nw);
}

double TriSquare(TTriangle *t)
{ double d1,d2,d3;

  d1=((t->B).y-(t->A).y)*((t->C).z-(t->A).z)-((t->B).z-(t->A).z)*((t->C).y-(t->A).y);
  d2=((t->B).z-(t->A).z)*((t->C).x-(t->A).x)-((t->B).x-(t->A).x)*((t->C).z-(t->A).z);
  d3=((t->B).x-(t->A).x)*((t->C).y-(t->A).y)-((t->B).y-(t->A).y)*((t->C).x-(t->A).x);

  return(0.5*sqrt( pow(d1,2)+pow(d2,2)+pow(d3,2) ));
}

