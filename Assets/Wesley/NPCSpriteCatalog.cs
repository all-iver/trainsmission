using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/NPCSpriteCatalog")]
public class NPCSpriteCatalog : ScriptableObject
{
    public static NPCSpriteCatalog Inst;

    public Sprite Monocle;
    public Sprite Waiter;
    public Sprite FanLady;
    public Sprite Hobo;
    public Sprite Bumpkin;
    public Sprite Cowgirl;
    public Sprite Cat;
}
