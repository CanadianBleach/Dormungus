using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TaskManager : MonoBehaviour
{
    public static TaskManager taskManager;
    public Task[] allTasks;
    private List<Task> pickableTasks;
    IDictionary<Task, GameObject> textDict = new Dictionary<Task, GameObject>();
    public GameObject taskTextPrefab;
    public GameObject taskTextParent;

    private void Start()
    {
        taskManager = this;
        pickableTasks = new List<Task>(allTasks);
    }
    
    public Task GetRandomTask()
    {
        int num = Random.Range(0, pickableTasks.Count);
        Task taskReturn = pickableTasks[num];
        pickableTasks.Remove(taskReturn);
        return taskReturn;
    }

    public void AddTaskText(Task task)
    {
        GameObject go = Instantiate(taskTextPrefab);
        textDict.Add(task, go);
        go.GetComponent<TextMeshProUGUI>().text = task.taskName;
        go.transform.SetParent(taskTextParent.transform);
    }

    public void RemoveTask(Task task)
    {
        Destroy(textDict[task]);
        textDict.Remove(task);
    }  
}
