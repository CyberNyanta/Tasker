package com.cybernyanta.core.manager;

import com.cybernyanta.core.database.FirebaseArrayList;
import com.cybernyanta.core.database.OnChangedListener;
import com.cybernyanta.core.model.Project;
import com.cybernyanta.core.model.Task;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Date;
import java.util.List;

/**
 * Created by evgeniy.siyanko on 04.01.2017.
 */

public class TaskManager{

    private FirebaseArrayList<Task> datasource;

    public TaskManager(FirebaseArrayList<Task> datasource) {
        this.datasource = datasource;
    }

    public void addOnChangedListener(OnChangedListener listener) {
        datasource.addOnChangedListener(listener);
    }

    public void cleanup() {
        datasource.cleanup();
    }

   /* public List<Task> GetProjectTasks(String projectId){
    }

    public List<Task> GetProjectOpenTasks(String projectId){
    }

    public List<Task> GetProjectCompletedTasks(String projectId){

    }*/

    public List<Task> GetAllOpen(){
        List<Task> result = new ArrayList<>();
        for(Task task:datasource){
            if(!task.isCompleted()){
                result.add(task);
            }
        }
        return  result;
    }

    public List<Task> GetAllCompleted(){
        List<Task> result = new ArrayList<>();
        for(Task task:datasource){
            if(task.isCompleted()){
                result.add(task);
            }
        }
        return result;
    }

/*    public List<Task> GetWhere(Predicate<Task> predicate){

    }*/

   /* public List<Project> GetProjects(){

    }*/

    public List<Task> GetForToday(){
        List<Task> result = new ArrayList<>();
        for(Task task:datasource){
            if(task.getDueDate().compareTo(new Date())==1){
                result.add(task);

            }
        }
        return  result;
    }

    public List<Task> GetForTomorrow(){
        return null;
    }

    public List<Task> GetForNextWeek(){
        return null;
    }

    public void ChangeStatus(int id){

    }

    public void ChangeStatus(Task task){

    }
}
