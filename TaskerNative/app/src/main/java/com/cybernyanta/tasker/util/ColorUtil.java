package com.cybernyanta.tasker.util;

import android.support.annotation.ColorInt;
import android.support.annotation.ColorRes;
import android.support.v4.content.ContextCompat;

import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.TaskerApplication;
import com.cybernyanta.tasker.enums.TaskColor;

/**
 * Created by evgeniy.siyanko on 13.02.2017.
 */

public final class ColorUtil {

    @ColorInt
    public  static int getColor(@ColorRes int id){
        return ContextCompat.getColor(TaskerApplication.getContext(), id);
    }
    @ColorInt
    public static int getTaskColor(int number){
        switch (number){
            case TaskColor.NONE:
                return getColor(R.color.none);
            case TaskColor.LIME:
                return getColor(R.color.lime);
            case TaskColor.PEACH:
                return getColor(R.color.peach);
            case TaskColor.AQUA:
                return getColor(R.color.aqua);
            case TaskColor.BLUE:
                return getColor(R.color.blue);
            case TaskColor.SALMON:
                return getColor(R.color.salmon);
            case TaskColor.TEAL:
                return getColor(R.color.teal);
            case TaskColor.TAN:
                return getColor(R.color.tan);
            case TaskColor.YELLOW:
                return getColor(R.color.yellow);
            case TaskColor.VIOLET:
                return getColor(R.color.violet);
            default:
                return 0;
        }
    }
}
