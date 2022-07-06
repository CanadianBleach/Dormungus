using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using static TaskManager;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Task Controls")]
    public int taskCount;
	public float useDistance;
	public List<Task> playerTasks;
	public LayerMask taskMask;
    public KeyCode useKey;

	[Header("Movement")]
    Rigidbody2D body;
    float horizontal;
    float vertical;

    SpriteRenderer spriteRenderer;

    public static PlayerController playerController;

    private Task lastHighlightedTask;

    public float runSpeed = 20.0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerTasks = GetNewTasks();
        playerController = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleTaskInput();
    }

    private void HandleTaskInput()
    {
        //Handles input for opening tasks and highlights of nearby tasks

        Task closestTask = GetClosestTask();

        if (closestTask != null)
        {
            if (closestTask.isDone)
             return;

            // If the closest task changes (Highlight Logic)
            if (closestTask != lastHighlightedTask)
            {
                if (lastHighlightedTask == null)
                {
                    lastHighlightedTask = closestTask;
                    lastHighlightedTask.Highlight();
                }
                else
                {
                    lastHighlightedTask.RemoveHighlight();
                    lastHighlightedTask = closestTask;
                    lastHighlightedTask.Highlight();
                }
            }

            // Input Logic
            if (Input.GetKeyDown(useKey))
            {
                Debug.Log("Key Pressed");
                ((ITask)closestTask).ToggleTask();
            }
        }
        else
        {
            // If there is no close tasks remove highlights
            if (lastHighlightedTask != null)
            {
                lastHighlightedTask.RemoveHighlight();
                lastHighlightedTask = null;
            }
        }
    }

    private void FixedUpdate()
    {

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    private Task GetClosestTask()
    {
        // May need to sort tasks by distance
        List<Task> tasks = new List<Task>();
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, useDistance, taskMask);

        // Returns first task the player has
        if (cols.Length > 0)
        {
            //Debug.Log("SHIT FOUND");
            foreach (Collider2D col in cols)
                if (playerTasks.Contains(col.GetComponent<Task>()))
                    return (col.GetComponent<Task>());
        }

        return null;
    }

    private List<Task> GetNewTasks()
    {
        List<Task> returnList = new List<Task>();

        for (int i = 0; i < taskCount; i++)
        {

            Task t = taskManager.GetRandomTask();
            taskManager.AddTaskText(t);
            returnList.Add(t);
        }

        return returnList;
    }
	void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, useDistance);
    }
}
