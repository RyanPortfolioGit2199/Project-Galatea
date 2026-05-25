using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteMenu : MonoBehaviour
{
    [Header("Scene Numbers")]
    [SerializeField] int mainMenuScene;
    [SerializeField] int upgradesLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void ExitToMenu()
    {
        Debug.Log("Exit Back out to Main Menu");
        SceneManager.LoadScene(mainMenuScene);
        SaveManager.Instance.SaveCurrency(CurrencyManager.Instance.currency);
    }

    public void ContinueToUpgradeMenu()
    {
        Debug.Log("To the Upgrade Menu");
        SceneManager.LoadScene(upgradesLevel);
        SaveManager.Instance.SaveCurrency(CurrencyManager.Instance.currency);
        this.gameObject.SetActive(false);
    }
}
