using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BMF_TpPlayersPositions
{
	//public BMF_EnumClass.Scenes m_scene; 

	[Tooltip("Ordered by EnumClass.PlayerCharacter -> Bluey ,Bingo ,Bandit ,Chilli")]
	public List<GameObject> m_positionsToTpPlayers = new List<GameObject>(4);
}
