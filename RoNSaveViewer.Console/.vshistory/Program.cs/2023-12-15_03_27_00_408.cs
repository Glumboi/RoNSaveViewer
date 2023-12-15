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
        //writes the data to the stream
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i].Serialize(bw, true);
        }
        /*bw.Write(25);
        bw.Write(23.98);
        bw.Write('c');
        bw.Write("GeeksForGeeks");
        bw.Write(true);
        Console.WriteLine("Successfully Written");
        Console.ReadLine();*/
    }

    var jsonConverter = JsonConvert.SerializeObject((File.ReadAllText(
      @"C:\Users\merli\Downloads\test.txt"), Formatting.Indented));

    string unescapedString = Regex.Unescape(jsonConverter);

    // Deserialize the string into a dynamic object
    dynamic jsonObject = JsonConvert.DeserializeObject(unescapedString);

    // Display the JSON object properties
    foreach (var property in jsonObject)
    {
        Console.WriteLine($"{property.Name}: {property.Value}");
    }

    Console.ReadKey();