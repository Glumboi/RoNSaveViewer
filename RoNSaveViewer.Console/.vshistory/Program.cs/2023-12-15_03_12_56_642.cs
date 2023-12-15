using UeSaveGame;

SaveGame game;
StreamWriter stream = ;

using (FileStream fs = File.OpenRead(@"C:\Users\merli\AppData\Local\ReadyOrNot\Saved\SaveGames\GameSettings.sav"))
{
    game = SaveGame.LoadFrom(fs);
    game.WriteTo(stream);
}