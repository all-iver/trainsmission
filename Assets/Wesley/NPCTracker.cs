﻿using System.Collections;
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
        Conductor,
        Prospector,
        Apple,
        Overalls,
        WhiteDress,
		TwinRed,
		TwinBlue,

        Count,
        None
    }

    [SerializeField] private NPCSpriteCatalog SpriteCatalog;

    public bool reroll = false;

    private void Awake()
    {
        NPCSpriteCatalog.Inst = SpriteCatalog;

		if (reroll)
            RollCulprit();
	}

    public static ID Culprit;
    public static ID RedHerring;
    public static ID Accused;

	public static bool IsTwin(ID npcid)
	{
		return
			npcid == ID.TwinRed
			|| npcid == ID.TwinBlue
		;
	}

    public static void RollCulprit()
    {
        Culprit = GetRandomNPCID();

		//both twins feel the same, so they should only occur roughly half the time
		if (IsTwin(Culprit) && Random.value < 0.5f)
			Culprit = GetRandomNPCID();

        RedHerring = Culprit;
        while (RedHerring == Culprit)
            RedHerring = GetRandomNPCID();
        Debug.Log(string.Format("Culprit is {0}, Red Herring is {1}", Culprit, RedHerring));
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
            case ID.Conductor: return NPCSpriteCatalog.Inst.Conductor;
            case ID.Prospector: return NPCSpriteCatalog.Inst.Prospector;
            case ID.Apple: return NPCSpriteCatalog.Inst.Apple;
            case ID.Overalls: return NPCSpriteCatalog.Inst.Overalls;
            case ID.WhiteDress: return NPCSpriteCatalog.Inst.WhiteDress;
			case ID.TwinRed: return NPCSpriteCatalog.Inst.TwinRed;
			case ID.TwinBlue: return NPCSpriteCatalog.Inst.TwinBlue;
			default: return NPCSpriteCatalog.Inst.Cat;
        }
    }

    public static ID GetRandomNPCID()
    {
        return (ID)Random.Range(0, (int)NPCTracker.ID.Count);
    }

    public static NPC FindNPC(ID id)
    {
        var npcs = GameObject.FindObjectsOfType<NPC>();
        foreach (var npc in npcs)
        {
            if (npc.ID == id)
                return npc;
        }
        return null;
    }

    public static NPC FindCulprit()
    {
        return FindNPC(Culprit);
    }
}
