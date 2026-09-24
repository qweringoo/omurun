using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S2_GameManager : MonoBehaviour
{
    public static S2_GameManager inst;
    public GameObject Folk;
    public GameObject Gohaikei;
    public GameObject Retrybutton;
    public float Rndmax;
    public float Rndmin;
    public float Counttime = 0;
    public float Counttimeold=0;
    public float Folkspeed;
    public int Member;
    public bool Okgofolk;
    public bool Gameover;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        inst = this;
        Gameover = false;
        Okgofolk = true;
        StartCoroutine("Gofolk");
    }

    // Update is called once per frame
    void Update()
    {
        Counttime += Time.deltaTime;

        if ((int)(Counttime - Counttimeold) == 10)
        {
            StartCoroutine("Stopfolk");
            Counttimeold = Counttime;
            Folkspeed += 0.03f;
            if (Rndmax > 0.5)
            {
                Rndmax -= 0.3f;
            }
        }
    }

    IEnumerator Gofolk()
    {
        while (!Gameover)
        {
            if (Okgofolk)
            {
                if (Random.Range(0, 3) == 0)
                {
                    Instantiate(Folk, new Vector3(15f, -2.414f, 0f), Quaternion.identity);
                    Instantiate(Folk, new Vector3(16f, -2.414f, 0f), Quaternion.identity);
                    yield return new WaitForSeconds(Random.Range(Rndmin, Rndmax));
                }
                else
                {
                    Instantiate(Folk, new Vector3(15f, -2.414f, 0f), Quaternion.identity);
                    yield return new WaitForSeconds(Random.Range(Rndmin, Rndmax));
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator Stopfolk()
    {
        Okgofolk = false;
        yield return new WaitForSeconds(0.7f);
        Okgofolk = true;
    }

    public void Startgameoverhaikei()
    {
        StartCoroutine("Gohs");
    }

    IEnumerator Gohs()
    {
        while (Gohaikei.GetComponent<Image>().color.a < 1f)
        {
            Gohaikei.GetComponent<Image>().color += new Color(0f, 0f, 0f, 0.01f);
            yield return null;
        }

        Retrybutton.SetActive(true);
        while (Retrybutton.GetComponent<Image>().color.a < 1f)
        {
            Retrybutton.GetComponent<Image>().color += new Color(0f, 0f, 0f, 0.1f);
            yield return null;
        }
        Retrybutton.GetComponent<Button>().enabled = true;
    }

    public void Retrystart()
    {
        SceneManager.LoadScene("2");
    }
}
