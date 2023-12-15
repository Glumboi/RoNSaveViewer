using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using UeSaveGame;

SaveGame game;

using (FileStream fs = File.OpenRead(@"C:\Users\merli\AppData\Local\ReadyOrNot\Saved\SaveGames\GameSettings.sav"))
{
    game = SaveGame.LoadFrom(fs);
    UProperty[] arr = new UProperty[game.Properties.Count];
    game.Properties.CopyTo(arr, 0);

    using (var writer = new BinaryWriter(fs, Encoding.UTF8, false))
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i].Serialize(writer, true);
        }
    }

    Console.WriteLine(JsonConvert.SerializeObject(arr, Formatting.Indented));
}

Console.ReadKey();