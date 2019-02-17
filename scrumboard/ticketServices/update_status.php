<?php
  header("Content-Type: application/json; charset=UTF-8");

  include_once '../config/dbclass.php';
  include_once '../entities/ticket.php';
  include_once '../entities/task.php';

  $dbclass = new DBClass();
  $connection = $dbclass->getConnection();

  $ticket = new ticket($connection);
  $task = new task($connection);

  $id = $_POST["id"];
  $solution = $_POST["solution"];
  //$id = 22;
  //$solution = "asdasd";

  $stmt = $task->readTasksByTicketId($id);
  $count = $stmt->rowCount();
  if($count > 0){
  	$response=array(
  		'status' => -1,
  		'status_message' =>'There are Tasks That need to be deleted first.'
  	);
    echo json_encode($response);
    exit();
  }

  $output = $ticket->updateStatus($id, $solution);
  if($output && $output != 'notExisting')
  {
  	$response=array(
  		'status' => 1,
  		'status_message' =>'ticket Updated(Done) Successfully.'
  	);
  }
  elseif($output == 'notExisting')
  {
  	$response=array(
  		'status' => -2,
  		'status_message' =>'ticket does not exists.'
  	);
  }
  else{
    $response=array(
      'status' => 0,
      'status_message' =>'ticket Update Failed.'
    );
  }
  echo json_encode($response);
?>
