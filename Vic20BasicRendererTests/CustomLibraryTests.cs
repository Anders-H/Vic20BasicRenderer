using Vic20BasicRenderer;
using Vic20BasicRenderer.BaseTypes;
using Vic20BasicRenderer.Variables;

namespace Vic20BasicRendererTests;

public class CustomLibraryTests
{
    [Test]
    public void CalledLibraryGetsIncluded()
    {
        var p = new BasicProgram(Platform.Vic20);
        p.LibraryCall(new CountLibrary(10));

        var code = p.GetCode();
        System.Diagnostics.Debug.WriteLine(code);
        Assert.That(code, Is.EqualTo(@"0gosub2
1end
2rem""testing the count library
3fora=1to10:printa:next
4rem""thank you"));
    }

    [Test]
    public void CalledLibraryGetsIncludedOnlyOnce()
    {
        var p = new BasicProgram(Platform.Vic20);
        var l = new CountLibrary(10);
        p.LibraryCall(l);
        p.Print("hello");
        p.LibraryCall(l);
        var code = p.GetCode();
        System.Diagnostics.Debug.WriteLine(code);
        Assert.That(code, Is.EqualTo(@"0gosub4
1?""hello""
2gosub4
3end
4rem""testing the count library
5fora=1to10:printa:next
6rem""thank you"));
    }

    public class CountLibrary : CustomLibrary
    {
        private static FloatVariable? CountToVariable { get; set; }
        public int CountTo { get; set; }
        
        public CountLibrary(int countTo)
        {
            CountTo = countTo;
        }

        public override string GetCode(List<ProgramContent> currentProgramContent, BasicProgram currentProgram, int currentIndex) =>
            $@"gosub{TargetLineNumber}";


        public override void AddLibraryCode(BasicProgram p)
        {
            p.Add(LibraryLabel);
            CountToVariable ??= p.CreateFloat();
            p.AddFree(@"rem""testing the count library");
            p.AddFree($"for{CountToVariable}=1to{CountTo}:print{CountToVariable}:next");
            p.AddFree(@"rem""thank you");
        }
    }
}