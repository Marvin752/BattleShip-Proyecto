//Variables globales0

// Definición del tablero y los buques=========================
int[,] tablero = new int[20, 20];
//Esta dice cuanto espacio en el tablero ocupara cada nave
int[] espacionaves = { 2, 3, 4 };
//Esta dice cuantas de cada nave va a haber
int[] naves = { 4, 6, 8 };
//Esta casi no se usa hasta el final pero era parte de la prueba y error del incio
string[] nombresNaves = { "Barcos", "Navíos", "Portaaviones" };
int op = 1;
//====================================================================================================================================================================

//Funciones

// Función para colocar los buques=========================
static void colocarBuques(int[,] enemigos, int[] tamanosNaves, string[] nombresNaves, int[] naves)
{
    // Generador de números aleatorios
    Random aleatorio = new Random();

    // Inicializar el tablero con valores vacíos
    for (int x = 0; x < enemigos.GetLength(0); x++)
    {
        for (int y = 0; y < enemigos.GetLength(1); y++)
        {
            enemigos[x, y] = 0;
        }
    }
    // Para cada tipo de nave
    for (int i = 0; i < tamanosNaves.Length; i++)
    {
        int cantidadNaves = tamanosNaves[i];
        string nombreNave = nombresNaves[i];

        // Ajustar la cantidad de barcos según el tipo de nave
        switch (i)
        {
            case 0:
                cantidadNaves = naves[i]; // 4 barcos de 2 posiciones
                break;
            case 1:
                cantidadNaves = naves[i]; // 6 barcos de 3 posiciones
                break;
            case 2:
                cantidadNaves = naves[i]; // 8 barcos de 4 posiciones
                break;
            default:
                break;
        }

        // Generar la cantidad indicada de naves
        for (int j = 0; j < cantidadNaves; j++)
        {
            int filaInicial, columnaInicial, direccion;
            // Bucle para generar una posición válida para la nave
            bool posicionValida;
            do
            {
                posicionValida = true; // Suponemos que la posición es válida hasta que se demuestre lo contrario
                filaInicial = aleatorio.Next(0, enemigos.GetLength(0));
                columnaInicial = aleatorio.Next(0, enemigos.GetLength(1));
                direccion = aleatorio.Next(2);
                // Verificar si la posición se sale de los límites del tablero
                if ((direccion == 0 && columnaInicial + tamanosNaves[i] > enemigos.GetLength(1)) ||
                    (direccion == 1 && filaInicial + tamanosNaves[i] > enemigos.GetLength(0)))
                {
                    posicionValida = false;
                    continue; // Reintentar generando una nueva posición
                }
                // Verificar si la posición está ocupada por otra nave
                for (int k = 0; k < tamanosNaves[i]; k++)
                {
                    if (direccion == 0 && enemigos[filaInicial, columnaInicial + k] != 0 ||
                        direccion == 1 && enemigos[filaInicial + k, columnaInicial] != 0)
                    {
                        posicionValida = false;
                        break; // Reintentar generando una nueva posición
                    }
                }
            } while (!posicionValida);

            // Funcion para colocar la nave en el tablero
            for (int k = 0; k < tamanosNaves[i]; k++)
            {
                if (direccion == 0)
                {
                    enemigos[filaInicial, columnaInicial + k] = i + 1;
                }
                else
                {
                    enemigos[filaInicial + k, columnaInicial] = i + 1;
                }
            }
        }
    }
}

//Funcion creada para ver el correcto funcionamento del juego=========================
static void mostrarTableros(int[,] tablero)
{
    Console.WriteLine("Generación de Barcos:");
    Console.WriteLine("====================\n");
    // Imprimir encabezado de columnas
    Console.Write("   ");
    for (int col = 1; col <= tablero.GetLength(1); col++)
    {
        Console.Write($"{col,3}");
    }
    Console.WriteLine();
    Console.WriteLine();
    // Recorrer el tablero y mostrar cada posición con índices de fila y columna
    for (int fila = 0; fila < tablero.GetLength(0); fila++)
    {
        Console.Write($"{fila + 1,3}");
        for (int col = 0; col < tablero.GetLength(1); col++)
        {
            Console.Write($"{tablero[fila, col],3}");
        }
        Console.WriteLine();
    }
}

//Funcion usada para el juego=========================
static void mostrarTablero(int[,] mapas)
{
    // Imprimir encabezado de columnas
    Console.Write("   ");
    for (int col = 1; col <= mapas.GetLength(1); col++)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{col,3}");
        Console.ResetColor();
    }
    Console.WriteLine();
    // Recorrer el tablero y mostrar cada posición con índices de fila y columna
    for (int fila = 1; fila <= mapas.GetLength(0); fila++)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{fila,3}");
        Console.ResetColor();
        for (int col = 1; col <= mapas.GetLength(1); col++)
        {
            if (mapas[fila - 1, col - 1] == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(string.Format("{0,3}", "-"));
                Console.ResetColor();
            }
            else if (mapas[fila - 1, col - 1] < 4)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(string.Format("{0,3}", "-"));
                Console.ResetColor();
            }
            else if (mapas[fila - 1, col - 1] == 7)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(string.Format("{0,3}", "X"));
                Console.ResetColor();
            }
            else if (mapas[fila - 1, col - 1] == 5)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(string.Format("{0,3}", "O"));
                Console.ResetColor();
            }
        }
        Console.WriteLine();
    }
}

// Funcion del mero juego=========================
static int GOTY(int[,] tablero, int[] espacionaves, string[] nombresNaves, int op, int[] naves)
{
    int continuar = 0;
    do
    {
        colocarBuques(tablero, espacionaves, nombresNaves, naves);
        int contador = 0;
        int pegue = 0;
        int aciertos = (espacionaves[0] * naves[0]) + (espacionaves[1] * naves[1]) + (espacionaves[2] * naves[2]);
        int puntos = 1500;
        do
        {
            Console.WriteLine("Información Enemgia:");
            Console.WriteLine("{0}: {1}", nombresNaves[2], naves[2]);
            Console.WriteLine("{0}: {1}", nombresNaves[1], naves[1]);
            Console.WriteLine("{0}: {1}", nombresNaves[0], naves[0]);
            Console.WriteLine("====================");
            Console.WriteLine("Debe acertar un total de {0} disparos", aciertos);
            Console.WriteLine("Disparos: " + contador);
            Console.WriteLine("Aciertos: " + pegue);
            Console.WriteLine("Puntos de Disparo: " + puntos);
            Console.WriteLine();
            if (op == 777)
            {
                mostrarTableros(tablero);
            }
            else
            {
                mostrarTablero(tablero);
            }
            Console.WriteLine();
            try
            {
                Console.WriteLine("\nElija la posicion a atacar Capitán (fila, columna)\n");
                Console.Write("> ");
                string[] posicion = Console.ReadLine().Split(',');
                int fila = int.Parse(posicion[0]) - 1;
                int columna = int.Parse(posicion[1]) - 1;
                if (tablero[fila, columna] == 1 || tablero[fila, columna] == 2 || tablero[fila, columna] == 3)
                {
                    tablero[fila, columna] = 7;
                    Console.Beep();
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("¡Acertaste Capitán! +75 Pts\n");
                    Console.ResetColor();
                    puntos += 75;
                    pegue++;
                    contador++;
                    aciertos--;
                }
                else if (tablero[fila, columna] == 0)
                {
                    tablero[fila, columna] = 5;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Fallaste Capitán -25 Pts\n");
                    Console.ResetColor();
                    puntos -= 25;
                    contador++;
                }
                else if (tablero[fila, columna] == 7 || tablero[fila, columna] == 5)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("¡Ya habias disparado a esa posición Capitán! -125 Pts\n");
                    Console.ResetColor();
                    puntos -= 125;
                    contador++;
                }
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("La ubicación seleccionada no es valida\n");
                Console.WriteLine("Presione \"ENTER\" para continuar\n");
                Console.Write("> ");
                Console.ReadKey();
            }
        } while (aciertos != 0 && puntos > 0);
        if (aciertos != 0 && puntos <= 0)
        {
            do
            {
                try
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Lo sentimos capitán te quedaste sin Puntos de Disparo :C\n");
                    Console.ResetColor();
                    Console.WriteLine("Disparaste un total de: {0} veces", contador);
                    Console.WriteLine("Acertaste un total de: {0} veces", pegue);
                    Console.WriteLine("¿Qué desea hacer capitán?\n");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("1) Volver a jugar :3");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("2) Regresar al menú anterior :D");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("0) Salir :c\n");
                    Console.ResetColor();
                    Console.Write("> ");
                    continuar = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    if (continuar != 0 && continuar != 1 && continuar != 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Opción no valida\n");
                        Console.WriteLine("Presione \"ENTER\" para continuar\n");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("El caracter ingresado no es valido\n");
                    Console.WriteLine("Presione \"ENTER\" para continuar\n");
                    Console.Write("> ");
                    Console.ReadKey();
                    Console.Clear();
                    continuar = 3;
                }
            } while (continuar != 0 && continuar != 1 && continuar != 2);
        }
        else if (puntos >= 3000)
        {
            do
            {
                try
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Arrasaste con ellos, ahora todos te temen en el mar... Hasta tus compañeros\n");
                    Console.ResetColor();
                    Console.WriteLine("Obtuviste un total de {0} Pts", puntos);
                    Console.WriteLine("Disparaste un total de: {0} veces", contador);
                    Console.WriteLine("Acertaste un total de: {0} veces", pegue);
                    Console.WriteLine("¿Qué desea hacer capitán?\n");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("1) Volver a jugar :3");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("2) Regresar al menú anterior :D");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("0) Salir :c\n");
                    Console.ResetColor();
                    Console.Write("> ");
                    continuar = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    if (continuar != 0 && continuar != 1 && continuar != 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Opción no valida\n");
                        Console.WriteLine("Presione \"ENTER\" para continuar\n");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("El caracter ingresado no es valido\n");
                    Console.WriteLine("Presione \"ENTER\" para continuar\n");
                    Console.Write("> ");
                    Console.ReadKey();
                    Console.Clear();
                    continuar = 3;
                }
            } while (continuar != 0 && continuar != 1 && continuar != 2);
        }
        else if (puntos >= 2000 && puntos <= 2999)
        {
            do
            {
                try
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("¡Impresionante! tus rivales nunca tuvieron oportunidad :3\n");
                    Console.ResetColor();
                    Console.WriteLine("Obtuviste un total de {0} Pts", puntos);
                    Console.WriteLine("Disparaste un total de: {0} veces", contador);
                    Console.WriteLine("Acertaste un total de: {0} veces", pegue);
                    Console.WriteLine("¿Qué desea hacer capitán?\n");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("1) Volver a jugar :3");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("2) Regresar al menú anterior :D");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("0) Salir :c\n");
                    Console.ResetColor();
                    Console.Write("> ");
                    continuar = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    if (continuar != 0 && continuar != 1 && continuar != 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Opción no valida\n");
                        Console.WriteLine("Presione \"ENTER\" para continuar\n");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("El caracter ingresado no es valido\n");
                    Console.WriteLine("Presione \"ENTER\" para continuar\n");
                    Console.Write("> ");
                    Console.ReadKey();
                    Console.Clear();
                    continuar = 3;
                }
            } while (continuar != 0 && continuar != 1 && continuar != 2);
        }
        else if (puntos >= 1000 && puntos <= 1999)
        {
            do
            {
                try
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("¡Felicidaes Capitán! Hundiste a toda las fuerzas enemigas! :D\n");
                    Console.ResetColor();
                    Console.WriteLine("Obtuviste un total de {0} Pts", puntos);
                    Console.WriteLine("Disparaste un total de: {0} veces", contador);
                    Console.WriteLine("Acertaste un total de: {0} veces", pegue);
                    Console.WriteLine("¿Qué desea hacer capitán?\n");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("1) Volver a jugar :3");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("2) Regresar al menú anterior :D");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("0) Salir :c\n");
                    Console.ResetColor();
                    Console.Write("> ");
                    continuar = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    if (continuar != 0 && continuar != 1 && continuar != 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Opción no valida\n");
                        Console.WriteLine("Presione \"ENTER\" para continuar\n");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("El caracter ingresado no es valido\n");
                    Console.WriteLine("Presione \"ENTER\" para continuar\n");
                    Console.Write("> ");
                    Console.ReadKey();
                    Console.Clear();
                    continuar = 3;
                }
            } while (continuar != 0 && continuar != 1 && continuar != 2);
        }
        else if (puntos > 0 && puntos <= 999)
        {
            do
            {
                try
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Por poco... Pero salimos con vida :)\n");
                    Console.ResetColor();
                    Console.WriteLine("Obtuviste un total de {0} Pts", puntos);
                    Console.WriteLine("Disparaste un total de: {0} veces", contador);
                    Console.WriteLine("Acertaste un total de: {0} veces", pegue);
                    Console.WriteLine("¿Qué desea hacer capitán?\n");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("1) Volver a jugar :3");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("2) Regresar al menú anterior :D");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("0) Salir :c\n");
                    Console.ResetColor();
                    Console.Write("> ");
                    continuar = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    if (continuar != 0 && continuar != 1 && continuar != 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Opción no valida\n");
                        Console.WriteLine("Presione \"ENTER\" para continuar\n");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("El caracter ingresado no es valido\n");
                    Console.WriteLine("Presione \"ENTER\" para continuar\n");
                    Console.Write("> ");
                    Console.ReadKey();
                    Console.Clear();
                    continuar = 3;
                }
            } while (continuar != 0 && continuar != 1 && continuar != 2);
        }
    } while (continuar != 0 && continuar != 2);
    if (continuar == 0)
    {
        op = 0;
    }
    else
    {
        op = 2;
    }
    return op;
}
//====================================================================================================================================================================

//Programa principal

//BATTLESHIP=========================
do
{
    try
    {
        Console.WriteLine("\t\t\t\t\t\t BATTLESHIP\n");
        Console.WriteLine("\t\t ¡Hola Capitán! El día de hoy preparamos un entretenido desafio para ti\n");
        Console.WriteLine("\t\t ¿Seras capaz de hundir las fuerzas enemigas y llevar la victoria a casa?");
        Console.WriteLine("==========================================================================================================\n");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("1) Jugar :3");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("2) Instrucciones :D");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("0) Salir :c\n");
        Console.ResetColor();
        Console.Write("> ");
        op = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        if (op != 0)
        {
            switch (op)
            {
                case 1:
                    op = GOTY(tablero, espacionaves, nombresNaves, op, naves);
                    break;
                case 2:
                    Console.WriteLine("1) El juego consiste en un tablero de 20x20 al cual debe disparar usando coordenas (X,Y)\n");
                    Console.WriteLine("2) Se le proporcionara una cierta cantidad de \"Puntos de Disparo\", los cuales aumentaran o disminuiran");
                    Console.WriteLine("dependiendo los aciertos y fallos que cometa si la cantiad de puntos llega a 0 y usted no");
                    Console.WriteLine("ha logrado acertar en todas las ubicaciones de los barcos, perdera.\n");
                    Console.WriteLine("3) Debe disparar utilizando este formato: 3,4\n");
                    Console.WriteLine("4) La \"Información Enemgia\" solo indica el numero de barcos al iniciar la partida, mas no indica el");
                    Console.WriteLine("número de barcos durante la partida (hundidos o en pie) para eso fijarse en el numero de disparos por");
                    Console.WriteLine("acertar.\n");
                    Console.WriteLine("5) Para una mejor experiencia maximizar el tamaño de la ventana\n");
                    Console.WriteLine("Presione \"ENTER\" para continuar\n");
                    Console.Write("> ");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 777:
                    op = GOTY(tablero, espacionaves, nombresNaves, op, naves);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("La opción ingresada no es valida");
                    Console.ResetColor();
                    break;
            }

        }
    }
    catch (Exception)
    {
        Console.Clear();
        Console.WriteLine("El caracter ingresado no es valido\n");
        Console.WriteLine("Presione \"ENTER\" para continuar\n");
        Console.Write("> ");
        Console.ReadKey();
        Console.Clear();
    }
} while (op != 0);
Console.Clear();
Console.ForegroundColor = ConsoleColor.DarkCyan;
Console.WriteLine("Gracias por usar el programa :3\n");
Console.WriteLine("-By Marvin Cámbara");
Console.ResetColor();