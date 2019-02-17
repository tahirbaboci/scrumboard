<?php
header("Content-Type: application/json; charset=UTF-8");

include_once '../config/dbclass.php';
include_once '../entities/ticket.php';

$dbclass = new DBClass();
$connection = $dbclass->getConnection();

$ticket = new ticket($connection);


$sprint = $_POST["sprint"];
$idBoard = $_POST["idBoard"];

$stmt = $ticket->read($sprint, $idBoard);
$count = $stmt->rowCount();


if($count > 0){
    echo '{"tickets":[';
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

    //echo json_encode($ticket);
}
else {
  echo '[]';
}
?>
