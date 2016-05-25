<?php 
    class TicketrRepository{
        
        function getAllKunden() {

            $sql = "SELECT person.vorname, person.name, person.id as personId,
                    person.eMail, person.erstellDatum, person.aenderungsDatum,
                    kunde.id as kundeId, kunde.erstellDatum as kundeSeit
                    FROM kunde
                    INNER JOIN person ON person.id = kunde.person_id";

            $result = $this->query($sql);
            
            $response = array();
            
            while ($row = mysqli_fetch_array($result)) {
                $kunde = array();
                
                $kunde["id"] = $row["kundeId"];
                $kunde["erstellDatum"] = $row["kundeSeit"];
                $kunde["person"]["id"] = $row["personId"];
                $kunde["person"]["name"] = $row["name"];
                $kunde["person"]["vorname"] = $row["vorname"];
                $kunde["person"]["email"] = $row["eMail"];
                $kunde["person"]["erstellDatum"] = $row["erstellDatum"];
                $kunde["person"]["aenderungsDatum"] = $row["aenderungsDatum"];
                
                array_push($response,  $kunde);
            }
            
            return $response;
            
        }

        //Gibt alle verfügbaren Mitarbeiter zurück
        function getAllMitarbeiter() {

            $sql = "SELECT person.vorname, person.name, person.id as personId,
                    person.eMail, person.erstellDatum, person.aenderungsDatum,
                    mitarbeiter.id as mitarbeiterId, mitarbeiter.erstellDatum as mitarbeiterSeit
                    FROM mitarbeiter
                    INNER JOIN person ON person.id = mitarbeiter.person_id";

            $result = $this->query($sql);
            
            $response = array();
            
            while ($row = mysqli_fetch_array($result)) {
                $mitarbeiter = array();
                
                $mitarbeiter["id"] = $row["mitarbeiterId"];
                $mitarbeiter["erstellDatum"] = $row["mitarbeiterSeit"];
                $mitarbeiter["person"]["id"] = $row["personId"];
                $mitarbeiter["person"]["name"] = $row["name"];
                $mitarbeiter["person"]["vorname"] = $row["vorname"];
                $mitarbeiter["person"]["email"] = $row["eMail"];
                $mitarbeiter["person"]["erstellDatum"] = $row["erstellDatum"];
                $mitarbeiter["person"]["aenderungsDatum"] = $row["aenderungsDatum"];
                
                array_push($response,  $mitarbeiter);
            }
            
            return $response;
        }

        //Gibt die Daten für die Detailansicht eines Kunden zurück
        function getKundeDetail() {

        }

        //Gibt die Daten für die Detailansicht eines Benutzers zurück
        function getMitarbeiterDetail($id) {
            
            $sql = "SELECT 
                        mitarbeiter.id, mitarbeiter.erstellDatum, person.name, person.vorname, person.id as personId, person.eMail, 
                        person.erstellDatum as personErstelldatum, person.aenderungsDatum as personAenderungsdatum, COUNT(ticket.id) as ticketCount
                    FROM mitarbeiter
                    INNER JOIN person ON mitarbeiter.person_id = person.id
                    INNER JOIN ticket ON mitarbeiter.id = ticket.bearbeiter_id
                    WHERE mitarbeiter.id = $id";
                    
            $result = $this->query($sql);
            $row =mysqli_fetch_assoc($result);
                
            $ticket = array();
            
            $ticket["id"] = $row["id"];
            $ticket["ticketsCount"] = $row["ticketCount"];
            $ticket["erstellDatum"] = $row["erstellDatum"];
            $ticket["person"]["id"] = $row["personId"];
            $ticket["person"]["name"] = $row["name"];
            $ticket["person"]["vorname"] = $row["vorname"];
            $ticket["person"]["eMail"] = $row["eMail"];
            $ticket["person"]["erstellDatum"] = $row["personErstelldatum"];
            $ticket["person"]["aenderungsDatum"] = $row["personAenderungsdatum"];
            
            return $ticket;
        }

        //fügt einen neuen Mitarbeiter hinzu und gibt die erstellte Mitarbeiter-ID zurück
        function addMitarbeiter($data) {

            global $db;
            
            $name = $this->realString($data["person"]["name"]);
            $vorname = $this->realString($data["person"]["vorname"]);
            $email = $this->realString($data["person"]["email"]);
            $passwort = $this->realString($data["passwort"]);

            if (!empty($name) && !empty($vorname) && !empty($email) && !empty($passwort)) {
                $pwHash = hash('sha256', $passwort);
                //add Person
                $sqlPerson = "INSERT INTO person (name, vorname, email, erstellDatum, aenderungsDatum) VALUES ('$name', '$vorname', '$email', NOW(), NOW())";
                $result = $this->query($sqlPerson);

                //get id of new inserted person
                $personId = $db->insert_id;

                //add Mitabeiter
                $sqlMitabeiter = "INSERT INTO mitarbeiter (person_id, passwort, erstellDatum) VALUES ($personId,'$pwHash', NOW())";
                $result = $this->query($sqlMitabeiter);

                $json = "{ \"mitarbeiterId\":".$db->insert_id."}";

                header('Content-Type: application/json');
                
                echo $json;
            }
        }
        
        //Gibt das Profilbild einer Person zurück
        function getPersonPicture($id)
        {
            $sql = "SELECT picture FROM person WHERE id = " . $id;
            
            $result = $this->query($sql);
            $row = mysqli_fetch_assoc($result);
            
            //todo hack
            header('Content-Type: image/png');
            
            return $row["picture"];
        }
        
        function setPersonPicture($image, $personId)
        {
            $sql = "UPDATE person SET picture = '$image' WHERE id = $personId";
            return $this->query($sql);
        }
        
        //löscht einen Mitarbeiter
        function deleteMitarbeiter()
        { 
            $mitarbeiterId = $_POST["id"];
            $sql = "DELETE FROM mitarbeiter WHERE id = $mitarbeiterId";
        }

        //Fügt einen neuen Kunden hinzu
        function addKunde($data) {
            global $db;

            //Gets the JSON in the post-Body
            $data = json_decode(file_get_contents('php://input'), true);

            $name = $this->realString($data["person"]["name"]);
            $vorname = $this->realString($data["person"]["vorname"]);
            $email = $this->realString($data["person"]["email"]);

            if (!empty($name) && !empty($vorname) && !empty($email)) {
                //add Person
                $sqlPerson = "INSERT INTO person (name, vorname, email, erstellDatum, aenderungsDatum) VALUES ('$name', '$vorname', '$email', NOW(), NOW())";
                $result = $this->query($sqlPerson);

                //get id of new inserted person
                $personId = $db->insert_id;

                //add Mitabeiter
                $sqlKunde = "INSERT INTO kunde (person_id, erstellDatum) VALUES ($personId, NOW())";
                $result = $this->query($sqlKunde);

                $json = "{ \"kundeId\":".$db->insert_id."}";
                
                return $json;
            }
        }
        
        //Gibt alle Tickets zurück
        function getTickets(){
            
            $sql = "
                  SELECT 
                    ticket.id, ticket.erstellDatum, ticket.aenderungsDatum, ticket.kategorie_id, kategorie.name as kategorieName,
                    ticket.bezeichnung, ticket.beschreibung, ticket.abgeschlossen, ticket.prioritaet,
                    ticket.bearbeiter_id as bearbeiterMitarbeiterId, bearbeiterP.id as bearbeiterPersonId, bearbeiterP.name as bearbeiterName,  bearbeiterP.vorname as bearbeiterVorname, 
                    ticket.kunde_id as kundeId, kundeP.id as kundePersonId, kundeP.name as kundeName, kundeP.vorname as kundeVorname,
                    COUNT(kommentar.id) as 'kommentarCount'
                FROM ticket
                INNER JOIN kategorie ON kategorie.id = ticket.kategorie_id
                LEFT JOIN kommentar ON kommentar.ticket_id = ticket.id
                INNER JOIN mitarbeiter bearbeiterM ON bearbeiterM.id = ticket.bearbeiter_id
                INNER JOIN person bearbeiterP ON bearbeiterP.id = bearbeiterM.person_id
                INNER JOIN kunde kundeK ON kundeK.id = ticket.kunde_id
                INNER JOIN person kundeP ON kundeP.id = kundeK.person_id
                GROUP BY ticket.id";
                
                
                $result = $this->query($sql);
                
                $response = array();
                
                while ($row = mysqli_fetch_array($result)) {
                    
                    $ticket = array();
                    
                    $ticket["id"] = $row["id"];
                    $ticket["erstellDatum"] = $row["erstellDatum"];
                    $ticket["aenderungsDatum"] = $row["aenderungsDatum"];
                    $ticket["bezeichnung"] = $row["bezeichnung"];
                    $ticket["beschreibung"] = $row["beschreibung"];
                    $ticket["abgeschlossen"] = ($row["abgeschlossen"] == "1" ? true : false);
                    $ticket["kategorie"]["id"] = $row["kategorie_id"];
                    $ticket["kategorie"]["name"] = $row["kategorieName"];
                    $ticket["prioritaet"] = $row["prioritaet"];
                    $ticket["kommentarCount"] = $row["kommentarCount"];
                    
                    $ticket["bearbeiter"]["id"] = $row["bearbeiterMitarbeiterId"];
                    $ticket["bearbeiter"]["person"]["id"] = $row["bearbeiterPersonId"];
                    $ticket["bearbeiter"]["person"]["vorname"] = $row["bearbeiterVorname"];
                    $ticket["bearbeiter"]["person"]["name"] = $row["bearbeiterName"];
                    
                    $ticket["kunde"]["id"] = $row["kundeId"];
                    $ticket["kunde"]["person"]["id"] = $row["kundePersonId"];
                    $ticket["kunde"]["person"]["vorname"] = $row["kundeVorname"];
                    $ticket["kunde"]["person"]["name"] = $row["kundeName"];

                    // push single product into final response array
                    array_push($response,  $ticket);
                }
                
            return $response;
        }
        
        
        //Gibt die Detailansicht eines Tickets zurück
        function getTicketDetail(){
            
            $id = $_GET["id"];
            
            //------------Load Ticket---------
            
            $ticketSql = "
                SELECT 
                    ticket.id, ticket.erstellDatum, ticket.aenderungsDatum, ticket.kategorie_id, kategorie.name as kategorieName,
                    ticket.bezeichnung, ticket.beschreibung, ticket.abgeschlossen, ticket.prioritaet,
                    ticket.bearbeiter_id as bearbeiterMitarbeiterId, bearbeiterP.id as bearbeiterPersonId, bearbeiterP.name as bearbeiterName,  bearbeiterP.vorname as bearbeiterVorname, 
                    ticket.kunde_id as kundeId, kundeP.id as kundePersonId, kundeP.name as kundeName, kundeP.vorname as kundeVorname
                FROM ticket
                INNER JOIN kategorie ON kategorie.id = ticket.kategorie_id
                INNER JOIN mitarbeiter bearbeiterM ON bearbeiterM.id = ticket.bearbeiter_id
                INNER JOIN person bearbeiterP ON bearbeiterP.id = bearbeiterM.person_id
                INNER JOIN kunde kundeK ON kundeK.id = ticket.kunde_id
                INNER JOIN person kundeP ON kundeP.id = kundeK.person_id
                WHERE ticket.id = $id";
                
            $ticketResult = $this->query($ticketSql);
            $ticketRow = mysqli_fetch_assoc($ticketResult);
            
            //-------Load Comments---------------
            
            $commentsSql = "
                SELECT 
                kommentar.id, kommentar.text, kommentar.erstellDatum, mitarbeiter.id as mitarbeiterId, 
                person.name as personName, person.vorname as personVorname, person.id as personId 
                FROM kommentar
                INNER JOIN mitarbeiter ON kommentar.bearbeiter_id = mitarbeiter.id
                INNER JOIN person ON mitarbeiter.person_id = person.id
                WHERE kommentar.id = $id";
            
            $commentResult = $this->query($commentsSql);
            
            
            //---Create JSON-Object----------

            $ticket = array();
            
            $ticket["id"] = $ticketRow["id"];
            $ticket["erstellDatum"] = $ticketRow["erstellDatum"];
            $ticket["aenderungsDatum"] = $ticketRow["aenderungsDatum"];
            $ticket["bezeichnung"] = $ticketRow["bezeichnung"];
            $ticket["beschreibung"] = $ticketRow["beschreibung"];
            $ticket["abgeschlossen"] = ($row["abgeschlossen"] == "1" ? true : false);
            $ticket["prioritaet"] = $ticketRow["prioritaet"];
            $ticket["kategorie"]["id"] = $ticketRow["kategorie_id"];
            $ticket["kategorie"]["name"] = $ticketRow["kategorieName"];
            
            $ticket["bearbeiter"]["id"] = $ticketRow["bearbeiterMitarbeiterId"];
            $ticket["bearbeiter"]["person"]["id"] = $ticketRow["bearbeiterPersonId"];
            $ticket["bearbeiter"]["person"]["vorname"] = $ticketRow["bearbeiterVorname"];
            $ticket["bearbeiter"]["person"]["name"] = $ticketRow["bearbeiterName"];
            
            $ticket["kunde"]["id"] = $ticketRow["kundeId"];
            $ticket["kunde"]["person"]["id"] = $ticketRow["kundePersonId"];
            $ticket["kunde"]["person"]["vorname"] = $ticketRow["kundeVorname"];
            $ticket["kunde"]["person"]["name"] = $ticketRow["kundeName"];
            
            $ticket["kommentare"] = array();
            
            while ($commentRow = mysqli_fetch_array($commentResult)) 
            {
                $comment = array();
                
                $comment["id"] = $commentRow["id"];
                $comment["text"] = $commentRow["text"];
                $comment["erstellDatum"] = $commentRow["erstellDatum"];
                $comment["mitarbeiter"]["id"] = $commentRow["mitarbeiterId"];
                $comment["mitarbeiter"]["person"]["id"] = $commentRow["personId"];
                $comment["mitarbeiter"]["person"]["name"] = $commentRow["personName"];
                $comment["mitarbeiter"]["person"]["vorname"] = $commentRow["personVorname"];
                
                array_push($ticket["kommentare"],  $comment);
            }
                
            return $ticket;
        }
        
        //Erstellt ein neues Ticket
        function createTicket($ticket)
        {
            global $db;
            
            $bezeichnung = $ticket["bezeichnung"];
            $beschreibung = $ticket["beschreibung"];
            $kundeId = $ticket["kunde"]["id"];
            $bearbeiterId = $ticket["bearbeiter"]["id"];
            $prioritaet = $ticket["prioritaet"];
            $kategorieId = $ticket["kategorie"]["id"];
            
            $sql = "INSERT INTO ticket 
                    (bezeichnung, beschreibung, kunde_id, bearbeiter_id, abgeschlossen, prioritaet, kategorie_id, erstellDatum, aenderungsDatum)
                    VALUES ('$bezeichnung', '$beschreibung', $kundeId, $bearbeiterId, 0, $prioritaet ,$kategorieId, NOW(), NOW())";
                       
            $this->query($sql);
            
            $json = "{ \"ticketId\":".$db->insert_id."}";
            
            return $json;
        }
        
        //Updatet ein Ticket
        function udpateTicket($ticket)
        {
            $id = $ticket["id"];
            $bezeichnung = $ticket["bezeichnung"];
            $beschreibung = $ticket["beschreibung"];
            $kundeId = $ticket["kunde"]["id"];
            $bearbeiterId = $ticket["bearbeiter"]["id"];
            $prioritaet = $ticket["prioritaet"];
            $kategorieId = $ticket["kategorie"]["id"];
            $abgeschlossen = ($ticket["abgeschlossen"] == "true" ? "1" : "0");
            
            $sql = "
                UPDATE ticket
                SET 
                    bezeichnung = '$bezeichnung',
                    beschreibung = '$beschreibung ',
                    kunde_id = $kundeId,
                    bearbeiter_id = $bearbeiterId,
                    prioritaet = $prioritaet,
                    kategorie_id = $kategorieId,
                    aenderungsDatum = NOW(),
                    abgeschlossen = $abgeschlossen 
                WHERE id = $id";
                
            
            return $this->query($sql);
        }
        
        //Gibt alle, im System verfügbaren, Kategorien zurück
        function getKategorien()
        {
            global $db;
            $sql = "SELECT * FROM kategorie";
            
            $result = $this->query($sql);
            
            $parentCategories = array();
            $subCategories = array();
                 
            //-----read categories from database-----
            
            while ($row = mysqli_fetch_array($result)) 
            {
                $category = array();
                
                $category["id"] = $row["id"];
                $category["name"] = $row["name"];
                $category["beschreibung"] = $row["beschreibung"];
                
                //Wenn Subkategorie
                if(isset($row["parentKategorie_id"]))
                {
                    $category["parentKategorieId"] = $row["parentKategorie_id"];
                    array_push($subCategories,  $category);
                }
                else{
                    array_push($parentCategories,  $category);
                }
            }
            
            //----prepare for JSON--------
            
            foreach ($parentCategories as &$parentCategory) 
            {
                $parentCategory["subKategorien"] = array();
                foreach ($subCategories as &$subCategory) 
                {
                    //Wenn die SubKategorie der Parent-Kategorie zugewiesen ist
                    if($subCategory["parentKategorieId"] == $parentCategory["id"])
                    {
                        array_push($parentCategory["subKategorien"],  $subCategory);
                    }
                } 
            }
            
            return $parentCategories;
        }
        
        
        //Gibt die PersonId des aktuell angemeldeten Benutzers zurück
        function getCurrentPersonId()
        {
            $username = $_SERVER['PHP_AUTH_USER'];
            $sql = "
                SELECT person_id 
                FROM mitarbeiter
                INNER JOIN person ON person.id = mitarbeiter.person_id
                WHERE person.email = '" . $username . "'";
            
            $result = $this->query($sql);
            $row = mysqli_fetch_assoc($result);
            
            return $row["person_id"];
        }
        
        //Gibt die MirarbeiterId des aktuell angemeldeten Benutzers zurück
        function getCurrentMitarbeiterId()
        {
            $username = $_SERVER['PHP_AUTH_USER'];
            $sql = "
                SELECT mitarbeiter.id
                FROM mitarbeiter
                INNER JOIN person ON person.id = mitarbeiter.person_id
                WHERE person.email = '" . $username . "'";
            
            $result = $this->query($sql);
            $row = mysqli_fetch_assoc($result);
            
            return $row["id"];
        }
        
        
            //executes a $this->query on the database
        function query($sql) {
            global $db;
            $result = $db->query($sql);

            if (!$result) {
                die('FAIL: '.mysqli_error($db));
            }

            return $result;
        }

        function realString($value) 
        {
            global $db;
            return $db->real_escape_string($value);
        }
        
        
        //Schreibt das angegebene SQL-Result als JSON
        function printQueryAsJson($result) {
            $rows = array();

            while ($r = mysqli_fetch_assoc($result)) {
                $rows[] = $r;
            }

            $json = json_encode($rows, JSON_UNESCAPED_UNICODE);
            echo $json;

            header('Content-Type: application/json');
        }
        
        function printJson($response){
            $json = json_encode($response, JSON_UNESCAPED_UNICODE);
            header('Content-Type: application/json');
            echo $json;
        }
    }



?>