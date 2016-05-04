<?php
	//Database Host
	$dbHost = "localhost";
	
	//MySQL-User
	$dbUser = "root";
	
	//MySQL-Password
	$dbPassword = "";
	
	//Database Name
	$dbName = "ticketr";

	//open db connection
	$db = mysqli_connect($dbHost, $dbUser, $dbPassword, $dbName);
	
	if (mysqli_connect_errno())
	{
		echo "Failed to connect to MySQL: " . mysqli_connect_error();
	}
	
	mysqli_set_charset($db,"utf8");

?>