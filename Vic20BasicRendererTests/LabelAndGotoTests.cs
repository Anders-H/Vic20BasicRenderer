using Vic20BasicRenderer;
using Vic20BasicRenderer.Commands;
using Vic20BasicRenderer.Expressions;

namespace Vic20BasicRendererTests;

public class LabelAndGotoTests
{
    [Test]
    public void LabelsCanRepresentLineNumbers()
    {
        var p = new BasicProgram(Platform.Vic20);
        p.Print((StringConstant)"a");
        p.Print((StringConstant)"b");
        p.Print((StringConstant)"c");
        p.Print((StringConstant)"d");
        var l1 = new Label();
        p.Add(l1);
        p.Print((StringConstant)"e");
        p.Print((StringConstant)"f");
        p.Print((StringConstant)"g");
        p.Print((StringConstant)"h");
        var l2 = new Label();
        p.Add(l2);
        p.Print((StringConstant)"i");
        p.Print((StringConstant)"j");
        p.Print((StringConstant)"k");
        p.Goto(l1);
        p.Print((StringConstant)"l");
        p.Print((StringConstant)"m");
        p.Goto(l2);
        p.Print((StringConstant)"n");

        var code = p.GetCode();
        System.Diagnostics.Debug.WriteLine(code);
        Assert.That(code, Is.EqualTo(@"0?""a""
1?""b""
2?""c""
3?""d""
4?""e""
5?""f""
6?""g""
7?""h""
8?""i""
9?""j""
10?""k""
11goto4
12?""l""
13?""m""
14goto8
15?""n"""));
    }

    [Test]
    public void LabelsCanRepresentFutureLineNumbers()
    {
        var p = new BasicProgram(Platform.Vic20);
        var label = new Label();
        p.Goto(label);
        p.Print((StringConstant)"hello");
        p.Add(label);
        p.Print((StringConstant)"also hello");

        var code = p.GetCode();
        System.Diagnostics.Debug.WriteLine(code);
        Assert.That(code, Is.EqualTo(@"0goto2
1?""hello""
2?""also hello"""));
    }
}