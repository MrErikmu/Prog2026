using static System.Console;
/*Ej 37
WriteLine( "Ejercicio 37: ");
for (int i = 0; i > 5; i++)
{
    Write("*");
}
Ej 38 
WriteLine("Ejercicio 38: ");
for (int i = 5; i > 0; i--)
{
    Write("*");
}*/
//Ej 39 
WriteLine("Ejercicio 39: ");
for (var i = 5; i >= 5; i--)
{
    for (var u = 4; u >= 4; u--)
    {
        for (var y = 3; y >= 3; y++)
        {
            for (var t = 2; t >= 2; t++)
            {
                for (var r = 1; r >= 1; r++)
                {
                    WriteLine($"{r}");
                }
                WriteLine($"{t}");
            }
            WriteLine($"{y}");
        }
        WriteLine($"{u}");
    }
    WriteLine($"{i}");
}

/*ej 40
for (int i = 1; i <= 100; i++)
{
    if (i % 3 == 0)
    {
        WriteLine("Fizz");
    }
    if (i % 5 == 0)
    {
        WriteLine("Buzz");
    }
    if (i % 3 == 0 && i % 5 == 0)
    {
        WriteLine("FizzBuzz");
    }
    else
    {
        WriteLine(i);   
    }
} */