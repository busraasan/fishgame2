using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    public TextMeshProUGUI countdownDisplay;

    public IEnumerator CountdownToStart()
    {
        countdownDisplay.gameObject.SetActive(true);
        countdownTime = 3;

        while(countdownTime > 0)
        {
            countdownDisplay.SetText(countdownTime.ToString());

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownDisplay.text = "GO!";

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
    }

}
