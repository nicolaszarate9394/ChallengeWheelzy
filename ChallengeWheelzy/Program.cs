using ChallengeWheelzy.QuestionNumber3;
using ChallengeWheelzy.QuestionNumber4;
using ChallengeWheelzy.QuestionNumber5;

namespace ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Challenge Demo");
            Console.WriteLine("elige una opcion:");
            Console.WriteLine("1 - respuesta punto 3 ");
            Console.WriteLine("2 - respuesta punto 4 ");
            Console.WriteLine("3 - respuesta punto 5 ");
            Console.WriteLine("0 - finalizar");

            var op = Console.ReadLine();

            while (op != "0")
            {

                if (op == "1")
                {
                    Console.WriteLine("corriendo punto 3...");
                    await QuestionNumber3Demo.Run();
                }
                else if (op == "2")
                {
                    Console.WriteLine("corriendo punto 4...");
                    await QuestionNumber4Demo.Run();
                }
                else if (op == "3")
                {
                    Console.WriteLine("corriendo punto 5...");
                    QuestionNumber5Demo.Run();
                }
                else
                {
                    Console.WriteLine("opcion no valida");
                }

                Console.WriteLine();
                Console.WriteLine("elige una opcion (0 para salir):");
                op = Console.ReadLine();
            }

            Console.WriteLine("fin del programa");
        }
    }
}
