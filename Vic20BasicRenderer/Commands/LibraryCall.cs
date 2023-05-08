using Vic20BasicRenderer.BaseTypes;

namespace Vic20BasicRenderer.Commands;

public class LibraryCall : ProgramContent
{
    internal CustomLibrary Library { get; }
    public int? TargetLineNumber { get; internal set; }

    public LibraryCall(CustomLibrary library)
    {
        Library = library;
    }

    public override string GetCode(List<ProgramContent> currentProgramContent, BasicProgram currentProgram, int currentIndex) =>
        $@"gosub{TargetLineNumber}";

    public override bool HasLineNumber =>
        true;
}