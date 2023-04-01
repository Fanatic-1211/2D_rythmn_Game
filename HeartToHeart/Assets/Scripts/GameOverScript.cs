using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject brokeHLeft;
    public GameObject brokeHRight;

    public AudioSource brokeHeartSounds;

    private void Update()
    {
        if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))
        {
            brokeHLeft.transform.localPosition = new Vector3(-125, 0, 0);
            brokeHRight.transform.localPosition = new Vector3(125, 0, 0);

            brokeHeartSounds.Play();
        }
        else if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow))
        {
            brokeHLeft.transform.localPosition = new Vector3(-125, -75, 0);
            brokeHRight.transform.localPosition = new Vector3(125, -75, 0);

            brokeHeartSounds.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (brokeHLeft.transform.localPosition.y > -10)
                SceneManager.LoadScene(1);
            else
                SceneManager.LoadScene(0);

        }
    }
}
