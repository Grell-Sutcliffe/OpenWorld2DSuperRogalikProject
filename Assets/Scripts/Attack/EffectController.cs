using System.ComponentModel;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    Animator anim;
    ElementType handledEffect;
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    protected int HandleEffect(Damage dmg)
    {
        return 1;
    }
}

public enum ElementType
{
    Physical = 0,
    Cryo = 1,
    Pyro = 2,
    Energo = 3,
    Floro = 4,
}