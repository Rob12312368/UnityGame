using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIScript : MonoBehaviour {

    public Transform PlayerInfo;
    public Transform SpeedInfo;
    public Transform LaneInfo;
    public Transform ScoreInfo;
    public Transform SkillInfo;
    public Transform SkillGraphic;

	// Use this for initialization
	void Start () {
        SpeedInfo.GetComponent<UnityEngine.UI.Text>().text = "Speed: 0 m/s";
        LaneInfo.GetComponent<UnityEngine.UI.Text>().text = "0 m";
        ScoreInfo.GetComponent<UnityEngine.UI.Text>().text = "Score: 0";
        // No cooldown at the begining


    }
	
	// Update is called once per frame
	void Update () {

        // Speed Info
        float vfloat = cuber.playerSpeed;
        int vint = (int)vfloat;
        if (vfloat - vint > 0.5)
            vint += 1;

        SpeedInfo.GetComponent<UnityEngine.UI.Text>().text = "Speed: " + vint + "m/s";

        // Lane Info
        float runthrough = PlayerInfo.transform.localPosition.z;
        if(runthrough < 1000f)
            LaneInfo.GetComponent<UnityEngine.UI.Text>().text = runthrough.ToString("0.00") + "m";
        else
            LaneInfo.GetComponent<UnityEngine.UI.Text>().text = (runthrough/1000).ToString("0.00") + "km";

        // Score Info
        ScoreInfo.GetComponent<UnityEngine.UI.Text>().text = "Score: " + cuber.playerScore;

        // Skill Info
        SkillInfo.GetComponent<UnityEngine.UI.Text>().text = "Dive-Crash";
        if(cuber.diveSinceCoolDown < 2f)
        {
            SkillGraphic.GetComponent<UnityEngine.UI.Image>().fillAmount += cuber.diveSinceCoolDown / 2f * Time.deltaTime;
        }
        else
        {
            SkillGraphic.GetComponent<UnityEngine.UI.Image>().fillAmount = 1;
            SkillGraphic.GetComponent<UnityEngine.UI.Image>().color = Color.green;
        }
        if(cuber.diveReset == true)
        {
            cuber.diveReset = false;
            SkillGraphic.GetComponent<UnityEngine.UI.Image>().fillAmount = 0;
            SkillGraphic.GetComponent<UnityEngine.UI.Image>().color = Color.gray;
        }
    }
}
