<?php

    if (!(isset($_SERVER['PHP_AUTH_USER']) && authorize($_SERVER['PHP_AUTH_USER'], $_SERVER['PHP_AUTH_PW']))) 
    {
        header('WWW-Authenticate: Basic');
        header('HTTP/1.0 401 Unauthorized');
        echo 'HTTP/1.0 401 Unauthorized';
        exit;
    }
    
    
    function authorize($email, $password)
    {
        global $db;
        $passwordHash = hash('sha256', $password);
        $sql = 
            "SELECT eMail FROM person " .
            "INNER JOIN mitarbeiter ON person.id = mitarbeiter.person_id " .
            "WHERE person.eMail ='" . $db->real_escape_string($email) . "' " .
            "AND mitarbeiter.passwort ='" . $db->real_escape_string($passwordHash) . "'";
        
        $result = $db->query($sql);
        
        if(mysqli_num_rows($result) == 1){
            return true;
        }
        return false;
    }
    
?>