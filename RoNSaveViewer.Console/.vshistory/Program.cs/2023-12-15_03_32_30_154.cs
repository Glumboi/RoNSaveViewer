﻿using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using UeSaveGame;
using static System.Net.Mime.MediaTypeNames;

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

    string output = UnescapeUnicode(jsonConverter);

    Console.WriteLine(output);
}

static string UnescapeUnicode(string input)
{
    return System.Text.RegularExpressions.Regex.Unescape(input);
}
Console.ReadKey();