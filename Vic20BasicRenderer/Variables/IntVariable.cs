using Vic20BasicRenderer.BaseTypes;

namespace Vic20BasicRenderer.Variables;

public class IntVariable : Variable
{
    public IntVariable(string underlyingName) : base(underlyingName)
    {
    }

    public override string GetCode(List<ProgramContent> currentProgramContent, BasicProgram currentProgram, int currentIndex) =>
        $"{base.GetCode(currentProgramContent, currentProgram, currentIndex)}%";

    public override string ToString() =>
        $"{UnderlyingName}%";
}