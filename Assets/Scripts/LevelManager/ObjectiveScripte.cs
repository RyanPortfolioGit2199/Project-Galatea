using UnityEngine;

public abstract class ObjectiveScripte : MonoBehaviour
{
    private bool isFinished = false;


    protected void FinishObjectiveStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            Debug.Log("1st Level Completed");
            // TODO - End the Level and give the option to go to upgrade menu or next level
            GameManager.Instance.TriggerLevelCompleted();

            Destroy(this.gameObject);
        }
    }
}
