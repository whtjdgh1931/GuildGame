using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelSceneUIManager : MonoBehaviour
{
    private static LevelSceneUIManager instance;

    public static LevelSceneUIManager Instance()
    {
        return instance;
    }

    public string className;
    public int classLevel;
    public Text classNameText;
    public Text levelText;
    public Text hpText;
    public Text apkPowerText;
    public Text apkRangeText;

    public Image classImage;
    public Image classBGImage;

    public List<Dictionary<string, object>> classLevelData;

    public Button levelUpBtn;

    public RectTransform contents;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;

        ClassSelectBtn firstBtn = contents.GetChild(0).gameObject.GetComponent<ClassSelectBtn>();
        SetClassDataInfo(firstBtn.className,firstBtn.classImage.sprite,firstBtn.classLogoSprite);
    }

    public void SetClassDataInfo(string ClassName,Sprite classImageSprite,Sprite classLogoSprite)
    {
        className = ClassName;
        classLevelData = ClassManager.Instance().GetLevelData(className);
        classLevel = Mathf.Max(1,PlayerPrefs.GetInt(className));

        classNameText.text = className;
        levelText.text = classLevel.ToString();

        hpText.text = classLevelData[classLevel - 1]["MaxHp"].ToString();

        apkPowerText.text = classLevelData[classLevel - 1]["AttackPower"].ToString();
        apkRangeText.text = classLevelData[classLevel - 1]["AttackRange"].ToString();

        classImage.sprite = classImageSprite;
        classBGImage.sprite = classLogoSprite;

    }

    public void SetClassDataInfo()
    {
        if (classLevel == Constants.maxLevel)
        {
            levelUpBtn.interactable = true;

            levelUpBtn.image.color = Color.white;
        }
        classLevel = 1;
        PlayerPrefs.SetInt(className, classLevel);

        classLevelData = ClassManager.Instance().GetLevelData(className);
        classLevel = PlayerPrefs.GetInt(className);

        classNameText.text = className;
        levelText.text = classLevel.ToString();

        hpText.text = classLevelData[classLevel - 1]["MaxHp"].ToString();

        apkPowerText.text = classLevelData[classLevel - 1]["AttackPower"].ToString();
        apkRangeText.text = classLevelData[classLevel - 1]["AttackRange"].ToString();
    }

    public void LevelUp()
    {
        if (classLevel >= Constants.maxLevel) return;
        
        classLevel++;


        PlayerPrefs.SetInt(className, classLevel);

        classLevelData = ClassManager.Instance().GetLevelData(className);
        classLevel = PlayerPrefs.GetInt(className);

        classNameText.text = className;
        levelText.text = classLevel.ToString();

        hpText.text = classLevelData[classLevel - 1]["MaxHp"].ToString();

        apkPowerText.text = classLevelData[classLevel - 1]["AttackPower"].ToString();
        apkRangeText.text = classLevelData[classLevel - 1]["AttackRange"].ToString();

        if(classLevel == Constants.maxLevel)
        {
            levelUpBtn.interactable = false;

            levelUpBtn.image.color = Color.gray;
        }


    }

}
