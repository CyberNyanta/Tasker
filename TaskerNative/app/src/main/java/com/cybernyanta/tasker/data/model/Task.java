package com.cybernyanta.tasker.data.model;

import com.google.firebase.database.IgnoreExtraProperties;

import java.io.Serializable;

import static com.cybernyanta.tasker.constants.FirebaseConstants.INBOX_ID;

/**
 * Created by evgeniy.siyanko on 03.01.2017.
 */
@IgnoreExtraProperties
public class Task extends BaseModel implements Serializable {
    private String title = "";
    private String description = "";
    private long dueDate = Long.MAX_VALUE;
    private long remindDate = Long.MAX_VALUE;
    private int color = 0;
    private String projectId = INBOX_ID;
    private boolean isCompleted = false;

    public Task(){
        super();
    }

    public Task(boolean isCompleted, String title, String description, long dueDate, long remindDate, int color, String projectId) {
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

    public long getDueDate(){
        return dueDate;
    }

    public void setDueDate(long dueDate){
        this.dueDate = dueDate;
    }

    public long getRemindDate(){
        return remindDate;
    }

    public void setRemindDate(long remindDate){
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
