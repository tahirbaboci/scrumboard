<?php
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Max-Age: 3600");
header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");

include_once '../config/dbclass.php';
include_once '../entities/task.php';

$dbclass = new DBClass();
$connection = $dbclass->getConnection();

$task = new task($connection);

$tasktitle = $_POST["tasktitle"];
$idticket = $_POST["idticket"];
//$iddeveloper = $_POST["iddeveloper"];

//$tasktitle = 'deneme';
$idcategory = 7;
//$idticket = 1;

if(!empty($tasktitle) && !empty($idcategory) && !empty($idticket)){
  $task->tasktitle = $tasktitle;
  $task->idcategory = $idcategory;
  $task->idticket = $idticket;
  if($task->create()){
    $response=array(
      'status' => 1,
      'status_message' =>'task Created Successfully.'
    );
  }
  else{
    $response=array(
      'status' => 0,
      'status_message' =>'Unable to create ticket.'
    );
  }
}
else{
  http_response_code(400);
  echo json_encode(array("message" => "Unable to create task. Data is incomplete."));
}
echo json_encode($response);
?>
