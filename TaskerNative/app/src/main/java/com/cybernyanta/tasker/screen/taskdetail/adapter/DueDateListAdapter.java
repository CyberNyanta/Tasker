package com.cybernyanta.tasker.screen.taskdetail.adapter;

import android.app.Activity;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.cybernyanta.tasker.R;
import com.cybernyanta.tasker.data.util.DateUtil;
import com.cybernyanta.tasker.enums.TaskDueDate;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

/**
 * Created by evgeniy.siyanko on 09.02.2017.
 */

public class DueDateListAdapter extends BaseAdapter{

    private final Activity context;
    private final List<TaskDueDate> dates;
    private final long current;
    private final TaskDueDate currentType;
    private final View.OnClickListener onClickListener;

    public DueDateListAdapter(Activity context,long current, TaskDueDate currentType, View.OnClickListener onClickListener) {
        this.context = context;
        this.dates = Arrays.asList(TaskDueDate.values());
        this.current = current;
        this.currentType = currentType;
        this.onClickListener = onClickListener;

    }
    //  private event EventHandler OnClick;
    @Override
    public int getCount() {
        return dates.size();
    }

    @Override
    public Object getItem(int position) {
        return dates.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        TaskDueDate item = dates.get(position);
        View view;

        view = context.getLayoutInflater().inflate(R.layout.date_list_item, null);

        TextView dateName =(TextView) view.findViewById(R.id.date_name);

        if (item == currentType)
        {
            switch (item)
            {
                case CUSTOM:
                case TODAY:
                case TOMORROW:
                    view.setBackgroundResource(R.color.item_selected);
                    dateName.setText(DateUtil.dateToString(current,true));
                    break;
                case REMOVED:
                    dateName.setText(context.getString(R.string.due_dates_remove));
                    break;
            }
        }
        else
        {
            switch (item)
            {
                case CUSTOM:
                    dateName.setText(context.getString(R.string.due_dates_pick));
                    break;
                case TODAY:
                    dateName.setText(context.getString(R.string.due_dates_today));
                    break;
                case TOMORROW:
                    dateName.setText(context.getString(R.string.due_dates_tomorrow));
                    break;
                case NEXT_WEEK:
                    dateName.setText(context.getString(R.string.due_dates_nextWeek));
                    break;
                case REMOVED:
                    dateName.setText(context.getString(R.string.due_dates_remove));
                    break;
            }
        }
        view.setTag(item);
        view.setOnClickListener(onClickListener);
        return view;
        
    }
    
}
