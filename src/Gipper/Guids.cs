// Guids.cs
// MUST match guids.h
using System;

namespace Gipper
{
    static class GuidList
    {
        public const string guidGipperPackagePkgString = "e802ad4f-aad1-451f-b672-ec01f121113f";
        public const string guidGipperPackageCmdSetString = "47b2cc9b-0725-4caf-855a-75ab30c9053a";
        public const string guidToolWindowPersistanceString = "e685a747-28ee-4b43-8821-81dea0028cdd";

        public static readonly Guid guidGipperPackageCmdSet = new Guid(guidGipperPackageCmdSetString);
    };
}