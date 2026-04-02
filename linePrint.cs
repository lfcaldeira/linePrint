    using System;
    using System.IO;
    using System.Linq;

    class Program
    {
        static void Main()
        {
            string folderPath = @"C:\Users\lcaldei\Downloads";
            string[] linesToAdd = {
                "smart sizing:i:1",
                "desktopwidth:i:1920",
                "desktopheight:i:1080",
                "screen mode id:i:1"
            };

            try
            {
                string[] rdpFiles = Directory.GetFiles(folderPath, "*.rdp");
                foreach (string file in rdpFiles) 
                {
                    var lines = File.ReadAllLines(file, System.Text.Encoding.Unicode).ToList();
                    bool modified = false;

                    foreach (string newLine in linesToAdd)
                    {
                        string key = newLine.Split(':')[0]; // Ex: "smart sizing"
                        
                        // Procura se já existe alguma linha que comece com essa chave
                        int index = lines.FindIndex(l => l.StartsWith(key + ":", StringComparison.OrdinalIgnoreCase));

                        if (index != -1) {
                            // Se o valor for diferente, atualiza a linha existente
                            if (lines[index] != newLine) {
                                lines[index] = newLine;
                                modified = true;
                            }
                        } else {
                            // Se não existe, adiciona
                            lines.Add(newLine);
                            modified = true;
                        }
                    }

                    if (modified) {
                        File.WriteAllLines(file, lines, System.Text.Encoding.Unicode);
                        Console.WriteLine($"Ficheiro {Path.GetFileName(file)} atualizado.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }

            Console.WriteLine("Concluído! Pressione qualquer tecla para sair.");
            Console.ReadKey();
        }
    }