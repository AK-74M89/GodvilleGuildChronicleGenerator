using System.IO;

namespace GodvilleGuildChronicleGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var pageText = File.ReadAllText("page.html");
            var parser = new GuildPageParser();
            parser.Parse(pageText);
        }
    }
}
