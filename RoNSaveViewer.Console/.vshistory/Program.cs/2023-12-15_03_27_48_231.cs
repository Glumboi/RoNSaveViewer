using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using UeSaveGame;

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

    var jsonConverter = JsonConvert.SerializeObject((File.ReadAllText(
      @"C:\Users\merli\Downloads\test.txt"), Formatting.Indented));

    string unescapedString = Regex.Unescape(jsonConverter);

    try
    {
        // Attempt to parse the string into a JSON object
        dynamic jsonObject = JsonConvert.DeserializeObject(unescapedString);

        // Display the JSON object properties
        foreach (var property in jsonObject)
        {
            Console.WriteLine($"{property.Name}: {property.Value}");
        }
    }
    catch (JsonReaderException ex)
    {
        // Handle the exception, print details, or log the error
        Console.WriteLine($"Error parsing JSON: {ex.Message}");
        Console.WriteLine($"Line: {ex.LineNumber}, Position: {ex.LinePosition}");
    }
}
Console.ReadKey();