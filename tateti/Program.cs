using System;
using System.Diagnostics;
using System.Linq;

namespace tateti
{
    internal class Program
    {
        static private int jugador = 1;
        static private int ingreso = 0;
        static private bool ingresoCorrecto = true;

        static public int Jugador
        {
            get => jugador;
            set => jugador = value;
        }
        static public int Ingreso
        {
            get => ingreso;
            set => ingreso = value;
        }
        static public bool IngresoCorrecto
        {
            get => ingresoCorrecto;
            set => ingresoCorrecto = value;
        }

        static void Main(string[] args)
        {
            byte turno = 1;

            CrearTablero(dateGame);

            while (IngresoCorrecto && turno <= 9)
            {
                PedirIngreso();
                RecibirIngreso();

                if (VerificarIngreso())
                {
                    RunGame();
                    ActualizarTablero();
                    turno++;

                    if (IsPlayerWin())
                        break;
                }
            }
        }

        // Datos del Tablero
        static char[,] dateGame = { { '3', '1', '2' }, { '6', '4', '5' }, { '9', '7', '8' }, };

        static int[] dataInput = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        //Renderizar Tablero en la Consola
        static public void CrearTablero(char[,] data)
        {
            Console.WriteLine("     |     |");
            Console.WriteLine($"  {data[0, 1]}  |  {data[0, 2]}  |  {data[0, 0]}");
            Console.WriteLine("-----|-----|-----");
            Console.WriteLine("     |     |");
            Console.WriteLine($"  {data[1, 1]}  |  {data[1, 2]}  |  {data[1, 0]}");
            Console.WriteLine("-----|-----|-----");
            Console.WriteLine("     |     |");
            Console.WriteLine($"  {data[2, 1]}  |  {data[2, 2]}  |  {data[2, 0]}");
            Console.WriteLine("     |     |");
        }

        //Instrucciones Iniciales
        static public void PedirIngreso()
        {
            Console.WriteLine($"Es el turno del jugador {Jugador}");

            Console.WriteLine("Introduce ún número del 1 al 9");
        }

        //Recibir y Guardar Dato de Consola
        static public void RecibirIngreso()
        {
            int ingresoSeguro = 0;
            IngresoCorrecto = int.TryParse(Console.ReadLine(), out ingresoSeguro);
            Ingreso = ingresoSeguro;
        }

        //Verificar que solo ingresen números
        static public bool VerificarIngreso()
        {
            if (!IngresoCorrecto || dataInput.Contains(Ingreso) || Ingreso.ToString().Length != 1)
            {
                Console.WriteLine(
                    "-Solo se admiten números del 1 - 9\n-Los numeros no pueden repetirse\n-Escriba un número válido para seguir jugando, sino se finalizará el juego"
                );

                RecibirIngreso();

                if (
                    !IngresoCorrecto
                    || dataInput.Contains(Ingreso)
                    || Ingreso.ToString().Length != 1
                )
                {
                    Console.WriteLine("Gracias por jugar");
                    Console.ReadKey();
                }
                else
                    dataInput[Ingreso] = Ingreso;
            }
            else
                dataInput[Ingreso] = Ingreso;

            return IngresoCorrecto;
        }

        //Localizar en que ubicación está el número
        static public int[] LocalizarDataGame()
        {
            int key = Ingreso;
            int row = 0;
            int index = key % 3;

            if (key == 1 || key == 2 || key == 3)
                row = 0;
            else if (key == 4 || key == 5 || key == 6)
                row = 1;
            else if (key == 7 || key == 8 || key == 9)
                row = 2;

            return new int[] { row, index };
        }

        //Remplazar el número por el simbolo dependiendo del jugador
        static public void RemplazarDataGame(int[] location)
        {
            dateGame[location[0], location[1]] = Jugador == 1 ? 'X' : 'O';
        }

        //Juego en Estado Activo
        static public void RunGame()
        {
            int[] location = LocalizarDataGame();

            RemplazarDataGame(location);

            CambiarJugador();
        }

        //Verificar si algún jugador ganó
        static public bool IsPlayerWin()
        {
            //f == figurePlayerContrarie
            char f = Jugador == 1 ? 'O' : 'X';
            int playerWinner = f == 'O' ? 2 : 1;
            string playerWinText = $"El ganador es el jugador {playerWinner}, su simbolo es: {f}";

            bool isWin = false;

            //----------------------------------------------------------------------------------

            if (
                dateGame[0, 0] == f && dateGame[0, 1] == f && dateGame[0, 2] == f
                || dateGame[1, 0] == f && dateGame[1, 1] == f && dateGame[1, 2] == f
                || dateGame[2, 0] == f && dateGame[2, 1] == f && dateGame[2, 2] == f
                || dateGame[0, 0] == f && dateGame[1, 0] == f && dateGame[2, 0] == f
                || dateGame[0, 1] == f && dateGame[1, 1] == f && dateGame[2, 1] == f
                || dateGame[0, 2] == f && dateGame[1, 2] == f && dateGame[2, 2] == f
                || dateGame[0, 1] == f && dateGame[1, 2] == f && dateGame[2, 0] == f
                || dateGame[0, 0] == f && dateGame[1, 2] == f && dateGame[2, 1] == f
            )
            {
                isWin = true;
                Console.WriteLine(playerWinText);
            }

            return isWin;
        }

        //Recargar Tabla cada vez que se modifica
        static public void ActualizarTablero()
        {
            Console.Clear();
            CrearTablero(dateGame);
        }

        //Cambiar Turno Dependiendo del jugador Actual
        static public void CambiarJugador()
        {
            if (Jugador == 1)
                Jugador = 2;
            else
                Jugador = 1;
        }
    }
}
