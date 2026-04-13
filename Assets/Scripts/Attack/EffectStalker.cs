using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EffectStalker : MonoBehaviour
{
    Animator anim;
    Creature creature;
    ElementType handledEffect = ElementType.None;

    Dictionary<ElementType, ElementType> dict_elementType_to_opposite_elementType;

    bool canHandle;
    void Start()
    {   
        anim = GetComponent<Animator>();
        creature = GetComponentInParent<Creature>();
        canHandle = true;
        MakeDictionary();
    }

    void MakeDictionary()
    {
        dict_elementType_to_opposite_elementType = new Dictionary<ElementType, ElementType>();

        dict_elementType_to_opposite_elementType[ElementType.Physical] = ElementType.None;
        dict_elementType_to_opposite_elementType[ElementType.None] = ElementType.None;
        dict_elementType_to_opposite_elementType[ElementType.Cryo] = ElementType.Pyro;
        dict_elementType_to_opposite_elementType[ElementType.Pyro] = ElementType.Cryo;
        dict_elementType_to_opposite_elementType[ElementType.Energo] = ElementType.Floro;
        dict_elementType_to_opposite_elementType[ElementType.Floro] = ElementType.Energo;
    }

    void flush()
    {
        handledEffect = ElementType.None;
        creature.ChangeHandledElement(ElementType.None);
    }
    void handle(ElementType elementType)
    {
        handledEffect = elementType;
        creature.ChangeHandledElement(elementType);
    }
    public void HandleEffect(Damage new_damage)
    {
        if (new_damage.element_type == ElementType.None)
        {
            Debug.LogError("Не должно быть нана в оружке лол");
            return;
        }

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
            Debug.LogError("двойная бебе");
            // handledEffect = new_damage.element_type;

            new_damage.elemental_dmg *= 2;

            //return;
        }
        if (!canHandle) return;
        // АНИМАШКИ ДАМАГОВ

        // НАКЛАДЫВАЕТСЯ dmg.element_type
        if (handledEffect == ElementType.None)
        {
            handle(new_damage.element_type);
            return;
        }
        Debug.Log($"Наложение {new_damage.element_type} уже был {handledEffect}");

        switch (handledEffect, new_damage.element_type)
        {
            case (ElementType.Cryo, ElementType.Cryo):
                //Debug.Log("крио на крио");
                CryoToCryo();
                break;

            case (ElementType.Pyro, ElementType.Pyro):
                // логика
                break;

            case (ElementType.Floro, ElementType.Floro):
                // логика
                break;
            case (ElementType.Energo, ElementType.Energo):
                // логика
                break;

            case (ElementType.Cryo, ElementType.Pyro):
                PyroToCryo();
                break;
            case (ElementType.Cryo, ElementType.Floro):
                // логика
                break;
            case (ElementType.Cryo, ElementType.Energo):
                CryoToEnergo();
                break;

            case (ElementType.Pyro, ElementType.Cryo):
                PyroToCryo();
                break;
            case (ElementType.Pyro, ElementType.Floro):
                // логика
                break;
            case (ElementType.Pyro, ElementType.Energo):
                PyroToEnergo();
                break;

            case (ElementType.Floro, ElementType.Cryo):
                // логика
                break;
            case (ElementType.Floro, ElementType.Pyro):
                // логика
                break;
            case (ElementType.Floro, ElementType.Energo):
                // логика
                break;

            case (ElementType.Energo, ElementType.Cryo):
                // логика
                break;
            case (ElementType.Energo, ElementType.Pyro):
                // логика
                break;
            case (ElementType.Energo, ElementType.Floro):
                // логика
                break;
        }
        handle(ElementType.None);

    }

    public delegate void EffectDelegate(bool a);
    public void StartStopAnim(float duration, EffectDelegate del = null)
    {
        StartCoroutine(StopAnimCoroutine(duration, del));
    }


    private IEnumerator StopAnimCoroutine(float duration, EffectDelegate del = null)
    {
        del?.Invoke(true);
        canHandle = false;
        yield return new WaitForSeconds(duration);

        del?.Invoke(false);
        canHandle = true;
        StopAnim();
    }

    void StopCreatuer(bool isStop)
    {
        creature.isStopped = isStop;
    }
    public void StopAnim()
    {
        canHandle = true;
        SetAnim(0);
        anim.SetTrigger("Stop");
    }
    void CryoToCryo()
    {
        //Debug.Log("крио внутри функции");
        SetAnim(11);
        StartStopAnim(2, StopCreatuer);
    }
    void PyroToCryo()
    {
        canHandle = false;
        SetAnim(21);
    }
    void DoDamage()
    {
        creature.TakeDamage(new Damage(2, 0, ElementType.Physical));
    }
    void PyroToEnergo()
    {
        SetAnim(23);
        StartStopAnim(1);
    }
    void SlowDown(bool isSlowed)
    {

    }
    void CryoToEnergo()
    {
        SetAnim(13);
        StartStopAnim(1);
    }

    void SetAnim(int react)
    {
        Debug.Log($"Наложение на {creature.name} реакции {react}");
        anim.SetInteger("Reaction", react);
    }
    int concateElementTypes(ElementType elementType1, ElementType elementType2)
    {
        int a = (int)elementType1;
        int b = (int)elementType2;

        string sumString = a.ToString() + b.ToString();
        int result = int.Parse(sumString);
        return result;
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