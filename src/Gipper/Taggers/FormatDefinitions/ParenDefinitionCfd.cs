using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Gipper._2015.Classifiers.GipperClassifier.ClassificationFormatDefinitions
{
	[Export(typeof(EditorFormatDefinition))]
	[ClassificationType(ClassificationTypeNames = Name)]
	[Name(Name)]
	[UserVisible(true)]
	[Order(After = Priority.Default)]
	internal class ParenDefinitionCfd : ClassificationFormatDefinition
	{
		#region constants
		public const string Name = "Gipper.Cfd.ParenDefinition";
		#endregion region

		#region constructors
		public ParenDefinitionCfd()
		{
			DisplayName = "Paren Definition";
			FontTypeface = StyleHelper.DefinitionFontFace;
			FontRenderingSize = StyleHelper.ParenFontRenderingSizes[0];
		}
		#endregion

		#region exports
#pragma warning disable CS0649
		[Export(typeof(ClassificationTypeDefinition))]
		[Name(ParenDefinitionCfd.Name)]
		internal static ClassificationTypeDefinition ParenDefinition;
#pragma warning restore CS0649
		#endregion

		[Export(typeof(EditorFormatDefinition))]
		[ClassificationType(ClassificationTypeNames = Name)]
		[Name(Name)]
		[UserVisible(true)]
		[Order(After = Priority.Default)]
		internal class ParenDefinitionCfd_0 : ClassificationFormatDefinition
		{
			#region constants
			public const string Name = "Gipper.Cfd.ParenDefinition_0";
			#endregion region

			#region constructors
			public ParenDefinitionCfd_0()
			{
				DisplayName = "Paren Definition 0";
				FontTypeface = StyleHelper.DefinitionFontFace;
				FontRenderingSize = StyleHelper.ParenFontRenderingSizes[0];
			}
			#endregion

			#region exports
#pragma warning disable CS0649
			[Export(typeof(ClassificationTypeDefinition))]
			[Name(ParenDefinitionCfd_0.Name)]
			internal static ClassificationTypeDefinition ParenDefinition;
#pragma warning restore CS0649
			#endregion
		}

		[Export(typeof(EditorFormatDefinition))]
		[ClassificationType(ClassificationTypeNames = Name)]
		[Name(Name)]
		[UserVisible(true)]
		[Order(After = Priority.Default)]
		internal class ParenDefinitionCfd_1 : ClassificationFormatDefinition
		{
			#region constants
			public const string Name = "Gipper.Cfd.ParenDefinition_1";
			#endregion region

			#region constructors
			public ParenDefinitionCfd_1()
			{
				DisplayName = "Paren Definition 1";
				FontTypeface = StyleHelper.DefinitionFontFace;
				FontRenderingSize = StyleHelper.ParenFontRenderingSizes[1];
			}
			#endregion

			#region exports
#pragma warning disable CS0649
			[Export(typeof(ClassificationTypeDefinition))]
			[Name(ParenDefinitionCfd_1.Name)]
			internal static ClassificationTypeDefinition ParenDefinition;
#pragma warning restore CS0649
			#endregion
		}

		[Export(typeof(EditorFormatDefinition))]
		[ClassificationType(ClassificationTypeNames = Name)]
		[Name(Name)]
		[UserVisible(true)]
		[Order(After = Priority.Default)]
		internal class ParenDefinitionCfd_2 : ClassificationFormatDefinition
		{
			#region constants
			public const string Name = "Gipper.Cfd.ParenDefinition_1";
			#endregion region

			#region constructors
			public ParenDefinitionCfd_2()
			{
				DisplayName = "Paren Definition 2";
				FontTypeface = StyleHelper.DefinitionFontFace;
				FontRenderingSize = StyleHelper.ParenFontRenderingSizes[2];
			}
			#endregion

			#region exports
#pragma warning disable CS0649
			[Export(typeof(ClassificationTypeDefinition))]
			[Name(ParenDefinitionCfd_2.Name)]
			internal static ClassificationTypeDefinition ParenDefinition;
#pragma warning restore CS0649
			#endregion
		}

		[Export(typeof(EditorFormatDefinition))]
		[ClassificationType(ClassificationTypeNames = Name)]
		[Name(Name)]
		[UserVisible(true)]
		[Order(After = Priority.Default)]
		internal class ParenDefinitionCfd_3 : ClassificationFormatDefinition
		{
			#region constants
			public const string Name = "Gipper.Cfd.ParenDefinition_3";
			#endregion region

			#region constructors
			public ParenDefinitionCfd_3()
			{
				DisplayName = "Paren Definition 3";
				FontTypeface = StyleHelper.DefinitionFontFace;
				FontRenderingSize = StyleHelper.ParenFontRenderingSizes[3];
			}
			#endregion

			#region exports
#pragma warning disable CS0649
			[Export(typeof(ClassificationTypeDefinition))]
			[Name(ParenDefinitionCfd_3.Name)]
			internal static ClassificationTypeDefinition ParenDefinition;
#pragma warning restore CS0649
			#endregion
		}

		[Export(typeof(EditorFormatDefinition))]
		[ClassificationType(ClassificationTypeNames = Name)]
		[Name(Name)]
		[UserVisible(true)]
		[Order(After = Priority.Default)]
		internal class ParenDefinitionCfd_4 : ClassificationFormatDefinition
		{
			#region constants
			public const string Name = "Gipper.Cfd.ParenDefinition_4";
			#endregion region

			#region constructors
			public ParenDefinitionCfd_4()
			{
				DisplayName = "Paren Definition 4";
				FontTypeface = StyleHelper.DefinitionFontFace;
				FontRenderingSize = StyleHelper.ParenFontRenderingSizes[4];
			}
			#endregion

			#region exports
#pragma warning disable CS0649
			[Export(typeof(ClassificationTypeDefinition))]
			[Name(ParenDefinitionCfd_4.Name)]
			internal static ClassificationTypeDefinition ParenDefinition;
#pragma warning restore CS0649
			#endregion
		}

		[Export(typeof(EditorFormatDefinition))]
		[ClassificationType(ClassificationTypeNames = Name)]
		[Name(Name)]
		[UserVisible(true)]
		[Order(After = Priority.Default)]
		internal class ParenDefinitionCfd_5 : ClassificationFormatDefinition
		{
			#region constants
			public const string Name = "Gipper.Cfd.ParenDefinition_5";
			#endregion region

			#region constructors
			public ParenDefinitionCfd_5()
			{
				DisplayName = "Paren Definition 5";
				FontTypeface = StyleHelper.DefinitionFontFace;
				FontRenderingSize = StyleHelper.ParenFontRenderingSizes[5];
			}
			#endregion

			#region exports
#pragma warning disable CS0649
			[Export(typeof(ClassificationTypeDefinition))]
			[Name(ParenDefinitionCfd_5.Name)]
			internal static ClassificationTypeDefinition ParenDefinition;
#pragma warning restore CS0649
			#endregion
		}

		[Export(typeof(EditorFormatDefinition))]
		[ClassificationType(ClassificationTypeNames = Name)]
		[Name(Name)]
		[UserVisible(true)]
		[Order(After = Priority.Default)]
		internal class ParenDefinitionCfd_6 : ClassificationFormatDefinition
		{
			#region constants
			public const string Name = "Gipper.Cfd.ParenDefinition_6";
			#endregion region

			#region constructors
			public ParenDefinitionCfd_6()
			{
				DisplayName = "Paren Definition 6";
				FontTypeface = StyleHelper.DefinitionFontFace;
				FontRenderingSize = StyleHelper.ParenFontRenderingSizes[6];
			}
			#endregion

			#region exports
#pragma warning disable CS0649
			[Export(typeof(ClassificationTypeDefinition))]
			[Name(ParenDefinitionCfd_6.Name)]
			internal static ClassificationTypeDefinition ParenDefinition;
#pragma warning restore CS0649
			#endregion
		}

		[Export(typeof(EditorFormatDefinition))]
		[ClassificationType(ClassificationTypeNames = Name)]
		[Name(Name)]
		[UserVisible(true)]
		[Order(After = Priority.Default)]
		internal class ParenDefinitionCfd_7 : ClassificationFormatDefinition
		{
			#region constants
			public const string Name = "Gipper.Cfd.ParenDefinition_7";
			#endregion region

			#region constructors
			public ParenDefinitionCfd_7()
			{
				DisplayName = "Paren Definition 7";
				FontTypeface = StyleHelper.DefinitionFontFace;
				FontRenderingSize = StyleHelper.ParenFontRenderingSizes[7];
			}
			#endregion

			#region exports
#pragma warning disable CS0649
			[Export(typeof(ClassificationTypeDefinition))]
			[Name(ParenDefinitionCfd_7.Name)]
			internal static ClassificationTypeDefinition ParenDefinition;
#pragma warning restore CS0649
			#endregion
		}

		[Export(typeof(EditorFormatDefinition))]
		[ClassificationType(ClassificationTypeNames = Name)]
		[Name(Name)]
		[UserVisible(true)]
		[Order(After = Priority.Default)]
		internal class ParenDefinitionCfd_8 : ClassificationFormatDefinition
		{
			#region constants
			public const string Name = "Gipper.Cfd.ParenDefinition_8";
			#endregion region

			#region constructors
			public ParenDefinitionCfd_8()
			{
				DisplayName = "Paren Definition 8";
				FontTypeface = StyleHelper.DefinitionFontFace;
				FontRenderingSize = StyleHelper.ParenFontRenderingSizes[8];
			}
			#endregion

			#region exports
#pragma warning disable CS0649
			[Export(typeof(ClassificationTypeDefinition))]
			[Name(ParenDefinitionCfd_8.Name)]
			internal static ClassificationTypeDefinition ParenDefinition;
#pragma warning restore CS0649
			#endregion
		}

		[Export(typeof(EditorFormatDefinition))]
		[ClassificationType(ClassificationTypeNames = Name)]
		[Name(Name)]
		[UserVisible(true)]
		[Order(After = Priority.Default)]
		internal class ParenDefinitionCfd_9 : ClassificationFormatDefinition
		{
			#region constants
			public const string Name = "Gipper.Cfd.ParenDefinition_9";
			#endregion region

			#region constructors
			public ParenDefinitionCfd_9()
			{
				DisplayName = "Paren Definition 9";
				FontTypeface = StyleHelper.DefinitionFontFace;
				FontRenderingSize = StyleHelper.ParenFontRenderingSizes[9];
			}
			#endregion

			#region exports
#pragma warning disable CS0649
			[Export(typeof(ClassificationTypeDefinition))]
			[Name(ParenDefinitionCfd_9.Name)]
			internal static ClassificationTypeDefinition ParenDefinition;
#pragma warning restore CS0649
			#endregion
		}

		[Export(typeof(EditorFormatDefinition))]
		[ClassificationType(ClassificationTypeNames = Name)]
		[Name(Name)]
		[UserVisible(true)]
		[Order(After = Priority.Default)]
		internal class ParenDefinitionCfd_10 : ClassificationFormatDefinition
		{
			#region constants
			public const string Name = "Gipper.Cfd.ParenDefinition_10";
			#endregion region

			#region constructors
			public ParenDefinitionCfd_10()
			{
				DisplayName = "Paren Definition 10";
				FontTypeface = StyleHelper.DefinitionFontFace;
				FontRenderingSize = StyleHelper.ParenFontRenderingSizes[10];
			}
			#endregion

			#region exports
#pragma warning disable CS0649
			[Export(typeof(ClassificationTypeDefinition))]
			[Name(ParenDefinitionCfd_10.Name)]
			internal static ClassificationTypeDefinition ParenDefinition;
#pragma warning restore CS0649
			#endregion
		}

		[Export(typeof(EditorFormatDefinition))]
		[ClassificationType(ClassificationTypeNames = Name)]
		[Name(Name)]
		[UserVisible(true)]
		[Order(After = Priority.Default)]
		internal class ParenDefinitionCfd_11 : ClassificationFormatDefinition
		{
			#region constants
			public const string Name = "Gipper.Cfd.ParenDefinition_11";
			#endregion region

			#region constructors
			public ParenDefinitionCfd_11()
			{
				DisplayName = "Paren Definition 11";
				FontTypeface = StyleHelper.DefinitionFontFace;
				FontRenderingSize = StyleHelper.ParenFontRenderingSizes[11];
			}
			#endregion

			#region exports
#pragma warning disable CS0649
			[Export(typeof(ClassificationTypeDefinition))]
			[Name(ParenDefinitionCfd_11.Name)]
			internal static ClassificationTypeDefinition ParenDefinition;
#pragma warning restore CS0649
			#endregion
		}

		[Export(typeof(EditorFormatDefinition))]
		[ClassificationType(ClassificationTypeNames = Name)]
		[Name(Name)]
		[UserVisible(true)]
		[Order(After = Priority.Default)]
		internal class ParenDefinitionCfd_12 : ClassificationFormatDefinition
		{
			#region constants
			public const string Name = "Gipper.Cfd.ParenDefinition_12";
			#endregion region

			#region constructors
			public ParenDefinitionCfd_12()
			{
				DisplayName = "Paren Definition 12";
				FontTypeface = StyleHelper.DefinitionFontFace;
				FontRenderingSize = StyleHelper.ParenFontRenderingSizes[12];
			}
			#endregion

			#region exports
#pragma warning disable CS0649
			[Export(typeof(ClassificationTypeDefinition))]
			[Name(ParenDefinitionCfd_12.Name)]
			internal static ClassificationTypeDefinition ParenDefinition;
#pragma warning restore CS0649
			#endregion
		}

		[Export(typeof(EditorFormatDefinition))]
		[ClassificationType(ClassificationTypeNames = Name)]
		[Name(Name)]
		[UserVisible(true)]
		[Order(After = Priority.Default)]
		internal class ParenDefinitionCfd_13 : ClassificationFormatDefinition
		{
			#region constants
			public const string Name = "Gipper.Cfd.ParenDefinition_13";
			#endregion region

			#region constructors
			public ParenDefinitionCfd_13()
			{
				DisplayName = "Paren Definition 13";
				FontTypeface = StyleHelper.DefinitionFontFace;
				FontRenderingSize = StyleHelper.ParenFontRenderingSizes[13];
			}
			#endregion

			#region exports
#pragma warning disable CS0649
			[Export(typeof(ClassificationTypeDefinition))]
			[Name(ParenDefinitionCfd_13.Name)]
			internal static ClassificationTypeDefinition ParenDefinition;
#pragma warning restore CS0649
			#endregion
		}

		[Export(typeof(EditorFormatDefinition))]
		[ClassificationType(ClassificationTypeNames = Name)]
		[Name(Name)]
		[UserVisible(true)]
		[Order(After = Priority.Default)]
		internal class ParenDefinitionCfd_14 : ClassificationFormatDefinition
		{
			#region constants
			public const string Name = "Gipper.Cfd.ParenDefinition_14";
			#endregion region

			#region constructors
			public ParenDefinitionCfd_14()
			{
				DisplayName = "Paren Definition 14";
				FontTypeface = StyleHelper.DefinitionFontFace;
				FontRenderingSize = StyleHelper.ParenFontRenderingSizes[14];
			}
			#endregion

			#region exports
#pragma warning disable CS0649
			[Export(typeof(ClassificationTypeDefinition))]
			[Name(ParenDefinitionCfd_14.Name)]
			internal static ClassificationTypeDefinition ParenDefinition;
#pragma warning restore CS0649
			#endregion
		}

		[Export(typeof(EditorFormatDefinition))]
		[ClassificationType(ClassificationTypeNames = Name)]
		[Name(Name)]
		[UserVisible(true)]
		[Order(After = Priority.Default)]
		internal class ParenDefinitionCfd_15 : ClassificationFormatDefinition
		{
			#region constants
			public const string Name = "Gipper.Cfd.ParenDefinition_15";
			#endregion region

			#region constructors
			public ParenDefinitionCfd_15()
			{
				DisplayName = "Paren Definition 15";
				FontTypeface = StyleHelper.DefinitionFontFace;
				FontRenderingSize = StyleHelper.ParenFontRenderingSizes[15];
			}
			#endregion

			#region exports
#pragma warning disable CS0649
			[Export(typeof(ClassificationTypeDefinition))]
			[Name(ParenDefinitionCfd_15.Name)]
			internal static ClassificationTypeDefinition ParenDefinition;
#pragma warning restore CS0649
			#endregion
		}
	}
}