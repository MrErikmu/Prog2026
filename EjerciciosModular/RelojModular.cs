using static System.Console; 
WriteLine("Bienvenido al reloj modular");
ConsoleKeyInfo tecla;
var formato=0;
(int hora, int minuto, int segundos) horainicial = (0, 0, 0); 
(int hora, int minuto, int segundos) horafinal = (0, 0, 0); 
(int hora, int minuto, int segundos, string formato) horainicialanalog = (0, 0, 0, ""); 
(int hora, int minuto, int segundos, string formato) horafinalanalog = (0, 0, 0, "");
do
{ 
    WriteLine("Pulsa 1 para iniciar el reloj en formato 24h");
    WriteLine("Pulsa 2 para iniciar el reloj en formato am/pm");
    tecla= ReadKey();
    Clear();
    if (tecla.KeyChar == '1')
        formato = 1;
    if (tecla.KeyChar == '2')
        formato = 2; 
}while(formato != 1 && formato!= 2 );

bool ishoraok;
    if (formato == 1)
    {
        do
        {
            WriteLine("Solicitando hora inicial....");
            horainicial = ObtenerHoraDigital();
            WriteLine("Solicitando hora final....");
            horafinal = ObtenerHoraDigital();
            if (horafinal.hora < horainicial.hora)
            {
                WriteLine("La hora final no puede ser menor que la inicial");
                ishoraok = false;
                WriteLine($"\rHora inicial: {horainicial.hora:D2}:{horainicial.minuto:D2}:{horainicial.segundos:D2}");
                WriteLine($"\rHora final: {horafinal.hora:D2}:{horafinal.minuto:D2}:{horafinal.segundos:D2}");
            }
            else
            {
                ishoraok = true;
            }
        } while (!ishoraok);
    }
    if(formato==2)
    {
        do
        {
        WriteLine("Solicitando hora inicial....");
        horainicialanalog = ObtenerHoraAm();
        WriteLine("Solicitando hora final....");
        horafinalanalog = ObtenerHoraAm(); 
        
        if (horafinalanalog.hora < horainicialanalog.hora)
        {
            WriteLine("La hora final no puede ser menor que la inicial");
            ishoraok = false;
            WriteLine($"\rHora inicial: {horainicialanalog.hora:D2}:{horainicialanalog.minuto:D2}:{horainicialanalog.segundos:D2} {horainicialanalog.formato}");
            WriteLine($"\rHora final: {horafinalanalog.hora:D2}:{horafinalanalog.minuto:D2}:{horafinalanalog.segundos:D2} {horafinalanalog.formato}");
        }
        else
        {
            ishoraok = true;
        }
        } while (!ishoraok);
    }
do
{
    Console.WriteLine("Pulsa Enter para arrancar el reloj...");
    tecla = Console.ReadKey();
} while (tecla.Key != ConsoleKey.Enter);

switch (formato)
{
    case 1: IniciarRelojDigital(horainicial,horafinal); break;
    case 2: IniciarRelojAm(horainicialanalog,horafinalanalog); break;
    //default: WriteLine("Error desconocido"); break;
}


void IniciarRelojAm(
    (int hora, int minuto, int segundos, string formato) horainicialam, 
    (int hora, int minuto, int segundos, string formato) horafinalam)
{
    bool isfinish = false;
    do
    {
        Write($"\rHora actual: {horainicialam.hora:D2}:{horainicialam.minuto:D2}:{horainicialam.segundos:D2} {horainicialam.formato}");
        Thread.Sleep(1000);
        horainicialam.segundos ++;
        if (horainicialam.segundos == 60)
        {
            horainicialam.minuto++;
            horainicialam.segundos = 0;
        }

        if (horainicialam.minuto==60)
        {
            horainicialam.hora++;
            horainicialam.minuto = 0; 
        }
        if (horainicialam.formato.ToLower() == "am" && horainicialam.hora < 12)
        {
            horainicialam.formato = "pm";
            horainicialam.hora = 1; 
        }
        else
        {
            horainicialam.formato = "am";
            horainicialam.hora = 1; 
        }
        if (horainicialam == horafinalam)
        {
            isfinish = true;
        }
    } while (!isfinish); 
    WriteLine("");
    WriteLine("Fin");
    WriteLine("___");
}
void IniciarRelojDigital(
    (int hora, int minuto, int segundos) horainicialDigital, 
    (int hora, int minuto, int segundos) horafinalDigital)
{
    bool isfinish = false;
    do
    {
        Console.Write($"\rHora actual: {horainicialDigital.hora:D2}:{horainicialDigital.minuto:D2}:{horainicialDigital.segundos:D2}");
        Thread.Sleep(1000);
        horainicialDigital.segundos ++;
        if (horainicialDigital.segundos == 60)
        {
            horainicialDigital.minuto++;
            horainicialDigital.segundos = 0;
        }

        if (horainicialDigital.minuto==60)
        {
            horainicialDigital.hora++;
            horainicialDigital.minuto = 0; 
        }
        if (horainicialDigital == horafinalDigital)
        {
            isfinish = true;
        }
    } while (!isfinish); 
    WriteLine("");
    WriteLine("Fin");
    WriteLine("___");
}

(int hora, int minuto, int segundo, string formato) ObtenerHoraAm()
{
    int horaam;
    int minutosam;
    int segundosam;
    string input;
    string periodo;
    var periodook = false;
    do
    {
        WriteLine("Introduzca la hora separada en hora, minuto y segundos y periodo");
        WriteLine("Hora: ");
        input = ReadLine()!;
        if (!int.TryParse(input, out horaam) || horaam < 1 || horaam > 12)
        {
            WriteLine("Formato incorrecto la hora introducida debe estar entre 1 y 12 ");
            horaam = -1;
        }
    } while (horaam < 1 || horaam > 12);

    do
    {
        WriteLine("minutos: ");
        input = ReadLine()!;
        if (!int.TryParse(input, out minutosam) || minutosam < 0 || minutosam > 59)
        {
            WriteLine("Formato incorrecto los munutos introducidos deben estar entre 0 y 59 ");
            minutosam = -1;
        }
    } while (minutosam < 0 || minutosam > 59);

    do
    {
        WriteLine("Segundos: ");
        input = ReadLine()!;
        if (!int.TryParse(input, out segundosam) || segundosam < 0 || segundosam > 59)
        {
            WriteLine("Formato incorrecto los segundos introducidos deben estar entre 0 y 59 ");
            segundosam = -1;
        }
    } while (segundosam < 0 || segundosam > 59);

    do
    {
        WriteLine("Periodo: ");
        periodo = ReadLine()!.ToLower();
        if (periodo == "am" || periodo == "pm")
        {
            periodook = true;
        }
    } while (!periodook);
    Clear();
    return (horaam, minutosam, segundosam, periodo);
}

(int hora , int minuto , int segundo) ObtenerHoraDigital(){
    string input;
    int horas = 0;
    int minutos = 0;
    int segundos = 0;
    do
    {
        WriteLine("Introduzca la hora de inicio separada en hora, minuto y segundos");
        WriteLine("Hora: ");
         input = ReadLine()!;
         if (!int.TryParse(input, out horas) || horas < 0 || horas > 23)
         {
             WriteLine("Formato incorrecto la hora introducida debe estar entre 0 y 23 ");
             horas = -1;
         }
    } while (horas < 0 || horas > 23);
    Clear();
    do
    {
        WriteLine("minutos: ");
        input = ReadLine()!;
        if (!int.TryParse(input, out minutos) || minutos < 0 || minutos > 59)
        {
            WriteLine("Formato incorrecto los munutos introducidos deben estar entre 0 y 59 ");
            minutos = -1;
        }
    } while (minutos < 0 || minutos > 59);
    Clear();
    do
    {
        WriteLine("Segundos: ");
        input = ReadLine();
        if (!int.TryParse(input, out segundos) || segundos < 0 || segundos > 59)
        {
            WriteLine("Formato incorrecto los segundos introducidos deben estar entre 0 y 59 ");
            segundos = -1;
        }
    } while (segundos<0|| segundos>59);
    Clear();
    return (horas, minutos, segundos); 
}
