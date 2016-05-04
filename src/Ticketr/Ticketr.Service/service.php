<?php 
	require_once("dbconnect.php");
	require_once("functions.php");
	
	global $dbHost;
	global $dbUser;
	global $dbPassword;
	global $dbName;
	
	//open db connection
	$db = mysqli_connect($dbHost, $dbUser, $dbPassword, $dbName);
	
	if (mysqli_connect_errno())
	{
		echo "Failed to connect to MySQL: " . mysqli_connect_error();
	}
	
	$allowedMethods = "getallkunden,getallmitarbeiter,getkundendetail,getmitarbeiterdetail,addperson";
	

?>