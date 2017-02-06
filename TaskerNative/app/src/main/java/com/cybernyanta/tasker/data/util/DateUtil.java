package com.cybernyanta.tasker.data.util;

import android.app.Application;
import android.content.Context;
import android.icu.text.SimpleDateFormat;
import android.text.format.DateFormat;

import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.TaskerApplication;

import java.util.Calendar;
import java.util.Date;

/**
 * Created by evgeniy.siyanko on 05.01.2017.
 */

public final class DateUtil
{

    public static Calendar getTodayCalendar(){
        Calendar calendar = Calendar.getInstance();
        calendar.set(Calendar.HOUR_OF_DAY, 0);
        calendar.set(Calendar.MINUTE, 0);
        calendar.set(Calendar.SECOND, 0);
        calendar.set(Calendar.MILLISECOND,0);
        return calendar;
    }

    public static Date getTodayDate(){
        return getTodayCalendar().getTime();
    }

    public static long getTodayEpochDate(){
        return getTodayCalendar().getTimeInMillis();
    }

    public static Calendar addDays(Calendar calendar, int days){
        calendar.add(Calendar.DAY_OF_MONTH, days);
        return calendar;
    }

    public static Date addDays(Date date, int days){
        Calendar calendar = Calendar.getInstance();
        calendar.setTime(date);
        return addDays(calendar, days).getTime();
    }

    public static long addDays(long epochDate, int days){
        Calendar calendar = Calendar.getInstance();
        calendar.setTimeInMillis(epochDate);
        return addDays(calendar, days).getTimeInMillis();
    }

    public static long getDay(long epochDate){
        Calendar calendar = Calendar.getInstance();
        calendar.setTimeInMillis(epochDate);
        calendar.set(Calendar.HOUR_OF_DAY, 0);
        calendar.set(Calendar.MINUTE, 0);
        calendar.set(Calendar.SECOND, 0);
        calendar.set(Calendar.MILLISECOND,0);
        return calendar.getTimeInMillis();
    }

/*    public static String dateToString(Date date, String pattern){
        DateFormat format = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");
        format.setTimeZone(TimeZone.getTimeZone("Australia/Sydney"));
        String formatted = format.format(date);
        return calendar.getTimeInMillis();
    }*/


 /*   public static String dueDateToString(long dueDate)
    {
        Context context = TaskerApplication.getContext();
//        boolean is24hoursTimeFormate = context.getSharedPreferences(Constans.SHARED_PREFERENCES_FILE, FileCreationMode.Private)
//                .GetBoolean(context.getString(R.string.settings_24hours_format), false);
        if (dueDate != Long.MAX_VALUE)
        {
            if (dueDate == getTodayEpochDate())
            {
                return context.getString(R.string.due_dates_today);
            }
            else if(getDay(dueDate) == getTodayEpochDate())
            {
                return context.getString(R.string.due_dates_today_at, GetLocaleTime(dueDate, is24hoursTimeFormate));
            }
            else if (dueDate == DateTime.Today.AddDays(1))
            {
                return context.getString(R.string.due_dates_tomorrow);
            }
            else if (dueDate.Date == DateTime.Today.AddDays(1))
            {
                return context.getString(R.string.due_dates_tomorrow_at, GetLocaleTime(dueDate, is24hoursTimeFormate));
            }
            else if (dueDate > DateTime.Today && dueDate < DateTime.Today.AddDays(8))
            {
                return $"{dueDate.ToString(context.getString(R.string.datetime_format_date))}, {GetLocaleTime(dueDate, is24hoursTimeFormate)}";
            }
            else
            {
                return dueDate.ToString(dueDate.Year == DateTime.Today.Year ? context.getString(R.string.datetime_format_date)
                        : context.getString(R.string.datetime_format_date_year));
            }
        }
        else
        {
            return context.getString(R.string.datetime_none);
        }
    }*/
}
