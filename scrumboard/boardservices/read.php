<?php
header("Content-Type: application/json; charset=UTF-8");

include_once '../config/dbclass.php';
include_once '../entities/board.php';

$dbclass = new DBClass();
$connection = $dbclass->getConnection();

$board = new board($connection);

$stmt = $board->read();
$count = $stmt->rowCount();


if($count > 0){
    echo '{"boards":[';
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
