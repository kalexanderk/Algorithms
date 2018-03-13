#include<iostream>
#include<math.h>
#include <time.h>
#include <cstdlib>
#include <ctime>

using namespace std;

int indicator(double x, double y, double z) {
	if ((y<=1) and (y>=0) and (x<=2-y) and (x>=y) and (z>=0) and (z<=x-y)) return 1;
	else return 0;
}

void MonteCarlo(int N1, double V1){
	double x,y,z,V,p,m;
	int N=0, i=0;
	srand(time(NULL));
	while(i<N1){
		x=3*((double)rand()/RAND_MAX);
		y=2*((double)rand()/RAND_MAX);
		z=3*((double)rand()/RAND_MAX);
		if (indicator(x,y,z)) N++;
		i++;
	}
	p=(double)N/N1;	;
	V=p*V1;
	cout<<"N*="<<N1<<endl;
	cout<<"N="<<N<<endl;
	cout<<"NEW V="<<V<<endl;
	m=1.96*sqrt((1-p)/(N1*p));
	cout<<"m = "<<m<<endl;
	cout<<"With the probability, not less than 0.95, we can affirm that volume is in interval ("<<V*(1-m)<<","<<V*(1+m)<<")"<<endl;
}

int main(){
	int N_appr=5500000;
	cout<<"V1="<<18<<endl;
	MonteCarlo(N_appr,18);
}

