<?php
  include_once '../config/dbclass.php';
  $dbclass = new DBClass();
  $connection = $dbclass->getConnection();


  $id = $_POST["id"];
  $isloggedin = $_POST["isloggedin"];


  $update_isloggedinQuery = "UPDATE developers SET isloggedin = 0 WHERE id = :id";
  $stmtupdate_isloggedinQuery = $connection->prepare($update_isloggedinQuery, array(PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY));
  $stmtupdate_isloggedinQuery->execute(array(':id' => $id)) or die("isloggedin update failed");

  echo "has been updated";

 ?>
