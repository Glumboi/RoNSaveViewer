using UeSaveGame;
using UeSaveGame.PropertyTypes;

Console.WriteLine("Enter the input file name:");

var inputFile =
    "C:\\Users\\merli\\AppData\\Local\\ReadyOrNot\\Saved\\SaveGames\\MetaGameProfile.sav"; //Console.ReadLine();

using (FileStream fs = File.OpenRead(inputFile))
{
    var sg = SaveGame.LoadFrom(fs);
    var levelProperty = sg.Properties[3] as ArrayProperty;

    foreach (var arrayElement in levelProperty.Value)
    {
        Console.WriteLine(arrayElement.Value);
    }
}