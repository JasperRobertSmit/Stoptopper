package com.sara.watchers;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;

public class MatchInfoActivity extends AppCompatActivity {
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_match_info);

        String titelString = "";
        int eventid;
        String receivedString = getIntent().getStringExtra(MainActivity.MATCH_INFO);


        try {
            // get event
            String dataString = makeRequest("event/all/guid/" + receivedString + "/4004d869-f629-4acd-8360-2475d7270fce");
            JSONObject data = new JSONObject(dataString);
            titelString = data.getString("Naam") + " " + data.getString("StartDatum");
            eventid = data.getInt("Id");
            titelString = titelString + "       " + eventid;

            TextView eventinfo = (TextView) findViewById(R.id.tv_eventinfo);
            eventinfo.setText(titelString);

            // get blokken
            dataString = makeRequest("blok/all/event/" + eventid + "/4004d869-f629-4acd-8360-2475d7270fce");
            JSONArray dataArray = new JSONArray(dataString);

            String blokTitelString = "";
            int blokId = 0;
            ArrayList<Integer> blokIds = new ArrayList<Integer>();

            for(int i = 0; i < dataArray.length(); i++){
                JSONObject dataObject = dataArray.getJSONObject(i);
                blokId = dataObject.getInt("Id");
                blokTitelString += blokId + ";";
                blokIds.add(blokId);
            }

            //blokid tonen
            //TextView blokinfo = (TextView) findViewById(R.id.tv_blokinfo);
            //blokinfo.setText(blokTitelString);

            // get velden
            String veldTitelString = "";
            int veldId = 0;
            ArrayList<Integer> veldIds = new ArrayList<Integer>();

            for(int blokid : blokIds){
                dataString = makeRequest("veld/all/blok/" + blokid + "/4004d869-f629-4acd-8360-2475d7270fce");
                JSONArray dataVeldArray = new JSONArray(dataString);

                for(int i = 0; i < dataVeldArray.length(); i++){
                    JSONObject dataObject = dataArray.getJSONObject(i);
                    veldId = dataObject.getInt("Id");
                    veldTitelString += veldId + ";";
                    veldIds.add(blokId);
                }
            }

            //veldid tonen
            //TextView veldinfo = (TextView) findViewById(R.id.tv_veldinfo);
            //veldinfo.setText(veldTitelString);

            // get ploegen
            String ploegTitelString = "";
            int ploegId = 0;
            String ploegNaam = "";
            ArrayList<Integer> ploegIds = new ArrayList<Integer>();
            ArrayList<String> ploegNamen = new ArrayList<String>();

            for(int veldid : veldIds){
                dataString = makeRequest("ploeg/all/veld/" + veldid + "/4004d869-f629-4acd-8360-2475d7270fce");
                JSONArray dataVeldArray = new JSONArray(dataString);

                for(int i = 0; i < dataVeldArray.length(); i++){
                    JSONObject dataObject = dataVeldArray.getJSONObject(i);
                    ploegId = dataObject.getInt("Id");
                    ploegNaam = dataObject.getString("Naam");
                    ploegTitelString += ploegId + ";";
                    ploegIds.add(ploegId);
                    ploegNamen.add(ploegNaam);
                }
            }

            //ploegid tonen
            //TextView ploeginfo = (TextView) findViewById(R.id.tv_ploeginfo);
            //ploeginfo.setText(ploegTitelString);

            // fill listview
            final ListView listView = (ListView) findViewById(R.id.lv_ploegen);
            ArrayAdapter<String> adapter = new ArrayAdapter<String>(this,android.R.layout.simple_list_item_1, android.R.id.text1, ploegNamen);

            // Assign adapter to ListView
            listView.setAdapter(adapter);

            // ListView Item Click Listener
            listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {

                @Override
                public void onItemClick(AdapterView<?> parent, View view,int position, long id) {

                    // ListView Clicked item index
                    int itemPosition     = position;

                    // ListView Clicked item value
                    String  itemValue    = (String) listView.getItemAtPosition(position);

                    // Show Alert
                    Toast.makeText(getApplicationContext(),
                            "Position :"+itemPosition+"  ListItem : " +itemValue , Toast.LENGTH_LONG)
                            .show();

                }
            });
        }
        catch (Exception e) {
            String s = "";
        }
    }

    private String makeRequest(String info) {

        HttpURLConnection connection = null;
        BufferedReader reader = null; //deze is hier aangemaakt zodat bereikbaar vanuit finally-tak
        String data = null;

        try {
            URL url = new URL("http://87.253.157.240/"+info);
            connection = (HttpURLConnection) url.openConnection();
            connection.connect();

            InputStream stream = connection.getInputStream();

            reader = new BufferedReader(new InputStreamReader(stream));

            StringBuilder sb = new StringBuilder();
            String line = "";
            while ((line = reader.readLine()) != null) {
                sb.append(line);
            }

            String finalJson = sb.toString();
            JSONObject parentObject = new JSONObject(finalJson);
            //uit deze string (Data) kan een JSON-object of een array komen
            //in JSON omzetten doe je pas in MatchInfoActivity
            data = parentObject.getString("Data");

        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            if (connection != null) { //zonder deze zou reader.close NullPointerException kunnen krijgen
                connection.disconnect();
            }
            try {
                if (reader != null) {
                    reader.close();
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
        return data;
    }
}
