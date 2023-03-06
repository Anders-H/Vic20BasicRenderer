using Vic20BasicRenderer.BaseTypes;

namespace Vic20BasicRenderer.Commands;

public class FreeTextCode : ProgramContent
{
    private readonly string _content;

    public FreeTextCode(string content)
    {
        _content = content;
    }

    public override string GetCode(List<ProgramContent> currentProgramContent, BasicProgram currentProgram, int currentIndex) =>
        _content;

    public override bool HasLineNumber =>
        true;
}