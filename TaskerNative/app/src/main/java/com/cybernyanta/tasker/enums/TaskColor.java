package com.cybernyanta.tasker.enums;

import android.app.Application;
import android.content.res.Resources;
import android.graphics.Color;

/**
 * Created by evgeniy.siyanko on 20.12.2016.
 */

public enum TaskColor {
    NONE, LIME, PEACH, AQUA, BLUE, SALMON, TEAL, TAN, YELLOW, VIOLET;

    public int getColor() {
        switch (this) {
            case LIME:
                return Color.parseColor("#FF00FF00");
            case PEACH:
                return Color.parseColor("#FFFFDAB9");
            case AQUA:
                return Color.parseColor("#FF00FFFF");
            case BLUE:
                return Color.parseColor("#FF87CEFA");
            case SALMON:
                return Color.parseColor("#FFFA8072");
            case TEAL:
                return Color.parseColor("#FF008080");
            case TAN:
                return Color.parseColor("#FFD2B48C");
            case YELLOW:
                return Color.parseColor("#FFFFFF00");
            case VIOLET:
                return Color.parseColor("#FFEE82EE");
            case NONE:
            default:
                return Color.parseColor("#FF9E9E9E");
        }
    }
}
