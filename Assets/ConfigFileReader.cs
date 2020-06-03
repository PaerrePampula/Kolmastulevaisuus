using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public static class ConfigFileReader
{

    static Dictionary<string, string> readLines;
    [RuntimeInitializeOnLoadMethod]
    static void Init()
    {
        string[] lines = File.ReadAllLines(System.IO.Path.Combine(Application.streamingAssetsPath, "config.txt"));
        List<string> filteredLines = new List<string>();
        foreach (var line in lines)
        {
            if (line.StartsWith("#")) //Pystyy lisäämäänkommentteja
            {
                continue;
            }
            else
            {
                filteredLines.Add(line);
            }
        }

        var pairs = filteredLines.Select(l => new { Line = l, Pos = l.IndexOf("=") });
        readLines = pairs.ToDictionary(p => p.Line.Substring(0, p.Pos), p => p.Line.Substring(p.Pos + 1).Trim());


    }
    public static float getValue(string keyString)
    {
        var returnedKey = readLines[keyString];
        return float.Parse(returnedKey);
    }
}
