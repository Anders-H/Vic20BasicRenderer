using Vic20BasicRenderer.BaseTypes;

namespace Vic20BasicRenderer.Commands;

public class Label : ProgramContent
{
    public int UnderlyingLineNumber { get; internal set; }

    public override string GetCode(List<ProgramContent> currentProgramContent, BasicProgram currentProgram, int currentIndex) =>
        "";

    public override bool HasLineNumber =>
        false;
}