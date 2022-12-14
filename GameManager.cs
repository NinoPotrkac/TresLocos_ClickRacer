using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("values")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private float countdown = 3;
    [SerializeField]
    private int lvl1CarCountdown;
    [SerializeField]
    private int lvl2CarCountdown; 
    [SerializeField]
    private int lvl3CarCountdown;

    [Header("Prefabs")]
    public GameObject car1Prefab;
    public GameObject car2Prefab;
    public GameObject car3Prefab;
    
    [Header("Texts")]
    public TextMeshProUGUI speedText;

    [Header("Camera")]
    public Camera mainCamera;

    [Header("Sliders")]
    public Slider rSlider;
    public Slider gSlider;
    public Slider bSlider;

    [Header("Car Counter")]
    [SerializeField]
    private int lvl1Car = 1;
    [SerializeField]
    private int lvl2Car;
    [SerializeField]
    private int lvl3Car;

    [Header("Buttons")]
    public Button car1Button;
    public Button car2Button;
    public Button car3Button;
    public Button extraCheckpointButton;
    public Button moreMoneyButton;

    [Header("Button Values")]
    public float car1Value = 20;
    public float car2Value = 150;
    public float car3Value = 300;
    public float extraCheckpointValue = 20000;
    public float moreMoneyValue = 10000;

    public TextMeshProUGUI car1ValueText;
    public TextMeshProUGUI car2ValueText;
    public TextMeshProUGUI car3ValueText;
    public TextMeshProUGUI extraCheckpointValueText;
    public TextMeshProUGUI moreMoneyValueText;

    [Header("Script")]
    public MoneyMaker moneyScript;

    [Header("Car Destroyers")]
    public GameObject greenCarDestroyer;
    public GameObject yellowCarDestroyer;

    [Header("On Startup")]
    public int firstStart = 0;

    private void Awake()
    {
        firstStart = PlayerPrefs.GetInt("startup1");
        if (firstStart == 0)
        {
            PlayerPrefs.SetFloat("car1Value", car1Value = 20);
            PlayerPrefs.SetFloat("car2Value", car2Value = 150);
            PlayerPrefs.SetFloat("car3Value", car3Value = 300);
            PlayerPrefs.SetFloat("mmv", moreMoneyValue = 20000);
            PlayerPrefs.SetInt("lvl1Car", lvl1Car = 1);
            PlayerPrefs.SetFloat("rSlider", rSlider.value = 41);
            PlayerPrefs.SetFloat("gSlider", gSlider.value = 209);
            PlayerPrefs.SetFloat("bSlider", bSlider.value = 214);
        }
    }

    void Start()
    {
        extraCheckpointValue = 20000;
        lvl1CarCountdown = PlayerPrefs.GetInt("lvl1Car");
        lvl2CarCountdown = PlayerPrefs.GetInt("lvl2Car");
        lvl3CarCountdown = PlayerPrefs.GetInt("lvl3Car");
        car1Value = PlayerPrefs.GetFloat("car1Value");
        car2Value = PlayerPrefs.GetFloat("car2Value");
        car3Value = PlayerPrefs.GetFloat("car3Value");
        moreMoneyValue = PlayerPrefs.GetFloat("mmv");
        InvokeRepeating("SpawnLvL1Car", 1, 1); 
        
        lvl1Car = PlayerPrefs.GetInt("lvl1Car");
        lvl2Car = PlayerPrefs.GetInt("lvl2Car");
        lvl3Car = PlayerPrefs.GetInt("lvl3Car");
       
        InvokeRepeating("CountdownDown", 1, 1);
        
        rSlider.value = PlayerPrefs.GetFloat("rSlider");
        gSlider.value = PlayerPrefs.GetFloat("gSlider");
        bSlider.value = PlayerPrefs.GetFloat("bSlider");


        if (lvl2Car > 0)
        {
            InvokeRepeating("SpawnLvL2Car", 0, 1);
        }
        
        if(lvl3Car > 0)
        {
            InvokeRepeating("SpawnLvL3Car", 0, 1);
        }
        
    }

    void Update()
    {

        car1Value = Mathf.RoundToInt(car1Value);
        car2Value = Mathf.RoundToInt(car2Value);
        car3Value = Mathf.RoundToInt(car3Value);

        car1ValueText.text = car1Value.ToString();
        car2ValueText.text = car2Value.ToString();
        car3ValueText.text = car3Value.ToString();
        moreMoneyValueText.text = moreMoneyValue.ToString();
        extraCheckpointValueText.text = extraCheckpointValue.ToString();

        if(moneyScript.money >= car1Value)
        {
            car1Button.interactable = true;
        }
        else
        {
            car1Button.interactable = false;
        }

        if (moneyScript.money >= car2Value && lvl1Car >= 3)
        {
            car2Button.interactable = true;
        }

        else
        {
            car2Button.interactable = false;
        }

        if(moneyScript.money >= car3Value && lvl2Car >= 3)
        {
            car3Button.interactable = true;
        }

        else
        {
            car3Button.interactable = false;
        }

        if(moneyScript.money >= extraCheckpointValue)
        {
            extraCheckpointButton.interactable = true;
        }

        else
        {
            extraCheckpointButton.interactable = false;
        }

        if(moneyScript.money >= moreMoneyValue)
        {
            moreMoneyButton.interactable = true;
        }

        else
        {
            moreMoneyButton.interactable = false;
        }

        PlayerPrefs.SetFloat("rSlider", rSlider.value);
        PlayerPrefs.SetFloat("gSlider", gSlider.value);
        PlayerPrefs.SetFloat("bSlider", bSlider.value);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            firstStart = 1;
            speed++;
            countdown = 3;
            PlayerPrefs.SetInt("startup1", firstStart = 1);
            CancelInvoke("SlowDown");
           if(speed < 3)
            {
                Handheld.Vibrate();
            }
        }       


        if (countdown <= 0)
        {
            speed = 0;
        }

        if (lvl1CarCountdown <=0)
        {
            CancelInvoke("SpawnLvL1Car");
        }
        
        if (lvl2CarCountdown <=0)
        {
            CancelInvoke("SpawnLvL2Car");
        }
        
        if (lvl3CarCountdown <=0)
        {
            CancelInvoke("SpawnLvL3Car");
        }


            if (speed == 0)
        {
            Time.timeScale = 0.5f;
            speedText.text = "Speed X1";
            speedText.color = new Color32(130, 125, 100, 255);
        }
       else if(speed == 1)
        {
            Time.timeScale = 0.65f;
            speedText.text = "Speed X1.25";
            speedText.color = new Color32(254, 255, 73, 255);
        }
        else if(speed == 2)
        {
            Time.timeScale = 0.80f;
            speedText.text = "Speed X1.5";
            speedText.color = new Color32(255, 176, 0, 255);
        }
        else
        {
            Time.timeScale = 1;
            speedText.text = "Speed X2";
            speedText.color = new Color32(255, 0, 0, 255);
        }

        if(speed >= 3)
        {
            speed = 3;
        }    

        if(speed <= 0)
        {
            speed = 0;
        }

        mainCamera.backgroundColor = new Color32(((byte)rSlider.value), ((byte)gSlider.value), ((byte)bSlider.value), 255);
        PlayerPrefs.SetFloat("car1Value", car1Value);
        PlayerPrefs.SetFloat("car2Value", car2Value);
        PlayerPrefs.SetFloat("car3Value", car3Value);
        PlayerPrefs.SetFloat("mmv", moreMoneyValue);
        PlayerPrefs.SetInt("lvl1Car", lvl1Car);
        PlayerPrefs.SetInt("lvl2Car", lvl2Car);
        PlayerPrefs.SetInt("lvl3Car", lvl3Car);
    }


    private void CountdownDown()
    {
        countdown--;
    }

    private void SpawnLvL1Car()
    {
        Instantiate(car1Prefab, new Vector3(0.4f, 0.698f, 2.78f), Quaternion.identity);
        lvl1CarCountdown--;
    }
    private void SpawnLvL2Car()
    {
        Instantiate(car2Prefab, new Vector3(0.4f, 0.698f, 2.78f), Quaternion.identity);
        lvl2CarCountdown--;
    }
    
    private void SpawnLvL3Car()
    {
        Instantiate(car3Prefab, new Vector3(0.4f, 0.698f, 2.78f), Quaternion.identity);
        lvl3CarCountdown--;
    }

    public void Lvl1CarButton()
    {
        Instantiate(car1Prefab, new Vector3(0.4f, 0.698f, 2.78f), Quaternion.identity);
        lvl1Car++;
        moneyScript.money -=(int) car1Value;
        car1Value +=(int) Random.Range(20, 51);
        PlayerPrefs.SetFloat("car1Value", car1Value);
    }

    public void Lvl2CarButton()
    {
        Instantiate(car2Prefab, new Vector3(-9.19f, 0.68f, 5.41f), Quaternion.identity);
        lvl2Car++;
        moneyScript.money -= (int)car2Value;
        car2Value += Random.Range(100, 126);
        lvl1Car -= 3;
        greenCarDestroyer.SetActive(true);
        PlayerPrefs.SetFloat("car2Value", car1Value);
    }

    public void Lvl3CarButton()
    {
        Instantiate(car3Prefab, new Vector3(-9.09f, 0.48f, 6.03f), Quaternion.identity);
        lvl3Car++;
        moneyScript.money -= (int)car3Value;
        car3Value += (int) Random.Range(150, 201);
        lvl2Car -= 3;
        yellowCarDestroyer.SetActive(true);
        PlayerPrefs.SetFloat("car3Value", car3Value);
    }

    public void MoreMoneyButton()
    {
        moneyScript.lvl1Money *= 2;
        moneyScript.lvl2Money *= 2;
        moneyScript.lvl3Money *= 2;
        moneyScript.money -= (int)moreMoneyValue;
        moreMoneyValue +=(int) moreMoneyValue * 0.5f;
        PlayerPrefs.SetFloat("mmv", moreMoneyValue);
    }

    public void AddMoney()
    {
        moneyScript.money *= 2;
    }

    public void TakeMoney()
    {
        moneyScript.money /= 2;
    }
}
