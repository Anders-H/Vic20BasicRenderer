using Vic20BasicRenderer;
using Vic20BasicRenderer.Commands;
using Vic20BasicRenderer.Expressions;

var p = new BasicProgram(Platform.Vic20);

var counter = p.CreateFloat();
var something = p.CreateFloat();
p.AddFree($"{something}=1");
p.AddFree($"for{counter}=1to10");
p.AddFree($"{something}={something}*2");
p.Print(something);
p.AddFree($"next{counter}");

Console.WriteLine(p.GetCode());