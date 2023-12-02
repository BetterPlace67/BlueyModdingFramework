using System.Collections.Generic;
using UnityEngine;

namespace BlueyModdingFramework
{
    [CreateAssetMenu(fileName = "ItemProxy", menuName = "Bluey Modding/Item", order = 1)]
    public class BlueyItemProxy : ScriptableObject, BMF_iItemPositionOffset
    {
        [Header("--------------------")]
        [Header("VISUALS")]

        public GameObject itemPrefab;
        public Vector3 colliderBounds = new Vector3(1.4f, 1.4f, 1.4f);
        public Vector3 colliderOffset;
        public float pickupRadius = 1.8f;

        [Header("--------------------")]
        [Header("ACTIONS")]

        public BMF_EnumClass.ContextualAction m_action = BMF_EnumClass.ContextualAction.ThrowObject;

        public GameObject m_contextualActionPrompt;

        public bool m_kidsCanDoIt = true;

        public bool m_parentsCanDoIt = true;

        public int m_defaultFairyDustRewardPoints = 20;

        public bool m_isTrigger;

        public bool m_canGoInRange = true;

        public bool m_requiresUsableObject;

        protected bool[] _isPlayerPriorityAction = new bool[4];
        [Header("--------------------")]

        [Header("KEEPIE UPPIES")]

        public float m_minForceMagnitudeHorizontal = 0.3f;

        public float m_maxInitialRandomForceMagnitudeHorizontal = 0.4f;

        public float m_maxFinalRandomForceMagnitudeHorizontal = 1f;

        public float m_minForceMagnitudeVertical = 0.3f;

        public float m_maxInitialRandomForceMagnitudeVertical = 0.4f;

        public float m_maxFinalRandomForceMagnitudeVertical = 0.7f;

        public int m_hitsToGetMaxForce = 20;

        [Header("--------------------")]
        [Header("PICK")]
        public bool m_isPickable = true;

        [Header("--------------------")]
        [Header("THROW")]
        public float m_minHorizontalForce = 5f;

        public float m_minVerticalForce = 10f;

        public float m_maxHorizontalForce = 5f;

        public float m_maxVerticalForce = 10f;

        public float m_minSmashHorizontalForce = 5f;

        public float m_minSmashVerticalForce = 2.5f;

        public float m_maxSmashHorizontalForce = 10f;

        public float m_maxSmashVerticalForce = 5f;

        [Header("--------------------")]
        [Header("KICK")]
        public bool m_canBeKickedWhileWalkAndRun;

        public float m_horizontalForceKick = 10f;

        public float m_verticalForceKick = 5f;

        [Tooltip("Randomize rotation between 0 and the rotationforceVector variable")]
        public bool m_randomizeRotation;

        public Vector3 m_rotationForce = new Vector3(0f, 0f, 0f);

        [Header("--------------------")]
        [Header("NATURAL PUSH")]
        public bool m_isEpisodePickable;

        [Header("--------------------")]
        [Header("HIT")]
        public float m_limitDistanceToExecuteLowSmashAnimtion = 1.25f;

        public float m_limitDistanceToExecuteLowBigSmashAnimtion = 0.75f;

        [Tooltip("Percent of force when autohit the keepie uppies")]
        public float m_forceAutoHit = 0.5f;

        [Header("--------------------")]
        [Header("BACKWARD THROW")]
        public float m_backwardHorizontalForce = 2f;

        public float m_backwardVerticalForce = 4f;

        [Header("--------------------")]
        [Header("RIGIDBODY (physics)")]
        public float m_mass = 1f;

        public float m_drag;

        public float m_angularDrag = 0.05f;

        [Header("--------------------")]
        [Header("PHYSICS MATERIAL")]
        public float m_dynamicFriction = 0.6f;

        public float m_staticFriction = 0.6f;

        [Range(0f, 3f)]
        public float m_bounciness = 0.35f;

        public float m_collisionBounceForce = 10f;

        public PhysicMaterialCombine m_frictionCombine;

        public PhysicMaterialCombine m_bounceCombine;

        [Header("--------------------")]
        [Header("FAIRY DUST")]
        public int m_minFairyDustReceivedPickAndThrow = 10;

        public int m_maxFairyDustReceivedPickAndThrow = 20;

        public int m_probabiltyToGetRewarKeepieUppiesd = 60;

        [Header("--------------------")]
        [Header("TOY")]
        public bool m_isToy;

        //[DrawIf("m_isToy", true)]
        public BMF_EnumClass.Toys m_toyID;

        public BMF_EnumClass.TypeOfPickUpObject m_typeOfPickUpObject;

        [SerializeField]
        private bool spawnParticlesWhenInstantiate;

        [Header("--------------------")]
        [Header("SFX")]
        public string m_OnGroundSFX = "/SFX/GameworldInteractions/ObjectHit_Big";

        public string m_OnKickSFX = "/SFX/PlayerActions/BanditSteps";

        [Header("")]
        public Animator m_actionIconAnimator;

        [Header("--------------------")]
        [Header("MISC")]
        //public PullableObject m_pullableObject;

        public Vector3 m_startingScale = Vector3.one;

        public List<BMF_iItemPositionOffset.BMF_ItemPositionOffsets> m_changesTransformPlayer = new List<BMF_iItemPositionOffset.BMF_ItemPositionOffsets>
    {
        new BMF_iItemPositionOffset.BMF_ItemPositionOffsets
        {
            m_id = BMF_EnumClass.PlayerCharacter.Bluey
        },
        new BMF_iItemPositionOffset.BMF_ItemPositionOffsets
        {
            m_id = BMF_EnumClass.PlayerCharacter.Bingo
        },
        new BMF_iItemPositionOffset.BMF_ItemPositionOffsets
        {
            m_id = BMF_EnumClass.PlayerCharacter.Bandit
        },
        new BMF_iItemPositionOffset.BMF_ItemPositionOffsets
        {
            m_id = BMF_EnumClass.PlayerCharacter.Chilli
        }
    };

        public BMF_EnumClass.PlayerCharacter m_characterTarget;

        public List<BMF_iItemPositionOffset.BMF_ItemPositionOffsets> GetItemPositionOffsets()
        {
            return m_changesTransformPlayer;
        }
    }
}
