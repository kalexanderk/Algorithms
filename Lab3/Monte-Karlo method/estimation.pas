program area_int;
uses crt;
var i, e, N: integer;  k, l, x, y, f: real;
begin  clrscr;
writeln('Vvvedit znachennia N:');
write(' N = ');
readln(N);
writeln('_________________________');
writeln('-------------------------');
randomize;
for i:=1 to N do begin
k:=random;
l:=random;
x:=2*k;
y:=6*l;
f:=6*cos(x)/( 1+sqr(sin(x)/cos(x)) );
if y<f then e:=1 else e:=0;
writeln('r = ', k);
writeln('x = ', x);
writeln('R = ', l);
writeln('y = ', y);
writeln('f = ', f);
writeln('n = ', e);
writeln('-----------------------------------------');
writeln('-----------------------------------------');
readln;
end;
readln;
end.
