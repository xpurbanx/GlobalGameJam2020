using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public Nail nail;
    public bool toStartPlay;

    private void Start()
    {
        nail = GetComponent<Nail>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nail.isFixed == true)
        {
            if (toStartPlay == true)
                SceneManager.LoadScene("VR SCENE2");
            else
                Application.Quit();
        }
    }

}
