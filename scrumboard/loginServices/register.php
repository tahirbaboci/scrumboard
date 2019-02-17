<?php
    header("Content-Type: application/json; charset=UTF-8");
    header("Access-Control-Allow-Methods: POST");
    header("Access-Control-Max-Age: 3600");
    header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");

    include_once '../config/dbclass.php';
    include_once '../entities/developer.php';

    $dbclass = new DBClass();
    $connection = $dbclass->getConnection();

    $developer = new Developer($connection);

    $firstname = $_POST["firstname"];
    $lastname = $_POST["lastname"];
    $shortname = $_POST["shortname"];
    $password = $_POST["password"];
    $isloggedin = 0;
    $userexists = false;

    //$firstname = "Yunus Emre";
    //$lastname = "Bicer";
    //$shortname = "YEB";
    //$password = "yunusemrebicer";
    //$isloggedin = 1;


    $stmt = $developer->read();
    $num = $stmt->rowCount();

    // check if more than 0 record found
    if($num>0){
    	while ($row = $stmt->fetch(PDO::FETCH_ASSOC)){
        if($row['shortname'] == $shortname){
          $userexists = true;
        }
    	}
    }
    else{
      echo '404';
    }

    if($userexists == true){
      echo "ERROR code nr. 2: user already exists";
      exit();
    }

    $salt = "\$5\$rounds=5000\$" . "hallobruder" . $shortname . "\$";
    $hash = crypt($password, $salt);


    if(!empty($firstname) && !empty($lastname) && !empty($shortname) && !empty($hash) && !empty($salt)){
      $developer->firstname = $firstname;
      $developer->lastname = $lastname;
      $developer->shortname = $shortname;
      $developer->hash = $hash;
      $developer->salt = $salt;
      $developer->isloggedin = $isloggedin;

      if($developer->create()){
          echo "0";
      } else{
          echo "ERROR code nr. 3: Could not able to execute";
      }
    }
    else{
      echo json_encode(array("message" => "Unable to create user. Data is incomplete."));
    }
/*
    try{
      // Attempt insert query execution
      $insertuserquery = "INSERT INTO developers(firstname, lastname, shortname, hash, salt, isloggedin)
                          VALUES(:firstname, :lastname, :shortname, :hash, :salt, :isloggedin)";
      $stmt_insertuserquery = $connection->prepare($insertuserquery, array(PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY));
      $stmt_insertuserquery->execute(array(':firstname' => $firstname, ':lastname' => $lastname, ':shortname' => $shortname, ':hash' => $hash, ':salt' => $salt, ':isloggedin' => $isloggedin));
      if($stmt_insertuserquery){
          echo "0";
      } else{
          echo "ERROR code nr. 3: Could not able to execute";
      }

    }
    catch(PDOException $exception){
        echo $exception;
    }
    */

?>
