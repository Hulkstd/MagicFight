using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ServerListUI : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Transform Content;

    public int Counts;
    
    private string[] Titles = new string[5];

    private void Start()
    {
        Titles[0] = "혼자 게임만들기 거참 힘드네";
        Titles[1] = "서버는또 왜 이지랄이야";
        Titles[2] = "아이고 시부럴";
        Titles[3] = "기분이 ㅈ같구만";
        Titles[4] = "때려처 ㅅㅂ";
    }
}
