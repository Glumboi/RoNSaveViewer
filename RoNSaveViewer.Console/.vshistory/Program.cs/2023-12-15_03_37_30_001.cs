using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using UeSaveGame;
using static System.Net.Mime.MediaTypeNames;
using JsonSerializer = System.Text.Json.JsonSerializer;

SaveGame game;

using (FileStream fs = File.OpenRead(@"C:\Users\merli\AppData\Local\ReadyOrNot\Saved\SaveGames\GameSettings.sav"))
{
    game = SaveGame.LoadFrom(fs);
    UProperty[] arr = new UProperty[game.Properties.Count];
    game.Properties.CopyTo(arr, 0);

    using (BinaryWriter bw = new BinaryWriter(File.Open(
      @"C:\Users\merli\Downloads\test.txt", FileMode.Create)))
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i].Serialize(bw, true);
        }
    }

    var options = new JsonSerializerOptions { WriteIndented = true };
    string jsonString = JsonSerializer.Serialize(File.ReadAllText(
      @"C:\Users\merli\Downloads\test.txt"));

    Console.WriteLine(jsonString);
}

static string UnescapeUnicode(string input)
{
    // Check if the input has the extra backslash before 'u' and remove it if present
    if (input.Contains("\\u"))
    {
        // If the extra backslash is present, remove it
        input = input.Replace("\\u", "u");
    }

    // Unescape the Unicode characters
    return System.Text.RegularExpressions.Regex.Unescape(input);
}
Console.ReadKey();