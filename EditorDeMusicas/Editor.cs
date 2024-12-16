using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorDeMusicas
{
    internal class Editor{
        public static void Play()
        {
            string filePath = @"C:\Users\jenif\OneDrive\Área de Trabalho\Wicked Game.mp3";
            string newfilePath = @"C:\Users\jenif\OneDrive\Área de Trabalho\Wicked Game teste.mp3";

            // Carregar o arquivo MP3
            var file = TagLib.File.Create(filePath);
            //Directory.GetFiles;

            // Ler as informações atuais
            Console.WriteLine("Título: " + file.Tag.Title);
            Console.WriteLine("Artista: " + string.Join(", ", file.Tag.Performers));
            Console.WriteLine("Álbum: " + file.Tag.Album);

            // Alterar as informações
            file.Tag.Title = "Wicked Game Teste";
            file.Tag.Performers = new[] { "Um artista qualquer" };
            file.Tag.Album = "Album qualquer";
            System.IO.File.Move(filePath, newfilePath);

            // Salvar as alterações
            file.Save();
        }
    }
}
