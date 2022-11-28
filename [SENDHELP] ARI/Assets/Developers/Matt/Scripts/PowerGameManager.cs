using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class PowerGameManager : MonoBehaviour
{

    public string SceneToLoad;
    public Button fuse1;
    public Button fuse2;
    public Button fuse3;
    public Button fuse4;
    public Button fuse5;

    private ColorBlock red = new ColorBlock();
    private ColorBlock green = new ColorBlock();

    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        initColors();
        Random rnd = new Random();
        if(rnd.Next(2) == 0)
            fuse1.colors = red;
        else fuse1.colors = green;
        if(rnd.Next(2) == 0)
            fuse2.colors = red;
        else fuse2.colors = green;
        if(rnd.Next(2) == 0)
            fuse3.colors = red;
        else fuse3.colors = green;
        if(rnd.Next(2) == 0)
            fuse4.colors = red;
        else fuse4.colors = green;
        if(rnd.Next(2) == 0)
            fuse5.colors = red;
        else fuse5.colors = green;
    }

    // Update is called once per frame
    void Update()
    {
        if(fuse1.colors == green &&
            fuse2.colors == green &&
            fuse3.colors == green &&
            fuse4.colors == green &&
            fuse5.colors == green) {
                fuse1.enabled = false;
                fuse2.enabled = false;
                fuse3.enabled = false;
                fuse4.enabled = false;
                fuse5.enabled = false;
                Invoke("changeScene", 1f);
            }
    }

    public void fuse1Clicked() {
        if(fuse1.colors == red)
            fuse1.colors = green;
        else fuse1.colors = red;
    }
    public void fuse2Clicked() {
        if(fuse2.colors == red)
            fuse2.colors = green;
        else fuse2.colors = red;
    }
    public void fuse3Clicked() {
        if(fuse3.colors == red)
            fuse3.colors = green;
        else fuse3.colors = red;
    }
    public void fuse4Clicked() {
        if(fuse4.colors == red)
            fuse4.colors = green;
        else fuse4.colors = red;
    }
    public void fuse5Clicked() {
        if(fuse5.colors == red)
            fuse5.colors = green;
        else fuse5.colors = red;
    }

    void changeScene() {
        SceneManager.LoadScene(SceneToLoad);
    }

    void initColors() {
        red.normalColor = new Color(255, 0, 0, 200);
        red.pressedColor = new Color(255, 0, 0, 200);
        red.selectedColor = new Color(255, 0, 0, 200);
        red.disabledColor = new Color(255, 0, 0, 200);
        red.highlightedColor = new Color(255, 0, 0, 200);
        red.colorMultiplier = 1;
        green.normalColor = new Color(0, 255, 0, 200);
        green.pressedColor = new Color(0, 255, 0, 200);
        green.selectedColor = new Color(0, 255, 0, 200);
        green.disabledColor = new Color(0, 255, 0, 200);
        green.highlightedColor = new Color(0, 255, 0, 200);
        green.colorMultiplier = 1;
    }
}
