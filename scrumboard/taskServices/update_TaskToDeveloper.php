<?php
  header("Content-Type: application/json; charset=UTF-8");

  include_once '../config/dbclass.php';
  include_once '../entities/task.php';

  $dbclass = new DBClass();
  $connection = $dbclass->getConnection();

  $task = new task($connection);

  $id = $_POST["id"];
  $idDeveloper = $_POST["idDeveloper"];
  //$id = 7;
  //$idDeveloper = 9;

  $output = $task->updateTaskToDeveloper($id, $idDeveloper);
  if($output && $output != 'notExisting')
  {
  	$response=array(
  		'status' => 1,
  		'status_message' =>'Task Updated Successfully (Task has been given to a developer).'
  	);
  }
  elseif($output == 'notExisting')
  {
  	$response=array(
  		'status' => -1,
  		'status_message' =>'Task does not exists.'
  	);
  }
  else{
    $response=array(
      'status' => 0,
      'status_message' =>'Task Update Failed.'
    );
  }
  echo json_encode($response);
?>
