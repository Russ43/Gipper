using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Gipper
{
	static internal class StyleHelper
	{
		// fields
		static private Font _formFont;
		static private Font _toolWindowFont;
		static private Font _keyListTextFont;
		static private Font _keyListKeyFont;
		static private Font _consoleFont;
		static private Brush _fileNavigatorBackBrush;
		static private Brush _fileNavigatorTextBrush;


		// properties
		static public int ButtonHeight
		{
		   get
			{
				return 43;
			}
		}

		static public Font FormFont
		{
			get
			{
				if(_formFont == null)
					_formFont = new Font("Segoe UI Semibold", 9);

				return _formFont;
			}
		}

		static public Font ToolWindowFont
		{
			get
			{
				if(_toolWindowFont == null)
					_toolWindowFont = new Font("Segoe UI Semibold", 9);

				return _toolWindowFont;
			}
		}

		static public Font KeyListTextFont
		{
			get
			{
				if(_keyListTextFont == null)
					_keyListTextFont = new Font("Segoe UI Semibold", 9);

				return _keyListTextFont;
			}
		}

		static public Font KeyListKeyFont
		{
			get
			{
				if(_keyListKeyFont == null)
					_keyListKeyFont = new Font("Segoe Keycaps", 12);

				return _keyListKeyFont;
			}
		}

		static public Font ConsoleFont
		{
			get
			{
				if(_consoleFont == null)
					_consoleFont = new Font("Consolas", 9);

				return _consoleFont;
			}
		}

		static public Brush FileNavigatorBackBrush1
		{
			get
			{
				if(_fileNavigatorBackBrush == null)
					_fileNavigatorBackBrush = new SolidBrush(Color.FromArgb(43, 0, 43));

				return _fileNavigatorBackBrush;
			}
		}

		static public Brush FileNavigatorBackBrush2
		{
			get
			{
				return FileNavigatorBackBrush1;
			}
		}

		static public Brush FileNavigatorTextBrush1
		{
			get
			{
				if(_fileNavigatorTextBrush == null)
					_fileNavigatorTextBrush = new SolidBrush(Color.FromArgb(227, 227, 227));

				return _fileNavigatorTextBrush;
			}
		}

		static public Brush FileNavigatorTextBrush2
		{
			get
			{
				return FileNavigatorTextBrush1;
			}
		}
	}
}

