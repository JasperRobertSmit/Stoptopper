<?php

error_reporting(E_ALL);
ini_set('display_errors', '1');

	class User{


		private $sEmail;
		private $sUsername;
		private $sPassword;

		function User($sEmail, $sUsername, $sPassword){

			$this->sEmail = $sEmail;
			$this->sUsername = $sUsername;
			$this->sPassword = password_hash($sPassword, PASSWORD_BCRYPT);

		}


		function Compare($sNewPassword){

			//TODO: PW ophalen uit DB en vergelijken


			if(password_verify($sNewPassword, $this->sPassword)){
				$password_matches = true;
			}else{
				$password_matches = false;
			}


			$aResponse = array("password_matches" => $password_matches);

			echo json_encode($aResponse);

		}

	}

    if(isset($_POST['username']) && isset($_POST['password'])){
        $username = $_POST['username'];
        $password = $_POST['password'];
    }

    if(isset($_POST['email'])){
        $email = $_POST['email'];
    }

    $test = new User($email, $username, $password);


	$test->Compare("admin");


