using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskScreen : MonoBehaviour
{
    public TextMeshProUGUI taskName;

    public void SetTaskInfo(Task task)
    {
        taskName.text = task.taskName;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
}
