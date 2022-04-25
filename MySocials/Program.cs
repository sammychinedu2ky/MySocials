using MySocials.Actions;

if (args.Length == 0)
    Actions.CopyAllToClipBoard();
else
    switch (args[0].ToLower())
    {
        case Constants.Select:
            Actions.Select();
            break;
        case Constants.Add:
            Actions.Add();
            break;
        case Constants.Update:
            Actions.Update();
            break;
        case Constants.Delete:
            Actions.Delete();
            break;
        case Constants.Help:
            Actions.Help();
            break;
        default:
            Actions.SelectFromArgs(args[0].ToLower());
            break;
    }


internal class Constants
{
    public const string Select = "select";
    public const string Add = "add";
    public const string Update = "update";
    public const string Delete = "delete";
    public const string Help = "help";
    public static string FileName = "mysocials73f171af-81eb-4221-b4ae-7050ebedfe81.json";

    public static string FilePath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), FileName);
}