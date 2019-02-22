using System;
using System.Diagnostics;


namespace Gipper
{
	static internal class GipperConstants
	{
		// formats for our custom classification types (IMPORTANT: Don't forget to add fields to ClassificationTypeDefinitions!)
		public const string FormatDefinition_Comment_Name = "Gipper.Comment";
		public const string FormatDefinition_Comment_DisplayName = "Gipper - Comment";
		public const string FormatDefinition_Comment_ToDo_Name = "Gipper.Comment.ToDo";
		public const string FormatDefinition_Comment_ToDo_DisplayName = "Gipper - Comment - TODO";
		public const string FormatDefinition_Comment_BugBug_Name = "Gipper.Comment.BugBug";
		public const string FormatDefinition_Comment_BugBug_DisplayName = "Gipper - Comment - BUGBUG";
		public const string FormatDefinition_Comment_HackHack_Name = "Gipper.Comment.HackHack";
		public const string FormatDefinition_Comment_HackHack_DisplayName = "Gipper - Comment - HACKHACK";
		public const string FormatDefinition_Declaration_Namespace_Name = "Gipper.Declaration.Namespace";
		public const string FormatDefinition_Declaration_Namespace_DisplayName = "Gipper - Declaration - Namespace";
		public const string FormatDefinition_Declaration_Type_Name = "Gipper.Declaration.Type";
		public const string FormatDefinition_Declaration_Type_DisplayName = "Gipper - Declaration - Type";
		public const string FormatDefinition_Declaration_Member_Name = "Gipper.Declaration.Member";
		public const string FormatDefinition_Declaration_Member_DisplayName = "Gipper - Declaration - Member";
		public const string FormatDefinition_Literal_Name = "Gipper.Literal";
		public const string FormatDefinition_Literal_DisplayName = "Gipper - Literal";

		// default classification types (fully qualified using Helper.FormatClassificationType)
		public const string FormatClassificationType_Comment = "formal language > comment";
		public const string FormatClassificationType_Keyword = "formal language > keyword";
		public const string FormatClassificationType_Identifier = "formal language > identifier";
		public const string FormatClassificationType_Operator = "formal language > operator";
		public const string FormatClassificationType_ClassName = "formal language > class name";
		public const string FormatClassificationType_StructName = "formal language > struct name";
		public const string FormatClassificationType_InterfaceName = "formal language > interface name";
		public const string FormatClassificationType_EnumName = "formal language > enum name";
		public const string FormatClassificationType_DelegateName = "formal language > delegate name";
		public const string FormatClassificationType_UserTypes = "formal language > User Types";
		public const string FormatClassificationType_Literal = "formal language > literal";
		public const string FormatClassificationType_Literal_Number = "formal language > literal > number";
		public const string FormatClassificationType_Literal_String = "formal language > literal > string";
		public const string FormatClassificationType_XmlDocComment = "formal language > xml doc comment";
		public const string FormatClassificationType_XmlDocTag = "formal language > XML Doc Tag";
	}
}
