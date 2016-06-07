<?php 
//Controller representiert einen Controller für alle Service Methoden.
//Alle öffentlichen Methoden dieser Klasse werden aufgerufen durch Routing
//z.B. Methode getAllKunden -> GET /service/getAllKunden
class Controller {
    
    private $repo;
    
    function __construct() {
        $this->repo = new TicketrRepository();
    }
    
    
    //Gibt alle verfügbaren Kunden zurück
    function getAllKunden() {
        
        $reponse = $this->repo->getAllKunden();
        $this->repo->printJson($reponse);
        
    }
    

    //Gibt alle verfügbaren Mitarbeiter zurück
    function getAllMitarbeiter() {
        $reponse = $this->repo->getAllMitarbeiter();
        $this->repo->printJson($reponse);
    }

    //Gibt die Daten für die Detailansicht eines Kunden zurück
    function getKundeDetail() {
        $reponse = $this->repo->getKundeDetail($_GET["id"]);
        $this->repo->printJson($reponse);
    }
    

    //Gibt die Daten für die Detailansicht eines Benutzers zurück
    function getMitarbeiterDetail() {
        $reponse = $this->repo->getMitarbeiterDetail($_GET["id"]);
        $this->repo->printJson($reponse);
    }
    

    //fügt einen neuen Mitarbeiter hinzu und gibt die erstellte Mitarbeiter-ID zurück
    function addMitarbeiter() {

        global $db;
        
        //Gets the json in post-Body
        $data = json_decode(file_get_contents('php://input'), true);
        
        header('Content-Type: application/json');
        
        echo $this->repo->addMitarbeiter($data);
    }
    
    
    //Gibt das Profilbild einer Person zurück
    function getPersonPicture(){
        
        $id = $_GET["id"];
        
        //todo hack
        header('Content-Type: image/png');
        
        echo $this->repo->getPersonPicture($id);
    }
    
    
    function setPersonPicture(){
        
        $personId = $_POST["personId"];
        $image = addslashes(file_get_contents($_FILES['image']['tmp_name']));
        
        echo $this->repo->setPersonPicture($image,$personId);
    }
    
    
    //löscht einen Mitarbeiter
    function deleteMitarbeiter(){
        
        $mitarbeiterId = $_POST["id"];
        echo $this->repo->deleteMitarbeiter($mitarbeiterId);
    }
    
    //Löscht einen Kunden
    function deleteKunde(){
        
        $kundeId = $_POST["id"];
        echo $this->repo->deleteKunde($kundeId);
    }
    

    //Fügt einen neuen Kunden hinzu
    function addKunde() {
        //Gets the json in post-Body
        $data = json_decode(file_get_contents('php://input'), true);
        
        header('Content-Type: application/json');
        echo $this->repo->addKunde($data);
    }
    
    //$Ändert die Angaben einer Person
    function updatePerson(){
        //Gets the json in post-Body
        $data = json_decode(file_get_contents('php://input'), true);

        $response = $this->repo->updatePerson($data);
       
        header('Content-Type: application/json');
        echo $response;
    }
    
    //Gibt alle Tickets zurück
    function getTickets(){
       $response = $this->repo->getTickets();
       $this->repo->printJson($response);
    }
    
    
    //Gibt die Detailansicht eines Tickets zurück
    function getTicketDetail(){
        $response = $this->repo->getTicketDetail();
        $this->repo->printJson($response);
    }
    
    //löscht ein Ticket und dessen Kommentare
    function deleteTicket(){
        $ticketId = $_POST["id"];
        echo $this->repo->deleteTicket($ticketId);
    }
    
    //löscht ein Kommentar
    function deleteKommentar(){
        $commentId = $_POST["id"];
        echo $this->repo->deleteKommentar($commentId);
    }
    
    //fügt einem Artikel einen Kommentar hinzu
    function addKommentar()
    {
        //Gets the json in post-Body
        $data = json_decode(file_get_contents('php://input'), true);
        $response = $this->repo->addKommentar($data);
        
        header('Content-Type: application/json');
        echo $response;
        
    }
    
    //Erstellt ein neues Ticket
    function createTicket(){
        //Gets the json in post-Body
        $data = json_decode(file_get_contents('php://input'), true);
        $response = $this->repo->createTicket($data);
        
        header('Content-Type: application/json');
        echo $response;
    }
    
    //Bearbeitet ein neues Ticket
    function updateTicket(){
        //Gets the json in post-Body
        $data = json_decode(file_get_contents('php://input'), true);

        $response = $this->repo->udpateTicket($data);
        
        $this->repo->printJson($response);
    }
    
    
    //Gibt die Details des angemeldeten Benutzers zurück
    function getCurrentMitarbeiterDetail(){
        //todo hack :)
        $personId = $this->repo->getCurrentPersonId();
        
        $response = $this->repo->getMitarbeiterDetail($personId);
        
        $this->repo->printJson($response);
    }
    
    //Gibt alle vorhandenen Kategorien zurück
    function getKategorien(){
        $response = $this->repo->getKategorien();
        $this->repo->printJson($response);
    }
    
    function info()
    {
        echo "<h1>M120 Ticktr Service</h1>";
        echo "<p>by Lukas Weber & Livio Brunner</p>";
        $class_methods = get_class_methods($this);
        echo "<h2>All Methods of the Controller Class: </h2><ul>";
        foreach ($class_methods as $method_name) {
            echo "<li>$method_name </li>";
        }
        echo "</ul>";
        
    }
    
    
    
    



}


?>