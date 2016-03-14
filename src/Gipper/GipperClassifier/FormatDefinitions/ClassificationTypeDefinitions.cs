using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Media;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;


namespace Gipper
{
	static internal class ClassificationTypeDefinitions
	{
		[Export(typeof(ClassificationTypeDefinition))]
		[Name(GipperConstants.FormatDefinition_Literal_Name)]
		internal static ClassificationTypeDefinition LiteralTypeDefinition = null;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name(GipperConstants.FormatDefinition_Comment_Name)]
		internal static ClassificationTypeDefinition CommentTypeDefinition = null;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name(GipperConstants.FormatDefinition_Comment_ToDo_Name)]
		internal static ClassificationTypeDefinition ToDoCommentTypeDefinition = null;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name(GipperConstants.FormatDefinition_Comment_BugBug_Name)]
		internal static ClassificationTypeDefinition BugBugCommentTypeDefinition = null;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name(GipperConstants.FormatDefinition_Comment_HackHack_Name)]
		internal static ClassificationTypeDefinition HackHackCommentTypeDefinition = null;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name(GipperConstants.FormatDefinition_Declaration_Namespace_Name)]
		internal static ClassificationTypeDefinition NamespaceDeclarationTypeDefinition = null;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name(GipperConstants.FormatDefinition_Declaration_Type_Name)]
		internal static ClassificationTypeDefinition TypeDeclarationTypeDefinition = null;

		[Export(typeof(ClassificationTypeDefinition))]
		[Name(GipperConstants.FormatDefinition_Declaration_Member_Name)]
		internal static ClassificationTypeDefinition MemberDeclarationTypeDefinition = null;
	}
}
