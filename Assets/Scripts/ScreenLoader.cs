using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenLoader : MonoBehaviour
{
    [SerializeField] float delaySeconds = 3.5f;
    int currentSceneIndex;

    private void Awake()
    {
        //SetupSingleton();
    }

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0) // se sono nello splash screen
        {
            StartCoroutine(WaitAndLoadNextScene());
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadMainScene(bool isContinueGame)
    {
        StartCoroutine(HandleLoadMainScene(isContinueGame));
    }

    public void LoadMinigame()
    {
        SceneManager.LoadScene("Minigame Scene");
    }

    private IEnumerator WaitAndLoadNextScene()
    {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadYouLose()
    {
        StartCoroutine(YouLose());
    }

    private IEnumerator YouLose()
    {
        //yield return new WaitForSeconds(delaySeconds);
        yield return null;
        //FindObjectOfType<Rotator>().enabled = false;
        SceneManager.LoadScene("Game Over Screen");
    }

    public void LoadYouWin()
    {
        //FindObjectOfType<Rotator>().enabled = false;
        SceneManager.LoadScene("Game Over Screen");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ReturnToMain()
    {
        StartCoroutine(HandleReturnToMain());
    }

    private IEnumerator HandleReturnToMain()
    {
        yield return StartCoroutine(WaitForSceneLoading("Main Scene"));
        //FindObjectOfType<GameManager>().RequestNewEncounter();
    }

    private IEnumerator HandleLoadMainScene(bool isContinueGame)
    {
        Debug.Log("entrato in HandleLoadMainScene");
        yield return StartCoroutine(WaitForSceneLoading("Main Scene"));
        if(isContinueGame)
        {
            //FindObjectOfType<DBManager>()?.LoadPreviousGame();
        }
        else
        {
            Debug.Log("Start New Game");
        //    FindObjectOfType<DBManager>()?.StartNewGame();
        }
    }

    private IEnumerator WaitForSceneLoading(string whichSceneToLoad)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(whichSceneToLoad);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}


