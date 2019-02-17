<?php
header("Content-Type: application/json; charset=UTF-8");

include_once '../config/dbclass.php';
include_once '../entities/task.php';

$dbclass = new DBClass();
$connection = $dbclass->getConnection();

$task = new task($connection);

$id = $_POST["id"];
//$id = 2;

$stmt = $task->FindTaskOwner($id);
$count = $stmt->rowCount();

if($count > 0){

  $row = $stmt->fetch(PDO::FETCH_ASSOC);
  echo json_encode($row);
}
else {
  echo '[]';
}
?>
