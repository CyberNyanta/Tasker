package com.cybernyanta.tasker.enums;

import android.app.Application;
import android.content.res.Resources;
import android.graphics.Color;

/**
 * Created by evgeniy.siyanko on 20.12.2016.
 */

public class TaskColor {
    public static final int NONE = 0;
    public static final int LIME = 1;
    public static final int PEACH = 2;
    public static final int AQUA = 3;
    public static final int BLUE = 4;
    public static final int SALMON = 5;
    public static final int TEAL = 6;
    public static final int TAN = 7;
    public static final int YELLOW = 8;
    public static final int VIOLET = 9;

    private static final int COUNT = 10;

    public static int count(){
        return COUNT;
    }
}
