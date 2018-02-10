using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechHelper : MonoBehaviour
{
    public enum TraincarID
    {
        Caboose,
        Storage,
        Passenger,
        Dining,
        Bar,
        Engine
    }

    private static SpeechCatalog_Traincars SpeechCatalog_Traincars;
    private static SpeechCatalog_People SpeechCatalog_People;
    private static SpeechCatalog_Directions SpeechCatalog_Directions;
    private static SpeechCatalog_Certainty SpeechCatalog_Certainty;
	public static SpeechCatalog_Misc MiscIcons;
	private static NPCTraitList_Catalog TraitList;
    public static Sprite UnknownSprite;
	public static Sprite DrunkSprite;

	[SerializeField] private SpeechCatalog_Traincars SpeechCatalog_Traincars_Cached;
    [SerializeField] private SpeechCatalog_People SpeechCatalog_People_Cached;
    [SerializeField] private SpeechCatalog_Directions SpeechCatalog_Directions_Cached;
    [SerializeField] private SpeechCatalog_Certainty SpeechCatalog_Certainty_Cached;
	[SerializeField] private SpeechCatalog_Misc SpeechCatalog_Misc_Cached;
	[SerializeField] private NPCTraitList_Catalog TraitList_Cached;
	[SerializeField] private Sprite DrunkSprite_Cached;
	[SerializeField] private Sprite UnknownSprite_Cached;

    private void Awake()
    {
        SpeechCatalog_Traincars = SpeechCatalog_Traincars_Cached;
        SpeechCatalog_People = SpeechCatalog_People_Cached;
        SpeechCatalog_Directions = SpeechCatalog_Directions_Cached;
        SpeechCatalog_Certainty = SpeechCatalog_Certainty_Cached;
		MiscIcons = SpeechCatalog_Misc_Cached;
		TraitList = TraitList_Cached;
        UnknownSprite = UnknownSprite_Cached;
		DrunkSprite = DrunkSprite_Cached;
	}

    public static Sprite GetIcon_Certainty(bool certain)
    {
        if (certain) return SpeechCatalog_Certainty.Certain;
        else return SpeechCatalog_Certainty.Uncertain;
    }

    public static Sprite GetIcon_Car(TraincarID id)
    {
        switch (id)
        {
            case TraincarID.Caboose: return SpeechCatalog_Traincars.Caboose;
            case TraincarID.Storage: return SpeechCatalog_Traincars.Storage;
            case TraincarID.Passenger: return SpeechCatalog_Traincars.Passenger;
            case TraincarID.Dining: return SpeechCatalog_Traincars.Dining;
            case TraincarID.Bar: return SpeechCatalog_Traincars.Bar;
            case TraincarID.Engine: return SpeechCatalog_Traincars.Engine;
            default: return UnknownSprite;
        }
    }

    public static Sprite GetIcon_Direction(float delta)
    {
        if (delta < 0.0f) return SpeechCatalog_Directions.Left;
        else return SpeechCatalog_Directions.Right;
    }

    public static Sprite GetIcon_Person_Random()
    {
        return GetIcon_Person(NPCTracker.GetRandomNPCID());
    }

    public static NPCTraitList GetTraitList(NPCTracker.ID id)
    {
        switch (id)
        {
			case NPCTracker.ID.Monocle: return TraitList.Monocle;
			case NPCTracker.ID.Waiter: return TraitList.Waiter;
			case NPCTracker.ID.FanLady: return TraitList.FanLady;
			case NPCTracker.ID.Hobo: return TraitList.Hobo;
			case NPCTracker.ID.Bumpkin: return TraitList.Bumpkin;
			case NPCTracker.ID.Cowgirl: return TraitList.Cowgirl;
			case NPCTracker.ID.Cat: return TraitList.Cat;
			case NPCTracker.ID.Conductor: return TraitList.Conductor;
			case NPCTracker.ID.Prospector: return TraitList.Prospector;
			case NPCTracker.ID.Apple: return TraitList.Apple;
			case NPCTracker.ID.Overalls: return TraitList.Overalls;
			case NPCTracker.ID.WhiteDress: return TraitList.WhiteDress;
			case NPCTracker.ID.TwinRed: return TraitList.TwinRed;
			case NPCTracker.ID.TwinBlue: return TraitList.TwinBlue;
			default: return null;
        }
    }

    public static Sprite GetIcon_Person(NPCTracker.ID id)
    {
        switch (id)
        {
            case NPCTracker.ID.Monocle: return SpeechCatalog_People.Monocle;
            case NPCTracker.ID.Waiter: return SpeechCatalog_People.Waiter;
            case NPCTracker.ID.FanLady: return SpeechCatalog_People.FanLady;
            case NPCTracker.ID.Hobo: return SpeechCatalog_People.Hobo;
            case NPCTracker.ID.Bumpkin: return SpeechCatalog_People.Bumpkin;
            case NPCTracker.ID.Cowgirl: return SpeechCatalog_People.Cowgirl;
            case NPCTracker.ID.Cat: return SpeechCatalog_People.Cat;
            case NPCTracker.ID.Conductor: return SpeechCatalog_People.Conductor;
            case NPCTracker.ID.Prospector: return SpeechCatalog_People.Prospector;
            case NPCTracker.ID.Apple: return SpeechCatalog_People.Apple;
            case NPCTracker.ID.Overalls: return SpeechCatalog_People.Overalls;
            case NPCTracker.ID.WhiteDress: return SpeechCatalog_People.WhiteDress;
			case NPCTracker.ID.TwinRed: return SpeechCatalog_People.TwinRed;
			case NPCTracker.ID.TwinBlue: return SpeechCatalog_People.TwinBlue;
			default: return UnknownSprite;
        }
    }

}
