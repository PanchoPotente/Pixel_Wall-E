using Godot;
using   System;
using System.Collections.Generic;

public partial class Variable : Node
{
    public static Dictionary< string, Expression> Index = new Dictionary<string, Expression>(0);
    public static void Asing( string name, Expression value) => Index.Add(name,value);
    public static void Reset() =>  Index = new Dictionary<string, Expression>(0);
}