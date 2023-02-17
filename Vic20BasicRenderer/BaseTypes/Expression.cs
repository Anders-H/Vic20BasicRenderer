namespace Vic20BasicRenderer.BaseTypes;

public abstract class Expression : ProgramContent
{
    public override bool HasLineNumber =>
        false;
}