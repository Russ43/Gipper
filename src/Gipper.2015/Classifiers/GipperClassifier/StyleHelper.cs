using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Gipper._2015.Classifiers.GipperClassifier
{
	internal class StyleHelper
	{
		public static readonly Typeface DefinitionFontFace = new Typeface("Segoe UI Black");
		public static readonly Typeface CommentFontFace = new Typeface("Short Stack");
		public const double CommentFontRenderingSize = 13;
		public static readonly Typeface LiteralFontFace = new Typeface("Courier Prime");
		public const double NamespaceFontRenderingSize = 30;
		public const double TypeFontRenderingSize = 24;
		public const double MemberFontRenderingSize = 18;
	}
}
