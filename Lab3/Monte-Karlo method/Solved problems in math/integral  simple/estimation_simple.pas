program simple;
var i: integer;  h, k, d, f, a: real;
begin

h:=0;
a:=0;
randomize;
for i:= 1 to 10 do begin
k:=random;
d:=k*6 + 1;
f:=sqrt(d*d*d)/(cos(d)*cos(d)+1);
writeln('Випадкове число k = ', k:3:4);
writeln('Значення xi = ', d:3:4);
writeln('Значення f(x) = ', f:3:4);
h:=h+f;
a:=a+sqr(f);
writeln('-----------------------');
writeln('-----------------------');
end;
writeln('_______________________');
writeln('Suma f(x) = ',h:3:4);
writeln('Suma sqr(f(x)) = ', a:3:4);
readln;
end.