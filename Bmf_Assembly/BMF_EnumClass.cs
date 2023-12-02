
public static class BMF_EnumClass
{
	public enum ScreenLoader
	{
		LoadingScreen = 0,
		SplashScreen = 1,
		BlackFadeInOut = 2
	}

	public enum PlayerCharacter
	{
		Bluey = 0,
		Bingo = 1,
		Bandit = 2,
		Chilli = 3
	}

	public enum NpcCharacter
	{
		Muffin = 0,
		Stripe = 1,
		Rad = 2,
		Mort = 3,
		Chattermax = 4
	}

	public enum PlayerRotationLookingAt
	{
		Front = 0,
		FrontLeft = 1,
		SideLeft = 2,
		BackLeft = 3,
		Back = 4,
		BackRight = 5,
		SideRight = 6,
		FrontRight = 7
	}

	public enum Activity
	{
		None = 0,
		TheChattermaxHunt = 1,
		FreezeTag = 2,
		FloorIsLava = 3,
		MagicClaw = 4,
		KeepieUppies = 5
	}

	public enum ActivityState
	{
		Started = 0,
		Ended = 1
	}

	public enum Episodes
	{
		Episode1 = 0,
		Episode2 = 1,
		Episode3 = 2,
		Episode4 = 3
	}

	public enum MagicClawState
	{
		NoStarted = 0,
		CameraClamping = 1,
		Intro = 2,
		Moving = 3,
		WaitPressButtonAnim = 4,
		TryingToCatchToy = 5,
		PickingToy = 6,
		EndingCelebration = 7,
		Ending = 8
	}

	public enum ContextualAction
	{
		Jump = 0,
		PickObject = 1,
		ThrowObject = 2,
		Crawl = 3,
		StandUp = 4,
		TurnLight = 5,
		PullOrPushObject = 6,
		ReleasePullableObject = 7,
		StartActivity = 8,
		HitObject = 9,
		OpenFurniture = 10,
		CloseFurniture = 11,
		JoinSeesaw = 12,
		LeaveSeesaw = 13,
		OpenHappyDaysMenu = 14,
		CloseHappyDaysMenu = 15,
		PickSeeds = 16,
		PickShovel = 17,
		PickPick = 18,
		PickWateringCan = 19,
		PlantSeeds = 20,
		DigHole = 21,
		WaterPlants = 22,
		RemovePlant = 23,
		FreezePlayer = 24,
		FreePlayer = 25,
		OpenCostumeChestMenu = 26,
		CloseCostumeChestMenu = 27,
		VolleyballServe = 28,
		StartClawActivity = 29,
		StartChattermaxActivity = 30,
		None = 31,
		StartVolleyballActivity = 32,
		LeaveObject = 33,
		JumpOntoBed = 34,
		JumpOntoParent = 35,
		NaturalPush = 36,
		Kick = 37,
		LandOnGround = 38,
		ThrowChild = 39,
		OpenShopMenu = 40,
		CloseShopMenu = 41,
		OpenStickerAlbum = 42,
		RefillWateringCan = 43,
		StartFloorIsLava = 44,
		StopFloorIsLava = 45,
		TeleportTo = 46,
		StartMagicXilophoneActivity = 47,
		StopPlayingMagicXilophoneActivity = 48,
		StartNPCDialog = 49,
		OpenToyBoxInventory = 50,
		CloseToyBoxInventory = 51,
		StartKeepieUppies = 52,
		StopKeepieUppies = 53,
		PickBalloon = 54,
		StopChattermaxActivity = 55,
		ThrowBalloon = 56,
		OpenCloseLevelSelector = 57,
		OpenCloseTap = 58,
		MaxWaterPlant = 59,
		Slide = 60,
		SlideJump = 61,
		SlideEnd = 62,
		Run = 63,
		JumpTutorial = 64,
		ThrowTrashBag = 65,
		TurnOffFaucet = 66,
		CarryBalloon = 67,
		MortInteract = 68,
		PushPullHoriz = 69,
		PushPullVert = 70
	}

	public enum ContextualPlayerStates
	{
		Neutral = 0,
		CarryingItem = 1,
		PushingOrPulling = 2,
		Jumping = 3,
		JumpingWithItem = 4,
		Crawling = 5,
		CrawlingWithItem = 6,
		OnParent = 7,
		OnParentWithItem = 8,
		PlayingFloorIsLava = 9,
		PlayingMagicXilophone = 10,
		PlayingKeepieUppie = 11,
		Sliding = 12,
		PlayingChattermax = 13
	}

	public enum ContextualActionJoystickAnim
	{
		None = 0,
		Up = 1,
		Down = 2,
		Left = 3,
		Right = 4,
		Vertical = 5,
		Horizontal = 6
	}

	public enum ContextualActionButtonAnim
	{
		None = 0,
		Accept = 1,
		Cancel = 2,
		Interact = 3,
		InteractPressing = 4
	}

	public enum ObjectSize
	{
		Big = 0,
		Mid = 1,
		SpecialSize = 2
	}

	public enum PullingDirection
	{
		Horizontal = 0,
		Vertical = 1
	}

	public enum AudioTypes
	{
		Loop = 0,
		OneShot = 1,
		VoiceOver = 2,
		Empty = 3
	}

	public enum MisionType
	{
		GoToPoint = 0,
		TalkToNPC = 1,
		CarryObjectToPoint = 2,
		CarryObjectsToPoint = 3,
		CarryObjectToNPC = 4,
		PerformObjectAction = 5,
		KeepyUppy = 6,
		WaterPlant = 7,
		Activity = 8,
		RefillWateringCan = 9,
		Trashbag = 10
	}

	public enum GroundState
	{
		Planted = 0,
		Grew = 1,
		Dug = 2,
		Empty = 3
	}

	public enum Plants
	{
		Peperomia = 0,
		Calathea = 1,
		CutLeafDaisy = 2,
		Agapanthus = 3,
		Hydrangea = 4,
		FanFlower = 5,
		Gardenia = 6,
		HairpinBanksia = 7,
		BrisbaneRivelLilly = 8,
		BlueFlax = 9,
		Bottlebrush = 10,
		JacarandaTree = 11,
		Episode3HH = 12
	}

	public enum VcamPriority
	{
		PreFollow = 4,
		Follow = 5,
		PosFollow = 6,
		Activity = 10,
		EpisodeDolly = 14,
		Cinematic = 15
	}

	public enum MusicThemes
	{
		None = 0,
		BlueyIntro = 1,
		HeelerHouse = 2,
		TheYard = 3,
		Playgrounds = 4,
		TheCreek = 5,
		TheBeach = 6,
		KeepyUppy = 7,
		MagicXylophone = 8,
		FloorIsLava = 9,
		ChattermaxChase = 10,
		Episode2 = 11,
		Episode3 = 12,
		Episode4_LongLoop = 13,
		Episode4_ShortLoop = 14,
		Ep1St1_ItsHoliday = 15,
		Ep4St67_BeatifullCreekV1 = 16,
		Ep4St67_BeatifullCreekV2 = 17,
		Ep4St68TheNewTreasure = 18,
		Ep1St26_TheBalloonFliesAway = 19,
		Ep3St75_AllsWellThatEndsWell = 20,
		MagicXylophoneV2 = 21,
		Ep2St31AFineReward = 22,
		Ep1Stage32PathCleared = 23
	}

	public enum MusicMode
	{
		Episode = 0,
		Activity = 1,
		Normal = 2
	}

	public enum CostumeType
	{
		Head = 0,
		Face = 1,
		Body = 2,
		Hands = 3,
		Feets = 4
	}

	public enum CostumePosition
	{
		Right = 0,
		Left = 1,
		Both = 2,
		Null = 3
	}

	public enum Iks
	{
		RightHand = 0,
		LeftHand = 1
	}

	public enum ToysRoomLocation
	{
		GirlsRoom = 0,
		GirlsRoomHall = 1,
		KiwiRoom = 2
	}

	public enum TriggerCelebrationSubStateAnimator
	{
		Chattermax = 0,
		None = 1
	}

	public enum ConetxtualActionPriority
	{
		High = 0,
		Medium = 1,
		Low = 2,
		VeryLow = 3
	}

	public enum Stickers
	{
		Peperomia = 0,
		Calathea = 1,
		CutDaisy = 2,
		Agapanthus = 3,
		Hydrangea = 4,
		FanFlower = 5,
		Gardenia = 6,
		HairpinBanksia = 7,
		BrisbaneLilly = 8,
		BlueFlax = 9,
		Bottlebrush = 10,
		JacarandaTree = 11,
		Dolphin = 12,
		Bunny = 13,
		Teddy = 14,
		StripedCat = 15,
		Frog = 16,
		WhiteDuck = 17,
		Monkey = 18,
		Cloud = 19,
		Polly = 20,
		StickyGekko = 21,
		Floppy = 22,
		Unicorse = 23,
		HatPup = 24,
		Balloon = 25,
		Ball = 26,
		BeachBall = 27,
		ElasticBed = 28,
		BlueyEp1 = 29,
		BingoEp1 = 30,
		BanditEp1 = 31,
		ChilliEp1 = 32,
		BlueyEp2 = 33,
		BingoEp2 = 34,
		BanditEp2 = 35,
		ChilliEp2 = 36,
		BlueyEp3 = 37,
		BingoEp3 = 38,
		BanditEp3 = 39,
		ChilliEp3 = 40,
		BlueyEp4 = 41,
		BingoEp4 = 42,
		BanditEp4 = 43,
		ChilliEp4 = 44,
		BlueyEp5 = 45,
		BingoEp5 = 46,
		BanditEp5 = 47,
		ChilliEp5 = 48,
		Lion = 49,
		Penguin = 50,
		None = 51,
		JumpingBall = 52,
		VolleyBall = 53,
		SoccerBall = 54,
		Basketball = 55
	}

	public enum StickerItemTypes
	{
		Plant = 0,
		StuffedAnimal = 1,
		SpecialToy = 2,
		CharacterPose = 3,
		Collectible = 4
	}

	public enum Shops
	{
		Toys = 0,
		Garden = 1
	}

	public enum CrawlingDirection
	{
		none = 0,
		xAxis = 1,
		yAxis = 2
	}

	public enum Toys
	{
		Null = 0,
		Bunny = 1,
		Cloud = 2,
		Dolphin = 3,
		Floppy = 4,
		Frog = 5,
		GreenHatPuppy = 6,
		Monkey = 7,
		Polly = 8,
		StrippedCat = 9,
		Teddy = 10,
		Unihorse = 11,
		WhiteDuck = 12
	}

	public enum WheelPositions
	{
		North = 0,
		East = 1,
		South = 2,
		West = 3
	}

	public enum WheelStatus
	{
		Available = 0,
		Unavailable = 1
	}

	public enum Rooms
	{
		LivingRoom = 0,
		GirlsBedroom = 1,
		TvRoom = 2,
		KitchenRoom = 3,
		BathToiletRoom = 4,
		MasterBedroom = 5,
		BabyRoom = 6,
		PlayRoom = 7,
		Hall = 8,
		HallwayEast = 9,
		HallwayWest = 10,
		HallwayDownstairs = 11,
		Verandah = 12,
		TopDeck = 13,
		Entrance = 14,
		FrontYard = 15
	}

	public enum DayTime
	{
		Morning = 0,
		afternoon = 1,
		Night = 2
	}

	public enum Scenes
	{
		SetUp = 0,
		MainMenu = 1,
		GameplayLoader = 2,
		HeelerHouse = 3,
		Playgrounds = 4,
		TheCreek = 5,
		TheBeach = 6,
		TheYard = 7
	}

	public enum KeepyUppyType
	{
		None = 0,
		Balloon = 1,
		BeachBall = 2,
		Stripedball = 3
	}

	public enum SubtitleVoice
	{
		Generic = 0,
		Bluey = 1,
		Bingo = 2,
		Bandit = 3,
		Chilli = 4,
		Muffin = 5,
		Stripe = 6,
		Rad = 7,
		Mort = 8,
		Nana = 9
	}

	public enum OptionsText
	{
		OptionVoices = 0,
		OptionSubtitles = 1,
		OptionUIText = 2
	}

	public enum OptionVoices
	{
		en_US = 0,
		es_ES = 1,
		es_MX = 2,
		ar_SA = 3,
		it_IT = 4,
		de_DE = 5,
		nl_NL = 6,
		pl_PL = 7,
		da_DK = 8,
		sv_SE = 9,
		nb_NO = 10,
		fi_FI = 11,
		pt_BR = 12,
		zh_CN = 13,
		zh_TW = 14,
		ko_KR = 15,
		fr_FR = 16
	}

	public enum OptionSubtitles
	{
		None = 0,
		Yes = 1
	}

	public enum OptionUIText
	{
		English = 0
	}

	public enum RewardTravelingParticles
	{
		Orbstraveling = 1,
		Flares = 2,
		FinalBurst = 3
	}

	public enum Compare
	{
		Greater = 0,
		GreaterOrEqual = 1,
		Equal = 2,
		LessOrEqual = 3,
		Less = 4
	}

	public enum CardinalPoints
	{
		North = 0,
		South = 1,
		East = 2,
		West = 3
	}

	public enum AreaCollectibles
	{
		HeelerHouse = 0,
		Playgrounds = 1,
		Garden = 2,
		TheCreek = 3,
		TheBeach = 4
	}

	public enum StickerBookTutorial
	{
		Intro = 0,
		CollectiblesHeelerHouse = 1,
		CollectiblesPlayground = 2,
		CollectiblesGarden = 3,
		CollectiblesTheCreekAndTheBeach = 4,
		EpisodeSelection = 5
	}

	public enum SlideStatus
	{
		None = 0,
		Sliding = 1,
		Flying = 2
	}

	public enum TypeOfPickUpObject
	{
		Generic = 0,
		Book = 1,
		WoodToy = 2
	}

	public enum SeesawSeats
	{
		Left = 0,
		Rigth = 1
	}

	public enum EndEpisodeMenuState
	{
		None = 0,
		EndMenu = 1,
		PopUp = 2
	}

	public enum AILookTo
	{
		CameraTarget = 0,
		InverseCameraTarget = 1,
		Front = 2,
		Left = 3,
		Right = 4,
		Back = 5
	}
}
