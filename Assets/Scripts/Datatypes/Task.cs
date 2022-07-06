using UnityEngine;
using static TaskManager;
using static PlayerController;

public class Task : MonoBehaviour
{
    public string taskName;
    public int parts = 1;
    public TaskType taskType = TaskType.Short;
    public GameObject taskCanvas;

    private bool isHighlighted = false;
    public bool isOpen = false;
    public bool isDone = false;

    private void Start()
    {
        isHighlighted = false;
        taskCanvas.SetActive(false);
    }
    public void Highlight()
    {
        if(isHighlighted)
            return;
        
        isHighlighted = true;
        print($"Highlighted {taskName}");
    }
    public void RemoveHighlight()
    {
        if (!isHighlighted)
            return;

        isHighlighted = false;
        print($"Removed Highlight from {taskName}");
    }
    
    public void OpenTask()
    {
        isOpen = true;
        taskCanvas.SetActive(true);
        print("Task opened");
    }

    public void CloseTask()
    {
        isOpen = false;
        taskCanvas.SetActive(false);
        RemoveHighlight();
        print("Task closed");
    }

    public void FinishTask()
    {
        isDone = true;
        RemoveHighlight();
        taskManager.RemoveTask(this);
        CloseTask();
        playerController.playerTasks.Remove(this);
    }
}

public enum TaskType
{
    Long,
    Medium,
    Short
}
