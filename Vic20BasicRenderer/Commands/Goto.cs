using Vic20BasicRenderer.BaseTypes;

namespace Vic20BasicRenderer.Commands;

public class Goto : ProgramContent
{
    internal Label Label { get; }
    public int? TargetLineNumber { get; internal set; }

    public Goto(Label label)
    {
        Label = label;
    }

    public override string GetCode(List<ProgramContent> currentProgramContent, BasicProgram currentProgram, int currentIndex)
    {
        return $@"goto{TargetLineNumber}";
    }

    public override bool HasLineNumber =>
        true;
}