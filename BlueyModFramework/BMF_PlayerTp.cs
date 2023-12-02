using BlueyModdingFramework;
using KinematicCharacterController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BlueyModFramework
{
    public class BMF_PlayerTp : MonoBehaviour
    {
        void Start() {

            Debug.Log("Attempting to TP to: " + gameObject.name);

            ProxyLevelManager proxy = gameObject.GetComponent<ProxyLevelManager>();

            PlayerMovementKinematic[] players = GameObject.FindObjectsOfType<PlayerMovementKinematic>();
            foreach (PlayerMovementKinematic player in players) {
                switch(player.gameObject.name) {
                    case "Bluey": player.TeleportTo(proxy.m_listTpScenes[0].m_positionsToTpPlayers[0].transform.position, true); break;
                    case "Bingo": player.TeleportTo(proxy.m_listTpScenes[0].m_positionsToTpPlayers[1].transform.position, true); break;
                    case "Bandit": player.TeleportTo(proxy.m_listTpScenes[0].m_positionsToTpPlayers[2].transform.position, true); break;
                    case "Chilli": player.TeleportTo(proxy.m_listTpScenes[0].m_positionsToTpPlayers[3].transform.position, true); break;
                }
            }
        }
    }
}
