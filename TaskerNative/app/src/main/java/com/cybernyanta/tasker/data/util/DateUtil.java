package com.cybernyanta.tasker.data.util;

import android.app.Application;
import android.content.Context;

import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.TaskerApplication;

import java.util.Calendar;
import java.util.Date;
import java.text.SimpleDateFormat;
import java.util.TimeZone;

/**
 * Created by evgeniy.siyanko on 05.01.2017.
 */

public final class DateUtil {

    public static Calendar getTodayCalendar() {
        Calendar calendar = Calendar.getInstance();
        calendar.set(Calendar.HOUR_OF_DAY, 0);
        calendar.set(Calendar.MINUTE, 0);
        calendar.set(Calendar.SECOND, 0);
        calendar.set(Calendar.MILLISECOND, 0);
        return calendar;
    }

    public static Date getTodayDate() {
        return getTodayCalendar().getTime();
    }

    public static long getTodayEpochDate() {
        return getTodayCalendar().getTimeInMillis();
    }

    public static Calendar addDays(Calendar calendar, int days) {
        calendar.add(Calendar.DAY_OF_MONTH, days);
        return calendar;
    }

    public static Date addDays(Date date, int days) {
        Calendar calendar = Calendar.getInstance();
        calendar.setTime(date);
        return addDays(calendar, days).getTime();
    }

    public static long addDays(long epochDate, int days) {
        Calendar calendar = Calendar.getInstance();
        calendar.setTimeInMillis(epochDate);
        return addDays(calendar, days).getTimeInMillis();
    }

    public static long getDay(long epochDate) {
        Calendar calendar = Calendar.getInstance();
        calendar.setTimeInMillis(epochDate);
        calendar.set(Calendar.HOUR_OF_DAY, 0);
        calendar.set(Calendar.MINUTE, 0);
        calendar.set(Calendar.SECOND, 0);
        calendar.set(Calendar.MILLISECOND, 0);
        return calendar.getTimeInMillis();
    }

    public static String dateToString(Date date, String pattern) {
        SimpleDateFormat format = new SimpleDateFormat(pattern);
        format.setTimeZone(TimeZone.getDefault());
        return format.format(date);
    }

    public static String dateToString(long epochTime, String pattern) {
        return dateToString(new Date(epochTime), pattern);
    }

    public static boolean isDateInCurrentYear(long epochTime) {
        Calendar calendar = Calendar.getInstance();
        int current = calendar.get(Calendar.YEAR);
        calendar.setTimeInMillis(epochTime);
        return current == calendar.get(Calendar.YEAR) ? true : false;
    }


    public static String dueDateToString(long dueDate, boolean is24hoursTimeFormat) {
        Context context = TaskerApplication.getContext();
        if (dueDate != Long.MAX_VALUE) {
            if (dueDate == getTodayEpochDate()) {
                return context.getString(R.string.due_dates_today);
            } else if (getDay(dueDate) == getTodayEpochDate()) {
                return context.getString(R.string.due_dates_today_at, dateToString(dueDate,
                        context.getString(is24hoursTimeFormat ? R.string.time_format_24 : R.string.time_format_12)));
            } else if (dueDate == addDays(getTodayEpochDate(), 1)) {
                return context.getString(R.string.due_dates_tomorrow);
            } else if (getDay(dueDate) == addDays(getTodayEpochDate(), 1)) {
                return context.getString(R.string.due_dates_tomorrow_at, dateToString(dueDate,
                        context.getString(is24hoursTimeFormat ? R.string.time_format_24 : R.string.time_format_12)));
            } else if (dueDate > getTodayEpochDate() && dueDate < addDays(getTodayEpochDate(), 8)) {
                return dateToString(dueDate, context.getString(R.string.datetime_format_date)) + dateToString(new Date(dueDate),
                        context.getString(is24hoursTimeFormat ? R.string.time_format_24 : R.string.time_format_12));
            } else {
                return dateToString(dueDate, isDateInCurrentYear(dueDate) ? context.getString(R.string.datetime_format_date)
                        : context.getString(R.string.datetime_format_date_year));
            }
        } else {
            return context.getString(R.string.datetime_none);
        }
    }
}
