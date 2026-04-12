using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    Animator anim;
    Creature creature;
    ElementType handledEffect;

    Dictionary<ElementType, ElementType> dict_elementType_to_opposite_elementType;

    void Start()
    {
        anim = GetComponent<Animator>();
        creature = GetComponentInParent<Creature>();

        MakeDictionary();
    }

    void MakeDictionary()
    {
        dict_elementType_to_opposite_elementType = new Dictionary<ElementType, ElementType>();

        dict_elementType_to_opposite_elementType[ElementType.Physical] = ElementType.None;
        dict_elementType_to_opposite_elementType[ElementType.Cryo] = ElementType.Pyro;
        dict_elementType_to_opposite_elementType[ElementType.Pyro] = ElementType.Cryo;
        dict_elementType_to_opposite_elementType[ElementType.Energo] = ElementType.Floro;
        dict_elementType_to_opposite_elementType[ElementType.Floro] = ElementType.Energo;
    }

    public void HandleEffect(Damage new_damage)
    {
        if (new_damage.element_type == ElementType.Physical)
        {
            // ФИЗИЧЕСКИЙ ДАМАГ, НЕ ЗАВИСИТ ОТ ЭЛЕМЕНТА СУЩЕСТВА

            new_damage.physical_dmg += new_damage.elemental_dmg;
            new_damage.elemental_dmg = 0;

            return;
        }
        
        if (new_damage.element_type == creature.elementType_of_creature)
        {
            // ИММУНИТЕТ, НЕТ ДАМАГА И РЕАКЦИЙ (писать "иммунитет" или "сопротивление") --- бьём только физой

            new_damage.elemental_dmg = 0;

            return;
        }
        
        if (new_damage.element_type == dict_elementType_to_opposite_elementType[creature.elementType_of_creature] || new_damage.element_type == dict_elementType_to_opposite_elementType[handledEffect])
        {
            // УСИЛЕННЫЙ ДАМАГ Х2. после дамага все элементы должны сняться с сущности

            // НАКЛАДЫВАЕТСЯ dmg.element_type
            handledEffect = new_damage.element_type;

            new_damage.elemental_dmg *= 2;

            return;
        }

        // АНИМАШКИ ДАМАГОВ

        // НАКЛАДЫВАЕТСЯ dmg.element_type
        handledEffect = new_damage.element_type;
    }






}

public enum ElementType
{
    Physical = 0,
    Cryo = 1,
    Pyro = 2,
    Energo = 3,
    Floro = 4,
    None = 5,
}