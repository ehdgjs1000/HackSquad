using System.Collections;
using System;
using UnityEngine;


public class TimeManager : MonoBehaviour
{
    DateTime nextDailyResetTime;
    DateTime nextWeeklyResetTime;

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

            // ���� ���� üũ
            if (now >= nextDailyResetTime)
            {
                StartCoroutine(DailyReset());
                ScheduleNextDailyReset();
            }

            // �ְ� ���� üũ
            if (now >= nextWeeklyResetTime)
            {
                StartCoroutine(WeeklyReset());
                ScheduleNextWeeklyReset();
            }

            // ���� �ð� ��� �� UI ������Ʈ
            TimeSpan remainDaily = nextDailyResetTime - now;
            TimeSpan remainWeekly = nextWeeklyResetTime - now;
            TimeUI(remainDaily, remainWeekly);

            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator DailyReset()
    {
        Debug.Log("Daily Reset");
        yield return new WaitForSeconds(1f);
        BackEndGameData.Instance.UserQuestData.ResetDaily();
        BackEndGameData.Instance.GameDataUpdate();
        //QuestManager.instance.UpdateQuestUI();
    }
    IEnumerator WeeklyReset()
    {
        Debug.Log("Weekly Reset");
        BackEndGameData.Instance.UserQuestData.ResetWeekly();
        BackEndGameData.Instance.GameDataUpdate();
        yield return new WaitForSeconds(1f);
        //QuestManager.instance.UpdateQuestUI();
    }
    private void TimeUI(TimeSpan remainDaily, TimeSpan remainWeekly)
    {
        // ���� Ÿ�̸� ǥ�� (HH��MM��SS��)
        string dailyStr = string.Format("{0:D2}��{1:D2}��{2:D2}��",
            (int)remainDaily.TotalHours, remainDaily.Minutes, remainDaily.Seconds);

        // �ְ� Ÿ�̸� ǥ�� (D�� HH��MM�� �Ǵ� HH��MM��SS��)
        string weeklyStr;
        if (remainWeekly.Days > 0)
        {
            weeklyStr = string.Format("{0}�� {1:D2}��{2:D2}��",
                remainWeekly.Days, remainWeekly.Hours, remainWeekly.Minutes);
        }
        else
        {
            weeklyStr = string.Format("{0:D2}��{1:D2}��{2:D2}��",
                (int)remainWeekly.TotalHours, remainWeekly.Minutes, remainWeekly.Seconds);
        }

        //UIManager.Instance.questUIHandler.SetUITimer(dailyStr, weeklyStr);
    }
}
