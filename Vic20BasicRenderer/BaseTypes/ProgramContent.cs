namespace Vic20BasicRenderer.BaseTypes;

public abstract class ProgramContent
{
    public int Index { get; internal set; }
    public int? LineNumber { get; internal set; }
    public abstract string GetCode(List<ProgramContent> currentProgramContent, BasicProgram currentProgram, int currentIndex);
    public abstract bool HasLineNumber { get; }
}