using static System.Console;
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
}