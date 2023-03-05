using System.Diagnostics;
using Vic20BasicRenderer;
using Vic20BasicRenderer.Commands;
using Vic20BasicRenderer.Expressions;

var p = new BasicProgram(Platform.Vic20);

var testLabel = new Label();
p.CharacterCase(false, true);
p.Color(Vic20BorderColor.Green, Vic20Color.LightYellow, false);
p.Print((StringConstant)"test program!");
p.Add(testLabel);
p.Print((StringConstant)"hello world");
p.Goto(testLabel);
p.Goto(testLabel);

var tempFilename = Path.Combine(Path.GetTempPath(), "example_program_v20.bas");
using var sw = new StreamWriter(tempFilename);
sw.Write(p.GetCode());
sw.Flush();
sw.Close();
const string programPath = @"notepad";
var startInfo = new ProcessStartInfo(programPath, tempFilename)
{
    UseShellExecute = true
};

Process.Start(startInfo);