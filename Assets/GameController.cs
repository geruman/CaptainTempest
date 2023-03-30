using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    public void WaitAndReload()
    {
        StartCoroutine(ReloadIn1Sec());
    }
    private IEnumerator ReloadIn1Sec()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
