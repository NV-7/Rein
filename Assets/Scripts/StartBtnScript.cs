using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartBtnScript : MonoBehaviour
{
    public Button startBtn;

    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(loadGameScene);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void loadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}