using Vic20BasicRenderer;

namespace Vic20BasicRendererTests;

public class VariableTests
{
    [Test]
    public void CanCreateVariables()
    {
        var p = new BasicProgram(Platform.Vic20);
        var i = p.CreateInt();
        Assert.That(i.ToString(), Is.EqualTo("a%"));
        i = p.CreateInt();
        Assert.That(i.ToString(), Is.EqualTo("b%"));
        i = p.CreateInt();
        Assert.That(i.ToString(), Is.EqualTo("c%"));
        var f = p.CreateFloat();
        Assert.That(f.ToString(), Is.EqualTo("a"));
        f = p.CreateFloat();
        Assert.That(f.ToString(), Is.EqualTo("b"));
        f = p.CreateFloat();
        Assert.That(f.ToString(), Is.EqualTo("c"));
        var s = p.CreateString();
        Assert.That(s.ToString(), Is.EqualTo("a$"));
        s = p.CreateString();
        Assert.That(s.ToString(), Is.EqualTo("b$"));
        s = p.CreateString();
        Assert.That(s.ToString(), Is.EqualTo("c$"));
        i = p.CreateInt();
        Assert.That(i.ToString(), Is.EqualTo("d%"));
        i = p.CreateInt();
        Assert.That(i.ToString(), Is.EqualTo("e%"));
        i = p.CreateInt();
        Assert.That(i.ToString(), Is.EqualTo("f%"));
        f = p.CreateFloat();
        Assert.That(f.ToString(), Is.EqualTo("d"));
        f = p.CreateFloat();
        Assert.That(f.ToString(), Is.EqualTo("e"));
        f = p.CreateFloat();
        Assert.That(f.ToString(), Is.EqualTo("f"));
        s = p.CreateString();
        Assert.That(s.ToString(), Is.EqualTo("d$"));
        s = p.CreateString();
        Assert.That(s.ToString(), Is.EqualTo("e$"));
        s = p.CreateString();
        Assert.That(s.ToString(), Is.EqualTo("f$"));
    }
}