program integralA;
var
begin n, i: integer; k, e, f: real;
write('N = ');
readln(n);
writeln('__________________________________');
writeln('__________________________________');
randomize;
for i:=1 to n do begin
k:=random;
e:=arctan(sqrt(1-sqr(exp (ln (1-2*k) / 3)))/(1-sqr(exp (ln (1-2*k) / 3))));
f:=(e*sin(e)/((1+sqr(cos(e)))*sqr(cos(e))*sin(e));
writeln('__________________________________');
writeln('__________________________________');
end;
end.
