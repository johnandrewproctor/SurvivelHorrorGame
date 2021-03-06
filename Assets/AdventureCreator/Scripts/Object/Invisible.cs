/*
 *
 *	Adventure Creator
 *	by Chris Burton, 2013-2019
 *	
 *	"Invisible.cs"
 * 
 *	This script makes any gameObject it is attached to invisible.
 * 
 */

using UnityEngine;

namespace AC
{

	/**
	 * This script disables the Renderer component of any GameObject it is attached to, making it invisible.
	 */
	#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0)
	[HelpURL("https://www.adventurecreator.org/scripting-guide/class_a_c_1_1_invisible.html")]
	#endif
	public class Invisible : MonoBehaviour
	{

		#region UnityStandards
		
		protected void Awake ()
		{
			this.GetComponent <Renderer>().enabled = false;
		}

		#endregion

	}

}