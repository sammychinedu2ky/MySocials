using System.Diagnostics;
using System.Text.Json;

namespace MySocials.Actions;

internal class Utilities
{
    public static Dictionary<string, string> JsonFileToDictionary()
    {
        var fileData = File.ReadAllText(Constants.FilePath);
        var jsonData = string.IsNullOrEmpty(fileData) ? "{}" : File.ReadAllText(Constants.FilePath);
        return JsonSerializer.Deserialize<Dictionary<string, string>>(jsonData) ?? new Dictionary<string, string>();
    }

    public static void AddOrUpdateDataToJsonFile(string key, string value)
    {
        var dictionaryData = JsonFileToDictionary();
        dictionaryData[key.ToLower()] = value.ToLower();
        var serializeData = JsonSerializer.Serialize(dictionaryData, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(Constants.FilePath, serializeData);
    }

    public static void PrintHeader(string text)
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(text);
        Console.ResetColor();
        Console.WriteLine();
    }

    public static void ColorLine(string text)
    {
        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write($"=>  {text} ");
        Console.ResetColor();
        Console.WriteLine();
    }

    public static void CopyToClipBoard(string text)
    {
        try
        {
            if (!string.IsNullOrEmpty(text))
            {
                var process = Process.Start(new ProcessStartInfo
                {
                    FileName = "Powershell",
                    ArgumentList = {$"Set-Clipboard -Value \"{text}\""},
                    RedirectStandardOutput = true
                });
                var output = process?.StandardOutput.ReadToEnd();
                if (!string.IsNullOrEmpty(output)) throw new Exception(output);
                process?.WaitForExit();
            }
        }
        catch
        {
            Console.Clear();
            Console.WriteLine("Ensure you have Powershell v7+ installed");
        }
    }

    public static void ScrollUpAndDown(ref int cursor, ConsoleKey key, DicObj[] dictionaryToList)
    {
        if (key == ConsoleKey.DownArrow)
        {
            cursor = (cursor + 1) % dictionaryToList.Length;
            foreach (var item in dictionaryToList)
                if (cursor == item.Index)
                    ColorLine(item.Key);
                else
                    Console.WriteLine(item.Key);
        }

        if (key == ConsoleKey.UpArrow)
        {
            cursor = (cursor + (dictionaryToList.Length - 1)) % dictionaryToList.Length;
            foreach (var item in dictionaryToList)
                if (cursor == item.Index)
                    ColorLine(item.Key);
                else
                    Console.WriteLine(item.Key);
        }
    }


    public static DicObj[] DictionaryToList(Dictionary<string, string> dictionaryData)
    {
        return dictionaryData.Select((item, index) =>
            new DicObj
            {
                Index = index,
                Key = item.Key,
                Value = item.Value,
                Word = $"{item.Key} : {item.Value}"
            }).ToArray();
    }

    public static void DeleteDataFromJsonFile(string key)
    {
        var dictionaryData = JsonFileToDictionary();
        dictionaryData.Remove(key);
        var serializeData = JsonSerializer.Serialize(dictionaryData, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(Constants.FilePath, serializeData);
    }

    public static void HighLightFirstItem(DicObj[] dictionaryToList)
    {
        foreach (var item in dictionaryToList)
            if (item.Index == 0)
                ColorLine(item.Key);
            else
                Console.WriteLine(item.Key);
    }
}