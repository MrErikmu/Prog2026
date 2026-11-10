using System.Text.RegularExpressions;
using static System.Console;
string[] letrasValidasDni = ["T", "R", "W", "A", "G", "M", "Y", "F", "P", "D", "X", "B", "N", "J", "Z", "S", "Q", "V", "H", "L", "C", "K", "E" ];
const string LETRAS_VALIDAS_MATRICULA = "BCDFGHJKLMNPRSTVWXYZ";
const string REGEX_DNI= @"^[0-9]{8}[A-Z]$";
const string REGEX_MATRICULA = @"^[0-9]{4}[BCDFGHJKLMNPRSTVWXYZ]{3}$";
const int MODULO_FORMATO_DNI = 23;
const int TAMAÑO_MATRICULA = 7;
const int TAMAÑO_DNI = 9;
string[] matriculasPrueba = ["4589ABC", "9876XYZ", "1234AEI", "12345BC"];
string[] dniPrueba = ["12345678Z", "tengosueño", "12345678A", "87654321X"];
ImprimirPruebas(matriculasPrueba,dniPrueba);
return;

bool ValidarMatriculaRegex(string matricula)
{
    return Regex.IsMatch(matricula, REGEX_MATRICULA);
}

bool ValidarMatricula(string matricula)
{
    if (matricula.Length != TAMAÑO_MATRICULA) return false;
    var numerosMatricula = matricula.Substring(0,4);
    var letrasMatricula= matricula.Substring(4);
    foreach (var numero in numerosMatricula)
    {
        if (!int.TryParse(numerosMatricula, out _))
        {
            return false;
        }
    }
    return ValidarLetrasMatricula(letrasMatricula);
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

bool ValidarDniRegex(string dni)
{
    if (!Regex.IsMatch(dni, REGEX_DNI)) return false;
    var numerosDni = int.Parse(dni.Substring(0, 8));
    var resultadoModulo = numerosDni % MODULO_FORMATO_DNI;
    //Manera fea de escribir la comparacion
    return letrasValidasDni[resultadoModulo].Equals(dni[8].ToString(), StringComparison.CurrentCultureIgnoreCase);
}

bool ValidarDni(string dni)
{
    if (dni.Length != TAMAÑO_DNI) return false;
    var numerosDni = dni.Substring(0, 8);
    if (!int.TryParse(numerosDni, out var numerosParsed))
    {
        return false;
    }
    var resultadoModulo = numerosParsed % MODULO_FORMATO_DNI;
    //Manera bonita de escribir la comparación
    return letrasValidasDni[resultadoModulo].Equals(dni[8].ToString(), StringComparison.CurrentCultureIgnoreCase);
}

void ImprimirPruebas(string[] matriculas, string[] dnis)
{
    foreach (var matricula in matriculas)
    {
        bool validarSinRegex = ValidarMatricula(matricula);
        bool validarConRegex = ValidarMatriculaRegex(matricula);
        var resultadoSinRegex = validarSinRegex ? "Válido" : "Falso";
        var resultadoConRegex = validarConRegex ? "Válido" : "Falso";
        WriteLine($"Matrícula: {matricula} -> sin Regex: {resultadoSinRegex} --> con Regex: {resultadoConRegex}");
    }
    foreach (var dni in dnis)
    {
        bool validarSinRegex = ValidarDni(dni);
        bool validarConRegex = ValidarDniRegex(dni);
        var resultadoSinRegex = validarSinRegex ? "Válido" : "Falso";
        var resultadoConRegex = validarConRegex ? "Válido" : "Falso";
        WriteLine($"Matrícula: {dni} -> sin Regex: {resultadoSinRegex} --> con Regex: {resultadoConRegex}");
    }
}