package com.cybernyanta.data.database;

import android.app.DownloadManager;

import com.cybernyanta.data.model.BaseModel;
import com.cybernyanta.data.model.Task;
import com.google.firebase.database.ChildEventListener;
import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseError;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;
import java.util.Objects;
import java.util.concurrent.Executor;

import io.reactivex.internal.schedulers.NewThreadScheduler;

/**
 * Created by evgeniy.siyanko on 03.01.2017.
 */

public class FirebaseArray<M extends BaseModel> extends ArrayList<M> implements ChildEventListener {


    public interface OnChangedListener {
        enum EventType {ADDED, CHANGED, REMOVED, MOVED}

        void onChanged(FirebaseArray.OnChangedListener.EventType type, int index, int oldIndex);

        void onCancelled(DatabaseError databaseError);
    }

    private DatabaseReference mDatabaseReference;
    private FirebaseArray.OnChangedListener mListener;

    public FirebaseArray(DatabaseReference ref) {
        super();
        mDatabaseReference = ref;
        mDatabaseReference.addChildEventListener(this);
    }

    public void cleanup() {
        mDatabaseReference.removeEventListener(this);
    }

    @Override
    public void onChildAdded(DataSnapshot snapshot, String previousChildKey) {
        M model = snapshot.getValue(new Class<M>());
        task.setId(snapshot.getKey());
        this.add(task);
        notifyChangedListeners(FirebaseArray.OnChangedListener.EventType.ADDED, this.size() - 1);
    }

    @Override
    public void onChildChanged(DataSnapshot snapshot, String previousChildKey) {
        Task task = snapshot.getValue(Task.class);
        task.setId(snapshot.getKey());
        this.add(task);
        int index = -1;
        for (int i = 0; i < this.size(); i++) {
            if (Objects.equals(this.get(i).getId(), task.getId())) {
                this.set(i, task);
                index = i;
                break;
            }
        }
        notifyChangedListeners(FirebaseArray.OnChangedListener.EventType.CHANGED, index);
    }

    @Override
    public void onChildRemoved(DataSnapshot snapshot) {
        final String key = snapshot.getKey();
        int index = -1;
        for (int i = 0; i < this.size(); i++) {
            if (Objects.equals(this.get(i).getId(), key)) {
                this.remove(i);
                index = i;
                break;
            }
        }
        notifyChangedListeners(FirebaseArray.OnChangedListener.EventType.REMOVED, index);
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

    @Override
    public Task set(int index, Task element) {
        String id = this.get(index).getId();
        mDatabaseReference.child(id).setValue(element);
        element.setId(id);
        return element;
    }

    @Override
    public boolean add(Task task) {
        mDatabaseReference.push().setValue(task);
        return true;
    }

    @Override
    public Task remove(int index) {
        mDatabaseReference.child(this.get(index).getId()).setValue(null);
        return null;
    }

    public void setOnChangedListener(FirebaseArray.OnChangedListener listener) {
        mListener = listener;
    }

    protected void notifyChangedListeners(FirebaseArray.OnChangedListener.EventType type, int index) {
        notifyChangedListeners(type, index, -1);
    }

    protected void notifyChangedListeners(FirebaseArray.OnChangedListener.EventType type, int index, int oldIndex) {
        if (mListener != null) {
            mListener.onChanged(type, index, oldIndex);
        }
    }

    protected void notifyCancelledListeners(DatabaseError databaseError) {
        if (mListener != null) {
            mListener.onCancelled(databaseError);
        }
    }

}