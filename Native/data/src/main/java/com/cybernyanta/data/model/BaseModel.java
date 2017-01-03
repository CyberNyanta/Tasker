package com.cybernyanta.data.model;

import com.google.firebase.database.Exclude;

/**
 * Created by evgeniy.siyanko on 03.01.2017.
 */

public abstract class BaseModel {
    @Exclude
    private String id;

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }
}
