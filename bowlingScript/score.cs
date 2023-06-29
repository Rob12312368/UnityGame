using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour {
    public int display_score=0; // score at current round
    int game_num=1; // number of round
    public int total_score = 0; // total score
    public Text text;
    public Text strike;
    public Text gamenumber;
    public Text total;

    private void Start()
    {
        gamenumber.text = "GAME" + game_num.ToString();
        display_score = 0;
        score_update();
    }
    public void change_game_num()
    {
        game_num += 1;
    }
    public void gamenumber_update()
    {
        gamenumber.text = "GAME" + game_num;
    }
    public void add(int amount)
    {
        display_score += amount;
        score_update();

    }
    public void total_update()
    {
        total_score += display_score;
        total.text = "Total Score " + total_score;
    }
    public void set(int amount)
    {
        display_score = amount;
        score_update();
    }
    public void score_update()
    {
        text.text = "Score" + display_score;

        if (display_score == 10)
        {
            strike.text = "STRIKE!";
            
        }
        
    }
 
}
