using OpenQA.Selenium;
using static Farsica.Framework.Test.Common.Constants;

namespace Farsica.Framework.Test.Selenium
{
	internal static class Converter
	{
		public static string Convert(Key key)
		{
			switch (key)
			{
				case Key.Null:
					return Keys.Null;
				case Key.Cancel:
					return Keys.Cancel;
				case Key.Help:
					return Keys.Help;
				case Key.Backspace:
					return Keys.Backspace;
				case Key.Tab:
					return Keys.Tab;
				case Key.Clear:
					return Keys.Clear;
				case Key.Return:
					return Keys.Return;
				case Key.Enter:
					return Keys.Enter;
				case Key.Shift:
					return Keys.Shift;
				case Key.LeftShift:
					return Keys.LeftShift;
				case Key.Control:
					return Keys.Control;
				case Key.LeftControl:
					return Keys.LeftControl;
				case Key.Alt:
					return Keys.Alt;
				case Key.LeftAlt:
					return Keys.LeftAlt;
				case Key.Pause:
					return Keys.Pause;
				case Key.Escape:
					return Keys.Escape;
				case Key.Space:
					return Keys.Space;
				case Key.PageUp:
					return Keys.PageUp;
				case Key.PageDown:
					return Keys.PageDown;
				case Key.End:
					return Keys.End;
				case Key.Home:
					return Keys.Home;
				case Key.Left:
					return Keys.Left;
				case Key.ArrowLeft:
					return Keys.ArrowLeft;
				case Key.Up:
					return Keys.Up;
				case Key.ArrowUp:
					return Keys.ArrowUp;
				case Key.Right:
					return Keys.Right;
				case Key.ArrowRight:
					return Keys.ArrowRight;
				case Key.Down:
					return Keys.Down;
				case Key.ArrowDown:
					return Keys.ArrowDown;
				case Key.Insert:
					return Keys.Insert;
				case Key.Delete:
					return Keys.Delete;
				case Key.Semicolon:
					return Keys.Semicolon;
				case Key.Equal:
					return Keys.Equal;
				case Key.NumberPad0:
					return Keys.NumberPad0;
				case Key.NumberPad1:
					return Keys.NumberPad1;
				case Key.NumberPad2:
					return Keys.NumberPad2;
				case Key.NumberPad3:
					return Keys.NumberPad3;
				case Key.NumberPad4:
					return Keys.NumberPad4;
				case Key.NumberPad5:
					return Keys.NumberPad5;
				case Key.NumberPad6:
					return Keys.NumberPad6;
				case Key.NumberPad7:
					return Keys.NumberPad7;
				case Key.NumberPad8:
					return Keys.NumberPad8;
				case Key.NumberPad9:
					return Keys.NumberPad9;
				case Key.Multiply:
					return Keys.Multiply;
				case Key.Add:
					return Keys.Add;
				case Key.Separator:
					return Keys.Separator;
				case Key.Subtract:
					return Keys.Subtract;
				case Key.Decimal:
					return Keys.Decimal;
				case Key.Divide:
					return Keys.Divide;
				case Key.F1:
					return Keys.F1;
				case Key.F2:
					return Keys.F2;
				case Key.F3:
					return Keys.F3;
				case Key.F4:
					return Keys.F4;
				case Key.F5:
					return Keys.F5;
				case Key.F6:
					return Keys.F6;
				case Key.F7:
					return Keys.F7;
				case Key.F8:
					return Keys.F8;
				case Key.F9:
					return Keys.F9;
				case Key.F10:
					return Keys.F10;
				case Key.F11:
					return Keys.F11;
				case Key.F12:
					return Keys.F12;
				case Key.Meta:
					return Keys.Meta;
				case Key.Command:
					return Keys.Command;
				case Key.ZenkakuHankaku:
					return Keys.ZenkakuHankaku;
				default:
					throw new ArgumentOutOfRangeException(nameof(key));
			}
		}
	}
}