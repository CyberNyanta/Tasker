package com.cybernyanta.tasker.models;

import android.graphics.Color;

import java.util.Date;

import android.os.Parcel;
import android.os.Parcelable;
/**
 * Created by evgeniy.siyanko on 15.12.2016.
 */

public class Task{

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

    public Task setTitle(String title){
        this.title = title;
        return this;
    }

    public String getDescription(){
        return description;
    }

    public Task setDescription(String description){
        this.description = description;
        return this;
    }

    public Date getDueDate(){
        return dueDate;
    }

    public Task setDueDate(Date dueDate){
        this.dueDate = dueDate;
        return this;
    }

    public Date getRemindDate(){
        return remindDate;
    }

    public Task setRemindDate(Date remindDate){
        this.remindDate = remindDate;
        return this;
    }
    public int getColor(){
        return color;
    }

    public Task setColor(int color){
        this.color = color;
        return this;
    }

}
