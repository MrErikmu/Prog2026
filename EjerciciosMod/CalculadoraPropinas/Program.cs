Console.WriteLine("Comprobador de palindormos");
Console.WriteLine("---------------------------");
string? palabra = String.Empty;
do
{
    Console.WriteLine("Introduce una palabra para comprobar:");
    palabra = Console.ReadLine();
} while (palabra == null);
if (!EsPalin(palabra!))
{
    Console.WriteLine($"{palabra} no es palindromo");
}
else
{
    Console.WriteLine($"{palabra} es palindromo");
}
bool EsPalin(string palabra)
{
    string p = palabra.ToLower();
    var j = palabra.Length-1;
    for (int i=0; i<=p.Length-1; i++)
    {
            if (p[j] != p[i])
            {
                return false;
            }
            j--;
    }
    return true;
} 

