package com.cybernyanta.tasker.data.manager;

import com.cybernyanta.tasker.data.database.Datasource;
import com.cybernyanta.tasker.data.database.OnChangedListener;
import com.cybernyanta.tasker.data.manager.contract.ProjectManagerContract;
import com.cybernyanta.tasker.data.model.Project;
import com.cybernyanta.tasker.data.model.Task;

import java.util.List;

/**
 * Created by evgeniy.siyanko on 03.02.2017.
 */

public class ProjectManager implements ProjectManagerContract {

    private Datasource<Project> projectDatasource;

    public ProjectManager(Datasource<Project> projectDatasource) {
        this.projectDatasource = projectDatasource;
    }

    public void addOnChangedListener(OnChangedListener listener) {
        projectDatasource.addOnChangedListener(listener);
    }

    public void cleanup() {
        projectDatasource.cleanup();
    }

    @Override
    public List<Project> getAll() {
        return projectDatasource;
    }

    @Override
    public void addProject(Project project) {
        projectDatasource.add(project);
    }

    @Override
    public void setProject(Project project) {
        projectDatasource.set(project);
    }

    @Override
    public void deleteProject(String id) {
        projectDatasource.remove(id);
    }

    @Override
    public Project getProject(String id) {
        return  projectDatasource.get(id);
    }
}
