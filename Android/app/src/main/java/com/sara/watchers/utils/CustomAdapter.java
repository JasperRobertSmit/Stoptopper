package com.sara.watchers.utils;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;
import com.sara.watchers.MatchInfoActivity;
import com.sara.watchers.R;

import java.util.ArrayList;

public class CustomAdapter extends BaseAdapter{

    private static String token = "4004d869-f629-4acd-8360-2475d7270fce";
    private static Context context;
    private static ArrayList<Integer> teamids;
    private static ArrayList<String> teamnames;
    private static LayoutInflater inflater=null;

    public CustomAdapter(MatchInfoActivity mainActivity, ArrayList<Integer> teamid, ArrayList<String> teamname) {
        teamids=teamid;
        teamnames = teamname;
        context=mainActivity;
        inflater = (LayoutInflater)context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
    }
    @Override
    public int getCount() {
        return teamids.size();
    }

    @Override
    public Object getItem(int position) {
        return position;
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    public class Holder
    {
        TextView tv;
        ImageView img;
        ImageButton btn;
    }

    @Override
    public View getView(final int position, View convertView, ViewGroup parent) {
        Holder holder=new Holder();
        View rowView;
        rowView = inflater.inflate(R.layout.custom_list_item, null);
        holder.tv=(TextView) rowView.findViewById(R.id.textView1);
        holder.tv.setText(teamnames.get(position));
        holder.btn = (ImageButton)rowView.findViewById(R.id.trackTimeButton);
        holder.btn.setOnClickListener(new OnClickListener() {
            @Override
            public void onClick(View v) {
                // send the current team time
                int teamid =teamids.get(position);
                long TICKS_AT_EPOCH = 621355968000000000L;
                long tick = System.currentTimeMillis()*10000 + TICKS_AT_EPOCH;
                DataAccess.makeTimePostRequest(token, teamid+"", "1", tick);

                // show confirmation to user
                Context context2 = context.getApplicationContext();
                CharSequence text = "Time saved!";
                int duration = Toast.LENGTH_SHORT;
                Toast toast = Toast.makeText(context2, text, duration);
                toast.show();
            }
        });
        return rowView;
    }
}