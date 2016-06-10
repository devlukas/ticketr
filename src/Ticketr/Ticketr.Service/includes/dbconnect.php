<?php
	//Database Host
	$dbHost = "localhost";
	
	//MySQL-User
	$dbUser = "web322";
	
	//MySQL-Password
	$dbPassword = "RadioBob2015!";
	
	//Database Name
	$dbName = "usr_web322_6";

	//open db connection
	$db = mysqli_connect($dbHost, $dbUser, $dbPassword, $dbName);
	
	if (mysqli_connect_errno())
	{
		echo "Failed to connect to MySQL: " . mysqli_connect_error();
	}
	
	mysqli_set_charset($db,"utf8");

?>