using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class Upgrade : MonoBehaviour {

    public Text 햄;
    public Text 외;
    public Text 싸;
    public Text 홍;
    public Text 날;
    public Text 창;
    StringBuilder sb = new StringBuilder();
    StringBuilder sb2 = new StringBuilder();

    public static bool 햄스터On = false;
    public static float 햄스터Delay = 3.0f;
    public static int 싸게싸게 = 0;
    public static bool 피버On = false;
    public static bool 피버time = false;
    public static float 렛츠파뤼롹엔롤 = 5.0f;
    public static int 날씨퍼센트 = 50;
    public static int 창최몇 = 5;
    public int Costof햄스터 = 0, Costof외관 = 0, Costof싸게 = 0, Costof홍보 = 0, Costof날씨 = 0, Costof창고 = 0;
    //                       45              60              35              45              30              55
    //                      1.6               "               "               "             1.7             1.6
    // (int) Math.round();

    float times = 0.0f;

	// Use this for initialization
	void Start ()
    {
        setText(햄, Costof햄스터);
        setText(외, Costof외관);
        setText(싸, Costof싸게);
        setText(홍, Costof홍보);
        setText(날, Costof날씨);
        setText(창, Costof창고);
    }
	
	// Update is called once per frame
	void Update () {
        if (!피버time && times >= 렛츠파뤼롹엔롤) // 피버타임ON
        {
            FruitManager.instance.sellTimeMin = 0;
            FruitManager.instance.sellTimeMax = 1;
            피버time = true;
            Debug.Log(피버time);
            times = 0.0f;
        }
        else if (피버time && times >= 5.0f) // 피버타임OFF
        {
            FruitManager.instance.sellTimeMin = 1;
            FruitManager.instance.sellTimeMax = 5;
            피버time = false;
            Debug.Log(피버time);
            times = 0.0f;
        }

        if (피버On)
            times += Time.deltaTime;
    }

    void setText(Text text, int cost)
    {

        sb.Remove(0, sb.Length);
        sb2.Remove(0, sb2.Length);

        int _cost = cost;
        int count = 0;

        while (_cost > 0)
        {
            if (count != 0 && count % 3 == 0)
                sb.Append(',');

            sb.Append(_cost % 10);
            _cost /= 10;
            count++;
        }

        for (int i = sb.Length - 1; i >= 0; i--)
            sb2.Append(sb[i]);

        text.text = sb2.ToString();
    }

    public void MrAssistHam() // 보조 햄스터
    {
        if (GameController.totalMoney >= Costof햄스터)
        {
            Debug.Log("햄스터Go!");
            if (!햄스터On)
                햄스터On = true;
            else
                햄스터Delay -= 2.0f;
            GameController.instance.addMoney(-Costof햄스터);
            Costof햄스터 = (int)Mathf.Round(Costof햄스터 * 1.6f);
            setText(햄, Costof햄스터);
        }
    }

    public void DecoExterior() // 외관 꾸미기
    {
        if (GameController.totalMoney >= Costof외관)
        {
            Debug.Log("외관Up");
            Environment.instance.changeEnvironment();
            GameController.instance.addMoney(-Costof외관);
            Costof외관 = (int)Mathf.Round(Costof외관 * 1.6f);
            setText(외, Costof외관);
        }
    }

    public void BuyFruitsMoreCheaper() // 싸게 사기
    {
        if (GameController.totalMoney >= Costof싸게)
        {
            Debug.Log("싸게싸게");
            싸게싸게++;
            Costof싸게 = (int)Mathf.Round(Costof싸게 * 1.6f);
            setText(싸, Costof싸게);
        }
    }

    public void Promoting() // 홍보 하기
    {
        if (GameController.totalMoney >= Costof홍보)
        {
            Debug.Log("피버타임!");
            if (!피버On)
                피버On = true;
            else
                렛츠파뤼롹엔롤 -= 2.0f;
            Costof홍보 = (int)Mathf.Round(Costof홍보 * 1.6f);
            setText(홍, Costof홍보);
        }
    }

    public void WeatherAccuracy() // 날씨 정확도
    {
        if (GameController.totalMoney >= Costof날씨)
        {
            Debug.Log("날씨퍼센트Up");
            if (날씨퍼센트 < 80)
                날씨퍼센트 += 5;
            else
                날씨퍼센트 += 2;
            GameController.instance.addMoney(-Costof날씨);
            Costof날씨 = (int)Mathf.Round(Costof날씨 * 1.7f);
            setText(날, Costof날씨);
        }
    }

    public void BuildAdditionalStorage() // 창고 증설
    {
        if (GameController.totalMoney >= Costof날씨)
        {
            Debug.Log("창고Up");
            창최몇 += 5;
            GameController.instance.addMoney(-Costof창고);
            Costof창고 = (int)Mathf.Round(Costof창고 * 1.6f);
            setText(창, Costof창고);
        }
    }
}
