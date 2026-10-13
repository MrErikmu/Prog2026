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
    if (b < a && b < c)
    {
        if (a < c)
        {
            temp = a;
            a = b;
            b = temp;
        }
    }
    if (c < a && c < b)
    {
        if (b < a)
        {
            temp = c;
            c = a;
            a = temp;
        }
    }
}