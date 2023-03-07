using Vic20BasicRenderer.BaseTypes;
using Vic20BasicRenderer.Commands;

namespace Vic20BasicRenderer;

public abstract class CustomLibrary : ProgramContent
{
    public int? TargetLineNumber { get; internal set; }
    public Label LibraryLabel { get; }

    protected CustomLibrary()
    {
        LibraryLabel = new Label();
    }

    public override bool HasLineNumber =>
        true;

    public abstract void AddLibraryCode(BasicProgram p);
}