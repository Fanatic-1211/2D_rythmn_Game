using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class titleEvents : MonoBehaviour
{
    // title object
    public GameObject title;

    // text and selector
    public GameObject text;
    public GameObject selector;

    // fader UI
    public Image fader;

    // selector value
    int currentSelection = 0;
    // is the selector usable?
    bool selecting = true;

    // title music
    public AudioSource music;
    static float bpmDiff = 0.6f;

    // sound source
    public AudioSource soundSource;

    // sound effects
    public AudioClip moveSound;
    public AudioClip selectSound;

    // bool to check if the selection screen is active
    bool awake = false;

    // index for the current screen displayed
    // 0 = TITLE
    // 1 = HOW TO PLAY
    public GameObject titleScreen;
    public GameObject how2playScreen;

    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
        selector.SetActive(false);

        // start pulser
        Invoke("pulseControl", bpmDiff);
    }

    // Update is called once per frame
    void Update()
    {
        // if any key is pressed, enable title screen
        if ((!awake) && Input.anyKey)
        {
            awake = true;

            // enable selectors
            text.SetActive(true);
            selector.SetActive(true);

            // play sound
            soundSource.PlayOneShot(moveSound);

            // call coroutine
            StartCoroutine(moveTitle(260f));
        }

        // once enabled, we can start looking at stuff
        else if (awake)
        {
            // move selector if not diabled
            if (selecting)
            {
                if ((Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)) && (currentSelection > 0))
                    selectorControl(-1);
                else if ((Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow)) && (currentSelection < 3))
                    selectorControl(1);
            }

            // if enter is pressed, change scenes
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // play selection sound
                soundSource.PlayOneShot(selectSound);

                // if on how2play, change back to title
                if (!selecting)
                {
                    screenControl(0);
                    return;
                }

                switch (currentSelection)
                {
                    case 0:                                 // VN start
                        StartCoroutine(selectScene(1, 1f));
                        break;
                    case 1:                                 // tutorial screen is not a diff scene but on top of the title screen
                        screenControl(1);
                        break;
                    case 2:                                 // quick play (game screen)
                        StartCoroutine(selectScene(2, 1f));
                        break;
                    case 3:                                 // quit game
                        StartCoroutine(selectScene(-1, 1f));
                        break;
                }
            }
        }
    }

    void pulseControl()
    {
        // pulse on ever beat (105bpm)
        Invoke("pulseControl", bpmDiff);

        StartCoroutine(pulseTitle(1.05f));
    }

    void selectorControl(int moveDir)
    {
        currentSelection += moveDir;

        selector.GetComponent<RectTransform>().localPosition = new Vector3(
                    selector.GetComponent<RectTransform>().localPosition.x,
                    90f - (95f * currentSelection),
                    0);

        // make sound
        soundSource.PlayOneShot(moveSound);
    }

    void screenControl(int newScreen)
    {
        // diable all screens
        titleScreen.SetActive(false);
        how2playScreen.SetActive(false);

        // change the screen
        switch (newScreen)
        {
            default:
            case 0:
                // enable title
                titleScreen.SetActive(true);
                selecting = true;
                break;
            case 1:
                // enable tutorial
                how2playScreen.SetActive(true);
                selecting = false;
                break;
        }
    }

    IEnumerator selectScene(int scene, float duration)
    {
        // fade scene & music
        float time = 0;
        
        while (time < duration)
        {
            fader.color = new Color(0, 0, 0, (time / duration));
            music.volume = Mathf.Lerp(0.5f, 0, (time / duration));

            time += Time.deltaTime;

            yield return null;
        }

        if (scene != -1)
            // load different scene
            SceneManager.LoadScene(scene);
        else if (scene == -1)
        {
            /*  SCENES
             *  0 = TITLE SCREEN
             *  1 = QUICK PLAY
             *  2 = VISUAL NOVEL START
             *  3 = TUTORIAL
             */
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;    // quit for editor
        }
    }
    IEnumerator moveTitle(float endPos)
    {
        while(Mathf.Abs(title.GetComponent<RectTransform>().localPosition.x - endPos) > 0.01f)
        {
            // start to decrease position
            title.GetComponent<RectTransform>().localPosition = new Vector3(
                Mathf.Lerp(title.GetComponent<RectTransform>().localPosition.x, endPos, Time.deltaTime * 6f),
                0,
                1);

            yield return null;
        }

        title.GetComponent<RectTransform>().localPosition = new Vector3(endPos, 0, 0);

        yield return null;
    }
    IEnumerator pulseTitle(float strength)
    {
        // get current scale
        float currScale = title.GetComponent<RectTransform>().localScale.x;

        // set pulse
        title.GetComponent<RectTransform>().localScale = new Vector3(strength, strength, 1);

        yield return null;

        while (title.GetComponent<RectTransform>().localScale.x - currScale > 0.001f)
        {
            // start to decrease scale
            title.GetComponent<RectTransform>().localScale = new Vector3(
                Mathf.Lerp(title.GetComponent<RectTransform>().localScale.x, currScale, Time.deltaTime * 6f),
                Mathf.Lerp(title.GetComponent<RectTransform>().localScale.y, currScale, Time.deltaTime * 6f),
                1);

            yield return null;
        }

        title.GetComponent<RectTransform>().localScale = new Vector3(currScale, currScale, 1f);
    }
}
