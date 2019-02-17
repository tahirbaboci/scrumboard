<?php
header("Content-Type: application/json; charset=UTF-8");

include_once '../config/dbclass.php';
include_once '../entities/ticket.php';

$dbclass = new DBClass();
$connection = $dbclass->getConnection();

$ticket = new ticket($connection);

$stmt = $ticket->readSprintInfo();
$count = $stmt->rowCount();


if($count > 0){
  echo '{"sprints":[';
  $first = true;
  while ($row = $stmt->fetch(PDO::FETCH_ASSOC)){

      if($first) {
          $first = false;
      } else {
          echo ',';
      }
      echo json_encode($row);
  }
  echo ']}';
}
else {
  echo '[]';
}
?>
