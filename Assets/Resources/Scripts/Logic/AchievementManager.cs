using System.Linq;
using UnityEngine;
using System.Collections;

// AchievementManager contains Achievements, which players are able to earn through performing various actions
// in the game. Each Achievement specifies 

[System.Serializable]
public class Achievement
{
    public string Name;
    public string Description;
    public Texture2D IconIncomplete;
    public Texture2D IconComplete;
    public int RewardPoints;
    public float TargetProgress;
    public bool Secret;
    
    

    [HideInInspector]
    public bool Earned = false;
    public bool showPop = false;
    public float currentTime = 0.0f;
    public float currentTime2 = 0.0f;
    public float timeDelay = 3.0f;
    public float maxTime = 5.0f;
    public float posX;

    private float currentProgress = 0.0f;
    


	// Returns true if this progress added results in the Achievement being earned.
    public bool AddProgress(float progress)
    {
        if (Earned)
        {
            return false;
        }

        currentProgress += progress;
        if (currentProgress >= TargetProgress)
        {
            Earned = true;
            return true;
        }

        return false;
    }

	// Returns true if this progress set results in the Achievement being earned.
    public bool SetProgress(float progress)
    {
        if (Earned)
        {
            return false;
        }

        currentProgress = progress;
        if (progress >= TargetProgress)
        {
            Earned = true;
            return true;
        }

        return false;
    }


	// Basic GUI for displaying an achievement. Has a different style when earned and not earned.
    public void OnGUI(Rect position, GUIStyle GUIStyleAchievementEarned, GUIStyle GUIStyleAchievementNotEarned)
    {
        GUIStyle style = GUIStyleAchievementNotEarned;
        if (Earned)
        {
            style = GUIStyleAchievementEarned;
        }

        GUI.BeginGroup(position);
        GUI.Box(new Rect(0.0f, 0.0f, position.width, position.height), "");

        if (Earned)
        {
            GUI.Box(new Rect(0.0f, 0.0f, position.height, position.height), IconComplete);
        }
        else
        {
            GUI.Box(new Rect(0.0f, 0.0f, position.height, position.height), IconIncomplete);
        }

        GUI.Label(new Rect(80.0f, 5.0f, position.width - 80.0f - 50.0f, 25.0f), Name, style);

        if (Secret && !Earned)
        {
            GUI.Label(new Rect(80.0f, 25.0f, position.width - 80.0f, 25.0f), "Description Hidden!", style);
            //GUI.Label(new Rect(position.width - 50.0f, 5.0f, 25.0f, 25.0f), "???", style);
            GUI.Label(new Rect(position.width - 250.0f, 50.0f, 250.0f, 25.0f), "Progress Hidden!", style);
        }

        else if (showPop)
        {
            //GUI.Label(new Rect(80.0f, 25.0f, position.width - 80.0f, 25.0f), Description, style);
            //GUI.Label(new Rect(position.width - 50.0f, 5.0f, 25.0f, 25.0f), RewardPoints.ToString(), style);
            //GUI.Label(new Rect(position.width / 2, 50.0f, 250.0f, 25.0f), "Progress: [" + currentProgress.ToString("0.#") + " out of " + TargetProgress.ToString("0.#") + "]", style);
        }
        else
        {
            GUI.Label(new Rect(80.0f, 25.0f, position.width - 80.0f, 25.0f), Description, style);
            //GUI.Label(new Rect(position.width - 50.0f, 5.0f, 25.0f, 25.0f), RewardPoints.ToString(), style);
            GUI.Label(new Rect(position.width/2, 50.0f, 250.0f, 25.0f), "Progress: [" + currentProgress.ToString("0.#") + " out of " + TargetProgress.ToString("0.#") + "]", style);
        }

        GUI.EndGroup();
    }

}

public class AchievementManager : MonoBehaviour
{
    public Achievement[] Achievements;
    public AudioClip EarnedSound;
    public GUIStyle GUIStyleAchievementEarned;
    public GUIStyle GUIStyleAchievementNotEarned;
    public bool onMenu = false;
    //public GameObject popUp;

    private int currentRewardPoints = 0;
    private int potentialRewardPoints = 0;
    private float finalPosX = 0.0f;
    private float initialPosX = -250.0f;
    private bool showArchievementMenu = false;
    private Vector2 achievementScrollviewLocation = Vector2.zero;

	void Start()
	{
	    ValidateAchievements();
        UpdateRewardPointTotals();
        Application.targetFrameRate = 60;

        for (int i = 0; i < Achievements.GetLength(0); i++)
        {
            Achievements[i].currentTime = 0.0f;
            Achievements[i].currentTime2 = 0.0f;
            Achievements[i].timeDelay = 3.0f * 60;
            Achievements[i].maxTime = 5.0f * 60;
            Achievements[i].posX = initialPosX;
        }
	}

    void Update()
    {
        for (int i = 0; i < Achievements.GetLength(0); i++)
        {
            if(Achievements[i].showPop == true)
            {
                Achievements[i].currentTime ++;
                Achievements[i].timeDelay --;

                if (Achievements[i].currentTime <= 1*60)
                {
                    Achievements[i].posX = (float)Easing.CubicEaseIn(Achievements[i].currentTime, initialPosX, (finalPosX - initialPosX), 1.0f*60);
                    
                }
                else if (Achievements[i].currentTime >= Achievements[i].maxTime)
                {
                    Achievements[i].showPop = false;

                }

                if (Achievements[i].timeDelay <= 0)
                {
                    Achievements[i].currentTime2 ++;
                    if (Achievements[i].currentTime2 <= 1*60) Achievements[i].posX = (float)Easing.CubicEaseIn(Achievements[i].currentTime2, finalPosX, (initialPosX - finalPosX), 1.0f*60);
                }

                
            }
        }
    }
	
    // Make sure some assumptions about achievement data setup are followed.
    private void ValidateAchievements()
    {
        ArrayList usedNames = new ArrayList();
        foreach (Achievement achievement in Achievements)
        {
            if (achievement.RewardPoints < 0)
            {
                Debug.LogError("AchievementManager::ValidateAchievements() - Achievement with negative RewardPoints! " + achievement.Name + " gives " + achievement.RewardPoints + " points!");
            }

            if (usedNames.Contains(achievement.Name))
            {
                Debug.LogError("AchievementManager::ValidateAchievements() - Duplicate achievement names! " + achievement.Name + " found more than once!");
            }
            usedNames.Add(achievement.Name);
        }
    }

    private Achievement GetAchievementByName(string achievementName)
    {
        return Achievements.FirstOrDefault(achievement => achievement.Name == achievementName);
    }

    private void UpdateRewardPointTotals()
    {
        currentRewardPoints = 0;
        potentialRewardPoints = 0;

        foreach (Achievement achievement in Achievements)
        {
            if (achievement.Earned)
            {
                currentRewardPoints += achievement.RewardPoints;
            }

            potentialRewardPoints += achievement.RewardPoints;
        }
    }

    private void AchievementEarned(string achievementName)
    {
        UpdateRewardPointTotals();
        AudioSource.PlayClipAtPoint(EarnedSound, Camera.main.transform.position);
        Achievement achievement = GetAchievementByName(achievementName);
        achievement.showPop = true;
    }

    public void AddProgressToAchievement(string achievementName, float progressAmount)
    {
        Achievement achievement = GetAchievementByName(achievementName);
        if (achievement == null)
        {
            Debug.LogWarning("AchievementManager::AddProgressToAchievement() - Trying to add progress to an achievemnet that doesn't exist: " + achievementName);
            return;
        }

        if (achievement.AddProgress(progressAmount))
        {
            AchievementEarned(achievementName);
        }
    }

    public void SetProgressToAchievement(string achievementName, float newProgress)
    {
        Achievement achievement = GetAchievementByName(achievementName);
        if (achievement == null)
        {
            Debug.LogWarning("AchievementManager::SetProgressToAchievement() - Trying to add progress to an achievemnet that doesn't exist: " + achievementName);
            return;
        }

        if (achievement.SetProgress(newProgress))
        {
            AchievementEarned(achievementName);
        }
    }
    
	// Sets up a scrollview and fills it out with each Achievement.
	// Also displays the total number of reward points earned.
    void OnGUI()
    {
        if (onMenu)
        {
            float yValue = (Screen.width / 2 - 75 * 4) - 25;
            float achievementGUIWidth = 400.0f;

            //GUI.Label(new Rect(200.0f, 5.0f, 200.0f, 25.0f), "-- Achievements --");

            achievementScrollviewLocation = GUI.BeginScrollView(new Rect((Screen.width / 2 - achievementGUIWidth / 2), (Screen.width / 2 - 75*4), achievementGUIWidth + 25.0f, 250.0f), achievementScrollviewLocation,
                                                                new Rect((Screen.width / 2 - achievementGUIWidth / 2), (Screen.width / 2 - 75*4) - 25, achievementGUIWidth, Achievements.Count() * 80.0f));

            foreach (Achievement achievement in Achievements)
            {
                Rect position = new Rect((Screen.width / 2 - achievementGUIWidth / 2) + 5.0f, yValue, achievementGUIWidth, 75.0f);
                achievement.OnGUI(position, GUIStyleAchievementEarned, GUIStyleAchievementNotEarned);
                yValue += 80.0f;
            }

            GUI.EndScrollView();

            //GUI.Label(new Rect(10.0f, 440.0f, 200.0f, 25.0f), "Reward Points: [" + currentRewardPoints + " out of " + potentialRewardPoints + "]");
        }

        foreach (Achievement achievement in Achievements)
        {
            if (achievement.showPop)
            {
                Rect position = new Rect(achievement.posX, Screen.height - 75, 200, 75.0f);
                achievement.OnGUI(position, GUIStyleAchievementEarned, GUIStyleAchievementNotEarned);
            }
        }
    }
}
