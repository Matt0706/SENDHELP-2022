using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebsiteManager : MonoBehaviour
{

    public GameObject wireframe1;
    public GameObject wireframe2;
    public GameObject wireframe3;
    public GameObject wireframe4;
    public GameObject wireframe5;
    public GameObject wireframe6;
    public Button button;

    public int currSite = 1;

    public void changeSite() {
        switch(currSite) {
            case(1):
                wireframe1.transform.localPosition = new Vector3(170.9f, 314.5f, 63f); //Into the background
                wireframe2.transform.localPosition = new Vector3(170.9f, 314.5f, 53f); //Into the foreground
                button.transform.localPosition = new Vector3(-50, -50, 0);
                break;
            case(2):
                wireframe2.transform.localPosition = new Vector3(170.9f, 314.5f, 63f); //Into the background
                wireframe3.transform.localPosition = new Vector3(170.9f, 314.5f, 53f); //Into the foreground
                button.transform.localPosition = new Vector3(-50, -50, 0);
                break;
            case(3):
                wireframe3.transform.localPosition = new Vector3(170.9f, 314.5f, 63f); //Into the background
                wireframe4.transform.localPosition = new Vector3(170.9f, 314.5f, 53f); //Into the foreground
                button.transform.localPosition = new Vector3(-50, -50, 0);
                break;
            case(4):
                wireframe4.transform.localPosition = new Vector3(170.9f, 314.5f, 63f); //Into the background
                wireframe5.transform.localPosition = new Vector3(170.9f, 314.5f, 53f); //Into the foreground
                button.transform.localPosition = new Vector3(-50, -50, 0);
                break;
            case(5):
                wireframe5.transform.localPosition = new Vector3(170.9f, 314.5f, 63f); //Into the background
                wireframe6.transform.localPosition = new Vector3(170.9f, 314.5f, 53f); //Into the foreground
                button.transform.localPosition = new Vector3(-50, -50, 0);
                break;
            case(6):
                endGame();
                break;
            default:
                break;
        }
        currSite++;
    }


    void endGame() {
        Debug.Log("Game over");
    }
}
