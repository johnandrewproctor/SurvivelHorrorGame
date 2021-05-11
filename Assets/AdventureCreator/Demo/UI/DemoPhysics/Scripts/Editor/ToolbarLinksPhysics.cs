using UnityEngine;
using UnityEditor;
using System.Collections;
using AC;

public class ToolbarLinksPhysics : EditorWindow
{

	[MenuItem ("Adventure Creator/Getting started/Load Physics Demo", false, 7)]
	static void DemoPhysics ()
	{
		ManagerPackage package = AssetDatabase.LoadAssetAtPath (Resource.MainFolderPath + "/DemoPhysics/PhysicsDemo_ManagerPackage.asset", typeof (ManagerPackage)) as ManagerPackage;
		if (package != null)
		{
			if (!ACInstaller.IsInstalled ())
			{
				ACInstaller.DoInstall ();
			}

			package.AssignManagers ();
			AdventureCreator.RefreshActions ();

			if (UnityVersionHandler.GetCurrentSceneName () != "Office")
			{
				#if UNITY_5_3 || UNITY_5_4 || UNITY_5_3_OR_NEWER
				bool canProceed = EditorUtility.DisplayDialog ("Open demo scene", "Would you like to open the Physics Demo scene, Office, now?", "Yes", "No");
				if (canProceed)
				{
					if (UnityVersionHandler.SaveSceneIfUserWants ())
					{
						UnityEditor.SceneManagement.EditorSceneManager.OpenScene (Resource.MainFolderPath + "/DemoPhysics/Scenes/Office.unity");
					}
				}
				#else
				ACDebug.Log ("Physics Demo managers loaded - you can now run the Physics Demo scene in '" + Resource.mainFolderPath + "/DemoPhysics/Scenes/Office.unity'");
				#endif
			}

			AdventureCreator.Init ();
		}
	}

}