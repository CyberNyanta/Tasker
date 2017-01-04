package com.cybernyanta.core.model;

import com.google.firebase.database.Exclude;
import com.google.firebase.database.IgnoreExtraProperties;

/**
 * Created by evgeniy.siyanko on 03.01.2017.
 */
@IgnoreExtraProperties
public abstract class BaseModel {
    @Exclude
    private String id;

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public BaseModel(){

    }
}
