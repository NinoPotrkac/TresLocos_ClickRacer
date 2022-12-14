using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyMaker : MonoBehaviour
{
    [Header("Money Values")]
    public int money;
    public float lvl1Money = 10;
    public float lvl2Money = 65;
    public float lvl3Money = 150;
    
    [Header("Texts")]
    [SerializeField]
    TextMeshProUGUI moneyText;

    [Header("Transforms")]
    public Transform instTransform;

    [Header("Prefabs")]
    public GameObject floatingText;
    public GameObject floatingText2;
    public GameObject floatingText3;

    [Header("Second Money Making")]
    public Collider money2Colider;
    public GameObject checkPoint;
    public int secondOn;

    [Header("Goal")]
    public Slider goalSlider;
    public TextMeshProUGUI goalText;

    [Header("Sold Out")]
    public GameObject soldOutImage;
    public int soldOutOn = 0;

    [Header("On Startup")]
    public int firstStart;

    [Header("End")]
    public GameObject endScreen;

    private void Awake()
    {
        firstStart = PlayerPrefs.GetInt("startup");
    }

    private void Start()
    {
        if(firstStart == 0)
        {
            PlayerPrefs.SetFloat("lvl1Money", lvl1Money = 10);
            PlayerPrefs.SetFloat("lvl2Money", lvl2Money = 65);
            PlayerPrefs.SetFloat("lvl3Money", lvl3Money = 150);

        }
        soldOutOn = PlayerPrefs.GetInt("soldOut");
        secondOn = PlayerPrefs.GetInt("secondOn");
        money = PlayerPrefs.GetInt("money");
        lvl1Money = PlayerPrefs.GetFloat("lvl1Money");
        lvl2Money = PlayerPrefs.GetFloat("lvl2Money");
        lvl3Money = PlayerPrefs.GetFloat("lvl3Money");        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Level 1 car")
        {
            money +=(int) lvl1Money;
            PlayerPrefs.SetInt("money", money);
            var go = Instantiate(floatingText, new Vector3(9f, 5.29f, 6.04f), Quaternion.identity, instTransform);
        }

        if(other.gameObject.tag == "Level 2 car")
        {
            money +=(int) lvl2Money;
            PlayerPrefs.SetInt("money", money);
            var go = Instantiate(floatingText2, new Vector3(9f, 5.29f, 6.04f), Quaternion.identity, instTransform);
        }

        if (other.gameObject.tag == "Level 3 car")
        {
            money +=(int) lvl3Money;
            PlayerPrefs.SetInt("money", money);
            var go = Instantiate(floatingText3, new Vector3(9f, 5.29f, 6.04f), Quaternion.identity, instTransform);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            firstStart = 1;
        }
        PlayerPrefs.SetInt("startup", firstStart = 1);

        if (money < 0)
        {
            money = 0;
        }
        moneyText.text = money.ToString();  

        if(secondOn == 1)
        {
            money2Colider.enabled = true;
            checkPoint.SetActive(true);
        }
        else if(secondOn <=0)
        {
            money2Colider.enabled = false;
            checkPoint.SetActive(false);
        }

        goalSlider.value = money;
        goalText.text = money + "/1000000";

        if(soldOutOn == 1)
        {
            soldOutImage.SetActive(true);
        }
        else
        {
            soldOutImage.SetActive(false);
            checkPoint.SetActive(false);
        }
        PlayerPrefs.SetInt("soldOut", soldOutOn);
        PlayerPrefs.SetInt("secondOn", secondOn);
        PlayerPrefs.SetFloat("lvl1Money", lvl1Money);
        PlayerPrefs.SetFloat("lvl2Money", lvl2Money);
        PlayerPrefs.SetFloat("lvl3Money", lvl3Money);

        if(money >= 1000000)
        {
            endScreen.SetActive(true);
        }

    }

    public void SecondMoneyMaker()
    {
        money2Colider.enabled = true;
        checkPoint.SetActive(true);
        secondOn = 1;
        soldOutOn = 1;
        money -= 20000;
        PlayerPrefs.SetInt("secondOn", secondOn);
        PlayerPrefs.SetInt("soldOut", soldOutOn);
    }

    public void destroyEnd()
    {
        Destroy(endScreen);
    }
}
