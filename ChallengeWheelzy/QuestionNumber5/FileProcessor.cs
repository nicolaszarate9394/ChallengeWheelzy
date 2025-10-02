using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ChallengeWheelzy.QuestionNumber5
{
    public static class FileProcessor
    {
        public static void ProcessCsFiles(string folderPath)
        {
            var files = Directory.GetFiles(folderPath, "*.cs", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var lines = File.ReadAllLines(file).ToList();

                for (int i = 0; i < lines.Count; i++)
                {
                    // metodos async sin Async en el nombre
                    if (lines[i].Contains("async Task") || lines[i].Contains("async ValueTask"))
                    {
                        var match = Regex.Match(lines[i], @"\s+(\w+)\s*\(");
                        if (match.Success && !match.Groups[1].Value.EndsWith("Async"))
                        {
                            lines[i] = lines[i].Replace(match.Groups[1].Value, match.Groups[1].Value + "Async");
                        }
                    }

                    // cambio Vm por VM, Dto por DTO
                    lines[i] = Regex.Replace(lines[i], @"\b(\w*?)(Vm|Vms|Dto|Dtos)\b", m =>
                    {
                        string baseWord = m.Groups[1].Value;
                        string type = m.Groups[2].Value;
                        // si termina con s la dejamos minscula
                        if (type.EndsWith("s"))
                            return baseWord + type.Substring(0, type.Length - 1).ToUpper() + "s";
                        return baseWord + type.ToUpper();
                    });
                }

                // agregar blank line entre metodos consecutivos
                // tambien puse que se recorra desde atras para no desordenar el indice cuando inserta
                for (int i = lines.Count - 1; i > 0; i--)
                {
                    if (lines[i].TrimStart().StartsWith("public") &&
                        !string.IsNullOrWhiteSpace(lines[i - 1]) &&
                        !lines[i - 1].Trim().StartsWith("}"))
                    {
                        lines.Insert(i, "");
                    }
                }

                File.WriteAllLines(file, lines);
            }
        }
    }
}
