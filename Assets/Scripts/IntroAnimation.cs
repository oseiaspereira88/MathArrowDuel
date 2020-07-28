using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroAnimation : MonoBehaviour
{
    public Text txtUI;
    public string[] presentations = new string[] {
        "Lop no Loop",
        "2020.1 que nao passa",
        "Apresenta..."};

    public float increaseSizeTime = 0.25f;
    public int introSize = 80;
    public int maxSize = 120;
    public float interval = 1;
    private float currentInterval = 0;
    private bool isInterval = false;
    private float time = 0;
    private int count = 0;

    private void Start()
    {
        txtUI.fontSize = introSize;
        time = increaseSizeTime;
    }

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0 && !isInterval) {
            txtUI.fontSize++;
            time = increaseSizeTime;
        } else if (isInterval)
        {
            currentInterval += Time.deltaTime;
            if (currentInterval >= interval)
            {
                isInterval = false;
                time = 0;
                txtUI.enabled = true;
            }
        }

        if (txtUI.fontSize >= maxSize && count< presentations.Length) {
            isInterval = true;
            txtUI.fontSize = introSize;
            txtUI.enabled = false;
            count++;

            if (count!= presentations.Length) {
                txtUI.text = presentations[count];
            }
            else if(!isInterval)
            {
                isInterval = true;
            }
            else { SceneManager.LoadScene(1); }
        }
    }
}
