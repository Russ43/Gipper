using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Media;

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Gipper
{
	internal class GipperClassifier : IClassifier
	{
		// events
#pragma warning disable 67
		public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore 67


		// fields
		IClassificationType _literalClassificationType;
		IClassificationType _commentClassificationType;
		IClassificationType _todoCommentClassificationType;
		IClassificationType _bugBugCommentClassificationType;
		IClassificationType _hackHackCommentClassificationType;
		IClassificationType _namespaceDeclarationClassificationType;
		IClassificationType _typeDeclarationClassificationType;
		IClassificationType _memberDeclarationClassificationType;
		IClassifier _aggregateClassifier;


		// constructors
		internal GipperClassifier(IClassificationTypeRegistryService registry, IClassifier aggregateClassifier)
		{
			_literalClassificationType = registry.GetClassificationType(GipperConstants.FormatDefinition_Literal_Name);
			_commentClassificationType = registry.GetClassificationType(GipperConstants.FormatDefinition_Comment_Name);
			_todoCommentClassificationType = registry.GetClassificationType(GipperConstants.FormatDefinition_Comment_ToDo_Name);
			_bugBugCommentClassificationType = registry.GetClassificationType(GipperConstants.FormatDefinition_Comment_BugBug_Name);
			_hackHackCommentClassificationType = registry.GetClassificationType(GipperConstants.FormatDefinition_Comment_HackHack_Name);
			_namespaceDeclarationClassificationType = registry.GetClassificationType(GipperConstants.FormatDefinition_Declaration_Namespace_Name);
			_typeDeclarationClassificationType = registry.GetClassificationType(GipperConstants.FormatDefinition_Declaration_Type_Name);
			_memberDeclarationClassificationType = registry.GetClassificationType(GipperConstants.FormatDefinition_Declaration_Member_Name);

			_aggregateClassifier = aggregateClassifier;

			// an share it with other things like ShowClassificationsCommand
			GipperPackage.ClassifierAggregatorService = aggregateClassifier;
		}


		// IClassifier methods
		public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
		{
			IList<ClassificationSpan> classificationSpans = Helper.TrapExceptions(
				() => InternalGetClassificationSpans(span),
				new ClassificationSpan[0],
				"Gipper Classifier"
			);
			return classificationSpans;
		}


		// static private methods
		private IList<ClassificationSpan> InternalGetClassificationSpans(SnapshotSpan span)
		{
			IList<ClassificationSpan> existingSpans = _aggregateClassifier.GetClassificationSpans(span);
			IList<ClassificationSpan> newSpans = new List<ClassificationSpan>();

			// TODO: uncomment this line to help debug classification issues
			//Helper.WriteClassifierSnapshotToDebug(span, existingSpans);

			foreach(ClassificationSpan existingSpan in existingSpans)
			{
				// literals
				if(isLiteral(existingSpan))
				{
					ClassificationSpan newSpan = new ClassificationSpan(existingSpan.Span, _literalClassificationType);
					newSpans.Add(newSpan);
				}

				// comments
				if(IsComment(existingSpan))
				{
					ClassificationSpan newSpan = new ClassificationSpan(existingSpan.Span, _commentClassificationType);
					newSpans.Add(newSpan);
				}

				// special comments
				if(IsTodoComment(existingSpan))
				{
					ClassificationSpan newSpan = new ClassificationSpan(existingSpan.Span, _todoCommentClassificationType);
					newSpans.Add(newSpan);
				}
				else if(IsBugBugComment(existingSpan))
				{
					ClassificationSpan newSpan = new ClassificationSpan(existingSpan.Span, _bugBugCommentClassificationType);
					newSpans.Add(newSpan);
				}
				else if(IsHackHackComment(existingSpan))
				{
					ClassificationSpan newSpan = new ClassificationSpan(existingSpan.Span, _hackHackCommentClassificationType);
					newSpans.Add(newSpan);
				}

				// declarations
				if(IsNamespaceDeclaration(existingSpan, existingSpans))
				{
					ClassificationSpan newSpan = new ClassificationSpan(existingSpan.Span, _namespaceDeclarationClassificationType);
					newSpans.Add(newSpan);
				}
				else if(IsTypeDeclaration(existingSpan, existingSpans))
				{
					ClassificationSpan newSpan = new ClassificationSpan(existingSpan.Span, _typeDeclarationClassificationType);
					newSpans.Add(newSpan);
				}
				else if(IsMemberDeclaration(existingSpan, existingSpans))
				{
					ClassificationSpan newSpan = new ClassificationSpan(existingSpan.Span, _memberDeclarationClassificationType);
					newSpans.Add(newSpan);
				}
			}

			return newSpans;
		}

		static private bool isLiteral(ClassificationSpan span)
		{
			bool isLiteral = false;
			string formattedClassificationType = Helper.FormatClassificationType(span.ClassificationType);
			if(formattedClassificationType.StartsWith(GipperConstants.FormatClassificationType_Literal))
				isLiteral = true;

			return isLiteral;
		}

		static private bool IsComment(ClassificationSpan span)
		{
			bool isComment = false;
			string formattedClassificationType = Helper.FormatClassificationType(span.ClassificationType);
			if(	formattedClassificationType.StartsWith(GipperConstants.FormatClassificationType_Comment))
				isComment = true;

			return isComment;
		}

		static private bool IsTodoComment(ClassificationSpan span)
		{
			bool isTodoComment = false;
			// TODO: 
			if(IsComment(span))
			{
				if(span.Span.GetText().ToUpper().StartsWith("// TODO"))
					isTodoComment = true;
			}

			return isTodoComment;
		}

		static private bool IsBugBugComment(ClassificationSpan span)
		{
			bool isBugBugComment = false;
			if(IsComment(span))
			{
				if(span.Span.GetText().ToUpper().StartsWith("// BUGBUG"))
					isBugBugComment = true;
			}

			return isBugBugComment;
		}

		static private bool IsHackHackComment(ClassificationSpan span)
		{
			bool isHackHackComment = false;
			if(IsComment(span))
			{
				if(span.Span.GetText().ToUpper().StartsWith("// HACKHACK"))
					isHackHackComment = true;
			}

			return isHackHackComment;
		}

		static private bool IsNamespaceDeclaration(ClassificationSpan classificationSpan, IList<ClassificationSpan> snapshotSpans)
		{
			bool isNamepaceDeclaration = false;

			string formattedClassificationType = Helper.FormatClassificationType(classificationSpan.ClassificationType);
			if(
				formattedClassificationType == GipperConstants.FormatClassificationType_Identifier
				|| formattedClassificationType == GipperConstants.FormatClassificationType_UserTypes
			)
			{
				foreach(ClassificationSpan span in snapshotSpans)
				{
					if(Helper.FormatClassificationType(span.ClassificationType) == GipperConstants.FormatClassificationType_Keyword)
					{
						string text = span.Span.GetText();
						if(text == "namespace")
						{
							isNamepaceDeclaration = true;
							break;
						}
					}
				}
			}

			return isNamepaceDeclaration;
		}

		static private bool IsTypeDeclaration(ClassificationSpan classificationSpan, IList<ClassificationSpan> snapshotSpans)
		{
			bool isTypeDeclaration = false;

			string formattedClassificationType = Helper.FormatClassificationType(classificationSpan.ClassificationType);
			if(
				formattedClassificationType == GipperConstants.FormatClassificationType_ClassName
				|| formattedClassificationType == GipperConstants.FormatClassificationType_StructName
				|| formattedClassificationType == GipperConstants.FormatClassificationType_InterfaceName
				|| formattedClassificationType == GipperConstants.FormatClassificationType_EnumName
				|| formattedClassificationType == GipperConstants.FormatClassificationType_DelegateName
			)
			{
				isTypeDeclaration = true;
			}

			return isTypeDeclaration;
		}

		static private bool IsMemberDeclaration(ClassificationSpan classificationSpan, IList<ClassificationSpan> snapshotSpans)
		{
			// TODO: add way more criteria than simply checked the visiblity keyword
			bool hasVisibilityKeyword = false;

			string formattedClassificationType = Helper.FormatClassificationType(classificationSpan.ClassificationType);
			if(formattedClassificationType == GipperConstants.FormatClassificationType_Identifier)
			{
				foreach(ClassificationSpan span in snapshotSpans)
				{
					if(Helper.FormatClassificationType(span.ClassificationType) == GipperConstants.FormatClassificationType_Keyword)
					{
						string text = span.Span.GetText();
						if(text == "private" || text == "public" || text == "internal" || text == "function" || text == "void" || text == "event")
						{
							hasVisibilityKeyword = true;
							break;
						}
					}
				}
			}

			return hasVisibilityKeyword;
		}
	}
}
