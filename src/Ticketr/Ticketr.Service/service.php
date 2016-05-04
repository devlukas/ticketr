<?php 
	require_once("includes/dbconnect.php");
	require_once("includes/functions.php");
	
	$allowedFunctions = array(
		"getAllKunden",
		"getAllMitarbeiter",
		"getkundendetail",
		"getmitarbeiterdetail",
		"addMitarbeiter",
		"addKunde"
	);
	
	
	//Check if this method is allowed
	if(isset($_GET["method"]) && in_array($_GET["method"], $allowedFunctions))
	{
		//Call service method
		call_user_func($_GET["method"]);
	}
	else{
		header("HTTP/1.0 404 Not Found");
	}
	
	mysqli_close($db);
?>