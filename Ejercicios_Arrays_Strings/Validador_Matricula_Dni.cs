using System.Text.RegularExpressions;
using static System.Console;
string[] letrasValidasDni = ["T", "R", "W", "A", "G", "M", "Y", "F", "P", "D", "X", "B", "N", "J", "Z", "S", "Q", "V", "H", "L", "C", "K", "E"];
const string LETRAS_VALIDAS_MATRICULA = "BCDFGHJKLMNPRSTVWXYZ";
const string regexDni= @"^[0-9]{8}[A-Z]$";
const string regexMatricula = @"^[0-9]{4}[A-Z]{3}$";
const int MODULO_FORMATO_DNI = 23;
const int TAMAÑO_MATRICULA = 7;
const int TAMAÑO_DNI = 9;
WriteLine("Introduzca la matricula en formato NNNNLLL");
var matriculaInput = ReadLine() ?? string.Empty;
ValidarMatricula(matriculaInput);
ValidarMatriculaRegex(matriculaInput);
WriteLine("Introduzca el DNI en formato NNNNNNNNL");
var dniInput = ReadLine() ?? string.Empty;
ValidarDniRegex(dniInput);
ValidarDni(dniInput);
return;

void ValidarMatriculaRegex(string matricula)
{
    if (!Regex.IsMatch(matricula, regexMatricula))
    {
        WriteLine($"La matricula{matricula} no es valida");
    }
    else
    {
        WriteLine($"La matricula {matricula} es valida");
    }
}

void ValidarMatricula(string matricula)
{
    if (matricula.Length != TAMAÑO_MATRICULA)
    {
        WriteLine($"Formato de matricula incorrecto {matricula} no es valida");
        return; 
    }
    var numerosMatricula = matricula.Substring(0,4);
    var letrasMatricula= matricula.Substring(4,3);
    foreach (var numero in numerosMatricula)
    {
        if (!int.TryParse(numerosMatricula, out _))
        {
            WriteLine($"Formato de matricula incorrecto {matricula} no es valida");
            return;
        }
    }
    bool isLetraOk = ValidarLetrasMatricula(letrasMatricula);
    if (!isLetraOk)
    {
        WriteLine($"Letras de la matricula {matricula} no validas");
        return;
    }
    WriteLine($"La matricula {matricula} es valida");
}
//Funcion auxilar para ValidarMatricula compara todos los chars de letrasMatricula con los chars de las letras validas
// si encuentra 3 positivos las letras de la matricula son validas 
bool ValidarLetrasMatricula(string letrasMatricula)
{
    var letrasCorrectas = 0;
    foreach (char letra in LETRAS_VALIDAS_MATRICULA)
    {
        //Tambien se puede hacer asi: 
        //if(!LETRAS_VALIDAS_MATRICULA.Contains(letrasMatricula))
        // y que devuelva falso. 
        for(var i= 0 ; i<= letrasMatricula.Length-1;i++)
            if (letra == letrasMatricula[i])
                letrasCorrectas++;
    }
    return letrasCorrectas == 3;

}

void ValidarDniRegex(string dni)
{
    if (!Regex.IsMatch(dni, regexDni))
    {
        WriteLine($"Formato dni incorrecto {dni} no es valido");
        return;
    }
    var numerosDni = int.Parse(dni.Substring(0, dni.Length - 2));
    var resultadoModulo = numerosDni % MODULO_FORMATO_DNI;
    //Manera fea de escribir la comparacion
    if (letrasValidasDni[resultadoModulo] == dni[dni.Length - 1].ToString().ToUpper())
    {
        WriteLine($"El {dni} es valido");
        return;
    }
    WriteLine($"Letra del dni:  {dni} no es valida");
}

void ValidarDni(string dni)
{
    if (dni.Length != TAMAÑO_DNI) return;
    var numerosDni = dni.Substring(0, 8);
    if (!int.TryParse(numerosDni, out var numerosParsed))
    {
        WriteLine($"Formato dni incorrecto {dni} los primeros 8 caracteres deben ser digitos");
        return;
    }
    var resultadoModulo = numerosParsed % MODULO_FORMATO_DNI;
    //Manera bonita de escribir la comparación
    if (letrasValidasDni[resultadoModulo].Equals(dni[^1].ToString(), StringComparison.CurrentCultureIgnoreCase))
    {
        WriteLine($"El {dni} es valido");
        return;
    }
    WriteLine($"Letra del dni:  {dni} no es valida");
}

