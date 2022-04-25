namespace MySocials.Actions;

public class Actions
{
    public static void CopyAllToClipBoard()
    {
        if (File.Exists(Constants.FilePath))

        {
            var dictionaryData = Utilities.JsonFileToDictionary();
            if (dictionaryData.Keys.Count == 0)
            {
                Console.WriteLine("No Data Available");
                return;
            }

            var text = string.Empty;
            foreach (var item in dictionaryData) text += $"{item.Key}: {item.Value} \n";

            Utilities.CopyToClipBoard(text);
        }
        else
        {
            Console.WriteLine("No Data Available");
            File.Create(Constants.FilePath);
        }
    }

    public static void Add()
    {
        if (File.Exists(Constants.FilePath))

        {
            while (true)
            {
                Utilities.PrintHeader("Input key and press enter");
                var key = Console.ReadLine();
                Utilities.PrintHeader("Input value and press enter");
                var value = Console.ReadLine();
                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) break;
                Utilities.AddOrUpdateDataToJsonFile(key, value);
                Utilities.PrintHeader("Press Enter key to add more or any key to terminate");
                var checkEnter = Console.ReadKey();
                if (checkEnter.Key != ConsoleKey.Enter) break;
            }
        }
        else
        {
            Console.WriteLine("Created Json File");
            File.Create(Constants.FilePath);
        }
    }

    public static void Select()
    {
        if (File.Exists(Constants.FilePath))
        {
            var cursor = 0;
            var dictionaryData = Utilities.JsonFileToDictionary();
            if (dictionaryData.Count == 0)
            {
                Console.WriteLine("No Data Available");
                return;
            }

            var dictionaryToList = Utilities.DictionaryToList(dictionaryData);
            Utilities.PrintHeader("Scroll using the arrow-key and select using the enter-key");
            Utilities.HighLightFirstItem(dictionaryToList);

            while (true)
            {
                var key = Console.ReadKey().Key;
                Utilities.PrintHeader("Scroll using the arrow-key and select using the enter-key");
                if (key == ConsoleKey.Enter)
                {
                    Utilities.CopyToClipBoard(dictionaryToList[cursor].Value);
                    Console.Clear();
                    break;
                }

                Utilities.ScrollUpAndDown(ref cursor, key, dictionaryToList);
            }
        }
        else
        {
            Console.WriteLine("No Data Available");
            File.Create(Constants.FilePath);
        }
    }

    public static void Update()
    {
        if (File.Exists(Constants.FilePath))
        {
            var cursor = 0;
            var dictionaryData = Utilities.JsonFileToDictionary();
            if (dictionaryData.Count == 0)
            {
                Console.WriteLine("No Data Available");
                return;
            }

            var dictionaryToList = Utilities.DictionaryToList(dictionaryData);


            Utilities.PrintHeader("Scroll using the arrow-key and select using the enter-key");
            Utilities.HighLightFirstItem(dictionaryToList);

            while (true)
            {
                var key = Console.ReadKey().Key;
                Utilities.PrintHeader("Scroll using the arrow-key and select using the enter-key");
                if (key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Utilities.PrintHeader(
                        $"Update value for {dictionaryToList[cursor].Key} key and press enter to save");
                    var value = Console.ReadLine();
                    if (!string.IsNullOrEmpty(value))
                        Utilities.AddOrUpdateDataToJsonFile(dictionaryToList[cursor].Key, value);
                    break;
                }

                Utilities.ScrollUpAndDown(ref cursor, key, dictionaryToList);
            }
        }
        else
        {
            Console.WriteLine("No Data Available");
            File.Create(Constants.FilePath);
        }
    }

    public static void Delete()
    {
        if (File.Exists(Constants.FilePath))
        {
            var cursor = 0;
            var dictionaryData = Utilities.JsonFileToDictionary();
            if (dictionaryData.Count == 0)
            {
                Console.WriteLine("No Data Available");
                return;
            }

            var dictionaryToList = Utilities.DictionaryToList(dictionaryData);


            Utilities.PrintHeader("Scroll using the arrow-key and select using the enter-key");
            Utilities.HighLightFirstItem(dictionaryToList);


            while (true)
            {
                var key = Console.ReadKey().Key;
                Utilities.PrintHeader("Scroll using the arrow-key and select using the enter-key");
                if (key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Utilities.DeleteDataFromJsonFile(dictionaryToList[cursor].Key);
                    break;
                }

                Utilities.ScrollUpAndDown(ref cursor, key, dictionaryToList);
            }
        }
        else
        {
            Console.WriteLine("No Data Available");
            File.Create(Constants.FilePath);
        }
    }

    public static void SelectFromArgs(string key)
    {
        if (File.Exists(Constants.FilePath))

        {
            var dictionaryData = Utilities.JsonFileToDictionary();
            if (dictionaryData.Keys.Count == 0)
            {
                Console.WriteLine("No Data Available");
                return;
            }

            try
            {
                Utilities.CopyToClipBoard(dictionaryData[key]);
            }
            catch
            {
                Console.WriteLine("key not found");
            }
        }
        else
        {
            Console.WriteLine("No Data Available");
            File.Create(Constants.FilePath);
        }
    }

    public static void Help()
    {
        var helpInfo = @"
        MySocials CLI tool is made by Samson Amaugo @https://github.com/sammychinedu2ky
        A handy tool to store and easily retrieve some of your important social links or details
        eg: Github profile link, youtube channel link, or any text data of your choice
        - Data is stored locally on your machine
     
    run-time options:
    Select          Displays a list of stored data which you select from and store in the clipboard

    Add             Store a text data by insert its key and value

    Update          Update an already stored data by selecting its key

    Delete          Delete an already stored data by selecting its key

    [*anytext]      Stores in the clipboard if data is available

    Help            Prints out help details
";
        Console.WriteLine(helpInfo);
    }
}

public class DicObj
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string Word { get; set; } = string.Empty;
    public int Index { get; set; }
}