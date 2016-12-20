package com.cybernyanta.tasker.enums;

import android.app.Application;
import android.content.res.Resources;
import android.graphics.Color;

/**
 * Created by evgeniy.siyanko on 20.12.2016.
 */

public enum TaskColors {
    None, Lime, Peach, Aqua, Blue, Salmon, Teal, Tan, Yellow, Violet;

    public int getColor(){
        switch (this){

            case Lime:
                return Color.parseColor("#FF00FF00");
            case Peach:
                return Color.parseColor("#FFFFDAB9");
            case Aqua:
                return Color.parseColor("#FF00FFFF");
            case Blue:
                return Color.parseColor("#FF87CEFA");
            case Salmon:
                return Color.parseColor("#FFFA8072");
            case Teal:
                return Color.parseColor("#FF008080");
            case Tan:
                return Color.parseColor("#FFD2B48C");
            case Yellow:
                return Color.parseColor("#FFFFFF00");
            case Violet:
                return Color.parseColor("#FFEE82EE");
            case None:
            default:
                return Color.parseColor("#FF9E9E9E");
        }
    }
}
