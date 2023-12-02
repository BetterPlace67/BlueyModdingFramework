using System;
using System.Collections.Generic;
using UnityEngine;

public interface BMF_iItemPositionOffset
{
	[Serializable]
	public class BMF_ItemPositionOffsets
	{
		public BMF_EnumClass.PlayerCharacter m_id;

		[Header("Front")]
		public Vector3 frontPosition;

		public Vector3 frontRotation;

		[Header("Front 3Q Right")]
		public Vector3 frontQPosition;

		public Vector3 frontQRotation;

		[Header("Back 3Q Right")]
		public Vector3 backPosition;

		public Vector3 backRotation;
	}

	List<BMF_ItemPositionOffsets> GetItemPositionOffsets();
}
