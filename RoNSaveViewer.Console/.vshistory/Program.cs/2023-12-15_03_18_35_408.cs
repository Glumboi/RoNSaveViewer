using Newtonsoft.Json;
using System.Text.Json.Serialization;
using UeSaveGame;

SaveGame game;
BinaryWriter binaryWriter;

using (FileStream fs = File.OpenRead(@"C:\Users\merli\AppData\Local\ReadyOrNot\Saved\SaveGames\GameSettings.sav"))
{
    game = SaveGame.LoadFrom(fs);
    UProperty[] arr = new UProperty[game.Properties.Count];
    game.Properties.CopyTo(arr, 0);
    for (int i = 0; i < arr.Length; i++)
    {
        arr[i].Serialize(binaryWriter);
    }
    Console.WriteLine(JsonConvert.SerializeObject(arr, Formatting.Indented));
}

Console.ReadKey();