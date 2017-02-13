package com.cybernyanta.tasker.screen.tasks.recycler;

import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.TextView;

import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.data.model.Task;

import butterknife.BindView;
import butterknife.ButterKnife;

import static android.view.View.GONE;
import static android.view.View.VISIBLE;

/**
 * Created by evgeniy.siyanko on 10.02.2017.
 */

public class TaskCategoryViewHolder extends RecyclerView.ViewHolder {

    @BindView(R.id.header)
    TextView headerTextView;
    @BindView(R.id.header_date)
    TextView headerDateTextView;

    public TaskCategoryViewHolder(View itemView) {
        super(itemView);
        ButterKnife.bind(this, itemView);
    }

    public void bind(@NonNull Task task){
        headerTextView.setText(task.getId());
        headerDateTextView.setText(task.getTitle());
    }

    public void hide(){
        itemView.setVisibility(GONE);
        headerTextView.setVisibility(GONE);
        headerDateTextView.setVisibility(GONE);
    }

    public void show(){
        itemView.setVisibility(VISIBLE);
        headerTextView.setVisibility(VISIBLE);
        headerDateTextView.setVisibility(VISIBLE);
    }
}
