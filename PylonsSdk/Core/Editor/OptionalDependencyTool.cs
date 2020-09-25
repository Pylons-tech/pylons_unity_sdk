using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace PylonsSdk.Editor
{
    /// <summary>
    /// Adds/removes references to optional dependencies and defines to enable conditional compilation for them.
    /// </summary>
    [InitializeOnLoad]
    class OptionalDependencyTool : ScriptableObject
    {
        private static OptionalDependencyTool instance;

        static OptionalDependencyTool() => EditorApplication.update += OnInit;

        static void OnInit()
        {
            EditorApplication.update -= OnInit;
            instance = CreateInstance<OptionalDependencyTool>();
            instance.hideFlags = HideFlags.HideAndDontSave;
        }

        void OnEnable() => FixOptionalDependencies();
        void OnDisable() => FixOptionalDependencies();

        /// <summary>
        /// Struct used by FixOptionalDependencies for tracking the data it needs to know in
        /// order to manage optional references.
        /// </summary>
        private struct OptionalRefMetadata
        {
            public readonly string AsmRefPath;
            public readonly string ReferencedBy;
            public readonly string ReferenceTo;

            public OptionalRefMetadata(string asmRefPath, string referencedBy, string referenceTo)
            {
                AsmRefPath = asmRefPath;
                ReferencedBy = referencedBy;
                ReferenceTo = referenceTo;
            }
        }

        // This shouldn't be exposed normally, but if AsmRefPreprocessor ever breaks this is useful for debugging
        // [MenuItem("Debug/Fix optional dependencies")]
        private static void FixOptionalDependencies()
        {
            const string MAGIC = "OPT_REF_";
            var assemblies = CompilationPipeline.GetAssemblies();
            var optionalRefs = new List<OptionalRefMetadata>();
            var foundAssemblies = new List<string>();

            // go over all assemblies, scan for optional references
            foreach (var assembly in assemblies)
            {
                var asmRef = CompilationPipeline.GetAssemblyDefinitionFilePathFromAssemblyName(assembly.name);

                if (asmRef != null)
                {
                    var path = CompilationPipeline.GetAssemblyDefinitionFilePathFromAssemblyName(assembly.name);
                    var obj = JsonConvert.DeserializeObject(File.ReadAllText(path)) as dynamic;
                    foundAssemblies.Add((obj.name as JValue).Value<string>());
                    if (obj.versionDefines == null) continue;
                    foreach (var define in obj.versionDefines)
                    {
                        var d = (define.define as JValue).Value<string>();
                        if (d.StartsWith(MAGIC))
                        {
                            // found one to resolve!
                            optionalRefs.Add(new OptionalRefMetadata(path, assembly.name, d.Substring(MAGIC.Length, d.Length - MAGIC.Length).Replace("_", ".")));                         
                        }
                    }
                }
            }

            // go over the found optional references and fix asmrefs
            foreach (var optionalRef in optionalRefs)
            {
                var defineString = optionalRef.ReferenceTo.Replace(".", "_").ToUpper();
                var obj = JsonConvert.DeserializeObject(File.ReadAllText(optionalRef.AsmRefPath)) as dynamic;

                // does a reference exist?
                var foundRefIndex = -1;
                for (int i = 0; i < obj.references.Count; i++) if (obj.references[i] == optionalRef.ReferenceTo)
                    {
                        foundRefIndex = i;
                        break;
                    }

                // does the conditional-compilation define exist?
                var foundDefineIndex = -1;
                for (int i = 0; i < obj.versionDefines.Count; i++) if (obj.versionDefines[i].name == optionalRef.ReferencedBy && obj.versionDefines[i].define == defineString)
                    {
                        foundDefineIndex = i;
                        break;
                    }

                // if the assembly is loaded, make sure we have the reference and the define, add them if not
                if (foundAssemblies.Contains(optionalRef.ReferenceTo))
                {
                    if (foundRefIndex == -1) obj.references.Add(optionalRef.ReferenceTo);
                    if (foundDefineIndex == -1)
                    {
                        // this is ugly but using an anonymous type here breaks jsonconvert, so we do it directly instead
                        obj.versionDefines.Add(new JObject
                        {
                            { "name", optionalRef.ReferencedBy },
                            { "expression", "" },
                            { "define", defineString }
                        });
                    }
                }
                else
                {
                    // if it's not loaded, remove any reference that exists
                    if (foundRefIndex != -1) obj.references.RemoveAt(foundRefIndex);
                    if (foundDefineIndex != -1) obj.versionDefines.RemoveAt(foundDefineIndex);
                }
                // done
                File.WriteAllText(optionalRef.AsmRefPath, JsonConvert.SerializeObject(obj));
            }
        }
    }
}
