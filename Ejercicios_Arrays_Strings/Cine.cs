using System.Text;
using System.Text.RegularExpressions;
using static System.Console;
OutputEncoding = Encoding.UTF8;
const double PRECIO_ENTRADA = 6.50;
var regexOpcionMenu = new Regex(@"^[1-6]{1}$");
var regexArgs = new Regex(@"^[4-7]:[5-9]$");
(int fila, int columna) tamañocine = ProcesarAgumentos();
var regexEntrada = new Regex(@"^[1-9]:[1-9]$");
var random = new Random();
int [,] salaCine = new int[tamañocine.fila,tamañocine.columna];
int opcionMenu;
var numeroVenta = 0;
CrearSala(salaCine);
GestionarCine();
Clear();
WriteLine("Presiona una tecla para cerrar el programa");
ReadKey();
return;

(int columnas, int filas) ProcesarAgumentos()
{
    (int columnas, int filas) tamañoCine = (0, 0);
    //Unimos todos los elementos de args sin espacios en 1 solo string
    if (args.Length==0)
    {
        WriteLine("Error Faltan argumentos para las filas y columnas con formato:  F:C" ); 
        return PedirArgumentos();
    }

    string argumentos = string.Concat(args).Replace(" ", "");
if (!regexArgs.IsMatch(argumentos))
    {
        WriteLine("ERROR: Formato inválido o fuera de rango. Use F:C. Ejemplo: 6:9");
        WriteLine("Rango de Columnas 4-7 y Filas 5-9");
        return PedirArgumentos();
    }
    var partes = argumentos.Split(':');
    tamañoCine.filas = int.Parse(partes[0]);
    tamañoCine.columnas = int.Parse(partes[1]);
    return tamañoCine;
}

(int columnas, int filas) PedirArgumentos()
{
    string input;
    do
    {
        WriteLine("Introduzca el tamaño de la sala en formato: F:C");
        WriteLine("Tamaño de fila debe estar comprendido entre 4 y 7");
        WriteLine("Tamaño de columna debe estar comprendido entre 5 y 9");
        input = ReadLine() ?? string.Empty;
    } while (!regexArgs.IsMatch(input));
    var partes = input.Split(':');
    var argumentos = (int.Parse(partes[0]), int.Parse(partes[1]));
    return argumentos;
}
void MostrarMenu()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Bienvenido al cine DAW, selecciona 1 opcion del menu");
    Console.WriteLine("=============================================");
    Console.WriteLine("1. Ver salas.");
    Console.WriteLine("2. Compra de entrada.");
    Console.WriteLine("3 Devolucion de entrada.");
    Console.WriteLine("4 Consultar recaudación.");
    Console.WriteLine("5. Ver informe.");
    Console.WriteLine("6. Salir.");
    Console.WriteLine();
}

void GestionarCine()
{
    do
    {
        MostrarMenu();
        opcionMenu = LeerInput(regexOpcionMenu);
            switch (opcionMenu)
            {
                case (int)OpcionesMenu.VerSala:
                    MostrarSala(salaCine);
                    break;
                case (int)OpcionesMenu.ComprarEntrada:
                    ComprarEntrada(salaCine);
                    break;
                case (int)OpcionesMenu.DevolverEntrada:
                    DevolverEntrada(salaCine);
                    break;
                case (int)OpcionesMenu.Recaudacion:
                    var total=CalcularGanancias(numeroVenta,PRECIO_ENTRADA);
                    Console.WriteLine($"Las ganancias totales de hoy ascienden a {total} €");
                    break;
                case (int)OpcionesMenu.Informe: 
                    Informe(salaCine,numeroVenta);
                    break;
                case (int)OpcionesMenu.Salir:
                    break;
            } 
    } while (opcionMenu != (int)OpcionesMenu.Salir);
}
int LeerInput(Regex expresion)
{
    string input;
    do
    {
        input = ReadLine()?.Trim() ?? "";
    } while (!expresion.IsMatch(input));
    return int.Parse(input);
}
string LeerInputEntrada(string mensaje, Regex expresion)
{
    string input;
    do
    {
        WriteLine("Introduce tu butaca en formato: N:N fila/columna por ejemplo 1:1");
        input = ReadLine()?.Trim().ToUpper() ?? "";
        if(!expresion.IsMatch(input))
            WriteLine(mensaje);
    } while (!expresion.IsMatch(input));
    return input;
}

void CrearSala(int[,]sala)
{
    var butacasNoDisponibles = random.Next(1, 4);
    var totalButacasNoDisponibles = 0;
    while (totalButacasNoDisponibles!=butacasNoDisponibles)
    {
        var filaAleatoria = random.Next(0, sala.GetLength(0) );
        var columbaAleatoria = random.Next(0, sala.GetLength(1));
        //Si la butaca ya esta fuera de servicio saltamos la iteracion del while con continue 
        if (sala[filaAleatoria, columbaAleatoria] == (int)EstadoButaca.FueraServicio) continue;
        sala[filaAleatoria, columbaAleatoria] = (int)EstadoButaca.FueraServicio;
        totalButacasNoDisponibles++;
    }
}

void MostrarSala(int[,] sala)
{
    WriteLine();
    for ( var i = 0; i<sala.GetLength(0); i++)
    {
        for (var j = 0; j < sala.GetLength(1); j++)
        {
            if (sala[i,j]==(int)EstadoButaca.Libre)
            {
                Write("[L]");
            }else if(sala[i,j]==(int)EstadoButaca.Ocupada)
            {
                Write("[O]"); 
            }
            else if(sala[i,j]==(int)EstadoButaca.FueraServicio)
            {
                Write("[X]");   
            }
        }
        WriteLine();
    }
    WriteLine("___________________________________");
}

void ComprarEntrada(int[,] sala)
{
    for ( var i = 0; i<sala.GetLength(0); i++)
    {
        for (var j = 0; j < sala.GetLength(1); j++)
        {
            if (sala[i,j]==(int)EstadoButaca.Libre)
            {
                sala[i,j] = (int)EstadoButaca.Ocupada;
                numeroVenta += 1;
                return;
            }
        }
    }
}

void DevolverEntrada(int[,] sala)
{
    var input=LeerInputEntrada("Error de formato, porfavor introduzca formato correcto", regexEntrada);
    var entradaInfo=input.Split(':');
    //-1 porque el usuario introduce la 0:0 como 1:1 
    var fila = int.Parse(entradaInfo[0])-1;
    var columna = int.Parse(entradaInfo[1])-1;
    if (fila<=sala.GetLength(0)&&fila>sala.GetLength(0)|| columna<=sala.GetLength(1)&&columna>sala.GetLength(1))
    {
        WriteLine("No se ha encontrado la butaca introducida, por favor intentelo de nuevo");
    }
    else
    {
        sala[fila,columna]=(int)EstadoButaca.Libre;
        numeroVenta -= 1;
    }
}

double CalcularGanancias(int numeroventa, double precio)
{
    double ganancias = numeroventa * precio;
    return ganancias;
}

void Informe(int[,] sala, int ventas)
{
    double butacasOcupadas = 0.0;
    double butacasFueraServicio = 0.0; 
    double butacasLibres = 0.0;
    var recaudacion=CalcularGanancias(ventas, PRECIO_ENTRADA);
    for (var i = 0; i < sala.GetLength(0); i++)
    {
        for (var j = 0; j < sala.GetLength(1); j++)
        {
            switch (salaCine[i, j])
            {
                case (int)EstadoButaca.Ocupada: butacasOcupadas++; break;
                case (int)EstadoButaca.FueraServicio: butacasFueraServicio++; break;
                case (int)EstadoButaca.Libre:  butacasLibres++; break;
            }
        }
    }
    var total = butacasOcupadas+butacasFueraServicio+butacasLibres;
    double ocupacion = (butacasOcupadas/total)*100;
    WriteLine("--- INFORME CINEDAW ---");
    WriteLine("-----------------------");
    WriteLine($"Total de entradas vendidas: {ventas}");
    WriteLine($"Porcentaje de ocupacion: {ocupacion}%");
    WriteLine($"Recaudacion total: {recaudacion}€");
}

enum OpcionesMenu
{
VerSala = 1,
ComprarEntrada ,
DevolverEntrada ,
Recaudacion ,
Informe ,
Salir 
}

enum EstadoButaca
{
    Libre,Ocupada,FueraServicio
}
