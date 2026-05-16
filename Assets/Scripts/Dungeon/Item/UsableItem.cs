using UnityEngine;

public class UsableItem : ConsumableItem
{
    public UseEffect useEffect;

    UsableItemSO data;

    public UsableItem(UsableItemSO data, int id) : base(data, id)
    {
        this.data = data;

        this.useEffect = new UseEffect(data.useType, data.use_percent_from_0_to_100, data.time_duration_seconds, data.time_for_close);
    }
}

public class UseEffect
{
    public UseType useType;
    public int use_percent_from_0_to_100;
    public int time_duration_seconds;
    public int time_for_close;

    public UseEffect(UseType useType, int use_percent_from_0_to_100, int time_duration_seconds = 0, int time_for_close = 0)
    {
        this.useType = useType;
        this.use_percent_from_0_to_100 = use_percent_from_0_to_100;
        this.time_duration_seconds = time_duration_seconds;
        this.time_for_close = time_for_close;
    }
}

public enum UseType
{
    None = 0,
    Health = 1,
    Attack = 2,
    CritChance = 3,
    CritDMG = 4,
    ElementalMastery = 5,
    Luck = 6,
}
