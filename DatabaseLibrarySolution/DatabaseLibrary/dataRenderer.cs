using System;
using System.IO;

namespace DatabaseLibrary;

public class DataRenderer
{
    public string _path { get; set; }
    public List<string> _lines = new List<String>();
    
    public void construct()
    {
        File.WriteAllLines(_path, _lines);
    }

    public DataRenderer()
    {
        // Constructor
        _path = "result.txt";
    }
}