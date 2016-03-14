// PkgCmdID.cs
// MUST match PkgCmdID.h
using System;

namespace Gipper
{
	static class PkgCmdIDList
	{
		public const uint cmdidGipperOpenFilesToolWindow =    0x101;
		public const uint cmdidGipperFileNavigatorWindow = 0x102;
		public const uint cmdidGipperAddFile = 0x103;
		public const uint cmdidGipperVersionControl = 0x104;
		public const uint cmdidGipperToolWindows = 0x105;
		public const uint cmdidCommands = 0x106;
		public const uint cmdidShellToCurrentFileDirectory = 0x107;
		public const uint cmdidShellToSolutionDirectory = 0x108;
		public const uint cmdidExploreCurrentFileDirectory = 0x109;
		public const uint cmdidExploreSolutionDirectory = 0x10A;
		public const uint cmdidFind = 0x10B;
		public const uint cmdidFindInFiles = 0x10C;
		public const uint cmdidReplace = 0x10D;
		public const uint cmdidReplaceInFiles = 0x10E;
		public const uint cmdidRunSolution = 0x10F;
		public const uint cmdidShowClassifications = 0x110;
	};
}