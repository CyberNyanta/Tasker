package com.cybernyanta.tasker;

import android.content.SharedPreferences;

import com.cybernyanta.tasker.enums.TasksScreenType;

/**
 * Created by evgeniy.siyanko on 18.01.2017.
 */

public class PreferenceHelper {

    private final String PUSH_NOTIFICATION_DISPLAYING = "PUSH_NOTIFICATION_DISPLAYING";
    private final String START_TASKS_SCREEN_TYPE = "START_TASKS_SCREEN_TYPE";
    private final String TIME_FORMAT = "TIME_FORMAT";

    private SharedPreferences sharedPreferences;

    public PreferenceHelper(SharedPreferences sharedPreferences) {
        this.sharedPreferences = sharedPreferences;
    }

    public boolean isPushNotificationDisplayed() {
        return sharedPreferences.getBoolean(PUSH_NOTIFICATION_DISPLAYING, true);
    }

    public void setPushNotificationDisplaying(boolean isPushNotificationDisplayed) {
        sharedPreferences.edit()
                .putBoolean(PUSH_NOTIFICATION_DISPLAYING, isPushNotificationDisplayed)
                .commit();
    }

    public TasksScreenType getStartTasksScreenType() {
        return TasksScreenType.values()[sharedPreferences.getInt(START_TASKS_SCREEN_TYPE, 0)];
    }

    public void setStartTasksScreenType(TasksScreenType tasksScreenType) {
        sharedPreferences.edit()
                .putInt(START_TASKS_SCREEN_TYPE, tasksScreenType.ordinal())
                .commit();
    }


    public boolean is24HoursTimeFormatDisplayed() {
        return sharedPreferences.getBoolean(TIME_FORMAT, true);
    }

    public void set24HoursTimeFormatDisplayed(boolean is24HoursFormat) {
        sharedPreferences.edit()
                .putBoolean(TIME_FORMAT, is24HoursFormat)
                .commit();
    }
}
