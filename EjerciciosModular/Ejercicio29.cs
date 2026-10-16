using static System.Console;

var n1 = 5;
var n2 = 2;
var n3 = 8;
WriteLine($"{n1} {n2} {n3}");
Ordenar(ref n1, ref n2, ref n3);
WriteLine($"{n1} {n2} {n3}");
void Ordenar(ref int a, ref int b, ref int c)
{
    int temp;
    if (a < b && a < c)
    {
        if (c < b)
        {
            temp = b;
            b = c;
            c = temp;
        }
    }
    else if (b < a && b < c)
    {
        if (a < c)
        {
            temp = a;
            a = b;
            b = temp;
        }
        else
        {
            temp= a;
            a=b;
            b=temp;
        }
    }
    else if (c < a && c < b)
    {
        if (b < a)
        {
            temp = c;
            c = a;
            a = temp;
           // Con tupla->  (a,b,c) = (c,a,b);
        }
        else
        {
            temp= c; 
            c=a;
            a=temp;
        }
    }
}
