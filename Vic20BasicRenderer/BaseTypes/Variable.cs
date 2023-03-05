namespace Vic20BasicRenderer.BaseTypes;

public abstract class Variable : ProgramContent
{
    public string UnderlyingName { get; internal set; }

    internal Variable(string underlyingName)
    {
        UnderlyingName = underlyingName;
    }

    public override string GetCode(List<ProgramContent> currentProgramContent, BasicProgram currentProgram, int currentIndex) =>
        UnderlyingName;

    public override bool HasLineNumber =>
        false;
}