using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpStateDisplay : MonoBehaviour
{
    public LevelUpBalanceConfig[] balanceConfigs;

    public int currentExperience = 0;
    public int experienceGainDelta = 10;
    private int currentLevel = 1;

    public Text currentExperienceOutput;
    public Text currentLevelOutput;
    public Button increaseExperienceButton;

    void Start()
    {
        this.increaseExperienceButton.onClick.AddListener(() =>
        {
            this.currentExperience += experienceGainDelta;
        });
    }

    void Update()
    {
        if (this.CheckIfLevelUpRequired())
        {
            Debug.Log("Level up!");
            this.currentLevel++;
        }

        this.currentExperienceOutput.text = $"Current experience: {this.currentExperience}";
        this.currentLevelOutput.text = $"Current level: {this.currentLevel}";
    }

    private bool CheckIfLevelUpRequired()
    {
        LevelUpBalanceConfig correspondingLevel = this.balanceConfigs
            .Where(balanceConfig => balanceConfig.ExpCumulative <= this.currentExperience)
            .OrderByDescending(balanceConfig => balanceConfig.ExpCumulative)
            .OrderByDescending(balanceConfig => balanceConfig.LevelId)
            .FirstOrDefault();

        return correspondingLevel.LevelId > this.currentLevel;
    }
}
