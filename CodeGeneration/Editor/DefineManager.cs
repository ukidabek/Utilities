using UnityEditor;

namespace Utilities.Editor.CodeGeneration
{

	[InitializeOnLoad]
	public static class DefineManager
	{
		static DefineManager()
		{
			var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
			var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
			if (!defines.Contains("CODE_GENERATOR_PRESENT"))
			{
				defines = string.Format("{0};CODE_GENERATOR_PRESENT", defines);
				PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, defines);
			}
		}
	}
}