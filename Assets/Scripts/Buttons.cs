
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public Material[] materials;
    public Renderer Rend;

    private int index = 1;

    public void changeColor()
    {
        if (materials == null || materials.Length == 0)
            return;

        index++;
        if (index > materials.Length)
            index = 1;

        Debug.Log(index);
        Rend.sharedMaterial = materials[index - 1];
    }

    public void ExitButton()
    {
        Application.Quit();

        Debug.Log("App Close");
    }

    public void MainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
