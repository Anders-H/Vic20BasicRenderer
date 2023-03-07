using Vic20BasicRenderer;
using Vic20BasicRenderer.Commands;
using Vic20BasicRenderer.Expressions;

var p = new BasicProgram(Platform.Vic20);

var counter = p.CreateFloat();
var something = p.CreateFloat();
p.AddFree($"{something}=1");
var myLabel = new Label();
p.Goto(myLabel);
p.AddFree($"for{counter}=1to10");
p.AddFree($"{something}={something}*2");
p.Print(something);
p.AddFree($"next{counter}");
p.Add(myLabel);
p.Print("skip");

Console.WriteLine(p.GetCode());