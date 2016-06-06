<?php
/**
 * Created by PhpStorm.
 * User: Jasper
 * Date: 27-5-2016
 * Time: 13:50
 */

$username = $_POST['username'];
$password = $_POST['password'];

function CheckTheCredentials($un, $pw){
    $aData = array("username" => $un, "password" => $pw);
    $aResponse = array("match_found" => true, "data" => $aData);
    return json_encode($aResponse);
}

CheckTheCredentials($username, $password);