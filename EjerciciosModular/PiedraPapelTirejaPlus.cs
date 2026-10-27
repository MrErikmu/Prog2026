
using static System.Console;
using System.Text;
OutputEncoding = Encoding.UTF8;
const string nombreJefe = "Wamuu El Huracan";
const int victoriasRequeridas = 5; 
int opcionMenu;
var victoriasJugador = 0;
Tirada tiradajugador; 
Random rnd = new Random();

WriteLine("Bienvenido aspirante al torneo de los Dark Alley Warriors... DAW para abreviar");
WriteLine("¿Seras capaz de conseguir esos preciados Doblones de Tymora 🪙 o te iras con las manos vacias?");
var nombre = PedirNombre();
do
{
    opcionMenu = MostrarMenu();
    EjecutarOpcion(opcionMenu);
} while (opcionMenu != (int)OpcionesMenu.Retirarte);
string PedirNombre()
{
    bool isnombreok; 
    do
    {
        WriteLine("Dime tu nombre aspirante");
        nombre = ReadLine();
        if (nombre == null || nombre.Length <= 1)
        {
            WriteLine("Mi paciencia tiene limites... no me tientes muchacho");
            isnombreok = false;
        }
        else
        { 
            isnombreok = true;
        }
    } while (!isnombreok);
    return nombre!;
}
void EjecutarOpcion(int numeroopcion)
{
    switch(numeroopcion)
    {
        case (int)OpcionesMenu.CombateNormal: CombateNormal(); break;
        case (int)OpcionesMenu.FinalBoss: CombateBoss(); break;
        case (int)OpcionesMenu.RevisarProbabilidades: RevisarProb(); break;
        case (int)OpcionesMenu.Retirarte: RevisarProb(); break;
        default: Console.WriteLine($"No me hagas perder el tiempo {nombre}. Habla que vas a hacer"); break;
    }
}
void CombateNormal()
{
    var puntuacion = 0;
    var puntuacionrival = 0;  
    Resultado resultadotirada;
    WriteLine("El combate se decide al mejor de 3 que Tymora os sonria");
    do
    {
        var manojugador = Seleccionarmano();
        var tiradarival= SacarTiradaAleatoria();
        resultadotirada = DecidirResultado(manojugador, tiradarival);
        switch(resultadotirada)
        {
            case Resultado.Derrota: puntuacionrival++; break;
            case Resultado.Victoria: puntuacion++; break;
            case Resultado.Empate: break;
        }
    } while (puntuacion != 2 && puntuacionrival!=2);
    if (Isawin(puntuacion, puntuacionrival))
    {
        WriteLine($" Enhorabuena {nombre} has ganado esta batalla te añadiremos un doblon de Tymora 🪙");
        victoriasJugador++; 
    }
    else
    {
        WriteLine($" Lastima {nombre} has perdido y por lo tanto me quedare 1 de tus doblones de Tymora 🪙");
        if(victoriasJugador>=0)
            victoriasJugador--;  
    }
}
void RevisarProb()
{
    if (victoriasJugador >= victoriasRequeridas)
    {
        MostrarProbabilidadesCompletas();
    }
    else 
     WriteLine($"Tus probabilidades de Victoria combatiendo contra los secuaces de {nombreJefe} son de un 26%");
}
bool Isawin(int puntosjugador, int puntosrival)
{
    //me elimina el operador ternario porque es redundante ya esta comparando.
    return (puntosjugador>puntosrival); 
}
void CombateBoss()
{
    var puntuacion = 0;
    var puntuacionrival = 0;
    if (victoriasJugador < victoriasRequeridas)
    {
        WriteLine("HAHAHAHAHA");
        WriteLine("No seas patetico y vuelve cuando seas digno");
        WriteLine($"Tras obtener {victoriasRequeridas} doblones de Tymora que portan mis  secuaces, insecto");
    }
    else
    {
        WriteLine($"Te felicito por llegar hasta aqui {nombre} ahora vamos a divertirnos");
        WriteLine("El combate se decide al mejor de 5 que Tymora te sonria lo vas a necesitar");
        do
        {
            var hacertrampa= rnd.Next(0, 101);
            var manojugador = Seleccionarmano();
            Resultado resultadotirada ;
            if (hacertrampa <= 66)
            {
                resultadotirada = Resultado.Derrota;
            }
            else
            {
                resultadotirada = DecidirResultado(manojugador, SacarTiradaAleatoria());
            }
            switch(resultadotirada)
            {
                case Resultado.Derrota: puntuacionrival++; break;
                case Resultado.Victoria: puntuacion++; break;
                case Resultado.Empate: break;
            }
        } while (puntuacion != 3 && puntuacionrival!=3 );

        if (Isawin(puntuacion, puntuacionrival))
        {
            WriteLine($" Enhorabuena {nombre} has ganado esta batalla te añadiremos un doblon de Tymora 🪙");
            victoriasJugador++; 
        }
        else
        {
            WriteLine($" Lastima {nombre} no estuviste a la altura del gran {nombreJefe} me quedare 2  doblones de Tymora 🪙");
            if(victoriasJugador>=2)
             victoriasJugador-=2;  
        }
    }
}
void MostrarProbabilidadesCompletas()
{
  WriteLine($"Tus probabilidades de Victoria combatiendo contra los secuaces de {nombreJefe} son de un 26%");
  WriteLine($"Ahora bien contra {nombreJefe} la cosa cambia:");
  WriteLine($"Las malas lenguas dicen que tiene un trato con la mismisima Tymora");
  WriteLine($"Tus probabilidades de Victoria calculo que serian de un 21% ");
}
int MostrarMenu()
{
    do
    {
        WriteLine($"Aspirante {nombre} elige tu destino");
        WriteLine($"Introduzca el número de la opción que desee:");
        WriteLine($" {(int)OpcionesMenu.CombateNormal}: Enfrentamiento contra los secuaces de {nombreJefe}. 🗡️");
        WriteLine($"{(int)OpcionesMenu.FinalBoss}. Retar al mismisimo {nombreJefe} ⚔️.");
        WriteLine($"{(int)OpcionesMenu.RevisarProbabilidades}. Evaluar tu destino. ℹ️");
        WriteLine($"{(int)OpcionesMenu.Retirarte}. Retirarte... Mientras puedas. 🏃‍♂️‍➡️");
    } while ( !int.TryParse(Console.ReadLine(), out opcionMenu));
    return opcionMenu;
}
Tirada Seleccionarmano()
{
    do
    {
        WriteLine($"Aspirante {nombre} elige tu mano ");
        WriteLine($" {(int)Tirada.Piedra}: La robusta Piedra 🪨 .");
        WriteLine($"{(int)Tirada.Papel}: El flexible Papel ⬜  .");
        WriteLine($"{(int)Tirada.Tijeras}: La afilada Tijera ✂️ ");
    } while ( !Enum.TryParse(ReadLine(), out tiradajugador));
    return tiradajugador;
}
Tirada SacarTiradaAleatoria()
{
    var tiradaaleatoria= rnd.Next(1, 4);
    return tiradaaleatoria switch
    {
        1 => Tirada.Piedra,
        2 => Tirada.Papel,
        3 => Tirada.Tijeras,
        _ => (Tirada)(-1)
    };
}
Resultado DecidirResultado(Tirada tiradadeljugador, Tirada tiradarival)
{
    WriteLine($"{nombre} has sacado: {tiradadeljugador} y tu adversario: {tiradarival}");
    if (tiradadeljugador == tiradarival)
    {
        WriteLine("Empate");
        return Resultado.Empate;
    }

    if (tiradadeljugador == Tirada.Piedra && tiradarival == Tirada.Tijeras
        || tiradadeljugador==Tirada.Papel&& tiradarival== Tirada.Piedra
        || tiradadeljugador==Tirada.Tijeras&& tiradarival== Tirada.Papel)
    {
        WriteLine($"Victoria para {nombre}"); 
        return Resultado.Victoria;
    }
    WriteLine($"{nombre} has perdido");
    return Resultado.Derrota;
}
enum OpcionesMenu{
CombateNormal=1,
FinalBoss,
RevisarProbabilidades,
Retirarte
}
enum Tirada{
Piedra=1,
Papel,
Tijeras
}
enum Resultado
{
    Victoria,
    Derrota,
    Empate, 
}
