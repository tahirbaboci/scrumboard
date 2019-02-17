<?php


header("Content-Type: application/json; charset=UTF-8");

include_once '../config/dbclass.php';
include_once '../entities/task.php';

$dbclass = new DBClass();
$connection = $dbclass->getConnection();

$task = new task($connection);

$id = $_POST["id"];
//$id = 7;

if($task->delete($id))
{
	$response=array(
		'status' => 1,
		'status_message' =>'Task Deleted Successfully.'
	);
}
else
{
	$response=array(
		'status' => 0,
		'status_message' =>'Task Deletion Failed.'
	);
}
echo json_encode($response);
?>
