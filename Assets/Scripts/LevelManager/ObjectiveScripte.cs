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


            // Replace this later with disable due to issue with retrying a level and the objective script being destroyed(Might not be an issue when spawning the objective at the start of the level from the folder)
            Destroy(this.gameObject);
        }
    }
}
