using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTracker : MonoBehaviour
{
    public enum ID
    {
        Monocle,
        Waiter,
        FanLady,
        Hobo,
        Bumpkin,
        Cowgirl,
        Cat,

        Count,
        None
    }

    [SerializeField] private NPCSpriteCatalog SpriteCatalog;
    [SerializeField] private NPCSpriteCatalog_Nobodies Nobodies;

    public bool reroll = false;

    private void Awake()
    {
        NPCSpriteCatalog.Inst = SpriteCatalog;
        NPCSpriteCatalog_Nobodies.Inst = Nobodies;
        if (reroll)
            RollCulprit();
    }

    public static ID Culprit;
    public static ID Accused;

    public static void RollCulprit()
    {
        Culprit = (ID)Random.Range(0, (int)ID.Count);
        Debug.Log("Culprit is " + Culprit.ToString());
    }

    public static bool AccusedCorrectly()
    {
        Debug.Log(string.Format("Accused {0}, culprit was {1}", Accused, Culprit));
        return Culprit == Accused;
    }

    public static Sprite GetSprite(ID id)
    {
        switch (id)
        {
            case ID.Monocle: return NPCSpriteCatalog.Inst.Monocle;
            case ID.Waiter: return NPCSpriteCatalog.Inst.Waiter;
            case ID.FanLady: return NPCSpriteCatalog.Inst.FanLady;
            case ID.Hobo: return NPCSpriteCatalog.Inst.Hobo;
            case ID.Bumpkin: return NPCSpriteCatalog.Inst.Bumpkin;
            case ID.Cowgirl: return NPCSpriteCatalog.Inst.Cowgirl;
            case ID.Cat: return NPCSpriteCatalog.Inst.Cat;
            default: return NPCSpriteCatalog.Inst.Cat;
        }
    }

    public static Sprite GetSprite_Nobody()
    {
        return NPCSpriteCatalog_Nobodies.Inst.Sprites[
            Random.Range(0, NPCSpriteCatalog_Nobodies.Inst.Sprites.Length)
        ];
    }
}
