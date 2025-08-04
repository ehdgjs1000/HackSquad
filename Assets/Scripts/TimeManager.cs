using System.Collections;
using System;
using UnityEngine;


public class TimeManager : MonoBehaviour
{
    public static TimeManager instance; 

    DateTime nextDailyResetTime;
    DateTime nextWeeklyResetTime;

    public string dailyStr;
    public string weeklyStr;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        SetResetTime();
        StartCoroutine(CountdownCoroutine());
    }
    private void SetResetTime()
    {
        if (PlayerPrefs.HasKey("nextDailyResetTime"))
        {
            nextDailyResetTime = DateTime.ParseExact(PlayerPrefs.GetString("nextDailyResetTime"), "yyyy-MM-dd HH:mm:ss", null);
        }
        if (PlayerPrefs.HasKey("nextWeeklyResetTime"))
        {
            nextWeeklyResetTime = DateTime.ParseExact(PlayerPrefs.GetString("nextWeeklyResetTime"), "yyyy-MM-dd HH:mm:ss", null);
        }

    }
    private void ScheduleNextDailyReset()
    {
        DateTime now = DateTime.Now;
        var todayNine = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
        nextDailyResetTime = now < todayNine ? todayNine : todayNine.AddDays(1);
        PlayerPrefs.SetString("nextDailyResetTime", nextDailyResetTime.ToString("yyyy-MM-dd HH:mm:ss"));
    }
    private void ScheduleNextWeeklyReset()
    {
        DateTime now = DateTime.Now;
        int daysUntilMonday = ((int)DayOfWeek.Monday - (int)now.DayOfWeek + 7) % 7;
        var targetDay = now.Date.AddDays(daysUntilMonday).AddHours(9);
        nextWeeklyResetTime = now < targetDay ? targetDay : targetDay.AddDays(7);
        PlayerPrefs.SetString("nextWeeklyResetTime", nextWeeklyResetTime.ToString("yyyy-MM-dd HH:mm:ss"));
    }
    private IEnumerator CountdownCoroutine()
    {
        while (true)
        {
            DateTime now = DateTime.Now;

            // 일일 리셋 체크
            if (now >= nextDailyResetTime)
            {
                StartCoroutine(DailyReset());
                ScheduleNextDailyReset();
            }

            // 주간 리셋 체크
            if (now >= nextWeeklyResetTime)
            {
                StartCoroutine(WeeklyReset());
                ScheduleNextWeeklyReset();
            }

            // 남은 시간 계산 및 UI 업데이트
            TimeSpan remainDaily = nextDailyResetTime - now;
            TimeSpan remainWeekly = nextWeeklyResetTime - now;
            TimeUI(remainDaily, remainWeekly);

            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator DailyReset()
    {
        Debug.Log("Daily Reset");
        yield return new WaitForSeconds(0.5f);
        //매일 리셋해야 하는 것 넣기
        BackEndGameData.Instance.UserQuestData.ResetDaily();
        PlayerPrefs.SetInt("FreePackage", 0);
        PlayerPrefs.SetInt("DrawVideo", 3);
        PlayerPrefs.SetInt("remainAbilityVideo", 1); 
        PlayerPrefs.SetInt("sweepLeftVideo", 3);

        BackEndGameData.Instance.GameDataUpdate();
    }
    IEnumerator WeeklyReset()
    {
        Debug.Log("Weekly Reset");
        BackEndGameData.Instance.UserQuestData.ResetWeekly();
        PlayerPrefs.SetInt("FreePackage", 0);
        PlayerPrefs.SetInt("DrawVideo", 3);
        PlayerPrefs.SetInt("remainAbilityVideo", 1);
        PlayerPrefs.SetInt("sweepLeftVideo", 3);
        BackEndGameData.Instance.GameDataUpdate();
        yield return new WaitForSeconds(1f);
        //QuestManager.instance.UpdateQuestUI();
    }
    private void TimeUI(TimeSpan remainDaily, TimeSpan remainWeekly)
    {
        // 일일 타이머 표시 (HH시MM분SS초)
        dailyStr = string.Format("{0:D2}시간{1:D2}분{2:D2}초",
            (int)remainDaily.TotalHours, remainDaily.Minutes, remainDaily.Seconds);

        // 주간 타이머 표시 (D일 HH시MM분 또는 HH시MM분SS초)
        //string weeklyStr;
        if (remainWeekly.Days > 0)
        {
            weeklyStr = string.Format("{0}일 {1:D2}시간 {2:D2}분",
                remainWeekly.Days, remainWeekly.Hours, remainWeekly.Minutes);
        }
        else
        {
            weeklyStr = string.Format("{0:D2}시{1:D2}분{2:D2}초",
                (int)remainWeekly.TotalHours, remainWeekly.Minutes, remainWeekly.Seconds);
        }
    }
}
