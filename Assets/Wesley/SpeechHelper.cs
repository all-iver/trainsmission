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
    public static Sprite UnknownSprite;

    [SerializeField] private SpeechCatalog_Traincars SpeechCatalog_Traincars_Cached;
    [SerializeField] private SpeechCatalog_People SpeechCatalog_People_Cached;
    [SerializeField] private SpeechCatalog_Directions SpeechCatalog_Directions_Cached;
    [SerializeField] private SpeechCatalog_Certainty SpeechCatalog_Certainty_Cached;
    [SerializeField] private Sprite UnknownSprite_Cached;

    private void Awake()
    {
        SpeechCatalog_Traincars = SpeechCatalog_Traincars_Cached;
        SpeechCatalog_People = SpeechCatalog_People_Cached;
        SpeechCatalog_Directions = SpeechCatalog_Directions_Cached;
        SpeechCatalog_Certainty = SpeechCatalog_Certainty_Cached;
        UnknownSprite = UnknownSprite_Cached;
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
        return GetIcon_Person((NPCTracker.ID)Random.Range(0, (int)NPCTracker.ID.Count));
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
            default: return UnknownSprite;
        }
    }

}
