using UnityEngine;
using System;
using System.Collections.Generic;

public class Flags
{
    private readonly Dictionary<string, bool> _bools = new();

    public bool GetBool(string id) => _bools.TryGetValue(id, out var v) && v;
    public void SetBool(string id, bool value) => _bools[id] = value;
}

public static class EventBus
{
    public static event Action<IEvent> OnEvent;

    public static void Raise(IEvent e)
    {
        OnEvent?.Invoke(e);
    }
}

public interface IEvent { }

public readonly struct ItemCollectedEvent : IEvent
{
    public readonly int item_id;
    public readonly string item_name;
    public readonly int amount;

    public ItemCollectedEvent(int item_id, string item_name, int amount)
    {
        this.item_id = item_id;
        this.item_name = item_name;
        this.amount = amount;
    }
}

public readonly struct EnemyKilledEvent : IEvent
{
    public readonly string enemy_name;

    public EnemyKilledEvent(string enemy_name)
    {
        this.enemy_name = enemy_name;
    }
}

public readonly struct CharacterUpgradeEvent : IEvent
{
    public readonly int level;

    public CharacterUpgradeEvent(int level)
    {
        this.level = level;
    }
} 

public readonly struct ChestOpenedEvent : IEvent
{
    //public readonly string enemy_name;

    public ChestOpenedEvent(string chest_name = "")
    {
        //this.enemy_name = enemy_name;
    }
}

public readonly struct LocationEnteredEvent : IEvent
{
    public readonly string location_title;
    public readonly string creature_name;

    public LocationEnteredEvent(string location_title, string creature_name)
    {
        this.location_title = location_title;
        this.creature_name = creature_name;
    }
}

public readonly struct WishMadeEvent : IEvent
{
    public readonly int wish_amount;
    public readonly List<WishReward> rewards;

    public WishMadeEvent(int wish_amount, List<WishReward> rewards)
    {
        this.wish_amount = wish_amount;
        this.rewards = rewards;
    }
}

public readonly struct ItemUsedEvent : IEvent
{
    public readonly string item_name;

    public ItemUsedEvent(string item_name)
    {
        this.item_name = item_name;
    }
}

public readonly struct ItemDeliveredEvent : IEvent
{
    public readonly List<CollectableItem> collectableItems;
    public readonly bool is_success;

    public ItemDeliveredEvent(List<CollectableItem> collectableItems, bool is_success)
    {
        this.collectableItems = collectableItems;
        this.is_success = is_success;
    }
}


public readonly struct ItemAcceptedEvent : IEvent
{
    public readonly List<CollectableItem> collectableItems;

    public ItemAcceptedEvent(List<CollectableItem> collectableItems)
    {
        this.collectableItems = collectableItems;
    }
}

public readonly struct DialogFinishedEvent : IEvent
{
    public readonly string dialog_title;

    public DialogFinishedEvent(string dialog_title)
    {
        this.dialog_title = dialog_title;
    }
}

public readonly struct QuestAcceptedEvent : IEvent
{
    public readonly string quest_title;

    public QuestAcceptedEvent(string quest_title)
    {
        this.quest_title = quest_title;
    }
}

public readonly struct QuestCompletedEvent : IEvent
{
    public readonly string quest_title;

    public QuestCompletedEvent(string quest_title)
    {
        this.quest_title = quest_title;
    }
}
