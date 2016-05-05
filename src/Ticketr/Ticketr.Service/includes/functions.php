<?php	

	//Gibt alle verfügbaren Kunden zurück
	function getAllKunden()
	{
		global $db;
		
		$sql = "SELECT person.vorname, person.name, person.id as personId,
				person.eMail, person.erstellDatum, person.aenderungsDatum,
				kunde.id as kundeId, kunde.erstellDatum as kundeSeit
				FROM kunde
				INNER JOIN person ON person.id = kunde.personId";
				
		$result = query($sql);
		printQueryAsJson($result);
	}
	
	//Gibt alle verfügbaren Mitarbeiter zurück
	function getAllMitarbeiter()
	{
		global $db;
		
		$sql = "SELECT person.vorname, person.name, person.id as personId,
				person.eMail, person.erstellDatum, person.aenderungsDatum,
				mitarbeiter.id as mitarbeiterId, mitarbeiter.erstellDatum as mitarbeiterSeit
				FROM mitarbeiter
				INNER JOIN person ON person.id = mitarbeiter.personId";
				
		$result = query($sql);
		printQueryAsJson($result);
	}
	
	//Gibt die Daten für die Detailansicht eines Kunden zurück
	function getKundeDetail()
	{
		
	}
	
	//Gibt die Daten für die Detailansicht eines Benutzers zurück
	function getMitarbeiterDetail()
	{
		
	}
	
	//fügt einen neuen Mitarbeiter hinzu und gibt die erstellte Mitarbeiter-ID zurück
	function addMitarbeiter()
	{
		global $db;
		
		$name = realString($_POST["name"]);
		$vorname = realString($_POST["vorname"]);
		$email = realString($_POST["email"]);
		$passwort = realString($_POST["passwort"]);
		
		if(!empty($name) && !empty($vorname) && !empty($email) && !empty($passwort))
		{
			$pwHash = hash('sha256', $passwort);
			//add Person
			$sqlPerson = "INSERT INTO person (name, vorname, email, erstellDatum, aenderungsDatum) VALUES ('$name', '$vorname', '$email', NOW(), NOW())";
			$result = query($sqlPerson);
			
			//get id of new inserted person
			$personId = $db->insert_id;
			
			//add Mitabeiter
			$sqlMitabeiter = "INSERT INTO mitarbeiter (personId, passwort, erstellDatum) VALUES ($personId,'$pwHash', NOW())";
			$result = query($sqlMitabeiter);
			
			$json = "{ \"mitarbeiterId\":" . $db->insert_id . "}";
			
			echo $json;
			
			header('Content-Type: application/json');
		}
	}
	
	//Fügt einen neuen Kunden hinzu
	function addKunde()
	{
		global $db;
		
		$name = realString($_POST["name"]);
		$vorname = realString($_POST["vorname"]);
		$email = realString($_POST["email"]);
		
		if(!empty($name) && !empty($vorname) && !empty($email))
		{
			//add Person
			$sqlPerson = "INSERT INTO person (name, vorname, email, erstellDatum, aenderungsDatum) VALUES ('$name', '$vorname', '$email', NOW(), NOW())";
			$result = query($sqlPerson);
			
			//get id of new inserted person
			$personId = $db->insert_id;
			
			//add Mitabeiter
			$sqlKunde = "INSERT INTO kunde (personId, erstellDatum) VALUES ($personId, NOW())";
			$result = query($sqlKunde);
			
			$json = "{ \"kundeId\":" . $db->insert_id . "}";
			
			echo $json;
			
			header('Content-Type: application/json');
		}
	}
	
	
	//Schreibt das angegebene SQL-Result als JSON
	function printQueryAsJson($result)
	{
		$rows = array();
		
		while($r = mysqli_fetch_assoc($result)) 
		{
    		$rows[] = $r;
		}

		$json = json_encode($rows, JSON_UNESCAPED_UNICODE);
		echo $json;

		header('Content-Type: application/json');
	}
	
	//executes a query on the database
	function query($sql)
	{
		global $db;
		$result = $db->query($sql);
		
		if (!$result)
		{
			die('FAIL: ' . mysqli_error($db));
		}
		
		return $result;
	}
	
	function realString($value)
	{
		global $db;
		return $db->real_escape_string($value);
	}
	

?>