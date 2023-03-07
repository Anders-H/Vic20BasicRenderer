using Vic20BasicRenderer.BaseTypes;

namespace Vic20BasicRenderer.Commands;

public class Gosub : ProgramContent
{
    internal Label Label { get; }
    public int? TargetLineNumber { get; internal set; }

    public Gosub(Label label)
    {
        Label = label;
    }

    public override string GetCode(List<ProgramContent> currentProgramContent, BasicProgram currentProgram, int currentIndex) =>
        $@"gosub{TargetLineNumber}";

    public override bool HasLineNumber =>
        true;
}