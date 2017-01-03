package com.cybernyanta.data.model;

import java.util.Date;

/**
 * Created by evgeniy.siyanko on 03.01.2017.
 */

public class Task extends BaseModel {
    private String id;
    private String title;
    private String description;
    private Date dueDate;
    private Date remindDate;
    private int color;

    public Task(){

    }

    public Task(String title, String description,Date dueDate, Date remindDate,int color ){
        this.title = title;
        this.description = description;
        this.dueDate = dueDate;
        this.remindDate = remindDate;
        this.color = color;
    }

    public String getTitle(){
        return title;
    }

    public void setTitle(String title){
        this.title = title;
    }

    public String getDescription(){
        return description;
    }

    public void setDescription(String description){
        this.description = description;
    }

    public Date getDueDate(){
        return dueDate;
    }

    public void setDueDate(Date dueDate){
        this.dueDate = dueDate;
    }

    public Date getRemindDate(){
        return remindDate;
    }

    public void setRemindDate(Date remindDate){
        this.remindDate = remindDate;
    }
    public int getColor(){
        return color;
    }

    public void setColor(int color){
        this.color = color;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }
}
