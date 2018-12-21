using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ServerListBox : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Transform Content;

    [SerializeField]
    public static int Counts;

    private string[] Titles = new string[5];

    private void Start()
    {
        Titles[0] = "혼자 게임만들기 거참 힘드네";
        Titles[1] = "서버는또 왜 이지랄이야";
        Titles[2] = "아이고 시부럴";
        Titles[3] = "기분이 ㅈ같구만";
        Titles[4] = "때려처 ㅅㅂ";

        InvokeRepeating("Reload", 0, 5.0f);
    }

    void Reload()
    {
        for(int i=0; i<Content.childCount; i++)
        {
            Destroy(Content.GetChild(0));
        }

        for(int i=0; i<Counts; i++)
        {
            GameObject g = new GameObject("GR" + i);
            g.transform.parent = Content;

            Text text = g.AddComponent<Text>();

            text.text = Titles[Random.Range(0, 4)];
        }
    }
}
