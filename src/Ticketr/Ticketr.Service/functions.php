<?php
	global $db;
	
	function GetAllKunden()
	{
		$sql = "SELECT person.vorname, person.name, person.id as personId,
				person.eMail, person.erstellDatum, person.aenderungsDatum,
				kunde.id as kundeId, kunde.erstellDatum as kundeSeit
				FROM kunde
				INNER JOIN person ON person.id = kunde.personId";
				
		$db->query($sql);
	}
	
	function GetAllMitarbeiter()
	{
		
	}
	
	function GetKundeDetail(){
		
	}
	
	function GetMitarbeiterDetail(){
		
	}
	
	function AddPerson(){
		
	}

?>