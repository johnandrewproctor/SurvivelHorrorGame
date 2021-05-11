﻿/*
 *
 *	Adventure Creator
 *	by Chris Burton, 2013-2019
 *	
 *	"DragTrack_Hinge.cs"
 * 
 *	This track fixes a Moveable_Drag's position, so it can only be rotated
 *	in a circle.
 * 
 */

using UnityEngine;

namespace AC
{

	/**
	 * A track that constrains a Moveable_Drag's position, so that it can only be rotated.
	 * This makes it suitable for objects that pivot, such as levers, doors, etc.
	 */
	#if !(UNITY_4_6 || UNITY_4_7 || UNITY_5_0)
	[HelpURL("https://www.adventurecreator.org/scripting-guide/class_a_c_1_1_drag_track___hinge.html")]
	#endif
	public class DragTrack_Hinge : DragTrack
	{

		#region Variables
	
		/** How much an object can be rotated by */
		public float maxAngle = 60f;
		/** The track's radius (for visualising in the Scene window) */
		public float radius = 2f;
		/** If True, then objects can be rotated a full revolution */
		public bool doLoop = false;
		/** If True, and doLoop = True, then the number of revolutions an object can rotate is limited */
		public bool limitRevolutions = false;
		/** If limitRevolutions = True, the maximum number of revolutions an object can be rotated by */
		public int maxRevolutions = 0;
		/** If True, then the calculated drag vector will be based on the track's orientation, rather than the object being rotated, so that the input drag vector will always need to be the same direction */
		public bool alignDragToFront = false;

		#endregion


		#region PublicFunctions

		public override void Connect (Moveable_Drag draggable)
		{
			if (maxRevolutions < 1) maxRevolutions = 1;

			LimitCollisions (draggable);
		}


		public override void ApplyAutoForce (float _position, float _speed, Moveable_Drag draggable)
		{
			if (Time.time <= 0.2f) return;

			Vector3 deltaForce = GetForceToPosition (draggable, _position);
			deltaForce *= _speed / draggable._rigidbody.mass;
			
			// Limit magnitude
			if (deltaForce.magnitude > draggable.maxSpeed)
			{
				deltaForce *= draggable.maxSpeed / deltaForce.magnitude;
			}
			
			deltaForce -= draggable._rigidbody.angularVelocity;
			draggable._rigidbody.AddTorque (deltaForce, ForceMode.VelocityChange);
		}


		public override void ApplyDragForce (Vector3 force, Moveable_Drag draggable)
		{
			float dotProduct = 0f;
			Vector3 axisOffset = Vector2.zero;

			if (alignDragToFront)
			{
				// Use the Hinge's transform, not the Draggable's
				dotProduct = Vector3.Dot (force, transform.up);
				
				// Invert force if on the "back" side
				axisOffset = GetAxisOffset (draggable._dragVector);
				if (Vector3.Dot (transform.right, axisOffset) < 0f)
				{
					dotProduct *= -1f;
				}
			}
			else
			{
				dotProduct = Vector3.Dot (force, draggable.transform.up);
				axisOffset = GetAxisOffset (draggable.GetGrabPosition ());
				
				// Invert force if on the "back" side
				if (Vector3.Dot (draggable.transform.right, axisOffset) < 0f)
				{
					dotProduct *= -1f;
				}
			}

			// Calculate the amount of force along the tangent
			Vector3 tangentForce = (draggable.transform.forward * dotProduct).normalized;
			tangentForce *= force.magnitude;
			
			// Take radius into account
			tangentForce /= axisOffset.magnitude / 0.43f;

			draggable._rigidbody.AddTorque (tangentForce);
		}


		public override float GetScreenPointProportionAlong (Vector2 point)
		{
			Vector2 screen_gizmoStartPosition = KickStarter.CameraMain.WorldToScreenPoint (GetGizmoPosition (0f));
			Vector2 screen_gizmoEndPosition = KickStarter.CameraMain.WorldToScreenPoint (GetGizmoPosition (1f));
			Vector2 screen_origin = KickStarter.CameraMain.WorldToScreenPoint (transform.position);

			Vector2 startToOrigin = screen_gizmoStartPosition - screen_origin;
			Vector2 endToOrigin = screen_gizmoEndPosition - screen_origin;

			Vector2 pointToOrigin = point - screen_origin;

			float startToPointAngle = SignedAngle (startToOrigin, pointToOrigin);
			float startToEndAngle = SignedAngle (startToOrigin, endToOrigin);

			bool isFlipped = (Vector3.Dot (transform.forward, KickStarter.CameraMain.transform.forward) < 0f);
			if (isFlipped)
			{
				startToEndAngle *= -1f;
				startToPointAngle *= -1f;
			}

			if (startToEndAngle < 0f) startToEndAngle += 360f;
			if (startToPointAngle < 0f) startToPointAngle += 360f;

			if (Loops)
			{
				startToEndAngle = 360f;
			}

			float reversedMidAngle = 180f + (startToEndAngle / 2f);
			if (startToPointAngle > reversedMidAngle) startToPointAngle -= 360f;

			return startToPointAngle / startToEndAngle;
		}


		public override void SetPositionAlong (float proportionAlong, Moveable_Drag draggable)
		{
			draggable.transform.position = transform.position;
			draggable.transform.rotation = Quaternion.AngleAxis (proportionAlong * MaxAngle, transform.forward) * transform.rotation;

			base.SetPositionAlong (proportionAlong, draggable);
		}
		

		public override float GetDecimalAlong (Moveable_Drag draggable)
		{
			float angle = Vector3.Angle (transform.up, draggable.transform.up);

			if (Vector3.Dot (-transform.right, draggable.transform.up) < 0f)
			{
				angle = 360f - angle;
			}
			if (angle > 180f + MaxAngle / 2f)
			{
				angle = 0f;
			}

			return (angle / MaxAngle);
		}
		

		public override void SnapToTrack (Moveable_Drag draggable, bool onStart)
		{
			draggable.transform.position = transform.position;
			
			if (onStart)
			{
				draggable.transform.rotation = transform.rotation;
				draggable.trackValue = 0f;
			}
		}
		

		public override void UpdateDraggable (Moveable_Drag draggable)
		{
			float oldValue = draggable.trackValue;

			draggable.transform.position = transform.position;
			draggable.trackValue = GetDecimalAlong (draggable);
			
			if (draggable.trackValue <= 0f || draggable.trackValue > 1f)
			{
				if (draggable.trackValue < 0f)
				{
					draggable.trackValue = 0f;
				}
				else if (draggable.trackValue > 1f)
				{
					draggable.trackValue = 1f;
				}

				draggable._rigidbody.angularVelocity = Vector3.zero;
			}
			SetPositionAlong (draggable.trackValue, draggable);

			if (Loops && limitRevolutions)
			{
				if (oldValue < 0.1f && draggable.trackValue > 0.9f)
				{
					draggable.revolutions --;
				}
				else if (oldValue > 0.9f && draggable.trackValue < 0.1f)
				{
					draggable.revolutions ++;
				}

				if (draggable.revolutions < 0)
				{
					draggable.revolutions = 0;
					draggable.trackValue = 0f;
					SetPositionAlong (draggable.trackValue, draggable);
					draggable._rigidbody.angularVelocity = Vector3.zero;
				}
				else if (draggable.revolutions > maxRevolutions - 1)
				{
					draggable.revolutions = maxRevolutions - 1;
					draggable.trackValue = 1f;
					SetPositionAlong (draggable.trackValue, draggable);
					draggable._rigidbody.angularVelocity = Vector3.zero;
				}
			}

			if (!onlySnapOnPlayerRelease)
			{
				DoSnapCheck (draggable);
			}
		}


		public override Vector3 GetGizmoPosition (float proportionAlong)
		{
			Quaternion rot = Quaternion.AngleAxis (proportionAlong * MaxAngle, transform.forward);
			Vector3 startPosition = transform.position + (radius * transform.right);
			return RotatePointAroundPivot (startPosition, transform.position, rot);
		}


		public override Vector3 GetForceToPosition (Moveable_Drag draggable, float targetProportionAlong)
		{
			float proportionalDifference = Mathf.Clamp01 (targetProportionAlong) - draggable.trackValue;

			if (Loops)
			{
				if (proportionalDifference > 0.5f)
				{
					proportionalDifference -= 1f;
				}
				else if (proportionalDifference < -0.5f)
				{
					proportionalDifference += 1f;
				}
			}

			return draggable.transform.forward * proportionalDifference * 1000f;
		}


		public override float GetMoveSoundIntensity (Moveable_Drag draggable)
		{
			return draggable._rigidbody.angularVelocity.magnitude;
		}

		#endregion


		#region ProtectedFunctions

		protected Vector3 GetAxisOffset (Vector3 grabPosition)
		{
			float dist = Vector3.Dot (grabPosition, transform.forward);
			Vector3 axisPoint = transform.position + (transform.forward * dist);
			return (grabPosition - axisPoint);
		}

		#endregion


		#region GetSet

		public override bool Loops
		{
			get
			{
				return doLoop || (maxAngle >= 360f);
			}
		}


		public float MaxAngle
		{
			get
			{
				return (Loops) ? 360f : maxAngle;
			}
		}

		#endregion

	}

}