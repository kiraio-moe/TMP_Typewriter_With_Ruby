#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

[InitializeOnLoad]
public class PluginsImporter : UnityEditor.AssetModificationProcessor
{
    static AddRequest addRequest;

    static PluginsImporter()
    {
        EditorApplication.update += OnEditorUpdate;
    }

    private static void OnEditorUpdate()
    {
        if (addRequest != null && addRequest.IsCompleted)
        {
            if (addRequest.Status == StatusCode.Success)
            {
                foreach (var path in addRequest.Result.resolvedPath.Split(';'))
                {
                    if (path.EndsWith("Plugins"))
                    {
                        AssetDatabase.ImportPackage(path, true);
                        break;
                    }
                }
            }

            addRequest = null;
        }
    }
}
#endif
