using Vic20BasicRenderer;

namespace Vic20BasicRendererTests
{
    public class ColorTests
    {
        [Test]
        public void CanSetColorsOnVic20()
        {
            var p = new BasicProgram(Platform.Vic20);
            p.Color(Vic20BorderColor.Red, Vic20Color.LightCyan, false);

            var code = p.GetCode();
            System.Diagnostics.Debug.WriteLine(code);
            Assert.That(code, Is.EqualTo(@"0poke36879,186"));
        }

        [Test]
        public void CanSetColorsOnVic20InInvertedMode()
        {
            var p = new BasicProgram(Platform.Vic20);
            p.Color(Vic20BorderColor.Red, Vic20Color.LightCyan, true);

            var code = p.GetCode();
            System.Diagnostics.Debug.WriteLine(code);
            Assert.That(code, Is.EqualTo(@"0poke36879,178"));
        }

        [Test]
        public void CanSetColorsOnC64()
        {
            var p = new BasicProgram(Platform.C64);
            p.Color(C64Color.Green, C64Color.LightGreen);

            var code = p.GetCode();
            System.Diagnostics.Debug.WriteLine(code);
            Assert.That(code, Is.EqualTo(@"0poke53280,5:poke53281,13"));
        }
    }
}