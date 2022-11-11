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

    public int currSite = 0;

    public void changeSite() {
        switch(currSite) {
            case(0):
                wireframe1.transform.localPosition = new Vector3(0, 0, -10);
                wireframe2.transform.localPosition = new Vector3(0, 0, 10);
                button.transform.localPosition = new Vector3(-50, -50, 0);
                break;
            default:
                break;
        }
    }


}
