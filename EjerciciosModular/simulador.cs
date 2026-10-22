using static System.Console;
Title= "Simulador de Vuelo"; 
WriteLine("Bienvenido al Simulador de Vuelo ");
WriteLine("_________________________________");
const double VARIACIONALTURA= 1.0;
const double VARIACIONVELOCIDAD= 1.0;
const double PROBABILIDADFALLO= 0.1;
const double INTERVALOALTURA= 0.25;
const double INTERVALOVELOCIDAD= 0.15;
const string MENSAJEDEERROR= "Se ha registrado un fallo del sistema, volviendo a control manual ...";
const string ERRORCRITICO= "ERROR CRITICO DEL SISTEMA. Volviendo a control manual...";
Random aleatorio = new();
int tiemposimulacion= 0; 
double velocidadInicial= 800.0;
double alturaInicial= 1000.0;
double velocidad= 0; 
double altura= 0; 
double aumentarAltitud(double alturaActual)
{
    return alturaActual-= VARIACIONALTURA;
}
double disminuirAltitud(double alturaActual)
{
    return alturaActual-= VARIACIONALTURA;
}
double aumentarVelocidad(double velocidadActual)
{
    return velocidadActual+= VARIACIONVELOCIDAD;
}
double disminuirVelocidad(double velocidadActual)
{
    return velocidadActual-= VARIACIONVELOCIDAD;
}
void Esperar()
{
    Thread.Sleep(1000);
}
void comprobarFallo()
{
    if (aleatorio.Next(0.0, 1.0)==PROBABILIDADFALLO)
    {
        throw new SystemException(ERRORCRITICO);
    }
}
double leerParametro()
{
    double param=0.0;
    int numintentos=0;
    bool lecturaCorrecta= false; 
    do
    {
        WriteLine("Introduzca el valor:");
       
        if(!double.TryParse(ReadLine(), out param))
        {
                WriteLine($"El valor introducido no es valido, debe estar entre {}");
                WriteLine($"Intentos realizados: {numintentos}");
                numintentos++;
        }
    
    } while (!lecturaCorrecta|| numintentos==10);
    if (numintentos == 10)
    {
        WriteLine($"{MENSAJEDEERROR}");
    }
    return param
}