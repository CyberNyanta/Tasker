package com.cybernyanta.core.database;

import com.cybernyanta.core.model.BaseModel;
import com.cybernyanta.core.model.Task;
import com.cybernyanta.core.util.DateUtil;
import com.google.firebase.database.ChildEventListener;
import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseError;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.GenericTypeIndicator;
import com.google.firebase.database.ValueEventListener;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Objects;

/**
 * Created by evgeniy.siyanko on 03.01.2017.
 */

public class TaskDatasource extends ArrayList<Task> implements Datasource<Task>, ChildEventListener {

    private DatabaseReference mDatabaseReference;
    private List<OnChangedListener> mListeners;

    public TaskDatasource(DatabaseReference ref) {
        super();
        mDatabaseReference = ref;
        mDatabaseReference.addChildEventListener(this);
        mListeners = new ArrayList<>();
//        Date date = DateUtil.addDays(new Date(), 1);
//        Task task= new Task("now","desc",date,date,false,0);
//
//        add((M)task);
    }

    public void cleanup() {
        mDatabaseReference.removeEventListener(this);
    }

    @Override
    public void onChildAdded(DataSnapshot snapshot, String previousChildKey) {
        Task model = snapshot.getValue(Task.class);
        model.setId(snapshot.getKey());
        super.add(model);
        notifyChangedListeners(OnChangedListener.EventType.ADDED, this.size() - 1);
    }

    @Override
    public void onChildChanged(DataSnapshot snapshot, String previousChildKey) {
        //GenericTypeIndicator<M> t = new GenericTypeIndicator<M>(){};
        Task model = snapshot.getValue(Task.class);
        model.setId(snapshot.getKey());
        int index = -1;
        for (int i = 0; i < this.size(); i++) {
            if (Objects.equals(this.get(i).getId(), model.getId())) {
                super.set(i, model);
                index = i;
                break;
            }
        }
        notifyChangedListeners(OnChangedListener.EventType.CHANGED, index);
    }

    @Override
    public void onChildRemoved(DataSnapshot snapshot) {
        final String key = snapshot.getKey();
        int index = -1;
        for (int i = 0; i < this.size(); i++) {
            if (Objects.equals(this.get(i).getId(), key)) {
                super.remove(i);
                index = i;
                break;
            }
        }
        notifyChangedListeners(OnChangedListener.EventType.REMOVED, index);
    }

    @Override
    public void onChildMoved(DataSnapshot snapshot, String previousChildKey) {
       /* int oldIndex = getIndexForKey(snapshot.getKey());
        mSnapshots.remove(oldIndex);
        int newIndex = previousChildKey == null ? 0 : (getIndexForKey(previousChildKey) + 1);
        mSnapshots.add(newIndex, snapshot);
        notifyChangedListeners(FirebaseArray.OnChangedListener.EventType.MOVED, newIndex, oldIndex);*/
    }

    @Override
    public void onCancelled(DatabaseError error) {
        notifyCancelledListeners(error);
    }

    @Deprecated
    @Override
    public Task set(int index, Task element) {
        String id = this.get(index).getId();
        mDatabaseReference.child(id).setValue(element);
        return element;
    }

    public void set(Task element) {
        mDatabaseReference.child(element.getId()).setValue(element);
    }

    public Task get(String id){
        for (int i = 0; i < this.size(); i++)
            if (Objects.equals(this.get(i).getId(), id))
                return this.get(i);
        return null;
    }
    @Override
    public boolean add(Task element) {
        mDatabaseReference.push().setValue(element);
        return true;
    }

    @Override
    public Task remove(int index) {
        mDatabaseReference.child(this.get(index).getId()).setValue(null);
        return null;
    }

    public void addOnChangedListener(OnChangedListener listener) {
        mListeners.add(listener);
    }

    protected void notifyChangedListeners(OnChangedListener.EventType type, int index) {
        notifyChangedListeners(type, index, -1);
    }

    protected void notifyChangedListeners(OnChangedListener.EventType type, int index, int oldIndex) {
        if (mListeners.size() != 0) {
            for(OnChangedListener listener:mListeners){
                listener.onChanged(type, index, oldIndex);
            }
        }
    }

    protected void notifyCancelledListeners(DatabaseError databaseError) {
        if (mListeners.size() != 0) {
            for(OnChangedListener listener:mListeners){
                listener.onCancelled(databaseError);
            }
        }
    }

}