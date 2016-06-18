<?php 
    class TicketrRepository{
        
        function getAllKunden() {

            $sql = "SELECT person.vorname, person.name, person.id as personId,
                    person.eMail, person.telefon, person.erstellDatum, person.aenderungsDatum,
                    kunde.id as kundeId, kunde.erstellDatum as kundeSeit, position.id as positionId, 
                    position.name as positionName
                    FROM kunde
                    INNER JOIN person ON person.id = kunde.person_id
                    INNER JOIN position ON position.id = kunde.position_id";

            $result = $this->query($sql);
            
            $response = array();
            
            while ($row = mysqli_fetch_array($result)) {
                $kunde = array();
                
                $kunde["id"] = $row["kundeId"];
                $kunde["erstellDatum"] = $row["kundeSeit"];
                $kunde["position"]["id"] = $row["positionId"];
                $kunde["position"]["name"] = $row["positionName"];
                $kunde["person"]["id"] = $row["personId"];
                $kunde["person"]["name"] = $row["name"];
                $kunde["person"]["vorname"] = $row["vorname"];
                $kunde["person"]["email"] = $row["eMail"];
                $kunde["person"]["telefon"] = $row["telefon"];
                $kunde["person"]["erstellDatum"] = $row["erstellDatum"];
                $kunde["person"]["aenderungsDatum"] = $row["aenderungsDatum"];
                
                array_push($response,  $kunde);
            }
            
            return $response;
            
        }

        //Gibt alle verfügbaren Mitarbeiter zurück
        function getAllMitarbeiter() {

            $sql = "SELECT person.vorname, person.name, person.id as personId,
                    person.eMail, person.telefon, person.erstellDatum, person.aenderungsDatum,
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
                $mitarbeiter["person"]["telefon"] = $row["telefon"];
                $mitarbeiter["person"]["erstellDatum"] = $row["erstellDatum"];
                $mitarbeiter["person"]["aenderungsDatum"] = $row["aenderungsDatum"];
                
                array_push($response,  $mitarbeiter);
            }
            
            return $response;
        }

        //Gibt die Daten für die Detailansicht eines Kunden zurück
        function getKundeDetail($id) {
            $sql = "SELECT 
                        kunde.id, kunde.erstellDatum, person.name, person.vorname, person.id as personId, person.eMail, person.telefon,
                        person.erstellDatum as personErstelldatum, person.aenderungsDatum as personAenderungsdatum, COUNT(ticket.id) as ticketCount,
                         position.id as positionId, position.name as positionName
                    FROM kunde
                    INNER JOIN person ON kunde.person_id = person.id
                    INNER JOIN ticket ON kunde.id = ticket.kunde_id
                    INNER JOIN position ON position.id = kunde.position_id
                    WHERE kunde.id = $id";
                    
            $result = $this->query($sql);
            $row =mysqli_fetch_assoc($result);
                
            $kunde = array();
            
            $kunde["id"] = $row["id"];
            $kunde["ticketsCount"] = $row["ticketCount"];
            $kunde["erstellDatum"] = $row["erstellDatum"];
            $kunde["position"]["id"] = $row["positionId"];
            $kunde["position"]["name"] = $row["positionName"];
            $kunde["person"]["id"] = $row["personId"];
            $kunde["person"]["name"] = $row["name"];
            $kunde["person"]["vorname"] = $row["vorname"];
            $kunde["person"]["eMail"] = $row["eMail"];
            $kunde["person"]["telefon"] = $row["telefon"];
            $kunde["person"]["erstellDatum"] = $row["personErstelldatum"];
            $kunde["person"]["aenderungsDatum"] = $row["personAenderungsdatum"];
            
            return $kunde;
        }
        
        //Ändert die Angaben einer Person
        function updatePerson($data)
        {
            $id = $data["id"];
            $name = $data["name"];
            $vorname = $data["vorname"];
            $email = $data["eMail"];
            $telefon = $data["telefon"];
            
            $sql = "
                UPDATE person 
                SET
                    name = '$name',
                    vorname = '$vorname',
                    eMail = '$email', 
                    telefon = '$telefon',
                    aenderungsDatum = NOW()
                WHERE id = $id";
                
            return $this->query($sql);
        }
        

        //Gibt die Daten für die Detailansicht eines Benutzers zurück
        function getMitarbeiterDetail($id) {
            
            $sql = "SELECT 
                        mitarbeiter.id, mitarbeiter.erstellDatum, person.name, person.vorname, person.id as personId, person.eMail, person.telefon,
                        person.erstellDatum as personErstelldatum, person.aenderungsDatum as personAenderungsdatum, COUNT(ticket.id) as ticketCount
                    FROM mitarbeiter
                    INNER JOIN person ON mitarbeiter.person_id = person.id
                    INNER JOIN ticket ON mitarbeiter.id = ticket.bearbeiter_id
                    WHERE mitarbeiter.id = $id";
                    
            $result = $this->query($sql);
            $row =mysqli_fetch_assoc($result);
                
            $mitarbeiter = array();
            
            $mitarbeiter["id"] = $row["id"];
            $mitarbeiter["ticketsCount"] = $row["ticketCount"];
            $mitarbeiter["erstellDatum"] = $row["erstellDatum"];
            $mitarbeiter["person"]["id"] = $row["personId"];
            $mitarbeiter["person"]["name"] = $row["name"];
            $mitarbeiter["person"]["vorname"] = $row["vorname"];
            $mitarbeiter["person"]["eMail"] = $row["eMail"];
            $mitarbeiter["person"]["telefon"] = $row["telefon"];
            $mitarbeiter["person"]["erstellDatum"] = $row["personErstelldatum"];
            $mitarbeiter["person"]["aenderungsDatum"] = $row["personAenderungsdatum"];
            
            return $mitarbeiter;
        }

        //fügt einen neuen Mitarbeiter hinzu und gibt die erstellte Mitarbeiter-ID zurück
        function addMitarbeiter($data) {

            global $db;
            
            $name = $this->realString($data["person"]["name"]);
            $vorname = $this->realString($data["person"]["vorname"]);
            $email = $this->realString($data["person"]["email"]);
            $telefon = $this->realString($data["person"]["telefon"]);
            $passwort = $this->realString($data["passwort"]);

            if (!empty($name) && !empty($vorname) && !empty($email) && !empty($passwort)) {
                $pwHash = hash('sha256', $passwort);
                //add Person
                $sqlPerson = "INSERT INTO person (name, vorname, email, telefon, erstellDatum, aenderungsDatum) VALUES ('$name', '$vorname', '$email', '$telefon', NOW(), NOW())";
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
        function deleteMitarbeiter($mitarbeiterId)
        { 
            $mitarbeiterDetail = $this->getMitarbeiterDetail($mitarbeiterId);
            
            $sql = "DELETE FROM mitarbeiter WHERE id = $mitarbeiterId";
            $this->query($sql);
            
            $sql = "DELETE FROM person WHERE id = " . $mitarbeiterDetail["person"]["id"];
            $this->query($sql);
        }
        
        //löscht einen Kunden
        function deleteKunde($kundeId)
        {
            $kundeDetail = $this->getKundeDetail($kundeId);
            
            $sql = "DELETE FROM kunde WHERE id = $kundeId";
            $this->query($sql);
            
            $sql = "DELETE FROM person WHERE id = " . $kundeDetail["person"]["id"];
            $this->query($sql);
        }

        //Fügt einen neuen Kunden hinzu
        function addKunde($data) {
            global $db;

            //Gets the JSON in the post-Body
            $data = json_decode(file_get_contents('php://input'), true);

            $name = $this->realString($data["person"]["name"]);
            $vorname = $this->realString($data["person"]["vorname"]);
            $email = $this->realString($data["person"]["email"]);
            $telefon = $this->realString($data["person"]["telefon"]);
            $positionId = $this->realString($data["position"]["id"]);

            if (!empty($name) && !empty($vorname) && !empty($email) && !empty($telefon)) {
                //add Person
                $sqlPerson = "INSERT INTO person (name, vorname, email, telefon, erstellDatum, aenderungsDatum) VALUES ('$name', '$vorname','$email', '$telefon', NOW(), NOW())";
                $result = $this->query($sqlPerson);

                //get id of new inserted person
                $personId = $db->insert_id;

                //add Mitabeiter
                $sqlKunde = "INSERT INTO kunde (person_id, erstellDatum, position_id) VALUES ($personId, NOW(), $positionId)";
                $result = $this->query($sqlKunde);

                $json = "{ \"kundeId\":".$db->insert_id."}";
                
                return $json;
            }
            else{
                var_dump(http_response_code(500));
            }
        }
        
        //Gibt alle Tickets zurück
        function getTickets(){
            
            $sql = "
                  SELECT 
                    ticket.id, ticket.erstellDatum, ticket.aenderungsDatum, ticket.kategorie_id, kategorie.name as kategorieName,
                    ticket.bezeichnung, ticket.beschreibung, ticket.loesung, ticket.abgeschlossen, ticket.prioritaet,
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
                    $ticket["loesung"] = $ticketRow["loesung"];
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
                    ticket.bezeichnung, ticket.beschreibung, ticket.loesung, ticket.abgeschlossen, ticket.prioritaet,
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

            //---Create JSON-Object----------

            $ticket = array();
            
            $ticket["id"] = $ticketRow["id"];
            $ticket["erstellDatum"] = $ticketRow["erstellDatum"];
            $ticket["aenderungsDatum"] = $ticketRow["aenderungsDatum"];
            $ticket["bezeichnung"] = $ticketRow["bezeichnung"];
            $ticket["beschreibung"] = $ticketRow["beschreibung"];
            $ticket["loesung"] = $ticketRow["loesung"];
            $ticket["abgeschlossen"] = ($ticketRow["abgeschlossen"] == "1" ? true : false);
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
            
            //Kommentare laden
            $ticket["kommentare"] = $this->getComments($id);
            
            //histories laden
            $ticket["history"] = $this->getTicketHistory($id);
                
            return $ticket;
        }
        
        //gibt die kommentare für ein ticket zurück
        function getComments($ticketId)
        {
            $commentsSql = "
                SELECT 
                kommentar.id, kommentar.text, kommentar.erstellDatum, mitarbeiter.id as mitarbeiterId, 
                person.name as personName, person.vorname as personVorname, person.id as personId 
                FROM kommentar
                INNER JOIN mitarbeiter ON kommentar.bearbeiter_id = mitarbeiter.id
                INNER JOIN person ON mitarbeiter.person_id = person.id
                WHERE kommentar.ticket_id = $ticketId";
            
            $commentResult = $this->query($commentsSql);
            
            $comments = array();
            
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
                
                array_push($comments,  $comment);
            }
            
            return $comments;
        }
        
        
        //Gibt die History eines Tickets zurück
        function getTicketHistory($ticketId){
            
            $historySql = "
                SELECT 
                person.name, person.vorname, person.id as personId, mitarbeiter.id as mitarbeiterId, ticket_history.id, 
                ticket_history.datum
                FROM ticket_history
                INNER JOIN mitarbeiter ON mitarbeiter.id = ticket_history.bearbeiter_id
                INNER JOIN person ON person.id = mitarbeiter.person_id
                WHERE ticket_history.ticket_id = $ticketId";
                
            $historyResult = $this->query($historySql);
             
            $histories = array();
            
            while ($historyRow = mysqli_fetch_array($historyResult)) 
            {
                $history = array();
                
                $history["id"] = $historyRow["id"];
                $history["datum"] = $historyRow["datum"];
                $history["mitarbeiter"]["id"] = $historyRow["mitarbeiterId"];
                $history["mitarbeiter"]["person"]["id"] = $historyRow["personId"];
                $history["mitarbeiter"]["person"]["name"] = $historyRow["name"];
                $history["mitarbeiter"]["person"]["vorname"] = $historyRow["vorname"];
                
                array_push($histories,  $history);
            }
            
            return $histories;
        }
        
        //Fügt einem Ticket ein History-Eintrag hinzu (mitarbeiter = der aktuelle)
        function addTicketHistory($ticketId){
            $mitarbeiterId = $this->getCurrentMitarbeiterId();
            $sql = "INSERT INTO ticket_history (ticket_id, bearbeiter_id, datum) VALUES ($ticketId, $mitarbeiterId, NOW())";
            
            $this->query($sql);
        }
        
        //Erstellt ein neues Ticket
        function createTicket($ticket)
        {
            global $db;
            
            $bezeichnung = $ticket["bezeichnung"];
            $beschreibung = isset($ticket["beschreibung"]) ? $ticket["beschreibung"] : "";
            $loesung = isset($ticket["loesung"]) ? $ticket["loesung"] : "";
            $kundeId = $ticket["kunde"]["id"];
            $bearbeiterId = $ticket["bearbeiter"]["id"];
            $prioritaet = $ticket["prioritaet"];
            $kategorieId = $ticket["kategorie"]["id"];
            
            $sql = "INSERT INTO ticket 
                    (bezeichnung, beschreibung, loesung, kunde_id, bearbeiter_id, abgeschlossen, prioritaet, kategorie_id, erstellDatum, aenderungsDatum)
                    VALUES ('$bezeichnung', '$beschreibung', '$loesung', $kundeId, $bearbeiterId, 0, $prioritaet ,$kategorieId, NOW(), NOW())";
                       
            $this->query($sql);
            
            $ticketId = $db->insert_id;
            
            //Fügt einen History Eintrag hinzu
            $this->addTicketHistory($ticketId);
            
            $json = "{ \"ticketId\":". $ticketId ."}";
            
            return $json;
        }
        
        //Updatet ein Ticket
        function udpateTicket($ticket)
        {
            $id = $ticket["id"];
            $bezeichnung = $ticket["bezeichnung"];
            $beschreibung = $ticket["beschreibung"];
            $loesung = $ticket["loesung"];
            $kundeId = $ticket["kunde"]["id"];
            $bearbeiterId = $ticket["bearbeiter"]["id"];
            $prioritaet = $ticket["prioritaet"];
            $kategorieId = $ticket["kategorie"]["id"];
            $abgeschlossen = ($ticket["abgeschlossen"] ? "1" : "0");
            
            $sql = "
                UPDATE ticket
                SET 
                    bezeichnung = '$bezeichnung',
                    beschreibung = '$beschreibung ',
                    loesung = '$loesung',
                    kunde_id = $kundeId,
                    bearbeiter_id = $bearbeiterId,
                    prioritaet = $prioritaet,
                    kategorie_id = $kategorieId,
                    aenderungsDatum = NOW(),
                    abgeschlossen = $abgeschlossen 
                WHERE id = $id";
                
            //Fügt einen History Eintrag hinzu
            $this->addTicketHistory($id);
            
            return $this->query($sql);
        }
        
        //Fügt einem Ticket einen Kommentar hinzu
        function addKommentar($data)
        {
            $mitarbeiterId = $this->getCurrentMitarbeiterId();
            $ticketId = $data["ticket"]["id"];
            $text = $data["text"];
            
            $sql = "INSERT INTO kommentar (ticket_id, bearbeiter_id, text, erstellDatum) VALUES ('$ticketId', '$mitarbeiterId', '$text', NOW())";
            
            return $this->query($sql);
        }
        
        
        //löscht ein ticket und dessen Kommentare
        function deleteTicket($id){
            $sql = "DELETE FROM kommentar WHERE ticket_id = $id";
            $this->query($sql);
            
            $sql = "DELETE FROM ticket WHERE id = $id";
            $this->query($sql);
            
            $sql = "DELETE FROM ticket_history WHERE ticket_id = $id";
            $this->query($sql);
        }
        
        function deleteKommentar($id){
            $sql = "DELETE FROM kommentar WHERE id = $id";
            $this->query($sql);
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
        
        //Gibt alle Kundenpostionen zurück
        function getPositionen(){
            $sql = "SELECT * FROM position";
            
            $result = $this->query($sql);
            
            $positionen = array();
                 
            //-----read categories from database-----
            
            while ($row = mysqli_fetch_array($result)) 
            {
                $position = array();
                
                $position["id"] = $row["id"];
                $position["name"] = $row["name"];  
                        
                array_push($positionen, $position);    
            }
            
            return $positionen;
        }
        
        //fügt eine position hinzu
        function addPosition($data){
            $name = $data["name"];
            $sql = "INSERT INTO postion (name) VALUES ('$name')";
            return $this->query($sql);
        }
        
        //löscht eine Position
        function deletePosition($id){
            $sql = "DELETE FROM position WHERE id = $id";
            return $this->query($sql);
        }
        
        //Ändert die Position eines Kunden
        function setKundenPosition($kundeId, $positionId){
            $sql = "UPDATE kunde SET position_id = $positionId WHERE id = $kundeId";
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