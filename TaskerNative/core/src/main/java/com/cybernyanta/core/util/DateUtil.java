package com.cybernyanta.core.util;

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

    public static Calendar addDays(Calendar calendar, int days){
        calendar.add(Calendar.DAY_OF_MONTH, days);
        return calendar;
    }

    public static Date addDays(Date date, int days){
        Calendar calendar = Calendar.getInstance();
        calendar.setTime(date);
        return addDays(calendar, days).getTime();
    }
}
