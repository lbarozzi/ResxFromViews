// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;
using System.Xml;

using ResxFromViews;

if (args.Length<2) {
    Console.WriteLine($"usage: {args[0]} path_to_views");
    Environment.Exit(-1);
}

foreach ( string f in Directory.GetFiles(args[1],"*.cshtml")) {
   Replacer.ProcessFile(f);
}


