package com.cybernyanta.core.model;

import com.google.firebase.database.Exclude;
import com.google.firebase.database.IgnoreExtraProperties;

import java.io.Serializable;
import java.util.Date;

/**
 * Created by evgeniy.siyanko on 03.01.2017.
 */
@IgnoreExtraProperties
public class Task extends BaseModel implements Serializable {
    private String title;
    private String description;
    private Date dueDate;
    private Date remindDate;
    private int color;
    private String projectId;
    private boolean isCompleted;

    public Task(){
        super();
        dueDate = new Date(Long.MAX_VALUE);
        remindDate = new Date(Long.MAX_VALUE);
    }

    public Task(boolean isCompleted, String title, String description, Date dueDate, Date remindDate, int color, String projectId) {
        this.isCompleted = isCompleted;
        this.title = title;
        this.description = description;
        this.dueDate = dueDate;
        this.remindDate = remindDate;
        this.color = color;
        this.projectId = projectId;
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

    public boolean isCompleted() {
        return isCompleted;
    }

    public void setCompleted(boolean completed) {
        isCompleted = completed;
    }

    public String getProject() {
        return projectId;
    }

    public void setProject(String projectId) {
        this.projectId = projectId;
    }
}
