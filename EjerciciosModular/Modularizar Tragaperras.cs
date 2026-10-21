// ============================================================================
// ENUNCIADO — Máquina Tragaperras (versión ESTRUCTURADA)
//
// Simula una máquina tragaperras de consola resuelta con las tres
// estructuras de control de la programación estructurada:
//   - SECUENCIA: se pide el saldo, se juega, se muestra el resultado.
//   - CONDICIONALES: validar saldo y apuestas, y decidir el premio (if-else
//     y switch para el menú).
//   - BUCLES: do-while para repetir hasta que un dato sea válido y para
//     mantener vivo el menú.
//
// Reglas del juego:
//   1. El saldo inicial debe ser como mínimo 0.50€ (se repite la petición si no).
//   2. Menú con tres opciones: 1) Tirar, 2) Añadir saldo, 3) Salir.
//   3. En la tirada se apuesta una cantidad (mínimo 0.50€ y nunca más del
//      saldo disponible), se generan TRES números aleatorios entre 0 y 9 y:
//        - Tres números iguales -> ganas el TRIPLE de lo apostado.
//        - Dos números iguales  -> se te devuelve lo apostado (x1).
//        - Ninguno igual        -> pierdes la apuesta.
//   4. Se puede añadir saldo en cualquier momento (nunca cantidades negativas).
//   5. El juego termina al elegir "Salir" o cuando el saldo baja de 0.50€.
//
// Ejecutar: dotnet run 01-estructurada-tragaperras.cs
// ============================================================================

decimal saldo;
decimal saldoApostado;
decimal saldoAnadido;
int opcionMenu = 0;
int numeroAleatorio1, numeroAleatorio2, numeroAleatorio3;
Random rnd = new();

Console.WriteLine("Simulación de Máquina Tragaperras");
SolicitarSaldo();
do
{
    opcionMenu = MostrarMenu();
    EjecutarOpcion(opcionMenu);
} while (opcionMenu != (int)OpcionesMenu.Salir && saldo>=0.5m);
Console.WriteLine($"Fin del programa. Saldo final: {saldo}€");

// 1. Solicitar saldo inicial
// do-while: el saldo es imprescindible, así que lo pedimos AL MENOS UNA VEZ
//Solo salimos cuando saldo es valido
void SolicitarSaldo()
{
    bool isSaldoInvalido;
    do
    {
        Console.Write("Introduce el saldo inicial (mínimo 0.50€): ");
        decimal.TryParse(Console.ReadLine(), out saldo); // Si escriben texto, saldo = 0 -> se repetirá

        if (saldo < 0.50m)
        {
            Console.WriteLine("Saldo inferior a 0.50€. Por favor, introduzca un valor superior o igual.");
            isSaldoInvalido = true;
        }
        else
        {
            Console.WriteLine($"Saldo inicial: {saldo}€");
            isSaldoInvalido = false;
        }
    } while (isSaldoInvalido);
}
// Funcion para mostrar el menu utilizando el Enum de opciones en el mensaje
// Solo sale del bucle cuando el usuario introduce un dato parseable a int
// Devuelve el numero seleccionado
int MostrarMenu()
{
    do
    {
        Console.WriteLine("-----------------------------------------------------------");
        Console.WriteLine($"Saldo actual: {saldo}€");
        Console.WriteLine("Introduzca el número de la opción que desee:");
        Console.WriteLine($" {(int)OpcionesMenu.HacerTirada}. Tirar de la tragaperras.");
        Console.WriteLine($"{(int)OpcionesMenu.AñadirSaldo}. Añadir saldo.");
        Console.WriteLine($"{(int)OpcionesMenu.Salir}. Salir.");
    } while ( !int.TryParse(Console.ReadLine(), out opcionMenu));
    return opcionMenu;
}
//Compara la variable seleccionada (opcionMenu) usando un switch 
// Y ejecuta su funcion correspondiente
 void EjecutarOpcion(int opcionElegida)
{
    switch(opcionElegida)
    {
        case 1: HacerTirada(); break;
        case 2: AñadirSaldo(); break;
        case 3: Console.WriteLine("Ha sido un placer. ¡Vuelva pronto!"); break;
        default: Console.WriteLine("Opción no válida. Por favor, elija 1, 2 o 3."); break;
    };
}


void HacerTirada()
{
     do
     {
         Console.Write("¿Cuánto dinero quiere apostar? (Mínimo 0.50€): ");
         decimal.TryParse(Console.ReadLine(), out saldoApostado);

         // La apuesta debe estar entre el mínimo y el saldo disponible
         if (saldoApostado < 0.50m || saldoApostado > saldo)
         {
             Console.WriteLine("Entrada inválida. Apuesta mínima 0.50€ y máxima su saldo.");
         }
         else
         {
             saldo -= saldoApostado; // Primero se DESCUENTA la apuesta..
             numeroAleatorio1 = rnd.Next(0, 10);
             numeroAleatorio2 = rnd.Next(0, 10);
             numeroAleatorio3 = rnd.Next(0, 10);
             Console.WriteLine($"Máquina: {numeroAleatorio1} | {numeroAleatorio2} | {numeroAleatorio3}");
             // ...y después se suma el premio según el resultado
             if (numeroAleatorio1 == numeroAleatorio2 && numeroAleatorio2 == numeroAleatorio3)
             {
                 saldo +=  saldoApostado*3; // Tres en raya: x3
                 Console.WriteLine("¡ENHORABUENA! TRES EN RAYA. Ganas el triple.");
                 Console.WriteLine($"Saldo total: {saldo}€");
             }
             else if (
                 (numeroAleatorio1 == numeroAleatorio2 && numeroAleatorio2 != numeroAleatorio3) ||
                 (numeroAleatorio1 == numeroAleatorio3 && numeroAleatorio1 != numeroAleatorio2) ||
                 (numeroAleatorio2 == numeroAleatorio3 && numeroAleatorio2 != numeroAleatorio1))
             {
                 saldo += saldoApostado; // Dos iguales: se devuelve la apuesta (x1)
                 Console.WriteLine("Dinero apostado devuelto (x1).");
                 Console.WriteLine($"Tu saldo es de: {saldo}€");
             }
             else
             {
                 Console.WriteLine("Más suerte la próxima."); // Ninguno igual: se pierde la apuesta
                 Console.WriteLine($"Tu saldo es de: {saldo}€");
             }
         }
     } while (saldoApostado < 0.50m || saldoApostado > saldo);
}
void AñadirSaldo()
{
    bool isSaldoAnadidoNegativo;
     do
     {
         Console.Write("¿Cuánto quieres añadir?: ");
         decimal.TryParse(Console.ReadLine(), out saldoAnadido);
         if (saldoAnadido < 0.0m)
         {
             Console.WriteLine("No se acepta dinero negativo.");
             isSaldoAnadidoNegativo = true;
         }
         else
         {
             saldo += saldoAnadido;
             Console.WriteLine($"Saldo final: {saldo}€");
             isSaldoAnadidoNegativo = false;
         }
     } while (isSaldoAnadidoNegativo);
}    
enum OpcionesMenu
{
 HacerTirada= 1,
 AñadirSaldo, 
 Salir   
}