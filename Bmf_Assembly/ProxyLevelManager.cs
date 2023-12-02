using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//using KinematicCharacterController;

public class ProxyLevelManager : MonoBehaviour
    {
        //[FoldoutGroup("Camera", 0)]
        public List<CinemachineVirtualCamera> m_levelFollowVirtualCameras;

        [Header("Spawn Points")]
        public List<BMF_TpPlayersPositions> m_listTpScenes;

        //[FoldoutGroup("Lights", 0)]
        public Light m_mainDirectionalLight;

        [Header("Player Movement")]
        public Vector3 playerGravity = new Vector3(0, -22, 0);
        public float playerWalkSpeed = 2.15f, playerRunSpeed = 4.15f, playerAirSpeed = 4.75f, playerJumpSpeed = 6.25f;

    [HideInInspector]
    public static ProxyLevelManager callback;

    private void Awake()
    {
        callback= this;
    }

    /*private void Start()
    {
        PlayerMovementKinematic[] players = GameObject.FindObjectsOfType<PlayerMovementKinematic>();

        foreach (PlayerMovementKinematic player in players) {
            player.TeleportTo(m_listTpScenes[0].m_positionsToTpPlayers[0].transform.position, true);
        }
    }*/
}
