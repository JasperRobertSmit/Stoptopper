package com.sara.watchers;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.os.Bundle;

import com.huge.zxingscanner.OnDecodeCompletionListener;
import com.huge.zxingscanner.ScannerView;

public class QRActivity extends Activity implements OnDecodeCompletionListener {


    private ScannerView scannerView;
    private Activity _this;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_qr);
        _this = this;

        // set the scannerview
        scannerView = (ScannerView) findViewById(R.id.scanner_view);
        scannerView.setOnDecodeListener(this);
    }

    @Override
    public void onDecodeCompletion(String barcodeFormat, String barcode, Bitmap bitmap) {
        // check if the qr code is a url
        if(barcode != null && barcode != ""){
            // start the new intent
            Intent intent = new Intent(this, MatchInfoActivity.class);
            intent.putExtra(MainActivity.MATCH_INFO, barcode);
            startActivity(intent);
            finish();
        }
    }

    @Override
    protected void onResume() {
        super.onResume();
        scannerView.onResume();
    }

    @Override
    protected void onPause() {
        super.onPause();
        scannerView.onPause();
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
    }
}
