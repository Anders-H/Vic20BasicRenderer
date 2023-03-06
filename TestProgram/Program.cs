using Vic20BasicRenderer;
using Vic20BasicRenderer.Commands;
using Vic20BasicRenderer.Expressions;

var p = new BasicProgram(Platform.Vic20);

var counter = p.CreateInt();

var testLabel = new Label();
p.CharacterCase(false, true);
p.Color(Vic20BorderColor.Green, Vic20Color.LightYellow, false);
p.Print((StringConstant)"test program!");
p.Add(testLabel);
p.Print((StringConstant)"hello world");
p.Goto(testLabel);
p.Goto(testLabel);

Console.WriteLine(p.GetCode());