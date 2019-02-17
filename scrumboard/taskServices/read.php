<?php
header("Content-Type: application/json; charset=UTF-8");

include_once '../config/dbclass.php';
include_once '../entities/task.php';

$dbclass = new DBClass();
$connection = $dbclass->getConnection();

$task = new task($connection);

$stmt = $task->read();
$count = $stmt->rowCount();


if($count > 0){
    echo '{"tasks":[';
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
