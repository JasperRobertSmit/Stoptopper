package com.sara.watchers;

import android.os.Bundle;
import android.os.StrictMode;
import android.support.v7.app.AppCompatActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.WindowManager;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.TextView;
import com.sara.watchers.utils.CustomAdapter;
import com.sara.watchers.utils.DataAccess;
import org.json.JSONArray;
import org.json.JSONObject;
import java.util.ArrayList;

public class MatchInfoActivity extends AppCompatActivity {
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_match_info);

        //het volgende (ThreadPolicy) is een tijdelijke oplossing
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        StrictMode.setThreadPolicy(policy);

        // set the custom actionbar
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,WindowManager.LayoutParams.FLAG_FULLSCREEN);
        android.support.v7.app.ActionBar actionBar = getSupportActionBar();
        actionBar.setDisplayShowHomeEnabled(false);
        actionBar.setDisplayShowTitleEnabled(false);
        LayoutInflater mInflater = LayoutInflater.from(this);
        View mCustomView = mInflater.inflate(R.layout.custom_actionbar, null);
        TextView mTitleTextView = (TextView) mCustomView.findViewById(R.id.title_text);
        mTitleTextView.setText("Stoptoppers");
        actionBar.setCustomView(mCustomView);
        actionBar.setDisplayShowCustomEnabled(true);

        // get the scanned code
        String titelString = "";
        int eventid;
        String receivedString = getIntent().getStringExtra(MainActivity.MATCH_INFO);

        //TEST overwrite
        //receivedString = "TST";

        try {
            // get event
            String dataString = DataAccess.makeGetRequest("event/all/guid/" + receivedString + "/4004d869-f629-4acd-8360-2475d7270fce");
            JSONObject data = new JSONObject(dataString);
            titelString = data.getString("Naam") + " " + data.getString("StartDatum");
            eventid = data.getInt("Id");
            titelString = titelString + "       " + eventid;

            // get blokken
            dataString = DataAccess.makeGetRequest("blok/all/event/" + eventid + "/4004d869-f629-4acd-8360-2475d7270fce");
            JSONArray dataArray = new JSONArray(dataString);

            String blokTitelString = "";
            int blokId = 0;
            ArrayList<Integer> blokIds = new ArrayList<Integer>();

            for(int i = 0; i < dataArray.length(); i++) {
                JSONObject dataObject = dataArray.getJSONObject(i);
                blokId = dataObject.getInt("Id");
                blokTitelString += blokId + ";";
                blokIds.add(blokId);
            }

            // get velden
            String veldTitelString = "";
            int veldId = 0;
            ArrayList<Integer> veldIds = new ArrayList<Integer>();

            for(int blokid : blokIds){
                dataString = DataAccess.makeGetRequest("veld/all/blok/" + blokid + "/4004d869-f629-4acd-8360-2475d7270fce");
                JSONArray dataVeldArray = new JSONArray(dataString);

                for(int i = 0; i < dataVeldArray.length(); i++){
                    JSONObject dataObject = dataArray.getJSONObject(i);
                    veldId = dataObject.getInt("Id");
                    veldTitelString += veldId + ";";
                    veldIds.add(blokId);
                }
            }

            // get ploegen
            String ploegTitelString = "";
            int ploegId = 0;
            String ploegNaam = "";
            ArrayList<Integer> ploegIds = new ArrayList<Integer>();
            ArrayList<String> ploegNamen = new ArrayList<String>();

            for(int veldid : veldIds){
                dataString = DataAccess.makeGetRequest("ploeg/all/veld/" + veldid + "/4004d869-f629-4acd-8360-2475d7270fce");
                JSONArray dataVeldArray = new JSONArray(dataString);

                for(int i = 0; i < dataVeldArray.length(); i++){
                    JSONObject dataObject = dataVeldArray.getJSONObject(i);
                    ploegId = dataObject.getInt("Id");
                    ploegNaam = dataObject.getString("Rugnummer") + " - " + dataObject.getString("Naam");
                    ploegTitelString += ploegId + ";";
                    ploegIds.add(ploegId);
                    ploegNamen.add(ploegNaam);
                }
            }

            // fill listview
            final ListView listView = (ListView) findViewById(R.id.lv_ploegen);
            ArrayAdapter<String> adapter = new ArrayAdapter<String>(this,android.R.layout.simple_list_item_1, android.R.id.text1, ploegNamen);

            // Assign adapter to ListView
            listView.setAdapter(new CustomAdapter(this, ploegIds,ploegNamen));
        }
        catch (Exception e) {
            String s = "";
        }
    }
}
