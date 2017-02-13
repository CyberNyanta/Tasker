package com.cybernyanta.tasker.data.model;

import com.google.firebase.database.IgnoreExtraProperties;

import java.io.Serializable;

/**
 * Created by evgeniy.siyanko on 03.01.2017.
 */
@IgnoreExtraProperties
public abstract class BaseModel implements Serializable {

    private String id;

    public BaseModel(){
    }

    public BaseModel(String id) {
        this.id = id;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

}
