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
//$id = 29;

$stmt = $task->readTasksByTicketId($id);
$count = $stmt->rowCount();
if($count > 0){
	$response=array(
		'status' => -1,
		'status_message' =>'There are Tasks That need to be deleted first.'
	);
}
else if($ticket->delete($id))
{
	$response=array(
		'status' => 1,
		'status_message' =>'Ticket Deleted Successfully.'
	);
}
else
{
	$response=array(
		'status' => 0,
		'status_message' =>'Ticket Deletion Failed.'
	);
}
echo json_encode($response);
?>
