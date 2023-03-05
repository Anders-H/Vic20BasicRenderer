namespace Vic20BasicRenderer.BaseTypes;

public class CharacterCase : ProgramContent
{
    private readonly bool _upperCase;
    private readonly bool _lockedCase;

    public CharacterCase(bool upperCase, bool lockedCase)
    {
        _upperCase = upperCase;
        _lockedCase = lockedCase;
    }

    public override string GetCode(List<ProgramContent> currentProgramContent, BasicProgram currentProgram, int currentIndex)
    {
        if (currentProgram.Platform == Platform.C64)
        {
            if (_upperCase)
                return _lockedCase ? "poke53272,21:?chr$(8)" : "poke53271,21:?chr$(9)";

            return _lockedCase ? "poke53272,23:?chr$(8)" : "poke53272,23:?chr$(9)";
        }

        if (currentProgram.Platform == Platform.Vic20)
        {
            if (_upperCase)
                return _lockedCase ? "poke36869,240:?chr$(8)" : "poke53271,21:?chr$(9)";

            return _lockedCase ? "poke36869,242:?chr$(8)" : "poke53272,23:?chr$(9)";
        }

        throw new UnsupportedPlatformException();
    }

    public override bool HasLineNumber =>
        true;
}