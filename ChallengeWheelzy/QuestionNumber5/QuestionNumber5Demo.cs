using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeWheelzy.QuestionNumber5
{
    public static class QuestionNumber5Demo
    {
        public static void Run()
        {
            string testFolder = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles");

            // cro carpeta de prueba si no existe
            if (!Directory.Exists(testFolder))
                Directory.CreateDirectory(testFolder);

            // creo un archivo .cs de ejemplo
            string testFile = Path.Combine(testFolder, "example.cs");
            File.WriteAllText(testFile,
                @"public class Example 
                {
                    public async Task DoSomething() { }
                    public void MethodOne(){}
                    public void MethodTwo(){}
                    private CustomerVm customer;
                    private List<OrderDtos> orders;
                }");

            // ejecuto el procesador
            FileProcessor.ProcessCsFiles(testFolder);

            //aca el resultado
            Console.WriteLine(File.ReadAllText(testFile));
        }
    }
}
