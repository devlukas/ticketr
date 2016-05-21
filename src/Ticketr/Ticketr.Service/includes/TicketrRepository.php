<?php 
    class TicketrRepository{
        
        function getAllKunden() {
            global $db;

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
                $kunde["person"]["erstellDatum"] = $row["erstellDatum"];
                $kunde["person"]["aenderungsDatum"] = $row["aenderungsDatum"];
                
                array_push($response,  $kunde);
            }
            
            return $response;
            
        }

        //Gibt alle verfügbaren Mitarbeiter zurück
        function getAllMitarbeiter() {
            global $db;

            $sql = "SELECT person.vorname, person.name, person.id as personId,
                    person.eMail, person.erstellDatum, person.aenderungsDatum,
                    mitarbeiter.id as mitarbeiterId, mitarbeiter.erstellDatum as mitarbeiterSeit
                    FROM mitarbeiter
                    INNER JOIN person ON person.id = mitarbeiter.person_id";

            $result = $this->query($sql);
            
            $response = array();
            
            while ($row = mysqli_fetch_array($result)) {
                $kunde = array();
                
                $kunde["id"] = $row["mitarbeiterId"];
                $kunde["erstellDatum"] = $row["mitarbeiterSeit"];
                $kunde["person"]["id"] = $row["personId"];
                $kunde["person"]["name"] = $row["name"];
                $kunde["person"]["vorname"] = $row["vorname"];
                $kunde["person"]["erstellDatum"] = $row["erstellDatum"];
                $kunde["person"]["aenderungsDatum"] = $row["aenderungsDatum"];
                
                array_push($response,  $kunde);
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
            $ticket["person"]["id"] = $row["personId"];
            $ticket["person"]["name"] = $row["name"];
            $ticket["person"]["vorname"] = $row["vorname"];
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
        
        function setPersonPicture($image)
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
        function addKunde() {
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
            global $db;
            
            $sql = "
                  SELECT 
                    ticket.id, ticket.erstellDatum, ticket.aenderungsDatum,
                    ticket.bezeichnung, ticket.beschreibung, ticket.abgeschlossen, ticket.prioritaet,
                    ticket.bearbeiter_id as bearbeiterMitarbeiterId, bearbeiterP.id as bearbeiterPersonId, bearbeiterP.name as bearbeiterName,  bearbeiterP.vorname as bearbeiterVorname, 
                    ticket.kunde_id as kundeId, kundeP.id as kundePersonId, kundeP.name as kundeName, kundeP.vorname as kundeVorname,
                    ticket.typ,COUNT(kommentar.id) as 'kommentarCount'
                FROM ticket
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
                    $ticket["abgeschlossen"] = $row["abgeschlossen"];
                    $ticket["kategorie"] = $row["typ"];
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
            global $db;
            
            $id = $_GET["id"];
            
            //------------Load Ticket---------
            
            $ticketSql = "
                SELECT 
                    ticket.id, ticket.erstellDatum, ticket.aenderungsDatum,
                    ticket.bezeichnung, ticket.beschreibung, ticket.abgeschlossen, ticket.prioritaet,
                    ticket.bearbeiter_id as bearbeiterMitarbeiterId, bearbeiterP.id as bearbeiterPersonId, bearbeiterP.name as bearbeiterName,  bearbeiterP.vorname as bearbeiterVorname, 
                    ticket.kunde_id as kundeId, kundeP.id as kundePersonId, kundeP.name as kundeName, kundeP.vorname as kundeVorname,
                    ticket.typ
                FROM ticket
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
            $ticket["abgeschlossen"] = $ticketRow["abgeschlossen"];
            $ticket["prioritaet"] = $ticketRow["prioritaet"];
            $ticket["kategorie"] = $ticketRow["typ"];
            
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
                $comment["mitarbeiter"]["person"]["name"] = $commentRow["personVorname"];
                
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
            $bearbeiterId = $this->getCurrentMitarbeiterId();
            $prioritaet = $ticket["prioritaet"];
            $kategorie = $ticket["kategorie"];
            
            $sql = "INSERT INTO ticket 
                    (bezeichnung, beschreibung, kunde_id, bearbeiter_id, abgeschlossen, prioritaet, typ, erstellDatum, aenderungsDatum)
                    VALUES ('$bezeichnung', '$beschreibung', $kundeId, $bearbeiterId, 0, $prioritaet ,'$kategorie', NOW(), NOW())";
                       
            return $this->query($sql);
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