// CommonComponents.AssemblyLoadFromGacEventArgs
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
namespace MyNamespace
{
    public sealed class AssemblyCacheOptimizeIndexResult
    {
        public readonly List<string> BadImageFormatAssemblyPaths = new List<string>();

        public readonly List<KeyValuePair<string, Version>> UnsupportedTargetFrameworkAssemblies = new List<KeyValuePair<string, Version>>();

        public readonly List<KeyValuePair<string, Exception>> TargetFrameworkAnalysisExceptions = new List<KeyValuePair<string, Exception>>();

        public readonly List<Exception> CriticalExceptions = new List<Exception>();

        public readonly Stopwatch LoadAndCollectReferencesTime = new Stopwatch();
    }

    // CommonComponents.AssemblyLoadFromGacEventHandler
    public sealed class AssemblyCacheIndexationResult
    {
        public readonly List<string> BadImageFormatAssemblyPaths = new List<string>();

        public readonly Dictionary<string, List<FileInfo>> DuplicateIdenticalAssemblies = new Dictionary<string, List<FileInfo>>(StringComparer.OrdinalIgnoreCase);

        public readonly Dictionary<string, List<AssemblyDuplicateInfo>> DuplicateDifferentAssemblies = new Dictionary<string, List<AssemblyDuplicateInfo>>(StringComparer.OrdinalIgnoreCase);

        public readonly List<Exception> CriticalExceptions = new List<Exception>();

        public int IndexedAssembliesCount;

        public readonly Stopwatch IndexationTime = new Stopwatch();
    }

    public sealed class AssemblyDuplicateInfo
    {
        public readonly AssemblyName AssemblyName;

        public readonly FileInfo AssemblyFileInfo;

        public readonly byte[] AssemblyHash;

        public AssemblyDuplicateInfo(AssemblyName assemblyName, FileInfo assemblyFileInfo, byte[] assemblyHash)
        {
            if (object.ReferenceEquals(assemblyName, null))
            {
                throw new ArgumentNullException("assemblyName");
            }
            if (object.ReferenceEquals(assemblyFileInfo, null))
            {
                throw new ArgumentNullException("assemblyFileInfo");
            }
            if (object.ReferenceEquals(assemblyHash, null))
            {
                throw new ArgumentNullException("assemblyHash");
            }
            this.AssemblyName = assemblyName;
            this.AssemblyFileInfo = assemblyFileInfo;
            this.AssemblyHash = assemblyHash;
        }

        public sealed class AssemblyLoadFromGacEventArgs : EventArgs
        {




            public delegate void AssemblyLoadFromGacEventHandler(object sender, AssemblyLoadFromGacEventArgs e);

            public string RequestedAssemblyFilePath { get; private set; }

            public Assembly LoadedAssembly { get; private set; }

            public bool ReflectionOnly { get; private set; }

            internal AssemblyLoadFromGacEventArgs(string requestedAssemblyFilePath, Assembly loadedAssembly,
                bool reflectionOnly)
            {
                this.RequestedAssemblyFilePath = requestedAssemblyFilePath;
                this.LoadedAssembly = loadedAssembly;
                this.ReflectionOnly = reflectionOnly;
            }
        }
    }
}