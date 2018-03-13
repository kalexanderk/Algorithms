program error;
var i: integer; e, h, k, l: real;
begin
k:=0;
l:=0;
for i:= 1 to 30 do begin
write('Vvedit ej = ');
readln(e);
h:=e*sin(e)/( 1+sqr(cos(e)) );
writeln('f(j) = ', h:2:4);
writeln('sqr(f(j) = ', sqr(h):2:4);
k:=k+h;
l:=l+sqr(h);
writeln('-----------------------');
writeln('-----------------------');
end;
writeln('_______________________');
writeln('Suma f(x) = ',k:3:4);
writeln('Suma sqr(f(x)) = ', l:3:4);
end.