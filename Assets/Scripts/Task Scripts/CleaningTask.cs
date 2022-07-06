using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CleaningTask : Task, ITask
{
    public float cleanSpeed = .5f;
    public GameObject sponge;
    public Image dirt;

    private Vector3 lastSpongePos;
    float dirtOp;
    
    private void Start() {
        dirt.color = new Color(dirt.color.r, dirt.color.g, dirt.color.b, 255);
        dirtOp = 1;
    }

    private void Update()
    {
        if (Mathf.Approximately(Mathf.Clamp01(dirtOp), 0) && !isDone)
            FinishTask();

        if (isOpen && !isDone)
        {
            if (sponge.transform.position != lastSpongePos)
            {
                dirtOp -= cleanSpeed * Time.deltaTime;
                lastSpongePos = sponge.transform.position;
            }
        }

        Color tempColor = dirt.color;
        tempColor.a = Mathf.Clamp01(dirtOp);
        dirt.color = tempColor;
    }

    public void ToggleTask()
    {
        if (isOpen)
        {
            CloseTask();
        }
        else
        {
            OpenTask();
        }
    }

    public void TestButtonMethod()
    {
        isDone = true;
        CloseTask();
        print("Play finish animation");
    }
}
