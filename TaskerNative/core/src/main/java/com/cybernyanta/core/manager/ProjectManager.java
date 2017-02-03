package com.cybernyanta.core.manager;

import com.cybernyanta.core.database.Datasource;
import com.cybernyanta.core.database.OnChangedListener;
import com.cybernyanta.core.manager.contract.ProjectManagerContract;
import com.cybernyanta.core.model.Project;
import com.cybernyanta.core.model.Task;

import java.util.ArrayList;
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
