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

    using (BinaryWriter bw = new BinaryWriter(File.Open(
      @"C:\test\test.txt", FileMode.Create)))
    {
        //writes the data to the stream
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i].Serialize(bw, true);
        }
        bw.Write(25);
        bw.Write(23.98);
        bw.Write('c');
        bw.Write("GeeksForGeeks");
        bw.Write(true);
        Console.WriteLine("Successfully Written");
        Console.ReadLine();
    }

    Console.WriteLine(JsonConvert.SerializeObject(arr, Formatting.Indented));
}

Console.ReadKey();