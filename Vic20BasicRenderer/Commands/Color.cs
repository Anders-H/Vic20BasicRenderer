using Vic20BasicRenderer.BaseTypes;

namespace Vic20BasicRenderer.Commands;

public class Color : ProgramContent
{
    public C64Color C64BorderColor { get; internal set; }
    public C64Color C64BackgroundColor { get; internal set; }
    public Vic20BorderColor Vic20BorderColor { get; internal set; }
    public Vic20Color Vic20BackgroundColor { get; internal set; }

    public Color(C64Color borderColor, C64Color backgroundColor)
    {
        C64BorderColor = borderColor;
        C64BackgroundColor = backgroundColor;
        Vic20BorderColor = Vic20BorderColor.Cyan;
        Vic20BackgroundColor = Vic20Color.White;
    }

    public Color(Vic20BorderColor borderColor, Vic20Color backgroundColor)
    {
        C64BorderColor = C64Color.LightBlue;
        C64BackgroundColor = C64Color.Blue;
        Vic20BorderColor = borderColor;
        Vic20BackgroundColor = backgroundColor;
    }

    public override string GetCode(List<ProgramContent> currentProgramContent, BasicProgram currentProgram, int currentIndex)
    {
        if (currentProgram.Platform == Platform.C64)
        {
            var borderColor = (int)C64BorderColor;
            var backgroundColor = (int)C64BackgroundColor;
            return $"poke53280,{borderColor}:poke53281,{backgroundColor}";
        }

        if (currentProgram.Platform == Platform.Vic20)
        {
            var backgroundColor = (int)Vic20BackgroundColor;
            var borderColor = (int)Vic20BorderColor;
            var color = backgroundColor * 16 + borderColor + 8;
            return $"poke36879,{color}";
        }

        throw new UnsupportedPlatformException();
    }

    public override bool HasLineNumber =>
        true;
}