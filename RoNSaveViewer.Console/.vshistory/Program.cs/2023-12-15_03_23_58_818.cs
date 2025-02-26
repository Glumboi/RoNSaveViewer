﻿using Newtonsoft.Json;
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

    var uni = JsonConvert.SerializeObject((File.ReadAllText(
      @"C:\Users\merli\Downloads\test.txt"), Formatting.Indented));

    // Encode the string to UTF8
    byte[] bytes = Encoding.Default.GetBytes(jsonConverter.ToString());
    var myString = Encoding.UTF8.GetString(bytes);
    Console.WriteLine(myString);

    // Deserialize using class
    var sample = Newtonsoft.Json.JsonConvert.DeserializeObject<Example>(i);
    byte[] bytes = Encoding.Default.GetBytes(sample.Name);
    var myString = Encoding.UTF8.GetString(bytes);
    Console.WriteLine(myString);
}

Console.ReadKey();