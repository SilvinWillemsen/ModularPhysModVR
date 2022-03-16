 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.SceneManagement;
 
public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void OpenSandbox()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Open sandbox");
    }

    public void CloseSandbox()
    {
        // SceneManager.LoadScene(sceneName:"InstrumentScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Debug.Log("Close sandbox");

    }
}
