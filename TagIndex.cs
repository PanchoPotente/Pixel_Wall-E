using Godot;
using   System;
using System.Collections.Generic;

public partial class TagIndex : Node
{
    private static Dictionary<string,int> tags = new Dictionary<string, int>(0);
    public static void Add(string name, int line)
    {
        tags.Add(name,line);
    }
    public static int GetLine(string name)
    {
        if(tags.ContainsKey(name)) return tags[name];
        else throw new ExecutionError("La etiqueta no existe en el contexto actual"); 
    }
    public static void Reset() => tags = new Dictionary<string, int>(0); 
}