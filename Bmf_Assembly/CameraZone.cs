using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlueyModdingFramework
{
    public class CameraZone : MonoBehaviour
    {
        public CinemachineVirtualCamera virtualCamera;
        public BMF_EnumClass.VcamPriority vcamPriority;

        private void Awake()
        {
            TrackSimpleTrigger trigger = gameObject.AddComponent<TrackSimpleTrigger>();

            EnumClass.VcamPriority priority = new EnumClass.VcamPriority();

            switch (vcamPriority) {
                case BMF_EnumClass.VcamPriority.PreFollow: priority = EnumClass.VcamPriority.PreFollow; break;
                case BMF_EnumClass.VcamPriority.PosFollow: priority = EnumClass.VcamPriority.PosFollow; break;
                case BMF_EnumClass.VcamPriority.Activity: priority = EnumClass.VcamPriority.Activity; break;
                case BMF_EnumClass.VcamPriority.Follow: priority = EnumClass.VcamPriority.Follow; break;
                case BMF_EnumClass.VcamPriority.Cinematic: priority = EnumClass.VcamPriority.Cinematic; break;
            }

            trigger.m_vcamPriority = priority;

            trigger._cinemachineVirtualCamera = virtualCamera;
        }
    }
}
