package com.sara.watchers.utils;

import org.json.JSONObject;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLConnection;
import java.net.URLEncoder;

/**
 * Created by Mathijs on 3-7-2016.
 */

public class DataAccess {
    public static String makeGetRequest(String info) {
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

    public static String makeTimePostRequest(String token, String team, String blok, long time){
        try {
            URLConnection connection = new URL("http://87.253.157.240/SaveTijd").openConnection();
            connection.setDoOutput(true);

            String content =
                    "time=" + URLEncoder.encode(time+"") +
                            "&ploegid=" + URLEncoder.encode(team) +
                            "&blokid=" + URLEncoder.encode(blok) +
                            "&token=" + URLEncoder.encode(token);

            connection.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");
            connection.setRequestProperty("Content-Length", content.getBytes().length+"");

            OutputStream output = connection.getOutputStream();
            output.write(content.getBytes());
            output.close();

            BufferedReader r = new BufferedReader(new InputStreamReader(connection.getInputStream()));
            StringBuilder total = new StringBuilder();
            String line;
            while ((line = r.readLine()) != null) {
                total.append(line).append('\n');
            }
            return line;
        }
        catch(Exception ex){
            ex.printStackTrace();
        }
        return "";
    }
}
