﻿using UnityEngine;
using UnityEditor;

namespace AC
{

	[CustomEditor (typeof (RememberCollider), true)]
	public class RememberColliderEditor : ConstantIDEditor
	{
		
		public override void OnInspectorGUI()
		{
			RememberCollider _target = (RememberCollider) target;

			EditorGUILayout.BeginVertical ("Button");
			EditorGUILayout.LabelField ("Hotspot", EditorStyles.boldLabel);
			_target.startState = (AC_OnOff) CustomGUILayout.EnumPopup ("Collider state on start:", _target.startState, "", "The enabled state of the Collider when the game begins");
			EditorGUILayout.EndVertical ();

			if (_target.GetComponent <Collider>() == null)
			{
				EditorGUILayout.HelpBox ("This script expects a Collider component!", MessageType.Warning);
			}
			
			SharedGUI ();
		}
		
	}

}