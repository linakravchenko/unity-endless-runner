using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour {

    public int fillAmount = 100;
    public Image content;
    public Text healthText;

	void Update ()
    {
        HandleBar();
    }

    private void HandleBar()
    {
        content.fillAmount = (float) fillAmount/100;
    }
}
