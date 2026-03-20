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
    public readonly int amount;

    public ItemCollectedEvent(int item_id, int amount)
    {
        this.item_id = item_id;
        this.amount = amount;
    }
}

public readonly struct ItemUsedEvent : IEvent
{
    public readonly int item_id;

    public ItemUsedEvent(int item_id)
    {
        this.item_id = item_id;
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

/*
public readonly struct ZoneEnteredEvent : IEvent
{
    public readonly string zoneId;
    public readonly int actorInstanceId;

    public ZoneEnteredEvent(string zoneId, int actorInstanceId)
    {
        this.zoneId = zoneId;
        this.actorInstanceId = actorInstanceId;
    }
}
*/
