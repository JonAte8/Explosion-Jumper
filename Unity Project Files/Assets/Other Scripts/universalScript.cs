using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class universalScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    public static int livesLeft;
    public static Color bgColor;
    public Image winBackground;
    public IEnumerator LoadLevel(int index)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(index);
    }

    public void loadLevelFunction(int ind)
    {
        StartCoroutine(LoadLevel(ind));
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "TitleScreen")
        {
            livesLeft = 3;
        }
        else if(SceneManager.GetActiveScene().name == "Death")
        {
            if(livesLeft > 1)
            {
                if (text)
                {
                    text.text = "Lives: " + livesLeft;
                }
                StartCoroutine(LoseLife());
            }
            else
            {
                if (text)
                {
                    text.text = "Game Over!";
                }
                StartCoroutine(LoadLevel(0));
            }
        }
        else if(SceneManager.GetActiveScene().name == "Win")
        {
            if (winBackground)
            {
                winBackground.color = bgColor;
            }
            loadLevelFunction(0);
        }
    }

    IEnumerator LoseLife()
    {
        yield return new WaitForSeconds(1f);
        livesLeft--;
        if (text)
        {
            text.text = "Lives: " + livesLeft;
        }
        StartCoroutine(LoadLevel(2));

    }

    public void setColor(Color col)
    {
        bgColor = col;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
