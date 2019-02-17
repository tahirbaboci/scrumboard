<?php
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Methods: POST");
header("Access-Control-Max-Age: 3600");
header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");

include_once '../config/dbclass.php';
include_once '../entities/ticket.php';

$dbclass = new DBClass();
$connection = $dbclass->getConnection();

$ticket = new ticket($connection);

$tickettitle = $_POST["tickettitle"];
$ticketdesc = $_POST["ticketdesc"];
$sprint = $_POST["sprint"];
$idboard = $_POST["idboard"];

//$tickettitle = 'deneme';
//$ticketdesc = 'asdasdasd';
//$sprint = 3;
//$idboard = 2;

if(!empty($tickettitle) && !empty($ticketdesc) && !empty($sprint) && !empty($idboard)){
  $ticket->tickettitle = $tickettitle;
  $ticket->ticketdesc = $ticketdesc;
  $ticket->sprint = $sprint;
  $ticket->idboard = $idboard;

  if($ticket->create()){
    $response=array(
  		'status' => 1,
  		'status_message' =>'ticket Created Successfully.'
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
  echo json_encode(array("message" => "Unable to create ticket. Data is incomplete."));
}
echo json_encode($response);
?>
