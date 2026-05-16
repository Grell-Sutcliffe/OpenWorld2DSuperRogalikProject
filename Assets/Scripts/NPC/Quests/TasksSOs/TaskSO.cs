using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "Quest/Task")]
public class TaskSO : ScriptableObject
{
    public string subtitle;
    public string description;

    public string finish_function_name;

    //public List<RewardSO> rewardSOs = new List<RewardSO>();

    public TaskSO next_taskSO;
}

/*
?? Quest ??
?? string title
?? Task current_task
?? List<Reward> rewards

?? Task ??
?? string subtitle
?? string description
?? string finish_function_name (вызывается по факту завершения задания)
     ?? точное название функции из QuestsController
     ?? должна называться по типу Task_QuestTitle_TaskSubtitle
?? List<Reward> rewards
?? Task next_task

??????????????????????????????

?? DialogTask : Task ??   ---   тут лучше CheckConditionTask
?? Dialog dialog
     ?? тут скорее DialogSO dialogSO

?? EnemyKillTask : Task ??
?? List<GameObject> enemy_GOs (или List<Enemy> enemies)
     ?? скорее тут лучше string enemy_GO_name - точное название родительского объекта врагов со сцены

?? ItemCollectTask : Task ??
?? List<Reward> collectacle_items

?? CheckConditionTask : Task ??
?? string function_name (функция будет возвращать true или false в зависимости от того, выполнено ли условие)

?? LocationFindTask : Task ??
?? Gameobject location_point_GO (или коллайдер)
     ?? скорее, тут будет string GO_name - точное имя объекта со сцены

?? ItemUseTask : Task ??
?? Item item (скорее здесь лучше CheckConditionTask)

- - - - - - - - - - - - - - - - - - - - - - -

В ИТОГЕ

CheckConditionTask  --  проверка функции из QuestsCotroller на true или false

EnemyKillTask  --  проверка всех врагов в родительском объекте на существование

*/
