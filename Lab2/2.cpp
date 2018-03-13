#include <stdlib.h> 
#include <conio.h>  
#include <math.h>
#include <iostream> 
#include <fstream>  
#include <time.h>
#include <cstring>

#define eps 0.00001

using namespace std;
unsigned long moves, compares;

ofstream fout("sort.txt");

//Quick sort
void QuickSort(int * a, int onep, int twop) {
  int m,i,j;
  int x; 
  i = onep;
  j = twop;
  m=(onep+twop)/2; 
  x = a[m];

  while (i<=j) {
    while (a[i]<x){
	 	i++; 
		compares++; 
	}
    while (x<a[j]){ 
		j--; 
		compares++; 
	}
	compares+=2;;
    if (i<=j) {
      	if (i<j) {
        	swap(a[i], a[j]);
        	moves+=3;
      	}
    	i++; j--;
    }
  }

  if (onep<j) QuickSort(a,onep,j);
  if (i<twop) QuickSort(a,i,twop);
}

//Sort with insert
void ins(int * a, int N){
	int i,c,k;
	for(i=1; i<N; i++){
		k=i-1;
		c=a[i];
		while((k>=0) and(c<a[k])){
			a[k+1]=a[k];
			k--;
			compares++;
		}
	a[k+1]=c;	
	moves++;
	}
}

//Main part of the program
int main(){
  int * a;
  int * b;
  int i,j,N;
  clock_t start, finish;
  double  duration;
  bool sorted = true;
  do{
  	cout<<"\n(Input '0' to finish) N=";
  	cin>>N;
  	if (N<=0) return 1;
  	a = new int[N];
  	b = new int[N];  
  	cout<<"\nRandom elements:\n\n"<<endl;
  	srand(time(0));
  	moves=0;     
	compares=0;  
	start = clock();
	j=0;
	while(duration = (double)(finish - start)/ CLOCKS_PER_SEC<eps){
	  	for(i=0; i<N; i++) a[i] = rand()%100+1;
	    for(i=0; i<N; i++) cout<<a[i]<<"  ";
	    cout<<endl;
	    for (i = 0; i<N; i++) b[i]=a[i];
	  	QuickSort(b,0,N-1);
	  	finish = clock();
	  	j++;
	}
	cout<<"\n\nQuick\n"<<endl;
	for(i=0; i<N; i++) cout<<b[i]<<"  ";
	cout<< "\nTime to sort  "<<j<<"  times array of " << N << " elements QuickSort:";
	duration = (double)(finish - start) / CLOCKS_PER_SEC;
	cout << duration << " seconds" << endl;
	fout << endl << "Time to sort array of " << N << " elements QuickSort: "<< duration << endl;
	cout <<compares<<" - compares; "<< moves << " moves \n";
	fout <<compares<<" - compares; "<< moves << " moves \n";	
	srand(time(0));
	moves=0;    
	compares=0;  
	start = clock();
	
	cout<<"\n\nSorting with insert\n"<<endl;
	j=0;
	while(duration = (double)(finish - start)<eps){
	  	for(i=0; i<N; i++) 
	    a[i] = rand()%100+1;
	    for (i = 0; i<N; i++) b[i]=a[i];
	  	ins(b,N);
	  	finish = clock();
	  	j++;
	}
	for(i=0; i<N; i++){
	    cout<<b[i]<<"  ";
	}
	cout<< "\nTime to sort  "<<j<<"  times   array of " << N << " elements Ins Sort:";
	duration = (double)(finish - start) / CLOCKS_PER_SEC;
	cout << duration << " seconds" << endl;
	fout << endl << "Time to sort array of " << N << " elements InsSort: "<< duration << endl;
	cout <<compares<<" - compares; "<< moves << " moves \n";
	fout <<compares<<" - compares; "<< moves << " moves \n";
  } while(N>0);
  
  return 0;
}

