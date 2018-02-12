
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using MyNamespace;

[Serializable]
public sealed class AssemblyCache
{
    [Serializable]
    private class AssemblyInfo
    {
        public short AssemblyRelativeDirectoryIndex;

        public string AssemblyFileNameWithoutExtension;

        public bool IsExe;

        public short[] ReferencedAssemblies;

        [NonSerialized]
        public Assembly Assembly;

        [NonSerialized]
        public Assembly ReflectionOnlyAssembly;

        public override string ToString()
        {
            return this.AssemblyFileNameWithoutExtension + " References: " + (object.ReferenceEquals(this.ReferencedAssemblies, null) ? "null" : this.ReferencedAssemblies.Length.ToString(CultureInfo.InvariantCulture)) + " Name: " + (object.ReferenceEquals(this.Assembly, null) ? "null" : this.Assembly.FullName);
        }
    }

    [Serializable]
    private sealed class IndexationAssemblyInfo : AssemblyInfo
    {
        [NonSerialized]
        public AssemblyName AssemblyName;

        [NonSerialized]
        public BitArray ReferencedAssembliesMask;

        [NonSerialized]
        public FileInfo AssemblyFileInfo;

        [NonSerialized]
        public string AssemblyFileFullPath;

        [NonSerialized]
        public byte[] AssemblyHash;
    }

    public const string DefaultStorageFileName = "AssemblyCache.dat";

    public const string DomainDataName = "AssemblyCache";

    private const string DllExtension = ".dll";

    private const string ExeExtension = ".exe";

    private const string DllSearchPattern = "*.dll";

    private const string ExeSearchPattern = "*.exe";

    private static readonly short[] EmptyShortArray = new short[0];

    private static readonly Type TypeOfTargetFrameworkAttribute = typeof(TargetFrameworkAttribute);

    private readonly string basePath;

    private readonly SortedList<string, short> knownAssembliesMap;

    private readonly AssemblyInfo[] knownAssemblies;

    private readonly string[] knownPaths;

    public event AssemblyDuplicateInfo.AssemblyLoadFromGacEventArgs.AssemblyLoadFromGacEventHandler AssemblyLoadFromGac;

    public event AssemblyDuplicateInfo.AssemblyLoadFromGacEventArgs.AssemblyLoadFromGacEventHandler ReflectionOnlyAssemblyLoadFromGac;

    private AssemblyCache(string basePath, string[] knownPaths, AssemblyInfo[] knownAssemblies, SortedList<string, short> knownAssembliesMap)
    {
        if (string.IsNullOrWhiteSpace(basePath))
        {
            throw new ArgumentNullException("basePath");
        }
        if (object.ReferenceEquals(knownPaths, null))
        {
            throw new ArgumentNullException("knownPaths");
        }
        if (object.ReferenceEquals(knownAssemblies, null))
        {
            throw new ArgumentNullException("knownAssemblies");
        }
        if (object.ReferenceEquals(knownAssembliesMap, null))
        {
            throw new ArgumentNullException("knownAssembliesMap");
        }
        this.basePath = basePath;
        this.knownPaths = knownPaths;
        this.knownAssemblies = knownAssemblies;
        this.knownAssembliesMap = knownAssembliesMap;
    }

    private Assembly ResolveAssembly(ResolveEventArgs args)
    {
        if (object.ReferenceEquals(args, null))
        {
            throw new ArgumentNullException("args");
        }
        AssemblyName assemblyName = new AssemblyName(args.Name);
        string name = assemblyName.Name;
        short num = default(short);
        if (!this.knownAssembliesMap.TryGetValue(name, out num))
        {
            if (!name.EndsWith(".XmlSerializers", StringComparison.OrdinalIgnoreCase) && !name.EndsWith(".resources", StringComparison.OrdinalIgnoreCase))
            {
                throw new FileNotFoundException(string.Format("Сборка [{0}] не найдена в кэше сборок КПИ. Вероятные причины: сборка не существует или текущий индекс сборок устарел.", name));
            }
            return null;
        }
        AssemblyInfo assemblyInfo = this.knownAssemblies[num];
        Assembly orLoadAssemblyAndItsReferences = this.GetOrLoadAssemblyAndItsReferences(assemblyInfo);
        AssemblyCache.CheckAssemblyAndThrow(assemblyName, orLoadAssemblyAndItsReferences.GetName());
        return orLoadAssemblyAndItsReferences;
    }

    public Assembly ResolveAssembly(AssemblyName assemblyRef, bool throwOnError = true)
    {
        if (object.ReferenceEquals(assemblyRef, null))
        {
            if (throwOnError)
            {
                throw new ArgumentNullException("assemblyRef");
            }
            return null;
        }
        string name = assemblyRef.Name;
        short num = default(short);
        if (!this.knownAssembliesMap.TryGetValue(name, out num))
        {
            if (throwOnError)
            {
                throw new FileNotFoundException(string.Format("Сборка [{0}] не найдена в кэше сборок КПИ. Вероятные причины: сборка не существует или текущий индекс сборок устарел.", assemblyRef.FullName));
            }
            return null;
        }
        AssemblyInfo assemblyInfo = this.knownAssemblies[num];
        Assembly orLoadAssemblyAndItsReferences;
        try
        {
            orLoadAssemblyAndItsReferences = this.GetOrLoadAssemblyAndItsReferences(assemblyInfo);
        }
        catch (FileNotFoundException)
        {
            if (throwOnError)
            {
                throw;
            }
            return null;
        }
        Exception ex2 = default(Exception);
        if (!AssemblyCache.CheckAssembly(assemblyRef, orLoadAssemblyAndItsReferences.GetName(), out ex2))
        {
            if (throwOnError)
            {
                throw ex2;
            }
            return null;
        }
        return orLoadAssemblyAndItsReferences;
    }

    private Assembly ResolveAssemblyReflectionOnly(ResolveEventArgs args)
    {
        if (object.ReferenceEquals(args, null))
        {
            throw new ArgumentNullException("args");
        }
        AssemblyName assemblyName = new AssemblyName(args.Name);
        string name = assemblyName.Name;
        short num = default(short);
        if (!this.knownAssembliesMap.TryGetValue(name, out num))
        {
            return Assembly.ReflectionOnlyLoad(args.Name);
        }
        AssemblyInfo assemblyInfo = this.knownAssemblies[num];
        Assembly orLoadAssemblyAndItsReferencesReflectionOnly = this.GetOrLoadAssemblyAndItsReferencesReflectionOnly(assemblyInfo);
        AssemblyCache.CheckAssemblyAndThrow(assemblyName, orLoadAssemblyAndItsReferencesReflectionOnly.GetName());
        return orLoadAssemblyAndItsReferencesReflectionOnly;
    }

    public Assembly ResolveAssemblyReflectionOnly(AssemblyName assemblyRef, bool throwOnError = true)
    {
        if (object.ReferenceEquals(assemblyRef, null))
        {
            if (throwOnError)
            {
                throw new ArgumentNullException("assemblyRef");
            }
            return null;
        }
        string name = assemblyRef.Name;
        short num = default(short);
        if (!this.knownAssembliesMap.TryGetValue(name, out num))
        {
            if (throwOnError)
            {
                throw new FileNotFoundException(string.Format("Сборка [{0}] не найдена в кэше сборок КПИ. Вероятные причины: сборка не существует или текущий индекс сборок устарел.", assemblyRef.FullName));
            }
            return null;
        }
        AssemblyInfo assemblyInfo = this.knownAssemblies[num];
        Assembly orLoadAssemblyAndItsReferencesReflectionOnly;
        try
        {
            orLoadAssemblyAndItsReferencesReflectionOnly = this.GetOrLoadAssemblyAndItsReferencesReflectionOnly(assemblyInfo);
        }
        catch (FileNotFoundException)
        {
            if (throwOnError)
            {
                throw;
            }
            return null;
        }
        Exception ex2 = default(Exception);
        if (!AssemblyCache.CheckAssembly(assemblyRef, orLoadAssemblyAndItsReferencesReflectionOnly.GetName(), out ex2))
        {
            if (throwOnError)
            {
                throw ex2;
            }
            return null;
        }
        return orLoadAssemblyAndItsReferencesReflectionOnly;
    }

    public Assembly GetAssemblyById(short assemblyId, bool loadReferences = true, bool throwOnError = true)
    {
        int num = this.knownAssemblies.Length;
        if (assemblyId >= num)
        {
            if (throwOnError)
            {
                throw new ArgumentOutOfRangeException("assemblyId", string.Format("Некорректный идентификатор сборки для загрузки: значение [{0}] не поддерживается. Значение идентификатора сборки должно находиться в пределах от 0 до {1}.", assemblyId, num));
            }
            return null;
        }
        AssemblyInfo assemblyInfo = this.knownAssemblies[assemblyId];
        try
        {
            return loadReferences ? this.GetOrLoadAssemblyAndItsReferences(assemblyInfo) : this.GetOrLoadAssembly(assemblyInfo);
        }
        catch (FileNotFoundException)
        {
            if (throwOnError)
            {
                throw;
            }
            return null;
        }
    }

    public Assembly GetAssemblyByIdReflectionOnly(short assemblyId, bool throwOnError = true)
    {
        int num = this.knownAssemblies.Length;
        if (assemblyId >= num)
        {
            if (throwOnError)
            {
                throw new ArgumentOutOfRangeException("assemblyId", string.Format("Некорректный идентификатор сборки для загрузки: значение [{0}] не поддерживается. Значение идентификатора сборки должно находиться в пределах от 0 до {1}.", assemblyId, num));
            }
            return null;
        }
        AssemblyInfo assemblyInfo = this.knownAssemblies[assemblyId];
        try
        {
            return this.GetOrLoadAssemblyAndItsReferencesReflectionOnly(assemblyInfo);
        }
        catch (FileNotFoundException)
        {
            if (throwOnError)
            {
                throw;
            }
            return null;
        }
    }

    public short GetAssemblyId(AssemblyName assemblyName)
    {
        short result = default(short);
        if (!this.TryGetAssemblyId(assemblyName, out result))
        {
            throw new InvalidOperationException(string.Format("Сборка с именем [{0}] не зарегистрирована в каталоге сборок.", assemblyName.FullName));
        }
        return result;
    }

    public bool TryGetAssemblyId(AssemblyName assemblyName, out short assemblyId)
    {
        return this.knownAssembliesMap.TryGetValue(assemblyName.Name, out assemblyId);
    }

    public static AssemblyCacheIndexationResult MakeIndex(string basePath, out AssemblyCache indexedAssemblyCache)
    {
        if (string.IsNullOrWhiteSpace(basePath))
        {
            throw new ArgumentNullException("basePath");
        }
        AssemblyCacheIndexationResult assemblyCacheIndexationResult = new AssemblyCacheIndexationResult();
        assemblyCacheIndexationResult.IndexationTime.Start();
        indexedAssemblyCache = null;
        try
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(basePath);
            if (!directoryInfo.Exists)
            {
                assemblyCacheIndexationResult.CriticalExceptions.Add(new DirectoryNotFoundException("Корневая папка [" + basePath + "] для индексации сборок не найдена."));
                return assemblyCacheIndexationResult;
            }
            basePath = directoryInfo.FullName;
            basePath = basePath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            int length = basePath.Length;
            List<string> list = new List<string>(64);
            string[] files = Directory.GetFiles(basePath, "*.dll", SearchOption.AllDirectories);
            int num = files.Length;
            string[] files2 = Directory.GetFiles(basePath, "*.exe", SearchOption.AllDirectories);
            int num2 = files2.Length;
            int num3 = num + num2;
            AssemblyInfo[] array = new AssemblyInfo[num3];
            short num4 = 0;
            SortedList<string, short> sortedList = new SortedList<string, short>(num3 + 2, StringComparer.OrdinalIgnoreCase);
            AssemblyCache.MakeIndexInternal(files, num, assemblyCacheIndexationResult, length, sortedList, array, list, ref num4, false);
            AssemblyCache.MakeIndexInternal(files2, num2, assemblyCacheIndexationResult, length, sortedList, array, list, ref num4, true);
            Array.Resize(ref array, num4);
            indexedAssemblyCache = new AssemblyCache(basePath, list.ToArray(), array, sortedList);
            assemblyCacheIndexationResult.IndexedAssembliesCount = num4;
            return assemblyCacheIndexationResult;
        }
        catch (Exception item)
        {
            assemblyCacheIndexationResult.CriticalExceptions.Add(item);
            return assemblyCacheIndexationResult;
        }
        finally
        {
            assemblyCacheIndexationResult.IndexationTime.Stop();
        }
    }

    private static void MakeIndexInternal(string[] assemblyFilePaths, int assemblyFilePathsLength, AssemblyCacheIndexationResult result, int basePathLength, SortedList<string, short> knownAssembliesMap, AssemblyInfo[] knownAssemblies, List<string> knownPaths, ref short knownAssembliesCount, bool isExeFile)
    {
        for (int i = 0; i < assemblyFilePathsLength; i++)
        {
            string fullPath = Path.GetFullPath(assemblyFilePaths[i]);
            string directoryName = Path.GetDirectoryName(fullPath);
            if (object.ReferenceEquals(directoryName, null))
            {
                result.CriticalExceptions.Add(new DirectoryNotFoundException("Не удалось определить папку расположения сборки [" + fullPath + "]."));
            }
            else
            {
                string assemblyRelativeDirectory = directoryName.Substring(basePathLength).Trim(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                AssemblyName assemblyName;
                try
                {
                    assemblyName = AssemblyName.GetAssemblyName(fullPath);
                }
                catch (BadImageFormatException)
                {
                    result.BadImageFormatAssemblyPaths.Add(fullPath);
                    continue;
                }
                catch (Exception inner)
                {
                    result.CriticalExceptions.Add(new FileLoadException("Не удалось получить CLR-имя сборки [" + fullPath + "].", fullPath, inner));
                    continue;
                }
                byte[] assemblyHash = default(byte[]);
                try
                {
                    using (MD5 mD = MD5.Create())
                    {
                        using (FileStream fileStream = File.OpenRead(fullPath))
                        {
                            assemblyHash = mD.ComputeHash(fileStream);
                            fileStream.Close();
                        }
                    }
                }
                catch (Exception innerException)
                {
                    result.CriticalExceptions.Add(new IOException("Не удалось выполнить расчет хэша сборки [" + fullPath + "].", innerException));
                    continue;
                }
                string name = assemblyName.Name;
                short num = default(short);
                if (knownAssembliesMap.TryGetValue(name, out num))
                {
                    IndexationAssemblyInfo indexationAssemblyInfo = (IndexationAssemblyInfo)knownAssemblies[num];
                    if (object.ReferenceEquals(indexationAssemblyInfo.AssemblyFileInfo, null))
                    {
                        indexationAssemblyInfo.AssemblyFileInfo = new FileInfo(indexationAssemblyInfo.AssemblyFileFullPath);
                    }
                    AssemblyCache.AnalyzeAssemblyDuplicate(result, indexationAssemblyInfo, new AssemblyDuplicateInfo(assemblyName, new FileInfo(fullPath), assemblyHash));
                }
                else
                {
                    short num2 = (short)knownPaths.FindIndex((string p) => string.Equals(p, assemblyRelativeDirectory, StringComparison.Ordinal));
                    if (num2 < 0)
                    {
                        num2 = (short)knownPaths.Count;
                        knownPaths.Add(assemblyRelativeDirectory);
                    }
                    IndexationAssemblyInfo indexationAssemblyInfo2 = new IndexationAssemblyInfo();
                    indexationAssemblyInfo2.Assembly = null;
                    indexationAssemblyInfo2.AssemblyName = assemblyName;
                    indexationAssemblyInfo2.AssemblyFileFullPath = fullPath;
                    indexationAssemblyInfo2.AssemblyFileInfo = null;
                    indexationAssemblyInfo2.IsExe = isExeFile;
                    indexationAssemblyInfo2.AssemblyHash = assemblyHash;
                    indexationAssemblyInfo2.AssemblyFileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullPath);
                    indexationAssemblyInfo2.AssemblyRelativeDirectoryIndex = num2;
                    indexationAssemblyInfo2.ReferencedAssemblies = AssemblyCache.EmptyShortArray;
                    IndexationAssemblyInfo indexationAssemblyInfo3 = indexationAssemblyInfo2;
                    knownAssemblies[knownAssembliesCount] = indexationAssemblyInfo3;
                    knownAssembliesMap.Add(name, knownAssembliesCount);
                    knownAssembliesCount++;
                }
            }
        }
    }

    private static void AnalyzeAssemblyDuplicate(AssemblyCacheIndexationResult result, IndexationAssemblyInfo existingAssemblyInfo, AssemblyDuplicateInfo duplicateAssemblyInfo)
    {
        AssemblyName assemblyName = existingAssemblyInfo.AssemblyName;
        AssemblyName assemblyName2 = duplicateAssemblyInfo.AssemblyName;
        if (!AssemblyCache.CheckPublicKeyToken(assemblyName.GetPublicKeyToken(), assemblyName2.GetPublicKeyToken()))
        {
            AssemblyCache.AddDuplicateDifferentAssembly(result, existingAssemblyInfo, duplicateAssemblyInfo);
        }
        else if (!AssemblyCache.CheckCultureInfo(assemblyName.CultureInfo, assemblyName2.CultureInfo))
        {
            AssemblyCache.AddDuplicateDifferentAssembly(result, existingAssemblyInfo, duplicateAssemblyInfo);
        }
        else if (!AssemblyCache.CheckVersion(assemblyName.Version, assemblyName2.Version))
        {
            AssemblyCache.AddDuplicateDifferentAssembly(result, existingAssemblyInfo, duplicateAssemblyInfo);
        }
        else
        {
            FileInfo assemblyFileInfo = existingAssemblyInfo.AssemblyFileInfo;
            FileInfo assemblyFileInfo2 = duplicateAssemblyInfo.AssemblyFileInfo;
            if (assemblyFileInfo.Length != assemblyFileInfo2.Length)
            {
                AssemblyCache.AddDuplicateDifferentAssembly(result, existingAssemblyInfo, duplicateAssemblyInfo);
            }
            else
            {
                byte[] assemblyHash = existingAssemblyInfo.AssemblyHash;
                byte[] assemblyHash2 = duplicateAssemblyInfo.AssemblyHash;
                if (!StructuralComparisons.StructuralEqualityComparer.Equals(assemblyHash, assemblyHash2))
                {
                    AssemblyCache.AddDuplicateDifferentAssembly(result, existingAssemblyInfo, duplicateAssemblyInfo);
                }
                else
                {
                    AssemblyCache.AddDuplicateIdenticalAssembly(result, existingAssemblyInfo, duplicateAssemblyInfo);
                }
            }
        }
    }

    private static void AddDuplicateDifferentAssembly(AssemblyCacheIndexationResult result, IndexationAssemblyInfo existingAssemblyInfo, AssemblyDuplicateInfo duplicateAssemblyInfo)
    {
        List<AssemblyDuplicateInfo> list = default(List<AssemblyDuplicateInfo>);
        if (!result.DuplicateDifferentAssemblies.TryGetValue(existingAssemblyInfo.AssemblyName.Name, out list))
        {
            List<AssemblyDuplicateInfo> list2 = new List<AssemblyDuplicateInfo>(2);
            list2.Add(new AssemblyDuplicateInfo(existingAssemblyInfo.AssemblyName, existingAssemblyInfo.AssemblyFileInfo, existingAssemblyInfo.AssemblyHash));
            list = list2;
            result.DuplicateDifferentAssemblies.Add(existingAssemblyInfo.AssemblyName.Name, list);
        }
        list.Add(duplicateAssemblyInfo);
    }

    private static void AddDuplicateIdenticalAssembly(AssemblyCacheIndexationResult result, IndexationAssemblyInfo existingAssemblyInfo, AssemblyDuplicateInfo duplicateAssemblyInfo)
    {
        List<FileInfo> list = default(List<FileInfo>);
        if (!result.DuplicateIdenticalAssemblies.TryGetValue(existingAssemblyInfo.AssemblyName.Name, out list))
        {
            List<FileInfo> list2 = new List<FileInfo>();
            list2.Add(existingAssemblyInfo.AssemblyFileInfo);
            list = list2;
            result.DuplicateIdenticalAssemblies.Add(existingAssemblyInfo.AssemblyName.Name, list);
        }
        list.Add(duplicateAssemblyInfo.AssemblyFileInfo);
    }

    public AssemblyCacheOptimizeIndexResult OptimizeIndex()
    {
        AssemblyCacheOptimizeIndexResult assemblyCacheOptimizeIndexResult = new AssemblyCacheOptimizeIndexResult();
        assemblyCacheOptimizeIndexResult.LoadAndCollectReferencesTime.Start();
        try
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            Version v = default(Version);
            try
            {
                if (!AssemblyCache.TryGetTargetFrameworkVersion(entryAssembly, out v))
                {
                    assemblyCacheOptimizeIndexResult.CriticalExceptions.Add(new InvalidProgramException("Не удалось получить целевую версию .NET Framework для текущего исполняемого приложения [" + entryAssembly.FullName + "]. " + "Вероятная причина: текущее исполняемое приложение собрано для версии .NET Framework младше 4.0."));
                    return assemblyCacheOptimizeIndexResult;
                }
            }
            catch (Exception ex)
            {
                assemblyCacheOptimizeIndexResult.CriticalExceptions.Add(new InvalidProgramException("Не удалось получить целевую версию .NET Framework для текущего исполняемого приложения [" + entryAssembly.FullName + "]. " + "Вероятные причины: метаданные текущего приложения некорректны или оно собрано для версии .NET Framework младше 4.0.", ex.InnerException));
                return assemblyCacheOptimizeIndexResult;
            }
            int num = this.knownAssemblies.Length;
            for (int i = 0; i < num; i++)
            {
                try
                {
                    IndexationAssemblyInfo indexationAssemblyInfo = (IndexationAssemblyInfo)this.knownAssemblies[i];
                    Assembly orLoadAssembly;
                    try
                    {
                        orLoadAssembly = this.GetOrLoadAssembly(indexationAssemblyInfo);
                    }
                    catch (BadImageFormatException)
                    {
                        assemblyCacheOptimizeIndexResult.BadImageFormatAssemblyPaths.Add(Path.Combine(this.basePath, indexationAssemblyInfo.AssemblyFileFullPath));
                        continue;
                    }
                    try
                    {
                        Version version = default(Version);
                        if (AssemblyCache.TryGetTargetFrameworkVersion(orLoadAssembly, out version) && version > v)
                        {
                            assemblyCacheOptimizeIndexResult.UnsupportedTargetFrameworkAssemblies.Add(new KeyValuePair<string, Version>(Path.Combine(this.basePath, indexationAssemblyInfo.AssemblyFileFullPath), version));
                            continue;
                        }
                    }
                    catch (Exception value)
                    {
                        assemblyCacheOptimizeIndexResult.TargetFrameworkAnalysisExceptions.Add(new KeyValuePair<string, Exception>(Path.Combine(this.basePath, indexationAssemblyInfo.AssemblyFileFullPath), value));
                        continue;
                    }
                    indexationAssemblyInfo.ReferencedAssembliesMask = new BitArray(num);
                    AssemblyName[] referencedAssemblies = orLoadAssembly.GetReferencedAssemblies();
                    int num2 = referencedAssemblies.Length;
                    if (num2 >= 1)
                    {
                        short[] array = new short[num2];
                        int num3 = 0;
                        for (int j = 0; j < num2; j++)
                        {
                            AssemblyName assemblyName = referencedAssemblies[j];
                            string name = assemblyName.Name;
                            short num4 = default(short);
                            if (this.knownAssembliesMap.TryGetValue(name, out num4))
                            {
                                array[num3++] = num4;
                                indexationAssemblyInfo.ReferencedAssembliesMask[num4] = true;
                            }
                        }
                        if (num3 >= 1)
                        {
                            if (num3 < num2)
                            {
                                Array.Resize(ref array, num3);
                            }
                            indexationAssemblyInfo.ReferencedAssemblies = array;
                        }
                    }
                }
                catch (Exception item)
                {
                    assemblyCacheOptimizeIndexResult.CriticalExceptions.Add(item);
                }
            }
            short[] array2 = new short[num];
            for (int k = 0; k < num; k++)
            {
                IndexationAssemblyInfo indexationAssemblyInfo2 = (IndexationAssemblyInfo)this.knownAssemblies[k];
                bool flag = indexationAssemblyInfo2.ReferencedAssemblies.Length > 0;
                if (flag)
                {
                    BitArray bitArray = indexationAssemblyInfo2.ReferencedAssembliesMask;
                    while (flag)
                    {
                        flag = false;
                        for (int l = 0; l < num; l++)
                        {
                            if (bitArray[l])
                            {
                                IndexationAssemblyInfo indexationAssemblyInfo3 = (IndexationAssemblyInfo)this.knownAssemblies[l];
                                if (l < k)
                                {
                                    if (!object.ReferenceEquals(indexationAssemblyInfo3.ReferencedAssembliesMask, null))
                                    {
                                        bitArray = bitArray.Or(indexationAssemblyInfo3.ReferencedAssembliesMask);
                                    }
                                }
                                else
                                {
                                    int num6 = indexationAssemblyInfo3.ReferencedAssemblies.Length;
                                    for (int m = 0; m < num6; m++)
                                    {
                                        short index = indexationAssemblyInfo3.ReferencedAssemblies[m];
                                        if (!bitArray[index])
                                        {
                                            bitArray[index] = true;
                                            flag = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    int num7 = 0;
                    for (short num8 = 0; num8 < num; num8 = (short)(num8 + 1))
                    {
                        if (bitArray[num8])
                        {
                            array2[num7++] = num8;
                        }
                    }
                    short[] array3 = new short[num7];
                    Array.Copy(array2, 0, array3, 0, num7);
                    indexationAssemblyInfo2.ReferencedAssemblies = array3;
                }
            }
            return assemblyCacheOptimizeIndexResult;
        }
        catch (Exception item2)
        {
            assemblyCacheOptimizeIndexResult.CriticalExceptions.Add(item2);
            return assemblyCacheOptimizeIndexResult;
        }
        finally
        {
            assemblyCacheOptimizeIndexResult.LoadAndCollectReferencesTime.Stop();
        }
    }

    public static void DeleteIndexFile(string basePath)
    {
        if (string.IsNullOrWhiteSpace(basePath))
        {
            throw new ArgumentNullException("basePath");
        }
        DirectoryInfo directoryInfo = new DirectoryInfo(basePath);
        if (!directoryInfo.Exists)
        {
            throw new DirectoryNotFoundException(string.Format("Корневая папка [{0}] для индексации сборок не найдена.", basePath));
        }
        string path = Path.Combine(basePath, "AssemblyCache.dat");
        for (int i = 0; i < 5; i++)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                return;
            }
            catch (Exception)
            {
                Thread.Sleep(100);
            }
        }
    }

    public void SaveToFile()
    {
        byte[] outputBuffer = new byte[4];
        using (FileStream output = File.Create(Path.Combine(this.basePath, "AssemblyCache.dat")))
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(output))
            {
                using (MD5 mD = MD5.Create())
                {
                    binaryWriter.Write(23);
                    binaryWriter.Write(mD.HashSize);
                    int num = this.knownPaths.Length;
                    AssemblyCache.WriteValidatableInt(binaryWriter, num);
                    mD.TransformBlock(BitConverter.GetBytes(num), 0, 4, outputBuffer, 0);
                    for (int i = 0; i < num; i++)
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(this.knownPaths[i]);
                        AssemblyCache.WriteValidatableInt(binaryWriter, bytes.Length);
                        binaryWriter.Write(bytes);
                        mD.TransformBlock(BitConverter.GetBytes(bytes.Length), 0, 4, outputBuffer, 0);
                        mD.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
                    }
                    AssemblyCache.WriteValidatableInt(binaryWriter, this.knownAssemblies.Length);
                    mD.TransformBlock(BitConverter.GetBytes(this.knownAssemblies.Length), 0, 4, outputBuffer, 0);
                    for (int j = 0; j < this.knownAssemblies.Length; j++)
                    {
                        AssemblyInfo assemblyInfo = this.knownAssemblies[j];
                        byte[] bytes2 = Encoding.UTF8.GetBytes(assemblyInfo.AssemblyFileNameWithoutExtension);
                        AssemblyCache.WriteValidatableInt(binaryWriter, bytes2.Length);
                        binaryWriter.Write(bytes2);
                        mD.TransformBlock(BitConverter.GetBytes(bytes2.Length), 0, 4, outputBuffer, 0);
                        mD.TransformBlock(bytes2, 0, bytes2.Length, bytes2, 0);
                        binaryWriter.Write(assemblyInfo.IsExe);
                        binaryWriter.Write(assemblyInfo.AssemblyRelativeDirectoryIndex);
                        mD.TransformBlock(BitConverter.GetBytes(assemblyInfo.AssemblyRelativeDirectoryIndex), 0, 2, outputBuffer, 0);
                        short[] referencedAssemblies = assemblyInfo.ReferencedAssemblies;
                        int num2 = referencedAssemblies.Length;
                        AssemblyCache.WriteValidatableInt(binaryWriter, referencedAssemblies.Length);
                        for (int k = 0; k < num2; k++)
                        {
                            binaryWriter.Write(referencedAssemblies[k]);
                        }
                    }
                    AssemblyCache.WriteValidatableInt(binaryWriter, this.knownAssembliesMap.Count);
                    mD.TransformBlock(BitConverter.GetBytes(this.knownAssembliesMap.Count), 0, 4, outputBuffer, 0);
                    foreach (KeyValuePair<string, short> item in this.knownAssembliesMap)
                    {
                        byte[] bytes3 = Encoding.UTF8.GetBytes(item.Key);
                        AssemblyCache.WriteValidatableInt(binaryWriter, bytes3.Length);
                        binaryWriter.Write(bytes3);
                        mD.TransformBlock(BitConverter.GetBytes(bytes3.Length), 0, 4, outputBuffer, 0);
                        mD.TransformBlock(bytes3, 0, bytes3.Length, bytes3, 0);
                        binaryWriter.Write(item.Value);
                        mD.TransformBlock(BitConverter.GetBytes(item.Value), 0, 2, outputBuffer, 0);
                    }
                    mD.TransformFinalBlock(new byte[0], 0, 0);
                    binaryWriter.Write(mD.Hash);
                    mD.Clear();
                }
            }
        }
    }

    public static AssemblyCache LoadFromFile(string basePath)
    {
        if (string.IsNullOrWhiteSpace(basePath))
        {
            throw new ArgumentNullException("basePath");
        }
        string path = Path.Combine(basePath, "AssemblyCache.dat");
        byte[] outputBuffer = new byte[4];
        using (FileStream input = File.OpenRead(path))
        {
            using (BinaryReader binaryReader = new BinaryReader(input))
            {
                using (MD5 mD = MD5.Create())
                {
                    int num = binaryReader.ReadInt32();
                    if (num != 23)
                    {
                        throw new FormatException();
                    }
                    int num2 = binaryReader.ReadInt32();
                    if (num2 != mD.HashSize)
                    {
                        throw new FormatException();
                    }
                    int num3 = AssemblyCache.ReadValidatableInt(binaryReader);
                    string[] array = new string[num3];
                    mD.TransformBlock(BitConverter.GetBytes(num3), 0, 4, outputBuffer, 0);
                    for (int i = 0; i < num3; i++)
                    {
                        int num4 = AssemblyCache.ReadValidatableInt(binaryReader);
                        byte[] array2 = new byte[num4];
                        binaryReader.Read(array2, 0, num4);
                        string text = array[i] = string.Intern(Encoding.UTF8.GetString(array2));
                        mD.TransformBlock(BitConverter.GetBytes(num4), 0, 4, outputBuffer, 0);
                        mD.TransformBlock(array2, 0, num4, array2, 0);
                    }
                    int num5 = AssemblyCache.ReadValidatableInt(binaryReader);
                    AssemblyInfo[] array3 = new AssemblyInfo[num5];
                    mD.TransformBlock(BitConverter.GetBytes(num5), 0, 4, outputBuffer, 0);
                    for (int j = 0; j < num5; j++)
                    {
                        AssemblyInfo assemblyInfo = new AssemblyInfo();
                        int num6 = AssemblyCache.ReadValidatableInt(binaryReader);
                        byte[] array4 = new byte[num6];
                        binaryReader.Read(array4, 0, num6);
                        assemblyInfo.AssemblyFileNameWithoutExtension = string.Intern(Encoding.UTF8.GetString(array4));
                        mD.TransformBlock(BitConverter.GetBytes(num6), 0, 4, outputBuffer, 0);
                        mD.TransformBlock(array4, 0, array4.Length, array4, 0);
                        assemblyInfo.IsExe = binaryReader.ReadBoolean();
                        assemblyInfo.AssemblyRelativeDirectoryIndex = binaryReader.ReadInt16();
                        mD.TransformBlock(BitConverter.GetBytes(assemblyInfo.AssemblyRelativeDirectoryIndex), 0, 2, outputBuffer, 0);
                        int num7 = AssemblyCache.ReadValidatableInt(binaryReader);
                        if (num7 < 1)
                        {
                            assemblyInfo.ReferencedAssemblies = AssemblyCache.EmptyShortArray;
                        }
                        else
                        {
                            short[] array5 = new short[num7];
                            for (int k = 0; k < num7; k++)
                            {
                                array5[k] = binaryReader.ReadInt16();
                            }
                            assemblyInfo.ReferencedAssemblies = array5;
                        }
                        array3[j] = assemblyInfo;
                    }
                    int num8 = AssemblyCache.ReadValidatableInt(binaryReader);
                    SortedList<string, short> sortedList = new SortedList<string, short>(num8, StringComparer.OrdinalIgnoreCase);
                    mD.TransformBlock(BitConverter.GetBytes(num8), 0, 4, outputBuffer, 0);
                    for (int l = 0; l < num8; l++)
                    {
                        int num9 = AssemblyCache.ReadValidatableInt(binaryReader);
                        byte[] array6 = new byte[num9];
                        binaryReader.Read(array6, 0, num9);
                        string key = string.Intern(Encoding.UTF8.GetString(array6));
                        mD.TransformBlock(BitConverter.GetBytes(num9), 0, 4, outputBuffer, 0);
                        mD.TransformBlock(array6, 0, num9, array6, 0);
                        short value = binaryReader.ReadInt16();
                        mD.TransformBlock(BitConverter.GetBytes(value), 0, 2, outputBuffer, 0);
                        sortedList.Add(key, value);
                    }
                    mD.TransformFinalBlock(new byte[0], 0, 0);
                    byte[] hash = mD.Hash;
                    byte[] array7 = new byte[hash.Length];
                    binaryReader.Read(array7, 0, array7.Length);
                    IStructuralEquatable structuralEquatable = array7;
                    if (!structuralEquatable.Equals(hash, StructuralComparisons.StructuralEqualityComparer))
                    {
                        throw new BadImageFormatException();
                    }
                    return new AssemblyCache(basePath, array, array3, sortedList);
                }
            }
        }
    }

    private static void CheckAssemblyAndThrow(AssemblyName requiredAssemblyName, AssemblyName knownAssemblyName)
    {
        if (!AssemblyCache.CheckPublicKeyToken(requiredAssemblyName.GetPublicKeyToken(), knownAssemblyName.GetPublicKeyToken()))
        {
            throw new FileLoadException(string.Format("Обнаружено несоответствие ключей сборок [{0}] и [{1}].", requiredAssemblyName.FullName, knownAssemblyName.FullName));
        }
        if (!AssemblyCache.CheckVersion(requiredAssemblyName.Version, knownAssemblyName.Version))
        {
            throw new FileLoadException(string.Format("Обнаружено несоответствие версий сборок [{0}] и [{1}].", requiredAssemblyName.FullName, knownAssemblyName.FullName));
        }
        if (AssemblyCache.CheckCultureInfo(requiredAssemblyName.CultureInfo, knownAssemblyName.CultureInfo))
        {
            return;
        }
        throw new FileLoadException(string.Format("Обнаружено несоответствие культур сборок [{0}] и [{1}].", requiredAssemblyName.FullName, knownAssemblyName.FullName));
    }

    private static bool CheckAssembly(AssemblyName requiredAssemblyName, AssemblyName knownAssemblyName, out Exception exception)
    {
        exception = null;
        if (!AssemblyCache.CheckPublicKeyToken(requiredAssemblyName.GetPublicKeyToken(), knownAssemblyName.GetPublicKeyToken()))
        {
            exception = new FileLoadException(string.Format("Обнаружено несоответствие ключей сборок [{0}] и [{1}].", requiredAssemblyName.FullName, knownAssemblyName.FullName));
            return false;
        }
        if (!AssemblyCache.CheckVersion(requiredAssemblyName.Version, knownAssemblyName.Version))
        {
            exception = new FileLoadException(string.Format("Обнаружено несоответствие версий сборок [{0}] и [{1}].", requiredAssemblyName.FullName, knownAssemblyName.FullName));
            return false;
        }
        if (!AssemblyCache.CheckCultureInfo(requiredAssemblyName.CultureInfo, knownAssemblyName.CultureInfo))
        {
            exception = new FileLoadException(string.Format("Обнаружено несоответствие культур сборок [{0}] и [{1}].", requiredAssemblyName.FullName, knownAssemblyName.FullName));
            return false;
        }
        return true;
    }

    private static bool CheckPublicKeyToken(byte[] required, byte[] known)
    {
        bool flag = object.ReferenceEquals(required, null);
        bool flag2 = object.ReferenceEquals(known, null);
        if (flag)
        {
            return true;
        }
        if (flag2)
        {
            return false;
        }
        return ((IStructuralEquatable)required).Equals((object)known, StructuralComparisons.StructuralEqualityComparer);
    }

    private static bool CheckCultureInfo(CultureInfo required, CultureInfo known)
    {
        bool flag = object.ReferenceEquals(required, null);
        bool flag2 = object.ReferenceEquals(known, null);
        if (flag)
        {
            return true;
        }
        if (flag2)
        {
            return false;
        }
        return string.Equals(required.Name, known.Name, StringComparison.OrdinalIgnoreCase);
    }

    private static bool CheckVersion(Version required, Version known)
    {
        bool flag = object.ReferenceEquals(required, null);
        bool flag2 = object.ReferenceEquals(known, null);
        if (flag)
        {
            return true;
        }
        if (flag2)
        {
            return false;
        }
        return required.Equals(known);
    }

    [SecuritySafeCritical]
    public static AssemblyCache GetCurrentCache()
    {
        return (AssemblyCache)AppDomain.CurrentDomain.GetData("AssemblyCache");
    }

    [DebuggerNonUserCode]
    private Assembly GetOrLoadAssemblyAndItsReferences(AssemblyInfo assemblyInfo)
    {
        Assembly assembly = assemblyInfo.Assembly;
        if (!object.ReferenceEquals(assembly, null))
        {
            return assembly;
        }
        lock (assemblyInfo)
        {
            assembly = assemblyInfo.Assembly;
            if (!object.ReferenceEquals(assembly, null))
            {
                return assembly;
            }
            short[] referencedAssemblies = assemblyInfo.ReferencedAssemblies;
            int num = referencedAssemblies.Length;
            for (int i = 0; i < num; i++)
            {
                this.LoadAssemblyIfNotLocked(referencedAssemblies[i]);
            }
            string text = Path.Combine(this.basePath, this.knownPaths[assemblyInfo.AssemblyRelativeDirectoryIndex], assemblyInfo.AssemblyFileNameWithoutExtension + (assemblyInfo.IsExe ? ".exe" : ".dll"));
            assembly = (assemblyInfo.Assembly = Assembly.LoadFrom(text));
            if (assembly.GlobalAssemblyCache)
            {
                AssemblyDuplicateInfo.AssemblyLoadFromGacEventArgs.AssemblyLoadFromGacEventHandler assemblyLoadFromGac = this.AssemblyLoadFromGac;
                if (!object.ReferenceEquals(assemblyLoadFromGac, null))
                {
                    try
                    {
                        assemblyLoadFromGac(this, new AssemblyDuplicateInfo.AssemblyLoadFromGacEventArgs(text, assembly, false));
                        return assembly;
                    }
                    catch
                    {
                        return assembly;
                    }
                }
                return assembly;
            }
            return assembly;
        }
    }

    [DebuggerNonUserCode]
    private Assembly GetOrLoadAssembly(AssemblyInfo assemblyInfo)
    {
        Assembly assembly = assemblyInfo.Assembly;
        if (!object.ReferenceEquals(assembly, null))
        {
            return assembly;
        }
        lock (assemblyInfo)
        {
            assembly = assemblyInfo.Assembly;
            if (!object.ReferenceEquals(assembly, null))
            {
                return assembly;
            }
            string text = Path.Combine(this.basePath, this.knownPaths[assemblyInfo.AssemblyRelativeDirectoryIndex], assemblyInfo.AssemblyFileNameWithoutExtension + (assemblyInfo.IsExe ? ".exe" : ".dll"));
            assembly = (assemblyInfo.Assembly = Assembly.LoadFrom(text));
            if (assembly.GlobalAssemblyCache)
            {
                AssemblyDuplicateInfo.AssemblyLoadFromGacEventArgs.AssemblyLoadFromGacEventHandler assemblyLoadFromGac = this.AssemblyLoadFromGac;
                if (!object.ReferenceEquals(assemblyLoadFromGac, null))
                {
                    try
                    {
                        assemblyLoadFromGac(this, new AssemblyDuplicateInfo.AssemblyLoadFromGacEventArgs(text, assembly, false));
                        return assembly;
                    }
                    catch
                    {
                        return assembly;
                    }
                }
                return assembly;
            }
            return assembly;
        }
    }

    [DebuggerNonUserCode]
    private void LoadAssemblyIfNotLocked(int assemblyIndex)
    {
        AssemblyInfo assemblyInfo = this.knownAssemblies[assemblyIndex];
        Assembly assembly = assemblyInfo.Assembly;
        if (object.ReferenceEquals(assembly, null))
        {
            bool flag = false;
            try
            {
                Monitor.TryEnter(assemblyInfo, ref flag);
                if (flag)
                {
                    assembly = assemblyInfo.Assembly;
                    if (object.ReferenceEquals(assembly, null))
                    {
                        string text = Path.Combine(this.basePath, this.knownPaths[assemblyInfo.AssemblyRelativeDirectoryIndex], assemblyInfo.AssemblyFileNameWithoutExtension + (assemblyInfo.IsExe ? ".exe" : ".dll"));
                        try
                        {
                            assembly = (assemblyInfo.Assembly = Assembly.LoadFrom(text));
                        }
                        catch (BadImageFormatException)
                        {
                            assemblyInfo.Assembly = null;
                            return;
                        }
                        if (assembly.GlobalAssemblyCache)
                        {
                            AssemblyDuplicateInfo.AssemblyLoadFromGacEventArgs.AssemblyLoadFromGacEventHandler assemblyLoadFromGac = this.AssemblyLoadFromGac;
                            if (!object.ReferenceEquals(assemblyLoadFromGac, null))
                            {
                                try
                                {
                                    assemblyLoadFromGac(this, new AssemblyDuplicateInfo.AssemblyLoadFromGacEventArgs(text, assembly, false));
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                if (flag)
                {
                    Monitor.Exit(assemblyInfo);
                }
            }
        }
    }

    [DebuggerNonUserCode]
    private void LoadAssemblyReflectionOnly(int assemblyIndex)
    {
        AssemblyInfo assemblyInfo = this.knownAssemblies[assemblyIndex];
        Assembly assembly = assemblyInfo.Assembly;
        if (object.ReferenceEquals(assembly, null))
        {
            assembly = assemblyInfo.Assembly;
            if (object.ReferenceEquals(assembly, null))
            {
                string text = Path.Combine(this.basePath, this.knownPaths[assemblyInfo.AssemblyRelativeDirectoryIndex], assemblyInfo.AssemblyFileNameWithoutExtension + (assemblyInfo.IsExe ? ".exe" : ".dll"));
                assembly = (assemblyInfo.Assembly = Assembly.ReflectionOnlyLoadFrom(text));
                if (assembly.GlobalAssemblyCache)
                {
                    AssemblyDuplicateInfo.AssemblyLoadFromGacEventArgs.AssemblyLoadFromGacEventHandler reflectionOnlyAssemblyLoadFromGac = this.ReflectionOnlyAssemblyLoadFromGac;
                    if (!object.ReferenceEquals(reflectionOnlyAssemblyLoadFromGac, null))
                    {
                        try
                        {
                            reflectionOnlyAssemblyLoadFromGac(this, new AssemblyDuplicateInfo.AssemblyLoadFromGacEventArgs(text, assembly, false));
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }
    }

    [DebuggerNonUserCode]
    private Assembly GetOrLoadAssemblyAndItsReferencesReflectionOnly(AssemblyInfo assemblyInfo)
    {
        Assembly assembly = assemblyInfo.ReflectionOnlyAssembly;
        if (object.ReferenceEquals(assembly, null))
        {
            short[] referencedAssemblies = assemblyInfo.ReferencedAssemblies;
            int num = referencedAssemblies.Length;
            for (int i = 0; i < num; i++)
            {
                this.LoadAssemblyReflectionOnly(referencedAssemblies[i]);
            }
            string text = Path.Combine(this.basePath, this.knownPaths[assemblyInfo.AssemblyRelativeDirectoryIndex], assemblyInfo.AssemblyFileNameWithoutExtension + (assemblyInfo.IsExe ? ".exe" : ".dll"));
            assembly = (assemblyInfo.ReflectionOnlyAssembly = Assembly.ReflectionOnlyLoadFrom(text));
            if (assembly.GlobalAssemblyCache)
            {
                AssemblyDuplicateInfo.AssemblyLoadFromGacEventArgs.AssemblyLoadFromGacEventHandler reflectionOnlyAssemblyLoadFromGac = this.ReflectionOnlyAssemblyLoadFromGac;
                if (!object.ReferenceEquals(reflectionOnlyAssemblyLoadFromGac, null))
                {
                    try
                    {
                        reflectionOnlyAssemblyLoadFromGac(this, new AssemblyDuplicateInfo.AssemblyLoadFromGacEventArgs(text, assembly, false));
                        return assembly;
                    }
                    catch
                    {
                        return assembly;
                    }
                }
            }
        }
        return assembly;
    }

    [SecuritySafeCritical]
    public void AttachToDomain(AppDomain domain)
    {
        if (object.ReferenceEquals(domain, null))
        {
            throw new ArgumentNullException("domain");
        }
        if (object.ReferenceEquals(domain, AppDomain.CurrentDomain))
        {
            domain.SetData(typeof(AssemblyCache).Name, this);
            AssemblyCache.AttachToCurrentDomain();
        }
        else
        {
            domain.Load(Assembly.GetAssembly(base.GetType()).GetName());
            domain.SetData(typeof(AssemblyCache).Name, this);
            domain.DoCallBack(AssemblyCache.AttachToCurrentDomain);
        }
    }

    [SecuritySafeCritical]
    public void DetachFromDomain(AppDomain domain)
    {
        if (object.ReferenceEquals(domain, null))
        {
            throw new ArgumentNullException("domain");
        }
        if (object.ReferenceEquals(domain, AppDomain.CurrentDomain))
        {
            AssemblyCache.DetachFromCurrentDomain();
            domain.SetData(typeof(AssemblyCache).Name, null);
        }
        else
        {
            domain.DoCallBack(AssemblyCache.DetachFromCurrentDomain);
            domain.SetData(typeof(AssemblyCache).Name, null);
        }
    }

    private static ushort ReverseBytes(ushort value)
    {
        return (ushort)((value & 0xFF) << 8 | (int)((uint)(value & 0xFF00) >> 8));
    }

    private static void WriteValidatableInt(BinaryWriter writer, int value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Значение целого числа для записи должно быть больше или равно нулю.", "value");
        }
        ushort value2 = (ushort)value;
        ushort value3 = AssemblyCache.ReverseBytes(value2);
        writer.Write(value2);
        writer.Write(value3);
    }

    private static int ReadValidatableInt(BinaryReader reader)
    {
        ushort num = reader.ReadUInt16();
        ushort value = reader.ReadUInt16();
        ushort num2 = AssemblyCache.ReverseBytes(value);
        if (num2 != num)
        {
            throw new FormatException("Ошибка при валидации целочисленного значения. Файл поврежден.");
        }
        if (num > 2147483647)
        {
            throw new FormatException("Ошибка при валидации целочисленного значения. Значение слишком велико.");
        }
        return num;
    }

    public static bool TryGetTargetFrameworkVersion(Assembly assembly, out Version frameworkVersion)
    {
        try
        {
            object[] customAttributes = assembly.GetCustomAttributes(AssemblyCache.TypeOfTargetFrameworkAttribute, false);
            if (customAttributes.Length < 1)
            {
                frameworkVersion = null;
                return false;
            }
            TargetFrameworkAttribute targetFrameworkAttribute = (TargetFrameworkAttribute)customAttributes[0];
            FrameworkName frameworkName = new FrameworkName(targetFrameworkAttribute.FrameworkName);
            frameworkVersion = frameworkName.Version;
            return true;
        }
        catch (Exception inner)
        {
            throw new InvalidProgramException("Ошибка определения целевой версии .NET Framework для сборки [" + assembly.FullName + "], расположенной по пути [" + assembly.Location + "]. Вероятные причины: метаданные сборки некорректны или не удалось разрешить зависимости сборки.", inner);
        }
    }

    private Assembly CurrentDomainOnAssemblyResolve(object sender, ResolveEventArgs args)
    {
        return this.ResolveAssembly(args);
    }

    private Assembly CurrentDomainOnReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
    {
        return this.ResolveAssemblyReflectionOnly(args);
    }

    [SecuritySafeCritical]
    private static void AttachToCurrentDomain()
    {
        AssemblyCache assemblyCache = AppDomain.CurrentDomain.GetData(typeof(AssemblyCache).Name) as AssemblyCache;
        if (object.ReferenceEquals(assemblyCache, null))
        {
            throw new InvalidOperationException();
        }
        AppDomain.CurrentDomain.AssemblyResolve += assemblyCache.CurrentDomainOnAssemblyResolve;
        AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += assemblyCache.CurrentDomainOnReflectionOnlyAssemblyResolve;
    }

    [SecuritySafeCritical]
    private static void DetachFromCurrentDomain()
    {
        AssemblyCache assemblyCache = AppDomain.CurrentDomain.GetData(typeof(AssemblyCache).Name) as AssemblyCache;
        if (object.ReferenceEquals(assemblyCache, null))
        {
            throw new InvalidOperationException();
        }
        AppDomain.CurrentDomain.AssemblyResolve -= assemblyCache.CurrentDomainOnAssemblyResolve;
        AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= assemblyCache.CurrentDomainOnReflectionOnlyAssemblyResolve;
    }
}