<?php
  include_once '../config/dbclass.php';
  include_once '../entities/developer.php';

  $dbclass = new DBClass();
  $connection = $dbclass->getConnection();

  $shortname = $_POST["shortname"];
  $password = $_POST["password"];

  //$shortname = 'TB';
  //$password = 'tahirbaboci';

  $developer = new Developer($connection);

  $id;
  $hash;
  $salt;
  $userexists = false;
  $isloggedin = 0;

  $stmt = $developer->read();
  $num = $stmt->rowCount();

  // check if more than 0 record found
  if($num>0){
  	while ($row = $stmt->fetch(PDO::FETCH_ASSOC)){
      if($row['shortname'] == $shortname){

        $userexists = true;
        $id = $row['id'];
        $hash = $row['hash'];
        $salt = $row['salt'];
        $isloggedin = $row['isloggedin'];
      }
  	}
  }
  else{
    echo '404';
  }

  if($userexists == false){
    echo "ERROR code nr. 4: this user doesn't exists";
    exit();
  }
  if($isloggedin == 1){
    echo "ERROR code nr. 7: this user is already loggedin";
    exit();
  }

  $loginhash = crypt($password, $salt);

  if($hash != $loginhash){
    echo "ERROR code nr. 6: Incorrect password";
    exit();
  }else{

    echo "0"."\t";
    echo $id;
    $update_isloggedinQuery = "UPDATE developers SET isloggedin = 1 WHERE id = :id";
    $stmt_update_isloggedinQuery = $connection->prepare($update_isloggedinQuery, array(PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY));
    $stmt_update_isloggedinQuery ->execute(array(':id' => $id)) or die("isloggedin update failed");

  }
?>
