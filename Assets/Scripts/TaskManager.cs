using UnityEngine;
using UnityEngine.Events;

public class TaskManager : MonoBehaviour
{
    //task 1 variables
    bool isTaskOnePrereqDone = false;
    bool isTaskOneDone = false;
    
    //task 2 variables
    bool isTaskTwoDone = false;
    
    //task 3 variables
    bool isTaskThreeDone = false;
    
    ActDirctor actDirctor;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actDirctor = GetComponent<ActDirctor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTaskOneDone && isTaskTwoDone && isTaskThreeDone)
        {
            actDirctor.AllTasksComplete();
        }
    }

    void resetFlags()
    {
        isTaskOneDone = false;
        isTaskTwoDone = false;
        isTaskThreeDone = false;
    }

    public void TaskOneDone()
    {
        isTaskOneDone = true;
    }
    
    public void TaskTwoDone()
    {
        isTaskTwoDone = true;
    }

    public void TaskThreeDone()
    {
        isTaskThreeDone = true;
    }
}
