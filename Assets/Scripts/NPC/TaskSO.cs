using System.Collections.Generic;
using UnityEngine;
using static QuestsController;

public class TaskSO : ScriptableObject
{
    public string subtitle;
    public string description;

    public string finish_function_name;

    public List<Reward> rewards;

    public TaskSO next_task;
}
